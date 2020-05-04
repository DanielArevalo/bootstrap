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
    public class EnvioDeCorreoExtractoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public EnvioDeCorreoExtractoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de Lineas de credito
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Lineas de credito obtenidos</returns>
        public List<EnvioDeCorreoExtracto> ListarEnvioDeCorreoExtractos(EnvioDeCorreoExtracto entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EnvioDeCorreoExtracto> lstEnvioDeCorreoExtractos = new List<EnvioDeCorreoExtracto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    //connection.Open();
                    //cmdTransaccionFactory.Connection = connection;
                    //cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                    //cmdTransaccionFactory.CommandText = "XPF_AS_DATOSENVIOCORREO";
                    
                    //DbParameter p_DATA = new OracleParameter("p_data", OracleType.Cursor);
                    //p_DATA.Direction = ParameterDirection.Output;
                    //cmdTransaccionFactory.Parameters.Add(p_DATA);
                    //cmdTransaccionFactory.Parameters.Add(new OracleParameter("p_data", OracleType.Cursor)).Direction = ParameterDirection.Output;


                    string sql = " Select 'xpinnn@gmail.com' as NombreCorreoOrigen, 'smtp.gmail.com' as ServidorSMTP, 587  as PuertoDeServidorSMTP, 1 as UsarSSL, 'xpinnn@gmail.com' as Usuario, 'xpin2012' as Clave, "
                                  +"'Correo con Extracto' as TextoDelAsunto, 'Esto es una prueba de envio de correo electrónico automático' as TextoDelMensaje from dual";

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    while (resultado.Read())
                    {
                        entidad = new EnvioDeCorreoExtracto();
                        //Asociar todos los valores a la entidad
                        if (resultado["NombreCorreoOrigen"] != DBNull.Value) entidad.NombreCorreoOrigen = Convert.ToString(resultado["NombreCorreoOrigen"]);
                        if (resultado["ServidorSMTP"] != DBNull.Value) entidad.ServidorSMTP = Convert.ToString(resultado["ServidorSMTP"]);
                        if (resultado["PuertoDeServidorSMTP"] != DBNull.Value) entidad.PuertoDeServidorSMTP = Convert.ToUInt16(resultado["PuertoDeServidorSMTP"]);
                        if (resultado["UsarSSL"] != DBNull.Value) entidad.UsarSSL = Convert.ToBoolean(resultado["UsarSSL"]);
                        if (resultado["Usuario"] != DBNull.Value) entidad.Usuario = Convert.ToString(resultado["Usuario"]);
                        if (resultado["Clave"] != DBNull.Value) entidad.Clave = Convert.ToString(resultado["Clave"]);
                        if (resultado["TextoDelAsunto"] != DBNull.Value) entidad.TextoDelAsunto = Convert.ToString(resultado["TextoDelAsunto"]);
                        if (resultado["TextoDelMensaje"] != DBNull.Value) entidad.TextoDelMensaje = Convert.ToString(resultado["TextoDelMensaje"]);
                        lstEnvioDeCorreoExtractos.Add(entidad);
                    }

                    return lstEnvioDeCorreoExtractos;
                    //}
                    //catch (Exception ex)
                    //{
                    //    BOExcepcion.Throw("EnvioDeCorreoExtractoData", "ListarEnvioDeCorreoExtractos", ex);
                    //    return null;
                    //}
                }
            }
        }

    }
}
