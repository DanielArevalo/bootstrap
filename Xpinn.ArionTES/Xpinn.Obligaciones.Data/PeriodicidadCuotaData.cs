using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Obligaciones.Entities;

namespace Xpinn.Obligaciones.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla PeriodicidadCuota
    /// </summary>
    public class PeriodicidadCuotaData : GlobalData
    {
          protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PeriodicidadCuota
        /// </summary>
        public PeriodicidadCuotaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TIPOLIQUIDACION dados unos filtros
        /// </summary>
        /// <param name="pTIPOLIQUIDACION">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipo Liquidacion obtenidos</returns>
        public List<PeriodicidadCuota> ListarPeriodicidadCuota(PeriodicidadCuota pSolicitud, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PeriodicidadCuota> lstTipoLiq = new List<PeriodicidadCuota>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PERIODICIDAD  ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PeriodicidadCuota entidad = new PeriodicidadCuota();

                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.COD_PERIODICIDAD = Convert.ToInt64(resultado["COD_PERIODICIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.DESCRIPCION = Convert.ToString(resultado["DESCRIPCION"]);

                            lstTipoLiq.Add(entidad);
                        }

                        return lstTipoLiq;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PeriodicidadCuotaData", "ListarPeriodicidadCuota", ex);
                        return null;
                    }
                }
            }
        }
    }
}
