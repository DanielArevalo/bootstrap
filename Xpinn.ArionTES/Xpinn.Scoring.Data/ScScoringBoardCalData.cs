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
    public class ScScoringBoardCalData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PERSONA
        /// </summary>
        public ScScoringBoardCalData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pScScoringBoardCal">Entidad ScScoringBoardCal</param>
        /// <returns>Entidad ScScoringBoardCal creada</returns>
        public ScScoringBoardCal CrearScScoringBoardCal(ScScoringBoardCal pScScoringBoardCal, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDSCORECAL = cmdTransaccionFactory.CreateParameter();
                        pIDSCORECAL.ParameterName = "pIDSCORECAL";
                        pIDSCORECAL.Value = pScScoringBoardCal.idscorecal;
                        pIDSCORECAL.Direction = ParameterDirection.InputOutput;

                        DbParameter pIDSCORE = cmdTransaccionFactory.CreateParameter();
                        pIDSCORE.ParameterName = "pIDSCORE";
                        pIDSCORE.Value = pScScoringBoardCal.idscore;

                        DbParameter pCAL_MINIMO = cmdTransaccionFactory.CreateParameter();
                        pCAL_MINIMO.ParameterName = "pCAL_MINIMO";
                        pCAL_MINIMO.Value = pScScoringBoardCal.cal_minimo;

                        DbParameter pCAL_MAXIMO = cmdTransaccionFactory.CreateParameter();
                        pCAL_MAXIMO.ParameterName = "pCAL_MAXIMO";
                        pCAL_MAXIMO.Value = pScScoringBoardCal.cal_maximo;

                        DbParameter pCALIFICACION = cmdTransaccionFactory.CreateParameter();
                        pCALIFICACION.ParameterName = "pCALIFICACION";
                        pCALIFICACION.Value = pScScoringBoardCal.calificacion;

                        DbParameter pTIPO = cmdTransaccionFactory.CreateParameter();
                        pTIPO.ParameterName = "pTIPO";
                        pTIPO.Value = pScScoringBoardCal.tipo;

                        DbParameter pOBSERVACION = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACION.ParameterName = "pOBSERVACION";
                        pOBSERVACION.Value = pScScoringBoardCal.observacion;

                        cmdTransaccionFactory.Parameters.Add(pIDSCORECAL);
                        cmdTransaccionFactory.Parameters.Add(pIDSCORE);
                        cmdTransaccionFactory.Parameters.Add(pCAL_MINIMO);
                        cmdTransaccionFactory.Parameters.Add(pCAL_MAXIMO);
                        cmdTransaccionFactory.Parameters.Add(pCALIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pTIPO);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SCBOCAL_CREA";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pScScoringBoardCal, "CrearScScoringBoardCal", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pScScoringBoardCal.idscore = Convert.ToInt64(pIDSCORE.Value);

                        return pScScoringBoardCal;
                    }
                    catch (Exception ex)
                    {
                        string a = ex.ToString();
                        BOExcepcion.Throw("ScScoringBoardCalData", "CrearScScoringBoardCal", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ScScoringBoardCal modificada</returns>
        public ScScoringBoardCal ModificarScScoringBoardCal(ScScoringBoardCal pScScoringBoardCal, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDSCORECAL = cmdTransaccionFactory.CreateParameter();
                        pIDSCORECAL.ParameterName = "pIDSCORECAL";
                        pIDSCORECAL.Value = pScScoringBoardCal.idscorecal;
                        pIDSCORECAL.Direction = ParameterDirection.InputOutput;

                        DbParameter pIDSCORE = cmdTransaccionFactory.CreateParameter();
                        pIDSCORE.ParameterName = "pIDSCORE";
                        pIDSCORE.Value = pScScoringBoardCal.idscore;

                        DbParameter pCAL_MINIMO = cmdTransaccionFactory.CreateParameter();
                        pCAL_MINIMO.ParameterName = "pCAL_MINIMO";
                        pCAL_MINIMO.Value = pScScoringBoardCal.cal_minimo;

                        DbParameter pCAL_MAXIMO = cmdTransaccionFactory.CreateParameter();
                        pCAL_MAXIMO.ParameterName = "pCAL_MAXIMO";
                        pCAL_MAXIMO.Value = pScScoringBoardCal.cal_maximo;

                        DbParameter pCALIFICACION = cmdTransaccionFactory.CreateParameter();
                        pCALIFICACION.ParameterName = "pCALIFICACION";
                        pCALIFICACION.Value = pScScoringBoardCal.calificacion;

                        DbParameter pTIPO = cmdTransaccionFactory.CreateParameter();
                        pTIPO.ParameterName = "pTIPO";
                        pTIPO.Value = pScScoringBoardCal.tipo;

                        DbParameter pOBSERVACION = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACION.ParameterName = "pOBSERVACION";
                        pOBSERVACION.Value = pScScoringBoardCal.observacion;

                        cmdTransaccionFactory.Parameters.Add(pIDSCORECAL);
                        cmdTransaccionFactory.Parameters.Add(pIDSCORE);
                        cmdTransaccionFactory.Parameters.Add(pCAL_MINIMO);
                        cmdTransaccionFactory.Parameters.Add(pCAL_MAXIMO);
                        cmdTransaccionFactory.Parameters.Add(pCALIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pTIPO);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SCBOCAL_MODI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        connection.Close();
                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pScScoringBoardCal, "SCBOCAL", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pScScoringBoardCal;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScScoringBoardCalData", "ModificarScScoringBoardCal", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PERSONA</param>
        public void EliminarScScoringBoardCal(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ScScoringBoardCal pScScoringBoardCal = new ScScoringBoardCal();

                        DbParameter pIDSCORECAL = cmdTransaccionFactory.CreateParameter();
                        pIDSCORECAL.ParameterName = "pIDSCORECAL";
                        pIDSCORECAL.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pIDSCORECAL);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SCBOCAL_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScScoringBoardCalData", "InsertarScScoringBoardCal", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PERSONA</param>
        /// <returns>Entidad ScScoringBoardCal consultado</returns>
        public ScScoringBoardCal ConsultarScScoringBoardCal(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ScScoringBoardCal entidad = new ScScoringBoardCal();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM ScScoringBoardCal where idscore =" + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDSCORECAL"] != DBNull.Value) entidad.idscorecal = Convert.ToInt64(resultado["IDSCORECAL"]);
                            if (resultado["IDSCORE"] != DBNull.Value) entidad.idscore = Convert.ToInt64(resultado["IDSCORE"]);
                            if (resultado["CAL_MINIMO"] != DBNull.Value) entidad.cal_minimo = Convert.ToInt64(resultado["CAL_MINIMO"]);
                            if (resultado["CAL_MAXIMO"] != DBNull.Value) entidad.cal_maximo = Convert.ToInt64(resultado["CAL_MAXIMO"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToString(resultado["CALIFICACION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScScoringBoardCalData", "ConsultarScScoringBoardCal", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PERSONA dados unos filtros
        /// </summary>
        /// <param name="pPERSONA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ScScoringBoardCal obtenidos</returns>
        public List<ScScoringBoardCal> ListarScScoringBoardCal(ScScoringBoardCal pScScoringBoardCal, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ScScoringBoardCal> lstScScoringBoardCal = new List<ScScoringBoardCal>();

            if (pScScoringBoardCal == null)
                return null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * FROM ScScoringBoard_CAL where idscore =" + pScScoringBoardCal.idscore +  " order by IDSCORECAL asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ScScoringBoardCal entidad = new ScScoringBoardCal();

                            if (resultado["IDSCORECAL"] != DBNull.Value) entidad.idscorecal = Convert.ToInt64(resultado["IDSCORECAL"]);
                            if (resultado["IDSCORE"] != DBNull.Value) entidad.idscore = Convert.ToInt64(resultado["IDSCORE"]);
                            if (resultado["CAL_MINIMO"] != DBNull.Value) entidad.cal_minimo = Convert.ToInt64(resultado["CAL_MINIMO"]);
                            if (resultado["CAL_MAXIMO"] != DBNull.Value) entidad.cal_maximo = Convert.ToInt64(resultado["CAL_MAXIMO"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToString(resultado["CALIFICACION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);                           
                            lstScScoringBoardCal.Add(entidad);
                        }
                        return lstScScoringBoardCal;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScScoringBoardCalData", "ListarScScoringBoardCal", ex);
                        return null;
                    }
                }
            }
        }
    }
}