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
    public class MotivoModificacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public MotivoModificacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<MotivoModificacion> ListarMotivoModificacion(MotivoModificacion pEntMotModif, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<MotivoModificacion> lstEntMotModif = new List<MotivoModificacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string query = "SELECT * FROM asmotmodificacion " + ObtenerFiltro(pEntMotModif);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            MotivoModificacion aseEntMotModif = new MotivoModificacion();

                            if (reader["icodmotivo"] != DBNull.Value) aseEntMotModif.IdMotivoModificacion     = Convert.ToInt64(reader["icodmotivo"].ToString());
                            if (reader["smotivo"] != DBNull.Value)    aseEntMotModif.NombreMotivoModificacion = reader["smotivo"].ToString();

                            lstEntMotModif.Add(aseEntMotModif);
                        }
                        return lstEntMotModif;
                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("DataMotivoModificacion", "ListarMotivoAfiliacion", ex);
                        return null;
                    }
                }
            }
        }
    }
}
