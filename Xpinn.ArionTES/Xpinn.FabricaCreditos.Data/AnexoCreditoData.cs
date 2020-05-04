using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using System.Web;

namespace Xpinn.FabricaCreditos.Data
{
    public class AnexoCreditoData : GlobalData 
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public AnexoCreditoData()
        {
           dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de anexos
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<AnexoCredito> ListarAnexos(AnexoCredito pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AnexoCredito> lstAnexos = new List<AnexoCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select iddocumento as id, descripcion from documentosanexos where numero_radicacion="+pEntidad.Radicacion;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            pEntidad = new AnexoCredito();
                            //Asociar todos los valores a la entidad
                            if (resultado["id"] != DBNull.Value) pEntidad.Id = Convert.ToInt32(resultado["id"]);
                            if (resultado["descripcion"] != DBNull.Value) pEntidad.Descripcion = Convert.ToString(resultado["descripcion"]);
                            lstAnexos.Add(pEntidad);
                        }

                        return lstAnexos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AnexoCreditorData", "ListarAnexos", ex);
                        return null;
                    }
                }
            }
        }
    }
}
