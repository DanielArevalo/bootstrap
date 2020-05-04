using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Xpinn.Comun.Entities;
using Xpinn.Util;

namespace Xpinn.Comun.Data
{
    public class EliminaRegistroData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public EliminaRegistroData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public EliminarRegistro EliminarRegistro(EliminarRegistro eliminarRegistro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter Id_Tabla = cmdTransaccionFactory.CreateParameter();
                        Id_Tabla.ParameterName = "Id_Tabla";
                        Id_Tabla.Value = eliminarRegistro.IdConsecutivo;
                        Id_Tabla.Direction = ParameterDirection.Input;
                        Id_Tabla.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(Id_Tabla);

                        DbParameter Nombre_Tabla = cmdTransaccionFactory.CreateParameter();
                        Nombre_Tabla.ParameterName = "Nombre_Tabla";
                        Nombre_Tabla.Value = eliminarRegistro.NombreTabla;
                        Nombre_Tabla.Direction = ParameterDirection.Input;
                        Nombre_Tabla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(Nombre_Tabla);

                        DbParameter Id_Nombre_Tabla = cmdTransaccionFactory.CreateParameter();
                        Id_Nombre_Tabla.ParameterName = "Id_Nombre_Tabla";
                        Id_Nombre_Tabla.Value = eliminarRegistro.IdTablaRes;
                        Id_Nombre_Tabla.Direction = ParameterDirection.Input;
                        Id_Nombre_Tabla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(Id_Nombre_Tabla);

                        DbParameter pError = cmdTransaccionFactory.CreateParameter();
                        pError.ParameterName = "pError";
                        pError.Value = eliminarRegistro.Perror;
                        pError.Direction = ParameterDirection.InputOutput;
                        pError.Size = 500;
                        pError.DbType = DbType.StringFixedLength;
                        cmdTransaccionFactory.Parameters.Add(pError);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Usp_Xpinn_Eliminar_Registro";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (!string.IsNullOrEmpty(pError.Value.ToString()))
                            eliminarRegistro.Perror = pError.Value.ToString();
                        return eliminarRegistro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EliminaRegistroData", "EliminarRegistro", ex);
                        return null;
                    }
                }
            }
        }
    }
}
