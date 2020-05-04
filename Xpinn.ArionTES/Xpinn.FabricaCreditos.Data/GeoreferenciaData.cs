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
    /// Objeto de acceso a datos para la tabla Georeferencia
    /// </summary>
    public class GeoreferenciaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Georeferencia
        /// </summary>
        public GeoreferenciaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Georeferencia de la base de datos
        /// </summary>
        /// <param name="pGeoreferencia">Entidad Georeferencia</param>
        /// <returns>Entidad Georeferencia creada</returns>
        public Georeferencia CrearGeoreferencia(Georeferencia pGeoreferencia, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODGEOREFERENCIA = cmdTransaccionFactory.CreateParameter();
                        pCODGEOREFERENCIA.ParameterName = "p_CODGEOREFERENCIA";
                        pCODGEOREFERENCIA.Value = 0;
                        pCODGEOREFERENCIA.Direction = ParameterDirection.InputOutput;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pGeoreferencia.cod_persona;

                        DbParameter pLATITUD = cmdTransaccionFactory.CreateParameter();
                        pLATITUD.ParameterName = "p_LATITUD";
                        pLATITUD.Value = pGeoreferencia.latitud;

                        DbParameter pLONGITUD = cmdTransaccionFactory.CreateParameter();
                        pLONGITUD.ParameterName = "p_LONGITUD";
                        pLONGITUD.Value = pGeoreferencia.longitud;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "p_OBSERVACIONES";
                        if (pGeoreferencia.observaciones != null) pOBSERVACIONES.Value = pGeoreferencia.observaciones; else pOBSERVACIONES.Value = DBNull.Value;

                        DbParameter pFECHACREACION = cmdTransaccionFactory.CreateParameter();
                        pFECHACREACION.ParameterName = "p_FECHACREACION";
                        pFECHACREACION.Value = pGeoreferencia.fechacreacion;

                        DbParameter pUSUARIOCREACION = cmdTransaccionFactory.CreateParameter();
                        pUSUARIOCREACION.ParameterName = "p_USUARIOCREACION";
                        pUSUARIOCREACION.Value = pGeoreferencia.usuariocreacion;

                        DbParameter pFECULTMOD = cmdTransaccionFactory.CreateParameter();
                        pFECULTMOD.ParameterName = "p_FECULTMOD";
                        if (pGeoreferencia.fecultmod != DateTime.MinValue) pFECULTMOD.Value = pGeoreferencia.fecultmod; else pFECULTMOD.Value = DBNull.Value;

                        DbParameter pUSUULTMOD = cmdTransaccionFactory.CreateParameter();
                        pUSUULTMOD.ParameterName = "p_USUULTMOD";
                        if (pGeoreferencia.usuultmod != null) pUSUULTMOD.Value = pGeoreferencia.usuultmod; else pUSUULTMOD.Value = DBNull.Value;

                        DbParameter p_NOMBRE_REFERENCIAS = cmdTransaccionFactory.CreateParameter();
                        p_NOMBRE_REFERENCIAS.ParameterName = "p_NOMBRE_REFERENCIAS";
                        if (pGeoreferencia.NOMBRE_REFERENCIAS != null) p_NOMBRE_REFERENCIAS.Value = pGeoreferencia.NOMBRE_REFERENCIAS; else p_NOMBRE_REFERENCIAS.Value = DBNull.Value;

                        DbParameter p_TIEMPO_NEGOCIO = cmdTransaccionFactory.CreateParameter();
                        p_TIEMPO_NEGOCIO.ParameterName = "p_TIEMPO_NEGOCIO";
                        if (pGeoreferencia.TIEMPO_NEGOCIO != null) p_TIEMPO_NEGOCIO.Value = pGeoreferencia.TIEMPO_NEGOCIO; else p_TIEMPO_NEGOCIO.Value = DBNull.Value;

                        DbParameter p_PROPIETARIO_SI_NO = cmdTransaccionFactory.CreateParameter();
                        p_PROPIETARIO_SI_NO.ParameterName = "p_PROPIETARIO_SI_NO";
                        if (pGeoreferencia.PROPIETARIO_SI_NO != null) p_PROPIETARIO_SI_NO.Value = pGeoreferencia.PROPIETARIO_SI_NO; else p_PROPIETARIO_SI_NO.Value = DBNull.Value;

                        DbParameter p_CONCEPTO = cmdTransaccionFactory.CreateParameter();
                        p_CONCEPTO.ParameterName = "p_CONCEPTO";
                        if (pGeoreferencia.CONCEPTO != null) p_CONCEPTO.Value = pGeoreferencia.CONCEPTO; else p_CONCEPTO.Value = DBNull.Value;

                        DbParameter p_NUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        p_NUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        if (pGeoreferencia.numero_radicacion != 0) p_NUMERO_RADICACION.Value = pGeoreferencia.numero_radicacion; else p_NUMERO_RADICACION.Value = DBNull.Value;

                        cmdTransaccionFactory.Parameters.Add(pCODGEOREFERENCIA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pLATITUD);
                        cmdTransaccionFactory.Parameters.Add(pLONGITUD);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        cmdTransaccionFactory.Parameters.Add(pFECHACREACION);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIOCREACION);
                        cmdTransaccionFactory.Parameters.Add(pFECULTMOD);
                        cmdTransaccionFactory.Parameters.Add(pUSUULTMOD);
                        cmdTransaccionFactory.Parameters.Add(p_NOMBRE_REFERENCIAS);
                        cmdTransaccionFactory.Parameters.Add(p_TIEMPO_NEGOCIO);
                        cmdTransaccionFactory.Parameters.Add(p_PROPIETARIO_SI_NO);
                        cmdTransaccionFactory.Parameters.Add(p_CONCEPTO);
                        cmdTransaccionFactory.Parameters.Add(p_NUMERO_RADICACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_GEORE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pGeoreferencia, "Georeferencia",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pGeoreferencia.codgeoreferencia = Convert.ToInt64(pCODGEOREFERENCIA.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pGeoreferencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GeoreferenciaData", "CrearGeoreferencia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Georeferencia de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Georeferencia modificada</returns>
        public Georeferencia ModificarGeoreferencia(Georeferencia pGeoreferencia, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODGEOREFERENCIA = cmdTransaccionFactory.CreateParameter();
                        pCODGEOREFERENCIA.ParameterName = "p_CODGEOREFERENCIA";
                        pCODGEOREFERENCIA.Value = pGeoreferencia.codgeoreferencia;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pGeoreferencia.cod_persona;

                        DbParameter pLATITUD = cmdTransaccionFactory.CreateParameter();
                        pLATITUD.ParameterName = "p_LATITUD";
                        pLATITUD.Value = pGeoreferencia.latitud;

                        DbParameter pLONGITUD = cmdTransaccionFactory.CreateParameter();
                        pLONGITUD.ParameterName = "p_LONGITUD";
                        pLONGITUD.Value = pGeoreferencia.longitud;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "p_OBSERVACIONES";
                        if (pGeoreferencia.observaciones != null) pOBSERVACIONES.Value = pGeoreferencia.observaciones; else pOBSERVACIONES.Value = DBNull.Value;

                        DbParameter pFECULTMOD = cmdTransaccionFactory.CreateParameter();
                        pFECULTMOD.ParameterName = "P_Fecultmod";
                        pFECULTMOD.Value = pGeoreferencia.fecultmod;

                        DbParameter pUSUULTMOD = cmdTransaccionFactory.CreateParameter();
                        pUSUULTMOD.ParameterName = "p_USUULTMOD";
                        pUSUULTMOD.Value = pGeoreferencia.usuultmod;

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        if (pGeoreferencia.numero_radicacion != 0) pNUMERO_RADICACION.Value = pGeoreferencia.numero_radicacion; else pNUMERO_RADICACION.Value = DBNull.Value;

                        cmdTransaccionFactory.Parameters.Add(pCODGEOREFERENCIA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pLATITUD);
                        cmdTransaccionFactory.Parameters.Add(pLONGITUD);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);                       
                        cmdTransaccionFactory.Parameters.Add(pFECULTMOD);
                        cmdTransaccionFactory.Parameters.Add(pUSUULTMOD);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_GEORE_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pGeoreferencia, "Georeferencia",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pGeoreferencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GeoreferenciaData", "ModificarGeoreferencia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Georeferencia de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Georeferencia</param>
        public void EliminarGeoreferencia(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Georeferencia pGeoreferencia = new Georeferencia();

                        DbParameter pCODGEOREFERENCIA = cmdTransaccionFactory.CreateParameter();
                        pCODGEOREFERENCIA.ParameterName = "p_CODGEOREFERENCIA";
                        pCODGEOREFERENCIA.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODGEOREFERENCIA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_GEORE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pGeoreferencia, "Georeferencia", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GeoreferenciaData", "EliminarGeoreferencia", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Georeferencia de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Georeferencia</param>
        /// <returns>Entidad Georeferencia consultado</returns>
        public Georeferencia ConsultarGeoreferencia(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Georeferencia entidad = new Georeferencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  GEOREFERENCIA WHERE CODGEOREFERENCIA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODGEOREFERENCIA"] != DBNull.Value) entidad.codgeoreferencia = Convert.ToInt64(resultado["CODGEOREFERENCIA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["LATITUD"] != DBNull.Value) entidad.latitud = Convert.ToString(resultado["LATITUD"]);
                            if (resultado["LONGITUD"] != DBNull.Value) entidad.longitud = Convert.ToString(resultado["LONGITUD"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
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
                        BOExcepcion.Throw("GeoreferenciaData", "ConsultarGeoreferencia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Georeferencia dados unos filtros
        /// </summary>
        /// <param name="pGeoreferencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Georeferencia obtenidos</returns>
        public List<Georeferencia> ListarGeoreferencia(Georeferencia pGeoreferencia, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Georeferencia> lstGeoreferencia = new List<Georeferencia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  GEOREFERENCIA ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Georeferencia entidad = new Georeferencia();

                            if (resultado["CODGEOREFERENCIA"] != DBNull.Value) entidad.codgeoreferencia = Convert.ToInt64(resultado["CODGEOREFERENCIA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["LATITUD"] != DBNull.Value) entidad.latitud = Convert.ToString(resultado["LATITUD"]);
                            if (resultado["LONGITUD"] != DBNull.Value) entidad.longitud = Convert.ToString(resultado["LONGITUD"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);

                            lstGeoreferencia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGeoreferencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GeoreferenciaData", "ListarGeoreferencia", ex);
                        return null;
                    }
                }
            }
        }
        public List<Georeferencia> ListarGeoreferenciacion(Georeferencia pGeoreferencia, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
                      
            List<Georeferencia> lstReferencia = new List<Georeferencia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    int LineaError = 0;
                    try
                    {
                        string sql = "Select * from V_GEOREFERENCIACION " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Georeferencia entidad = new Georeferencia();
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = resultado["COD_NOMINA"].ToString();
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);

                            lstReferencia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReferencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GeoreferenciaData", "ListarGeoreferencia", ex);
                        return null;
                    }
                }
            }
        }

        public Georeferencia ConsultarGeoreferenciaXPERSONA(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Georeferencia entidad = new Georeferencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  GEOREFERENCIA WHERE COD_PERSONA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODGEOREFERENCIA"] != DBNull.Value) entidad.codgeoreferencia = Convert.ToInt64(resultado["CODGEOREFERENCIA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["LATITUD"] != DBNull.Value) entidad.latitud = Convert.ToString(resultado["LATITUD"]);
                            if (resultado["LONGITUD"] != DBNull.Value) entidad.longitud = Convert.ToString(resultado["LONGITUD"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                        }                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GeoreferenciaData", "ConsultarGeoreferenciaXPERSONA", ex);
                        return null;
                    }
                }
            }
        }

    }
}