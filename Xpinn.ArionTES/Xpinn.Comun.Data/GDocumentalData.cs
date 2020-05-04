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
    public class GDocumentalData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ConceptoS
        /// </summary>
        public GDocumentalData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        
        /// <summary>
        /// Obtiene una lista de cierres
        /// </summary>
        /// <param name="pConceptoS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de cierres obtenidos</returns>
        public List<GestionDocumental> ListarGDocumental(GestionDocumental  pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<GestionDocumental> lstGestionDocumental = new List<GestionDocumental>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  GD_TIPOSDOCUMENTOS " + ObtenerFiltro(pTipo);

                          connection.Open();  
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            GestionDocumental  entidad = new GestionDocumental();

                            if (resultado["IdTipo"] != DBNull.Value) entidad.IdTipo = Convert.ToInt32(resultado["IdTipo"]);
                            if (resultado["Descripcion"] != DBNull.Value) entidad.Descripcion  = Convert.ToString(resultado["Descripcion"]);
                            if (resultado["NombreTabla"] != DBNull.Value) entidad.NombreTabla  = Convert.ToString(resultado["NombreTabla"]);
                            if (resultado["CampoTabla"] != DBNull.Value) entidad.CampoTabla  = Convert.ToString(resultado["CampoTabla"]);


                            lstGestionDocumental.Add(entidad);
                        }

                        return lstGestionDocumental;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiereaData", "ListarCierea", ex);
                        return null;
                    }
                }
            }
        }

        



    }
}
