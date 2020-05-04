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
    /// Objeto de acceso a datos para la tabla PagoExtraord
    /// </summary>
    public class PagoExtraordData: GlobalData
    {
         protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PagoExtraord
        /// </summary>
        public PagoExtraordData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla OBCOMPONENTESADICIONAL dados unos filtros
        /// </summary>
        /// <param name="pPagoExtraord">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PagoExtraord obtenidos</returns>
        public List<PagoExtraord> ListarPagoExtraord(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PagoExtraord> lstPagoExtraord = new List<PagoExtraord>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT IDCUOTAEXTRA,CODOBLIGACION,(select y.descripcion from periodicidad y where y.cod_periodicidad=x.cod_periodicidad) NOMPERIODICIDAD, " +
                                     " x.VALOR FROM  OBCUOTAEXTRA x Where x.codobligacion= " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PagoExtraord entidad = new PagoExtraord();

                            if (resultado["IDCUOTAEXTRA"] != DBNull.Value) entidad.IDOBCUOTAEXTRA = Convert.ToInt64(resultado["IDCUOTAEXTRA"]);
                            if (resultado["CODOBLIGACION"] != DBNull.Value) entidad.CODOBLIGACION= Convert.ToInt64(resultado["CODOBLIGACION"]);
                            if (resultado["NOMPERIODICIDAD"] != DBNull.Value) entidad.NOM_PERIODICIDAD = Convert.ToString(resultado["NOMPERIODICIDAD"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.VALOR = Convert.ToInt64(resultado["VALOR"]);

                            lstPagoExtraord.Add(entidad);
                        }

                        return lstPagoExtraord;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagoExtraordData", "ListarPagoExtraord", ex);
                        return null;
                    }
                }
            }
        }
    }
}
