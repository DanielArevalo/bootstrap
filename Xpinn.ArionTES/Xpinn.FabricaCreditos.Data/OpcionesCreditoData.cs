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
    /// Objeto de acceso a datos para la tabla OpcionesCredito
    /// </summary>
    public class OpcionesCreditoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla OpcionesCredito
        /// </summary>
        public OpcionesCreditoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla OpcionesCredito de la base de datos
        /// </summary>
        /// <param name="pOpcionesCredito">Entidad OpcionesCredito</param>
        /// <returns>Entidad OpcionesCredito creada</returns>
        public OpcionesCredito CrearOpcionesCredito(OpcionesCredito pOpcionesCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCLASIFICACION = cmdTransaccionFactory.CreateParameter();
                        pCLASIFICACION.ParameterName = "pCLASIFICA";
                        pCLASIFICACION.Value = pOpcionesCredito.cod_clasifica;
                        pCLASIFICACION.DbType = DbType.Int64;
                        pCLASIFICACION.Direction = ParameterDirection.Input;

                        DbParameter pCODOPCION = cmdTransaccionFactory.CreateParameter();
                        pCODOPCION.ParameterName = "pCOD_OPCION";
                        pCODOPCION.Value = pOpcionesCredito.cod_opcion;
                        pCODOPCION.DbType = DbType.Int64;
                        pCODOPCION.Direction = ParameterDirection.Input;



                        cmdTransaccionFactory.Parameters.Add(pCLASIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pCODOPCION);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CLASIFICAC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pOpcionesCredito, "OpcionesCredito", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        //   pOpcionesCredito.cod_linea_credito = Convert.ToString(pCOD_LINEA_CREDITO.Value);

                        dbConnectionFactory.CerrarConexion(connection);

                        return pOpcionesCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OpcionesCreditoData", "CrearOpcionesCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla OpcionesCredito de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad OpcionesCredito modificada</returns>
        public OpcionesCredito ModificarOpcionesCredito(OpcionesCredito pOpcionesCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCLASIFICACION = cmdTransaccionFactory.CreateParameter();
                        pCLASIFICACION.ParameterName = "pCLASIFICA";
                        pCLASIFICACION.Value = pOpcionesCredito.cod_clasifica;
                        pCLASIFICACION.DbType = DbType.Int64;
                        pCLASIFICACION.Direction = ParameterDirection.Input;

                        DbParameter pCODOPCION = cmdTransaccionFactory.CreateParameter();
                        pCODOPCION.ParameterName = "pCOD_OPCION";
                        pCODOPCION.Value = pOpcionesCredito.cod_opcion;
                        pCODOPCION.DbType = DbType.Int64;
                        pCODOPCION.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pCLASIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pCODOPCION);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_LINEA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pOpcionesCredito, "OpcionesCredito", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);

                        return pOpcionesCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OpcionesCreditoData", "ModificarOpcionesCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla OpcionesCredito de la base de datos
        /// </summary>
        /// <param name="pId">identificador de OpcionesCredito</param>
        public void EliminarOpcionesCredito(string pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        OpcionesCredito pOpcionesCredito = new OpcionesCredito();

                        if (pUsuario.programaGeneraLog)
                            pOpcionesCredito = ConsultarOpcionesCredito(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCLASIFICACION = cmdTransaccionFactory.CreateParameter();
                        pCLASIFICACION.ParameterName = "pCLASIFICA";
                        pCLASIFICACION.Value = pOpcionesCredito.cod_clasifica;
                        pCLASIFICACION.DbType = DbType.Int64;
                        pCLASIFICACION.Direction = ParameterDirection.Input;

                        DbParameter pCODOPCION = cmdTransaccionFactory.CreateParameter();
                        pCODOPCION.ParameterName = "pCOD_OPCION";
                        pCODOPCION.Value = pOpcionesCredito.cod_opcion;
                        pCODOPCION.DbType = DbType.Int64;
                        pCODOPCION.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pCLASIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pCODOPCION);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_LINEA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pOpcionesCredito, "OpcionesCredito", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OpcionesCreditoData", "EliminarOpcionesCredito", ex);
                    }
                }
            }
        }


       
        /// <summary>
        /// Obtiene un registro en la tabla OpcionesCredito de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla OpcionesCredito</param>
        /// <returns>Entidad OpcionesCredito consultado</returns>
        public OpcionesCredito ConsultarOpcionesCredito(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            OpcionesCredito entidad = new OpcionesCredito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  OpcionesCredito WHERE COD_LINEA_CREDITO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {


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
                        BOExcepcion.Throw("OpcionesCreditoData", "ConsultarOpcionesCredito", ex);
                        return null;
                    }
                }
            }
        }

        public List<OpcionesCredito> ListarOpciones(Int64 IdPerfil, Int64 CodModulo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<OpcionesCredito> lstPerfil = new List<OpcionesCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Opciones.cod_opcion, Opciones.nombre,clasificacion_proceso.cod_clasifica FROM Opciones INNER JOIN Proceso ON Opciones.cod_proceso = Proceso.cod_proceso INNER JOIN clasificacion_proceso ON clasificacion_proceso.COD_OPCION=Opciones.COD_OPCION where Proceso.cod_proceso=1001 ORDER BY Opciones.COD_OPCION";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            OpcionesCredito entidad = new OpcionesCredito();

                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["COD_OPCION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombreopcion = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt64(resultado["COD_CLASIFICA"]);
                         
                            lstPerfil.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPerfil;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilData", "ListarOpciones", ex);
                        return null;
                    }
                }
            }
        }
        public List<OpcionesCredito> ListarOpcionesModulo(Int64 IdPerfil, Int64 CodModulo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<OpcionesCredito> lstPerfil = new List<OpcionesCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @" SELECT Opciones.cod_opcion, Opciones.nombre,clasificacion_proceso.cod_opcion as checked                                      
                                        FROM Opciones INNER JOIN Proceso ON Opciones.cod_proceso = Proceso.cod_proceso
                                        left JOIN clasificacion_proceso ON clasificacion_proceso.cod_opcion=OPCIONES.COD_OPCION                    
                                        WHERE Proceso.cod_modulo = " + CodModulo.ToString() + " ORDER BY Opciones.cod_opcion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            OpcionesCredito entidad = new OpcionesCredito();
                          
                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["COD_OPCION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombreopcion = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CHECKED"] != DBNull.Value) entidad.check = Convert.ToBoolean(resultado["CHECKED"]);
                            if (entidad.check != null)
                            {
                                if (resultado["CHECKED"] != DBNull.Value) entidad.check = Convert.ToBoolean(resultado["CHECKED"]);
                           
                            }
                            lstPerfil.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPerfil;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilData", "ListarOpcionesModulo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla MODULO dados unos filtros
        /// </summary>
        /// <param name="pMODULO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Modulo obtenidos</returns>
        public List<OpcionesCredito> ListarModulo(OpcionesCredito pModulo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<OpcionesCredito> lstModulo = new List<OpcionesCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  MODULO " + ObtenerFiltro(pModulo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            OpcionesCredito entidad = new OpcionesCredito();

                            if (resultado["COD_MODULO"] != DBNull.Value) entidad.cod_modulo = Convert.ToInt64(resultado["COD_MODULO"]);
                            if (resultado["NOM_MODULO"] != DBNull.Value) entidad.nom_modulo = Convert.ToString(resultado["NOM_MODULO"]);

                            lstModulo.Add(entidad);
                        }

                        return lstModulo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ModuloData", "ListarModulo", ex);
                        return null;
                    }
                }
            }
        }

    }
}