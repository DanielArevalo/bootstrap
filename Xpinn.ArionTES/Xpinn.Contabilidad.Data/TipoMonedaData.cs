using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para PlanCuentas
    /// </summary>    
    public class TipoMonedaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public TipoMonedaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        

        /// <summary>
        /// Método para consultar el TipoMoneda
        /// </summary>       
        /// <param name="pUsuario"></param>     
        /// <returns></returns>
        public List<TipoMoneda> ListarTipoMoneda(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoMoneda> lstTipoMoneda = new List<TipoMoneda>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                         Configuracion conf = new Configuracion();
                         string sql = "select * from tipomoneda";
                        
                       
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoMoneda entidad = new TipoMoneda();

                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.tipo_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

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
