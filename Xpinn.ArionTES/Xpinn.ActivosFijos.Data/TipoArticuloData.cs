using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.ActivosFijos.Entities;

namespace Xpinn.ActivosFijos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla ActivoFijos
    /// </summary>
    public class TipoArticuloData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ACTIVOS_FIJOS
        /// </summary>
        public TipoArticuloData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public TipoArticulo CrearTipoArticulo(TipoArticulo pTipoArticulo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdTipo_Articulo = cmdTransaccionFactory.CreateParameter();
                        pIdTipo_Articulo.ParameterName = "p_IdTipo_Articulo";
                        pIdTipo_Articulo.Value = pTipoArticulo.IdTipo_Articulo;
                        pIdTipo_Articulo.Direction = ParameterDirection.Input;
                        pIdTipo_Articulo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pIdTipo_Articulo);

                        DbParameter pDescripcion = cmdTransaccionFactory.CreateParameter();
                        pDescripcion.ParameterName = "p_Descripcion";
                        pDescripcion.Value = pTipoArticulo.Descripcion;
                        pDescripcion.Direction = ParameterDirection.Input;
                        pDescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pDescripcion);

                        DbParameter pDias_Periodicidad = cmdTransaccionFactory.CreateParameter();
                        pDias_Periodicidad.ParameterName = "p_Dias_Periodicidad";
                        pDias_Periodicidad.Value = pTipoArticulo.Dias_Periodicidad;
                        pDias_Periodicidad.Direction = ParameterDirection.Input;
                        pDias_Periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pDias_Periodicidad);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_COM_TARTICULO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        //pActivoFijo.consecutivo = Convert.ToInt64(pconsecutivo.Value);

                        return pTipoArticulo;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "CrearActivoFijo", ex);
                        return null;
                    }
                }
            }
        }




        /// <summary>
        /// Modifica un registro en la tabla ActivoFijo de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ActivoFijo modificada</returns>
        public TipoArticulo ModificarTipoArticulo(TipoArticulo pTipoArticulo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdTipo_Articulo = cmdTransaccionFactory.CreateParameter();
                        pIdTipo_Articulo.ParameterName = "p_IdTipo_Articulo";
                        pIdTipo_Articulo.Value = pTipoArticulo.IdTipo_Articulo  ;
                        pIdTipo_Articulo.Direction = ParameterDirection.Input;
                        pIdTipo_Articulo.DbType = DbType.Int32 ;
                        cmdTransaccionFactory.Parameters.Add(pIdTipo_Articulo);

                        DbParameter pDescripcion= cmdTransaccionFactory.CreateParameter();
                        pDescripcion.ParameterName = "p_Descripcion";
                        pDescripcion.Value = pTipoArticulo.Descripcion  ;
                        pDescripcion.Direction = ParameterDirection.Input;
                        pDescripcion.DbType = DbType.String ;
                        cmdTransaccionFactory.Parameters.Add(pDescripcion);

                        DbParameter pDias_Periodicidad = cmdTransaccionFactory.CreateParameter();
                        pDias_Periodicidad.ParameterName = "p_Dias_Periodicidad";
                        pDias_Periodicidad.Value = pTipoArticulo.Dias_Periodicidad  ;
                        pDias_Periodicidad.Direction = ParameterDirection.Input;
                        pDias_Periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pDias_Periodicidad);

                        

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_COM_TARTICULO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pTipoArticulo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreasData", "ModificarAreas", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla ActivoFijoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de ActivoFijoS</param>
        public void EliminarTipoArticulo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                 

                        DbParameter pIdTipo_Articulo = cmdTransaccionFactory.CreateParameter();
                        pIdTipo_Articulo.ParameterName = "p_IdTipo_Articulo";
                        pIdTipo_Articulo.Value = Convert.ToInt32(pId);
                        pIdTipo_Articulo.Direction = ParameterDirection.Input;
                        pIdTipo_Articulo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pIdTipo_Articulo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_COM_TARTICULO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "EliminarConcepto", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla ActivoFijoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ActivoFijoS</param>
        /// <returns>Entidad ActivoFijo consultado</returns>
        public TipoArticulo ConsultarTipoArticulo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoArticulo entidad = new TipoArticulo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM cm_tipo_articulo
                                            WHERE IdTipo_Articulo = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["Dias_Periodicidad"] != DBNull.Value) entidad.IdTipo_Articulo  = Convert.ToInt64(resultado["IdTipo_Articulo"]);
                            if (resultado["Descripcion"] != DBNull.Value) entidad.Descripcion  = resultado["Descripcion"].ToString();
                            if (resultado["Dias_Periodicidad"] != DBNull.Value) entidad.Dias_Periodicidad   = Convert.ToInt64(resultado["Dias_Periodicidad"]);
                           
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
                        BOExcepcion.Throw("AreasData", "ConsultarAreas", ex);
                        return null;
                    }
                }
            }
        }

       

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla ActivoFijoS dados unos filtros
        /// </summary>
        /// <param name="pActivoFijoS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ActivoFijo obtenidos</returns>
        public List<TipoArticulo> ListarTipoArticulo(TipoArticulo pTipoArticulo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoArticulo> lstTipoArticulo = new List<TipoArticulo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT *
                                            FROM cm_tipo_articulo                                             
                                      " + ObtenerFiltro(pTipoArticulo, "Tipo_Articulo.") + " ORDER BY IdTipo_Articulo";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoArticulo entidad = new TipoArticulo();
                            if (resultado["IdTipo_Articulo"] != DBNull.Value) entidad.IdTipo_Articulo   = Convert.ToInt64(resultado["IdTipo_Articulo"]);
                            if (resultado["Descripcion"] != DBNull.Value) entidad.Descripcion   = resultado["Descripcion"].ToString();
                            if (resultado["Dias_Periodicidad"] != DBNull.Value) entidad.Dias_Periodicidad   = Convert.ToInt32(resultado["Dias_Periodicidad"]);

                            lstTipoArticulo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoArticulo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ListarActivoFijo", ex);
                        return null;
                    }
                }
            }
        }

    

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(cod_act) + 1 FROM ACTIVOS_FIJOS ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch 
                    {
                        return 1;
                    }
                }
            }
        }



    }
}
