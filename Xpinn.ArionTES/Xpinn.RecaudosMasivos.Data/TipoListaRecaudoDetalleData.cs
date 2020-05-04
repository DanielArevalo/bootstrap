using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla TipoListaRecaudoS
    /// </summary>
    public class TipoListaRecaudoDetalleData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TipoListaRecaudoS
        /// </summary>
        public TipoListaRecaudoDetalleData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public TipoListaRecaudoDetalle CrearTipoListaRecaudoDetalle(TipoListaRecaudoDetalle pTipoListaRecaudoDetalle, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodtipo_lista_detalle = cmdTransaccionFactory.CreateParameter();
                        pcodtipo_lista_detalle.ParameterName = "p_codtipo_lista_detalle";
                        pcodtipo_lista_detalle.Value = pTipoListaRecaudoDetalle.codtipo_lista_detalle;
                        pcodtipo_lista_detalle.Direction = ParameterDirection.Output;
                        pcodtipo_lista_detalle.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodtipo_lista_detalle);

                        DbParameter pidtipo_lista = cmdTransaccionFactory.CreateParameter();
                        pidtipo_lista.ParameterName = "p_idtipo_lista";
                        pidtipo_lista.Value = pTipoListaRecaudoDetalle.idtipo_lista;
                        pidtipo_lista.Direction = ParameterDirection.Input;
                        pidtipo_lista.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtipo_lista);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        ptipo_producto.Value = pTipoListaRecaudoDetalle.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pcod_linea = cmdTransaccionFactory.CreateParameter();
                        pcod_linea.ParameterName = "p_cod_linea";
                        if (pTipoListaRecaudoDetalle.cod_linea == null)
                            pcod_linea.Value = DBNull.Value;
                        else
                            pcod_linea.Value = pTipoListaRecaudoDetalle.cod_linea;
                        pcod_linea.Direction = ParameterDirection.Input;
                        pcod_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_TIPLISDET_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pTipoListaRecaudoDetalle.codtipo_lista_detalle = Convert.ToInt64(pcodtipo_lista_detalle.Value);

                        return pTipoListaRecaudoDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoListaRecaudoDetalleData", "CrearTipoListaRecaudoDetalle", ex);
                        return null;
                    }
                }
            }
        }

        public TipoListaRecaudoDetalle ModificarTipoListaRecaudoDetalle(TipoListaRecaudoDetalle pTipoListaRecaudoDetalle, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodtipo_lista_detalle = cmdTransaccionFactory.CreateParameter();
                        pcodtipo_lista_detalle.ParameterName = "p_codtipo_lista_detalle";
                        pcodtipo_lista_detalle.Value = pTipoListaRecaudoDetalle.codtipo_lista_detalle;
                        pcodtipo_lista_detalle.Direction = ParameterDirection.Input;
                        pcodtipo_lista_detalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodtipo_lista_detalle);

                        DbParameter pidtipo_lista = cmdTransaccionFactory.CreateParameter();
                        pidtipo_lista.ParameterName = "p_idtipo_lista";
                        pidtipo_lista.Value = pTipoListaRecaudoDetalle.idtipo_lista;
                        pidtipo_lista.Direction = ParameterDirection.Input;
                        pidtipo_lista.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtipo_lista);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        ptipo_producto.Value = pTipoListaRecaudoDetalle.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pcod_linea = cmdTransaccionFactory.CreateParameter();
                        pcod_linea.ParameterName = "p_cod_linea";
                        if (pTipoListaRecaudoDetalle.cod_linea == null)
                            pcod_linea.Value = DBNull.Value;
                        else
                            pcod_linea.Value = pTipoListaRecaudoDetalle.cod_linea;
                        pcod_linea.Direction = ParameterDirection.Input;
                        pcod_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_TIPLISDET_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoListaRecaudoDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoListaRecaudoDetalleData", "CrearTipoListaRecaudoDetalle", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarTipoListaRecaudoDetalle(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoListaRecaudoDetalle pTipoListaRecaudoDetalle = new TipoListaRecaudoDetalle();
                        pTipoListaRecaudoDetalle = ConsultarTipoListaRecaudoDetalle(pId, vUsuario);

                        DbParameter pcodtipo_lista_detalle = cmdTransaccionFactory.CreateParameter();
                        pcodtipo_lista_detalle.ParameterName = "p_codtipo_lista_detalle";
                        pcodtipo_lista_detalle.Value = pTipoListaRecaudoDetalle.codtipo_lista_detalle;
                        pcodtipo_lista_detalle.Direction = ParameterDirection.Input;
                        pcodtipo_lista_detalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodtipo_lista_detalle);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_TIPLISDET_ELI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoListaRecaudoDetalleData", "EliminarTipoListaRecaudoDetalle", ex);
                    }
                }
            }
        }

        public TipoListaRecaudoDetalle ConsultarTipoListaRecaudoDetalle(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoListaRecaudoDetalle entidad = new TipoListaRecaudoDetalle();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Tipo_Lista_Recaudo_Detalle WHERE CODTIPO_LISTA_DETALLE = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODTIPO_LISTA_DETALLE"] != DBNull.Value) entidad.codtipo_lista_detalle = Convert.ToInt32(resultado["CODTIPO_LISTA_DETALLE"]);
                            if (resultado["IDTIPO_LISTA"] != DBNull.Value) entidad.idtipo_lista = Convert.ToInt32(resultado["IDTIPO_LISTA"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["COD_LINEA"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA"]);
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
                        BOExcepcion.Throw("TipoListaRecaudoDetalleData", "ConsultarTipoListaRecaudoDetalle", ex);
                        return null;
                    }
                }
            }
        }

        public List<TipoListaRecaudoDetalle> ListarTipoListaRecaudoDetalle(TipoListaRecaudoDetalle pTipoListaRecaudoDetalle, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoListaRecaudoDetalle> lstTipoListaRecaudoDetalle = new List<TipoListaRecaudoDetalle>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Tipo_Lista_Recaudo_Detalle " + ObtenerFiltro(pTipoListaRecaudoDetalle) + " ORDER BY CODTIPO_LISTA_DETALLE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoListaRecaudoDetalle entidad = new TipoListaRecaudoDetalle();
                            if (resultado["CODTIPO_LISTA_DETALLE"] != DBNull.Value) entidad.codtipo_lista_detalle = Convert.ToInt32(resultado["CODTIPO_LISTA_DETALLE"]);
                            if (resultado["IDTIPO_LISTA"] != DBNull.Value) entidad.idtipo_lista = Convert.ToInt32(resultado["IDTIPO_LISTA"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["COD_LINEA"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA"]);
                            lstTipoListaRecaudoDetalle.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoListaRecaudoDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoListaRecaudoDetalleData", "ListarTipoListaRecaudoDetalle", ex);
                        return null;
                    }
                }
            }
        }

    }
}