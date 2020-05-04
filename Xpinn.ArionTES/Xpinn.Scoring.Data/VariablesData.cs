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
    /// Objeto de acceso a datos para la tabla SCVARIABLE
    /// </summary>
    public class VariablesData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PERSONA
        /// </summary>
        public VariablesData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pDefinirVariables">Entidad DefinirVariables</param>
        /// <returns>Entidad DefinirVariables creada</returns>
        public Variables CrearDefinirVariables(Variables pDefinirVariables, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idvariable = cmdTransaccionFactory.CreateParameter();
                        p_idvariable.ParameterName = "p_idvariable";
                        p_idvariable.Value = pDefinirVariables.idvariable;
                        p_idvariable.Direction = ParameterDirection.InputOutput;

                        DbParameter p_variable = cmdTransaccionFactory.CreateParameter();
                        p_variable.ParameterName = "p_variable";
                        p_variable.Value = pDefinirVariables.variable;

                        DbParameter p_nombre = cmdTransaccionFactory.CreateParameter();
                        p_nombre.ParameterName = "p_nombre";
                        p_nombre.Value = pDefinirVariables.nombre;

                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        p_tipo.Value = pDefinirVariables.tipo;

                        DbParameter p_sentencia = cmdTransaccionFactory.CreateParameter();
                        p_sentencia.ParameterName = "p_sentencia";
                        p_sentencia.Value = pDefinirVariables.sentencia;

                        cmdTransaccionFactory.Parameters.Add(p_idvariable);
                        cmdTransaccionFactory.Parameters.Add(p_variable);
                        cmdTransaccionFactory.Parameters.Add(p_nombre);
                        cmdTransaccionFactory.Parameters.Add(p_tipo);
                        cmdTransaccionFactory.Parameters.Add(p_sentencia);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SCVARIA_CREA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pDefinirVariables, "CrearDefinirVariables", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pDefinirVariables.idvariable = Convert.ToInt64(p_idvariable.Value);

                        return pDefinirVariables;
                    }
                    catch (Exception ex)
                    {
                        string a = ex.ToString();
                        BOExcepcion.Throw("DefinirVariablesData", "CrearDefinirVariables", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad DefinirVariables modificada</returns>
        public Variables ModificarDefinirVariables(Variables pDefinirVariables, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idvariable = cmdTransaccionFactory.CreateParameter();
                        p_idvariable.ParameterName = "p_idvariable";
                        p_idvariable.Value = pDefinirVariables.idvariable;
                        p_idvariable.Direction = ParameterDirection.InputOutput;

                        DbParameter p_variable = cmdTransaccionFactory.CreateParameter();
                        p_variable.ParameterName = "p_variable";
                        p_variable.Value = pDefinirVariables.variable;

                        DbParameter p_nombre = cmdTransaccionFactory.CreateParameter();
                        p_nombre.ParameterName = "p_nombre";
                        p_nombre.Value = pDefinirVariables.nombre;

                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        p_tipo.Value = pDefinirVariables.tipo;

                        DbParameter p_sentencia = cmdTransaccionFactory.CreateParameter();
                        p_sentencia.ParameterName = "p_sentencia";
                        p_sentencia.Value = pDefinirVariables.sentencia;

                        cmdTransaccionFactory.Parameters.Add(p_idvariable);
                        cmdTransaccionFactory.Parameters.Add(p_variable);
                        cmdTransaccionFactory.Parameters.Add(p_nombre);
                        cmdTransaccionFactory.Parameters.Add(p_tipo);
                        cmdTransaccionFactory.Parameters.Add(p_sentencia);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SCVARIA_MODI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        connection.Close();
                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pDefinirVariables, "SCBOCAL", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pDefinirVariables;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DefinirVariablesData", "ModificarDefinirVariables", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PERSONA</param>
        public void EliminarDefinirVariables(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Variables pDefinirVariables = new Variables();

                        DbParameter pID = cmdTransaccionFactory.CreateParameter();
                        pID.ParameterName = "pID";
                        pID.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pID);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SCVARIA_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DefinirVariablesData", "InsertarDefinirVariables", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PERSONA</param>
        /// <returns>Entidad DefinirVariables consultado</returns>
        public Variables ConsultarDefinirVariables(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Variables entidad = new Variables();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM DefinirVariables where idscore =" + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //if (resultado["IDSCORECAL"] != DBNull.Value) entidad.idscorecal = Convert.ToInt64(resultado["IDSCORECAL"]);
                            //if (resultado["IDSCORE"] != DBNull.Value) entidad.idscore = Convert.ToInt64(resultado["IDSCORE"]);
                            //if (resultado["CAL_MINIMO"] != DBNull.Value) entidad.cal_minimo = Convert.ToInt64(resultado["CAL_MINIMO"]);
                            //if (resultado["CAL_MAXIMO"] != DBNull.Value) entidad.cal_maximo = Convert.ToInt64(resultado["CAL_MAXIMO"]);
                            //if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToString(resultado["CALIFICACION"]);
                            //if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);
                            //if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DefinirVariablesData", "ConsultarDefinirVariables", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PERSONA dados unos filtros
        /// </summary>
        /// <param name="pPERSONA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de DefinirVariables obtenidos</returns>
        public List<Variables> ListarDefinirVariables(Variables pDefinirVariables, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Variables> lstDefinirVariables = new List<Variables>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * FROM Scvariable ORDER BY IDVARIABLE ASC";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Variables entidad = new Variables();

                            if (resultado["IDVARIABLE"] != DBNull.Value) entidad.idvariable = Convert.ToInt64(resultado["IDVARIABLE"]);
                            if (resultado["VARIABLE1"] != DBNull.Value) entidad.variable = Convert.ToString(resultado["VARIABLE1"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);
                            if (resultado["SENTENCIA"] != DBNull.Value) entidad.sentencia = Convert.ToString(resultado["SENTENCIA"]);                         
                            lstDefinirVariables.Add(entidad);
                        }
                        return lstDefinirVariables;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DefinirVariablesData", "ListarDefinirVariables", ex);
                        return null;
                    }
                }
            }
        }

    }
}