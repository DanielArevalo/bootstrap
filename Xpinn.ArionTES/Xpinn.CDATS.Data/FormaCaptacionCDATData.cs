using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.CDATS.Entities;
 
namespace Xpinn.CDATS.Data
{
    public class FormaCaptacionCDATData : GlobalData
    {
 
        protected ConnectionDataBase dbConnectionFactory;
 
        public FormaCaptacionCDATData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public FormaCaptacion CrearFormaCaptacion(FormaCaptacion pFormaCaptacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodforma_captacion = cmdTransaccionFactory.CreateParameter();
                        pcodforma_captacion.ParameterName = "p_codforma_captacion";
                        pcodforma_captacion.Value = pFormaCaptacion.codforma_captacion;
                        pcodforma_captacion.Direction = ParameterDirection.Input;
                        pcodforma_captacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodforma_captacion);
 
                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pFormaCaptacion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_FORMACAPTA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pFormaCaptacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FormaCaptacionData", "CrearFormaCaptacion", ex);
                        return null;
                    }
                }
            }
        }
 
 
        public FormaCaptacion ModificarFormaCaptacion(FormaCaptacion pFormaCaptacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodforma_captacion = cmdTransaccionFactory.CreateParameter();
                        pcodforma_captacion.ParameterName = "p_codforma_captacion";
                        pcodforma_captacion.Value = pFormaCaptacion.codforma_captacion;
                        pcodforma_captacion.Direction = ParameterDirection.Input;
                        pcodforma_captacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodforma_captacion);
 
                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pFormaCaptacion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_FORMACAPTA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pFormaCaptacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FormaCaptacionData", "ModificarFormaCaptacion", ex);
                        return null;
                    }
                }
            }
        }
 
 
        public void EliminarFormaCaptacion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        FormaCaptacion pFormaCaptacion = new FormaCaptacion();
                        pFormaCaptacion = ConsultarFormaCaptacion(pId, vUsuario);
                        
                        DbParameter pcodforma_captacion = cmdTransaccionFactory.CreateParameter();
                        pcodforma_captacion.ParameterName = "p_codforma_captacion";
                        pcodforma_captacion.Value = pFormaCaptacion.codforma_captacion;
                        pcodforma_captacion.Direction = ParameterDirection.Input;
                        pcodforma_captacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodforma_captacion);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_FORMACAPTA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FormaCaptacionData", "EliminarFormaCaptacion", ex);
                    }
                }
            }
        }

        public FormaCaptacion ConsultarFormaCaptacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            FormaCaptacion entidad = new FormaCaptacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM formacaptacion_cdat WHERE CODFORMA_CAPTACION = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODFORMA_CAPTACION"] != DBNull.Value) entidad.codforma_captacion = Convert.ToInt32(resultado["CODFORMA_CAPTACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
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
                        BOExcepcion.Throw("FormaCaptacionData", "ConsultarFormaCaptacion", ex);
                        return null;
                    }
                }
            }
        }
 
 
        public List<FormaCaptacion> ListarFormaCaptacion(FormaCaptacion pFormaCaptacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<FormaCaptacion> lstFormaCaptacion = new List<FormaCaptacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM formacaptacion_cdat " + ObtenerFiltro(pFormaCaptacion) + " ORDER BY CODFORMA_CAPTACION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            FormaCaptacion entidad = new FormaCaptacion();
                            if (resultado["CODFORMA_CAPTACION"] != DBNull.Value) entidad.codforma_captacion = Convert.ToInt32(resultado["CODFORMA_CAPTACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstFormaCaptacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstFormaCaptacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FormaCaptacionData", "ListarFormaCaptacion", ex);
                        return null;
                    }
                }
            }
        }

    }
}