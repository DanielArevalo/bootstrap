using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla tipoProducto
    /// </summary>
    public class TipoProductoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla LineaAporteS
        /// </summary>
        public TipoProductoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

      

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla LineaAporteS dados unos filtros
        /// </summary>
        /// <param name="pLineaAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de LineaAporte obtenidos</returns>
        public List<TipoProducto> ListarTipoProducto(TipoProducto pTipoProducto, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoProducto> lstTipoProducto = new List<TipoProducto>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM tipoproducto " + ObtenerFiltro(pTipoProducto) + " ORDER BY COD_TIPO_PRODUCTO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoProducto entidad = new TipoProducto();
                            if (resultado["COD_TIPO_PRODUCTO"] != DBNull.Value) entidad.cod_tipo_prod = Convert.ToInt32(resultado["COD_TIPO_PRODUCTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTipoProducto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoProductoData", "ListarTipoProducto", ex);
                        return null;
                    }
                }
            }
        }
        public List<TipoProducto> ListarTipoTran(TipoProducto pTipoProducto, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoProducto> lstTipoProducto = new List<TipoProducto>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM tipo_tran where tipo_producto= "+pTipoProducto.cod_tipo_prod+ " order by 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoProducto entidad = new TipoProducto();
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["TIPO_TRAN"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTipoProducto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoProductoData", "ListarTipoTran", ex);
                        return null;
                    }
                }
            }
        }

    }
}