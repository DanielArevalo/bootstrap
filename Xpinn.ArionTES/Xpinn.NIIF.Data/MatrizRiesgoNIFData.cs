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
    /// Objeto de acceso a datos para la tabla MATRIZ_RIESGO
    /// </summary>
    public class MatrizRiesgoNIFData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PERSONA
        /// </summary>
        public MatrizRiesgoNIFData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pMatrizRiesgoNIF">Entidad MatrizRiesgoNIF</param>
        /// <returns>Entidad MatrizRiesgoNIF creada</returns>
        public MatrizRiesgoNIF CrearMatrizRiesgoNIF(MatrizRiesgoNIF pMatrizRiesgoNIF, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidmatriz = cmdTransaccionFactory.CreateParameter();
                        pidmatriz.ParameterName = "p_idmatriz";
                        pidmatriz.Value = pMatrizRiesgoNIF.idmatriz;
                        pidmatriz.Direction = ParameterDirection.Output;
                        pidmatriz.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidmatriz);

                        DbParameter pcod_clasifica = cmdTransaccionFactory.CreateParameter();
                        pcod_clasifica.ParameterName = "p_cod_clasifica";
                        pcod_clasifica.Value = pMatrizRiesgoNIF.cod_clasifica;
                        pcod_clasifica.Direction = ParameterDirection.Input;
                        pcod_clasifica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_clasifica);

                        DbParameter ptipo_persona = cmdTransaccionFactory.CreateParameter();
                        ptipo_persona.ParameterName = "p_tipo_persona";
                        ptipo_persona.Value = pMatrizRiesgoNIF.tipo_persona;
                        ptipo_persona.Direction = ParameterDirection.Input;
                        ptipo_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_persona);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = pMatrizRiesgoNIF.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        pusuariocreacion.Value = pMatrizRiesgoNIF.usuariocreacion;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        pfecultmod.Value = pMatrizRiesgoNIF.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "p_usuultmod";
                        pusuultmod.Value = pMatrizRiesgoNIF.usuultmod;
                        pusuultmod.Direction = ParameterDirection.Input;
                        pusuultmod.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_MATRIZRIES_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pMatrizRiesgoNIF.idmatriz = Convert.ToInt32(pidmatriz.Value);

                        return pMatrizRiesgoNIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizRiesgoNIFData", "CrearMatrizRiesgoNIF", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Modifica un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad MatrizRiesgoNIF modificada</returns>
        public MatrizRiesgoNIF ModificarMatrizRiesgoNIF(MatrizRiesgoNIF pMatrizRiesgoNIF, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidmatriz = cmdTransaccionFactory.CreateParameter();
                        pidmatriz.ParameterName = "p_idmatriz";
                        pidmatriz.Value = pMatrizRiesgoNIF.idmatriz;
                        pidmatriz.Direction = ParameterDirection.Input;
                        pidmatriz.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidmatriz);

                        DbParameter pcod_clasifica = cmdTransaccionFactory.CreateParameter();
                        pcod_clasifica.ParameterName = "p_cod_clasifica";
                        pcod_clasifica.Value = pMatrizRiesgoNIF.cod_clasifica;
                        pcod_clasifica.Direction = ParameterDirection.Input;
                        pcod_clasifica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_clasifica);

                        DbParameter ptipo_persona = cmdTransaccionFactory.CreateParameter();
                        ptipo_persona.ParameterName = "p_tipo_persona";
                        ptipo_persona.Value = pMatrizRiesgoNIF.tipo_persona;
                        ptipo_persona.Direction = ParameterDirection.Input;
                        ptipo_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_persona);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = pMatrizRiesgoNIF.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        pusuariocreacion.Value = pMatrizRiesgoNIF.usuariocreacion;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        pfecultmod.Value = pMatrizRiesgoNIF.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "p_usuultmod";
                        pusuultmod.Value = pMatrizRiesgoNIF.usuultmod;
                        pusuultmod.Direction = ParameterDirection.Input;
                        pusuultmod.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_MATRIZRIES_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        connection.Close();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pMatrizRiesgoNIF, "MATRIZ_RIESGO_NIF", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pMatrizRiesgoNIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizRiesgoNIFData", "ModificarMatrizRiesgoNIF", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PERSONA</param>
        public void EliminarMatrizRiesgoNIF(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        MatrizRiesgoNIF pMatrizRiesgoNIF = new MatrizRiesgoNIF();
                        pMatrizRiesgoNIF = ConsultarMatrizRiesgoNIF(pId, vUsuario);

                        DbParameter pidmatriz = cmdTransaccionFactory.CreateParameter();
                        pidmatriz.ParameterName = "p_idmatriz";
                        pidmatriz.Value = pMatrizRiesgoNIF.idmatriz;
                        pidmatriz.Direction = ParameterDirection.Input;
                        pidmatriz.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidmatriz);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_MATRIZRIES_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizRiesgoNIFData", "EliminarMatrizRiesgoNIF", ex);
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PERSONA</param>
        /// <returns>Entidad MatrizRiesgoNIF consultado</returns>
        public MatrizRiesgoNIF ConsultarMatrizRiesgoNIF(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            MatrizRiesgoNIF entidad = new MatrizRiesgoNIF();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM MATRIZ_RIESGO_NIF WHERE IDMATRIZ = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDMATRIZ"] != DBNull.Value) entidad.idmatriz = Convert.ToInt32(resultado["IDMATRIZ"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["COD_CLASIFICA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToInt32(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToInt32(resultado["USUULTMOD"]);
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
                        BOExcepcion.Throw("MatrizRiesgoNIFData", "ConsultarMatrizRiesgoNIF", ex);
                        return null;
                    }
                }
            }
        }

 
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PERSONA dados unos filtros
        /// </summary>
        /// <param name="pPERSONA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MatrizRiesgoNIF obtenidos</returns>
        public List<MatrizRiesgoNIF> ListarMatrizRiesgoNIF(MatrizRiesgoNIF pMatrizRiesgoNIF, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<MatrizRiesgoNIF> lstMatrizRiesgoNIF = new List<MatrizRiesgoNIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT sc.*, c.descripcion AS nom_clasifica, Case sc.tipo_persona When 'N' Then 'Natural' When 'J' Then 'Juridica' End As nom_tipo_persona 
                                        FROM MATRIZ_RIESGO_NIF sc LEFT JOIN clasificacion c ON sc.cod_clasifica = c.cod_clasifica
                                        WHERE sc.idmatriz != 0 " + pMatrizRiesgoNIF.filtro + "  ORDER BY SC.IDMATRIZ ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            MatrizRiesgoNIF entidad = new MatrizRiesgoNIF();
                            if (resultado["IDMATRIZ"] != DBNull.Value) entidad.idmatriz = Convert.ToInt32(resultado["IDMATRIZ"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["COD_CLASIFICA"]);
                            if (resultado["NOM_CLASIFICA"] != DBNull.Value) entidad.nom_clasifica = Convert.ToString(resultado["NOM_CLASIFICA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["NOM_TIPO_PERSONA"] != DBNull.Value) entidad.nom_tipo_persona = Convert.ToString(resultado["NOM_TIPO_PERSONA"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToInt32(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToInt32(resultado["USUULTMOD"]);
                            lstMatrizRiesgoNIF.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstMatrizRiesgoNIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MatrizRiesgoNIFData", "ListarMatrizRiesgoNIF", ex);
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
       
    }
}