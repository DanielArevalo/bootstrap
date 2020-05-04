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
    /// Objeto de acceso a datos para la tabla DiaSemana
    /// </summary>
    public class DiaSemanaData:GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        
        /// <summary>
        /// Constructor del objeto de acceso a datos para DiaSemana
        /// </summary>
        public DiaSemanaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de DiaSemanas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de DiaSemanas obtenidas</returns>
        public List<DiaSemana> ListarDiaSemana(DiaSemana pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DiaSemana> lstDiaSemana = new List<DiaSemana>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT * FROM DIASSEMANA" + ObtenerFiltro(pEntidad) + " order by cod_diasemana asc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DiaSemana entidad = new DiaSemana();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_diasemana"] != DBNull.Value) entidad.cod_diasemana  = Convert.ToInt64(resultado["cod_diasemana"]);
                            if (resultado["nom_diasemana"] != DBNull.Value) entidad.nom_diasemana = Convert.ToString(resultado["nom_diasemana"]);
                            lstDiaSemana.Add(entidad);
                        }

                        return lstDiaSemana;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiaSemanaData", "ListarDiaSemana", ex);
                        return null;
                    }
                }
            }
        }
    }
}
