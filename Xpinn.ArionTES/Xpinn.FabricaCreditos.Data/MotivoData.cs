using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    public class MotivoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public MotivoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea una entidad Motivo de negacion en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Motivo negacion</param>
        /// <returns>Entidad creada</returns>
        public Motivo InsertarMotivo(Motivo pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pEntidad.Descripcion;
                        p_descripcion.DbType = DbType.AnsiString;
                        p_descripcion.Size = 100;
                        p_descripcion.Direction = ParameterDirection.Input;

                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        p_tipo.Value = pEntidad.Tipo;
                        p_tipo.DbType = DbType.Int32;
                        p_tipo.Size=38;
                        p_tipo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_tipo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APR_MOTIV_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoData", "InsertarMotivo", ex);
                        return null;
                    }
                }
            }
        }

       

        /// <summary>
        /// Obtiene la lista de Motivos de negacion
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Motivos de negacion obtenidos</returns>
        public List<Motivo> ListarMotivos(Motivo entidad, Usuario pUsuario) 
        {
            DbDataReader resultado = default(DbDataReader);
            List<Motivo> lstMotivos = new List<Motivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select cod_motivo as codigo, descripcion, Decode(tipo, 1, 'Aplazamento', 2, 'Negacion') as tipo from motivos_cre";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new Motivo();
                            //Asociar todos los valores a la entidad
                            if (resultado["codigo"] != DBNull.Value) entidad.Codigo = Convert.ToInt32(resultado["codigo"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["tipo"] != DBNull.Value) entidad.Tipo = Convert.ToString(resultado["tipo"]);
                            lstMotivos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstMotivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoData", "ListarMotivos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un Motivo de negacion
        /// </summary>
        /// <param name="pId">identificador del motivo</param>
        public void EliminarMotivo(Int32 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_codigo = cmdTransaccionFactory.CreateParameter();
                        p_codigo.ParameterName = "p_codigo";
                        p_codigo.Value = pId;
                        p_codigo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_codigo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APR_MOTIV_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoData", "EliminarMotivo", ex);
                    }

                }
            }
        }

        /// <summary>
        /// Obtiene la lista de Motivos de negacion
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Motivos de negacion obtenidos</returns>
        public List<Motivo> ListarMotivosFiltro(Motivo entidad, Usuario pUsuario, int filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Motivo> lstMotivos = new List<Motivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select cod_motivo as codigo, descripcion from motivos_cre where tipo="+filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new Motivo();
                            //Asociar todos los valores a la entidad
                            if (resultado["codigo"] != DBNull.Value) entidad.Codigo = Convert.ToInt32(resultado["codigo"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["descripcion"]);
                            lstMotivos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstMotivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoData", "ListarMotivosFiltro", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de Motivos de retiro
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Motivos de retiro obtenidos</returns>
        public List<Motivo> ListarMotivosRetiro(Motivo entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Motivo> lstMotivos = new List<Motivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select cod_motivo, descripcion from motivo_retiro";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new Motivo();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_motivo"] != DBNull.Value) entidad.Codigo = Convert.ToInt32(resultado["cod_motivo"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["descripcion"]);
                            lstMotivos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstMotivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MotivoData", "ListarMotivos", ex);
                        return null;
                    }
                }
            }
        }
    }
}
