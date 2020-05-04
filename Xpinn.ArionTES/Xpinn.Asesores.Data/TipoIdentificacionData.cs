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
    public class TipoIdentificacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public TipoIdentificacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<TipoIdentificacion> ListarTipoDocs(TipoIdentificacion pAseEntTipoDoc, Usuario pUsuario)
        {

            DbDataReader reader = default(DbDataReader);
            List<TipoIdentificacion> lstAseEntTipoDoc = new List<TipoIdentificacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string query = "SELECT * FROM TIPOIDENTIFICACION " + ObtenerFiltro(pAseEntTipoDoc);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            TipoIdentificacion aseEntTipoDoc = new TipoIdentificacion();

                            if (reader["CODTIPOIDENTIFICACION"] != DBNull.Value) aseEntTipoDoc.IdTipoIdentificacion = Convert.ToInt64(reader["CODTIPOIDENTIFICACION"].ToString());
                            if (reader["DESCRIPCION"] != DBNull.Value) aseEntTipoDoc.NombreTipoIdentificacion = reader["DESCRIPCION"].ToString();

                            lstAseEntTipoDoc.Add(aseEntTipoDoc);
                        }

                        return lstAseEntTipoDoc;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsesoresTipoDocData", "ListarTipoDocs", ex);
                        return null;
                    }
                }
            }
        }

        public TipoIdentificacion ConsultarTipoDocs(TipoIdentificacion pAseEntTipoDoc, Usuario pUsuario)
        {
            TipoIdentificacion aseEntTipoDoc = new TipoIdentificacion();
            DbDataReader reader = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string query = "SELECT * FROM TIPOIDENTIFICACION " + ObtenerFiltro(pAseEntTipoDoc);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        if (reader.Read())
                        {                            
                            if (reader["CODTIPOIDENTIFICACION"] != DBNull.Value) aseEntTipoDoc.IdTipoIdentificacion = Convert.ToInt64(reader["CODTIPOIDENTIFICACION"].ToString());
                            if (reader["DESCRIPCION"] != DBNull.Value) aseEntTipoDoc.NombreTipoIdentificacion = reader["DESCRIPCION"].ToString();
                        }

                        return aseEntTipoDoc;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsesoresTipoDocData", "ConsultarTipoDocs", ex);
                        return null;
                    }
                }
            }
        }
    }//end class
}//end NameSpace