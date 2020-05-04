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
    public class AsEjecutivoMetaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public AsEjecutivoMetaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public EjecutivoMeta Crear(EjecutivoMeta pAseEntiEjeMeta, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdEjecutivo = cmdTransaccionFactory.CreateParameter();
                        pIdEjecutivo.Direction = ParameterDirection.Input;
                        pIdEjecutivo.ParameterName = "ICODIGO";
                        pIdEjecutivo.Value = pAseEntiEjeMeta.IdEjecutivo;

                        DbParameter pIdMeta = cmdTransaccionFactory.CreateParameter();
                        pIdMeta.Direction = ParameterDirection.Input;
                        pIdMeta.ParameterName = "ICODMETA";
                        pIdMeta.Value = pAseEntiEjeMeta.IdMeta;

                        DbParameter pVlrMeta = cmdTransaccionFactory.CreateParameter();
                        pIdMeta.Direction = ParameterDirection.Input;
                        pVlrMeta.ParameterName = "IEXPR";
                        pVlrMeta.Value = pAseEntiEjeMeta.VlrMeta;

                        cmdTransaccionFactory.Parameters.Add(pIdEjecutivo);
                        cmdTransaccionFactory.Parameters.Add(pIdMeta);
                        cmdTransaccionFactory.Parameters.Add(pVlrMeta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_EJECUTIVO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAseEntiEjeMeta, "EJECUTIVO_META", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pAseEntiEjeMeta.IdEjecutivo = Convert.ToInt64(pIdEjecutivo.Value);
                        return pAseEntiEjeMeta;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsEjecutivoMetaData", "CrearCliente", ex);
                        return null;
                    }
                }
            }

        }//end crear

    }
}
