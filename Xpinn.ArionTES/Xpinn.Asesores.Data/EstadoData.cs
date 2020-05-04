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
    public class EstadoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public EstadoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Estado> ListarEstado(Estado pEntEstado, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Estado> lstEntEstado = new List<Estado>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                      string query = "SELECT * FROM asestado ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Estado entityEstado = new Estado();

                            if (reader["iestado"] != DBNull.Value)      entityEstado.IdEstado = Convert.ToInt64(reader["iestado"].ToString());
                            if (reader["descripcion"] != DBNull.Value)  entityEstado.Descripcion = reader["descripcion"].ToString();

                            lstEntEstado.Add(entityEstado);
                        }
                        return lstEntEstado;
                    
                }
            }
        }
    }
}