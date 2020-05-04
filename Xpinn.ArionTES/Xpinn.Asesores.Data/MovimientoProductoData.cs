using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class MovimientoProductoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public MovimientoProductoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<MovimientoProducto> Listar(Int64 pNumeroRadicacion, Usuario pUsuario, int detalle)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoProducto> lstMovProd = new List<MovimientoProducto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNumRadicacion = cmdTransaccionFactory.CreateParameter();
                        pNumRadicacion.ParameterName = "P_NUMRADICACION";
                        pNumRadicacion.Direction = ParameterDirection.Input;
                        pNumRadicacion.Value = pNumeroRadicacion;

                        DbParameter P_TIPO = cmdTransaccionFactory.CreateParameter();
                        P_TIPO.ParameterName = "P_TIPO";
                        P_TIPO.Direction = ParameterDirection.Input;
                        P_TIPO.Value = detalle;

                        cmdTransaccionFactory.Parameters.Add(pNumRadicacion);
                        cmdTransaccionFactory.Parameters.Add(P_TIPO);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_MOVPRODUCTO_CONSULTAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql;
                        if (detalle == 1)
                            sql = "Select ac.*, t.Descripcion as desc_tran From temp_movimientos ac inner join tipo_tran t on ac.tipo_tran = t.tipo_tran Where ac.numero_radicacion = " + pNumeroRadicacion.ToString() + " Order by ac.consecutivo, ac.fecha_pago, ac.fecha_cuota";
                        else
                            sql = "Select ac.*, t.Descripcion as desc_tran From temp_movimientos ac inner join tipo_tran t on ac.tipo_tran = t.tipo_tran Where ac.numero_radicacion = " + pNumeroRadicacion.ToString() + " Order by ac.consecutivo, ac.fecha_cuota, ac.fecha_pago";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoProducto entidad = new MovimientoProducto();
                            
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["numero_radicacion"].ToString());
                            if (resultado["num_cuota"] != DBNull.Value) entidad.NumCuota = Convert.ToInt64(resultado["num_cuota"].ToString());
                            if (resultado["fecha_cuota"] != DBNull.Value) entidad.FechaCuota = Convert.ToDateTime(resultado["fecha_cuota"].ToString());
                            if (resultado["fecha_pago"] != DBNull.Value) entidad.FechaPago = Convert.ToDateTime(resultado["fecha_pago"].ToString());
                            if (resultado["dias_mora"] != DBNull.Value) entidad.DiasMora = Convert.ToInt64(resultado["dias_mora"].ToString());
                            if (resultado["cod_ope"] != DBNull.Value) entidad.CodOperacion = Convert.ToInt64(resultado["cod_ope"].ToString());
                            if (resultado["tipo_ope"] != DBNull.Value) entidad.TipoOperacion = resultado["tipo_ope"].ToString();
                            if (resultado["tipo_mov"] != DBNull.Value) entidad.TipoMovimiento = resultado["tipo_mov"].ToString();
                            if (resultado["capital"] != DBNull.Value) entidad.Capital = Convert.ToDouble(resultado["capital"].ToString());
                            if (resultado["intcte"] != DBNull.Value) entidad.IntCte = Convert.ToDouble(resultado["intcte"].ToString());
                            if (resultado["intmora"] != DBNull.Value) entidad.IntMora = Convert.ToDouble(resultado["intmora"].ToString());
                            if (resultado["leyMiPyme"] != DBNull.Value) entidad.LeyMiPyme = Convert.ToDouble(resultado["leyMiPyme"].ToString());
                            if (resultado["iva_leyMiPyme"] != DBNull.Value) entidad.ivaMiPyme = Convert.ToDouble(resultado["iva_leyMiPyme"].ToString());
                            if (resultado["poliza"] != DBNull.Value) entidad.Poliza = Convert.ToDouble(resultado["poliza"].ToString());
                            if (resultado["otros"] != DBNull.Value) entidad.Otros = Convert.ToDouble(resultado["otros"].ToString());
                            if (resultado["SEGURO"] != DBNull.Value) entidad.Seguros = Convert.ToDouble(resultado["SEGURO"].ToString());
                            if (resultado["Saldo"] != DBNull.Value) entidad.Saldo = Convert.ToDouble(resultado["Saldo"].ToString());
                            if (resultado["calificacion"] != DBNull.Value) entidad.Calificacion = Convert.ToInt64(resultado["calificacion"].ToString());
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = resultado["NUM_COMP"].ToString();
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.TIPO_COMP = resultado["TIPO_COMP"].ToString();
                            if (resultado["COBRANZAS"] != DBNull.Value) entidad.Prejuridico = Convert.ToDouble(resultado["COBRANZAS"].ToString());
                            if (resultado["IDAVANCE"] != DBNull.Value) entidad.idavance = Convert.ToInt64(resultado["IDAVANCE"].ToString());
                            if (resultado["plazo_avance"] != DBNull.Value) entidad.plazo_avance = Convert.ToInt64(resultado["plazo_avance"].ToString());                            
                            // Tipo_Tran
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["TIPO_TRAN"].ToString());
                            if (resultado["desc_tran"] != DBNull.Value) entidad.desc_tran = resultado["desc_tran"].ToString();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"].ToString());

                            lstMovProd.Add(entidad);
                        }
                        return lstMovProd;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoProductoData", "Listar", ex);
                        return null;
                    }
                }
            }
        }
    }
}