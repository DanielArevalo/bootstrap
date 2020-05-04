using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class MotivoAfiliacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public MotivoAfiliacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<MotivoAfiliacion> ListarMotivoAfiliacion(MotivoAfiliacion pEntMotAfilia, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<MotivoAfiliacion> lstEntMotAfilia = new List<MotivoAfiliacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string query = "SELECT * FROM asmotafiliacion " + ObtenerFiltro(pEntMotAfilia);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            MotivoAfiliacion aseEntMotAfilia = new MotivoAfiliacion();

                            if (reader["icodmotivo"] != DBNull.Value)        aseEntMotAfilia.IdMotivoAfiliacion = Convert.ToInt64(reader["icodmotivo"].ToString());
                            if (reader["smotivoafiliacion"] != DBNull.Value) aseEntMotAfilia.Observaciones      = reader["smotivoafiliacion"].ToString();

                            lstEntMotAfilia.Add(aseEntMotAfilia);
                        }
                        return lstEntMotAfilia;
                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("DataMotivoAfiliacion", "ListarMotivoAfiliacion", ex);
                        return null;
                    }
                }
            }
        }
    }
}