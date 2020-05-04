using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla EstructuraDetalleS
    /// </summary>
    public class EstructuraDetalleData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla EstructuraDetalleS
        /// </summary>
        public EstructuraDetalleData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla EstructuraDetalleS de la base de datos
        /// </summary>
        /// <param name="pEstructuraDetalle">Entidad EstructuraDetalle</param>
        /// <returns>Entidad EstructuraDetalle creada</returns>
        public EstructuraDetalle CrearEstructuraDetalle(EstructuraDetalle pEstructuraDetalle, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter PCOD_EST_DET = cmdTransaccionFactory.CreateParameter();
                        PCOD_EST_DET.ParameterName = "p_cod_est_det";
                        PCOD_EST_DET.Value = 0;
                        PCOD_EST_DET.Direction = ParameterDirection.InputOutput;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_detalle";
                        pDESCRIPCION.Value = pEstructuraDetalle.detalle;

                        cmdTransaccionFactory.Parameters.Add(PCOD_EST_DET);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_ESTRUCTURADETALLE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEstructuraDetalle.cod_est_det = Convert.ToInt32(PCOD_EST_DET.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEstructuraDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraDetalleData", "CrearEstructuraDetalle", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla EstructuraDetalleS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad EstructuraDetalle modificada</returns>
        public EstructuraDetalle ModificarEstructuraDetalle(EstructuraDetalle pEstructuraDetalle, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter PCOD_EST_DET = cmdTransaccionFactory.CreateParameter();
                        PCOD_EST_DET.ParameterName = "p_cod_est_det";
                        PCOD_EST_DET.Value = pEstructuraDetalle.cod_est_det;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_detalle";
                        pDESCRIPCION.Value = pEstructuraDetalle.detalle;

                        cmdTransaccionFactory.Parameters.Add(PCOD_EST_DET);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_ESTRUCTURADETALLE_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEstructuraDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraDetalleData", "ModificarEstructuraDetalle", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla EstructuraDetalleS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de EstructuraDetalleS</param>
        public void EliminarEstructuraDetalle(int pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter PCOD_EST_DET = cmdTransaccionFactory.CreateParameter();
                        PCOD_EST_DET.ParameterName = "p_cod_est_det";
                        PCOD_EST_DET.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(PCOD_EST_DET);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_ESTRUCTURADETALLE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraDetalleData", "EliminarEstructuraDetalle", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla EstructuraDetalleS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla EstructuraDetalleS</param>
        /// <returns>Entidad EstructuraDetalle consultado</returns>
        public EstructuraDetalle ConsultarEstructuraDetalle(int pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            EstructuraDetalle entidad = new EstructuraDetalle();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT cod_est_det, detalle FROm Estructura_Detalle" +
                                     " WHERE cod_est_det = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_est_det"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["cod_est_det"]);
                            if (resultado["detalle"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["detalle"]);
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
                        BOExcepcion.Throw("EstructuraDetalleData", "ConsultarEstructuraDetalle", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla EstructuraDetalle dados unos filtros
        /// </summary>
        /// <param name="pEstructuraDetalle">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EstructuraDetalles obtenidos</returns>
        public List<EstructuraDetalle> ListarEstructuraDetalle(EstructuraDetalle pEstructuraDetalle, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EstructuraDetalle> lstEstructuraDetalle = new List<EstructuraDetalle>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  Estructura_Detalle " + ObtenerFiltro(pEstructuraDetalle) + " ORDER BY cod_est_det";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EstructuraDetalle entidad = new EstructuraDetalle();

                            if (resultado["cod_est_det"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["cod_est_det"]);
                            if (resultado["detalle"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["detalle"]);

                            lstEstructuraDetalle.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEstructuraDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraDetalleData", "ListarEstructuraDetalle", ex);
                        return null;
                    }
                }
            }
        }

    }
}