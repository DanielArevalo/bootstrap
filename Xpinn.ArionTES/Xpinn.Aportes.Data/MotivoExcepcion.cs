using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Data
{
    public class MotivoExcepcionData : GlobalData
    {
 
        protected ConnectionDataBase dbConnectionFactory;
 
        public MotivoExcepcionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public MotivoExcepcion CrearMotivoExcepcion(MotivoExcepcion pMotivoExcepcion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_motivo_excepcion = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo_excepcion.ParameterName = "p_cod_motivo_excepcion";
                        pcod_motivo_excepcion.Value = pMotivoExcepcion.cod_motivo_excepcion;
                        pcod_motivo_excepcion.Direction = ParameterDirection.Input;
                        pcod_motivo_excepcion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo_excepcion);
 
                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pMotivoExcepcion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_MOTIVO_EXC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pMotivoExcepcion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoExcepcionData", "CrearMotivoExcepcion", ex);
                        return null;
                    }
                }
            }
        }
 
 
        public MotivoExcepcion ModificarMotivoExcepcion(MotivoExcepcion pMotivoExcepcion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_motivo_excepcion = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo_excepcion.ParameterName = "p_cod_motivo_excepcion";
                        pcod_motivo_excepcion.Value = pMotivoExcepcion.cod_motivo_excepcion;
                        pcod_motivo_excepcion.Direction = ParameterDirection.Input;
                        pcod_motivo_excepcion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo_excepcion);
 
                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pMotivoExcepcion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_MOTIVO_EXC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pMotivoExcepcion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoExcepcionData", "ModificarMotivoExcepcion", ex);
                        return null;
                    }
                }
            }
        }
 
 
        public void EliminarMotivoExcepcion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        MotivoExcepcion pMotivoExcepcion = new MotivoExcepcion();
                        pMotivoExcepcion = ConsultarMotivoExcepcion(pId, vUsuario);
                        
                        DbParameter pcod_motivo_excepcion = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo_excepcion.ParameterName = "p_cod_motivo_excepcion";
                        pcod_motivo_excepcion.Value = pMotivoExcepcion.cod_motivo_excepcion;
                        pcod_motivo_excepcion.Direction = ParameterDirection.Input;
                        pcod_motivo_excepcion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo_excepcion);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_MOTIVO_EXC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoExcepcionData", "EliminarMotivoExcepcion", ex);
                    }
                }
            }
        }
 
 
        public MotivoExcepcion ConsultarMotivoExcepcion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            MotivoExcepcion entidad = new MotivoExcepcion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM MOTIVO_EXCEPCION WHERE COD_MOTIVO_EXCEPCION = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_MOTIVO_EXCEPCION"] != DBNull.Value) entidad.cod_motivo_excepcion = Convert.ToInt32(resultado["COD_MOTIVO_EXCEPCION"]);
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
                        BOExcepcion.Throw("MotivoExcepcionData", "ConsultarMotivoExcepcion", ex);
                        return null;
                    }
                }
            }
        }
 
 
        public List<MotivoExcepcion> ListarMotivoExcepcion(MotivoExcepcion pMotivoExcepcion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<MotivoExcepcion> lstMotivoExcepcion = new List<MotivoExcepcion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM MOTIVO_EXCEPCION " + ObtenerFiltro(pMotivoExcepcion) + " ORDER BY COD_MOTIVO_EXCEPCION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            MotivoExcepcion entidad = new MotivoExcepcion();
                            if (resultado["COD_MOTIVO_EXCEPCION"] != DBNull.Value) entidad.cod_motivo_excepcion = Convert.ToInt32(resultado["COD_MOTIVO_EXCEPCION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstMotivoExcepcion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstMotivoExcepcion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoExcepcionData", "ListarMotivoExcepcion", ex);
                        return null;
                    }
                }
            }
        }
 
 
    }
}

