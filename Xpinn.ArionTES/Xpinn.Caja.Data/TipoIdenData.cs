using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla TipoIdentificacion
    /// </summary>
    public class TipoIdenData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TipoIdentificacion
        /// </summary>
        public TipoIdenData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TipoIdentificacion de la base de datos
        /// </summary>
        /// <param name="pTipoIden">Entidad TipoIden</param>
        /// <returns>Entidad TipoIden creada</returns>
        public TipoIden CrearTipoIden(TipoIden pTipoIden, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODTIPOIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pCODTIPOIDENTIFICACION.ParameterName = "p_codtipoidentificacion";
                        pCODTIPOIDENTIFICACION.Value = pTipoIden.codtipoidentificacion;
                        pCODTIPOIDENTIFICACION.Direction = ParameterDirection.InputOutput;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_descripcion";
                        pDESCRIPCION.Value = pTipoIden.descripcion;


                        cmdTransaccionFactory.Parameters.Add(pCODTIPOIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Caja_TipoIdentificacion_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoIden, "TipoIdentificacion",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pTipoIden.codtipoidentificacion = Convert.ToInt64(pCODTIPOIDENTIFICACION.Value);
                        return pTipoIden;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoIdenData", "CrearTipoIden", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla TipoIdentificacion de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad TipoIden modificada</returns>
        public TipoIden ModificarTipoIden(TipoIden pTipoIden, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODTIPOIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pCODTIPOIDENTIFICACION.ParameterName = "p_CODTIPOIDENTIFICACION";
                        pCODTIPOIDENTIFICACION.Value = pTipoIden.codtipoidentificacion;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pTipoIden.descripcion;

                        cmdTransaccionFactory.Parameters.Add(pCODTIPOIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Caja_TipoIdentificacion_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoIden, "TipoIdentificacion",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pTipoIden;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoIdenData", "ModificarTipoIden", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla TipoIdentificacion de la base de datos
        /// </summary>
        /// <param name="pId">identificador de TipoIdentificacion</param>
        public void EliminarTipoIden(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoIden pTipoIden = new TipoIden();

                        if (pUsuario.programaGeneraLog)
                            pTipoIden = ConsultarTipoIden(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODTIPOIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pCODTIPOIDENTIFICACION.ParameterName = "p_codtipoidentificacion";
                        pCODTIPOIDENTIFICACION.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODTIPOIDENTIFICACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Caja_TipoIdentificacion_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoIden, "TipoIdentificacion", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoIdenData", "EliminarTipoIden", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla TipoIdentificacion de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TipoIdentificacion</param>
        /// <returns>Entidad TipoIden consultado</returns>
        public TipoIden ConsultarTipoIden(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            TipoIden entidad = new TipoIden();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPOIDENTIFICACION WHERE codtipoidentificacion = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODTIPOIDENTIFICACION"] != DBNull.Value) entidad.codtipoidentificacion = Convert.ToInt64(resultado["CODTIPOIDENTIFICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("TipoIdenData", "ConsultarTipoIden", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TipoIdentificacion dados unos filtros
        /// </summary>
        /// <param name="pTipoIdentificacion">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoIden obtenidos</returns>
        public List<TipoIden> ListarTipoIden(TipoIden pTipoIden, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoIden> lstTipoIden = new List<TipoIden>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPOIDENTIFICACION " + ObtenerFiltro(pTipoIden)+" ORDER BY 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoIden entidad = new TipoIden();

                            if (resultado["CODTIPOIDENTIFICACION"] != DBNull.Value) entidad.codtipoidentificacion = Convert.ToInt64(resultado["CODTIPOIDENTIFICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstTipoIden.Add(entidad);
                        }

                        return lstTipoIden;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoIdenData", "ListarTipoIden", ex);
                        return null;
                    }
                }
            }
        }

    }
}