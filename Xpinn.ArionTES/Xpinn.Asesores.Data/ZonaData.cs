using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using System.Data.Common;
using System.Data;

namespace Xpinn.Asesores.Data
{
    public class ZonaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ZonaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }



        /// <summary>
        /// Método para listar las zonas que pertenecen a un barrio determinado
        /// </summary>
        /// <param name="barrio"></param>
        /// <returns></returns>
        public List<Barrios> ListarZonasBarrios(string barrio, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Barrios> lstAseEntbarrios = new List<Barrios>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                   
                        string query = "SELECT COD_ZONA AS CODCIUDAD, DESCRIPCION AS NOMCIUDAD FROM ZONAS ORDER BY COD_ZONA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Barrios aseEntTipoDoc = new Barrios();

                            if (reader["CODCIUDAD"] != DBNull.Value) aseEntTipoDoc.CODCIUDAD = Convert.ToInt64(reader["CODCIUDAD"].ToString());
                            if (reader["NOMCIUDAD"] != DBNull.Value) aseEntTipoDoc.NOMCIUDAD = reader["NOMCIUDAD"].ToString();

                            lstAseEntbarrios.Add(aseEntTipoDoc);
                        }

                        return lstAseEntbarrios;
                                      
                }
            }
        }
     




        public List<Zona> ListarZonas(Zona pAseEntiZona, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Zona> listAseEntiZonas = new List<Zona>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string query = "SELECT * FROM ASZONAS " + ObtenerFiltro(pAseEntiZona);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Zona aseEntiZonas = new Zona();

                            if (reader["ICODZONA"] != DBNull.Value) aseEntiZonas.IdZona = Convert.ToInt64(reader["ICODZONA"].ToString());
                            if (reader["ICODCIUDAD"] != DBNull.Value) aseEntiZonas.CodigoCiudad = reader["ICODCIUDAD"].ToString();
                            if (reader["SZONA"] != DBNull.Value) aseEntiZonas.NombreZona = reader["SZONA"].ToString();

                            listAseEntiZonas.Add(aseEntiZonas);
                        }
                        return listAseEntiZonas;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsesoresZonaData", "ListarZonas", ex);
                        return null;
                    }
                }
            }
        }
    }
}