using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Data
{
    public class AnulacionCDATData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public AnulacionCDATData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }



        public Cdat ModificarCDATAnulacion(Cdat pCdat, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_Codigo_Cdat";
                        pcodigo_cdat.Value = pCdat.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);
                       
                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_Estado";
                        pestado.Value = pCdat.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pobservacion = cmdTransaccionFactory.CreateParameter();
                        pobservacion.ParameterName = "p_Observacion";
                        if (pCdat.observacion != null) pobservacion.Value = pCdat.observacion; else pobservacion.Value = DBNull.Value;
                        pobservacion.Direction = ParameterDirection.Input;
                        pobservacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_ANULACION_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AnulacionCDATData", "ModificarCDATAnulacion", ex);
                        return null;
                    }
                }
            }
        }


        
    }
}
