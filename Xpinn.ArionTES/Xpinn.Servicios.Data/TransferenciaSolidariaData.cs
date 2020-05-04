using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Xpinn.Servicios.Entities;
using Xpinn.Util;

namespace Xpinn.Servicios.Data
{
    public class TransferenciaSolidariaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public TransferenciaSolidariaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public TransferenciaSolidaria CrearTransferenciaSolidaria(TransferenciaSolidaria pTransferenciaSolidaria, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnum_transferencia = cmdTransaccionFactory.CreateParameter();
                        pnum_transferencia.ParameterName = "p_Num_transferencia";
                        pnum_transferencia.Value = pTransferenciaSolidaria.num_transferencia;
                        pnum_transferencia.Direction = ParameterDirection.Output;
                        pnum_transferencia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_transferencia);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_Cod_persona";
                        pcod_persona.Value = pTransferenciaSolidaria.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pnum_producto = cmdTransaccionFactory.CreateParameter();
                        pnum_producto.ParameterName = "p_Num_producto";
                        pnum_producto.Value = pTransferenciaSolidaria.num_producto;
                        pnum_producto.Direction = ParameterDirection.Input;
                        pnum_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_producto);

                        DbParameter pcod_linea_producto = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_producto.ParameterName = "p_Cod_linea_producto";
                        pcod_linea_producto.Value = pTransferenciaSolidaria.cod_linea_producto;
                        pcod_linea_producto.Direction = ParameterDirection.Input;
                        pcod_linea_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_producto);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_Tipo_producto";
                        ptipo_producto.Value = pTransferenciaSolidaria.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pcod_destinacion = cmdTransaccionFactory.CreateParameter();
                        pcod_destinacion.ParameterName = "p_Cod_destinacion";
                        if (pTransferenciaSolidaria.cod_destinacion == null)
                            pcod_destinacion.Value = DBNull.Value;
                        else
                            pcod_destinacion.Value = pTransferenciaSolidaria.cod_destinacion;
                        pcod_destinacion.Direction = ParameterDirection.Input;
                        pcod_destinacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_destinacion);

                        DbParameter pfecha_transferencia = cmdTransaccionFactory.CreateParameter();
                        pfecha_transferencia.ParameterName = "p_Fecha_transferencia";
                        pfecha_transferencia.Value = pTransferenciaSolidaria.fecha_transferencia;
                        pfecha_transferencia.Direction = ParameterDirection.Input;
                        pfecha_transferencia.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_transferencia);

                        DbParameter pmonto = cmdTransaccionFactory.CreateParameter();
                        pmonto.ParameterName = "p_Monto";
                        pmonto.Value = pTransferenciaSolidaria.monto;
                        pmonto.Direction = ParameterDirection.Input;
                        pmonto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmonto);

                        DbParameter pvalor_compra = cmdTransaccionFactory.CreateParameter();
                        pvalor_compra.ParameterName = "p_Valor_Compra";
                        pvalor_compra.Value = pTransferenciaSolidaria.valor_compra;
                        pvalor_compra.Direction = ParameterDirection.Input;
                        pvalor_compra.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_compra);

                        DbParameter pbeneficio = cmdTransaccionFactory.CreateParameter();
                        pbeneficio.ParameterName = "p_Beneficio";
                        pbeneficio.Value = pTransferenciaSolidaria.beneficio;
                        pbeneficio.Direction = ParameterDirection.Input;
                        pbeneficio.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pbeneficio);

                        DbParameter pvalor_mercado = cmdTransaccionFactory.CreateParameter();
                        pvalor_mercado.ParameterName = "p_Valor_Mercado";
                        pvalor_mercado.Value = pTransferenciaSolidaria.valor_mercado;
                        pvalor_mercado.Direction = ParameterDirection.Input;
                        pvalor_mercado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_mercado);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        if (pTransferenciaSolidaria.cod_ope != null)
                            pcod_ope.Value = pTransferenciaSolidaria.cod_ope;
                        else
                            pcod_ope.Value = DBNull.Value;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pes_bono_contribucion = cmdTransaccionFactory.CreateParameter();
                        pes_bono_contribucion.ParameterName = "p_es_bono_contribucion";
                        pes_bono_contribucion.Value = pTransferenciaSolidaria.es_bono_contribucion;
                        pes_bono_contribucion.Direction = ParameterDirection.Input;
                        pes_bono_contribucion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pes_bono_contribucion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TRANSF_SOLIDAR_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pTransferenciaSolidaria.num_transferencia = Convert.ToInt32(pnum_transferencia.Value);
                        return pTransferenciaSolidaria;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransferenciaSolidariaData", "CrearTransferenciaSolidaria", ex);
                        return null;
                    }
                }
            }
        }

        
    }
}
