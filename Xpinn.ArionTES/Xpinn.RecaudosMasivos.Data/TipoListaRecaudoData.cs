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
    public class TipoListaRecaudoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TipoListaRecaudoS
        /// </summary>
        public TipoListaRecaudoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public TipoListaRecaudo CrearTipoListaRecaudo(TipoListaRecaudo pTipoListaRecaudo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidtipo_lista = cmdTransaccionFactory.CreateParameter();
                        pidtipo_lista.ParameterName = "p_idtipo_lista";
                        pidtipo_lista.Value = pTipoListaRecaudo.idtipo_lista;
                        pidtipo_lista.Direction = ParameterDirection.Input;
                        pidtipo_lista.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidtipo_lista);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pTipoListaRecaudo.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_TIPOLISTA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pTipoListaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoListaRecaudoData", "CrearTipoListaRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public TipoListaRecaudo ModificarTipoListaRecaudo(TipoListaRecaudo pTipoListaRecaudo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidtipo_lista = cmdTransaccionFactory.CreateParameter();
                        pidtipo_lista.ParameterName = "p_idtipo_lista";
                        pidtipo_lista.Value = pTipoListaRecaudo.idtipo_lista;
                        pidtipo_lista.Direction = ParameterDirection.Input;
                        pidtipo_lista.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtipo_lista);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pTipoListaRecaudo.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_TIPOLISTA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoListaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoListaRecaudoData", "CrearTipoListaRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarTipoListaRecaudo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoListaRecaudo pTipoListaRecaudo = new TipoListaRecaudo();
                        pTipoListaRecaudo = ConsultarTipoListaRecaudo(pId, vUsuario);

                        DbParameter pidtipo_lista = cmdTransaccionFactory.CreateParameter();
                        pidtipo_lista.ParameterName = "p_idtipo_lista";
                        pidtipo_lista.Value = pTipoListaRecaudo.idtipo_lista;
                        pidtipo_lista.Direction = ParameterDirection.Input;
                        pidtipo_lista.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtipo_lista);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_TIPOLISTA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoListaRecaudoData", "EliminarTipoListaRecaudo", ex);
                    }
                }
            }
        }

        public TipoListaRecaudo ConsultarTipoListaRecaudo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoListaRecaudo entidad = new TipoListaRecaudo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TIPO_LISTA_RECAUDO WHERE IDTIPO_LISTA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDTIPO_LISTA"] != DBNull.Value) entidad.idtipo_lista = Convert.ToInt32(resultado["IDTIPO_LISTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
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
                        BOExcepcion.Throw("TipoListaRecaudoData", "ConsultarTipoListaRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public List<TipoListaRecaudo> ListarTipoListaRecaudo(TipoListaRecaudo pTipoListaRecaudo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoListaRecaudo> lstTipoListaRecaudo = new List<TipoListaRecaudo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TIPO_LISTA_RECAUDO " + ObtenerFiltro(pTipoListaRecaudo) + " ORDER BY IDTIPO_LISTA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoListaRecaudo entidad = new TipoListaRecaudo();
                            if (resultado["IDTIPO_LISTA"] != DBNull.Value) entidad.idtipo_lista = Convert.ToInt32(resultado["IDTIPO_LISTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTipoListaRecaudo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoListaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoListaRecaudoData", "ListarTipoListaRecaudo", ex);
                        return null;
                    }
                }
            }
        }


    }
}