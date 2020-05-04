using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla MotivoNovedad
    /// </summary>
    public class MotivoNovedadData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla MotivoNovedad
        /// </summary>
        public MotivoNovedadData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public MotivoNovedad CrearMotivoNovedad(MotivoNovedad pMotivoNovedad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidmotivo_novedad = cmdTransaccionFactory.CreateParameter();
                        pidmotivo_novedad.ParameterName = "p_idmotivo_novedad";
                        pidmotivo_novedad.Value = pMotivoNovedad.idmotivo_novedad;
                        pidmotivo_novedad.Direction = ParameterDirection.Input;
                        pidmotivo_novedad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidmotivo_novedad);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pMotivoNovedad.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_MOTIVONOV_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pMotivoNovedad.idmotivo_novedad = Convert.ToInt32(pidmotivo_novedad.Value);

                        dbConnectionFactory.CerrarConexion(connection);

                        return pMotivoNovedad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoNovedadData", "CrearMotivoNovedad", ex);
                        return null;
                    }
                }
            }
        }        

        public MotivoNovedad ModificarMotivoNovedad(MotivoNovedad pMotivoNovedad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidmotivo_novedad = cmdTransaccionFactory.CreateParameter();
                        pidmotivo_novedad.ParameterName = "p_idmotivo_novedad";
                        pidmotivo_novedad.Value = pMotivoNovedad.idmotivo_novedad;
                        pidmotivo_novedad.Direction = ParameterDirection.Input;
                        pidmotivo_novedad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidmotivo_novedad);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pMotivoNovedad.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_MOTIVONOV_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pMotivoNovedad, "MOTIVO_NOVEDAD", vUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);

                        return pMotivoNovedad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoNovedadData", "ModificarMotivoNovedad", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarMotivoNovedad(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        MotivoNovedad pMotivoNovedad = new MotivoNovedad();
                        pMotivoNovedad = ConsultarMotivoNovedad(pId, vUsuario);

                        DbParameter pidmotivo_novedad = cmdTransaccionFactory.CreateParameter();
                        pidmotivo_novedad.ParameterName = "p_idmotivo_novedad";
                        pidmotivo_novedad.Value = pMotivoNovedad.idmotivo_novedad;
                        pidmotivo_novedad.Direction = ParameterDirection.Input;
                        pidmotivo_novedad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidmotivo_novedad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_MOTIVONOV_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoNovedadData", "EliminarMotivoNovedad", ex);
                    }
                }
            }
        }

        public MotivoNovedad ConsultarMotivoNovedad(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            MotivoNovedad entidad = new MotivoNovedad();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Motivo_Novedad WHERE IDMOTIVO_NOVEDAD = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDMOTIVO_NOVEDAD"] != DBNull.Value) entidad.idmotivo_novedad = Convert.ToInt32(resultado["IDMOTIVO_NOVEDAD"]);
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
                        BOExcepcion.Throw("MotivoNovedadData", "ConsultarMotivoNovedad", ex);
                        return null;
                    }
                }
            }
        }

        public List<MotivoNovedad> ListarMotivoNovedad(MotivoNovedad pMotivoNovedad, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<MotivoNovedad> lstMotivoNovedad = new List<MotivoNovedad>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Motivo_Novedad " + ObtenerFiltro(pMotivoNovedad) + " ORDER BY IDMOTIVO_NOVEDAD ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            MotivoNovedad entidad = new MotivoNovedad();
                            if (resultado["IDMOTIVO_NOVEDAD"] != DBNull.Value) entidad.idmotivo_novedad = Convert.ToInt32(resultado["IDMOTIVO_NOVEDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstMotivoNovedad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstMotivoNovedad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoNovedadData", "ListarMotivoNovedad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene el siguiente codigo disponible de la tabla
        /// </summary>
        /// <returns>codigo disponible</returns>
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(idmotivo_novedad) + 1 FROM Motivo_Novedad ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoNovedadData", "ObtenerSiguienteCodigo", ex);
                        return 0;
                    }
                }
            }
        }

    }
}
