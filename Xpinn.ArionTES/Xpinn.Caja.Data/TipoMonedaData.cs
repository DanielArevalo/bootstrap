using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Data
{

    /// <summary>
    /// Objeto de acceso a datos para la tabla TipoMoneda
    /// </summary>
    public class TipoMonedaData:GlobalData
    {
         protected ConnectionDataBase dbConnectionFactory;
        
        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public TipoMonedaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de TipoMonedas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoMonedas obtenidas</returns>
        public List<TipoMoneda> ListarTipoMoneda(TipoMoneda pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoMoneda> lstTipoMoneda = new List<TipoMoneda>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT * FROM TIPOMONEDA" + ObtenerFiltro(pEntidad) + " Order by cod_moneda asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoMoneda entidad = new TipoMoneda();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_moneda"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["cod_moneda"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion= Convert.ToString(resultado["descripcion"]);
                            lstTipoMoneda.Add(entidad);
                        }

                        return lstTipoMoneda;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoMonedaData", "ListarTipoMoneda", ex);
                        return null;
                    }
                }
            }
        }
    }
}
