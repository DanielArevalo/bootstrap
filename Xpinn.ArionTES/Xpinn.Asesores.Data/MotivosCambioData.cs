using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla motivos_cambios_procesos
    /// </summary>
    public class MotivosCambioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla motivos_cambios_procesos
        /// </summary>
        public MotivosCambioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla motivos_cambios_procesos de la base de datos
        /// </summary>
        /// <param name="pMotivosCambio">Entidad MotivosCambio</param>
        /// <returns>Entidad MotivosCambio creada</returns>
        public MotivosCambio CrearMotivosCambio(MotivosCambio pMotivosCambio, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_MOTIVO_CAMBIO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MOTIVO_CAMBIO.ParameterName = "p_cod_motivo_cambio";
                        pCOD_MOTIVO_CAMBIO.Value = pMotivosCambio.cod_motivo_cambio;
                        pCOD_MOTIVO_CAMBIO.Direction = ParameterDirection.InputOutput;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_descripcion";
                        pDESCRIPCION.Value = pMotivosCambio.descripcion;


                        cmdTransaccionFactory.Parameters.Add(pCOD_MOTIVO_CAMBIO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_MOTIVOCAMBIOPRO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pMotivosCambio, "motivos_cambios_procesos",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pMotivosCambio.cod_motivo_cambio = Convert.ToInt64(pCOD_MOTIVO_CAMBIO.Value);
                        return pMotivosCambio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivosCambioData", "CrearMotivosCambio", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla motivos_cambios_procesos de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad MotivosCambio modificada</returns>
        public MotivosCambio ModificarMotivosCambio(MotivosCambio pMotivosCambio, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_MOTIVO_CAMBIO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MOTIVO_CAMBIO.ParameterName = "p_COD_MOTIVO_CAMBIO";
                        pCOD_MOTIVO_CAMBIO.Value = pMotivosCambio.cod_motivo_cambio;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pMotivosCambio.descripcion;

                        cmdTransaccionFactory.Parameters.Add(pCOD_MOTIVO_CAMBIO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_MOTIVOCAMBIOPRO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pMotivosCambio, "motivos_cambios_procesos",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pMotivosCambio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivosCambioData", "ModificarMotivosCambio", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla motivos_cambios_procesos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de motivos_cambios_procesos</param>
        public void EliminarMotivosCambio(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        MotivosCambio pMotivosCambio = new MotivosCambio();

                        //if (pUsuario.programaGeneraLog)
                        //    pMotivosCambio = ConsultarMotivosCambio(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_MOTIVO_CAMBIO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MOTIVO_CAMBIO.ParameterName = "p_cod_motivo_cambio";
                        pCOD_MOTIVO_CAMBIO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_MOTIVO_CAMBIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_MOTIVOCAMBIOPRO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pMotivosCambio, "motivos_cambios_procesos", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivosCambioData", "EliminarMotivosCambio", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla motivos_cambios_procesos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla motivos_cambios_procesos</param>
        /// <returns>Entidad MotivosCambio consultado</returns>
        public MotivosCambio ConsultarMotivosCambio(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            MotivosCambio entidad = new MotivosCambio();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  MOTIVOS_CAMBIOS_PROCESOS WHERE cod_motivo_cambio = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_MOTIVO_CAMBIO"] != DBNull.Value) entidad.cod_motivo_cambio = Convert.ToInt64(resultado["COD_MOTIVO_CAMBIO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivosCambioData", "ConsultarMotivosCambio", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla motivos_cambios_procesos dados unos filtros
        /// </summary>
        /// <param name="pmotivos_cambios_procesos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MotivosCambio obtenidos</returns>
        public List<MotivosCambio> ListarMotivosCambio(MotivosCambio pMotivosCambio, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MotivosCambio> lstMotivosCambio = new List<MotivosCambio>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  MOTIVOS_CAMBIOS_PROCESOS " + ObtenerFiltro(pMotivosCambio);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MotivosCambio entidad = new MotivosCambio();

                            if (resultado["COD_MOTIVO_CAMBIO"] != DBNull.Value) entidad.cod_motivo_cambio   = Convert.ToInt64(resultado["COD_MOTIVO_CAMBIO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value)       entidad.descripcion         = Convert.ToString(resultado["DESCRIPCION"]);

                            lstMotivosCambio.Add(entidad);
                        }

                        return lstMotivosCambio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivosCambioData", "ListarMotivosCambio", ex);
                        return null;
                    }
                }
            }
        }
    }
}