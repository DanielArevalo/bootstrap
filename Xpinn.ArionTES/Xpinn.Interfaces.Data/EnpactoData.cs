using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Interfaces.Entities;
 
namespace Xpinn.Interfaces.Data
{
    public class EnpactoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public EnpactoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Enpacto_Aud CrearEnpacto_Aud(Enpacto_Aud pEnpacto_Aud, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        if (pEnpacto_Aud.consecutivo == null)
                            pconsecutivo.Value = DBNull.Value;
                        else
                            pconsecutivo.Value = pEnpacto_Aud.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pexitoso = cmdTransaccionFactory.CreateParameter();
                        pexitoso.ParameterName = "p_exitoso";
                        if (pEnpacto_Aud.exitoso == null)
                            pexitoso.Value = DBNull.Value;
                        else
                            pexitoso.Value = pEnpacto_Aud.exitoso;
                        pexitoso.Direction = ParameterDirection.Input;
                        pexitoso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pexitoso);

                        DbParameter ptipooperacion = cmdTransaccionFactory.CreateParameter();
                        ptipooperacion.ParameterName = "p_tipooperacion";
                        if (pEnpacto_Aud.tipooperacion == null)
                            ptipooperacion.Value = DBNull.Value;
                        else
                            ptipooperacion.Value = pEnpacto_Aud.tipooperacion;
                        ptipooperacion.Direction = ParameterDirection.Input;
                        ptipooperacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipooperacion);

                        DbParameter pjsonentidadpeticion = cmdTransaccionFactory.CreateParameter();
                        pjsonentidadpeticion.ParameterName = "p_jsonentidadpeticion";
                        pjsonentidadpeticion.Value = pEnpacto_Aud.jsonentidadpeticion;
                        pjsonentidadpeticion.Direction = ParameterDirection.Input;
                        pjsonentidadpeticion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pjsonentidadpeticion);

                        DbParameter pjsonentidadrespuesta = cmdTransaccionFactory.CreateParameter();
                        pjsonentidadrespuesta.ParameterName = "p_jsonentidadrespuesta";
                        pjsonentidadrespuesta.Value = pEnpacto_Aud.jsonentidadrespuesta;
                        pjsonentidadrespuesta.Direction = ParameterDirection.Input;
                        pjsonentidadrespuesta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pjsonentidadrespuesta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_INT_ENPACTO_AU_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEnpacto_Aud.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt32(pconsecutivo.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);
                        return pEnpacto_Aud;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EnpactoData", "CrearEnpacto_Aud", ex);
                        return null;
                    }
                }
            }
        }
    }
}
