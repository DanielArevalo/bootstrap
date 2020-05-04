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
    public class ScScoringBoardVarData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PERSONA
        /// </summary>
        public ScScoringBoardVarData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pScScoringBoardVar">Entidad ScScoringBoardVar</param>
        /// <returns>Entidad ScScoringBoardVar creada</returns>
        public ScScoringBoardVar CrearScScoringBoardVar(ScScoringBoardVar pScScoringBoardVar, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDSCOREVAR = cmdTransaccionFactory.CreateParameter();
                        pIDSCOREVAR.ParameterName = "pIDSCOREVAR";
                        pIDSCOREVAR.Value = pScScoringBoardVar.idscorevar;
                        pIDSCOREVAR.Direction = ParameterDirection.InputOutput;

                        DbParameter pIDSCORE = cmdTransaccionFactory.CreateParameter();
                        pIDSCORE.ParameterName = "pIDSCORE";
                        pIDSCORE.Value = pScScoringBoardVar.idscore;

                        DbParameter pIDPARAMETRO = cmdTransaccionFactory.CreateParameter();
                        pIDPARAMETRO.ParameterName = "pIDPARAMETRO";
                        pIDPARAMETRO.Value = pScScoringBoardVar.idparametro;

                        DbParameter pMINIMO = cmdTransaccionFactory.CreateParameter();
                        pMINIMO.ParameterName = "pMINIMO";
                        pMINIMO.Value = pScScoringBoardVar.minimo;

                        DbParameter pMAXIMO = cmdTransaccionFactory.CreateParameter();
                        pMAXIMO.ParameterName = "pMAXIMO";
                        pMAXIMO.Value = pScScoringBoardVar.maximo;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "pVALOR";
                        pVALOR.Value = pScScoringBoardVar.valor;

                        DbParameter pBETA = cmdTransaccionFactory.CreateParameter();
                        pBETA.ParameterName = "pBETA";
                        pBETA.Value = pScScoringBoardVar.beta;


                        cmdTransaccionFactory.Parameters.Add(pIDSCOREVAR);
                        cmdTransaccionFactory.Parameters.Add(pIDSCORE);
                        cmdTransaccionFactory.Parameters.Add(pIDPARAMETRO);
                        cmdTransaccionFactory.Parameters.Add(pMINIMO);
                        cmdTransaccionFactory.Parameters.Add(pMAXIMO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pBETA);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SCBOVAR_CREA";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        if (pUsuario.programaGeneraLog)
                           DAauditoria.InsertarLog(pScScoringBoardVar, "PERSONA",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pScScoringBoardVar.idscore = Convert.ToInt64(pIDSCORE.Value);

                        return pScScoringBoardVar;
                    }
                    catch (Exception ex)
                    {
                        string a = ex.ToString();
                        BOExcepcion.Throw("ScScoringBoardVarData", "CrearScScoringBoardVar", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ScScoringBoardVar modificada</returns>
        public ScScoringBoardVar ModificarScScoringBoardVar(ScScoringBoardVar pScScoringBoardVar, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDSCOREVAR = cmdTransaccionFactory.CreateParameter();
                        pIDSCOREVAR.ParameterName = "pIDSCOREVAR";
                        pIDSCOREVAR.Value = pScScoringBoardVar.idscorevar;
                        pIDSCOREVAR.Direction = ParameterDirection.InputOutput;

                        DbParameter pIDSCORE = cmdTransaccionFactory.CreateParameter();
                        pIDSCORE.ParameterName = "pIDSCORE";
                        pIDSCORE.Value = pScScoringBoardVar.idscore;

                        DbParameter pIDPARAMETRO = cmdTransaccionFactory.CreateParameter();
                        pIDPARAMETRO.ParameterName = "pIDPARAMETRO";
                        pIDPARAMETRO.Value = pScScoringBoardVar.idparametro;

                        DbParameter pMINIMO = cmdTransaccionFactory.CreateParameter();
                        pMINIMO.ParameterName = "pMINIMO";
                        pMINIMO.Value = pScScoringBoardVar.minimo;

                        DbParameter pMAXIMO = cmdTransaccionFactory.CreateParameter();
                        pMAXIMO.ParameterName = "pMAXIMO";
                        pMAXIMO.Value = pScScoringBoardVar.maximo;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "pVALOR";
                        pVALOR.Value = pScScoringBoardVar.valor;

                        DbParameter pBETA = cmdTransaccionFactory.CreateParameter();
                        pBETA.ParameterName = "pBETA";
                        pBETA.Value = pScScoringBoardVar.beta;

                        cmdTransaccionFactory.Parameters.Add(pIDSCOREVAR);
                        cmdTransaccionFactory.Parameters.Add(pIDSCORE);
                        cmdTransaccionFactory.Parameters.Add(pIDPARAMETRO);
                        cmdTransaccionFactory.Parameters.Add(pMINIMO);
                        cmdTransaccionFactory.Parameters.Add(pMAXIMO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pBETA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SCBOVAR_MODI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        connection.Close();
                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pScScoringBoardVar, "SCBOARD", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pScScoringBoardVar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScScoringBoardVarData", "ModificarScScoringBoardVar", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PERSONA</param>
        public void EliminarScScoringBoardVar(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ScScoringBoardVar pScScoringBoardVar = new ScScoringBoardVar();

                        DbParameter pIdscorevar = cmdTransaccionFactory.CreateParameter();
                        pIdscorevar.ParameterName = "pIdscorevar";
                        pIdscorevar.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pIdscorevar);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SCBOVAR_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScScoringBoardVarData", "InsertarScScoringBoardVar", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PERSONA</param>
        /// <returns>Entidad ScScoringBoardVar consultado</returns>
        public ScScoringBoardVar ConsultarScScoringBoardVar(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ScScoringBoardVar entidad = new ScScoringBoardVar();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM ScScoringBoardVar where idscore =" + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDSCOREVAR"] != DBNull.Value) entidad.idscorevar = Convert.ToInt64(resultado["IDSCOREVAR"]);
                            if (resultado["IDSCORE"] != DBNull.Value) entidad.idscore = Convert.ToInt64(resultado["IDSCORE"]);
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["IDPARAMETRO"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.minimo = Convert.ToDecimal(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.maximo = Convert.ToDecimal(resultado["MAXIMO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["BETA"] != DBNull.Value) entidad.beta = Convert.ToInt64(resultado["BETA"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScScoringBoardVarData", "ConsultarScScoringBoardVar", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PERSONA de la base de datos por cedula
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PERSONA</param>
        /// <returns>Entidad ScScoringBoardVar consultado</returns>
        public ScScoringBoardVar ConsultarScScoringBoardVarParam(ScScoringBoardVar pScScoringBoardVar, Usuario pUsuario)
        {
            ScScoringBoardVar entidad = new ScScoringBoardVar();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ////string sql = null;
                        ////switch (pScScoringBoardVar.seleccionar)
                        ////{
                        ////    case "Identificacion":
                        ////        sql = "SELECT * FROM  PERSONA WHERE IDENTIFICACION = " + "'" + pScScoringBoardVar.identificacion.ToString() + "'";
                        ////        break;
                        ////    case "Codeudor":
                        ////        sql = "SELECT * FROM  PERSONA WHERE IDENTIFICACION = " + "'" + pScScoringBoardVar.identificacion.ToString() + "'";
                        ////        break;
                        ////}

                        ////connection.Open();
                        ////cmdTransaccionFactory.Connection = connection;
                        ////cmdTransaccionFactory.CommandType = CommandType.Text;
                        ////cmdTransaccionFactory.CommandText = sql;
                        ////resultado = cmdTransaccionFactory.ExecuteReader();

                        ////if (resultado.Read())
                        ////{
                        ////    if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                        ////    if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                        ////    if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                        ////    if (resultado["BARCORRESPONDENCIA"] != DBNull.Value) entidad.barrioCorrespondencia = Convert.ToInt64(resultado["BARCORRESPONDENCIA"]);
                        //}
                        //else
                        //{
                        //    throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        //}
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScScoringBoardVarData", "ConsultarScScoringBoardVar", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PERSONA dados unos filtros
        /// </summary>
        /// <param name="pPERSONA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ScScoringBoardVar obtenidos</returns>
        public List<ScScoringBoardVar> ListarScScoringBoardVar(ScScoringBoardVar pScScoringBoardVar, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ScScoringBoardVar> lstScScoringBoardVar = new List<ScScoringBoardVar>();

            if (pScScoringBoardVar == null)
                return null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT v.*, p.nombre FROM ScScoringBoard_Var v LEFT JOIN ScParametro p ON v.idparametro = p.idparametro WHERE v.idscore = " + pScScoringBoardVar.idscore + " ORDER BY v.idparametro, v.idscorevar";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ScScoringBoardVar entidad = new ScScoringBoardVar();

                            if (resultado["IDSCOREVAR"] != DBNull.Value) entidad.idscorevar = Convert.ToInt64(resultado["IDSCOREVAR"]);
                            if (resultado["IDSCORE"] != DBNull.Value) entidad.idscore = Convert.ToInt64(resultado["IDSCORE"]);
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["IDPARAMETRO"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.minimo = Convert.ToDecimal(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.maximo = Convert.ToDecimal(resultado["MAXIMO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["BETA"] != DBNull.Value) entidad.beta = Convert.ToDecimal(resultado["BETA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.descripcionParametro = Convert.ToString(resultado["NOMBRE"]);   
                            lstScScoringBoardVar.Add(entidad);
                        }
                        return lstScScoringBoardVar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScScoringBoardVarData", "ListarScScoringBoardVar", ex);
                        return null;
                    }
                }
            }
        }


    }
}