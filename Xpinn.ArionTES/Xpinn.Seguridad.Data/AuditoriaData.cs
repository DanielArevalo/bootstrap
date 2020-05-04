using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla AUDITORIA
    /// </summary>
    public class AuditoriaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla AUDITORIA
        /// </summary>
        public AuditoriaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene un registro en la tabla AUDITORIA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla AUDITORIA</param>
        /// <returns>Entidad Auditoria consultado</returns>
        public Auditoria ConsultarAuditoria(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Auditoria entidad = new Auditoria();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  AUDITORIA WHERE cod_auditoria = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_AUDITORIA"] != DBNull.Value) entidad.cod_auditoria = Convert.ToInt64(resultado["COD_AUDITORIA"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["CODOPCION"] != DBNull.Value) entidad.codopcion = Convert.ToInt64(resultado["CODOPCION"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["IP"] != DBNull.Value) entidad.ip = Convert.ToString(resultado["IP"]);
                            if (resultado["NAVEGADOR"] != DBNull.Value) entidad.navegador = Convert.ToString(resultado["NAVEGADOR"]);
                            if (resultado["ACCION"] != DBNull.Value) entidad.accion = Convert.ToString(resultado["ACCION"]);
                            if (resultado["TABLA"] != DBNull.Value) entidad.tabla = Convert.ToString(resultado["TABLA"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("AuditoriaData", "ConsultarAuditoria", ex);
                        return null;
                    }
                }
            }
        }

        public void CrearTablaAuditoria(string tablas, string operacion, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter PTABLA = cmdTransaccionFactory.CreateParameter();
                        PTABLA.ParameterName = "PTABLA";
                        PTABLA.Value = tablas;
                        PTABLA.Direction = ParameterDirection.Input;
                        PTABLA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PTABLA);

                        DbParameter PTIPOOPE = cmdTransaccionFactory.CreateParameter();
                        PTIPOOPE.ParameterName = "PTIPOOPE";
                        PTIPOOPE.Value = operacion;
                        PTIPOOPE.Direction = ParameterDirection.Input;
                        PTIPOOPE.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PTIPOOPE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ADM_AUDITORIA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AuditoriaData", "CrearTablaAuditoria", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla AUDITORIA dados unos filtros
        /// </summary>
        /// <param name="pAUDITORIA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Auditoria obtenidos</returns>
        public List<Auditoria> ListarAuditoria(Auditoria pAuditoria, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Auditoria> lstAuditoria = new List<Auditoria>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  AUDITORIA " + ObtenerFiltro(pAuditoria);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Auditoria entidad = new Auditoria();

                            if (resultado["COD_AUDITORIA"] != DBNull.Value) entidad.cod_auditoria = Convert.ToInt64(resultado["COD_AUDITORIA"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["CODOPCION"] != DBNull.Value) entidad.codopcion = Convert.ToInt64(resultado["CODOPCION"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["IP"] != DBNull.Value) entidad.ip = Convert.ToString(resultado["IP"]);
                            if (resultado["NAVEGADOR"] != DBNull.Value) entidad.navegador = Convert.ToString(resultado["NAVEGADOR"]);
                            if (resultado["ACCION"] != DBNull.Value) entidad.accion = Convert.ToString(resultado["ACCION"]);
                            if (resultado["TABLA"] != DBNull.Value) entidad.tabla = Convert.ToString(resultado["TABLA"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);

                            lstAuditoria.Add(entidad);
                        }

                        return lstAuditoria;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AuditoriaData", "ListarAuditoria", ex);
                        return null;
                    }
                }
            }
        }
    }
}