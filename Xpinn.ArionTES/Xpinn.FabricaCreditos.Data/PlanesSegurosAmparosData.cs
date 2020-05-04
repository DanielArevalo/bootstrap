using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.FabricaCreditos.Entities;


namespace Xpinn.FabricaCreditos.Data
{

    /// <summary>
    /// Objeto de acceso a datos para  la tabla PlanesSegurosAmparos
    /// </summary>
    public class PlanesSegurosAmparosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public PlanesSegurosAmparosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de PlanesSegurosAmparos dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PlanesSegurosAmparos obtenidos</returns>
        public List<PlanesSegurosAmparos> ListarPlanesSegurosAmparos(PlanesSegurosAmparos pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PlanesSegurosAmparos> lstPlanesSegurosAmparos = new List<PlanesSegurosAmparos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT * FROM PlanesSegurosAmparos ";// +ObtenerFiltro(pEntidad);
                        if (pEntidad.tipo_plan.ToString().Trim().Length >= 0)
                        {
                            sql = sql + " where tipo_plan = " + pEntidad.tipo_plan.ToString() + " and tipo = '" + pEntidad.tipo + "'";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PlanesSegurosAmparos entidad = new PlanesSegurosAmparos();
                            //Asociar todos los valores a la entidad
                            if (resultado["tipo_plan"] != DBNull.Value) entidad.tipo_plan = Convert.ToInt64(resultado["tipo_plan"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["tipo"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["tipo"]);
                            if (resultado["consecutivo"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["consecutivo"]);
                            if (resultado["valor_cubierto"] != DBNull.Value) entidad.valor_cubierto = Convert.ToInt64(resultado["valor_cubierto"]);
                            if (resultado["valor_cubierto_conyuge"] != DBNull.Value) entidad.valor_cubierto_conyuge = Convert.ToInt64(resultado["valor_cubierto_conyuge"]);
                            if (resultado["valor_cubierto_hijos"] != DBNull.Value) entidad.valor_cubierto_hijos = Convert.ToInt64(resultado["valor_cubierto_hijos"]);

                            lstPlanesSegurosAmparos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlanesSegurosAmparos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesSegurosAmparosData", "ListarPlanesSegurosAmparos", ex);
                        return null;
                    }

                }
            }
        }



        /// <summary>
        /// Modifica una entidad PlanesSegurosAmparos en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad PlanesSegurosAmparos</param>
        /// <returns>Entidad modificada</returns>
        public PlanesSegurosAmparos ModificarPlanesSegurosAmparos(PlanesSegurosAmparos pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_consecutivo = cmdTransaccionFactory.CreateParameter();
                        p_consecutivo.ParameterName = "p_consecutivo";
                        p_consecutivo.Value = pEntidad.consecutivo;
                        p_consecutivo.DbType = DbType.Int64;
                        p_consecutivo.Size = 8;
                        p_consecutivo.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pEntidad.descripcion;
                        p_descripcion.DbType = DbType.AnsiString;
                        p_descripcion.Size = 60;
                        p_descripcion.Direction = ParameterDirection.Input;

                        DbParameter p_valor_cubierto = cmdTransaccionFactory.CreateParameter();
                        p_valor_cubierto.ParameterName = "p_valor_cubierto";
                        p_valor_cubierto.Value = pEntidad.valor_cubierto;
                        p_valor_cubierto.DbType = DbType.Int64;
                        p_valor_cubierto.Size = 8;
                        p_valor_cubierto.Direction = ParameterDirection.Input;

                        DbParameter p_valor_cubierto_conyuge = cmdTransaccionFactory.CreateParameter();
                        p_valor_cubierto_conyuge.ParameterName = "p_valor_cubierto_conyuge";
                        p_valor_cubierto_conyuge.Value = pEntidad.valor_cubierto_conyuge;
                        p_valor_cubierto_conyuge.DbType = DbType.Int64;
                        p_valor_cubierto_conyuge.Size = 8;
                        p_valor_cubierto_conyuge.Direction = ParameterDirection.Input;

                        DbParameter p_valor_cubierto_hijos = cmdTransaccionFactory.CreateParameter();
                        p_valor_cubierto_hijos.ParameterName = "p_valor_cubierto_hijos";
                        p_valor_cubierto_hijos.Value = pEntidad.valor_cubierto_hijos;
                        p_valor_cubierto_hijos.DbType = DbType.Int64;
                        p_valor_cubierto_hijos.Size = 8;
                        p_valor_cubierto_hijos.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_consecutivo);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_valor_cubierto);
                        cmdTransaccionFactory.Parameters.Add(p_valor_cubierto_conyuge);
                        cmdTransaccionFactory.Parameters.Add(p_valor_cubierto_hijos);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SEG_AMP_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        /* if (pUsuario.programaGeneraLog)
                             DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                         */
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesSegurosAmparosData", "ModificarPlanesSegurosAmparos", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Crea una entidad PlanesSegurosAmparos en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad PlanesSegurosAmparos</param>
        /// <returns>Entidad creada</returns>
        public PlanesSegurosAmparos InsertarPlanesSegurosAmparos(PlanesSegurosAmparos pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_tipo_plan = cmdTransaccionFactory.CreateParameter();
                        p_tipo_plan.ParameterName = "p_tipo_plan";
                        p_tipo_plan.Value = pEntidad.tipo_plan;
                        p_tipo_plan.DbType = DbType.Int64;
                        p_tipo_plan.Direction = ParameterDirection.Input;

                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        p_tipo.Value = pEntidad.tipo;
                        p_tipo.DbType = DbType.AnsiString;
                        p_tipo.Size = 60;
                        p_tipo.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pEntidad.descripcion;
                        p_descripcion.DbType = DbType.AnsiString;
                        p_descripcion.Size = 60;
                        p_descripcion.Direction = ParameterDirection.Input;

                        DbParameter p_valor_cubierto = cmdTransaccionFactory.CreateParameter();
                        p_valor_cubierto.ParameterName = "p_valor_cubierto";
                        p_valor_cubierto.Value = pEntidad.valor_cubierto;
                        p_valor_cubierto.DbType = DbType.Int64;
                       // p_valor_cubierto.Size = 8;
                        p_valor_cubierto.Direction = ParameterDirection.Input;

                        DbParameter p_valor_cubierto_conyuge = cmdTransaccionFactory.CreateParameter();
                        p_valor_cubierto_conyuge.ParameterName = "p_valor_cubierto_conyuge";
                        p_valor_cubierto_conyuge.Value = pEntidad.valor_cubierto_conyuge;
                        p_valor_cubierto_conyuge.DbType = DbType.Int64;
                        //p_valor_cubierto_conyuge.Size = 8;
                        p_valor_cubierto_conyuge.Direction = ParameterDirection.Input;

                        DbParameter p_valor_cubierto_hijos = cmdTransaccionFactory.CreateParameter();
                        p_valor_cubierto_hijos.ParameterName = "p_valor_cubierto_hijos";
                        p_valor_cubierto_hijos.Value = pEntidad.valor_cubierto_hijos;
                        p_valor_cubierto_hijos.DbType = DbType.Int64;
                       // p_valor_cubierto_hijos.Size = 8;
                        p_valor_cubierto_hijos.Direction = ParameterDirection.Input;
                       
                        DbParameter p_consecutivo = cmdTransaccionFactory.CreateParameter();
                        p_consecutivo.ParameterName = "p_consecutivo";
                        p_consecutivo.DbType = DbType.Int64;
                       // p_consecutivo.Size = 8;
                        p_consecutivo.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(p_tipo_plan);
                        cmdTransaccionFactory.Parameters.Add(p_tipo);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_valor_cubierto);
                        cmdTransaccionFactory.Parameters.Add(p_valor_cubierto_conyuge);
                        cmdTransaccionFactory.Parameters.Add(p_valor_cubierto_hijos);
                        cmdTransaccionFactory.Parameters.Add(p_consecutivo);

                        //pEntidad.consecutivo = Convert.ToInt64(p_consecutivo.Value);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SEG_AMP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                      
                        /* if (pUsuario.programaGeneraLog)
                             DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                         */
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesSegurosAmparosData", "InsertarPlanesSegurosAmparos", ex);
                        return null;
                    }

                }

            }
        }

        /// <summary>
        /// Elimina un PlanesSegurosAmparos en la base de datos
        /// </summary>
        /// <param name="pId">identificador del PlanesSegurosAmparos</param>
        public void EliminarPlanesSegurosAmparos(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        PlanesSegurosAmparos pEntidad = new PlanesSegurosAmparos();

                        // if (pUsuario.programaGeneraLog)
                        pEntidad = ConsultarPlanesSegurosAmparos(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter p_consecutivo = cmdTransaccionFactory.CreateParameter();
                        p_consecutivo.ParameterName = "p_consecutivo";
                        p_consecutivo.Value = pId;
                        p_consecutivo.DbType = DbType.Int16;
                        p_consecutivo.Size = 8;
                        p_consecutivo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_consecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SEG_AMP_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        /*  if (pUsuario.programaGeneraLog)
                              DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Eliminar.ToString()); */
                        //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesSegurosAmparosData", "EliminarPlanesSegurosAmparos", ex);
                    }
                }

            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla PlanSegurosAmparos de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>PlanesSegurosAmparos consultada</returns>
        public PlanesSegurosAmparos ConsultarPlanesSegurosAmparos(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            PlanesSegurosAmparos entidad = new PlanesSegurosAmparos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM PLANESSEGUROSAMPAROS where  consecutivo  =" + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["consecutivo"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["consecutivo"]);
                            if (resultado["tipo_plan"] != DBNull.Value) entidad.tipo_plan = Convert.ToInt64(resultado["tipo_plan"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["tipo"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["tipo"]);
                            if (resultado["valor_cubierto"] != DBNull.Value) entidad.valor_cubierto = Convert.ToInt64(resultado["valor_cubierto"]);
                            if (resultado["valor_cubierto_conyuge"] != DBNull.Value) entidad.valor_cubierto_conyuge = Convert.ToInt64(resultado["valor_cubierto_conyuge"]);
                            if (resultado["valor_cubierto_hijos"] != DBNull.Value) entidad.valor_cubierto_hijos = Convert.ToInt64(resultado["valor_cubierto_hijos"]);
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
                        BOExcepcion.Throw("PlanesSegurosAmparosData", "ConsultarPlanesSegurosAmparos", ex);
                        return null;
                    }
                }
            }
        }

    }
}

