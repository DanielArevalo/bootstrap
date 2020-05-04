using System;
using System.Data;
using System.Data.Common;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Data
{
    public class SyncTrasladoCajaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        public SyncTrasladoCajaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public SyncTrasladoCaja CrearTrasladoDinero(SyncTrasladoCaja pTraslado, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_traslado = cmdTransaccionFactory.CreateParameter();
                        pcod_traslado.ParameterName = "pcod_traslado";
                        pcod_traslado.Value = pTraslado.cod_traslado;
                        pcod_traslado.Direction = ParameterDirection.Output;

                        DbParameter pcodes_opera = cmdTransaccionFactory.CreateParameter();
                        pcodes_opera.ParameterName = "pcodigooper";
                        pcodes_opera.Value = pTraslado.cod_ope;
                        pcodes_opera.Direction = ParameterDirection.Input;

                        DbParameter pfecha_traslado = cmdTransaccionFactory.CreateParameter();
                        pfecha_traslado.ParameterName = "pfechatraslado";
                        pfecha_traslado.Value = pTraslado.fecha_traslado;

                        DbParameter pcod_tipo_traslado = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_traslado.ParameterName = "ptipotraslado";
                        pcod_tipo_traslado.Value = pTraslado.tipo_traslado;

                        DbParameter pcod_caja_ori = cmdTransaccionFactory.CreateParameter();
                        pcod_caja_ori.ParameterName = "pcodigocajaori";
                        pcod_caja_ori.Value = pTraslado.cod_caja_ori;

                        DbParameter pcod_cajero_ori = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero_ori.ParameterName = "pcodigocajeroori";
                        pcod_cajero_ori.Value = pTraslado.cod_cajero_ori;

                        DbParameter pcod_caja_des = cmdTransaccionFactory.CreateParameter();
                        pcod_caja_des.ParameterName = "pcodigocajades";
                        pcod_caja_des.Value = pTraslado.cod_caja_des;

                        DbParameter pcod_cajero_des = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero_des.ParameterName = "pcodigocajerodes";
                        pcod_cajero_des.Value = pTraslado.cod_cajero_des;

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "pcodigomoneda";
                        pcod_moneda.Value = pTraslado.cod_moneda;

                        DbParameter pval_traslado = cmdTransaccionFactory.CreateParameter();
                        pval_traslado.ParameterName = "pvalortraslado";
                        pval_traslado.Value = pTraslado.valor;

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Value = pTraslado.estado;

                        cmdTransaccionFactory.Parameters.Add(pcod_traslado);
                        cmdTransaccionFactory.Parameters.Add(pcodes_opera);
                        cmdTransaccionFactory.Parameters.Add(pfecha_traslado);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_traslado);
                        cmdTransaccionFactory.Parameters.Add(pcod_caja_ori);
                        cmdTransaccionFactory.Parameters.Add(pcod_cajero_ori);
                        cmdTransaccionFactory.Parameters.Add(pcod_caja_des);
                        cmdTransaccionFactory.Parameters.Add(pcod_cajero_des);
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                        cmdTransaccionFactory.Parameters.Add(pval_traslado);
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_TRASLADOS_C";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pTraslado.cod_traslado = Convert.ToInt64(pcod_traslado.Value);

                        return pTraslado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoData", "CrearTrasladoDinero", ex);
                        return null;
                    }
                }
            }
        }


        public SyncTrasladoCaja GenerarRecepcionDinero(SyncTrasladoCaja pTraslado, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodec_opera = cmdTransaccionFactory.CreateParameter();
                        pcodec_opera.ParameterName = "pcodigooper";
                        pcodec_opera.Value = pTraslado.cod_ope;
                        pcodec_opera.Direction = ParameterDirection.Input;

                        DbParameter pfecha_Recepcion = cmdTransaccionFactory.CreateParameter();
                        pfecha_Recepcion.ParameterName = "pfecha";
                        pfecha_Recepcion.Value = pTraslado.fecha_traslado;
                        pfecha_Recepcion.Direction = ParameterDirection.Input;

                        DbParameter pcod_traslado = cmdTransaccionFactory.CreateParameter();
                        pcod_traslado.ParameterName = "pcodigotraslado";
                        pcod_traslado.Value = pTraslado.cod_traslado;
                        pcod_traslado.Direction = ParameterDirection.Input;

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Value = pTraslado.estado;
                        pestado.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcodec_opera);
                        cmdTransaccionFactory.Parameters.Add(pfecha_Recepcion);
                        cmdTransaccionFactory.Parameters.Add(pcod_traslado);
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_RECEPCION_U";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pTraslado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoData", "GenerarRecepcionDinero", ex);
                        return null;
                    }
                }
            }
        }
        

    }
}
