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
    /// Objeto de acceso a datos para la tabla CentroCosto
    /// </summary>
    public class CentroCostoData:GlobalData 
    {
        protected ConnectionDataBase dbConnectionFactory;
        
        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public CentroCostoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de Centro de Costos dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Centro de Costos obtenidas</returns>
        public List<CentroCosto> ListarCentroCosto(CentroCosto pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CentroCosto> lstCentroCosto = new List<CentroCosto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT * FROM CENTRO_COSTO " + ObtenerFiltro(pEntidad) + " Order By centro_costo asc"; 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CentroCosto entidad = new CentroCosto();
                            //Asociar todos los valores a la entidad
                            if (resultado["centro_costo"] != DBNull.Value) entidad.centro_costo= Convert.ToInt64(resultado["centro_costo"]);
                            if (resultado["nom_centro"] != DBNull.Value) entidad.nom_centro  = resultado["nom_centro"].ToString();
                            lstCentroCosto.Add(entidad);
                        }

                        return lstCentroCosto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CentroCostoData", "ListarCentroCosto", ex);
                        return null;
                    }
                }
            }
        }
    }
}
