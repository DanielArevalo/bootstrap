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
    public class AprobacionScoringNegadosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PERSONA
        /// </summary>
        public AprobacionScoringNegadosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<AprobacionScoringNegados> ListarAprobScoringNegados(AprobacionScoringNegados pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AprobacionScoringNegados> lstAprobacionScoringNegados = new List<AprobacionScoringNegados>();

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
                            AprobacionScoringNegados entidad = new AprobacionScoringNegados();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumeroSolicitud = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["MONTO_SOLICITADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.Plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PUNSCORING"] != DBNull.Value) entidad.puntscoring = Convert.ToInt64(resultado["PUNSCORING"]);
                            lstAprobacionScoringNegados.Add(entidad);
                        }

                        return lstAprobacionScoringNegados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionScoringNegadosData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }





        /// <summary>
        /// Obtiene las listas desplegables de la tabla Persona
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Datos Solicitud obtenidas</returns>
        public List<AprobacionScoringNegados> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AprobacionScoringNegados> lstDatosSolicitud = new List<AprobacionScoringNegados>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        switch (ListaSolicitada)
                        {
                            case "MotivosAprobacion":
                                sql = "select idmotivo as ListaId, descripcion as ListaDescripcion from motivos_aprobacion_scoring ";
                                break;                           
                        }

                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AprobacionScoringNegados entidad = new AprobacionScoringNegados();
                            if (ListaSolicitada == "TipoCredito" || ListaSolicitada == "Periodicidad" || ListaSolicitada == "Medio" || ListaSolicitada == "Lugares" || ListaSolicitada == "ESTADO_ACTIVO")  //Diferencia entre los Ids de tabla, que pueden ser integer o varchar
                            { if (resultado["ListaId"] != DBNull.Value) entidad.ListaIdStr = Convert.ToString(resultado["ListaId"]); }
                            else
                            { if (resultado["ListaId"] != DBNull.Value) entidad.ListaId = Convert.ToInt64(resultado["ListaId"]); }
                            if (resultado["ListaDescripcion"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["ListaDescripcion"]);
                            lstDatosSolicitud.Add(entidad);
                        }
                        return lstDatosSolicitud;


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosClienteData", "ListasDesplegables", ex);
                        return null;
                    }
                }
            }
        }





        /// <summary>
        /// Modifica un registro en la tabla CREDITO de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ScScoringBoard modificada</returns>
        public AprobacionScoringNegados ModificarCredito(AprobacionScoringNegados pAprobacionScoringNegados, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idControl = cmdTransaccionFactory.CreateParameter();
                        p_idControl.ParameterName = "p_idControl";
                        p_idControl.Value = pAprobacionScoringNegados.idControl;
                        p_idControl.Direction = ParameterDirection.InputOutput;

                        DbParameter p_NumeroSolicitud = cmdTransaccionFactory.CreateParameter();
                        p_NumeroSolicitud.ParameterName = "p_NumeroSolicitud";
                        p_NumeroSolicitud.Value = pAprobacionScoringNegados.NumeroSolicitud;

                        DbParameter p_codTipoProceso = cmdTransaccionFactory.CreateParameter();
                        p_codTipoProceso.ParameterName = "p_codTipoProceso";
                        p_codTipoProceso.Value = pAprobacionScoringNegados.codTipoProceso;

                        DbParameter p_fechaProceso = cmdTransaccionFactory.CreateParameter();
                        p_fechaProceso.ParameterName = "p_fechaProceso";
                        p_fechaProceso.Value = pAprobacionScoringNegados.fechaProceso;

                        DbParameter p_codMotivo = cmdTransaccionFactory.CreateParameter();
                        p_codMotivo.ParameterName = "p_codMotivo";
                        p_codMotivo.Value = pAprobacionScoringNegados.codMotivo;

                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";
                        p_observaciones.Value = pAprobacionScoringNegados.observaciones;

                        cmdTransaccionFactory.Parameters.Add(p_idControl);
                        cmdTransaccionFactory.Parameters.Add(p_NumeroSolicitud);
                        cmdTransaccionFactory.Parameters.Add(p_codTipoProceso);
                        cmdTransaccionFactory.Parameters.Add(p_fechaProceso);
                        cmdTransaccionFactory.Parameters.Add(p_codMotivo);
                        cmdTransaccionFactory.Parameters.Add(p_observaciones);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_APROBAC_MODI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        connection.Close();
                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAprobacionScoringNegados, "ModificarCredito", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pAprobacionScoringNegados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionScoringNegados", "ModificarCredito", ex);
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
        public AprobacionScoringNegados CrearMotivo(AprobacionScoringNegados pAprobacionScoringNegados, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_ListaId = cmdTransaccionFactory.CreateParameter();
                        p_ListaId.ParameterName = "p_ListaId";
                        p_ListaId.Value = pAprobacionScoringNegados.ListaId;
                        p_ListaId.Direction = ParameterDirection.InputOutput;

                        DbParameter p_ListaDescripcion = cmdTransaccionFactory.CreateParameter();
                        p_ListaDescripcion.ParameterName = "p_ListaDescripcion";
                        p_ListaDescripcion.Value = pAprobacionScoringNegados.ListaDescripcion;

                        cmdTransaccionFactory.Parameters.Add(p_ListaId);
                        cmdTransaccionFactory.Parameters.Add(p_ListaDescripcion);                     

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_MOTIVOS_CREA";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAprobacionScoringNegados, "CrearMotivo", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pAprobacionScoringNegados.ListaId = Convert.ToInt64(p_ListaId.Value);

                        return pAprobacionScoringNegados;
                    }
                    catch (Exception ex)
                    {
                        string a = ex.ToString();
                        BOExcepcion.Throw("AprobacionScoringNegadosData", "AprobacionScoringNegadosVar", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Elimina un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PERSONA</param>
        public void EliminarMotivo(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        AprobacionScoringNegados pAprobacionScoringNegados = new AprobacionScoringNegados();

                        DbParameter p_ListaId = cmdTransaccionFactory.CreateParameter();
                        p_ListaId.ParameterName = "p_ListaId";
                        p_ListaId.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(p_ListaId);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_MOTIVOS_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionScoringNegados", "EliminarMotivo", ex);
                    }
                }
            }
        }




        /// <summary>
        /// Modifica un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ScScoringBoardVar modificada</returns>
        public AprobacionScoringNegados ModificarMotivo(AprobacionScoringNegados pAprobacionScoringNegados, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                       
                        DbParameter p_ListaId = cmdTransaccionFactory.CreateParameter();
                        p_ListaId.ParameterName = "p_ListaId";
                        p_ListaId.Value = pAprobacionScoringNegados.ListaId;
                        p_ListaId.Direction = ParameterDirection.InputOutput;

                        DbParameter p_ListaDescripcion = cmdTransaccionFactory.CreateParameter();
                        p_ListaDescripcion.ParameterName = "p_ListaDescripcion";
                        p_ListaDescripcion.Value = pAprobacionScoringNegados.ListaDescripcion;
                      
                        cmdTransaccionFactory.Parameters.Add(p_ListaId);
                        cmdTransaccionFactory.Parameters.Add(p_ListaDescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SCORING_MOTIVOS_MODI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        connection.Close();


                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAprobacionScoringNegados, "ModificarMotivo", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pAprobacionScoringNegados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("pAprobacionScoringNegados", "ModificarMotivo", ex);
                        return null;
                    }
                }
            }
        }







    }
}