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
    /// Objeto de acceso a datos para la tabla Diligencia
    /// </summary>
    public class DiligenciaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Diligencia
        /// </summary>
        public DiligenciaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Diligencia de la base de datos
        /// </summary>
        /// <param name="pDiligencia">Entidad Diligencia</param>
        /// <returns>Entidad Diligencia creada</returns>
        public Diligencia CrearDiligencia(Diligencia pDiligencia, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODIGO_DILIGENCIA = cmdTransaccionFactory.CreateParameter();
                        pCODIGO_DILIGENCIA.ParameterName = "p_CODIGO_DILIGENCIA";
                        pCODIGO_DILIGENCIA.Value = pDiligencia.codigo_diligencia;
                        pCODIGO_DILIGENCIA.Direction = ParameterDirection.InputOutput;

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pDiligencia.numero_radicacion;

                        DbParameter pFECHA_DILIGENCIA = cmdTransaccionFactory.CreateParameter();
                        pFECHA_DILIGENCIA.ParameterName = "p_FECHA_DILIGENCIA";
                        pFECHA_DILIGENCIA.Value = pDiligencia.fecha_diligencia;
                       // pFECHA_DILIGENCIA.Value = pDiligencia.fecha_diligencia;

                        DbParameter pTIPO_DILIGENCIA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DILIGENCIA.ParameterName = "p_TIPO_DILIGENCIA";                     
                        pTIPO_DILIGENCIA.Value = pDiligencia.tipo_diligencia;               

                        DbParameter pATENDIO = cmdTransaccionFactory.CreateParameter();
                        pATENDIO.ParameterName = "p_ATENDIO";
                        pATENDIO.Value = pDiligencia.atendio;

                        DbParameter pRESPUESTA = cmdTransaccionFactory.CreateParameter();
                        pRESPUESTA.ParameterName = "p_RESPUESTA";
                        pRESPUESTA.Value = pDiligencia.respuesta;

                        DbParameter pACUERDO = cmdTransaccionFactory.CreateParameter();
                        pACUERDO.ParameterName = "p_ACUERDO";
                        pACUERDO.Value = pDiligencia.acuerdo;

                        DbParameter pFECHA_ACUERDO = cmdTransaccionFactory.CreateParameter();
                        pFECHA_ACUERDO.ParameterName = "p_FECHA_ACUERDO";
                        if (pDiligencia.fecha_acuerdo != DateTime.MinValue)
                            pFECHA_ACUERDO.Value = pDiligencia.fecha_acuerdo;
                        else
                            pFECHA_ACUERDO.Value = DBNull.Value;

                        DbParameter pVALOR_ACUERDO = cmdTransaccionFactory.CreateParameter();
                        pVALOR_ACUERDO.ParameterName = "p_VALOR_ACUERDO";
                        pVALOR_ACUERDO.Value = pDiligencia.valor_acuerdo;

                        DbParameter pANEXO = cmdTransaccionFactory.CreateParameter();
                        pANEXO.ParameterName = "p_ANEXO";
                        if (pDiligencia.anexo != null && pDiligencia.anexo != "")
                            pANEXO.Value = pDiligencia.anexo;
                        else
                            pANEXO.Value = DBNull.Value;

                        DbParameter pOBSERVACION = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACION.ParameterName = "p_OBSERVACION";
                        if (pDiligencia.observacion == "" || pDiligencia.observacion == null)
                            pOBSERVACION.Value = DBNull.Value;
                        else
                            pOBSERVACION.Value = pDiligencia.observacion;

                        DbParameter pCODIGO_USUARIO_REGIS = cmdTransaccionFactory.CreateParameter();
                        pCODIGO_USUARIO_REGIS.ParameterName = "p_CODIGO_USUARIO_REGIS";
                        pCODIGO_USUARIO_REGIS.Value = pDiligencia.codigo_usuario_regis;  //pCODIGO_USUARIO_REGIS.Value = pUsuario.codusuario;


                        DbParameter pTIPO_CONTACTO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_CONTACTO.ParameterName = "p_TIPO_CONTACTO";
                        pTIPO_CONTACTO.Value = pDiligencia.tipo_contacto;


                        DbParameter p_FECHA_CITACION = cmdTransaccionFactory.CreateParameter();
                        p_FECHA_CITACION.ParameterName = "p_FECHA_CITACION";
                        p_FECHA_CITACION.Value = pDiligencia.fecha_Citacion;

                        DbParameter p_NUM_CELULAR = cmdTransaccionFactory.CreateParameter();
                        p_NUM_CELULAR.ParameterName = "p_NUM_CELULAR";
                        if (pDiligencia.Num_Celular == "" || pDiligencia.Num_Celular == null)
                            p_NUM_CELULAR.Value = DBNull.Value;
                        else
                            p_NUM_CELULAR.Value = pDiligencia.Num_Celular;



                        cmdTransaccionFactory.Parameters.Add(pCODIGO_DILIGENCIA);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_DILIGENCIA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_DILIGENCIA);                      
                        cmdTransaccionFactory.Parameters.Add(pATENDIO);
                        cmdTransaccionFactory.Parameters.Add(pRESPUESTA);
                        cmdTransaccionFactory.Parameters.Add(pACUERDO);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_ACUERDO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_ACUERDO);
                        cmdTransaccionFactory.Parameters.Add(pANEXO);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACION);
                        cmdTransaccionFactory.Parameters.Add(pCODIGO_USUARIO_REGIS);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_CONTACTO);
                        cmdTransaccionFactory.Parameters.Add(p_FECHA_CITACION);
                        cmdTransaccionFactory.Parameters.Add(p_NUM_CELULAR);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_DIL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pDiligencia, "Diligencia",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pDiligencia.codigo_diligencia = Convert.ToInt64(pCODIGO_DILIGENCIA.Value);
                        return pDiligencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "CrearDiligencia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Diligencia de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Diligencia modificada</returns>
        public Diligencia ModificarDiligencia(Diligencia pDiligencia, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODIGO_DILIGENCIA = cmdTransaccionFactory.CreateParameter();
                        pCODIGO_DILIGENCIA.ParameterName = "p_CODIGO_DILIGENCIA";
                        pCODIGO_DILIGENCIA.Value = pDiligencia.codigo_diligencia;

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pDiligencia.numero_radicacion;

                        DbParameter pFECHA_DILIGENCIA = cmdTransaccionFactory.CreateParameter();
                        pFECHA_DILIGENCIA.ParameterName = "p_FECHA_DILIGENCIA";
                        pFECHA_DILIGENCIA.DbType = DbType.DateTime;
                        pFECHA_DILIGENCIA.Value = pDiligencia.fecha_diligencia.ToString("dd/MM/yyyy hh:mm:ss tt");


                        DbParameter pTIPO_DILIGENCIA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DILIGENCIA.ParameterName = "p_TIPO_DILIGENCIA";
                        pTIPO_DILIGENCIA.Value = pDiligencia.tipo_diligencia;

                        DbParameter pATENDIO = cmdTransaccionFactory.CreateParameter();
                        pATENDIO.ParameterName = "p_ATENDIO";
                        pATENDIO.Value = pDiligencia.atendio;

                        DbParameter pRESPUESTA = cmdTransaccionFactory.CreateParameter();
                        pRESPUESTA.ParameterName = "p_RESPUESTA";
                        pRESPUESTA.Value = pDiligencia.respuesta;

                        DbParameter pACUERDO = cmdTransaccionFactory.CreateParameter();
                        pACUERDO.ParameterName = "p_ACUERDO";
                        pACUERDO.Value = pDiligencia.acuerdo;

                        DbParameter pFECHA_ACUERDO = cmdTransaccionFactory.CreateParameter();
                        pFECHA_ACUERDO.ParameterName = "p_FECHA_ACUERDO";
                        pFECHA_ACUERDO.Value = pDiligencia.fecha_acuerdo;

                        DbParameter pVALOR_ACUERDO = cmdTransaccionFactory.CreateParameter();
                        pVALOR_ACUERDO.ParameterName = "p_VALOR_ACUERDO";
                        pVALOR_ACUERDO.Value = pDiligencia.valor_acuerdo;

                        DbParameter pANEXO = cmdTransaccionFactory.CreateParameter();
                        pANEXO.ParameterName = "p_ANEXO";
                        pANEXO.Value = pDiligencia.anexo;

                        DbParameter pOBSERVACION = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACION.ParameterName = "p_OBSERVACION";
                        pOBSERVACION.Value = pDiligencia.observacion;

                        DbParameter pCODIGO_USUARIO_REGIS = cmdTransaccionFactory.CreateParameter();
                        pCODIGO_USUARIO_REGIS.ParameterName = "p_CODIGO_USUARIO_REGIS";
                        pCODIGO_USUARIO_REGIS.Value = pDiligencia.codigo_usuario_regis;

                        DbParameter pTIPO_CONTACTO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_CONTACTO.ParameterName = "p_TIPO_CONTACTO";
                        pTIPO_CONTACTO.Value = pDiligencia.tipo_contacto;

                        cmdTransaccionFactory.Parameters.Add(pCODIGO_DILIGENCIA);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_DILIGENCIA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_DILIGENCIA);
                        cmdTransaccionFactory.Parameters.Add(pATENDIO);
                        cmdTransaccionFactory.Parameters.Add(pRESPUESTA);
                        cmdTransaccionFactory.Parameters.Add(pACUERDO);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_ACUERDO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_ACUERDO);
                        cmdTransaccionFactory.Parameters.Add(pANEXO);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACION);
                        cmdTransaccionFactory.Parameters.Add(pCODIGO_USUARIO_REGIS);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_CONTACTO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_DIL_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pDiligencia, "Diligencia",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pDiligencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ModificarDiligencia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Diligencia de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Diligencia</param>
        public void EliminarDiligencia(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Diligencia pDiligencia = new Diligencia();

                        //if (pUsuario.programaGeneraLog)
                        //    pDiligencia = ConsultarDiligencia(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODIGO_DILIGENCIA = cmdTransaccionFactory.CreateParameter();
                        pCODIGO_DILIGENCIA.ParameterName = "p_CODIGO_DILIGENCIA";
                        pCODIGO_DILIGENCIA.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODIGO_DILIGENCIA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_DIL_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pDiligencia, "Diligencia", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "EliminarDiligencia", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Diligencia de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Diligencia</param>
        /// <returns>Entidad Diligencia consultado</returns>
        public Diligencia ConsultarDiligencia(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Diligencia entidad = new Diligencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  diligenciascobro WHERE CODIGO_DILIGENCIA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODIGO_DILIGENCIA"] != DBNull.Value) entidad.codigo_diligencia = Convert.ToInt64(resultado["CODIGO_DILIGENCIA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["FECHA_DILIGENCIA"] != DBNull.Value) entidad.fecha_diligencia = Convert.ToDateTime(resultado["FECHA_DILIGENCIA"]);
                            if (resultado["TIPO_DILIGENCIA"] != DBNull.Value) entidad.tipo_diligencia = Convert.ToInt64(resultado["TIPO_DILIGENCIA"]);
                            if (resultado["ATENDIO"] != DBNull.Value) entidad.atendio = Convert.ToString(resultado["ATENDIO"]);
                            if (resultado["RESPUESTA"] != DBNull.Value) entidad.respuesta = Convert.ToString(resultado["RESPUESTA"]);
                            if (resultado["ACUERDO"] != DBNull.Value) entidad.acuerdo = Convert.ToInt64(resultado["ACUERDO"]);
                            if (resultado["FECHA_ACUERDO"] != DBNull.Value) entidad.fecha_acuerdo = Convert.ToDateTime(resultado["FECHA_ACUERDO"]);
                            if (resultado["VALOR_ACUERDO"] != DBNull.Value) entidad.valor_acuerdo = Convert.ToInt64(resultado["VALOR_ACUERDO"]);
                            if (resultado["ANEXO"] != DBNull.Value) entidad.anexo = Convert.ToString(resultado["ANEXO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["CODIGO_USUARIO_REGIS"] != DBNull.Value) entidad.codigo_usuario_regis = Convert.ToInt64(resultado["CODIGO_USUARIO_REGIS"]);
                            if (resultado["TIPO_CONTACTO"] != DBNull.Value) entidad.tipo_contacto= Convert.ToInt64(resultado["TIPO_CONTACTO"]);
                            if (resultado["NUM_CELULAR"] != DBNull.Value) entidad.Num_Celular = Convert.ToString(resultado["NUM_CELULAR"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ConsultarDiligencia", ex);
                        return null;
                    }
                }
            }
        }

        public Diligencia ConsultarDiligenciaEntidad(Diligencia pDiligencia, Usuario pUsuario)
        {
            DbDataReader resultado;
            Diligencia entidad = new Diligencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  diligenciascobro " + ObtenerFiltro(pDiligencia);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODIGO_DILIGENCIA"] != DBNull.Value) entidad.codigo_diligencia = Convert.ToInt64(resultado["CODIGO_DILIGENCIA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["FECHA_DILIGENCIA"] != DBNull.Value) entidad.fecha_diligencia = Convert.ToDateTime(resultado["FECHA_DILIGENCIA"]);
                            if (resultado["TIPO_DILIGENCIA"] != DBNull.Value) entidad.tipo_diligencia = Convert.ToInt64(resultado["TIPO_DILIGENCIA"]);
                            if (resultado["ATENDIO"] != DBNull.Value) entidad.atendio = Convert.ToString(resultado["ATENDIO"]);
                            if (resultado["RESPUESTA"] != DBNull.Value) entidad.respuesta = Convert.ToString(resultado["RESPUESTA"]);
                            if (resultado["ACUERDO"] != DBNull.Value) entidad.acuerdo = Convert.ToInt64(resultado["ACUERDO"]);
                            if (resultado["FECHA_ACUERDO"] != DBNull.Value) entidad.fecha_acuerdo = Convert.ToDateTime(resultado["FECHA_ACUERDO"]);
                            if (resultado["VALOR_ACUERDO"] != DBNull.Value) entidad.valor_acuerdo = Convert.ToInt64(resultado["VALOR_ACUERDO"]);
                            if (resultado["ANEXO"] != DBNull.Value) entidad.anexo = Convert.ToString(resultado["ANEXO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["CODIGO_USUARIO_REGIS"] != DBNull.Value) entidad.codigo_usuario_regis = Convert.ToInt64(resultado["CODIGO_USUARIO_REGIS"]);
                            if (resultado["TIPO_CONTACTO"] != DBNull.Value) entidad.tipo_contacto = Convert.ToInt64(resultado["TIPO_CONTACTO"]);
                            if (resultado["NUM_CELULAR"] != DBNull.Value) entidad.Num_Celular = Convert.ToString(resultado["NUM_CELULAR"]);

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ConsultarDiligencia", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene un registro en la tabla Diligencia de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Diligencia</param>
        /// <returns>Entidad Diligencia consultado</returns>
        public Diligencia ConsultarDiligenciaXcredito(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Diligencia entidad = new Diligencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  diligenciascobro WHERE NUMERO_RADICACION = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODIGO_DILIGENCIA"] != DBNull.Value) entidad.codigo_diligencia = Convert.ToInt64(resultado["CODIGO_DILIGENCIA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["FECHA_DILIGENCIA"] != DBNull.Value) entidad.fecha_diligencia = Convert.ToDateTime(resultado["FECHA_DILIGENCIA"]);
                            if (resultado["TIPO_DILIGENCIA"] != DBNull.Value) entidad.tipo_diligencia = Convert.ToInt64(resultado["TIPO_DILIGENCIA"]);
                            if (resultado["TIPO_CONTACTO"] != DBNull.Value) entidad.tipo_contacto= Convert.ToInt64(resultado["TIPO_CONTACTO"]);
                            if (resultado["ATENDIO"] != DBNull.Value) entidad.atendio = Convert.ToString(resultado["ATENDIO"]);
                            if (resultado["RESPUESTA"] != DBNull.Value) entidad.respuesta = Convert.ToString(resultado["RESPUESTA"]);
                            if (resultado["ACUERDO"] != DBNull.Value) entidad.acuerdo = Convert.ToInt64(resultado["ACUERDO"]);
                            if (resultado["FECHA_ACUERDO"] != DBNull.Value) entidad.fecha_acuerdo = Convert.ToDateTime(resultado["FECHA_ACUERDO"]);
                            if (resultado["VALOR_ACUERDO"] != DBNull.Value) entidad.valor_acuerdo = Convert.ToInt64(resultado["VALOR_ACUERDO"]);
                            if (resultado["ANEXO"] != DBNull.Value) entidad.anexo = Convert.ToString(resultado["ANEXO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["CODIGO_USUARIO_REGIS"] != DBNull.Value) entidad.codigo_usuario_regis = Convert.ToInt64(resultado["CODIGO_USUARIO_REGIS"]);
                            if (resultado["NUM_CELULAR"] != DBNull.Value) entidad.Num_Celular = Convert.ToString(resultado["NUM_CELULAR"]);
                        }
                      
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ConsultarDiligencia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla General de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Parametro consultado</returns>
        public Diligencia ConsultarparametroHabeasData(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Diligencia entidad = new Diligencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  valor FROM GENERAL WHERE CODIGO=6094";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.codigo_parametro= Convert.ToInt64(resultado["valor"]);


                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ConsultarparametroHabeasData", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Actualiz los estaods de los creditos 
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Parametro consultado</returns>
        public Diligencia ActualizarEstadosDiligencia(Diligencia pDiligencia, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Diligencia entidad = new Diligencia();
            Int64 diligencia;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "mERGE into COBROS_CREdito b USING ( sELECT  numero_radicacion, (CASE     WHEN  DIAS_MORA= 0   THEN '1'     WHEN  DIAS_MORA BETWEEN 1  AND  10  THEN '2'     WHEN  DIAS_MORA BETWEEN 11 AND 20 THEN '3'     WHEN  DIAS_MORA BETWEEN 21 AND 30 THEN '4'	 WHEN  DIAS_MORA BETWEEN 31 AND 44 THEN '5'	 WHEN  DIAS_MORA BETWEEN 45 AND 90 THEN '6'     WHEN  DIAS_MORA BETWEEN 91 AND 2000 THEN '7'  END) PROCESO FROM  V_REPORTE_cartera WHERE DIAS_MORA IS NOT NULL) e ON (b.numero_radicacion =  e.NUMERO_RADICACION) WHEN MATCHED THEN   UPDATE SET ESTADO_PROCESO  = PROCESO WHEN NOT MATCHED THEN insert (codigo,NUMERO_RADICACION,ESTADO_PROCESO,ENCARGADO,CIUDAD,COD_MOTIVO_CAMBIO,ABOGADO_ENCARGADO,FECHACREACION,CIUDAD_JUZGADO,NUMERO_JUZGADO,OBSERVACIONES,USUARIOCREACION,FECULTMOD,USUULTMOD) values(1,e.numero_radicacion,PROCESO,1,110010101,1,0,sysdate,0,0,0,1,sysdate,1)";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.RecordsAffected >0)
                        {

                            pDiligencia.codigo_diligencia = 1;
                        
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ActualizarEstadosDiligencia", ex);
                        return null;
                    }

                }
            }
        }
        /// <summary>
        /// Obtiene un registro de la tabla General de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Parametro consultado</returns>
        public Diligencia ConsultarparametroCobroPreJuridico(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Diligencia entidad = new Diligencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  valor FROM GENERAL WHERE CODIGO=6095";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.codigo_parametro= Convert.ToInt64(resultado["valor"]);


                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ConsultarparametroCobroPreJuridico", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla General de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Parametro consultado</returns>
        public Diligencia ConsultarparametroVisitaAbogado(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Diligencia entidad = new Diligencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  valor FROM GENERAL WHERE CODIGO=6099";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.codigo_parametro = Convert.ToInt64(resultado["valor"]);


                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ConsultarparametroVisitaAbogado", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla General de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Parametro consultado</returns>
        public Diligencia ConsultarparametroCampaña(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Diligencia entidad = new Diligencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  valor FROM GENERAL WHERE CODIGO=6101";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.codigo_parametro = Convert.ToInt64(resultado["valor"]);


                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ConsultarparametroCampaña", ex);
                        return null;
                    }

                }
            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla General de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Parametro consultado</returns>
        public Diligencia ConsultarparametroPrejuridico2(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Diligencia entidad = new Diligencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  valor FROM GENERAL WHERE CODIGO=6096";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.codigo_parametro = Convert.ToInt64(resultado["valor"]);


                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ConsultarparametroVisitaAbogado", ex);
                        return null;
                    }

                }
            }
        }
        /// <summary>
        /// Obtiene un registro de la tabla General de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Parametro consultado</returns>
        public Diligencia ConsultarparametroContactoCartas(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Diligencia entidad = new Diligencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  valor FROM GENERAL WHERE CODIGO=6097";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.codigo_parametro = Convert.ToInt64(resultado["valor"]);


                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ConsultarparametroContactoCartas", ex);
                        return null;
                    }

                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Diligencia dados unos filtros
        /// </summary>
        /// <param name="pDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Diligencia obtenidos</returns>
        public List<Diligencia> ListarDiligencia(Diligencia pDiligencia, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Diligencia> lstDiligencia = new List<Diligencia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "SELECT codigo_diligencia, numero_radicacion, fecha_diligencia, tipo_diligencia, atendio, respuesta, acuerdo, fecha_acuerdo, valor_acuerdo, Decode(anexo, null,'No', 'Si') as anexo, observacion, codigo_usuario_regis,tipo_contacto FROM  diligenciascobro" + ObtenerFiltro(pDiligencia);

                        string sql = "SELECT DISTINCT a.codigo_diligencia, a.numero_radicacion, a.fecha_diligencia,c.descripcion AS TIPO_DILIGENCIA, a.atendio, a.respuesta, Case a.acuerdo When 0 Then 'No' When 1 Then 'Si' End As acuerdo, a.fecha_acuerdo, a.valor_acuerdo, Decode(anexo, null,'No', 'Si') as anexo,a.observacion, a.codigo_usuario_regis, b.descripcion as TIPO_CONTACTO,u.NOMBRE, a.NUM_CELULAR FROM  diligenciascobro a inner join tipocontacto b on  a.tipo_contacto=b.tipocontacto inner join tipodiligencia c on a.tipo_diligencia=c.tipo_diligencia  inner join usuarios u on u.codusuario=a.CODIGO_USUARIO_REGIS " + ObtenerFiltro(pDiligencia);
                        sql = sql + " order BY codigo_diligencia desc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Diligencia entidad = new Diligencia();

                            if (resultado["CODIGO_DILIGENCIA"] != DBNull.Value) entidad.codigo_diligencia = Convert.ToInt64(resultado["CODIGO_DILIGENCIA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["FECHA_DILIGENCIA"] != DBNull.Value) entidad.fecha_diligencia = Convert.ToDateTime(resultado["FECHA_DILIGENCIA"]);
                            if (resultado["TIPO_DILIGENCIA"] != DBNull.Value) entidad.tipo_diligencia_consulta = Convert.ToString(resultado["TIPO_DILIGENCIA"]);
                            if (resultado["ATENDIO"] != DBNull.Value) entidad.atendio = Convert.ToString(resultado["ATENDIO"]);
                            if (resultado["RESPUESTA"] != DBNull.Value) entidad.respuesta = Convert.ToString(resultado["RESPUESTA"]);
                            if (resultado["ACUERDO"] != DBNull.Value) entidad.acuerdo_consulta = Convert.ToString(resultado["ACUERDO"]);
                            if (resultado["FECHA_ACUERDO"] != DBNull.Value) entidad.fecha_acuerdo = Convert.ToDateTime(resultado["FECHA_ACUERDO"]);
                            if (resultado["VALOR_ACUERDO"] != DBNull.Value) entidad.valor_acuerdo = Convert.ToInt64(resultado["VALOR_ACUERDO"]);
                            if (resultado["ANEXO"] != DBNull.Value) entidad.anexo = Convert.ToString(resultado["ANEXO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["CODIGO_USUARIO_REGIS"] != DBNull.Value) entidad.codigo_usuario_regis = Convert.ToInt64(resultado["CODIGO_USUARIO_REGIS"]);
                            if (resultado["TIPO_CONTACTO"] != DBNull.Value) entidad.tipo_contacto_consulta= Convert.ToString(resultado["TIPO_CONTACTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NUM_CELULAR"] != DBNull.Value) entidad.Num_Celular = Convert.ToString(resultado["NUM_CELULAR"]);

                            lstDiligencia.Add(entidad);
                        }

                        return lstDiligencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ListarDiligencia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Diligencia dados unos filtros
        /// </summary>
        /// <param name="pDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Diligencia obtenidos</returns>
        public List<Diligencia> ListarDiligenciaEstadocuenta(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Diligencia> lstDiligencia = new List<Diligencia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "SELECT codigo_diligencia, numero_radicacion, fecha_diligencia, tipo_diligencia, atendio, respuesta, acuerdo, fecha_acuerdo, valor_acuerdo, Decode(anexo, null,'No', 'Si') as anexo, observacion, codigo_usuario_regis,tipo_contacto FROM  diligenciascobro" + ObtenerFiltro(pDiligencia);

                        string sql = "SELECT a.codigo_diligencia, a.numero_radicacion, a.fecha_diligencia,c.descripcion AS TIPO_DILIGENCIA, a.atendio, a.respuesta, Case a.acuerdo When 0 Then 'No' When 1 Then 'Si' End As acuerdo, a.fecha_acuerdo, a.valor_acuerdo, Decode(anexo, null,'No', 'Si') as anexo,a.observacion, a.codigo_usuario_regis, b.descripcion as TIPO_CONTACTO,u.NOMBRE,cr.COD_DEUDOR, a.NUM_CELULAR FROM  diligenciascobro a inner join tipocontacto b on  a.tipo_contacto=b.tipocontacto inner join tipodiligencia c on a.tipo_diligencia=c.tipo_diligencia  inner join usuarios u on u.codusuario=a.CODIGO_USUARIO_REGIS inner join credito cr on cr.NUMERO_RADICACION=a.numero_radicacion   WHERE cr.cod_deudor = " + pId.ToString(); 
                        sql = sql + " order BY fecha_diligencia desc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Diligencia entidad = new Diligencia();

                            if (resultado["CODIGO_DILIGENCIA"] != DBNull.Value) entidad.codigo_diligencia = Convert.ToInt64(resultado["CODIGO_DILIGENCIA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["FECHA_DILIGENCIA"] != DBNull.Value) entidad.fecha_diligencia = Convert.ToDateTime(resultado["FECHA_DILIGENCIA"]);
                            if (resultado["TIPO_DILIGENCIA"] != DBNull.Value) entidad.tipo_diligencia_consulta = Convert.ToString(resultado["TIPO_DILIGENCIA"]);
                            if (resultado["ATENDIO"] != DBNull.Value) entidad.atendio = Convert.ToString(resultado["ATENDIO"]);
                            if (resultado["RESPUESTA"] != DBNull.Value) entidad.respuesta = Convert.ToString(resultado["RESPUESTA"]);
                            if (resultado["ACUERDO"] != DBNull.Value) entidad.acuerdo_consulta = Convert.ToString(resultado["ACUERDO"]);
                            if (resultado["FECHA_ACUERDO"] != DBNull.Value) entidad.fecha_acuerdo = Convert.ToDateTime(resultado["FECHA_ACUERDO"]);
                            if (resultado["VALOR_ACUERDO"] != DBNull.Value) entidad.valor_acuerdo = Convert.ToInt64(resultado["VALOR_ACUERDO"]);
                            if (resultado["ANEXO"] != DBNull.Value) entidad.anexo = Convert.ToString(resultado["ANEXO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["CODIGO_USUARIO_REGIS"] != DBNull.Value) entidad.codigo_usuario_regis = Convert.ToInt64(resultado["CODIGO_USUARIO_REGIS"]);
                            if (resultado["TIPO_CONTACTO"] != DBNull.Value) entidad.tipo_contacto_consulta = Convert.ToString(resultado["TIPO_CONTACTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.codigo_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["NUM_CELULAR"] != DBNull.Value) entidad.Num_Celular = Convert.ToString(resultado["NUM_CELULAR"]);
                            lstDiligencia.Add(entidad);
                        }

                        return lstDiligencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ListarDiligenciaEstadocuenta", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Diligencia dados unos filtros
        /// </summary>
        /// <param name="pDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Diligencia obtenidos</returns>
        public List<Diligencia> ListarReporteacuerdos(Usuario pUsuario, DateTime fechaini, DateTime fechafinal, Int64 usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Diligencia> lstDiligencia = new List<Diligencia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //Consulta modificado ya que se duplicaban los datos de la consulta
                        //string sql = "SELECT codigo_diligencia, numero_radicacion, fecha_diligencia, tipo_diligencia, atendio, respuesta, acuerdo, fecha_acuerdo, valor_acuerdo, Decode(anexo, null,'No', 'Si') as anexo, observacion, codigo_usuario_regis,tipo_contacto FROM  diligenciascobro" + ObtenerFiltro(pDiligencia);
                        string sql = @"SELECT DISTINCT d.COD_DEUDOR,a.codigo_diligencia, a.numero_radicacion, a.fecha_diligencia,persona.primer_nombre||' '||persona.primer_apellido as nombre, 
                            persona.identificacion,c.descripcion AS TIPO_DILIGENCIA, a.atendio, a.respuesta, Case a.acuerdo When 0 Then 'No' When 1 Then 'Si' End As acuerdo, a.fecha_acuerdo, 
                            a.valor_acuerdo, Decode(anexo, null,'No', 'Si') as anexo,a.observacion, a.codigo_usuario_regis, b.descripcion as TIPO_CONTACTO, a.NUM_CELULAR FROM  diligenciascobro 
                            a inner join credito on a.numero_radicacion= credito.numero_radicacion inner join persona on credito.cod_oficina=persona.cod_oficina  inner join tipocontacto b on  
                            a.tipo_contacto=b.tipocontacto inner join tipodiligencia c on a.tipo_diligencia=c.tipo_diligencia inner join credito d on a.NUMERO_RADICACION=d.NUMERO_RADICACION and  
                            FECHA_ACUERDO between to_date('" + fechaini.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') And to_date('" + fechafinal.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') and CODIGO_USUARIO_REGIS=" + usuario;
                      
                        //string sql = "SELECT a.codigo_diligencia, a.numero_radicacion, a.fecha_diligencia,c.descripcion AS TIPO_DILIGENCIA, a.atendio, a.respuesta,Case a.acuerdo When 0 Then 'No' When 1 Then 'Si' End As acuerdo, a.fecha_acuerdo, a.valor_acuerdo, Decode(anexo, null,'No', 'Si') as anexo,a.observacion, a.codigo_usuario_regis, b.descripcion as TIPO_CONTACTO,cred.cod_deudor,per.identificacion,per.PRIMER_NOMBRE|| CHR(160) ||per.segundo_nombre|| CHR(160) ||per.primer_apellido|| CHR(160)|| per.segundo_apellido as nombre FROM  diligenciascobro a inner join tipocontacto b on  a.tipo_contacto=b.tipocontacto inner join tipodiligencia c on a.tipo_diligencia=c.tipo_diligencia inner join credito cred on cred.numero_radicacion=a.numero_radicacion inner join persona per on per.cod_persona=cred.cod_deudor  and  FECHA_diligencia between to_date('" + fechaini.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') And to_date('" + fechafinal.ToString("dd/MM/yyyy") + "','dd/MM/yyyy')";
                        sql = sql + " order BY FECHA_ACUERDO desc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Diligencia entidad = new Diligencia();
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.codigo_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["FECHA_DILIGENCIA"] != DBNull.Value) entidad.fecha_diligencia = Convert.ToDateTime(resultado["FECHA_DILIGENCIA"]);
                            if (resultado["TIPO_DILIGENCIA"] != DBNull.Value) entidad.tipo_diligencia_consulta = Convert.ToString(resultado["TIPO_DILIGENCIA"]);
                            if (resultado["ATENDIO"] != DBNull.Value) entidad.atendio = Convert.ToString(resultado["ATENDIO"]);
                            if (resultado["RESPUESTA"] != DBNull.Value) entidad.respuesta = Convert.ToString(resultado["RESPUESTA"]);
                            if (resultado["ACUERDO"] != DBNull.Value) entidad.acuerdo_consulta = Convert.ToString(resultado["ACUERDO"]);
                            if (resultado["FECHA_ACUERDO"] != DBNull.Value) entidad.fecha_acuerdo = Convert.ToDateTime(resultado["FECHA_ACUERDO"]);
                            if (resultado["VALOR_ACUERDO"] != DBNull.Value) entidad.valor_acuerdo = Convert.ToInt64(resultado["VALOR_ACUERDO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            //  if (resultado["CODIGO_USUARIO_REGIS"] != DBNull.Value) entidad.codigo_usuario_regis = Convert.ToInt64(resultado["CODIGO_USUARIO_REGIS"]);
                            if (resultado["TIPO_CONTACTO"] != DBNull.Value) entidad.tipo_contacto_consulta = Convert.ToString(resultado["TIPO_CONTACTO"]);
                            if (resultado["NUM_CELULAR"] != DBNull.Value) entidad.Num_Celular = Convert.ToString(resultado["NUM_CELULAR"]);

                            lstDiligencia.Add(entidad);
                        }

                        return lstDiligencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ListarReporteDiligencia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Diligencia dados unos filtros
        /// </summary>
        /// <param name="pDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Diligencia obtenidos</returns>
        public List<Diligencia> ListarReporteacuerdosTodosUsuarios(Usuario pUsuario, DateTime fechaini, DateTime fechafinal, Int64 cod_zona)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Diligencia> lstDiligencia = new List<Diligencia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT p.IDENTIFICACION, p.nombre,d.cod_deudor, a.codigo_diligencia, a.numero_radicacion, a.fecha_diligencia,c.descripcion AS tipo_diligencia, a.atendio, a.respuesta, Case a.acuerdo When 0 Then 'No' When 1 Then 'Si' End As acuerdo, 
                                        a.fecha_acuerdo, a.valor_acuerdo, Decode(anexo, null,'No', 'Si') as anexo,a.observacion, a.codigo_usuario_regis, b.descripcion as tipo_contacto, a.NUM_CELULAR
                                        FROM  diligenciascobro a  
                                        INNER JOIN credito d on a.numero_radicacion = d.numero_radicacion 
                                        LEFT JOIN v_PERSONA p ON D.cod_deudor=p.cod_persona
                                        LEFT JOIN tipocontacto b on  a.tipo_contacto = b.tipocontacto 
                                        LEFT JOIN tipodiligencia c on a.tipo_diligencia = c.tipo_diligencia
                                        WHERE a.fecha_acuerdo between to_date('" + fechaini.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') And to_date('" + fechafinal.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + @"')";
                        //Agregado para consultar zona
                        if (cod_zona > 0)
                            sql = sql + " AND p.COD_ZONA = " + cod_zona;
                        sql = sql + " ORDER BY a.fecha_acuerdo desc ";
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Diligencia entidad = new Diligencia();
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.codigo_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["FECHA_DILIGENCIA"] != DBNull.Value) entidad.fecha_diligencia = Convert.ToDateTime(resultado["FECHA_DILIGENCIA"]);
                            if (resultado["TIPO_DILIGENCIA"] != DBNull.Value) entidad.tipo_diligencia_consulta = Convert.ToString(resultado["TIPO_DILIGENCIA"]);
                            if (resultado["ATENDIO"] != DBNull.Value) entidad.atendio = Convert.ToString(resultado["ATENDIO"]);
                            if (resultado["RESPUESTA"] != DBNull.Value) entidad.respuesta = Convert.ToString(resultado["RESPUESTA"]);
                            if (resultado["ACUERDO"] != DBNull.Value) entidad.acuerdo_consulta = Convert.ToString(resultado["ACUERDO"]);
                            if (resultado["FECHA_ACUERDO"] != DBNull.Value) entidad.fecha_acuerdo = Convert.ToDateTime(resultado["FECHA_ACUERDO"]);
                            if (resultado["VALOR_ACUERDO"] != DBNull.Value) entidad.valor_acuerdo = Convert.ToInt64(resultado["VALOR_ACUERDO"]);
                            //  if (resultado["ANEXO"] != DBNull.Value) entidad.anexo = Convert.ToString(resultado["ANEXO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            //  if (resultado["CODIGO_USUARIO_REGIS"] != DBNull.Value) entidad.codigo_usuario_regis = Convert.ToInt64(resultado["CODIGO_USUARIO_REGIS"]);
                            if (resultado["TIPO_CONTACTO"] != DBNull.Value) entidad.tipo_contacto_consulta = Convert.ToString(resultado["TIPO_CONTACTO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NUM_CELULAR"] != DBNull.Value) entidad.Num_Celular = Convert.ToString(resultado["NUM_CELULAR"]);
                            lstDiligencia.Add(entidad);
                        }

                        return lstDiligencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ListarReporteDiligencia", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Diligencia dados unos filtros
        /// </summary>
        /// <param name="pDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Diligencia obtenidos</returns>
        public List<Diligencia> ListarReporteDiligencia(Usuario pUsuario, DateTime fechaini, DateTime fechafinal, Int64 usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Diligencia> lstDiligencia = new List<Diligencia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "SELECT codigo_diligencia, numero_radicacion, fecha_diligencia, tipo_diligencia, atendio, respuesta, acuerdo, fecha_acuerdo, valor_acuerdo, Decode(anexo, null,'No', 'Si') as anexo, observacion, codigo_usuario_regis,tipo_contacto FROM  diligenciascobro" + ObtenerFiltro(pDiligencia);
                        string sql = "SELECT a.codigo_diligencia, a.numero_radicacion, a.fecha_diligencia,c.descripcion AS TIPO_DILIGENCIA, a.atendio, a.respuesta,Case a.acuerdo When 0 Then 'No' When 1 Then 'Si' End As acuerdo, a.fecha_acuerdo, a.valor_acuerdo, Decode(anexo, null,'No', 'Si') as anexo,a.observacion, a.codigo_usuario_regis, b.descripcion as TIPO_CONTACTO,cred.cod_deudor,per.identificacion,per.PRIMER_NOMBRE|| CHR(160) ||per.segundo_nombre|| CHR(160) ||per.primer_apellido|| CHR(160)|| per.segundo_apellido as nombre, a.NUM_CELULAR FROM  diligenciascobro a inner join tipocontacto b on  a.tipo_contacto=b.tipocontacto inner join tipodiligencia c on a.tipo_diligencia=c.tipo_diligencia inner join credito cred on cred.numero_radicacion=a.numero_radicacion inner join persona per on per.cod_persona=cred.cod_deudor and  FECHA_diligencia between to_date('" + fechaini.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') And to_date('" + fechafinal.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') and CODIGO_USUARIO_REGIS=" + usuario;
                  
                         sql = sql +  " order BY fecha_diligencia asc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Diligencia entidad = new Diligencia();


                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.codigo_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["FECHA_DILIGENCIA"] != DBNull.Value) entidad.fecha_diligencia = Convert.ToDateTime(resultado["FECHA_DILIGENCIA"]);
                            if (resultado["TIPO_DILIGENCIA"] != DBNull.Value) entidad.tipo_diligencia_consulta = Convert.ToString(resultado["TIPO_DILIGENCIA"]);
                            if (resultado["ATENDIO"] != DBNull.Value) entidad.atendio = Convert.ToString(resultado["ATENDIO"]);
                            if (resultado["RESPUESTA"] != DBNull.Value) entidad.respuesta = Convert.ToString(resultado["RESPUESTA"]);
                            if (resultado["ACUERDO"] != DBNull.Value) entidad.acuerdo_consulta = Convert.ToString(resultado["ACUERDO"]);
                            if (resultado["FECHA_ACUERDO"] != DBNull.Value) entidad.fecha_acuerdo = Convert.ToDateTime(resultado["FECHA_ACUERDO"]);
                            if (resultado["VALOR_ACUERDO"] != DBNull.Value) entidad.valor_acuerdo = Convert.ToInt64(resultado["VALOR_ACUERDO"]);
                            //  if (resultado["ANEXO"] != DBNull.Value) entidad.anexo = Convert.ToString(resultado["ANEXO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["CODIGO_USUARIO_REGIS"] != DBNull.Value) entidad.codigo_usuario_regis = Convert.ToInt64(resultado["CODIGO_USUARIO_REGIS"]);
                            if (resultado["TIPO_CONTACTO"] != DBNull.Value) entidad.tipo_contacto_consulta = Convert.ToString(resultado["TIPO_CONTACTO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NUM_CELULAR"] != DBNull.Value) entidad.Num_Celular = Convert.ToString(resultado["NUM_CELULAR"]);
                            lstDiligencia.Add(entidad);
                        }

                        return lstDiligencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ListarReporteDiligencia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Diligencia dados unos filtros
        /// </summary>
        /// <param name="pDiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Diligencia obtenidos</returns>
        public List<Diligencia> ListarReporteDiligenciaTodos(Usuario pUsuario, DateTime fechaini, DateTime fechafinal, Int64 cod_zona)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Diligencia> lstDiligencia = new List<Diligencia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT a.codigo_diligencia, a.numero_radicacion, a.fecha_diligencia, c.descripcion AS tipo_diligencia, a.atendio, a.respuesta, Case a.acuerdo When 0 Then 'No' When 1 Then 'Si' End As acuerdo, 
                                          a.fecha_acuerdo, a.valor_acuerdo, Decode(anexo, null,'No', 'Si') as anexo,a.observacion, a.codigo_usuario_regis, b.descripcion as tipo_contacto,cred.cod_deudor, per.identificacion, 
                                          per.primer_nombre|| CHR(160) || per.segundo_nombre || CHR(160) || per.primer_apellido || CHR(160) || per.segundo_apellido as nombre, A.NUM_CELULAR
                                          FROM  diligenciascobro a 
                                          INNER JOIN credito cred on cred.numero_radicacion = a.numero_radicacion 
                                          INNER JOIN persona per on per.cod_persona = cred.cod_deudor 
                                          LEFT JOIN tipocontacto b ON a.tipo_contacto = b.tipocontacto 
                                          LEFT JOIN tipodiligencia c ON a.tipo_diligencia = c.tipo_diligencia                                           
                                        WHERE fecha_diligencia BETWEEN to_date('" + fechaini.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') AND to_date('" + fechafinal.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                        //Agregado para consultar zona
                        if (cod_zona > 0)
                            sql = sql + " AND per.COD_ZONA = " + cod_zona;
                        sql = sql + " ORDER BY fecha_diligencia asc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Diligencia entidad = new Diligencia();


                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.codigo_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["FECHA_DILIGENCIA"] != DBNull.Value) entidad.fecha_diligencia = Convert.ToDateTime(resultado["FECHA_DILIGENCIA"]);
                            if (resultado["TIPO_DILIGENCIA"] != DBNull.Value) entidad.tipo_diligencia_consulta = Convert.ToString(resultado["TIPO_DILIGENCIA"]);
                            if (resultado["ATENDIO"] != DBNull.Value) entidad.atendio = Convert.ToString(resultado["ATENDIO"]);
                            if (resultado["RESPUESTA"] != DBNull.Value) entidad.respuesta = Convert.ToString(resultado["RESPUESTA"]);
                            if (resultado["ACUERDO"] != DBNull.Value) entidad.acuerdo_consulta = Convert.ToString(resultado["ACUERDO"]);
                            if (resultado["FECHA_ACUERDO"] != DBNull.Value) entidad.fecha_acuerdo = Convert.ToDateTime(resultado["FECHA_ACUERDO"]);
                            if (resultado["VALOR_ACUERDO"] != DBNull.Value) entidad.valor_acuerdo = Convert.ToInt64(resultado["VALOR_ACUERDO"]);
                            //  if (resultado["ANEXO"] != DBNull.Value) entidad.anexo = Convert.ToString(resultado["ANEXO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["CODIGO_USUARIO_REGIS"] != DBNull.Value) entidad.codigo_usuario_regis = Convert.ToInt64(resultado["CODIGO_USUARIO_REGIS"]);
                            if (resultado["TIPO_CONTACTO"] != DBNull.Value) entidad.tipo_contacto_consulta = Convert.ToString(resultado["TIPO_CONTACTO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NUM_CELULAR"] != DBNull.Value) entidad.Num_Celular = Convert.ToString(resultado["NUM_CELULAR"]);

                            lstDiligencia.Add(entidad);
                        }

                        return lstDiligencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiligenciaData", "ListarReporteDiligencia", ex);
                        return null;
                    }
                }
            }
        }

    }
}