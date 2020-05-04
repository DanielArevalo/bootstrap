using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;
 
namespace Xpinn.Nomina.Data
{
    public class DotacionData : GlobalData
    {
 
        protected ConnectionDataBase dbConnectionFactory;
 
        public DotacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Dotacion CrearDotacion(Dotacion pDotacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pid_dotacion = cmdTransaccionFactory.CreateParameter();
                        pid_dotacion.ParameterName = "p_id_dotacion";
                        pid_dotacion.Value = pDotacion.id_dotacion;
                        pid_dotacion.Direction = ParameterDirection.Output;
                        pid_dotacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_dotacion);
 
                        DbParameter pcod_empleado = cmdTransaccionFactory.CreateParameter();
                        pcod_empleado.ParameterName = "p_cod_empleado";
                        if (pDotacion.cod_empleado == null)
                            pcod_empleado.Value = DBNull.Value;
                        else
                            pcod_empleado.Value = pDotacion.cod_empleado;
                        pcod_empleado.Direction = ParameterDirection.Input;
                        pcod_empleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_empleado);
 
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        if (pDotacion.fecha == null)
                            pfecha.Value = DBNull.Value;
                        else
                            pfecha.Value = pDotacion.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);
 
                        DbParameter pubicacion = cmdTransaccionFactory.CreateParameter();
                        pubicacion.ParameterName = "p_ubicacion";
                        if (pDotacion.ubicacion == null)
                            pubicacion.Value = DBNull.Value;
                        else
                            pubicacion.Value = pDotacion.ubicacion;
                        pubicacion.Direction = ParameterDirection.Input;
                        pubicacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pubicacion);
 
                        DbParameter pcantidad = cmdTransaccionFactory.CreateParameter();
                        pcantidad.ParameterName = "p_cantidad";
                        if (pDotacion.cantidad == null)
                            pcantidad.Value = DBNull.Value;
                        else
                            pcantidad.Value = pDotacion.cantidad;
                        pcantidad.Direction = ParameterDirection.Input;
                        pcantidad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcantidad);
 
                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "p_centro_costo";
                        if (pDotacion.centro_costo == null)
                            pcentro_costo.Value = DBNull.Value;
                        else
                            pcentro_costo.Value = pDotacion.centro_costo;
                        pcentro_costo.Direction = ParameterDirection.Input;
                        pcentro_costo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_DOTACION_CREAR";                        
                        cmdTransaccionFactory.ExecuteNonQuery();
                        
                        if (pid_dotacion.Value != null)
                            pDotacion.id_dotacion = Convert.ToInt64(pid_dotacion.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return pDotacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DotacionData", "CrearDotacion", ex);
                        return null;
                    }
                }
            }
        }
 
 
        public Dotacion ModificarDotacion(Dotacion pDotacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pid_dotacion = cmdTransaccionFactory.CreateParameter();
                        pid_dotacion.ParameterName = "p_id_dotacion";
                        pid_dotacion.Value = pDotacion.id_dotacion;
                        pid_dotacion.Direction = ParameterDirection.Input;
                        pid_dotacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_dotacion);
 
                        DbParameter pcod_empleado = cmdTransaccionFactory.CreateParameter();
                        pcod_empleado.ParameterName = "p_cod_empleado";
                        if (pDotacion.cod_empleado == null)
                            pcod_empleado.Value = DBNull.Value;
                        else
                            pcod_empleado.Value = pDotacion.cod_empleado;
                        pcod_empleado.Direction = ParameterDirection.Input;
                        pcod_empleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_empleado);
 
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        if (pDotacion.fecha == null)
                            pfecha.Value = DBNull.Value;
                        else
                            pfecha.Value = pDotacion.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);
 
                        DbParameter pubicacion = cmdTransaccionFactory.CreateParameter();
                        pubicacion.ParameterName = "p_ubicacion";
                        if (pDotacion.ubicacion == null)
                            pubicacion.Value = DBNull.Value;
                        else
                            pubicacion.Value = pDotacion.ubicacion;
                        pubicacion.Direction = ParameterDirection.Input;
                        pubicacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pubicacion);
 
                        DbParameter pcantidad = cmdTransaccionFactory.CreateParameter();
                        pcantidad.ParameterName = "p_cantidad";
                        if (pDotacion.cantidad == null)
                            pcantidad.Value = DBNull.Value;
                        else
                            pcantidad.Value = pDotacion.cantidad;
                        pcantidad.Direction = ParameterDirection.Input;
                        pcantidad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcantidad);
 
                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "p_centro_costo";
                        if (pDotacion.centro_costo == null)
                            pcentro_costo.Value = DBNull.Value;
                        else
                            pcentro_costo.Value = pDotacion.centro_costo;
                        pcentro_costo.Direction = ParameterDirection.Input;
                        pcentro_costo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_DOTACION_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDotacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DotacionData", "ModificarDotacion", ex);
                        return null;
                    }
                }
            }
        }
 
 
        public void EliminarDotacion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Dotacion pDotacion = new Dotacion();
                        pDotacion = ConsultarDotacion(pId, vUsuario);
                        
                        DbParameter pid_dotacion = cmdTransaccionFactory.CreateParameter();
                        pid_dotacion.ParameterName = "p_id_dotacion";
                        pid_dotacion.Value = pDotacion.id_dotacion;
                        pid_dotacion.Direction = ParameterDirection.Input;
                        pid_dotacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_dotacion);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_DOTACION_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DotacionData", "EliminarDotacion", ex);
                    }
                }
            }
        }

        public Dotacion ConsultarDatosDotacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Dotacion entidad = new Dotacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Dotacion WHERE ID_DOTACION = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["ID_DOTACION"] != DBNull.Value) entidad.id_dotacion = Convert.ToInt64(resultado["ID_DOTACION"]);
                            if (resultado["COD_EMPLEADO"] != DBNull.Value) entidad.cod_empleado = Convert.ToInt64(resultado["COD_EMPLEADO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["UBICACION"] != DBNull.Value) entidad.ubicacion = Convert.ToString(resultado["UBICACION"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToInt64(resultado["CANTIDAD"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt32(resultado["CENTRO_COSTO"]);
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
                        BOExcepcion.Throw("DotacionData", "ConsultarDotacion", ex);
                        return null;
                    }
                }
            }
        }

        public Dotacion ConsultarDotacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Dotacion entidad = new Dotacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select dot.*, per.identificacion, per.TIPO_IDENTIFICACION, per.nombre
                                        from dotacion dot
                                        JOIN empleados emp on emp.consecutivo = dot.cod_empleado
                                        JOIN v_persona per on per.COD_PERSONA = emp.COD_PERSONA
                                        WHERE dot.ID_DOTACION = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ID_DOTACION"] != DBNull.Value) entidad.id_dotacion = Convert.ToInt64(resultado["ID_DOTACION"]);
                            if (resultado["COD_EMPLEADO"] != DBNull.Value) entidad.cod_empleado = Convert.ToInt64(resultado["COD_EMPLEADO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["UBICACION"] != DBNull.Value) entidad.ubicacion = Convert.ToString(resultado["UBICACION"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToInt64(resultado["CANTIDAD"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt32(resultado["CENTRO_COSTO"]);

                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["UBICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.cod_tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
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
                        BOExcepcion.Throw("DotacionData", "ConsultarDotacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<Dotacion> ListarDotacion(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Dotacion> lstDotacion = new List<Dotacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT dot.*, cst.DESCRIPCION as desc_centro_costo, per.nombre as nombre_empleado
                                        FROM Dotacion dot
                                        JOIN CENTRO_COSTO cst on cst.centro_costo = dot.CENTRO_COSTO
                                        JOIN EMPLEADOS emp on emp.consecutivo = dot.cod_empleado
                                        JOIN v_persona per on per.COD_PERSONA = emp.COD_PERSONA " + filtro.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Dotacion entidad = new Dotacion();

                            if (resultado["ID_DOTACION"] != DBNull.Value) entidad.id_dotacion = Convert.ToInt64(resultado["ID_DOTACION"]);
                            if (resultado["COD_EMPLEADO"] != DBNull.Value) entidad.cod_empleado = Convert.ToInt64(resultado["COD_EMPLEADO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["UBICACION"] != DBNull.Value) entidad.ubicacion = Convert.ToString(resultado["UBICACION"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToInt64(resultado["CANTIDAD"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt32(resultado["CENTRO_COSTO"]);

                            if (resultado["desc_centro_costo"] != DBNull.Value) entidad.desc_centro_costo = Convert.ToString(resultado["desc_centro_costo"]);
                            if (resultado["nombre_empleado"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre_empleado"]);

                            lstDotacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDotacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DotacionData", "ListarDotacion", ex);
                        return null;
                    }
                }
            }
        }
 
 
    }
}