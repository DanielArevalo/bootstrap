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
    /// Objeto de acceso a datos para la tabla SCPARAMETRO
    /// </summary>
    public class ParametroData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PERSONA
        /// </summary>
        public ParametroData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        /// <summary>
        /// Crea un Parametro
        /// </summary>
        /// <param name="pParametro">Entidad Parametro</param>
        /// <returns>Entidad Parametro creada</returns>
        public Parametro CrearParametro(Parametro pParametro, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idparametroP = cmdTransaccionFactory.CreateParameter();
                        p_idparametroP.ParameterName = "p_idparametroP";
                        p_idparametroP.Value = pParametro.idparametro;
                        p_idparametroP.Direction = ParameterDirection.InputOutput;

                        DbParameter p_tipoP = cmdTransaccionFactory.CreateParameter();
                        p_tipoP.ParameterName = "p_tipoP";
                        p_tipoP.Value = pParametro.tipo;

                        DbParameter p_nombreP = cmdTransaccionFactory.CreateParameter();
                        p_nombreP.ParameterName = "p_nombreP";
                        p_nombreP.Value = pParametro.nombre;

                        DbParameter p_idvariableP = cmdTransaccionFactory.CreateParameter();
                        p_idvariableP.ParameterName = "p_idvariableP";
                        p_idvariableP.Value = pParametro.idvariable;

                        DbParameter p_formulaP = cmdTransaccionFactory.CreateParameter();
                        p_formulaP.ParameterName = "p_formulaP";
                        p_formulaP.Value = pParametro.formula;

                        DbParameter p_sentenciaP = cmdTransaccionFactory.CreateParameter();
                        p_sentenciaP.ParameterName = "p_sentenciaP";
                        p_sentenciaP.Value = pParametro.sentencia;

                        DbParameter p_campoP = cmdTransaccionFactory.CreateParameter();
                        p_campoP.ParameterName = "p_campoP";
                        p_campoP.Value = pParametro.campo;

                        cmdTransaccionFactory.Parameters.Add(p_idparametroP);
                        cmdTransaccionFactory.Parameters.Add(p_tipoP);
                        cmdTransaccionFactory.Parameters.Add(p_nombreP);
                        cmdTransaccionFactory.Parameters.Add(p_idvariableP);
                        cmdTransaccionFactory.Parameters.Add(p_formulaP);
                        cmdTransaccionFactory.Parameters.Add(p_sentenciaP);
                        cmdTransaccionFactory.Parameters.Add(p_campoP);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SCPARAM_CREA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pParametro, "SCPARAMETRO", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pParametro.idparametro = Convert.ToInt64(p_idparametroP.Value);

                        return pParametro;
                    }
                    catch (Exception ex)
                    {
                        string a = ex.ToString();
                        BOExcepcion.Throw("ParametroData", "CrearpParametro", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad pParametro modificada</returns>
        public Parametro ModificarParametro(Parametro pParametro, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idparametroP = cmdTransaccionFactory.CreateParameter();
                        p_idparametroP.ParameterName = "p_idparametroP";
                        p_idparametroP.Value = pParametro.idparametro;
                        p_idparametroP.Direction = ParameterDirection.InputOutput;

                        DbParameter p_tipoP = cmdTransaccionFactory.CreateParameter();
                        p_tipoP.ParameterName = "p_tipoP";
                        p_tipoP.Value = pParametro.tipo;

                        DbParameter p_nombreP = cmdTransaccionFactory.CreateParameter();
                        p_nombreP.ParameterName = "p_nombreP";
                        p_nombreP.Value = pParametro.nombre;

                        DbParameter p_idvariableP = cmdTransaccionFactory.CreateParameter();
                        p_idvariableP.ParameterName = "p_idvariableP";
                        p_idvariableP.Value = pParametro.idvariable;

                        DbParameter p_formulaP = cmdTransaccionFactory.CreateParameter();
                        p_formulaP.ParameterName = "p_formulaP";
                        p_formulaP.Value = pParametro.formula;

                        DbParameter p_sentenciaP = cmdTransaccionFactory.CreateParameter();
                        p_sentenciaP.ParameterName = "p_sentenciaP";
                        p_sentenciaP.Value = pParametro.sentencia;

                        DbParameter p_campoP = cmdTransaccionFactory.CreateParameter();
                        p_campoP.ParameterName = "p_campoP";
                        p_campoP.Value = pParametro.campo;

                        cmdTransaccionFactory.Parameters.Add(p_idparametroP);
                        cmdTransaccionFactory.Parameters.Add(p_tipoP);
                        cmdTransaccionFactory.Parameters.Add(p_nombreP);
                        cmdTransaccionFactory.Parameters.Add(p_idvariableP);
                        cmdTransaccionFactory.Parameters.Add(p_formulaP);
                        cmdTransaccionFactory.Parameters.Add(p_sentenciaP);
                        cmdTransaccionFactory.Parameters.Add(p_campoP);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SCPARAM_MODI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        connection.Close();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pParametro, "SCPARAMETRO", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pParametro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroData", "ModificarParametro", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PERSONA</param>
        public void EliminarParametro(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Parametro pParametro = new Parametro();

                        DbParameter pID = cmdTransaccionFactory.CreateParameter();
                        pID.ParameterName = "pID";
                        pID.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pID);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SCPARAM_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pParametro, "SCPARAMETRO", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroData", "EliminarParametro", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PERSONA</param>
        /// <returns>Entidad DefinirVariables consultado</returns>
        public Parametro ConsultarParametro(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Parametro entidad = new Parametro();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM SCPARAMETRO WHERE IDPARAMETRO =" + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["IDPARAMETRO"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["IDVARIABLE"] != DBNull.Value) entidad.idvariable = Convert.ToInt64(resultado["IDVARIABLE"]);
                            if (resultado["FORMULA"] != DBNull.Value) entidad.formula = Convert.ToString(resultado["FORMULA"]);
                            if (resultado["SENTENCIA"] != DBNull.Value) entidad.sentencia = Convert.ToString(resultado["SENTENCIA"]);
                            if (resultado["CAMPO"] != DBNull.Value) entidad.campo = Convert.ToString(resultado["CAMPO"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroData", "ConsultarParametro", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla SCParametro dados unos filtros
        /// </summary>
        /// <param name="pPERSONA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de DefinirVariables obtenidos</returns>
        public List<Parametro> ListarParametro(Parametro pParametro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Parametro> lstParametro = new List<Parametro>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * FROM ScParametro ORDER BY IDPARAMETRO ASC";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Parametro entidad = new Parametro();

                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["IDPARAMETRO"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["IDVARIABLE"] != DBNull.Value) entidad.idvariable = Convert.ToInt64(resultado["IDVARIABLE"]);
                            if (resultado["FORMULA"] != DBNull.Value) entidad.formula = Convert.ToString(resultado["FORMULA"]);
                            if (resultado["SENTENCIA"] != DBNull.Value) entidad.sentencia = Convert.ToString(resultado["SENTENCIA"]);
                            if (resultado["CAMPO"] != DBNull.Value) entidad.campo = Convert.ToString(resultado["CAMPO"]);
                            lstParametro.Add(entidad);
                        }
                        return lstParametro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroData", "ListarParametro", ex);
                        return null;
                    }
                }
            }
        }

        
    }
}