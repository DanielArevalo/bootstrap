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
    public class ActividadData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ActividadData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Xpinn.Asesores.Entities.Common.Actividad> ListarActividades(Xpinn.Asesores.Entities.Common.Actividad pAseEntiAct, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Xpinn.Asesores.Entities.Common.Actividad> listAseEntiAct = new List<Xpinn.Asesores.Entities.Common.Actividad>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string query = "SELECT * FROM ACTIVIDAD ";// +ObtenerFiltro(pAseEntiAct);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Xpinn.Asesores.Entities.Common.Actividad aseEntiAct = new Xpinn.Asesores.Entities.Common.Actividad();

                            if (reader["CODACTIVIDAD"] != DBNull.Value) aseEntiAct.IdActividad = Convert.ToInt64(reader["CODACTIVIDAD"].ToString());
                            if (reader["DESCRIPCION"] != DBNull.Value) aseEntiAct.NombreActividad = reader["DESCRIPCION"].ToString();

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