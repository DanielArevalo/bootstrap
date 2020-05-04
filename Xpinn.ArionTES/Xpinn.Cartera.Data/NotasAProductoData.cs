using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;
using Xpinn.Caja.Entities;


namespace Xpinn.Cartera.Data
{
    public class NotasAProductoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Castigo
        /// </summary>
        public NotasAProductoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        //GRABAR OPERACION
        public TransaccionCaja CrearOperacionGeneral(TransaccionCaja pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pcode_opera = cmdTransaccionFactory.CreateParameter();
                        pcode_opera.ParameterName = "pcodigooper";
                        pcode_opera.Value = pEntidad.cod_ope;
                        pcode_opera.Direction = ParameterDirection.Output;

                        DbParameter pcode_tope = cmdTransaccionFactory.CreateParameter();
                        pcode_tope.ParameterName = "pcodigotipoope";
                        pcode_tope.Value = pEntidad.tipo_ope;
                        pcode_tope.Direction = ParameterDirection.Input;

                        DbParameter pcode_usuari = cmdTransaccionFactory.CreateParameter();
                        pcode_usuari.ParameterName = "pcodigousuario";
                        pcode_usuari.Value = pUsuario.codusuario;
                        pcode_usuari.Direction = ParameterDirection.Input;

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "pcodigooficina";
                        pcode_oficina.Value = pEntidad.cod_oficina;
                        pcode_oficina.Direction = ParameterDirection.Input;

                        DbParameter pcodi_caja = cmdTransaccionFactory.CreateParameter();
                        pcodi_caja.ParameterName = "pcodigocaja";
                        pcodi_caja.Value = pEntidad.cod_caja;
                        pcodi_caja.Direction = ParameterDirection.Input;

                        DbParameter pcodi_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodi_cajero.ParameterName = "pcodigocajero";
                        pcodi_cajero.Value = pEntidad.cod_cajero;
                        pcodi_cajero.Direction = ParameterDirection.Input;

                        DbParameter pfecha_oper = cmdTransaccionFactory.CreateParameter();
                        pfecha_oper.ParameterName = "pfechaoper";
                        pfecha_oper.Value = pEntidad.fecha_aplica;
                        pfecha_oper.Direction = ParameterDirection.Input;

                        DbParameter pfecha_cal = cmdTransaccionFactory.CreateParameter();
                        pfecha_cal.ParameterName = "pfechacalc";
                        pfecha_cal.Value = pEntidad.fecha_movimiento;
                        pfecha_cal.Direction = ParameterDirection.Input;

                        DbParameter pobservacion = cmdTransaccionFactory.CreateParameter();
                        pobservacion.ParameterName = "pobservacion";
                        if (pEntidad.observacion == null)
                            pobservacion.Value = DBNull.Value;
                        else
                            pobservacion.Value = pEntidad.observacion;
                        pobservacion.Direction = ParameterDirection.Input;

                        DbParameter pcod_proceso = cmdTransaccionFactory.CreateParameter();
                        pcod_proceso.ParameterName = "pcod_proceso";
                        if (pEntidad.cod_proceso == null)
                            pcod_proceso.Value = DBNull.Value;
                        else
                            pcod_proceso.Value = pEntidad.cod_proceso;
                        pcod_proceso.DbType = DbType.Int64;

                        DbParameter pnum_comp = cmdTransaccionFactory.CreateParameter();
                        pnum_comp.ParameterName = "pnum_comp";
                        pnum_comp.Value = -2;
                        pnum_comp.Direction = ParameterDirection.Input;

                        DbParameter ptipo_comp = cmdTransaccionFactory.CreateParameter();
                        ptipo_comp.ParameterName = "ptipo_comp";
                        ptipo_comp.Value = -2;
                        ptipo_comp.Direction = ParameterDirection.Input;

                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        p_ip.Value = pUsuario.IP;

                        DbParameter p_error = cmdTransaccionFactory.CreateParameter();
                        p_error.ParameterName = "p_error";
                        p_error.Direction = ParameterDirection.Output;
                        p_error.Value = "";
                        p_error.DbType = DbType.AnsiStringFixedLength;

                        cmdTransaccionFactory.Parameters.Add(pcode_opera);
                        cmdTransaccionFactory.Parameters.Add(pcode_tope);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuari);
                        cmdTransaccionFactory.Parameters.Add(pcode_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcodi_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodi_cajero);
                        cmdTransaccionFactory.Parameters.Add(pfecha_oper);
                        cmdTransaccionFactory.Parameters.Add(pfecha_cal);
                        cmdTransaccionFactory.Parameters.Add(pobservacion);
                        cmdTransaccionFactory.Parameters.Add(pcod_proceso);
                        cmdTransaccionFactory.Parameters.Add(pnum_comp);
                        cmdTransaccionFactory.Parameters.Add(ptipo_comp);
                        cmdTransaccionFactory.Parameters.Add(p_ip);
                        cmdTransaccionFactory.Parameters.Add(p_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_TES_OPERACION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pEntidad.cod_ope = Convert.ToInt64(pcode_opera.Value);
                        if (p_error != null)
                            if (p_error.Value != null)
                                if (p_error.Value.ToString() != "")
                                { 
                                    pEntidad.error = Convert.ToString(p_error.Value);
                                    return null;
                                }

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NotasAProductoData", "CrearOperacionGeneral", ex);
                        return null;
                    }
                }
            }
        }



        //GRABAR LA TRANSACCION 
        public void CrearTransaccion(string nroRef, long cod_persona, long cod_ope, DateTime fecha_cierre, decimal valor, long moneda, long tipoproducto, string nroprod, int tiponota, int cod_atr, Usuario pUsuario,Boolean Pendientes, ref string Error)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pn_radic = cmdTransaccionFactory.CreateParameter();
                        pn_radic.ParameterName = "pn_num_producto";
                        pn_radic.Value = nroRef;
                        pn_radic.DbType = DbType.String;
                        pn_radic.Direction = ParameterDirection.Input;
                        pn_radic.Size = 20;

                        DbParameter pn_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        pn_cod_cliente.ParameterName = "pn_cod_cliente";
                        pn_cod_cliente.Value = cod_persona;
                        pn_cod_cliente.DbType = DbType.Int64;
                        pn_cod_cliente.Direction = ParameterDirection.Input;
                        pn_cod_cliente.Size = 8;

                        DbParameter pn_cod_ope = cmdTransaccionFactory.CreateParameter();
                        pn_cod_ope.ParameterName = "pn_cod_ope";
                        pn_cod_ope.Value = cod_ope;
                        pn_cod_ope.DbType = DbType.Int64;
                        pn_cod_ope.Direction = ParameterDirection.Input;
                        pn_cod_ope.Size = 8;

                        DbParameter pf_fecha_pago = cmdTransaccionFactory.CreateParameter();
                        pf_fecha_pago.ParameterName = "pf_fecha_pago";
                        pf_fecha_pago.Value = fecha_cierre;
                        pf_fecha_pago.DbType = DbType.Date;
                        pf_fecha_pago.Direction = ParameterDirection.Input;

                       
                        DbParameter pn_valor_pago = cmdTransaccionFactory.CreateParameter();
                        pn_valor_pago.ParameterName = "pn_valor_pago";
                        pn_valor_pago.Value = valor;
                        pn_valor_pago.DbType = DbType.Decimal;
                        pn_valor_pago.Direction = ParameterDirection.Input;
                        pn_valor_pago.Size = 8;

                        DbParameter ps_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        ps_tipo_tran.ParameterName = "pn_tipo_prod";
                        ps_tipo_tran.Value = tipoproducto;
                        ps_tipo_tran.DbType = DbType.Int64;
                        ps_tipo_tran.Direction = ParameterDirection.Input;
                        ps_tipo_tran.Size = 1;

                        DbParameter pn_cod_usu = cmdTransaccionFactory.CreateParameter();
                        pn_cod_usu.ParameterName = "pn_cod_usu";
                        pn_cod_usu.Value = pUsuario.codusuario;
                        pn_cod_usu.DbType = DbType.Int64;
                        pn_cod_usu.Direction = ParameterDirection.Input;
                        pn_cod_usu.Size = 8;

                        DbParameter rn_sobrante = cmdTransaccionFactory.CreateParameter();
                        rn_sobrante.ParameterName = "rn_sobrante";
                        rn_sobrante.Value = -1;
                        rn_sobrante.DbType = DbType.Int64;
                        rn_sobrante.Direction = ParameterDirection.InputOutput;
                        rn_sobrante.Size = 8;

                        DbParameter n_error = cmdTransaccionFactory.CreateParameter();
                        n_error.ParameterName = "PN_ERROR";
                        n_error.Value = DBNull.Value;
                        n_error.DbType = DbType.StringFixedLength;
                        n_error.Direction = ParameterDirection.InputOutput;
                        n_error.Size = 1000;

                        DbParameter pn_tiponota = cmdTransaccionFactory.CreateParameter();
                        pn_tiponota.ParameterName = "pn_tiponota";
                        pn_tiponota.Value = tiponota;
                        pn_tiponota.DbType = DbType.Int32;
                        pn_tiponota.Direction = ParameterDirection.Input;

                        DbParameter PN_PENDIENTES = cmdTransaccionFactory.CreateParameter();
                        PN_PENDIENTES.ParameterName = "PN_PENDIENTES";
                        PN_PENDIENTES.Value = Pendientes ? 1 : 0;
                        PN_PENDIENTES.DbType = DbType.Int32;
                        PN_PENDIENTES.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_atr = cmdTransaccionFactory.CreateParameter();
                        pn_cod_atr.ParameterName = "pn_cod_atr";
                        if (cod_atr == 0)
                            pn_cod_atr.Value = DBNull.Value;
                        else
                            pn_cod_atr.Value = cod_atr;
                        pn_cod_atr.DbType = DbType.Int32;
                        pn_cod_atr.Direction = ParameterDirection.Input;
                        
                        cmdTransaccionFactory.Parameters.Add(pn_radic);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_cliente);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_ope);
                        cmdTransaccionFactory.Parameters.Add(pf_fecha_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_pago);
                        cmdTransaccionFactory.Parameters.Add(ps_tipo_tran);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_usu);
                        cmdTransaccionFactory.Parameters.Add(rn_sobrante);
                        cmdTransaccionFactory.Parameters.Add(n_error);
                        cmdTransaccionFactory.Parameters.Add(pn_tiponota);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_atr);
                        cmdTransaccionFactory.Parameters.Add(PN_PENDIENTES);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_TRAN_NOTASAPROD"; //XPF_CAJAFIN_REGISOPER
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (n_error.Value != DBNull.Value) Error = Convert.ToString(n_error.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NotasAProductoData", "CrearTransaccion", ex);
                        Error = ex.Message;
                    }
                }
            }
        }
        

    }
}
