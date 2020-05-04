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
    public class AsesoresActividadData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public AsesoresActividadData(){
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Xpinn.Comun.Entities.Actividad> ListarActividades(Xpinn.Comun.Entities.Actividad pAseEntiAct, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Xpinn.Comun.Entities.Actividad> listAseEntiAct = new List<Xpinn.Comun.Entities.Actividad>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string query = "SELECT * FROM ACTIVIDAD " + ObtenerFiltro(pAseEntiAct);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Xpinn.Comun.Entities.Actividad aseEntiAct = new Xpinn.Comun.Entities.Actividad();

                            if (reader["CODACTIVIDAD"] != DBNull.Value) aseEntiAct.IdActividad = Convert.ToInt64(reader["CODACTIVIDAD"].ToString());
                            if (reader["DESCRIPCION"] != DBNull.Value) aseEntiAct.Descripcion = reader["DESCRIPCION"].ToString();

                            listAseEntiAct.Add(aseEntiAct);
                        }
                        return listAseEntiAct;

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
