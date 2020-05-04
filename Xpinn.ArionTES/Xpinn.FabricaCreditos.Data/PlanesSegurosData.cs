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

    /// <summary>
    /// Objeto de acceso a datos para la tabla PlanesSeguros
    /// </summary>
    public class PlanesSegurosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public PlanesSegurosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de PlanesSeguros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PlanesSeguros obtenidos</returns>
        public List<PlanesSeguros> ListarPlanesSeguros(PlanesSeguros pplan, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PlanesSeguros> lstPlanesSeguros = new List<PlanesSeguros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT * FROM PLANESSEGUROS " + ObtenerFiltro(pplan) + " order by tipo_plan asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PlanesSeguros entidad = new PlanesSeguros();
                            //Asociar todos los valores a la entidad
                            if (resultado["tipo_plan"] != DBNull.Value) entidad.tipo_plan = Convert.ToInt64(resultado["tipo_plan"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["prima_individual"] != DBNull.Value) entidad.prima_individual = Convert.ToInt64(resultado["prima_individual"]);
                            if (resultado["prima_conyuge"] != DBNull.Value) entidad.prima_conyuge = Convert.ToInt64(resultado["prima_conyuge"]);
                            if (resultado["prima_accidentes_individual"] != DBNull.Value) entidad.prima_accidentes_individual = Convert.ToInt64(resultado["prima_accidentes_individual"]);
                            if (resultado["prima_accidentes_familiar"] != DBNull.Value) entidad.prima_accidentes_familiar = Convert.ToInt64(resultado["prima_accidentes_familiar"]);
                            lstPlanesSeguros.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlanesSeguros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesSegurosData", "ListarPlanesSeguros", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Modifica una entidad PlanesSeguros en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad PlanesSeguros</param>
        /// <returns>Entidad modificada</returns>
        public PlanesSeguros ModificarPlanesSeguros(PlanesSeguros pEntidad, Usuario pUsuario)
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
                        p_tipo_plan.Size = 8;
                        p_tipo_plan.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pEntidad.descripcion;
                        p_descripcion.DbType = DbType.AnsiString;
                        p_descripcion.Size = 50;
                        p_descripcion.Direction = ParameterDirection.Input;

                        DbParameter p_prima_individual = cmdTransaccionFactory.CreateParameter();
                        p_prima_individual.ParameterName = "p_prima_individual";
                        p_prima_individual.Value = pEntidad.prima_individual;
                        p_prima_individual.DbType = DbType.Int64;
                        p_prima_individual.Size = 8;
                        p_prima_individual.Direction = ParameterDirection.Input;

                        DbParameter p_prima_conyuge = cmdTransaccionFactory.CreateParameter();
                        p_prima_conyuge.ParameterName = "p_prima_conyuge";
                        p_prima_conyuge.Value = pEntidad.prima_conyuge;
                        p_prima_conyuge.DbType = DbType.Int64;
                        p_prima_conyuge.Size = 8;
                        p_prima_conyuge.Direction = ParameterDirection.Input;

                        DbParameter p_prima_accidentes_individual = cmdTransaccionFactory.CreateParameter();
                        p_prima_accidentes_individual.ParameterName = "p_prima_accidentes_individual";
                        p_prima_accidentes_individual.Value = pEntidad.prima_accidentes_individual;
                        p_prima_accidentes_individual.DbType = DbType.Int64;
                        p_prima_accidentes_individual.Size = 8;
                        p_prima_accidentes_individual.Direction = ParameterDirection.Input;

                        DbParameter p_prima_accidentes_familiar = cmdTransaccionFactory.CreateParameter();
                        p_prima_accidentes_familiar.ParameterName = "p_prima_accidentes_familiar";
                        p_prima_accidentes_familiar.Value = pEntidad.prima_accidentes_familiar;
                        p_prima_accidentes_familiar.DbType = DbType.Int64;
                        p_prima_accidentes_familiar.Size = 8;
                        p_prima_accidentes_familiar.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_tipo_plan);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_prima_individual);
                        cmdTransaccionFactory.Parameters.Add(p_prima_conyuge);
                        cmdTransaccionFactory.Parameters.Add(p_prima_accidentes_individual);
                        cmdTransaccionFactory.Parameters.Add(p_prima_accidentes_familiar);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SEG_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        /* if (pUsuario.programaGeneraLog)
                             DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                         */
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesSegurosData", "ModificarPlanesSeguros", ex);
                        return null;
                    }

                }

            }
        }

        /// <summary>
        /// Crea una entidad PlanesSeguros en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad PlanesSeguros</param>
        /// <returns>Entidad creada</returns>
        public PlanesSeguros InsertarPlanesSeguros(PlanesSeguros pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pEntidad.descripcion;
                        p_descripcion.DbType = DbType.AnsiString;
                        p_descripcion.Size = 50;
                        p_descripcion.Direction = ParameterDirection.Input;

                        DbParameter p_prima_individual = cmdTransaccionFactory.CreateParameter();
                        p_prima_individual.ParameterName = "p_prima_individual";
                        p_prima_individual.Value = pEntidad.prima_individual;
                        p_prima_individual.DbType = DbType.Int64;
                        p_prima_individual.Size = 8;
                        p_prima_individual.Direction = ParameterDirection.Input;

                        DbParameter p_prima_conyuge = cmdTransaccionFactory.CreateParameter();
                        p_prima_conyuge.ParameterName = "p_prima_conyuge";
                        p_prima_conyuge.Value = pEntidad.prima_conyuge;
                        p_prima_conyuge.DbType = DbType.Int64;
                        p_prima_conyuge.Size = 8;
                        p_prima_conyuge.Direction = ParameterDirection.Input;

                        DbParameter p_prima_accidentes_individual = cmdTransaccionFactory.CreateParameter();
                        p_prima_accidentes_individual.ParameterName = "p_prima_accidentes_individual";
                        p_prima_accidentes_individual.Value = pEntidad.prima_accidentes_individual;
                        p_prima_accidentes_individual.DbType = DbType.Int64;
                        p_prima_accidentes_individual.Size = 8;
                        p_prima_accidentes_individual.Direction = ParameterDirection.Input;

                        DbParameter p_prima_accidentes_familiar = cmdTransaccionFactory.CreateParameter();
                        p_prima_accidentes_familiar.ParameterName = "p_prima_accidentes_familiar";
                        p_prima_accidentes_familiar.Value = pEntidad.prima_accidentes_familiar;
                        p_prima_accidentes_familiar.DbType = DbType.Int64;
                        p_prima_accidentes_familiar.Size = 8;
                        p_prima_accidentes_familiar.Direction = ParameterDirection.Input;

                        DbParameter p_consecutivo = cmdTransaccionFactory.CreateParameter();
                        p_consecutivo.ParameterName = "p_consecutivo";
                        p_consecutivo.DbType = DbType.Int64;
                        p_consecutivo.Size = 8;
                        p_consecutivo.Direction = ParameterDirection.Output;

                        //p_consecutivo.Value = pEntidad.prima_accidentes_familiar;

                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_prima_individual);
                        cmdTransaccionFactory.Parameters.Add(p_prima_conyuge);
                        cmdTransaccionFactory.Parameters.Add(p_prima_accidentes_individual);
                        cmdTransaccionFactory.Parameters.Add(p_prima_accidentes_familiar);
                        cmdTransaccionFactory.Parameters.Add(p_consecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SEG_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEntidad.tipo_plan = Convert.ToInt64(p_consecutivo.Value);

                        /* if (pUsuario.programaGeneraLog)
                           DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                       */
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesSegurosData", "InsertarPlanesSeguros", ex);
                        return null;
                    }

                }

            }
        }

        /// <summary>
        /// Elimina un PlanesSeguros en la base de datos
        /// </summary>
        /// <param name="pId">identificador del PlanesSeguros</param>
        public void EliminarPlanesSeguros(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        PlanesSeguros pEntidad = new PlanesSeguros();

                        //if (pUsuario.programaGeneraLog)
                        pEntidad = ConsultarPlanesSeguros(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter p_tipo_plan = cmdTransaccionFactory.CreateParameter();
                        p_tipo_plan.ParameterName = "p_tipo_plan";
                        p_tipo_plan.Value = pId;
                        p_tipo_plan.DbType = DbType.Int64;
                        p_tipo_plan.Size = 8;
                        p_tipo_plan.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_tipo_plan);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_SEGUROS_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        /*  if (pUsuario.programaGeneraLog)
                              DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Eliminar.ToString()); */
                        //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanesSegurosData", "EliminarPlanesSeguros", ex);
                    }
                }

            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla Oficina de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>PlanesSeguros consultada</returns>
        public PlanesSeguros ConsultarPlanesSeguros(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            PlanesSeguros entidad = new PlanesSeguros();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT tipo_plan,descripcion,prima_individual,prima_Conyuge,prima_Accidentes_Individual,prima_Accidentes_Familiar FROM PLANESSEGUROS where tipo_plan =" + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["tipo_plan"] != DBNull.Value) entidad.tipo_plan = Convert.ToInt64(resultado["tipo_plan"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["prima_Individual"] != DBNull.Value) entidad.prima_individual = Convert.ToInt64(resultado["prima_Individual"]);
                            if (resultado["prima_Conyuge"] != DBNull.Value) entidad.prima_conyuge = Convert.ToInt64(resultado["prima_Conyuge"]);
                            if (resultado["prima_Accidentes_Individual"] != DBNull.Value) entidad.prima_accidentes_individual = Convert.ToInt64(resultado["prima_Accidentes_Individual"]);
                            if (resultado["prima_Accidentes_Familiar"] != DBNull.Value) entidad.prima_accidentes_familiar = Convert.ToInt64(resultado["prima_Accidentes_Familiar"].ToString());
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
                        BOExcepcion.Throw("PlanesSegurosData", "ConsultarPlanesSeguros", ex);
                        return null;
                    }

                }

            }
        }

    }
}
