using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Scoring.Entities;

namespace Xpinn.Scoring.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla PERSONA
    /// </summary>
    public class ScScoringBoardData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PERSONA
        /// </summary>
        public ScScoringBoardData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pScScoringBoard">Entidad ScScoringBoard</param>
        /// <returns>Entidad ScScoringBoard creada</returns>
        public ScScoringBoard CrearScScoringBoard(ScScoringBoard pScScoringBoard, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDSCORE = cmdTransaccionFactory.CreateParameter();
                        pIDSCORE.ParameterName = "pIDSCORE";
                        pIDSCORE.Value = 0;
                        pIDSCORE.Direction = ParameterDirection.InputOutput;

                        DbParameter pCOD_CLASIFICA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CLASIFICA.ParameterName = "pCOD_CLASIFICA";
                        pCOD_CLASIFICA.Value = pScScoringBoard.cod_clasifica;

                        DbParameter pCOD_LINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        pCOD_LINEA_CREDITO.ParameterName = "pCOD_LINEA_CREDITO";
                        pCOD_LINEA_CREDITO.Value = pScScoringBoard.cod_linea_credito;

                        DbParameter pAPLICA_A = cmdTransaccionFactory.CreateParameter();
                        pAPLICA_A.ParameterName = "pAPLICA_A";
                        pAPLICA_A.Value = pScScoringBoard.aplica_a;

                        DbParameter pMODELO = cmdTransaccionFactory.CreateParameter();
                        pMODELO.ParameterName = "pMODELO";
                        pMODELO.Value = pScScoringBoard.modelo;

                        DbParameter pBETA0 = cmdTransaccionFactory.CreateParameter();
                        pBETA0.ParameterName = "pBETA0";
                        pBETA0.Value = pScScoringBoard.beta0;

                        DbParameter pSCOREMAXIMO = cmdTransaccionFactory.CreateParameter();
                        pSCOREMAXIMO.ParameterName = "pSCOREMAXIMO";
                        pSCOREMAXIMO.Value = pScScoringBoard.score_maximo;

                        DbParameter pCLASE = cmdTransaccionFactory.CreateParameter();
                        pCLASE.ParameterName = "pCLASE";
                        pCLASE.Value = pScScoringBoard.clase;

                        DbParameter pFECHACREACION = cmdTransaccionFactory.CreateParameter();
                        pFECHACREACION.ParameterName = "pFECHACREACION";
                        pFECHACREACION.Value = pScScoringBoard.fechacreacion;

                        DbParameter pUSUARIOCREACION = cmdTransaccionFactory.CreateParameter();
                        pUSUARIOCREACION.ParameterName = "pUSUARIOCREACION";
                        pUSUARIOCREACION.Value = pScScoringBoard.usuariocreacion;

                        DbParameter pFECULTMOD = cmdTransaccionFactory.CreateParameter();
                        pFECULTMOD.ParameterName = "pFECULTMOD";
                        pFECULTMOD.Value = pScScoringBoard.fecultmod;

                        DbParameter pUSUULTMOD = cmdTransaccionFactory.CreateParameter();
                        pUSUULTMOD.ParameterName = "pUSUULTMOD";
                        pUSUULTMOD.Value = pScScoringBoard.usuultmod;

                        cmdTransaccionFactory.Parameters.Add(pIDSCORE);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CLASIFICA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_LINEA_CREDITO);
                        cmdTransaccionFactory.Parameters.Add(pAPLICA_A);
                        cmdTransaccionFactory.Parameters.Add(pMODELO);
                        cmdTransaccionFactory.Parameters.Add(pBETA0);
                        cmdTransaccionFactory.Parameters.Add(pSCOREMAXIMO);
                        cmdTransaccionFactory.Parameters.Add(pCLASE);
                        cmdTransaccionFactory.Parameters.Add(pFECHACREACION);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIOCREACION);
                        cmdTransaccionFactory.Parameters.Add(pFECULTMOD);
                        cmdTransaccionFactory.Parameters.Add(pUSUULTMOD);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SCBOARD_CREA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                           DAauditoria.InsertarLog(pScScoringBoard, "PERSONA",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pScScoringBoard.idscore = Convert.ToInt64(pIDSCORE.Value);

                        return pScScoringBoard;
                    }
                    catch (Exception ex)
                    {
                        string a = ex.ToString();
                        BOExcepcion.Throw("ScScoringBoardData", "CrearScScoringBoard", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ScScoringBoard modificada</returns>
        public ScScoringBoard ModificarScScoringBoard(ScScoringBoard pScScoringBoard, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDSCORE = cmdTransaccionFactory.CreateParameter();
                        pIDSCORE.ParameterName = "pIDSCORE";
                        pIDSCORE.Value = pScScoringBoard.idscore;
                        pIDSCORE.Direction = ParameterDirection.InputOutput;

                        DbParameter pCOD_CLASIFICA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CLASIFICA.ParameterName = "pCOD_CLASIFICA";
                        pCOD_CLASIFICA.Value = pScScoringBoard.cod_clasifica;

                        DbParameter pCOD_LINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        pCOD_LINEA_CREDITO.ParameterName = "pCOD_LINEA_CREDITO";
                        pCOD_LINEA_CREDITO.Value = pScScoringBoard.cod_linea_credito;

                        DbParameter pAPLICA_A = cmdTransaccionFactory.CreateParameter();
                        pAPLICA_A.ParameterName = "pAPLICA_A";
                        pAPLICA_A.Value = pScScoringBoard.aplica_a;

                        DbParameter pMODELO = cmdTransaccionFactory.CreateParameter();
                        pMODELO.ParameterName = "pMODELO";
                        pMODELO.Value = pScScoringBoard.modelo;

                        DbParameter pBETA0 = cmdTransaccionFactory.CreateParameter();
                        pBETA0.ParameterName = "pBETA0";
                        pBETA0.Value = pScScoringBoard.beta0;

                        DbParameter pSCOREMAXIMO = cmdTransaccionFactory.CreateParameter();
                        pSCOREMAXIMO.ParameterName = "pSCOREMAXIMO";
                        pSCOREMAXIMO.Value = pScScoringBoard.score_maximo;

                        DbParameter pCLASE = cmdTransaccionFactory.CreateParameter();
                        pCLASE.ParameterName = "pCLASE";
                        pCLASE.Value = pScScoringBoard.clase;
                       
                        DbParameter pFECULTMOD = cmdTransaccionFactory.CreateParameter();
                        pFECULTMOD.ParameterName = "pFECULTMOD";
                        pFECULTMOD.Value = pScScoringBoard.fecultmod;

                        DbParameter pUSUULTMOD = cmdTransaccionFactory.CreateParameter();
                        pUSUULTMOD.ParameterName = "pUSUULTMOD";
                        pUSUULTMOD.Value = pScScoringBoard.usuultmod;

                        cmdTransaccionFactory.Parameters.Add(pIDSCORE);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CLASIFICA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_LINEA_CREDITO);
                        cmdTransaccionFactory.Parameters.Add(pAPLICA_A);
                        cmdTransaccionFactory.Parameters.Add(pMODELO);
                        cmdTransaccionFactory.Parameters.Add(pBETA0);
                        cmdTransaccionFactory.Parameters.Add(pSCOREMAXIMO);
                        cmdTransaccionFactory.Parameters.Add(pCLASE);
                        cmdTransaccionFactory.Parameters.Add(pFECULTMOD);
                        cmdTransaccionFactory.Parameters.Add(pUSUULTMOD);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SCBOARD_MODI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        connection.Close();
                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pScScoringBoard, "SCBOARD", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pScScoringBoard;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScScoringBoardData", "ModificarScScoringBoard", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PERSONA</param>
        public void EliminarScScoringBoard(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ScScoringBoard pScScoringBoard = new ScScoringBoard();

                        DbParameter p_IDSCORE = cmdTransaccionFactory.CreateParameter();
                        p_IDSCORE.ParameterName = "p_IDSCORE";
                        p_IDSCORE.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(p_IDSCORE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SCBOARD_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScScoringBoardData", "InsertarScScoringBoard", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PERSONA</param>
        /// <returns>Entidad ScScoringBoard consultado</returns>
        public ScScoringBoard ConsultarScScoringBoard(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ScScoringBoard entidad = new ScScoringBoard();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM SCSCORINGBOARD where idscore =" + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDSCORE"] != DBNull.Value) entidad.idscore = Convert.ToInt64(resultado["IDSCORE"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt64(resultado["COD_CLASIFICA"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["APLICA_A"] != DBNull.Value) entidad.aplica_a = Convert.ToInt64(resultado["APLICA_A"]);
                            if (resultado["MODELO"] != DBNull.Value) entidad.modelo = Convert.ToInt64(resultado["MODELO"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["BETA0"] != DBNull.Value) entidad.beta0 = Convert.ToDecimal(resultado["BETA0"]);
                            if (resultado["CLASE"] != DBNull.Value) entidad.clase = Convert.ToInt64(resultado["CLASE"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScScoringBoardData", "ConsultarScScoringBoard", ex);
                        return null;
                    }
                }
            }
        }

 
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PERSONA dados unos filtros
        /// </summary>
        /// <param name="pPERSONA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ScScoringBoard obtenidos</returns>
        public List<ScScoringBoard> ListarScScoringBoard(ScScoringBoard pScScoringBoard, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ScScoringBoard> lstScScoringBoard = new List<ScScoringBoard>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * FROM SCSCORINGBOARD sc LEFT JOIN CLASIFICACION cl ON sc.cod_clasifica = cl.cod_clasifica LEFT JOIN LINEASCREDITO lc ON sc.cod_linea_credito = lc.cod_linea_credito WHERE idscore != 0 " + pScScoringBoard.filtro + " ORDER BY sc.idscore asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ScScoringBoard entidad = new ScScoringBoard();

                            if (resultado["IDSCORE"] != DBNull.Value) entidad.idscore = Convert.ToInt64(resultado["IDSCORE"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt64(resultado["COD_CLASIFICA"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["APLICA_A"] != DBNull.Value) entidad.aplica_a = Convert.ToInt64(resultado["APLICA_A"]);
                            if (resultado["MODELO"] != DBNull.Value) entidad.modelo = Convert.ToInt64(resultado["MODELO"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["BETA0"] != DBNull.Value) entidad.beta0 = Convert.ToDecimal(resultado["BETA0"]);
                            if (resultado["SCOREMAXIMO"] != DBNull.Value) entidad.score_maximo = Convert.ToDecimal(resultado["SCOREMAXIMO"]);
                            if (resultado["CLASE"] != DBNull.Value) entidad.clase = Convert.ToInt64(resultado["CLASE"]);
                            lstScScoringBoard.Add(entidad);
                        }

                        return lstScScoringBoard;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScScoringBoardData", "ListarScScoringBoard", ex);
                        return null;
                    }
                }
            }
        }

        public List<Clasificacion> ListarClasificacion(Clasificacion entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Clasificacion> lstClasificacion = new List<Clasificacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from clasificacion";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new Clasificacion();
                            //Asociar todos los valores a la entidad
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.Codigo = Convert.ToInt16(resultado["COD_CLASIFICA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["DESCRIPCION"]);
                            lstClasificacion.Add(entidad);
                        }

                        return lstClasificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListarClasificacion", "ListarClasificacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<Lineas> ListarLineas(Lineas entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Lineas> lstLineas = new List<Lineas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from lineascredito";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new Lineas();
                            //Asociar todos los valores a la entidad
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.Codigo = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstLineas.Add(entidad);
                        }

                        return lstLineas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListarClasificacion", "ListarClasificacion", ex);
                        return null;
                    }
                }
            }


        }

    }
}