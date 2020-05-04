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
    /// Objeto de acceso a datos para la tabla TipoTasa
    /// </summary>
    public class TipoTasaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TipoTasa
        /// </summary>
        public TipoTasaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TIPOLIQUIDACION dados unos filtros
        /// </summary>
        /// <param name="pTIPOLIQUIDACION">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipo Liquidacion obtenidos</returns>
        public List<TipoTasa> ListarTipoTasa(TipoTasa pSolicitud, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoTasa> lstTipoLiq = new List<TipoTasa>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPOTASA  ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoTasa entidad = new TipoTasa();

                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.COD_TIPO_TASA= Convert.ToInt64(resultado["COD_TIPO_TASA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.NOMBRE = Convert.ToString(resultado["NOMBRE"]);

                            lstTipoLiq.Add(entidad);
                        }

                        return lstTipoLiq;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoTasaData", "ListarTipoTasa", ex);
                        return null;
                    }
                }
            }
        }

        public List<TipoTasa> ListarTipoHistorico(TipoTasa pSolicitud, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoTasa> lstTipoLiq = new List<TipoTasa>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT TIPO_HISTORICO AS COD_TIPO_TASA, DESCRIPCION AS NOMBRE FROM TIPOTASAHIST";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoTasa entidad = new TipoTasa();

                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.COD_TIPO_TASA = Convert.ToInt64(resultado["COD_TIPO_TASA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.NOMBRE = Convert.ToString(resultado["NOMBRE"]);

                            lstTipoLiq.Add(entidad);
                        }

                        return lstTipoLiq;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoTasaData", "ListarTipoHistorico", ex);
                        return null;
                    }
                }
            }
        }

        public double ConsultaTasaHistorica(Int64 pTipoHistorico, DateTime pFecha, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            double valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = "SELECT VALOR FROM HISTORICOTASA WHERE TIPO_HISTORICO = " + pTipoHistorico.ToString() + " AND TO_DATE('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') BETWEEN FECHA_INICIAL AND FECHA_FINAL";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToDouble(resultado["VALOR"]);
                        }

                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoTasaData", "ConsultaTasaHistorica", ex);
                        return valor;
                    }
                }
            }
        }

    }
}
