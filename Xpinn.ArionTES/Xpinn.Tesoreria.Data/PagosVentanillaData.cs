using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Tesoreria.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla PagosVentanillaData
    /// </summary>
    public class PagosVentanillaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TransaccionesCaja
        /// </summary>
        public PagosVentanillaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public TransaccionCaja AplicarPagoVentanilla(TransaccionCaja pEntidad, GridView gvTransacciones, GridView gvFormaPago, GridView gvCheques, string pObservacion, Usuario pUsuario, ref string Error)
        {
            return AplicarPagoVentanilla(pEntidad, null, gvTransacciones, gvFormaPago, gvCheques, pObservacion, pUsuario, ref Error);
        }

        public TransaccionCaja AplicarPagoVentanilla(TransaccionCaja pEntidad, PersonaTransaccion perTran, GridView gvTransacciones, GridView gvFormaPago, GridView gvCheques, string pObservacion, Usuario pUsuario, ref string Error)
        {
            Error = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        // Se crea la operación de ventanilla

                        cmdTransaccionFactory.Parameters.Clear();
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
                      

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_TES_OPERACION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        Int64 pCod_Ope = Convert.ToInt64(pcode_opera.Value);
                        

                        // Realizando la aplicación de la transacciones

                        long moneda = 0;
                        long tipomov = 0;
                        string nomtipomov = "";
                        string nroprod = "";
                        long tipotran = 0;
                        long tipopago = 0;
                        decimal valor = 0;
                        string nroRef = "0";
                        string tippago = "";
                        string referencia = "";
                        string idavances = "";

                        foreach (GridViewRow fila in gvTransacciones.Rows)
                        {
                            moneda = long.Parse(fila.Cells[2].Text);
                            tipotran = long.Parse(fila.Cells[12].Text);
                            tipomov = long.Parse(fila.Cells[5].Text);
                            nroprod = Convert.ToString(fila.Cells[8].Text);
                            nomtipomov = tipomov == 2 ? "INGRESO" : "EGRESO";
                            valor = decimal.Parse(fila.Cells[9].Text);
                            nroRef = fila.Cells[8].Text;
                            tippago = fila.Cells[12].Text;

                            if (tippago == "2")
                            {
                                pEntidad.tipo_pago = 2;     // Pago total del crèdito
                            }
                            if (tippago == "3")
                            {
                                pEntidad.tipo_pago = 1;     // Pago por valor
                            }
                            if (tippago == "6")
                            {
                                pEntidad.tipo_pago = 3;     // Abono a capital
                            }

                            try
                            {
                                if (fila.Cells.Count >= 13)
                                    if (fila.Cells[13] != null)
                                        referencia = fila.Cells[13].Text;
                            }
                            catch
                            {
                                referencia = "";
                            }

                            try
                            {
                                if (fila.Cells.Count >= 14)
                                    if (fila.Cells[14] != null)
                                        idavances = fila.Cells[14].Text;
                                if ((tipotran == 2 || tipotran == 3 || tipotran == 6) && idavances != "&nbsp;")
                                    referencia = idavances;
                            }
                            catch
                            {
                                idavances = "";
                            }

                            if (valor > 0)
                            {
                                cmdTransaccionFactory.Parameters.Clear();
                                DbParameter pn_radic = cmdTransaccionFactory.CreateParameter();
                                pn_radic.ParameterName = "pn_num_producto";
                                pn_radic.Value = nroRef;
                                pn_radic.DbType = DbType.Int64;
                                pn_radic.Direction = ParameterDirection.Input;
                                pn_radic.Size = 16;

                                DbParameter pn_cod_cliente = cmdTransaccionFactory.CreateParameter();
                                pn_cod_cliente.ParameterName = "pn_cod_cliente";
                                pn_cod_cliente.Value = pEntidad.cod_persona;
                                pn_cod_cliente.DbType = DbType.Int64;
                                pn_cod_cliente.Direction = ParameterDirection.Input;
                                pn_cod_cliente.Size = 8;

                                DbParameter pn_cod_ope = cmdTransaccionFactory.CreateParameter();
                                pn_cod_ope.ParameterName = "pn_cod_ope";
                                pn_cod_ope.Value = pCod_Ope;
                                pn_cod_ope.DbType = DbType.Int64;
                                pn_cod_ope.Direction = ParameterDirection.Input;
                                pn_cod_ope.Size = 8;

                                DbParameter pf_fecha_pago = cmdTransaccionFactory.CreateParameter();
                                pf_fecha_pago.ParameterName = "pf_fecha_pago";
                                pf_fecha_pago.Value = pEntidad.fecha_cierre;
                                pf_fecha_pago.DbType = DbType.Date;
                                pf_fecha_pago.Direction = ParameterDirection.Input;
                                pf_fecha_pago.Size = 7;

                                DbParameter pn_valor_pago = cmdTransaccionFactory.CreateParameter();
                                pn_valor_pago.ParameterName = "pn_valor_pago";
                                pn_valor_pago.Value = valor;
                                pn_valor_pago.DbType = DbType.Decimal;
                                pn_valor_pago.Direction = ParameterDirection.Input;
                                pn_valor_pago.Size = 8;

                                DbParameter ps_tipo_tran = cmdTransaccionFactory.CreateParameter();
                                ps_tipo_tran.ParameterName = "pn_tipo_tran";
                                ps_tipo_tran.Value = tipotran;
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
                                n_error.ParameterName = "n_error";
                                n_error.Value = 0;
                                n_error.DbType = DbType.Int64;
                                n_error.Direction = ParameterDirection.InputOutput;
                                n_error.Size = 8;

                                DbParameter pn_documento = cmdTransaccionFactory.CreateParameter();
                                pn_documento.ParameterName = "pn_documento";
                                pn_documento.Value = referencia;
                                pn_documento.DbType = DbType.String;
                                pn_documento.Direction = ParameterDirection.Input;

                                DbParameter ppagorotativo = cmdTransaccionFactory.CreateParameter();
                                ppagorotativo.ParameterName = "ppagorotativo";
                                ppagorotativo.Value = pEntidad.tipo_pago;
                                ppagorotativo.DbType = DbType.Int16;
                                ppagorotativo.Direction = ParameterDirection.Input;

                                DbParameter pn_salida = cmdTransaccionFactory.CreateParameter();
                                pn_salida.ParameterName = "PN_SALIDA";
                                pn_salida.Value = "";
                                pn_salida.DbType = DbType.String;
                                pn_salida.Direction = ParameterDirection.Output;

                                cmdTransaccionFactory.Parameters.Add(pn_radic);
                                cmdTransaccionFactory.Parameters.Add(pn_cod_cliente);
                                cmdTransaccionFactory.Parameters.Add(pn_cod_ope);
                                cmdTransaccionFactory.Parameters.Add(pf_fecha_pago);
                                cmdTransaccionFactory.Parameters.Add(pn_valor_pago);
                                cmdTransaccionFactory.Parameters.Add(ps_tipo_tran);
                                cmdTransaccionFactory.Parameters.Add(pn_cod_usu);
                                cmdTransaccionFactory.Parameters.Add(rn_sobrante);
                                cmdTransaccionFactory.Parameters.Add(n_error);
                                cmdTransaccionFactory.Parameters.Add(pn_documento);
                                cmdTransaccionFactory.Parameters.Add(ppagorotativo);
                                cmdTransaccionFactory.Parameters.Add(pn_salida);

                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_REGISOPER";
                                cmdTransaccionFactory.ExecuteNonQuery();

                                string sobrante = rn_sobrante.Value != DBNull.Value ? rn_sobrante.Value.ToString() : string.Empty;
                                string error = n_error.Value != DBNull.Value ? n_error.Value.ToString() : string.Empty;

                                TransaccionCaja transaccion = new TransaccionCaja
                                {
                                    num_producto = !string.IsNullOrWhiteSpace(nroRef) ? Convert.ToInt64(nroRef) : 0,
                                    cod_persona = pEntidad.cod_persona,
                                    cod_ope = pCod_Ope,
                                    fecha_pago = pEntidad.fecha_cierre,
                                    valor_pago = valor,
                                    tipo_tran = tipotran,
                                    codigo_usuario = pUsuario.codusuario,
                                    sobrante = sobrante,
                                    error = error,
                                    documento = referencia,
                                    pagoRotativo = pEntidad.tipo_pago
                                };

                                DAauditoria.InsertarLog(transaccion, "Todos los productos", pUsuario, Accion.Crear.ToString(), TipoAuditoria.Tesoreria, "Creacion de pagos por ventanilla para el producto con numero " + nroRef);

                                if (n_error.Value.ToString() != "0" && n_error.Value.ToString() != "")
                                {
                                    Error = "No se pudo aplicar el pago de " + nroRef + " por valor de " + valor;
                                    return null;
                                }
                            }
                        }

                        // Guardando las formas de pago
                        if (gvFormaPago != null)
                        {
                            foreach (GridViewRow fila2 in gvFormaPago.Rows)
                            {
                                moneda = long.Parse(fila2.Cells[0].Text);
                                tipopago = long.Parse(fila2.Cells[1].Text);
                                valor = decimal.Parse(fila2.Cells[4].Text);
                                tipomov = long.Parse(fila2.Cells[5].Text);
                                long banco = 0;
                                int? idcuentabancaria = null;
                                string numeroboucher = "";

                                if (valor > 0) // se valida que el valor de la forma de pago sea mayor que cero para no insertar datos basura
                                {
                                    if (tipopago == 2)     // cheque
                                    {
                                        long moneda_cheque = 0;
                                        decimal valor_cheque = 0;
                                        string numcheque = "";

                                        foreach (GridViewRow fila3 in gvCheques.Rows)
                                        {
                                            banco = long.Parse(fila3.Cells[1].Text);
                                            moneda_cheque = long.Parse(fila3.Cells[2].Text);
                                            numcheque = fila3.Cells[3].Text;
                                            valor_cheque = decimal.Parse(fila3.Cells[5].Text);

                                            cmdTransaccionFactory.Parameters.Clear();

                                            DbParameter pcode_ope = cmdTransaccionFactory.CreateParameter();
                                            pcode_ope.ParameterName = "pcodigoope";
                                            pcode_ope.Value = pCod_Ope;
                                            pcode_ope.Direction = ParameterDirection.Input;

                                            cmdTransaccionFactory.Parameters.Clear();
                                            DbParameter pfecha_mov = cmdTransaccionFactory.CreateParameter();
                                            pfecha_mov.ParameterName = "pfechaope";
                                            pfecha_mov.Value = pEntidad.fecha_cierre;
                                            pfecha_mov.Direction = ParameterDirection.Input;

                                            DbParameter pcodx_caja = cmdTransaccionFactory.CreateParameter();
                                            pcodx_caja.ParameterName = "pcodigocaja";
                                            pcodx_caja.Value = pEntidad.cod_caja;
                                            pcodx_caja.Direction = ParameterDirection.Input;

                                            DbParameter pcodx_cajero = cmdTransaccionFactory.CreateParameter();
                                            pcodx_cajero.ParameterName = "pcodigocajero";
                                            pcodx_cajero.Value = pEntidad.cod_cajero;
                                            pcodx_cajero.Direction = ParameterDirection.Input;

                                            DbParameter pcodx_banco = cmdTransaccionFactory.CreateParameter();
                                            pcodx_banco.ParameterName = "pcodigobanco";
                                            pcodx_banco.Value = banco;
                                            pcodx_banco.Direction = ParameterDirection.Input;

                                            DbParameter pnro_cheque = cmdTransaccionFactory.CreateParameter();
                                            pnro_cheque.ParameterName = "pnumdoc";
                                            pnro_cheque.Value = numcheque;
                                            pnro_cheque.Direction = ParameterDirection.Input;

                                            DbParameter pcodx_persona = cmdTransaccionFactory.CreateParameter();
                                            pcodx_persona.ParameterName = "pcodpersona";
                                            pcodx_persona.Value = pEntidad.cod_persona;
                                            pcodx_persona.Direction = ParameterDirection.Input;

                                            DbParameter pcodx_moneda = cmdTransaccionFactory.CreateParameter();
                                            pcodx_moneda.ParameterName = "pcodigomoneda";
                                            pcodx_moneda.Value = moneda_cheque;
                                            pcodx_moneda.Direction = ParameterDirection.Input;

                                            DbParameter pvalory = cmdTransaccionFactory.CreateParameter();
                                            pvalory.ParameterName = "pvalor";
                                            pvalory.Value = valor_cheque;
                                            pvalory.Direction = ParameterDirection.Input;

                                            DbParameter pcodis_tipopay = cmdTransaccionFactory.CreateParameter();
                                            pcodis_tipopay.ParameterName = "ptipopago";
                                            pcodis_tipopay.Value = tipopago;
                                            pcodis_tipopay.Direction = ParameterDirection.Input;

                                            DbParameter pcody_tipomov = cmdTransaccionFactory.CreateParameter();
                                            pcody_tipomov.ParameterName = "ptipomov";
                                            pcody_tipomov.Value = 2;
                                            pcody_tipomov.Direction = ParameterDirection.Input;

                                            DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                                            pidctabancaria.ParameterName = "pidctabancaria";
                                            pidctabancaria.Value = DBNull.Value;
                                            pidctabancaria.Direction = ParameterDirection.Input;

                                            cmdTransaccionFactory.Parameters.Add(pcode_ope);
                                            cmdTransaccionFactory.Parameters.Add(pfecha_mov);
                                            cmdTransaccionFactory.Parameters.Add(pcodx_caja);
                                            cmdTransaccionFactory.Parameters.Add(pcodx_cajero);
                                            cmdTransaccionFactory.Parameters.Add(pcodx_banco);
                                            cmdTransaccionFactory.Parameters.Add(pnro_cheque);
                                            cmdTransaccionFactory.Parameters.Add(pcodx_persona);
                                            cmdTransaccionFactory.Parameters.Add(pcodx_moneda);
                                            cmdTransaccionFactory.Parameters.Add(pvalory);
                                            cmdTransaccionFactory.Parameters.Add(pcodis_tipopay);
                                            cmdTransaccionFactory.Parameters.Add(pcody_tipomov);
                                            cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                            cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_MOVIMIENTOCAJA_C";
                                            cmdTransaccionFactory.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        if (tipopago == 5)
                                            // Esto es para las consignaciones de Pagos por Ventanilla
                                            idcuentabancaria = int.Parse(fila2.Cells[6].Text);
                                        else if (tipopago == 10)
                                            // Esto es para las consignaciones de Pagos por Ventanilla
                                            numeroboucher = Convert.ToString(pEntidad.baucher);
                                        else
                                            idcuentabancaria = null;

                                        cmdTransaccionFactory.Parameters.Clear();

                                        DbParameter pcode_ope = cmdTransaccionFactory.CreateParameter();
                                        pcode_ope.ParameterName = "pcodigoope";
                                        pcode_ope.Value = pCod_Ope;
                                        pcode_ope.Direction = ParameterDirection.Input;

                                        DbParameter pfecha_mov = cmdTransaccionFactory.CreateParameter();
                                        pfecha_mov.ParameterName = "pfechaope";
                                        pfecha_mov.Value = pEntidad.fecha_cierre;
                                        pfecha_mov.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_caja = cmdTransaccionFactory.CreateParameter();
                                        pcodx_caja.ParameterName = "pcodigocaja";
                                        pcodx_caja.Value = pEntidad.cod_caja;
                                        pcodx_caja.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_cajero = cmdTransaccionFactory.CreateParameter();
                                        pcodx_cajero.ParameterName = "pcodigocajero";
                                        pcodx_cajero.Value = pEntidad.cod_cajero;
                                        pcodx_cajero.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_banco = cmdTransaccionFactory.CreateParameter();
                                        pcodx_banco.ParameterName = "pcodigobanco";
                                        if (idcuentabancaria == null)
                                            pcodx_banco.Value = DBNull.Value;
                                        else
                                            pcodx_banco.Value = idcuentabancaria;
                                        pcodx_banco.Direction = ParameterDirection.Input;

                                        DbParameter pnro_cheque = cmdTransaccionFactory.CreateParameter();
                                        pnro_cheque.ParameterName = "pnumdoc";
                                        if (numeroboucher != "")
                                            pnro_cheque.Value = numeroboucher;
                                        else
                                            pnro_cheque.Value = DBNull.Value;
                                        pnro_cheque.Direction = ParameterDirection.Input;

                                        DbParameter pcody_persona = cmdTransaccionFactory.CreateParameter();
                                        pcody_persona.ParameterName = "pcodpersona";
                                        pcody_persona.Value = pEntidad.cod_persona;
                                        pcody_persona.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_moneda = cmdTransaccionFactory.CreateParameter();
                                        pcodx_moneda.ParameterName = "pcodigomoneda";
                                        pcodx_moneda.Value = moneda;
                                        pcodx_moneda.Direction = ParameterDirection.Input;

                                        DbParameter pvalorz = cmdTransaccionFactory.CreateParameter();
                                        pvalorz.ParameterName = "pvalor";
                                        pvalorz.Value = valor;
                                        pvalorz.Direction = ParameterDirection.Input;

                                        DbParameter pcodiz_tipopay = cmdTransaccionFactory.CreateParameter();
                                        pcodiz_tipopay.ParameterName = "ptipopago";
                                        pcodiz_tipopay.Value = tipopago;
                                        pcodiz_tipopay.Direction = ParameterDirection.Input;

                                        DbParameter pcodz_tipomov = cmdTransaccionFactory.CreateParameter();
                                        pcodz_tipomov.ParameterName = "ptipomov";
                                        pcodz_tipomov.Value = tipomov;
                                        pcodz_tipomov.Direction = ParameterDirection.Input;

                                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                                        pidctabancaria.ParameterName = "pidctabancaria";
                                        if (idcuentabancaria == null)
                                            pidctabancaria.Value = DBNull.Value;
                                        else
                                            pidctabancaria.Value = idcuentabancaria;
                                        pidctabancaria.Direction = ParameterDirection.Input;

                                        cmdTransaccionFactory.Parameters.Add(pcode_ope);
                                        cmdTransaccionFactory.Parameters.Add(pfecha_mov);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_caja);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_cajero);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_banco);
                                        cmdTransaccionFactory.Parameters.Add(pnro_cheque);
                                        cmdTransaccionFactory.Parameters.Add(pcody_persona);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_moneda);
                                        cmdTransaccionFactory.Parameters.Add(pvalorz);
                                        cmdTransaccionFactory.Parameters.Add(pcodiz_tipopay);
                                        cmdTransaccionFactory.Parameters.Add(pcodz_tipomov);
                                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_MOVIMIENTOCAJA_C";
                                        cmdTransaccionFactory.ExecuteNonQuery();

                                    }
                                }
                            }
                        }
                        //REGISTRA PERSONA QUE HIZO LA OPERACIÓN
                        if (perTran != null)
                            if (!perTran.titular)
                                personaTransaccion(perTran, pCod_Ope, pUsuario);

                        pEntidad.cod_ope = pCod_Ope;
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        return null;
                    }
                }

            }
        }

        public PersonaTransaccion personaTransaccion(PersonaTransaccion perTran, Int64 cod_ope, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        p_cod_ope.Value = cod_ope;
                        p_cod_ope.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        DbParameter p_tipo_iden = cmdTransaccionFactory.CreateParameter();
                        p_tipo_iden.ParameterName = "p_tipo_iden";
                        p_tipo_iden.Value = perTran.tipo_documento;
                        p_tipo_iden.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_iden);

                        DbParameter p_iden = cmdTransaccionFactory.CreateParameter();
                        p_iden.ParameterName = "p_iden";
                        p_iden.Value = perTran.documento;
                        p_iden.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_iden);

                        DbParameter p_primer_nombre = cmdTransaccionFactory.CreateParameter();
                        p_primer_nombre.ParameterName = "p_primer_nombre";
                        p_primer_nombre.Value = perTran.primer_nombre;
                        p_primer_nombre.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_primer_nombre);

                        DbParameter p_segundo_nombre = cmdTransaccionFactory.CreateParameter();
                        p_segundo_nombre.ParameterName = "p_segundo_nombre";
                        p_segundo_nombre.Value = perTran.segundo_nombre;
                        p_segundo_nombre.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_segundo_nombre);

                        DbParameter p_primer_apellido = cmdTransaccionFactory.CreateParameter();
                        p_primer_apellido.ParameterName = "p_primer_apellido";
                        p_primer_apellido.Value = perTran.primer_apellido;
                        p_primer_apellido.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_primer_apellido);

                        DbParameter p_segundo_apellido = cmdTransaccionFactory.CreateParameter();
                        p_segundo_apellido.ParameterName = "p_segundo_apellido";
                        p_segundo_apellido.Value = perTran.segundo_apellido;
                        p_segundo_apellido.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_segundo_apellido);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PERSONA_OPE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return perTran;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }
        public TransaccionCaja AplicarTransaccion(TransaccionCaja pEntidad, Usuario pUsuario, ref string Error)
        {
            Error = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //PN_NUM_PRODUCTO NUMBER,                        
                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pn_radic = cmdTransaccionFactory.CreateParameter();
                        pn_radic.ParameterName = "pn_num_producto";
                        pn_radic.Value = pEntidad.num_producto;
                        pn_radic.DbType = DbType.Int64;
                        pn_radic.Direction = ParameterDirection.Input;
                        pn_radic.Size = 16;

                        //PN_COD_CLIENTE NUMBER,     
                        DbParameter pn_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        pn_cod_cliente.ParameterName = "pn_cod_cliente";
                        pn_cod_cliente.Value = pEntidad.cod_persona;
                        pn_cod_cliente.DbType = DbType.Int64;
                        pn_cod_cliente.Direction = ParameterDirection.Input;
                        pn_cod_cliente.Size = 8;

                        //PN_COD_OPE NUMBER,
                        DbParameter pn_cod_ope = cmdTransaccionFactory.CreateParameter();
                        pn_cod_ope.ParameterName = "pn_cod_ope";
                        pn_cod_ope.Value = pEntidad.cod_ope;
                        pn_cod_ope.DbType = DbType.Int64;
                        pn_cod_ope.Direction = ParameterDirection.Input;
                        pn_cod_ope.Size = 8;

                        //PF_FECHA_PAGO DATE,                             
                        DbParameter pf_fecha_pago = cmdTransaccionFactory.CreateParameter();
                        pf_fecha_pago.ParameterName = "pf_fecha_pago";
                        pf_fecha_pago.Value = pEntidad.fecha_cierre;
                        pf_fecha_pago.DbType = DbType.Date;
                        pf_fecha_pago.Direction = ParameterDirection.Input;
                        pf_fecha_pago.Size = 7;

                        //PN_VALOR_PAGO NUMBER,                        
                        DbParameter pn_valor_pago = cmdTransaccionFactory.CreateParameter();
                        pn_valor_pago.ParameterName = "pn_valor_pago";
                        pn_valor_pago.Value = pEntidad.valor_pago;
                        pn_valor_pago.DbType = DbType.Decimal;
                        pn_valor_pago.Direction = ParameterDirection.Input;
                        pn_valor_pago.Size = 8;

                        //PN_TIPO_TRAN NUMBER,                            
                        DbParameter ps_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        ps_tipo_tran.ParameterName = "pn_tipo_tran";
                        ps_tipo_tran.Value = pEntidad.tipo_pago;
                        ps_tipo_tran.DbType = DbType.Int64;
                        ps_tipo_tran.Direction = ParameterDirection.Input;
                        ps_tipo_tran.Size = 1;

                        //PN_COD_USU NUMBER,
                        DbParameter pn_cod_usu = cmdTransaccionFactory.CreateParameter();
                        pn_cod_usu.ParameterName = "pn_cod_usu";
                        pn_cod_usu.Value = pEntidad.cod_cajero;
                        pn_cod_usu.DbType = DbType.Int64;
                        pn_cod_usu.Direction = ParameterDirection.Input;
                        pn_cod_usu.Size = 8;

                        //RN_SOBRANTE IN OUT NUMBER,     
                        DbParameter rn_sobrante = cmdTransaccionFactory.CreateParameter();
                        rn_sobrante.ParameterName = "rn_sobrante";
                        rn_sobrante.Value = -1;
                        rn_sobrante.DbType = DbType.Int64;
                        rn_sobrante.Direction = ParameterDirection.InputOutput;
                        rn_sobrante.Size = 8;

                        //N_ERROR IN OUT NUMBER,
                        DbParameter n_error = cmdTransaccionFactory.CreateParameter();
                        n_error.ParameterName = "n_error";
                        n_error.Value = 0;
                        n_error.DbType = DbType.Int64;                        
                        n_error.Size = 8;

                        //PN_DOCUMENTO CHAR,    
                        DbParameter pn_documento = cmdTransaccionFactory.CreateParameter();
                        pn_documento.ParameterName = "pn_documento";
                        if (pEntidad.baucher == null)
                            pn_documento.Value = DBNull.Value;
                        else
                            pn_documento.Value = pEntidad.baucher;
                        pn_documento.DbType = DbType.String;
                        pn_documento.Direction = ParameterDirection.Input;

                        //PPAGOROTATIVO NUMBER
                        DbParameter ppagorotativo = cmdTransaccionFactory.CreateParameter();
                        ppagorotativo.ParameterName = "ppagorotativo";
                        ppagorotativo.Value = 1;
                        ppagorotativo.DbType = DbType.Int16;
                        ppagorotativo.Direction = ParameterDirection.Input;

                        //PN_SALIDA OUT VARCHAR2
                        DbParameter pn_salida = cmdTransaccionFactory.CreateParameter();
                        pn_salida.ParameterName = "PN_SALIDA";
                        pn_salida.Value = "";
                        pn_salida.DbType = DbType.String;
                        pn_salida.Direction = ParameterDirection.Output;



                        cmdTransaccionFactory.Parameters.Add(pn_radic);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_cliente);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_ope);
                        cmdTransaccionFactory.Parameters.Add(pf_fecha_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_pago);
                        cmdTransaccionFactory.Parameters.Add(ps_tipo_tran);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_usu);
                        cmdTransaccionFactory.Parameters.Add(rn_sobrante);
                        cmdTransaccionFactory.Parameters.Add(n_error);
                        cmdTransaccionFactory.Parameters.Add(pn_documento);
                        cmdTransaccionFactory.Parameters.Add(ppagorotativo);
                        cmdTransaccionFactory.Parameters.Add(pn_salida);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_REGISOPER";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (n_error.Value.ToString() != "0" && n_error.Value.ToString() != "")
                        {
                            Error = "No se pudo aplicar el pago de " + pEntidad.num_producto + " por valor de " + pEntidad.valor_pago;
                            return null;
                        }
                        if (!string.IsNullOrEmpty(pn_salida.Value.ToString()))
                        {
                            pEntidad.error = pn_salida.Value.ToString();
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        return null;
                    }
                }
            }
        }


        public TransaccionCaja ActualizarSaldoPersonaAfiliacion(TransaccionCaja pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Update PERSONA_AFILIACION  set SALDO = (VALOR- " + pEntidad.valor_pago + ") where COD_PERSONA = " + pEntidad.cod_persona;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagosVentanillaData", "ActualizarSaldoPersonaAfiliacion", ex);
                        return null;
                    }
                }

            }
        }

        public string FechasApertura(string NumProducto, int TipoProducto, Usuario pUsuario)
        {
            DbDataReader resultado;
            string FechaApertura = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        if (NumProducto != "" && TipoProducto != 0)
                        {
                            string sql = "";

                            if (TipoProducto == 1)      { sql = "SELECT trunc(FECHA_APERTURA)   AS FECHA_APERTURA FROM APORTE WHERE NUMERO_APORTE = " + NumProducto; }
                            else if (TipoProducto == 2) { sql = "SELECT trunc(FECHA_DESEMBOLSO) AS FECHA_APERTURA FROM CREDITO WHERE NUMERO_RADICACION =" + NumProducto; }
                            else if (TipoProducto == 3) { sql = "SELECT trunc(FECHA_APERTURA)   AS FECHA_APERTURA FROM AHORRO_VISTA WHERE NUMERO_CUENTA =" + NumProducto; }
                            else if (TipoProducto == 4) { sql = "SELECT trunc(NVL(FECHA_ACTIVACION, FECHA_APROBACION)) AS FECHA_APERTURA FROM SERVICIOS WHERE NUMERO_SERVICIO =" + NumProducto; }
                            //else if (TipoProducto == 5) {sql = "" + NumProducto; } No aplica ya que son CDATs
                            //else if (TipoProducto == 6) {sql = "" + NumProducto; } No aplica ya que son Afiliaciones
                            //else if (TipoProducto == 7) {sql = "" + NumProducto; }No aplica ya que son Otros
                            else if (TipoProducto == 8) { sql = "SELECT trunc(FECHA_DEVOLUCION) AS FECHA_APERTURA FROM DEVOLUCION WHERE NUM_DEVOLUCION =" + NumProducto; }
                            else if (TipoProducto == 9) { sql = "SELECT trunc(FECHA_APERTURA)   AS FECHA_APERTURA FROM AHORRO_PROGRAMADO WHERE NUMERO_PROGRAMADO =" + NumProducto; }
                            else { return ""; }

                            if (sql.Trim() != "")
                            { 
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.Text;
                                cmdTransaccionFactory.CommandText = sql;
                                resultado = cmdTransaccionFactory.ExecuteReader();
                                if (resultado.Read())
                                {
                                    if (resultado["FECHA_APERTURA"] != DBNull.Value) FechaApertura = Convert.ToString(resultado["FECHA_APERTURA"]);
                                }
                                dbConnectionFactory.CerrarConexion(connection);
                            }
                        }
                        return FechaApertura;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagosVentanillaData", "FechasApertura", ex);
                        return null;
                    }
                }
            }
        }


        public int TitularProducto(Int64 Cod_persona, string NumProducto, int TipoProducto, Usuario pUsuario)
        {
            DbDataReader resultado;
            int producto = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        if (Cod_persona != 0 && NumProducto != "" && TipoProducto != 0)
                        {
                            string sql = "select CONSULTAR_PRODUCTO(" + Cod_persona + ", " + NumProducto + ", " + TipoProducto + ") as PRODUCTO from dual";

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();
                            if (resultado.Read())
                            {
                                if (resultado["PRODUCTO"] != DBNull.Value) producto = Convert.ToInt32(resultado["PRODUCTO"]);
                            }
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return producto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagosVentanillaData", "TitularProducto", ex);
                        return producto;
                    }
                }
            }
        }

        public string ParametroGeneral(Int64 pCodigo, Usuario pUsuario)
        {
            string valor = "";
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        if (pCodigo != 0)
                        {
                            string sql = "select valor From general Where codigo = " + pCodigo;

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();
                            if (resultado.Read())
                            {
                                if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToString(resultado["VALOR"]);
                            }
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        return valor;
                    }
                }
            }
        }



    }
}
