using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Comun.Entities;

namespace Xpinn.Comun.Data
{
    public class GeneralData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla General
        /// </summary>
        public GeneralData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene datos de la tabla general
        /// </summary>
        /// <param name="pGeneral">Entidad con los filtros solicitados</param>
        /// <returns>Datos generales</returns>
        public General ConsultarGeneral(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;

            General entidad = new General
            {
                descripcion = string.Empty,
                valor = string.Empty
            };

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM General WHERE CODIGO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt32(resultado["CODIGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]).Trim();
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]).Trim();
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GeneralData", "ConsultarGeneral", ex);
                        return null;
                    }
                }
            }
        }

        public Int64 SMLVGeneral(Usuario vUsuario)
        {
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    Int64 valor = 0;
                    try
                    {
                        string sql = @"Select to_number(valor) as VALOR from general where codigo = 10";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                     if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToInt64(resultado["VALOR"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GeneralData", "ConsultarGeneral", ex);
                        return 0;
                    }
                }
            }
        }

    }
}
