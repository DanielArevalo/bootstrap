using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    public class TipoGarantiaData : GlobalData 
    {  
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public TipoGarantiaData()
        {
           dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de Tipos de Garantias
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipos de Grantias obtenidos</returns>
        public List<TipoGarantias> ListarTipoGarantia(TipoGarantias entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoGarantias> lstTipoGarantia = new List<TipoGarantias>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select cod_garantia as codigo, Nombre from TIPOS_GARANTIA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new TipoGarantias();
                            //Asociar todos los valores a la entidad
                            if (resultado["codigo"] != DBNull.Value) entidad.Codigo = Convert.ToInt64(resultado["Codigo"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.Nombre= Convert.ToString(resultado["Nombre"]);
                            lstTipoGarantia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoGarantia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoGarantiaData", "ListarTipoGarantia", ex);
                        return null;
                    }
                }
            }
        }
    }
}