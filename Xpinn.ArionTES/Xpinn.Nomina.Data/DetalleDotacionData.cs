using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class Detalle_DotacionData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public Detalle_DotacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Detalle_Dotacion CrearDetalle_Dotacion(Detalle_Dotacion pDetalle_Dotacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pid_detalle_dotacion = cmdTransaccionFactory.CreateParameter();
                        pid_detalle_dotacion.ParameterName = "p_id_detalle_dotacion";
                        pid_detalle_dotacion.Value = pDetalle_Dotacion.id_detalle_dotacion;
                        pid_detalle_dotacion.Direction = ParameterDirection.Input;
                        pid_detalle_dotacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_detalle_dotacion);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        if (pDetalle_Dotacion.id_dotacion == null)
                            pconsecutivo.Value = DBNull.Value;
                        else
                            pconsecutivo.Value = pDetalle_Dotacion.id_dotacion;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pDetalle_Dotacion.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pDetalle_Dotacion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pDetalle_Dotacion.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pDetalle_Dotacion.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pcaracteristica = cmdTransaccionFactory.CreateParameter();
                        pcaracteristica.ParameterName = "p_caracteristica";
                        if (pDetalle_Dotacion.caracteristica == null)
                            pcaracteristica.Value = DBNull.Value;
                        else
                            pcaracteristica.Value = pDetalle_Dotacion.caracteristica;
                        pcaracteristica.Direction = ParameterDirection.Input;
                        pcaracteristica.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcaracteristica);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_DETALLE_DO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pid_detalle_dotacion.Value != null)
                            pDetalle_Dotacion.id_detalle_dotacion = Convert.ToInt64(pid_detalle_dotacion.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return pDetalle_Dotacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Detalle_DotacionData", "CrearDetalle_Dotacion", ex);
                        return null;
                    }
                }
            }
        }


        public Detalle_Dotacion ModificarDetalle_Dotacion(Detalle_Dotacion pDetalle_Dotacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pid_detalle_dotacion = cmdTransaccionFactory.CreateParameter();
                        pid_detalle_dotacion.ParameterName = "p_id_detalle_dotacion";
                        pid_detalle_dotacion.Value = pDetalle_Dotacion.id_detalle_dotacion;
                        pid_detalle_dotacion.Direction = ParameterDirection.Input;
                        pid_detalle_dotacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_detalle_dotacion);

                        DbParameter p_id_dotacion = cmdTransaccionFactory.CreateParameter();
                        p_id_dotacion.ParameterName = "p_id_dotacion";
                        if (pDetalle_Dotacion.id_dotacion == null)
                            p_id_dotacion.Value = DBNull.Value;
                        else
                            p_id_dotacion.Value = pDetalle_Dotacion.id_dotacion;
                        p_id_dotacion.Direction = ParameterDirection.Input;
                        p_id_dotacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_id_dotacion);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        if (pDetalle_Dotacion.descripcion == null)
                            p_descripcion.Value = DBNull.Value;
                        else
                            p_descripcion.Value = pDetalle_Dotacion.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        if (pDetalle_Dotacion.valor == null)
                            p_valor.Value = DBNull.Value;
                        else
                            p_valor.Value = pDetalle_Dotacion.valor;
                        p_valor.Direction = ParameterDirection.Input;
                        p_valor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        DbParameter p_caracteristica = cmdTransaccionFactory.CreateParameter();
                        p_caracteristica.ParameterName = "p_caracteristica";
                        if (pDetalle_Dotacion.caracteristica == null)
                            p_caracteristica.Value = DBNull.Value;
                        else
                            p_caracteristica.Value = pDetalle_Dotacion.caracteristica;
                        p_caracteristica.Direction = ParameterDirection.Input;
                        p_caracteristica.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_caracteristica);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_DETALLE_DO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDetalle_Dotacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Detalle_DotacionData", "ModificarDetalle_Dotacion", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarDetalle_Dotacion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idDetalle = cmdTransaccionFactory.CreateParameter();
                        p_idDetalle.ParameterName = "p_idDetalle";
                        p_idDetalle.Value = pId;
                        p_idDetalle.Direction = ParameterDirection.Input;
                        p_idDetalle.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idDetalle);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_DETDOTA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Detalle_DotacionData", "EliminarDetalle_Dotacion", ex);
                    }
                }
            }
        }


        public Detalle_Dotacion ConsultarDetalle_Dotacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Detalle_Dotacion entidad = new Detalle_Dotacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM DETALLE_DOTACION WHERE ID_DETALLE_DOTACION = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["ID_DETALLE_DOTACION"] != DBNull.Value) entidad.id_detalle_dotacion = Convert.ToInt64(resultado["ID_DETALLE_DOTACION"]);
                            if (resultado["ID_DOTACION"] != DBNull.Value) entidad.id_dotacion = Convert.ToInt64(resultado["ID_DOTACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["CARACTERISTICA"] != DBNull.Value) entidad.caracteristica = Convert.ToString(resultado["CARACTERISTICA"]);
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
                        BOExcepcion.Throw("Detalle_DotacionData", "ConsultarDetalle_Dotacion", ex);
                        return null;
                    }
                }
            }
        }


        public List<Detalle_Dotacion> ListarDetalle_Dotacion(Detalle_Dotacion pDetalle_Dotacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Detalle_Dotacion> lstDetalle_Dotacion = new List<Detalle_Dotacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM DETALLE_DOTACION " + ObtenerFiltro(pDetalle_Dotacion) + " ORDER BY ID_DETALLE_DOTACION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Detalle_Dotacion entidad = new Detalle_Dotacion();
                            if (resultado["ID_DETALLE_DOTACION"] != DBNull.Value) entidad.id_detalle_dotacion = Convert.ToInt64(resultado["ID_DETALLE_DOTACION"]);
                            if (resultado["ID_DOTACION"] != DBNull.Value) entidad.id_dotacion = Convert.ToInt64(resultado["ID_DOTACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["CARACTERISTICA"] != DBNull.Value) entidad.caracteristica = Convert.ToString(resultado["CARACTERISTICA"]);
                            lstDetalle_Dotacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDetalle_Dotacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Detalle_DotacionData", "ListarDetalle_Dotacion", ex);
                        return null;
                    }
                }
            }
        }


    }
}
