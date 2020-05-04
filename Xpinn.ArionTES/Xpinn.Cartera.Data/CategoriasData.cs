using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;

namespace Xpinn.Cartera.Data
{
    public class CategoriasData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public CategoriasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Categorias CrearCategorias(Categorias pCategorias, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_categoria = cmdTransaccionFactory.CreateParameter();
                        pcod_categoria.ParameterName = "p_cod_categoria";
                        pcod_categoria.Value = pCategorias.cod_categoria;
                        pcod_categoria.Direction = ParameterDirection.Input;
                        pcod_categoria.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_categoria);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pCategorias.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pCategorias.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CATEGORIAS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCategorias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CategoriasData", "CrearCategorias", ex);
                        return null;
                    }
                }
            }
        }


        public Categorias ModificarCategorias(Categorias pCategorias, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_categoria = cmdTransaccionFactory.CreateParameter();
                        pcod_categoria.ParameterName = "p_cod_categoria";
                        pcod_categoria.Value = pCategorias.cod_categoria;
                        pcod_categoria.Direction = ParameterDirection.Input;
                        pcod_categoria.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_categoria);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pCategorias.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pCategorias.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CATEGORIAS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCategorias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CategoriasData", "ModificarCategorias", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarCategorias(string pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Categorias pCategorias = new Categorias();
                        pCategorias = ConsultarCategorias(pId, vUsuario);

                        DbParameter pcod_categoria = cmdTransaccionFactory.CreateParameter();
                        pcod_categoria.ParameterName = "p_cod_categoria";
                        pcod_categoria.Value = pCategorias.cod_categoria;
                        pcod_categoria.Direction = ParameterDirection.Input;
                        pcod_categoria.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_categoria);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CATEGORIAS_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CategoriasData", "EliminarCategorias", ex);
                    }
                }
            }
        }


        public Categorias ConsultarCategorias(string pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Categorias entidad = new Categorias();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Categorias WHERE COD_CATEGORIA = '" + pId.ToString() + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
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
                        BOExcepcion.Throw("CategoriasData", "ConsultarCategorias", ex);
                        return null;
                    }
                }
            }
        }


        public List<Categorias> ListarCategorias(Categorias pCategorias, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Categorias> lstCategorias = new List<Categorias>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Categorias " + ObtenerFiltro(pCategorias) + " ORDER BY COD_CATEGORIA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Categorias entidad = new Categorias();
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstCategorias.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCategorias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CategoriasData", "ListarCategorias", ex);
                        return null;
                    }
                }
            }
        }


    }
}
