using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class RegistroGeneraciónExtractoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public RegistroGeneraciónExtractoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        
        public RegistroGeneraciónExtracto AlmacenarRegistroGeneraciónExtractos(RegistroGeneraciónExtracto entidad, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidextracto = cmdTransaccionFactory.CreateParameter();
                        pidextracto.ParameterName = "p_idextracto";
                        pidextracto.Value = entidad.pidextracto;
                        pidextracto.Direction = ParameterDirection.Output;
                        pidextracto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidextracto);

                        DbParameter pfecha_generacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_generacion.ParameterName = "p_fecha_generacion";
                        pfecha_generacion.Value = entidad.pfecha_generacion;
                        pfecha_generacion.Direction = ParameterDirection.Input;
                        pfecha_generacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_generacion);

                        DbParameter pfecha_corte = cmdTransaccionFactory.CreateParameter();
                        pfecha_corte.ParameterName = "p_fecha_corte";
                        if (entidad.pfechacorte != DateTime.MinValue) pfecha_corte.Value = entidad.pfechacorte; else pfecha_corte.Value = DBNull.Value;
                        pfecha_corte.Direction = ParameterDirection.Input;
                        pfecha_corte.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_corte);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = entidad.pcod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = entidad.pnumero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_motivo_ext = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo_ext.ParameterName = "p_cod_motivo_ext";
                        pcod_motivo_ext.Value = entidad.pcodmotivo;
                        pcod_motivo_ext.Direction = ParameterDirection.Input;
                        pcod_motivo_ext.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo_ext);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        pobservaciones.Value = entidad.pobservaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "p_usuario";
                        pusuario.Value = pUsuario.nombre;
                        pusuario.Direction = ParameterDirection.Input;
                        pusuario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_REGISTRAEXT";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        entidad.pidextracto = Convert.ToInt64(pidextracto.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RegistroGeneraciónExtractoData", "AlmacenarRegistroGeneraciónExtractos", ex);
                        return null;
                    }
                }
            }
        }

    }
}
