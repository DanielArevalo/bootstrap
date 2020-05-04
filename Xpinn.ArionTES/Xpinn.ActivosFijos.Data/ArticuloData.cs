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
    public class ArticuloData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ACTIVOS_FIJOS
        /// </summary>
        public ArticuloData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Articulo CrearArticulo(Articulo pArticulo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        DbParameter pidarticulo = cmdTransaccionFactory.CreateParameter();
                        pidarticulo.ParameterName = "p_idarticulo";
                        pidarticulo.Value = pArticulo .idarticulo;
                        pidarticulo.Direction = ParameterDirection.Input;
                        pidarticulo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidarticulo);

                        DbParameter pserial = cmdTransaccionFactory.CreateParameter();
                        pserial.ParameterName = "p_serial";
                        pserial.Value = pArticulo.serial;
                        pserial.Direction = ParameterDirection.Input;
                        pserial.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pserial);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pArticulo.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);


                        DbParameter pidtipo_articulo = cmdTransaccionFactory.CreateParameter();
                        pidtipo_articulo.ParameterName = "p_idtipo_articulo";
                        pidtipo_articulo.Value = pArticulo.idtipo_articulo;
                        pidtipo_articulo.Direction = ParameterDirection.Input;
                        pidtipo_articulo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtipo_articulo);

                        DbParameter preferencia = cmdTransaccionFactory.CreateParameter();
                        preferencia.ParameterName = "p_referencia";
                        preferencia.Value = pArticulo.referencia;
                        preferencia.Direction = ParameterDirection.Input;
                        preferencia.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preferencia);

                        DbParameter pmarca = cmdTransaccionFactory.CreateParameter();
                        pmarca.ParameterName = "p_marca";
                        pmarca.Value = pArticulo.marca;
                        pmarca.Direction = ParameterDirection.Input;
                        pmarca.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmarca);                     





                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_COM_ARTICULO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        //pActivoFijo.consecutivo = Convert.ToInt64(pconsecutivo.Value);

                        return pArticulo;

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
        public Articulo ModificarArticulo(Articulo pArticulo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pidarticulo = cmdTransaccionFactory.CreateParameter();
                        pidarticulo.ParameterName = "p_idarticulo";
                        pidarticulo.Value = pArticulo.idarticulo;
                        pidarticulo.Direction = ParameterDirection.Input;
                        pidarticulo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidarticulo);

                        DbParameter pserial = cmdTransaccionFactory.CreateParameter();
                        pserial.ParameterName = "p_serial";
                        pserial.Value = pArticulo.serial;
                        pserial.Direction = ParameterDirection.Input;
                        pserial.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pserial);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pArticulo.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);


                        DbParameter pidtipo_articulo = cmdTransaccionFactory.CreateParameter();
                        pidtipo_articulo.ParameterName = "p_idtipo_articulo";
                        pidtipo_articulo.Value = pArticulo.idtipo_articulo;
                        pidtipo_articulo.Direction = ParameterDirection.Input;
                        pidtipo_articulo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtipo_articulo);

                        DbParameter preferencia = cmdTransaccionFactory.CreateParameter();
                        preferencia.ParameterName = "p_referencia";
                        preferencia.Value = pArticulo.referencia;
                        preferencia.Direction = ParameterDirection.Input;
                        preferencia.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preferencia);

                        DbParameter pmarca = cmdTransaccionFactory.CreateParameter();
                        pmarca.ParameterName = "p_marca";
                        pmarca.Value = pArticulo.marca;
                        pmarca.Direction = ParameterDirection.Input;
                        pmarca.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmarca);





                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_COM_ARTICULO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pArticulo;
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
        public void EliminarArticulo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Articulo pArticulo = new Articulo();

                        DbParameter pIdArticulo = cmdTransaccionFactory.CreateParameter();
                        pIdArticulo.ParameterName = "p_IdArticulo";
                        pIdArticulo.Value = Convert.ToInt32(pId);
                        pIdArticulo.Direction = ParameterDirection.Input;
                        pIdArticulo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pIdArticulo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_COM_ARTICULO_ELIMI";
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
        public Articulo ConsultarArticulo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Articulo entidad = new Articulo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM cm_articulo
                                            WHERE IdArticulo = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["idarticulo"] != DBNull.Value) entidad.idarticulo  = Convert.ToInt64(resultado["idarticulo"]);
                            if (resultado["idtipo_articulo"] != DBNull.Value) entidad.idtipo_articulo   = Convert.ToInt64(resultado["idtipo_articulo"].ToString());
                            if (resultado["serial"] != DBNull.Value) entidad.serial   = resultado["serial"].ToString();
                            if (resultado["marca"] != DBNull.Value) entidad.marca = resultado["marca"].ToString();
                            if (resultado["referencia"] != DBNull.Value) entidad.referencia  = resultado["referencia"].ToString();
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion  = resultado["descripcion"].ToString();

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
        public List<Articulo> ListarArticulo(Articulo pArticulo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Articulo> lstArticulo = new List<Articulo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT *
                                            FROM cm_articulo                                             
                                      " + ObtenerFiltro(pArticulo, "Articulo.") + " ORDER BY IdArticulo";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Articulo entidad = new Articulo();
                            if (resultado["idarticulo"] != DBNull.Value) entidad.idarticulo = Convert.ToInt64(resultado["idarticulo"]);
                            if (resultado["idtipo_articulo"] != DBNull.Value) entidad.idtipo_articulo = Convert.ToInt64(resultado["idtipo_articulo"].ToString());
                            if (resultado["serial"] != DBNull.Value) entidad.serial = resultado["serial"].ToString();
                            if (resultado["marca"] != DBNull.Value) entidad.marca = resultado["marca"].ToString();
                            if (resultado["referencia"] != DBNull.Value) entidad.referencia = resultado["referencia"].ToString();
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = resultado["descripcion"].ToString();

                            lstArticulo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstArticulo;
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
