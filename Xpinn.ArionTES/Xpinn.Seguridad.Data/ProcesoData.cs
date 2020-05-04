using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla PROCESO
    /// </summary>
    public class ProcesoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PROCESO
        /// </summary>
        public ProcesoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla PROCESO de la base de datos
        /// </summary>
        /// <param name="pProceso">Entidad Proceso</param>
        /// <returns>Entidad Proceso creada</returns>
        public Proceso CrearProceso(Proceso pProceso, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_PROCESO = cmdTransaccionFactory.CreateParameter();
                        pCOD_PROCESO.ParameterName = "p_cod_proceso";
                        pCOD_PROCESO.Value = 0;
                        pCOD_PROCESO.Direction = ParameterDirection.InputOutput;

                        DbParameter pCOD_MODULO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MODULO.ParameterName = "p_cod_modulo";
                        pCOD_MODULO.Value = pProceso.cod_modulo;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = "p_nombre";
                        pNOMBRE.Value = pProceso.nombre;


                        cmdTransaccionFactory.Parameters.Add(pCOD_PROCESO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MODULO);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_PROCESO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pProceso, "PROCESO",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pProceso.cod_proceso = Convert.ToInt64(pCOD_PROCESO.Value);
                        return pProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoData", "CrearProceso", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla PROCESO de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Proceso modificada</returns>
        public Proceso ModificarProceso(Proceso pProceso, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_PROCESO = cmdTransaccionFactory.CreateParameter();
                        pCOD_PROCESO.ParameterName = "p_cod_proceso";
                        pCOD_PROCESO.Value = pProceso.cod_proceso;

                        DbParameter pCOD_MODULO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MODULO.ParameterName = "p_cod_modulo";
                        pCOD_MODULO.Value = pProceso.cod_modulo;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = "p_nombre";
                        pNOMBRE.Value = pProceso.nombre;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PROCESO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MODULO);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_PROCESO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pProceso, "PROCESO",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoData", "ModificarProceso", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PROCESO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PROCESO</param>
        public void EliminarProceso(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Proceso pProceso = new Proceso();

                        if (pUsuario.programaGeneraLog)
                            pProceso = ConsultarProceso(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_PROCESO = cmdTransaccionFactory.CreateParameter();
                        pCOD_PROCESO.ParameterName = "p_cod_proceso";
                        pCOD_PROCESO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PROCESO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_PROCESO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pProceso, "PROCESO", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoData", "EliminarProceso", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PROCESO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PROCESO</param>
        /// <returns>Entidad Proceso consultado</returns>
        public Proceso ConsultarProceso(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Proceso entidad = new Proceso();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PROCESO WHERE cod_proceso = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                            if (resultado["COD_MODULO"] != DBNull.Value) entidad.cod_modulo = Convert.ToInt64(resultado["COD_MODULO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoData", "ConsultarProceso", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PROCESO dados unos filtros
        /// </summary>
        /// <param name="pPROCESO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<Proceso> ListarProceso(Proceso pProceso, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Proceso> lstProceso = new List<Proceso>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PROCESO " + ObtenerFiltro(pProceso);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Proceso entidad = new Proceso();

                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                            if (resultado["COD_MODULO"] != DBNull.Value) entidad.cod_modulo = Convert.ToInt64(resultado["COD_MODULO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);

                            lstProceso.Add(entidad);
                        }

                        return lstProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoData", "ListarProceso", ex);
                        return null;
                    }
                }
            }
        }

    }
}