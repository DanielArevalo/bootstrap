using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Comun.Entities;

namespace Xpinn.Comun.Data
{
    public class TipoIdentificacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ConceptoS
        /// </summary>
        public TipoIdentificacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<TipoIdentificacion> ListarTipoIdentificacion(TipoIdentificacion pTipoIdentificacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoIdentificacion> lstTipoIdentificacion = new List<TipoIdentificacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TipoIdentificacion " + ObtenerFiltro(pTipoIdentificacion) + " ORDER BY CODTIPOIDENTIFICACION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoIdentificacion entidad = new TipoIdentificacion();
                            if (resultado["CODTIPOIDENTIFICACION"] != DBNull.Value) entidad.IdTipoIdentificacion = Convert.ToInt32(resultado["CODTIPOIDENTIFICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTipoIdentificacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoIdentificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoIdentificacionData", "ListarTipoIdentificacion", ex);
                        return null;
                    }
                }
            }
        }

        public TipoIdentificacion ConsultarTipoIdentificacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoIdentificacion entidad = new TipoIdentificacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TipoIdentificacion WHERE CODTIPOIDENTIFICACION = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODTIPOIDENTIFICACION"] != DBNull.Value) entidad.IdTipoIdentificacion = Convert.ToInt32(resultado["CODTIPOIDENTIFICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
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
                        BOExcepcion.Throw("TipoIdentificacionData", "ConsultarTipoIdentificacion", ex);
                        return null;
                    }
                }
            }
        }

        public TipoIdentificacion ConsultarTipoIdentificacion(String pNId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoIdentificacion entidad = new TipoIdentificacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TipoIdentificacion WHERE DESCRIPCION = '" + pNId.ToString() + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODTIPOIDENTIFICACION"] != DBNull.Value) entidad.IdTipoIdentificacion = Convert.ToInt32(resultado["CODTIPOIDENTIFICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
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
                        BOExcepcion.Throw("TipoIdentificacionData", "ConsultarTipoIdentificacion", ex);
                        return null;
                    }
                }
            }
        }

    }
}
