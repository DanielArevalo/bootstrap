using System;
using System.Configuration;
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
    /// Objeto de acceso a datos para la tabla Credito
    /// </summary>
    public class ProductoConsumoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Credito
        /// </summary>
        public ProductoConsumoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<ProductoConsumo> ListarProductoConsumo(string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProductoConsumo> lstProductos = new List<ProductoConsumo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                       // string sql = "Select * from  V_ACTA_APROBACIONCRED " + filtro;
                        string sql = "Select * from PRODUCTO_CONSUMO where ESTADO IS NOT NULL " + filtro +" ORDER BY 2 DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProductoConsumo entidad = new ProductoConsumo();

                            if (resultado["ID_PRODUCTO"] != DBNull.Value) entidad.id_producto = Convert.ToInt32(resultado["ID_PRODUCTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["ID_CAT_PRODUCTO"] != DBNull.Value) entidad.id_cat_producto = Convert.ToInt32(resultado["ID_CAT_PRODUCTO"]);

                            lstProductos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("productoConsumoData", "ListarProductoConsumo", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<CategoriaProducto> ListarCategoriaProductoConsumo(string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CategoriaProducto> lstCatProductos = new List<CategoriaProducto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // string sql = "Select * from  V_ACTA_APROBACIONCRED " + filtro;
                        string sql = "Select * from CATEGORIA_PRODUCTO where ESTADO IS NOT NULL " + filtro + " ORDER BY 2 DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CategoriaProducto entidad = new CategoriaProducto();

                            if (resultado["ID_CAT_PRODUCTO"] != DBNull.Value) entidad.id_cat_producto = Convert.ToInt32(resultado["ID_CAT_PRODUCTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);

                            lstCatProductos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCatProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("productoConsumoData", "ListarCategoriaProductoConsumo", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public AutorizaConsulta consultarAutorizaPendiente(int cod_persona, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            AutorizaConsulta autoriza = new AutorizaConsulta();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // string sql = "Select * from  V_ACTA_APROBACIONCRED " + filtro;
                        string sql = "Select * from AUTORIZA_CONSULTA where cod_persona = " + cod_persona +" and autoriza is null";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ID_AUTORIZA"] != DBNull.Value) autoriza.id_autoriza = Convert.ToInt32(resultado["ID_AUTORIZA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) autoriza.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) autoriza.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["AUTORIZA"] != DBNull.Value) autoriza.autoriza = Convert.ToInt32(resultado["AUTORIZA"]);
                            if (resultado["FECHA_AUTORIZA"] != DBNull.Value) autoriza.fecha_autoriza = Convert.ToDateTime(resultado["FECHA_AUTORIZA"]);
                        }
                        else
                        {
                            autoriza = null;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return autoriza;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("productoConsumoData", "consultarAutorizaPendiente", ex);
                        return null;
                    }
                }
            }
        }


        public AutorizaConsulta verificarAutorizacion(int cod_persona, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            AutorizaConsulta autoriza = new AutorizaConsulta();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // string sql = "Select * from  V_ACTA_APROBACIONCRED " + filtro;
                        string sql = "Select * from AUTORIZA_CONSULTA where cod_persona = " + cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ID_AUTORIZA"] != DBNull.Value) autoriza.id_autoriza = Convert.ToInt32(resultado["ID_AUTORIZA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) autoriza.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) autoriza.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["AUTORIZA"] != DBNull.Value) autoriza.autoriza = Convert.ToInt32(resultado["AUTORIZA"]);
                            if (resultado["FECHA_AUTORIZA"] != DBNull.Value) autoriza.fecha_autoriza = Convert.ToDateTime(resultado["FECHA_AUTORIZA"]);
                        }
                        else
                        {
                            autoriza = null;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return autoriza;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("productoConsumoData", "verificarAutorizacion", ex);
                        return null;
                    }
                }
            }
        }


        public bool CrearAutorizacion(AutorizaConsulta pEntidad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_IDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        P_IDENTIFICACION.ParameterName = "P_IDENTIFICACION";
                        P_IDENTIFICACION.Value = pEntidad.identificacion;
                        P_IDENTIFICACION.Direction = ParameterDirection.Input;
                        P_IDENTIFICACION.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_IDENTIFICACION);

                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = pEntidad.cod_persona;
                        P_COD_PERSONA.Direction = ParameterDirection.Input;
                        P_COD_PERSONA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_AUTORIZA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "CrearAutorizacion", ex);
                        return false;
                    }
                }
            }
        }


        public bool ModificarAutorizacion(AutorizaConsulta pEntidad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_ID = cmdTransaccionFactory.CreateParameter();
                        P_ID.ParameterName = "P_ID";
                        P_ID.Value = pEntidad.id_autoriza;
                        P_ID.Direction = ParameterDirection.Input;
                        P_ID.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ID);

                        DbParameter P_AUTORIZA = cmdTransaccionFactory.CreateParameter();
                        P_AUTORIZA.ParameterName = "P_AUTORIZA";
                        P_AUTORIZA.Value = pEntidad.autoriza;
                        P_AUTORIZA.Direction = ParameterDirection.Input;
                        P_AUTORIZA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_AUTORIZA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_AUTORIZA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "ModificarAutorizacion", ex);
                        return false;
                    }
                }
            }
        }


    }   
}