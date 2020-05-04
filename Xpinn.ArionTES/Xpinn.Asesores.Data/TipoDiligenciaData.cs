using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla tipodiligencia
    /// </summary>
    public class TipoDiligenciaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla tipodiligencia
        /// </summary>
        public TipoDiligenciaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla tipodiligencia de la base de datos
        /// </summary>
        /// <param name="pTipoDiligencia">Entidad TipoDiligencia</param>
        /// <returns>Entidad TipoDiligencia creada</returns>
        public TipoDiligencia CrearTipoDiligencia(TipoDiligencia pTipoDiligencia, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pTIPO_DILIGENCIA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DILIGENCIA.ParameterName = "p_tipo_diligencia";
                        pTIPO_DILIGENCIA.Value = pTipoDiligencia.tipo_diligencia;
                        pTIPO_DILIGENCIA.Direction = ParameterDirection.InputOutput;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_descripcion";
                        pDESCRIPCION.Value = pTipoDiligencia.descripcion;


                        cmdTransaccionFactory.Parameters.Add(pTIPO_DILIGENCIA);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Asesores_tipodiligencia_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoDiligencia, "tipodiligencia", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pTipoDiligencia.tipo_diligencia = Convert.ToInt64(pTIPO_DILIGENCIA.Value);
                        return pTipoDiligencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoDiligenciaData", "CrearTipoDiligencia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla tipodiligencia de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad TipoDiligencia modificada</returns>
        public TipoDiligencia ModificarTipoDiligencia(TipoDiligencia pTipoDiligencia, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pTIPO_DILIGENCIA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DILIGENCIA.ParameterName = "p_TIPO_DILIGENCIA";
                        pTIPO_DILIGENCIA.Value = pTipoDiligencia.tipo_diligencia;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pTipoDiligencia.descripcion;

                        cmdTransaccionFactory.Parameters.Add(pTIPO_DILIGENCIA);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Asesores_tipodiligencia_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoDiligencia, "tipodiligencia", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pTipoDiligencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoDiligenciaData", "ModificarTipoDiligencia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla tipodiligencia de la base de datos
        /// </summary>
        /// <param name="pId">identificador de tipodiligencia</param>
        public void EliminarTipoDiligencia(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoDiligencia pTipoDiligencia = new TipoDiligencia();

                        if (pUsuario.programaGeneraLog)
                            pTipoDiligencia = ConsultarTipoDiligencia(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pTIPO_DILIGENCIA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DILIGENCIA.ParameterName = "p_tipo_diligencia";
                        pTIPO_DILIGENCIA.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pTIPO_DILIGENCIA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Asesores_tipodiligencia_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoDiligencia, "tipodiligencia", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoDiligenciaData", "EliminarTipoDiligencia", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla tipodiligencia de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla tipodiligencia</param>
        /// <returns>Entidad TipoDiligencia consultado</returns>
        public TipoDiligencia ConsultarTipoDiligencia(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            TipoDiligencia entidad = new TipoDiligencia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPODILIGENCIA WHERE tipo_diligencia = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TIPO_DILIGENCIA"] != DBNull.Value) entidad.tipo_diligencia = Convert.ToInt64(resultado["TIPO_DILIGENCIA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoDiligenciaData", "ConsultarTipoDiligencia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla tipodiligencia dados unos filtros
        /// </summary>
        /// <param name="ptipodiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoDiligencia obtenidos</returns>
        public List<TipoDiligencia> ListarTipoDiligencia(TipoDiligencia pTipoDiligencia, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoDiligencia> lstTipoDiligencia = new List<TipoDiligencia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT a.tipo_diligencia, a.descripcion FROM tipodiligencia a WHERE a.tipo_diligencia NOT IN (Select g.valor from general g Where g.codigo = 6094)AND a.tipo_diligencia NOT IN (Select g.valor from general g Where g.codigo = 6095) AND a.tipo_diligencia NOT IN (Select g.valor from general g Where g.codigo = 6096)" + ObtenerFiltro(pTipoDiligencia);
                          connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoDiligencia entidad = new TipoDiligencia();

                            if (resultado["TIPO_DILIGENCIA"] != DBNull.Value) entidad.tipo_diligencia = Convert.ToInt64(resultado["TIPO_DILIGENCIA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstTipoDiligencia.Add(entidad);
                        }

                        return lstTipoDiligencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoDiligenciaData", "ListarTipoDiligencia", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla tipodiligencia dados unos filtros
        /// </summary>
        /// <param name="ptipodiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoDiligencia obtenidos</returns>
        public List<TipoDiligencia> ListarTipoDiligenciaAgregar(TipoDiligencia pTipoDiligencia, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoDiligencia> lstTipoDiligencia = new List<TipoDiligencia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM tipodiligencia" + ObtenerFiltro(pTipoDiligencia);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoDiligencia entidad = new TipoDiligencia();

                            if (resultado["TIPO_DILIGENCIA"] != DBNull.Value) entidad.tipo_diligencia = Convert.ToInt64(resultado["TIPO_DILIGENCIA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstTipoDiligencia.Add(entidad);
                        }

                        return lstTipoDiligencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoDiligenciaData", "ListarTipoDiligencia", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla tipodiligencia dados unos filtros
        /// </summary>
        /// <param name="ptipodiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoDiligencia obtenidos</returns>
        public List<TipoContacto> ListarTipoContactoAgregar(TipoContacto pTipoContacto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoContacto> lstTipoContacto = new List<TipoContacto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                          string sql = "SELECT a.tipocontacto, a.descripcion FROM tipocontacto a WHERE a.tipocontacto NOT IN (Select g.valor from general g Where g.codigo = 6097) " + ObtenerFiltro(pTipoContacto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoContacto entidad = new TipoContacto();

                            if (resultado["TIPOCONTACTO"] != DBNull.Value) entidad.tipocontacto = Convert.ToInt64(resultado["TIPOCONTACTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstTipoContacto.Add(entidad);
                        }

                        return lstTipoContacto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoContactoData", "ListarTipoContacto", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla tipodiligencia dados unos filtros
        /// </summary>
        /// <param name="ptipodiligencia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoDiligencia obtenidos</returns>
        public List<TipoContacto> ListarTipoContacto(TipoContacto pTipoContacto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoContacto> lstTipoContacto = new List<TipoContacto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPOCONTACTO " + ObtenerFiltro(pTipoContacto);
                     
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoContacto entidad = new TipoContacto();

                            if (resultado["TIPOCONTACTO"] != DBNull.Value) entidad.tipocontacto = Convert.ToInt64(resultado["TIPOCONTACTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstTipoContacto.Add(entidad);
                        }

                        return lstTipoContacto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoContactoData", "ListarTipoContacto", ex);
                        return null;
                    }
                }
            }
        }

    }
}