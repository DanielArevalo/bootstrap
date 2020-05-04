using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;


namespace Xpinn.Riesgo.Data
{
    public class ActDatosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ActDatosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<ActDatos> ListarActDatos(string Fechaini,string Fechafin,ActDatos pActDatos, Usuario usuario)
        {
            DbDataReader resultado;
            List<ActDatos> listaActDatos = new List<ActDatos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql;
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        {
                            sql = @"SELECT MAX(PERSONA_ACT_DATOS.ID_UPDATE) AS ID_UPDATE, PERSONA.IDENTIFICACION, PERSONA.PRIMER_NOMBRE, PERSONA.SEGUNDO_NOMBRE, PERSONA.PRIMER_APELLIDO, MAX(PERSONA_ACT_DATOS.FECHA_ACT) AS FECHA_ACT 
                                        FROM  PERSONA_ACT_DATOS INNER JOIN PERSONA ON PERSONA_ACT_DATOS.COD_PERSONA = PERSONA.COD_PERSONA 
                                        WHERE FECHA_ACT >= TO_DATE('" + String.Format("{0:M/d/yyyy}", Fechaini) + "','YYYY-MM-DD') AND FECHA_ACT <= TO_DATE('" + String.Format("{0:M/d/yyyy}", Fechafin) + "','YYYY-MM-DD') " + ObtenerFiltro2(pActDatos) + @"                                         
                                        GROUP BY PERSONA.IDENTIFICACION, PERSONA.PRIMER_NOMBRE, PERSONA.SEGUNDO_NOMBRE, PERSONA.PRIMER_APELLIDO
                                        ORDER BY 6 ";
                        }
                        else
                        {
                            sql = @"SELECT MAX(PERSONA_ACT_DATOS.ID_UPDATE) AS ID_UPDATE, PERSONA.IDENTIFICACION, PERSONA.PRIMER_NOMBRE, PERSONA.SEGUNDO_NOMBRE, PERSONA.PRIMER_APELLIDO, MAX(PERSONA_ACT_DATOS.FECHA_ACT) AS FECHA_ACT 
                                        FROM  PERSONA_ACT_DATOS INNER JOIN PERSONA ON PERSONA_ACT_DATOS.COD_PERSONA = PERSONA.COD_PERSONA 
                                        WHERE FECHA_ACT >= '" + String.Format("{0:M/d/yyyy}", Fechaini) + "' AND FECHA_ACT <= '" + String.Format("{0:M/d/yyyy}", Fechafin) + "' " + ObtenerFiltro2(pActDatos) + @" 
                                        GROUP BY PERSONA.IDENTIFICACION, PERSONA.PRIMER_NOMBRE, PERSONA.SEGUNDO_NOMBRE, PERSONA.PRIMER_APELLIDO
                                        ORDER BY 6";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ActDatos entidad = new ActDatos();
                            if (resultado["ID_UPDATE"] != DBNull.Value) entidad.Id_update = Convert.ToInt64(resultado["ID_UPDATE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.Primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.Segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.Primer_Apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["FECHA_ACT"] != DBNull.Value) entidad.Fecha_act = Convert.ToString(resultado["FECHA_ACT"]);
                            listaActDatos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaActDatos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActDatosData", "ListarActDatos", ex);
                        return null;
                    }
                }
            }
        }

        public List<ActDatos> ListarActDatosNoActualizado(ActDatos pActDatos, Usuario usuario)
        {
            DbDataReader resultado;
            List<ActDatos> listaActDatos = new List<ActDatos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Persona.COD_PERSONA as ID_UPDATE,Persona.IDENTIFICACION,Persona.PRIMER_NOMBRE,Persona.SEGUNDO_NOMBRE,Persona.PRIMER_APELLIDO, null as FECHA_ACT FROM Persona LEFT JOIN persona_afiliacion ON Persona.COD_PERSONA = persona_afiliacion.COD_PERSONA  WHERE persona_afiliacion.COD_PERSONA NOT IN (Select COD_PERSONA from PERSONA_ACT_DATOS WHERE  persona_afiliacion.COD_PERSONA = PERSONA_ACT_DATOS.COD_PERSONA AND  PERSONA_ACT_DATOS.FECHA_ACT <= ADD_MONTHS(sysdate , -6))AND persona_afiliacion.ESTADO <> 'R'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ActDatos entidad = new ActDatos();
                            if (resultado["ID_UPDATE"] != DBNull.Value) entidad.Id_update = Convert.ToInt64(resultado["ID_UPDATE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.Primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.Segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.Primer_Apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["FECHA_ACT"] != DBNull.Value) entidad.Fecha_act = Convert.ToString(resultado["FECHA_ACT"]);
                            listaActDatos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaActDatos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActDatosData", "ListarActDatosNoActualizado", ex);
                        return null;
                    }
                }
            }
        }
    }
}
