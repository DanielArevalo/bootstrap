using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Integracion.Entities;
 
namespace Xpinn.Integracion.Data
{
    public class IntegracionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public IntegracionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Entities.Integracion ObtenerIntegracion(int id, Usuario pUsuario)
        {
            DbDataReader resultado;
            Entities.Integracion entidad = new Entities.Integracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from integracion where id = "+id+" and estado = 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["ID"] != DBNull.Value) entidad.id = Convert.ToInt32(resultado["ID"]);
                            if (resultado["ENTIDAD"] != DBNull.Value) entidad.entidad = Convert.ToString(resultado["ENTIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["USUARIO"] != DBNull.Value) entidad.usuario = Convert.ToString(resultado["USUARIO"]);
                            if (resultado["PASSWORD"] != DBNull.Value) entidad.password = Convert.ToString(resultado["PASSWORD"]);
                            if (resultado["DATOS"] != DBNull.Value) entidad.datos = Convert.ToString(resultado["DATOS"]);
                        }                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IntegracionData", "ObtenerIntegracion", ex);
                        return null;
                    }
                }
            }
        }


    }
}
