using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class TiposDeFiltroData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public TiposDeFiltroData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de Lineas de credito
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Lineas de credito obtenidos</returns>
        public List<TiposDeFiltro> ListarTiposDeFiltros(TiposDeFiltro entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TiposDeFiltro> lstTiposDeFiltros = new List<TiposDeFiltro>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select 'p.identificacion like' as SQLWhere, 'Identificación' as nombre from dual union
                                       select 'p.nombre like', 'Nombre Cliente' from dual union select 'c.numero_radicacion like', 'Número de Crédito' from dual";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new TiposDeFiltro();
                            //Asociar todos los valores a la entidad
                            if (resultado["SQLWhere"] != DBNull.Value) entidad.SQLWhere = Convert.ToString(resultado["SQLWhere"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["nombre"]);
                            lstTiposDeFiltros.Add(entidad);
                        }

                        return lstTiposDeFiltros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDeFiltroData", "ListarTiposDeFiltros", ex);
                        return null;
                    }
                }
            }
        }
    }
}