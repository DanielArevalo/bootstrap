using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Presupuesto.Entities;

namespace Xpinn.Presupuesto.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla TipoPresupuestoS
    /// </summary>
    public class TipoPresupuestoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TipoPresupuestoS
        /// </summary>
        public TipoPresupuestoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TipoPresupuestoS de la base de datos
        /// </summary>
        /// <param name="pTipoPresupuesto">Entidad TipoPresupuesto</param>
        /// <returns>Entidad TipoPresupuesto creada</returns>
        public Xpinn.Presupuesto.Entities.TipoPresupuesto CrearTipoPresupuesto(Xpinn.Presupuesto.Entities.TipoPresupuesto pTipoPresupuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Tipo_Presupuesto = cmdTransaccionFactory.CreateParameter();
                        p_Tipo_Presupuesto.ParameterName = "p_Tipo_Presupuesto";
                        p_Tipo_Presupuesto.Value = ObtenerSiguienteCodigo(vUsuario);
                        p_Tipo_Presupuesto.Direction = ParameterDirection.InputOutput;

                        DbParameter pDescripcion = cmdTransaccionFactory.CreateParameter();
                        pDescripcion.ParameterName = "p_Descripcion";
                        pDescripcion.Value = pTipoPresupuesto.descripcion;

                        cmdTransaccionFactory.Parameters.Add(p_Tipo_Presupuesto);
                        cmdTransaccionFactory.Parameters.Add(pDescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TIPOPRESUPUESTO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pTipoPresupuesto.tipo_presupuesto = Convert.ToInt64(p_Tipo_Presupuesto.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoPresupuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoPresupuestoData", "CrearTipoPresupuesto", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla TipoPresupuestoS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad TipoPresupuesto modificada</returns>
        public Xpinn.Presupuesto.Entities.TipoPresupuesto ModificarTipoPresupuesto(Xpinn.Presupuesto.Entities.TipoPresupuesto pTipoPresupuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Tipo_Presupuesto = cmdTransaccionFactory.CreateParameter();
                        p_Tipo_Presupuesto.ParameterName = "p_Tipo_Presupuesto";
                        p_Tipo_Presupuesto.Value = pTipoPresupuesto.tipo_presupuesto;
                        p_Tipo_Presupuesto.Direction = ParameterDirection.Input;

                        DbParameter pDescripcion = cmdTransaccionFactory.CreateParameter();
                        pDescripcion.ParameterName = "p_descripcion";
                        pDescripcion.Value = pTipoPresupuesto.descripcion;

                        cmdTransaccionFactory.Parameters.Add(p_Tipo_Presupuesto);
                        cmdTransaccionFactory.Parameters.Add(pDescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TIPOPRESUPUESTO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoPresupuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoPresupuestoData", "ModificarTipoPresupuesto", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla TipoPresupuestoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de TipoPresupuestoS</param>
        public void EliminarTipoPresupuesto(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Xpinn.Presupuesto.Entities.TipoPresupuesto pTipoPresupuesto = new Xpinn.Presupuesto.Entities.TipoPresupuesto();

                        DbParameter p_Tipo_Presupuesto = cmdTransaccionFactory.CreateParameter();
                        p_Tipo_Presupuesto.ParameterName = "p_Tipo_Presupuesto";
                        p_Tipo_Presupuesto.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(p_Tipo_Presupuesto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TIPOPRESUPUESTO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoPresupuestoData", "EliminarTipoPresupuesto", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla TipoPresupuestoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TipoPresupuestoS</param>
        /// <returns>Entidad TipoPresupuesto consultado</returns>
        public Xpinn.Presupuesto.Entities.TipoPresupuesto ConsultarTipoPresupuesto(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Xpinn.Presupuesto.Entities.TipoPresupuesto entidad = new Xpinn.Presupuesto.Entities.TipoPresupuesto();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT tipo_presupuesto, descripcion FROM Tipo_Presupuesto" +
                                     " WHERE tipo_presupuesto = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["tipo_presupuesto"] != DBNull.Value) entidad.tipo_presupuesto = Convert.ToInt64(resultado["tipo_presupuesto"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
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
                        BOExcepcion.Throw("TipoPresupuestoData", "ConsultarTipoPresupuesto", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TipoPresupuesto dados unos filtros
        /// </summary>
        /// <param name="pTipoPresupuesto">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoPresupuestos obtenidos</returns>
        public List<Xpinn.Presupuesto.Entities.TipoPresupuesto> ListarTipoPresupuesto(Xpinn.Presupuesto.Entities.TipoPresupuesto pTipoPresupuesto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.Presupuesto.Entities.TipoPresupuesto> lstTipoPresupuesto = new List<Xpinn.Presupuesto.Entities.TipoPresupuesto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  Tipo_Presupuesto " + ObtenerFiltro(pTipoPresupuesto) + " ORDER BY descripcion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.Presupuesto.Entities.TipoPresupuesto entidad = new Xpinn.Presupuesto.Entities.TipoPresupuesto();

                            if (resultado["tipo_presupuesto"] != DBNull.Value) entidad.tipo_presupuesto = Convert.ToInt64(resultado["tipo_presupuesto"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);

                            lstTipoPresupuesto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoPresupuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoPresupuestoData", "ListarTipoPresupuesto", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene el siguiente codigo disponible de la tabla
        /// </summary>
        /// <returns>codigo disponible</returns>
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(tipo_presupuesto) + 1 FROM Tipo_Presupuesto ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoPresupuestoData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }


    }
}