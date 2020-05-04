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
    public class ScoringCreditosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PERSONA
        /// </summary>
        public ScoringCreditosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<ScoringCreditos> ListarScoringCredito(ScoringCreditos pCredito, String pEstadoCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ScoringCreditos> lstScoringCreditos = new List<ScoringCreditos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from V_SCORING_CREDITOS ";
                        sql = sql + filtro;
                        if (pEstadoCredito.Contains(","))
                            if (sql.ToUpper().Contains("WHERE"))
                                sql = sql + " AND estado In (" + pEstadoCredito + ") ";
                            else
                                sql = sql + " WHERE estado In (" + pEstadoCredito + ") ";
                        else
                            if (sql.ToUpper().Contains("WHERE"))
                                sql = sql + " AND estado = '" + pEstadoCredito + "' ";
                            else
                                sql = sql + " WHERE estado = '" + pEstadoCredito + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ScoringCreditos entidad = new ScoringCreditos();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.Numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.Cod_Cliente = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.FechaSolicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["MONTO_SOLICITADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.Plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.Periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.DescripcionIdentificacion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["EJECUTIVO"] != DBNull.Value) entidad.Ejecutivo = Convert.ToString(resultado["EJECUTIVO"]);
                            lstScoringCreditos.Add(entidad);
                        }

                        return lstScoringCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringCreditosData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<ScoringCreditos> ListarCreditosRecogidos(ScoringCreditos pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ScoringCreditos> lstScoringCreditos = new List<ScoringCreditos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from V_SCORING_CREDITOSRECOGIDOS " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ScoringCreditos entidad = new ScoringCreditos();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.Numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["nombre"]);
                            if (resultado["monto_solicitado"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["monto_solicitado"]);
                            if (resultado["saldocapital"] != DBNull.Value) entidad.saldocapital = Convert.ToInt64(resultado["saldocapital"]);
                            if (resultado["numero_cuotas"] != DBNull.Value) entidad.Plazo = Convert.ToInt64(resultado["numero_cuotas"]);
                            if (resultado["cuotas_pendientes"] != DBNull.Value) entidad.cuotas_pendientes = Convert.ToInt64(resultado["cuotas_pendientes"]);
                            if (resultado["valorrecoge"] != DBNull.Value) entidad.valorrecoge = Convert.ToInt64(resultado["valorrecoge"]);
                            lstScoringCreditos.Add(entidad);
                        }

                        return lstScoringCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringCreditosData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<ScoringCreditos> ListarCodeudores(ScoringCreditos pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ScoringCreditos> lstScoringCreditos = new List<ScoringCreditos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from V_SCORING_CODEUDORES " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ScoringCreditos entidad = new ScoringCreditos();

                            if (resultado["numerosolicitud"] != DBNull.Value) entidad.NumeroSolicitud = Convert.ToInt64(resultado["numerosolicitud"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["nombre"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["telefono"]);
                            lstScoringCreditos.Add(entidad);
                        }

                        return lstScoringCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringCreditosData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<ScoringCreditos> ListarAprobScoringNegados(ScoringCreditos pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ScoringCreditos> lstScoringCreditos = new List<ScoringCreditos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from V_SCORING_APROBACION_NEGADOS " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ScoringCreditos entidad = new ScoringCreditos();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumeroSolicitud = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["MONTO_SOLICITADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.Plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PUNTSCORING"] != DBNull.Value) entidad.puntscoring = Convert.ToInt64(resultado["PUNTSCORING"]);
                            lstScoringCreditos.Add(entidad);
                        }

                        return lstScoringCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringCreditosData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Crea un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pScScoringBoardVar">Entidad ScScoringBoardVar</param>
        /// <returns>Entidad ScScoringBoardVar creada</returns>
        public ScScoringCredito CrearScScoringCredito(ScScoringCredito pScScoringCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idscorecre  = cmdTransaccionFactory.CreateParameter();
                        p_idscorecre.ParameterName = "p_idscorecre";
                        p_idscorecre.Value = pScScoringCredito.idscorecre;
                        p_idscorecre.Direction = ParameterDirection.InputOutput;

                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.DbType = DbType.Int64;
                        p_numero_radicacion.Value = pScScoringCredito.numero_radicacion;

                        DbParameter p_idscore = cmdTransaccionFactory.CreateParameter();
                        p_idscore.ParameterName = "p_idscore";
                        p_idscore.DbType = DbType.Int64;
                        p_idscore.Value = pScScoringCredito.idscore;

                        DbParameter p_fecha_scoring = cmdTransaccionFactory.CreateParameter();
                        p_fecha_scoring.ParameterName = "p_fecha_scoring";
                        p_fecha_scoring.DbType = DbType.Date;
                        p_fecha_scoring.Value = pScScoringCredito.fecha_scoring;

                        DbParameter p_resultado = cmdTransaccionFactory.CreateParameter();
                        p_resultado.ParameterName = "p_resultado";
                        p_resultado.DbType = DbType.Decimal;
                        p_resultado.Value = pScScoringCredito.resultado;

                        DbParameter p_calificacion = cmdTransaccionFactory.CreateParameter();
                        p_calificacion.ParameterName = "p_calificacion";
                        p_calificacion.DbType = DbType.String;
                        p_calificacion.Value = pScScoringCredito.calificacion;

                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        p_tipo.DbType = DbType.Int64;
                        p_tipo.Value = pScScoringCredito.tipo;

                        DbParameter p_observacion = cmdTransaccionFactory.CreateParameter();
                        p_observacion.ParameterName = "p_observacion";
                        p_observacion.DbType = DbType.String;
                        p_observacion.Value = pScScoringCredito.observacion;

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.DbType = DbType.Int64;
                        p_estado.Value = pScScoringCredito.estado;

                        DbParameter p_fechacreacion = cmdTransaccionFactory.CreateParameter();
                        p_fechacreacion.ParameterName = "p_fechacreacion";
                        p_fechacreacion.DbType = DbType.Date;
                        p_fechacreacion.Value = pScScoringCredito.fechacreacion;

                        DbParameter p_usuariocreacion = cmdTransaccionFactory.CreateParameter();
                        p_usuariocreacion.ParameterName = "p_usuariocreacion";
                        p_usuariocreacion.DbType = DbType.String;
                        p_usuariocreacion.Value = pUsuario.nombre;

                        DbParameter p_fecultmod = cmdTransaccionFactory.CreateParameter();
                        p_fecultmod.ParameterName = "p_fecultmod";
                        p_fecultmod.DbType = DbType.Date;
                        p_fecultmod.Value = pScScoringCredito.fecultmod;

                        DbParameter p_usuultmod = cmdTransaccionFactory.CreateParameter();
                        p_usuultmod.ParameterName = "p_usuultmod";
                        p_usuultmod.DbType = DbType.String;
                        p_usuultmod.Value = " ";

                        cmdTransaccionFactory.Parameters.Add(p_idscorecre);
                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_idscore);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_scoring);
                        cmdTransaccionFactory.Parameters.Add(p_resultado);
                        cmdTransaccionFactory.Parameters.Add(p_calificacion);
                        cmdTransaccionFactory.Parameters.Add(p_tipo);
                        cmdTransaccionFactory.Parameters.Add(p_observacion);
                        cmdTransaccionFactory.Parameters.Add(p_estado);
                        cmdTransaccionFactory.Parameters.Add(p_fechacreacion);
                        cmdTransaccionFactory.Parameters.Add(p_usuariocreacion);
                        cmdTransaccionFactory.Parameters.Add(p_fecultmod);
                        cmdTransaccionFactory.Parameters.Add(p_usuultmod);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_CREDITO_CREA";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pScScoringCredito, "CrearScScoringCredito", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pScScoringCredito.idscorecre = Convert.ToInt64(p_idscorecre.Value);

                        return pScScoringCredito;
                    }
                    catch (Exception ex)
                    {
                        string a = ex.ToString();
                        BOExcepcion.Throw("ScoringCreditosData", "CrearScScoringCredito", ex);
                        return null;
                    }
                }
            }
        }

        public ScScoringCredito ValidarScoringCredito(ScScoringCredito pScScoringCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.DbType = DbType.Int64;
                        p_numero_radicacion.Value = pScScoringCredito.numero_radicacion;

                        DbParameter p_idscore = cmdTransaccionFactory.CreateParameter();
                        p_idscore.ParameterName = "p_idscore";
                        p_idscore.DbType = DbType.Int64;
                        p_idscore.Value = pScScoringCredito.idscore;

                        DbParameter p_fecha_scoring = cmdTransaccionFactory.CreateParameter();
                        p_fecha_scoring.ParameterName = "p_fecha_scoring";
                        p_fecha_scoring.DbType = DbType.Date;
                        p_fecha_scoring.Value = pScScoringCredito.fecha_scoring;

                        DbParameter p_resultado = cmdTransaccionFactory.CreateParameter();
                        p_resultado.ParameterName = "p_resultado";
                        p_resultado.DbType = DbType.Decimal;
                        p_resultado.Value = pScScoringCredito.resultado;

                        DbParameter p_calificacion = cmdTransaccionFactory.CreateParameter();
                        p_calificacion.ParameterName = "p_calificacion";
                        p_calificacion.DbType = DbType.String;
                        p_calificacion.Value = pScScoringCredito.calificacion;

                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        p_tipo.DbType = DbType.Int64;
                        p_tipo.Value = pScScoringCredito.tipo;

                        DbParameter p_observacion = cmdTransaccionFactory.CreateParameter();
                        p_observacion.ParameterName = "p_observacion";
                        p_observacion.DbType = DbType.String;
                        p_observacion.Size = 200;
                        p_observacion.Value = pScScoringCredito.observacion;

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.DbType = DbType.Int64;
                        p_estado.Value = pScScoringCredito.estado;

                        DbParameter p_error = cmdTransaccionFactory.CreateParameter();
                        p_error.ParameterName = "p_error";
                        p_error.DbType = DbType.String;
                        p_error.Size = 200;
                        p_error.Direction = ParameterDirection.Output;
                        p_error.Value = pScScoringCredito.fechacreacion;

                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_idscore);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_scoring);
                        cmdTransaccionFactory.Parameters.Add(p_resultado);
                        cmdTransaccionFactory.Parameters.Add(p_calificacion);
                        cmdTransaccionFactory.Parameters.Add(p_tipo);
                        cmdTransaccionFactory.Parameters.Add(p_observacion);
                        cmdTransaccionFactory.Parameters.Add(p_estado);
                        cmdTransaccionFactory.Parameters.Add(p_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_CREDITO_VAL";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pScScoringCredito.error = "";
                        if (p_error.Value != null)
                            pScScoringCredito.error = p_error.Value.ToString();

                        return pScScoringCredito;
                    }
                    catch (Exception ex)
                    {
                        string a = ex.ToString();
                        BOExcepcion.Throw("ScoringCreditosData", "ValidarScoringCredito", ex);
                        return pScScoringCredito;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<ScScoringCredito> ListarScScoringCredito(ScScoringCredito pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ScScoringCredito> lstScoringCreditos = new List<ScScoringCredito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from SCSCORING_CREDITO " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ScScoringCredito entidad = new ScScoringCredito();

                            if (resultado["IDSCORECRE"] != DBNull.Value) entidad.idscorecre = Convert.ToInt64(resultado["IDSCORECRE"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDSCORE"] != DBNull.Value) entidad.idscore = Convert.ToInt64(resultado["IDSCORE"]);
                            if (resultado["FECHA_SCORING"] != DBNull.Value) entidad.fecha_scoring = Convert.ToDateTime(resultado["FECHA_SCORING"]);
                            if (resultado["RESULTADO"] != DBNull.Value) entidad.resultado = Convert.ToInt64(resultado["RESULTADO"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToString(resultado["CALIFICACION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                           
                            lstScoringCreditos.Add(entidad);
                        }

                        return lstScoringCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringCreditosData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }


      /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<SeguimientoScoring> ListarSegumientoScoring(SeguimientoScoring pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SeguimientoScoring> lstScoringCreditos = new List<SeguimientoScoring>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from v_scoring_seguimiento " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            SeguimientoScoring entidad = new SeguimientoScoring();

                            if (resultado["FECHAPROCESO"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHAPROCESO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuario = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.motivo = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);                           
                            lstScoringCreditos.Add(entidad);
                        }

                        return lstScoringCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringCreditosData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }

        public ScScoringCredito CalculaScScoringCredito(ScScoringCredito pScScoringCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNumero_Radicacion = cmdTransaccionFactory.CreateParameter();
                        pNumero_Radicacion.ParameterName = "pNumero_Radicacion";
                        pNumero_Radicacion.DbType = DbType.Int64;
                        pNumero_Radicacion.Value = pScScoringCredito.numero_radicacion;

                        DbParameter pCod_Cliente = cmdTransaccionFactory.CreateParameter();
                        pCod_Cliente.ParameterName = "pCod_Cliente";
                        pCod_Cliente.DbType = DbType.Int64;
                        pCod_Cliente.Value = pScScoringCredito.cod_cliente;

                        DbParameter pFecha_Scoring = cmdTransaccionFactory.CreateParameter();
                        pFecha_Scoring.ParameterName = "pFecha_Scoring";
                        pFecha_Scoring.DbType = DbType.Date;
                        pFecha_Scoring.Value = pScScoringCredito.fecha_scoring;

                        DbParameter pClase_Scoring = cmdTransaccionFactory.CreateParameter();
                        pClase_Scoring.ParameterName = "pClase_Scoring";
                        pClase_Scoring.DbType = DbType.Int64;
                        pClase_Scoring.Value = pScScoringCredito.clase_scoring;

                        DbParameter pNUsuario = cmdTransaccionFactory.CreateParameter();
                        pNUsuario.ParameterName = "pUsuario";
                        pNUsuario.DbType = DbType.String;
                        pNUsuario.Value = pUsuario.nombre;

                        DbParameter pidscore_cre = cmdTransaccionFactory.CreateParameter();
                        pidscore_cre.ParameterName = "pidscore_cre";
                        pidscore_cre.DbType = DbType.Int64;
                        pidscore_cre.Direction = ParameterDirection.Output;
                        pidscore_cre.Value = pScScoringCredito.idscore;

                        DbParameter pmodelo_cre = cmdTransaccionFactory.CreateParameter();
                        pmodelo_cre.ParameterName = "pmodelo_cre";
                        pmodelo_cre.DbType = DbType.Int64;
                        pmodelo_cre.Direction = ParameterDirection.Output;
                        pmodelo_cre.Value = pScScoringCredito.modelo;

                        DbParameter pResultado = cmdTransaccionFactory.CreateParameter();
                        pResultado.ParameterName = "pResultado";
                        pResultado.DbType = DbType.Int64;
                        pResultado.Direction = ParameterDirection.Output;
                        pResultado.Value = pScScoringCredito.resultado;

                        DbParameter pCalificacion = cmdTransaccionFactory.CreateParameter();
                        pCalificacion.ParameterName = "pCalificacion";
                        pCalificacion.DbType = DbType.String;
                        pCalificacion.Size = 50;
                        pCalificacion.Direction = ParameterDirection.Output;
                        pCalificacion.Value = pScScoringCredito.calificacion;

                        DbParameter pTipo = cmdTransaccionFactory.CreateParameter();
                        pTipo.DbType = DbType.Int64;
                        pTipo.Direction = ParameterDirection.Output;
                        pTipo.ParameterName = "pTipo";
                        pTipo.Value = pScScoringCredito.tipo;

                        DbParameter pObservacion = cmdTransaccionFactory.CreateParameter();
                        pObservacion.ParameterName = "pObservacion";
                        pObservacion.DbType = DbType.String;
                        pObservacion.Size = 1000;
                        pObservacion.Direction = ParameterDirection.Output;
                        pObservacion.Value = pScScoringCredito.observacion;

                        DbParameter pCalificacion_Cliente = cmdTransaccionFactory.CreateParameter();
                        pCalificacion_Cliente.ParameterName = "pCalificacion_Cliente";
                        pCalificacion_Cliente.DbType = DbType.Int64;
                        pCalificacion_Cliente.Direction = ParameterDirection.Output;
                        pCalificacion_Cliente.Value = pScScoringCredito.calificacion_cliente;

                        cmdTransaccionFactory.Parameters.Add(pNumero_Radicacion);
                        cmdTransaccionFactory.Parameters.Add(pCod_Cliente);
                        cmdTransaccionFactory.Parameters.Add(pFecha_Scoring);
                        cmdTransaccionFactory.Parameters.Add(pClase_Scoring);
                        cmdTransaccionFactory.Parameters.Add(pNUsuario);
                        cmdTransaccionFactory.Parameters.Add(pidscore_cre);
                        cmdTransaccionFactory.Parameters.Add(pmodelo_cre);
                        cmdTransaccionFactory.Parameters.Add(pResultado);
                        cmdTransaccionFactory.Parameters.Add(pCalificacion);
                        cmdTransaccionFactory.Parameters.Add(pTipo);
                        cmdTransaccionFactory.Parameters.Add(pObservacion);
                        cmdTransaccionFactory.Parameters.Add(pCalificacion_Cliente);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_CALCULAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pScScoringCredito.resultado = Convert.ToDouble(pResultado.Value);
                        pScScoringCredito.calificacion = Convert.ToString(pCalificacion.Value);
                        pScScoringCredito.calificacion_cliente = Convert.ToDouble(pCalificacion_Cliente.Value);
                        pScScoringCredito.tipo = Convert.ToInt64(pTipo.Value);
                        pScScoringCredito.modelo = Convert.ToInt64(pmodelo_cre.Value);
                        pScScoringCredito.observacion = Convert.ToString(pObservacion.Value);

                        return pScScoringCredito;
                    }
                    catch (Exception ex)
                    {
                        string a = ex.ToString();
                        BOExcepcion.Throw("ScoringCreditoData", "CalculaScScoringCredito", ex);
                        return null;
                    }
                }
            }
        }

        public List<ScScoringCreditoDetalle> ListarScoringCreditoDetalle(ScScoringCredito pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ScScoringCreditoDetalle> lstScoringCreditos = new List<ScScoringCreditoDetalle>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from TEMP_SCORING Where numero_radicacion = " + pCredito .numero_radicacion + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ScScoringCreditoDetalle entidad = new ScScoringCreditoDetalle();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["IDPARAMETRO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
                            if (resultado["PUNTAJE"] != DBNull.Value) entidad.puntaje = Convert.ToDouble(resultado["PUNTAJE"]);
                            lstScoringCreditos.Add(entidad);
                        }

                        return lstScoringCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringCreditosData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }

        public List<RiesgoCredito> ListarFechaCierreYaHechas(string pTipo = "R",  string pEstado = "D", Usuario usuario = null)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RiesgoCredito> lista = new List<RiesgoCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT fecha FROM cierea WHERE tipo = '" + pTipo + "' AND estado = '" + pEstado + "' ";
                        sql += " ORDER BY 1 DESC";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RiesgoCredito entidad = new RiesgoCredito();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha_corte = Convert.ToDateTime(resultado["FECHA"]);

                            lista.Add(entidad);
                        }

                        return lista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringCreditosData", "ListarFechaCierreYaHechas", ex);
                        return null;
                    }

                }
            }
        }

        public List<RiesgoCredito> ListarRiesgoCredito(DateTime pFechaCorte, String filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RiesgoCredito> lstScoringCreditos = new List<RiesgoCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string fecha_corte = "", fecha_corte_ini = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            fecha_corte = " To_Date('" + pFechaCorte.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "', 'NLS_DATE_LANGUAGE = American') ";
                        else
                            fecha_corte = " '" + pFechaCorte.ToString(conf.ObtenerFormatoFecha()) + "' ";
                        DateTime? fecha_inicial = (pFechaCorte == DateTime.MinValue ? DateTime.MinValue : pFechaCorte.AddDays(-365));
                        if (fecha_inicial != DateTime.MinValue)
                        { 
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                fecha_corte_ini = " To_Date('" + Convert.ToDateTime(fecha_inicial).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "', 'NLS_DATE_LANGUAGE = American') ";
                            else
                                fecha_corte_ini = " '" + Convert.ToDateTime(fecha_inicial).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        string sql = @"Select hc.*, DiasMoraPagoPromedio(hc.numero_radicacion, " + fecha_corte_ini + ", " + fecha_corte + @") As servicio
                                        From V_CREDITOS_SCORING hc Where hc.Fecha_historico = " + fecha_corte + " " + (filtro.Trim() == "" ? "": filtro);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        { 
                            RiesgoCredito entidad = new RiesgoCredito();
                            if (resultado["FECHA_HISTORICO"] != DBNull.Value) entidad.fecha_corte = Convert.ToDateTime(resultado["FECHA_HISTORICO"]);
                            if (resultado["NOMBRE_OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["NOMBRE_OFICINA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TOTAL_INGRESOS"] != DBNull.Value) entidad.total_ingresos = Convert.ToDecimal(resultado["TOTAL_INGRESOS"]);
                            if (resultado["TOTAL_ACTIVOS"] != DBNull.Value) entidad.total_activos = Convert.ToDecimal(resultado["TOTAL_ACTIVOS"]);
                            if (resultado["TOTAL_PASIVOS"] != DBNull.Value) entidad.total_pasivos = Convert.ToDecimal(resultado["TOTAL_PASIVOS"]);
                            if (resultado["LINEA_CREDITO"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA_CREDITO"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["MONTO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt32(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToInt32(resultado["DIAS_MORA"]);
                            if (resultado["APORTES"] != DBNull.Value) entidad.aportes = Convert.ToDecimal(resultado["APORTES"]);
                            if (resultado["COD_TIPO_GARANTIA"] != DBNull.Value) entidad.cod_tipo_garantia = Convert.ToString(resultado["COD_TIPO_GARANTIA"]);
                            if (resultado["TIPO_GARANTIA"] != DBNull.Value) entidad.tipo_garantia = Convert.ToString(resultado["TIPO_GARANTIA"]);
                            if (resultado["VALOR_AVALUO"] != DBNull.Value) entidad.valor_avaluo = Convert.ToDecimal(resultado["VALOR_AVALUO"]);
                            if (resultado["VALOR_GARANTIA"] != DBNull.Value) entidad.valor_garantia = Convert.ToDecimal(resultado["VALOR_GARANTIA"]);
                            if (resultado["REESTRUCTURADO"] != DBNull.Value) entidad.reestructurado = Convert.ToInt32(resultado["REESTRUCTURADO"]);
                            if (resultado["ANTIGUEDAD"] != DBNull.Value) entidad.antiguedad = Convert.ToDecimal(resultado["ANTIGUEDAD"]);
                            if (resultado["GARANTIAS_COBERTURAS"] != DBNull.Value) entidad.garantias = Convert.ToDecimal(resultado["GARANTIAS_COBERTURAS"]);
                            if (resultado["SERVICIO"] != DBNull.Value) entidad.servicio = Convert.ToInt32(resultado["SERVICIO"]);
                            if (resultado["PROVISION"] != DBNull.Value) entidad.provision = Convert.ToDecimal(resultado["PROVISION"]);
                            if (resultado["CENTRALES_RIESGO"] != DBNull.Value) entidad.centrales_riesgo = Convert.ToString(resultado["CENTRALES_RIESGO"]);
                            if (resultado["SEGMENTO"] != DBNull.Value) entidad.segmento = Convert.ToString(resultado["SEGMENTO"]);
                            if (resultado["RESULTADO"] != DBNull.Value) entidad.score = Convert.ToInt64(resultado["RESULTADO"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToString(resultado["CALIFICACION"]);
                            if (resultado["PROBABILIDAD_INCUMPLIMIENTO"] != DBNull.Value) entidad.probabilidad_incumplimiento = Convert.ToDouble(resultado["PROBABILIDAD_INCUMPLIMIENTO"]);
                            if (resultado["COD_CATEGORIA_PRO"] != DBNull.Value) entidad.cod_categoria_pro = Convert.ToString(resultado["COD_CATEGORIA_PRO"]);
                            if (resultado["PROVISION_RIESGO"] != DBNull.Value) entidad.provision_riesgo = Convert.ToDecimal(resultado["PROVISION_RIESGO"]);
                            // Calcular capacidad de pago
                            if (entidad.total_ingresos != 0)
                                entidad.capacidad_pago = Math.Round(entidad.valor_cuota / entidad.total_ingresos, 2);
                            // Calcular solvencia
                            if (entidad.total_activos != 0)
                                entidad.solvencia = Math.Round(entidad.total_pasivos / entidad.total_activos, 2);
                            lstScoringCreditos.Add(entidad);
                        }

                        return lstScoringCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringCreditosData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Mètodo que realiza la segmentaciòn de todos los crèditos activos a una fecha de corte con base en una hoja de scoring
        /// </summary>
        /// <param name="pFechaCorte"></param>
        /// <param name="pError"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public bool CierreSegmentacionCredito(DateTime pFechaCorte, string pEstado, ref string pError, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_FECHA_CORTE = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_CORTE.ParameterName = "P_FECHA_CORTE";
                        P_FECHA_CORTE.DbType = DbType.Date;
                        P_FECHA_CORTE.Value = pFechaCorte;
                        P_FECHA_CORTE.Direction = ParameterDirection.Input;

                        DbParameter p_ESTADO = cmdTransaccionFactory.CreateParameter();
                        p_ESTADO.ParameterName = "P_ESTADO";
                        p_ESTADO.DbType = DbType.AnsiStringFixedLength;
                        p_ESTADO.Value = pEstado;
                        p_ESTADO.Size = 1;
                        p_ESTADO.Direction = ParameterDirection.Input;

                        DbParameter p_USUARIO = cmdTransaccionFactory.CreateParameter();
                        p_USUARIO.ParameterName = "p_USUARIO";
                        p_USUARIO.DbType = DbType.Int32;
                        p_USUARIO.Value = pUsuario.codusuario;
                        p_USUARIO.Direction = ParameterDirection.Input;

                        DbParameter p_ERROR = cmdTransaccionFactory.CreateParameter();
                        p_ERROR.ParameterName = "P_ERROR";
                        p_ERROR.DbType = DbType.AnsiStringFixedLength;
                        p_ERROR.Size = 1000;
                        p_ERROR.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(P_FECHA_CORTE);
                        cmdTransaccionFactory.Parameters.Add(p_ESTADO);
                        cmdTransaccionFactory.Parameters.Add(p_USUARIO);
                        cmdTransaccionFactory.Parameters.Add(p_ERROR);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_SEGMENTACRE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (p_ERROR != null)
                            if (p_ERROR.Value != null)
                                pError = p_ERROR.Value.ToString();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return false;
                    }
                }
            }
        }

        
        public Xpinn.Comun.Entities.Cierea FechaUltimoCierre(Xpinn.Comun.Entities.Cierea pCierea, String filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from cierea " + ObtenerFiltro(pCierea) + " Order by fecha desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            Xpinn.Comun.Entities.Cierea entidad = new Xpinn.Comun.Entities.Cierea();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["CAMPO1"] != DBNull.Value) entidad.campo1 = Convert.ToString(resultado["CAMPO1"]);
                            if (resultado["CAMPO2"] != DBNull.Value) entidad.campo2 = Convert.ToString(resultado["CAMPO2"]);
                            if (resultado["FECREA"] != DBNull.Value) entidad.fecrea = Convert.ToDateTime(resultado["FECREA"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);

                            dbConnectionFactory.CerrarConexion(connection);

                            return entidad;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "FechaUltimoCierre", ex);
                        return null;
                    }
                }
            }
        }


        public void PeriodicidadCierre(ref int dias_cierre, ref int tipo_calendario, Usuario pUsuario)
        {
            dias_cierre = 30;
            tipo_calendario = 1;
            int periodicidad = 0;
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string valor = "";
                        string sql = "Select valor From general Where codigo = 1910 ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToString(resultado["VALOR"].ToString().Trim());
                        }
                        try
                        {
                            periodicidad = Convert.ToInt16(valor);
                        }
                        catch
                        {
                            return;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "PeriodicidadCierre", ex);
                        return;
                    }
                }
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select numero_dias, tipo_calendario From periodicidad Where cod_periodicidad = " + periodicidad;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_DIAS"] != DBNull.Value) dias_cierre = Convert.ToInt16(resultado["NUMERO_DIAS"].ToString());
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) tipo_calendario = Convert.ToInt16(resultado["TIPO_CALENDARIO"].ToString());
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "PeriodicidadCierre", ex);
                        return;
                    }
                }
            }
        }

        public List<RiesgoCredito> ListarCalificaciones(string pFiltro, Usuario usuario = null)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RiesgoCredito> lista = new List<RiesgoCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT DISTINCT calificacion, observacion FROM scscoringboard_cal WHERE idscore > 0 " + pFiltro + " ORDER BY calificacion ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RiesgoCredito entidad = new RiesgoCredito();

                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToString(resultado["CALIFICACION"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.segmento = Convert.ToString(resultado["OBSERVACION"]);

                            lista.Add(entidad);
                        }

                        return lista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringCreditosData", "ListarCalificaciones", ex);
                        return null;
                    }

                }
            }
        }

        public Xpinn.Comun.Entities.Cierea FechaPrimerCierre(Xpinn.Comun.Entities.Cierea pCierea, String filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from cierea " + ObtenerFiltro(pCierea) + " Order by fecha";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            Xpinn.Comun.Entities.Cierea entidad = new Xpinn.Comun.Entities.Cierea();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["CAMPO1"] != DBNull.Value) entidad.campo1 = Convert.ToString(resultado["CAMPO1"]);
                            if (resultado["CAMPO2"] != DBNull.Value) entidad.campo2 = Convert.ToString(resultado["CAMPO2"]);
                            if (resultado["FECREA"] != DBNull.Value) entidad.fecrea = Convert.ToDateTime(resultado["FECREA"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);

                            dbConnectionFactory.CerrarConexion(connection);

                            return entidad;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "FechaUltimoCierre", ex);
                        return null;
                    }
                }
            }
        }

        public List<RiesgoCredito> ListarRiesgoCreditoProvision(DateTime pFechaCorte, String filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RiesgoCredito> lstScoringCreditos = new List<RiesgoCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string fecha_corte = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            fecha_corte = " To_Date('" + pFechaCorte.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "', 'NLS_DATE_LANGUAGE = American') ";
                        else
                            fecha_corte = " '" + pFechaCorte.ToString(conf.ObtenerFormatoFecha()) + "' ";
                        string sql = @"Select hc.fecha_historico, o.nombre Nombre_Oficina, hc.numero_radicacion, p.identificacion, NombrePersona(p.tipo_persona, p.primer_nombre, p.segundo_Nombre, p.Primer_Apellido, p.Segundo_apellido, p.razon_social) Nombre,
                                        l.nombre Linea_Credito, hc.monto, hc.saldo_capital, hc.cod_categoria_cli, hc.dias_mora, hs.segmento, hs.resultado, hs.probabilidad_incumplimiento, hs.cod_categoria AS cod_categoria_pro, hs.calificacion, po.valor_provision, po.porc_provision,
                                        (Select d.porc_provision From dias_categorias d Where d.cod_clasifica = hc.cod_clasifica And d.cod_categoria = hs.cod_categoria) AS porc_provision_pro
                                        From Historico_cre hc  
                                        Left join historico_segmenta_cre hs On hs.fecha_historico = hc.fecha_historico And hs.numero_radicacion = hc.numero_radicacion
                                        Left join provision po On po.fecha_corte = hc.fecha_historico And po.numero_radicacion = hc.numero_radicacion And po.cod_atr = 1
                                        Left join Oficina o on o.cod_oficina = hc.cod_oficina
                                        Left join Persona p on p.Cod_persona = hc.cod_cliente
                                        Left join LineasCredito l on l.Cod_Linea_credito = hc.cod_linea_credito
                                        Where hc.Fecha_historico = " + fecha_corte + " " + (filtro.Trim() == "" ? "" : filtro) + " And hc.cod_categoria_cli < hs.cod_categoria ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RiesgoCredito entidad = new RiesgoCredito();
                            if (resultado["FECHA_HISTORICO"] != DBNull.Value) entidad.fecha_corte = Convert.ToDateTime(resultado["FECHA_HISTORICO"]);
                            if (resultado["NOMBRE_OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["NOMBRE_OFICINA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["LINEA_CREDITO"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA_CREDITO"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["MONTO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["COD_CATEGORIA_CLI"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA_CLI"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToInt32(resultado["DIAS_MORA"]);
                            if (resultado["SEGMENTO"] != DBNull.Value) entidad.segmento = Convert.ToString(resultado["SEGMENTO"]);
                            if (resultado["RESULTADO"] != DBNull.Value) entidad.score = Convert.ToInt64(resultado["RESULTADO"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToString(resultado["CALIFICACION"]);
                            if (resultado["PROBABILIDAD_INCUMPLIMIENTO"] != DBNull.Value) entidad.probabilidad_incumplimiento = Convert.ToDouble(resultado["PROBABILIDAD_INCUMPLIMIENTO"]);
                            if (resultado["COD_CATEGORIA_PRO"] != DBNull.Value) entidad.cod_categoria_pro = Convert.ToString(resultado["COD_CATEGORIA_PRO"]);
                            if (resultado["PORC_PROVISION"] != DBNull.Value) entidad.porc_provision = Convert.ToDouble(resultado["PORC_PROVISION"]);
                            if (resultado["VALOR_PROVISION"] != DBNull.Value) entidad.provision = Convert.ToDecimal(resultado["VALOR_PROVISION"]);
                            if (resultado["PORC_PROVISION_PRO"] != DBNull.Value) entidad.porc_provision_riesgo = Convert.ToDouble(resultado["PORC_PROVISION_PRO"]);
                            entidad.provision_riesgo = Math.Round(Convert.ToDecimal(entidad.porc_provision_riesgo)/100 * entidad.saldo_capital);
                            entidad.aumento_provision = entidad.provision_riesgo - entidad.provision;
                            lstScoringCreditos.Add(entidad);
                        }

                        return lstScoringCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ScoringCreditosData", "ListarRiesgoCreditoProvision", ex);
                        return null;
                    }
                }
            }
        }




    }
}