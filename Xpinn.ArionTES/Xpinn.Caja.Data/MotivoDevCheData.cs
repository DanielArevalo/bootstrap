using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Data
{
    public class MotivoDevCheData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public MotivoDevCheData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public MotivoDevChe CrearMotivoDevChe(MotivoDevChe pMotivoDevChe, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_motivo = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo.ParameterName = "p_cod_motivo";
                        pcod_motivo.Value = pMotivoDevChe.cod_motivo;
                        pcod_motivo.Direction = ParameterDirection.Input;
                        pcod_motivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pMotivoDevChe.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAJ_MOTIVO_DEV_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pMotivoDevChe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoDevCheData", "CrearMotivoDevChe", ex);
                        return null;
                    }
                }
            }
        }


        public MotivoDevChe ModificarMotivoDevChe(MotivoDevChe pMotivoDevChe, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_motivo = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo.ParameterName = "p_cod_motivo";
                        pcod_motivo.Value = pMotivoDevChe.cod_motivo;
                        pcod_motivo.Direction = ParameterDirection.Input;
                        pcod_motivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pMotivoDevChe.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAJ_MOTIVO_DEV_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pMotivoDevChe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoDevCheData", "ModificarMotivoDevChe", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarMotivoDevChe(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        MotivoDevChe pMotivoDevChe = new MotivoDevChe();
                        pMotivoDevChe = ConsultarMotivoDevChe(pId, vUsuario);

                        DbParameter pcod_motivo = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo.ParameterName = "p_cod_motivo";
                        pcod_motivo.Value = pMotivoDevChe.cod_motivo;
                        pcod_motivo.Direction = ParameterDirection.Input;
                        pcod_motivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAJ_MOTIVO_DEV_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoDevCheData", "EliminarMotivoDevChe", ex);
                    }
                }
            }
        }


        public MotivoDevChe ConsultarMotivoDevChe(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            MotivoDevChe entidad = new MotivoDevChe();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM MOTIVO_DEV_CHE WHERE COD_MOTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_MOTIVO"] != DBNull.Value) entidad.cod_motivo = Convert.ToInt32(resultado["COD_MOTIVO"]);
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
                        BOExcepcion.Throw("MotivoDevCheData", "ConsultarMotivoDevChe", ex);
                        return null;
                    }
                }
            }
        }


        public List<MotivoDevChe> ListarMotivoDevChe(MotivoDevChe pMotivoDevChe, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<MotivoDevChe> lstMotivoDevChe = new List<MotivoDevChe>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM MOTIVO_DEV_CHE " + ObtenerFiltro(pMotivoDevChe) + " ORDER BY COD_MOTIVO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            MotivoDevChe entidad = new MotivoDevChe();
                            if (resultado["COD_MOTIVO"] != DBNull.Value) entidad.cod_motivo = Convert.ToInt32(resultado["COD_MOTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstMotivoDevChe.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstMotivoDevChe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoDevCheData", "ListarMotivoDevChe", ex);
                        return null;
                    }
                }
            }
        }


    }
}
