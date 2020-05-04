using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Auxilios.Entities;

namespace Xpinn.Auxilios.Data
{   
    public class AprobacionAuxilioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public AprobacionAuxilioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public AprobacionAuxilio AprobarAuxilios(AprobacionAuxilio pAuxilio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Update auxilios set fecha_aprobacion = To_Date('" + pAuxilio.fecha_aprobacion.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha()
                            + "'), valor_aprobado = " + pAuxilio.valor_aprobado + ", estado = 'A' where numero_auxilio = " + pAuxilio.numero_auxilios;
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionAuxilioData", "AprobarAuxilios", ex);
                        return null;
                    }
                }
            }
        }
       
               
        
        public AprobacionAuxilio CrearControlAuxilio(AprobacionAuxilio pAuxilio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcontrolaux = cmdTransaccionFactory.CreateParameter();
                        pidcontrolaux.ParameterName = "p_idcontrolaux";
                        pidcontrolaux.Value = pAuxilio.idcontrolaux;
                        pidcontrolaux.Direction = ParameterDirection.Output;
                        pidcontrolaux.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidcontrolaux);

                        DbParameter pnumero_auxilios = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilios.ParameterName = "p_numero_auxilios";
                        pnumero_auxilios.Value = pAuxilio.numero_auxilios;
                        pnumero_auxilios.Direction = ParameterDirection.Input;
                        pnumero_auxilios.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilios);

                        DbParameter pcodtipo_proceso = cmdTransaccionFactory.CreateParameter();
                        pcodtipo_proceso.ParameterName = "p_codtipo_proceso";
                        pcodtipo_proceso.Value = pAuxilio.codtipo_proceso;
                        pcodtipo_proceso.Direction = ParameterDirection.Input;
                        pcodtipo_proceso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodtipo_proceso);

                        DbParameter pfecha_proceso = cmdTransaccionFactory.CreateParameter();
                        pfecha_proceso.ParameterName = "p_fecha_proceso";
                        pfecha_proceso.Value = pAuxilio.fecha_proceso;
                        pfecha_proceso.Direction = ParameterDirection.Input;
                        pfecha_proceso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_proceso);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        pcodusuario.Value = vUsuario.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pAuxilio.observaciones != null) pobservaciones.Value = pAuxilio.observaciones; else pobservaciones.Value = DBNull.Value;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_CONTROLAUX_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAuxilio.idcontrolaux = Convert.ToInt64(pidcontrolaux.Value);
                        return pAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionAuxilioData", "CrearControlAuxilio", ex);
                        return null;
                    }
                }
            }
        }
       

    }
}
