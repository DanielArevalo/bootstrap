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
    public class DatosAprobadorData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public DatosAprobadorData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la informacion de los creditos solicitados
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Datos del credito solicitado</returns>
        public List<DatosAprobador> ListarDatosAprobador(DatosAprobador pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosAprobador> lstAprobador = new List<DatosAprobador>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //Se elimina del select el string  "'NIVEL - ' " para no generar error en la conversion a entero
                        string sql = @"Select c.nivel as nivel, p.nombre as nombres, c.observaciones, t.descripcion 
                                        From controlcreditos c Left Join usuarios p On c.cod_persona = p.codusuario Left join tipoprocesos t on T.Codtipoproceso = C.Codtipoproceso
                                        Where c.numero_radicacion = " + pEntidad.Radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            pEntidad = new DatosAprobador();
                            //Asociar todos los valores a la entidad
                            if (resultado["nivel"] != DBNull.Value) pEntidad.Nivel = Convert.ToInt32(resultado["nivel"]);
                            if (resultado["nombres"] != DBNull.Value) pEntidad.Nombres = Convert.ToString(resultado["nombres"]);
                            if (resultado["observaciones"] != DBNull.Value) pEntidad.Observaciones = Convert.ToString(resultado["observaciones"]);
                            if (resultado["Descripcion"] != DBNull.Value) pEntidad.EstadoDescripcion = Convert.ToString(resultado["Descripcion"]);
                            lstAprobador.Add(pEntidad);
                        }

                        return lstAprobador;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosAprobadorData", "ListarDatosAprobador", ex);
                        return null;
                    }
                }
            }
        }
    }
}
