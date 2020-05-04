using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Data
{
    public class PersonaAutorizacionData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public PersonaAutorizacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public PersonaAutorizacion CrearPersonaAutorizacion(PersonaAutorizacion pPersonaAutorizacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidautorizacion = cmdTransaccionFactory.CreateParameter();
                        pidautorizacion.ParameterName = "p_idautorizacion";
                        pidautorizacion.Value = pPersonaAutorizacion.idautorizacion;
                        pidautorizacion.Direction = ParameterDirection.Input;
                        pidautorizacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidautorizacion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pPersonaAutorizacion.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pPersonaAutorizacion.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        ptipo_producto.Value = pPersonaAutorizacion.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "p_numero_producto";
                        pnumero_producto.Value = pPersonaAutorizacion.numero_producto;
                        pnumero_producto.Direction = ParameterDirection.Input;
                        pnumero_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);

                        DbParameter pip = cmdTransaccionFactory.CreateParameter();
                        pip.ParameterName = "p_ip";
                        if (pPersonaAutorizacion.ip == null)
                            pip.Value = DBNull.Value;
                        else
                            pip.Value = pPersonaAutorizacion.ip;
                        pip.Direction = ParameterDirection.Input;
                        pip.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pip);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        if (pPersonaAutorizacion.cod_usuario == null)
                            pcod_usuario.Value = DBNull.Value;
                        else
                            pcod_usuario.Value = pPersonaAutorizacion.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pPersonaAutorizacion.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pPersonaAutorizacion.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pfecha_excepcion = cmdTransaccionFactory.CreateParameter();
                        pfecha_excepcion.ParameterName = "p_fecha_excepcion";
                        if (pPersonaAutorizacion.fecha_excepcion == null)
                            pfecha_excepcion.Value = DBNull.Value;
                        else
                            pfecha_excepcion.Value = pPersonaAutorizacion.fecha_excepcion;
                        pfecha_excepcion.Direction = ParameterDirection.Input;
                        pfecha_excepcion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_excepcion);

                        DbParameter pcod_motivo_excepcion = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo_excepcion.ParameterName = "p_cod_motivo_excepcion";
                        if (pPersonaAutorizacion.cod_motivo_excepcion == null)
                            pcod_motivo_excepcion.Value = DBNull.Value;
                        else
                            pcod_motivo_excepcion.Value = pPersonaAutorizacion.cod_motivo_excepcion;
                        pcod_motivo_excepcion.Direction = ParameterDirection.Input;
                        pcod_motivo_excepcion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo_excepcion);

                        DbParameter pobservacion = cmdTransaccionFactory.CreateParameter();
                        pobservacion.ParameterName = "p_observacion";
                        if (pPersonaAutorizacion.observacion == null)
                            pobservacion.Value = DBNull.Value;
                        else
                            pobservacion.Value = pPersonaAutorizacion.observacion;
                        pobservacion.Direction = ParameterDirection.Input;
                        pobservacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservacion);

                        DbParameter pcod_usuario_excepcion = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario_excepcion.ParameterName = "p_cod_usuario_excepcion";
                        if (pPersonaAutorizacion.cod_usuario_excepcion == null)
                            pcod_usuario_excepcion.Value = DBNull.Value;
                        else
                            pcod_usuario_excepcion.Value = pPersonaAutorizacion.cod_usuario_excepcion;
                        pcod_usuario_excepcion.Direction = ParameterDirection.Input;
                        pcod_usuario_excepcion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario_excepcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONA_AU_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPersonaAutorizacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaAutorizacionData", "CrearPersonaAutorizacion", ex);
                        return null;
                    }
                }
            }
        }


        public PersonaAutorizacion ModificarPersonaAutorizacion(PersonaAutorizacion pPersonaAutorizacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidautorizacion = cmdTransaccionFactory.CreateParameter();
                        pidautorizacion.ParameterName = "p_idautorizacion";
                        pidautorizacion.Value = pPersonaAutorizacion.idautorizacion;
                        pidautorizacion.Direction = ParameterDirection.Input;
                        pidautorizacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidautorizacion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pPersonaAutorizacion.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pPersonaAutorizacion.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        ptipo_producto.Value = pPersonaAutorizacion.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "p_numero_producto";
                        pnumero_producto.Value = pPersonaAutorizacion.numero_producto;
                        pnumero_producto.Direction = ParameterDirection.Input;
                        pnumero_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);

                        DbParameter pip = cmdTransaccionFactory.CreateParameter();
                        pip.ParameterName = "p_ip";
                        if (pPersonaAutorizacion.ip == null)
                            pip.Value = DBNull.Value;
                        else
                            pip.Value = pPersonaAutorizacion.ip;
                        pip.Direction = ParameterDirection.Input;
                        pip.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pip);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        if (pPersonaAutorizacion.cod_usuario == null)
                            pcod_usuario.Value = DBNull.Value;
                        else
                            pcod_usuario.Value = pPersonaAutorizacion.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pPersonaAutorizacion.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pPersonaAutorizacion.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pfecha_excepcion = cmdTransaccionFactory.CreateParameter();
                        pfecha_excepcion.ParameterName = "p_fecha_excepcion";
                        if (pPersonaAutorizacion.fecha_excepcion == null)
                            pfecha_excepcion.Value = DBNull.Value;
                        else
                            pfecha_excepcion.Value = pPersonaAutorizacion.fecha_excepcion;
                        pfecha_excepcion.Direction = ParameterDirection.Input;
                        pfecha_excepcion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_excepcion);

                        DbParameter pcod_motivo_excepcion = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo_excepcion.ParameterName = "p_cod_motivo_excepcion";
                        if (pPersonaAutorizacion.cod_motivo_excepcion == null)
                            pcod_motivo_excepcion.Value = DBNull.Value;
                        else
                            pcod_motivo_excepcion.Value = pPersonaAutorizacion.cod_motivo_excepcion;
                        pcod_motivo_excepcion.Direction = ParameterDirection.Input;
                        pcod_motivo_excepcion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo_excepcion);

                        DbParameter pobservacion = cmdTransaccionFactory.CreateParameter();
                        pobservacion.ParameterName = "p_observacion";
                        if (pPersonaAutorizacion.observacion == null)
                            pobservacion.Value = DBNull.Value;
                        else
                            pobservacion.Value = pPersonaAutorizacion.observacion;
                        pobservacion.Direction = ParameterDirection.Input;
                        pobservacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservacion);

                        DbParameter pcod_usuario_excepcion = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario_excepcion.ParameterName = "p_cod_usuario_excepcion";
                        if (pPersonaAutorizacion.cod_usuario_excepcion == null)
                            pcod_usuario_excepcion.Value = DBNull.Value;
                        else
                            pcod_usuario_excepcion.Value = pPersonaAutorizacion.cod_usuario_excepcion;
                        pcod_usuario_excepcion.Direction = ParameterDirection.Input;
                        pcod_usuario_excepcion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario_excepcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONA_AU_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPersonaAutorizacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaAutorizacionData", "ModificarPersonaAutorizacion", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarPersonaAutorizacion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        PersonaAutorizacion pPersonaAutorizacion = new PersonaAutorizacion();
                        pPersonaAutorizacion = ConsultarPersonaAutorizacion(pId, vUsuario);

                        DbParameter pidautorizacion = cmdTransaccionFactory.CreateParameter();
                        pidautorizacion.ParameterName = "p_idautorizacion";
                        pidautorizacion.Value = pPersonaAutorizacion.idautorizacion;
                        pidautorizacion.Direction = ParameterDirection.Input;
                        pidautorizacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidautorizacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONA_AU_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaAutorizacionData", "EliminarPersonaAutorizacion", ex);
                    }
                }
            }
        }


        public PersonaAutorizacion ConsultarPersonaAutorizacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            PersonaAutorizacion entidad = new PersonaAutorizacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select * from PERSONA_AUTORIZACION inner join persona on persona_autorizacion.cod_persona=persona.cod_persona where persona_autorizacion.idautorizacion= " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDAUTORIZACION"] != DBNull.Value) entidad.idautorizacion = Convert.ToInt64(resultado["IDAUTORIZACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["IP"] != DBNull.Value) entidad.ip = Convert.ToString(resultado["IP"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_EXCEPCION"] != DBNull.Value) entidad.fecha_excepcion = Convert.ToDateTime(resultado["FECHA_EXCEPCION"]);
                            if (resultado["COD_MOTIVO_EXCEPCION"] != DBNull.Value) entidad.cod_motivo_excepcion = Convert.ToInt32(resultado["COD_MOTIVO_EXCEPCION"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["COD_USUARIO_EXCEPCION"] != DBNull.Value) entidad.cod_usuario_excepcion = Convert.ToInt32(resultado["COD_USUARIO_EXCEPCION"]);
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
                        BOExcepcion.Throw("PersonaAutorizacionData", "ConsultarPersonaAutorizacion", ex);
                        return null;
                    }
                }
            }
        }


        public List<PersonaAutorizacion> ListarPersonaAutorizacion(PersonaAutorizacion pPersonaAutorizacion, Usuario vUsuario, string filtro)
        {
            DbDataReader resultado;
            List<PersonaAutorizacion> lstPersonaAutorizacion = new List<PersonaAutorizacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    
                    try
                    {
                        string sql = @"";
                        if (pPersonaAutorizacion == null)
                        {
                            sql = @"Select persona.estado as nomestado , persona.*,persona_autorizacion.* from PERSONA_AUTORIZACION inner join persona on persona_autorizacion.cod_persona=persona.cod_persona ORDER BY IDAUTORIZACION ";
                        }
                        if (pPersonaAutorizacion.idautorizacion != 0)
                        {
                            sql = @"Select persona.estado as nomestado , persona.*,persona_autorizacion.* from PERSONA_AUTORIZACION inner join persona on persona_autorizacion.cod_persona=persona.cod_persona where persona_autorizacion.idautorizacion= " + pPersonaAutorizacion.idautorizacion + filtro + " ORDER BY IDAUTORIZACION ";
                        }
                        else {
                            sql = @"Select persona.estado as nomestado , persona.*,persona_autorizacion.* from PERSONA_AUTORIZACION inner join persona on persona_autorizacion.cod_persona=persona.cod_persona where persona_autorizacion.idautorizacion!=0 " + filtro + " ORDER BY IDAUTORIZACION ";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PersonaAutorizacion entidad = new PersonaAutorizacion();
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);                           
                            if (resultado["IDAUTORIZACION"] != DBNull.Value) entidad.idautorizacion = Convert.ToInt64(resultado["IDAUTORIZACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["IP"] != DBNull.Value) entidad.ip = Convert.ToString(resultado["IP"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["nomestado"] != DBNull.Value) entidad.estados = Convert.ToString(resultado["nomestado"]);
                            if (resultado["FECHA_EXCEPCION"] != DBNull.Value) entidad.fecha_excepcion = Convert.ToDateTime(resultado["FECHA_EXCEPCION"]);
                            if (resultado["COD_MOTIVO_EXCEPCION"] != DBNull.Value) entidad.cod_motivo_excepcion = Convert.ToInt32(resultado["COD_MOTIVO_EXCEPCION"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["COD_USUARIO_EXCEPCION"] != DBNull.Value) entidad.cod_usuario_excepcion = Convert.ToInt32(resultado["COD_USUARIO_EXCEPCION"]);
                            lstPersonaAutorizacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersonaAutorizacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaAutorizacionData", "ListarPersonaAutorizacion", ex);
                        return null;
                    }
                }
            }
        }


    }
}