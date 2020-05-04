using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla MATRIZ_RIESGO_FACTOR
    /// </summary>
    public class MatrizRiesgoFactorNIFData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla MATRIZ_RIESGO_FACTOR
        /// </summary>
        public MatrizRiesgoFactorNIFData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla MATRIZ_RIESGO_FACTOR de la base de datos
        /// </summary>
        /// <param name="pScScoringBoardVar">Entidad ScScoringBoardVar</param>
        /// <returns>Entidad ScScoringBoardVar creada</returns>
        public MatrizRiesgoFactorNIF CrearMatrizRiesgoFactorNIF(MatrizRiesgoFactorNIF pMatrizRiesgoFactorNIF, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidfactorpondera = cmdTransaccionFactory.CreateParameter();
                        pidfactorpondera.ParameterName = "p_idfactorpondera";
                        pidfactorpondera.Value = pMatrizRiesgoFactorNIF.idfactorpondera;
                        pidfactorpondera.Direction = ParameterDirection.Output;
                        pidfactorpondera.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidfactorpondera);

                        DbParameter pidmatriz = cmdTransaccionFactory.CreateParameter();
                        pidmatriz.ParameterName = "p_idmatriz";
                        pidmatriz.Value = pMatrizRiesgoFactorNIF.idmatriz;
                        pidmatriz.Direction = ParameterDirection.Input;
                        pidmatriz.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidmatriz);

                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pMatrizRiesgoFactorNIF.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pminimo = cmdTransaccionFactory.CreateParameter();
                        pminimo.ParameterName = "p_minimo";
                        pminimo.Value = pMatrizRiesgoFactorNIF.minimo;
                        pminimo.Direction = ParameterDirection.Input;
                        pminimo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pminimo);

                        DbParameter pmaximo = cmdTransaccionFactory.CreateParameter();
                        pmaximo.ParameterName = "p_maximo";
                        pmaximo.Value = pMatrizRiesgoFactorNIF.maximo;
                        pmaximo.Direction = ParameterDirection.Input;
                        pmaximo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmaximo);

                        DbParameter pfactor = cmdTransaccionFactory.CreateParameter();
                        pfactor.ParameterName = "p_factor";
                        pfactor.Value = pMatrizRiesgoFactorNIF.factor;
                        pfactor.Direction = ParameterDirection.Input;
                        pfactor.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pfactor);

                        DbParameter pvariable = cmdTransaccionFactory.CreateParameter();
                        pvariable.ParameterName = "p_variable";
                        pvariable.Value = pMatrizRiesgoFactorNIF.variable;
                        pvariable.Direction = ParameterDirection.Input;
                        pvariable.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvariable);

                        DbParameter pcalificacion = cmdTransaccionFactory.CreateParameter();
                        pcalificacion.ParameterName = "p_calificacion";
                        pcalificacion.Value = pMatrizRiesgoFactorNIF.calificacion;
                        pcalificacion.Direction = ParameterDirection.Input;
                        pcalificacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcalificacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_MATRIZFACTOR_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pMatrizRiesgoFactorNIF.idfactorpondera = Convert.ToInt32(pidfactorpondera.Value);

                        return pMatrizRiesgoFactorNIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizRiesgoFactorNIFData", "CrearMatrizRiesgoFactorNIF", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Modifica un registro en la tabla MATRIZ_RIESGO_FACTOR de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ScScoringBoardVar modificada</returns>
        public MatrizRiesgoFactorNIF ModificarMatrizRiesgoFactorNIF(MatrizRiesgoFactorNIF pMatrizRiesgoFactorNIF, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidfactorpondera = cmdTransaccionFactory.CreateParameter();
                        pidfactorpondera.ParameterName = "p_idfactorpondera";
                        pidfactorpondera.Value = pMatrizRiesgoFactorNIF.idfactorpondera;
                        pidfactorpondera.Direction = ParameterDirection.Input;
                        pidfactorpondera.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidfactorpondera);

                        DbParameter pidmatriz = cmdTransaccionFactory.CreateParameter();
                        pidmatriz.ParameterName = "p_idmatriz";
                        pidmatriz.Value = pMatrizRiesgoFactorNIF.idmatriz;
                        pidmatriz.Direction = ParameterDirection.Input;
                        pidmatriz.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidmatriz);

                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pMatrizRiesgoFactorNIF.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pminimo = cmdTransaccionFactory.CreateParameter();
                        pminimo.ParameterName = "p_minimo";
                        pminimo.Value = pMatrizRiesgoFactorNIF.minimo;
                        pminimo.Direction = ParameterDirection.Input;
                        pminimo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pminimo);

                        DbParameter pmaximo = cmdTransaccionFactory.CreateParameter();
                        pmaximo.ParameterName = "p_maximo";
                        pmaximo.Value = pMatrizRiesgoFactorNIF.maximo;
                        pmaximo.Direction = ParameterDirection.Input;
                        pmaximo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmaximo);

                        DbParameter pfactor = cmdTransaccionFactory.CreateParameter();
                        pfactor.ParameterName = "p_factor";
                        pfactor.Value = pMatrizRiesgoFactorNIF.factor;
                        pfactor.Direction = ParameterDirection.Input;
                        pfactor.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pfactor);

                        DbParameter pvariable = cmdTransaccionFactory.CreateParameter();
                        pvariable.ParameterName = "p_variable";
                        pvariable.Value = pMatrizRiesgoFactorNIF.variable;
                        pvariable.Direction = ParameterDirection.Input;
                        pvariable.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvariable);

                        DbParameter pcalificacion = cmdTransaccionFactory.CreateParameter();
                        pcalificacion.ParameterName = "p_calificacion";
                        pcalificacion.Value = pMatrizRiesgoFactorNIF.calificacion;
                        pcalificacion.Direction = ParameterDirection.Input;
                        pcalificacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcalificacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_MATRIZFACTOR_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        connection.Close();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pMatrizRiesgoFactorNIF, "MATRIZ_RIESGO_FACTOR_NIF", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pMatrizRiesgoFactorNIF;
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
        /// Elimina un registro en la tabla MATRIZ_RIESGO_FACTOR de la base de datos
        /// </summary>
        /// <param name="pId">identificador de MATRIZ_RIESGO_FACTOR</param>
        public void EliminarMatrizRiesgoFactorNIF(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        MatrizRiesgoFactorNIF pMatrizRiesgoFactorNIF = new MatrizRiesgoFactorNIF();
                        pMatrizRiesgoFactorNIF = ConsultarMatrizRiesgoFactorNIF(pId, vUsuario);

                        DbParameter pidfactorpondera = cmdTransaccionFactory.CreateParameter();
                        pidfactorpondera.ParameterName = "p_idfactorpondera";
                        pidfactorpondera.Value = pMatrizRiesgoFactorNIF.idfactorpondera;
                        pidfactorpondera.Direction = ParameterDirection.Input;
                        pidfactorpondera.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidfactorpondera);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_MATRIZFACTOR_ELI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizRiesgoFactorNIFData", "EliminarMatrizRiesgoFactorNIF", ex);
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro en la tabla MATRIZ_RIESGO_FACTOR de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla MATRIZ_RIESGO_FACTOR</param>
        /// <returns>Entidad ScScoringBoardVar consultado</returns>
        public MatrizRiesgoFactorNIF ConsultarMatrizRiesgoFactorNIF(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            MatrizRiesgoFactorNIF entidad = new MatrizRiesgoFactorNIF();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM MATRIZ_RIESGO_FACTOR_NIF WHERE IDFACTORPONDERA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDFACTORPONDERA"] != DBNull.Value) entidad.idfactorpondera = Convert.ToInt32(resultado["IDFACTORPONDERA"]);
                            if (resultado["IDMATRIZ"] != DBNull.Value) entidad.idmatriz = Convert.ToInt32(resultado["IDMATRIZ"]);
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt32(resultado["IDPARAMETRO"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["MAXIMO"]);
                            if (resultado["FACTOR"] != DBNull.Value) entidad.factor = Convert.ToInt32(resultado["FACTOR"]);
                            if (resultado["VARIABLE"] != DBNull.Value) entidad.variable = Convert.ToString(resultado["VARIABLE"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToDecimal(resultado["CALIFICACION"]);
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
                        BOExcepcion.Throw("MatrizRiesgoFactorNIFData", "ConsultarMatrizRiesgoFactorNIF", ex);
                        return null;
                    }
                }
            }
        }

        
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla MATRIZ_RIESGO_FACTOR dados unos filtros
        /// </summary>
        /// <param name="pMATRIZ_RIESGO_FACTOR">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ScScoringBoardVar obtenidos</returns>
        public List<MatrizRiesgoFactorNIF> ListarMatrizRiesgoFactorNIF(MatrizRiesgoFactorNIF pMatrizRiesgoFactorNIF, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<MatrizRiesgoFactorNIF> lstMatrizRiesgoFactorNIF = new List<MatrizRiesgoFactorNIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT m.*, p.nombre AS descripcionparametro FROM MATRIZ_RIESGO_FACTOR_NIF m LEFT JOIN scparametro p ON m.idparametro = p.idparametro " + ObtenerFiltro(pMatrizRiesgoFactorNIF, "m.") + " ORDER BY IDFACTORPONDERA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            MatrizRiesgoFactorNIF entidad = new MatrizRiesgoFactorNIF();
                            if (resultado["IDFACTORPONDERA"] != DBNull.Value) entidad.idfactorpondera = Convert.ToInt32(resultado["IDFACTORPONDERA"]);
                            if (resultado["IDMATRIZ"] != DBNull.Value) entidad.idmatriz = Convert.ToInt32(resultado["IDMATRIZ"]);
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt32(resultado["IDPARAMETRO"]);
                            if (resultado["DESCRIPCIONPARAMETRO"] != DBNull.Value) entidad.descripcionparametro = Convert.ToString(resultado["DESCRIPCIONPARAMETRO"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["MAXIMO"]);
                            if (resultado["FACTOR"] != DBNull.Value) entidad.factor = Convert.ToInt32(resultado["FACTOR"]);
                            if (resultado["VARIABLE"] != DBNull.Value) entidad.variable = Convert.ToString(resultado["VARIABLE"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToDecimal(resultado["CALIFICACION"]);
                            lstMatrizRiesgoFactorNIF.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstMatrizRiesgoFactorNIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizRiesgoFactorNIFData", "ListarMatrizRiesgoFactorNIF", ex);
                        return null;
                    }
                }
            }
        }


    }
}