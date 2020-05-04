using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Procesos
    /// </summary>
    public class ProcesosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Procesos
        /// </summary>
        public ProcesosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Procesos de la base de datos
        /// </summary>
        /// <param name="pProcesos">Entidad Procesos</param>
        /// <returns>Entidad Procesos creada</returns>
        public Procesos CrearProcesos(Procesos pProcesos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_PROCESO = cmdTransaccionFactory.CreateParameter();
                        pCOD_PROCESO.ParameterName = "pCOD_PROCESO";
                        pCOD_PROCESO.Value = 0;
                        pCOD_PROCESO.Direction = ParameterDirection.Output;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "pDESCRIPCION";
                        pDESCRIPCION.Value = pProcesos.descripcion;

                        DbParameter pTIPO_PROCESO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_PROCESO.ParameterName = "pTIPO_PROCESO";
                        pTIPO_PROCESO.Value = pProcesos.tipo_proceso;

                        DbParameter pCOD_ANTECESOR = cmdTransaccionFactory.CreateParameter();
                        pCOD_ANTECESOR.ParameterName = "pCOD_ANTECESOR";
                        pCOD_ANTECESOR.Value = pProcesos.cod_proceso_antec;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "pESTADO";
                        if (pProcesos.estado != null) pESTADO.Value = pProcesos.estado; else pESTADO.Value = DBNull.Value;
                        if (pProcesos.estado != null) pESTADO.Value = pProcesos.estado; else pESTADO.Value = DBNull.Value;

                        DbParameter PError = cmdTransaccionFactory.CreateParameter();
                        PError.ParameterName = "PError";
                        PError.Size = 100;
                        PError.Value = string.IsNullOrEmpty(pProcesos.Perror) ? (object)DBNull.Value : pProcesos.Perror;
                        PError.DbType = DbType.StringFixedLength;
                        PError.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PROCESO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_PROCESO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_ANTECESOR);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(PError);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_TIPOPROCES_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pProcesos, "Procesos",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        if (!string.IsNullOrEmpty(PError.Value.ToString()))
                        {
                            pProcesos.Perror = Convert.ToString(PError.Value).Trim();
                        }
                        else
                        {
                            pProcesos.cod_proceso = Convert.ToInt64(pCOD_PROCESO.Value);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return pProcesos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesosData", "CrearProcesos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Procesos de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Procesos modificada</returns>
        public Procesos ModificarProcesos(Procesos pProcesos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pCOD_PROCESO = cmdTransaccionFactory.CreateParameter();
                        pCOD_PROCESO.ParameterName = "pCOD_PROCESO";
                        pCOD_PROCESO.Value = pProcesos.cod_proceso;


                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "pDESCRIPCION";
                        pDESCRIPCION.Value = pProcesos.descripcion;

                        DbParameter pTIPO_PROCESO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_PROCESO.ParameterName = "pTIPO_PROCESO";
                        pTIPO_PROCESO.Value = pProcesos.tipo_proceso;

                        DbParameter pCOD_ANTECESOR = cmdTransaccionFactory.CreateParameter();
                        pCOD_ANTECESOR.ParameterName = "pCOD_ANTECESOR";
                        pCOD_ANTECESOR.Value = pProcesos.cod_proceso_antec;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "pESTADO";
                        if (pProcesos.estado != null) pESTADO.Value = pProcesos.estado; else pESTADO.Value = DBNull.Value;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PROCESO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_PROCESO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_ANTECESOR);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_TIPOPROCES_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pProcesos, "Procesos",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pProcesos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesosData", "ModificarProcesos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Procesos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Procesos</param>
        public void EliminarProcesos(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Procesos pProcesos = new Procesos();

                        //if (pUsuario.programaGeneraLog)
                        //    pProcesos = ConsultarProcesos(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_PROCESO = cmdTransaccionFactory.CreateParameter();
                        pCOD_PROCESO.ParameterName = "pCOD_PROCESO";
                        pCOD_PROCESO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PROCESO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VEHIC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pProcesos, "Procesos", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesosData", "EliminarProcesos", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Procesos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Procesos</param>
        /// <returns>Entidad Procesos consultado</returns>
        public Procesos ConsultarProcesos(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Procesos entidad = new Procesos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM tipoprocesos   WHERE CODTIPOPROCESO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODTIPOPROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["CODTIPOPROCESO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CODPROANTECEDE"] != DBNull.Value) entidad.cod_proceso_antec = Convert.ToInt64(resultado["CODPROANTECEDE"]);
                            if (resultado["TIPO_PROCESO"] != DBNull.Value) entidad.tipo_proceso = Convert.ToInt64(resultado["TIPO_PROCESO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
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
                        BOExcepcion.Throw("ProcesosData", "ConsultarProcesos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Procesos de la base de datos

        /// <param name="pId">identificador de regitro en la tabla Procesos</param>
        /// <returns>Entidad Procesos consultado</returns>
        public Procesos ConsultarProcesosSiguientes(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Procesos entidad = new Procesos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "SELECT * FROM tipoprocesos   WHERE CODTIPOPROCESO = " + pId.ToString();
                        string sql = "Select x.codtipoproceso as ListaId, x.descripcion as ListaDescripcion,x.tipo_proceso From tipoprocesos t Left Join tipoprocesos x On x.codproantecede = t.codtipoproceso where  t.codtipoproceso= " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ListaId"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["ListaId"]);
                            if (resultado["ListaDescripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["ListaDescripcion"]);
                            if (resultado["TIPO_PROCESO"] != DBNull.Value) entidad.tipo_proceso = Convert.ToInt64(resultado["TIPO_PROCESO"]);

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
                        BOExcepcion.Throw("ProcesosData", "ConsultarProcesosSiguientes", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Procesos dados unos filtros
        /// </summary>
        /// <param name="pProcesos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Procesos obtenidos</returns>
        public List<Procesos> ListarProcesosAutomaticos(Procesos pProcesos, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Procesos> lstProcesos = new List<Procesos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  estado_credito";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Procesos entidad = new Procesos();

                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstProcesos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProcesos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesosData", "ListarProcesosAutomaticos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Procesos dados unos filtros
        /// </summary>
        /// <param name="pProcesos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Procesos obtenidos</returns>
        public List<Procesos> ListarProcesosSiguientes(Procesos pProcesos, String filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Procesos> lstProcesos = new List<Procesos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select x.codtipoproceso as ListaId, x.descripcion as ListaDescripcion From tipoprocesos t Left Join tipoprocesos x On x.codproantecede = t.codtipoproceso where t.descripcion=" + "'" + filtro + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Procesos entidad = new Procesos();

                            if (resultado["ListaId"] != DBNull.Value) entidad.cod_proceso_antec = Convert.ToInt64(resultado["ListaId"]);
                            if (resultado["ListaDescripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["ListaDescripcion"]);

                            lstProcesos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProcesos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesosData", "ListarProcesosSiguientes", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Procesos dados unos filtros
        /// </summary>
        /// <param name="pProcesos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Procesos obtenidos</returns>
        public List<Procesos> ListarProcesos(Procesos pProcesos, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Procesos> lstProcesos = new List<Procesos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql1 = @"SELECT tipoprocesos.*, (Select t.descripcion From TipoProcesos t Where t.codtipoproceso = tipoprocesos.codproantecede) As antecede,
                                        Case tipoprocesos.tipo_proceso When 1 Then 'Automático' When 2 Then 'Manual' Else ' ' End As nom_tipo_proceso  
                                        FROM tipoprocesos " + filtro;
                        string sql2 = " ORDER BY tipoprocesos.codtipoproceso";
                        string sql = sql1 + sql2;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Procesos entidad = new Procesos();

                            if (resultado["CODTIPOPROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["CODTIPOPROCESO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CODPROANTECEDE"] != DBNull.Value) entidad.cod_proceso_antec = Convert.ToInt64(resultado["CODPROANTECEDE"]);
                            if (resultado["ANTECEDE"] != DBNull.Value) entidad.antecede = Convert.ToString(resultado["ANTECEDE"]);
                            if (resultado["TIPO_PROCESO"] != DBNull.Value) entidad.tipo_proceso = Convert.ToInt64(resultado["TIPO_PROCESO"]);
                            if (resultado["NOM_TIPO_PROCESO"] != DBNull.Value) entidad.nom_tipo_proceso = Convert.ToString(resultado["NOM_TIPO_PROCESO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);

                            lstProcesos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProcesos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesosData", "ListarProcesos", ex);
                        return null;
                    }
                }
            }
        }


    }
}