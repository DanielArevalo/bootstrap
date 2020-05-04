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
    public class TomadorPolizasData : GlobalData
    {

    protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
    public TomadorPolizasData()
        {
           dbConnectionFactory = new ConnectionDataBase();
        }


        /// <summary>
        /// Obtiene un registro de la tabla Personas  de la base de datos relacionado
        /// TomadorSeguro, Fundacion
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Entidad consultado</returns>
        public TomadorPoliza ConsultarDatosTomadorPoliza(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            TomadorPoliza entidad = new TomadorPoliza();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT identificacion,razon_social FROM persona where identificacion = '900534833'";
                        connection.Open();                       
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToInt64(resultado["identificacion"]);
                            if (resultado["razon_social"] != DBNull.Value) entidad.razonsocial = Convert.ToString(resultado["razon_social"]);                          
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListarTomadorPolizaData", "ConsultaTomadorPoliza", ex);
                        return null;
                    }
                }
            }
        }
    }
}
