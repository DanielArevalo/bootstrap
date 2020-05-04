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
    public class AsesoresTipoIdentificacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public AsesoresTipoIdentificacionData(){
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Xpinn.Comun.Entities.TipoIdentificacion> ListarTipoDocs(Xpinn.Comun.Entities.TipoIdentificacion pAseEntTipoDoc, Usuario pUsuario){
            
            DbDataReader reader = default(DbDataReader);
            List<Xpinn.Comun.Entities.TipoIdentificacion> lstAseEntTipoDoc = new List<Xpinn.Comun.Entities.TipoIdentificacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using(DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand()){
                    try{
                        string query = "SELECT * FROM TIPOIDENTIFICACION " + ObtenerFiltro(pAseEntTipoDoc);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while(reader.Read()){
                            Xpinn.Comun.Entities.TipoIdentificacion aseEntTipoDoc = new Xpinn.Comun.Entities.TipoIdentificacion();

                            if (reader["CODTIPOIDENTIFICACION"] != DBNull.Value) aseEntTipoDoc.IdTipoIdentificacion = Convert.ToInt64(reader["CODTIPOIDENTIFICACION"].ToString());
                            if (reader["DESCRIPCION"] != DBNull.Value) aseEntTipoDoc.Descripcion = reader["DESCRIPCION"].ToString();
                            
                            lstAseEntTipoDoc.Add(aseEntTipoDoc);
                        }

                        return lstAseEntTipoDoc;

                    }catch(DbException ex){
                        BOExcepcion.Throw("AsesoresTipoDocData", "ListarTipoDocs", ex);
                        return null;
                    }
                }
            }
        
        }

    }//end class
}//end NameSpace
