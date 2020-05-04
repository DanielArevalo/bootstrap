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
    public class AsUdMotivoAfiliacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public AsUdMotivoAfiliacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public MotivoAfiliacion CrearMotivoAfiliacion(ClientePotencial pEntCliente, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdEjecutivo = cmdTransaccionFactory.CreateParameter();
                        pIdEjecutivo.Direction = ParameterDirection.Input;
                        pIdEjecutivo.ParameterName = "P_IUSUARIO";
                        pIdEjecutivo.Value = pUsuario.codusuario;
                        pIdEjecutivo.DbType = DbType.Int64;

                        DbParameter pIdCliente = cmdTransaccionFactory.CreateParameter();
                        pIdCliente.Direction = ParameterDirection.Input;
                        pIdCliente.ParameterName = "P_ICODIGO";
                        pIdCliente.Value = pEntCliente.IdCliente;
                        pIdCliente.DbType = DbType.Int64;

                        DbParameter pFechaModi = cmdTransaccionFactory.CreateParameter();
                        pFechaModi.ParameterName = "P_FAFILIACION";
                        pFechaModi.Direction = ParameterDirection.Input;
                        pFechaModi.Value = pEntCliente.FechaRegistro;
                        pFechaModi.DbType = DbType.Date;

                        DbParameter pIdMotivoAfiliacion = cmdTransaccionFactory.CreateParameter();
                        pIdMotivoAfiliacion.Direction = ParameterDirection.Input;
                        pIdMotivoAfiliacion.ParameterName = "P_ICODMOTIVO";
                        pIdMotivoAfiliacion.Value = pEntCliente.MotivoAfiliacion.IdMotivoAfiliacion;
                        pIdMotivoAfiliacion.DbType = DbType.Int64;

                        DbParameter pMotAfili = cmdTransaccionFactory.CreateParameter();
                        pMotAfili.Direction = ParameterDirection.Input;
                        pMotAfili.ParameterName = "P_SOBSERVACION";
                        if (pEntCliente.MotivoAfiliacion.Observaciones != null) pMotAfili.Value = pEntCliente.MotivoAfiliacion.Observaciones;
                        else pMotAfili.Value = DBNull.Value;
                        pMotAfili.DbType = DbType.String;

                        cmdTransaccionFactory.Parameters.Add(pIdEjecutivo);
                        cmdTransaccionFactory.Parameters.Add(pIdCliente);
                        cmdTransaccionFactory.Parameters.Add(pFechaModi);
                        cmdTransaccionFactory.Parameters.Add(pIdMotivoAfiliacion);
                        cmdTransaccionFactory.Parameters.Add(pMotAfili);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_ASAUDAFILIACION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        // DAauditoria.InsertarLog(pEntCliente, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        //pEntCliente.MotivoAfiliacion.IdMotivoAfiliacion = Convert.ToInt64(pIdPrograma.Value);

                        return pEntCliente.MotivoAfiliacion;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsesoresData", "CrearCliente", ex);
                        return null;
                    }
                }
            }

        }//end CrearMotivoAfiliacion

        public void Eliminar(Int64 pIdCliente, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ClientePotencial pAseEntidadCliente = new ClientePotencial();

                        DbParameter pIdPrograma = cmdTransaccionFactory.CreateParameter();
                        pIdPrograma.Direction = ParameterDirection.Input;
                        pIdPrograma.ParameterName = "P_ICODIGO";
                        pIdPrograma.Value = pIdCliente;

                        cmdTransaccionFactory.Parameters.Add(pIdPrograma);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_AUDAFILIACION_ELIMINA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsUdMotivoAfiliacionData", "Eliminar", ex);
                    }
                }
            }
        }
    }
}