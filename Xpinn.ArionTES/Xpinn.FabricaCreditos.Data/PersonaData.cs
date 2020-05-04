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
    public class PersonaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public PersonaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de Lineas de credito
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Lineas de credito obtenidos</returns>
        public List<Persona> ListarPersonas(Persona entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona> lstPersonas = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select codusuario as codigo, codusuario   || ' - ' ||  nombre as nombre from usuarios";                                                                                                       
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new Persona();
                            //Asociar todos los valores a la entidad
                            if (resultado["codigo"] != DBNull.Value) entidad.CodigoUsuario = Convert.ToInt32(resultado["codigo"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["nombre"]);
                            lstPersonas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersonas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaData", "ListarPersonas", ex);
                        return null;
                    }
                }
            }
        }
    }
}