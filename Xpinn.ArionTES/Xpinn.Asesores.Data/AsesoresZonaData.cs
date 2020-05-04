using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Asesores.Entities;
using Xpinn.Comun.Entities;
using System.Data.Common;
using System.Data;

namespace Xpinn.Asesores.Data
{
    public class AsesoresZonaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public AsesoresZonaData() {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<Zona> ListarZonas(Zona pAseEntiZona, Usuario pUsuario) {
            DbDataReader reader = default(DbDataReader);
            List<Zona> listAseEntiZonas = new List<Zona>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using(DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand()){
                    try{
                        string query = "SELECT * FROM ASZONAS " + ObtenerFiltro(pAseEntiZona);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while(reader.Read()){
                            Zona aseEntiZonas = new Zona();

                            if (reader["ICODZONA"] != DBNull.Value) aseEntiZonas.IdZona = Convert.ToInt64(reader["ICODZONA"].ToString());
                            if (reader["ICODCIUDAD"] != DBNull.Value) aseEntiZonas.CodigoCiudad = reader["ICODCIUDAD"].ToString();
                            if (reader["SZONA"] != DBNull.Value) aseEntiZonas.Descripcion = reader["SZONA"].ToString();
                            
                            listAseEntiZonas.Add(aseEntiZonas);
                         }
                        return listAseEntiZonas;
                    
                    }catch(DbException ex){
                        BOExcepcion.Throw("AsesoresZonaData", "ListarZonas", ex);
                        return null;
                    }
                }
            }
        }
    }
}
