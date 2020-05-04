using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;
using Xpinn;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla ARQUEOCAJA
    /// </summary>
    public class ArqueoCajaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ARQUEOCAJA
        /// </summary>
        public ArqueoCajaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla ARQUEOCAJA de la base de datos
        /// </summary>
        /// <param name="pArqueoCaja">Entidad ArqueoCaja</param>
        /// <returns>Entidad ArqueoCaja creada</returns>
        public ArqueoCaja CrearArqueoCaja(ArqueoCaja pArqueoCaja, GridView saldos, GridView cheques, Usuario pUsuario)
        {
            //1) guardar el arqueo final con el codigo del cajero ppal y la caja que tenia en ese momento -- OK
            //2) se debe realizar cambio de estado de caja de activa a inactiva y que no le permita relizar ningun tipo de operacion --OK
            //3) realizar el traslado de dinero al cajero ppal y caja ppal de esa oficina; q los valores sean mayores a cero -- OK
            //4) se debe realizar la consignacion de los cheques que esten pendientes por entregar al cajero ppal
         
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        int orden = 0;//captura el valor del codigo de Traslado
                        int codMoneda = 0;
                        decimal efectivo = 0;
                        decimal cheque = 0;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;


                        // en esta porcion de codigo se inserta primero la operacion realizada con el fin de ir alimentar la operacion cod_ope
                        // en la tabla TransaccionesCaja
                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        p_ip.Value = pUsuario.IP;

                        DbParameter pcode_opera = cmdTransaccionFactory.CreateParameter();
                        pcode_opera.ParameterName = "pcodigooper";
                        pcode_opera.Value = pArqueoCaja.cod_ope;
                        pcode_opera.Direction = ParameterDirection.Output;

                        DbParameter pcode_tope = cmdTransaccionFactory.CreateParameter();
                        pcode_tope.ParameterName = "pcodigotipoope";
                        pcode_tope.Value = pArqueoCaja.tipo_ope;

                        DbParameter pcode_usuari = cmdTransaccionFactory.CreateParameter();
                        pcode_usuari.ParameterName = "pcodigousuario";
                        pcode_usuari.Value = pUsuario.codusuario;

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "pcodigooficina";
                        pcode_oficina.Value = pArqueoCaja.cod_oficina;

                        DbParameter pcodz_caja = cmdTransaccionFactory.CreateParameter();
                        pcodz_caja.ParameterName = "pcodigocaja";
                        pcodz_caja.Value = pArqueoCaja.cod_caja;

                        DbParameter pcodi_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodi_cajero.ParameterName = "pcodigocajero";
                        pcodi_cajero.Value = pArqueoCaja.cod_cajero;

                        DbParameter pfecha_cal = cmdTransaccionFactory.CreateParameter();
                        pfecha_cal.ParameterName = "pfechaoper";
                        pfecha_cal.Value = pArqueoCaja.fecha_cierre;

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "pobservaciones";
                        pobservaciones.Value = "  ";

                        cmdTransaccionFactory.Parameters.Add(pcode_opera);
                        cmdTransaccionFactory.Parameters.Add(pcode_tope);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuari);
                        cmdTransaccionFactory.Parameters.Add(pcode_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcodz_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodi_cajero);
                        cmdTransaccionFactory.Parameters.Add(pfecha_cal);
                        cmdTransaccionFactory.Parameters.Add(p_ip);
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_OPERACION_CAJA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        Int64? pCodOpe = null;
                        pCodOpe = Convert.ToInt64(pcode_opera.Value);
                        pArqueoCaja.cod_ope = pCodOpe.HasValue ? pCodOpe.Value : 0;

                        if (pCodOpe == null || pCodOpe == 0)
                            return null;

                        foreach (GridViewRow fila in saldos.Rows)
                        {
                            orden = int.Parse(fila.Cells[0].Text);
                            codMoneda = int.Parse(fila.Cells[1].Text);
                            efectivo = decimal.Parse(fila.Cells[5].Text);
                            cheque = decimal.Parse(fila.Cells[6].Text);

                            // si el orden es el saldo final entonces guarde los totales
                            if (orden == 8)
                            {

                                cmdTransaccionFactory.Parameters.Clear();

                                DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                                pcod_caja.ParameterName = "pcodigocaja";
                                pcod_caja.Value = pArqueoCaja.cod_caja;

                                DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                                pcod_cajero.ParameterName = "pcodigocajero";
                                pcod_cajero.Value = pArqueoCaja.cod_cajero;

                                DbParameter pfecha_Arqueo = cmdTransaccionFactory.CreateParameter();
                                pfecha_Arqueo.ParameterName = "pfechaarqueo";
                                pfecha_Arqueo.Value = pArqueoCaja.fecha_cierre;

                                DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                                pcod_moneda.ParameterName = "pcodigomoneda";
                                pcod_moneda.Value = codMoneda;

                                DbParameter pvalor_efectivo = cmdTransaccionFactory.CreateParameter();
                                pvalor_efectivo.ParameterName = "pvalorefectivo";
                                pvalor_efectivo.Value = efectivo;

                                DbParameter pvalor_cheque = cmdTransaccionFactory.CreateParameter();
                                pvalor_cheque.ParameterName = "pvalorcheque";
                                pvalor_cheque.Value = cheque;

                                cmdTransaccionFactory.Parameters.Add(pcod_caja);
                                cmdTransaccionFactory.Parameters.Add(pcod_cajero);
                                cmdTransaccionFactory.Parameters.Add(pfecha_Arqueo);
                                cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                                cmdTransaccionFactory.Parameters.Add(pvalor_efectivo);
                                cmdTransaccionFactory.Parameters.Add(pvalor_cheque);

                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_ARQUEOCAJA_C";
                                cmdTransaccionFactory.ExecuteNonQuery();

                                if (pUsuario.programaGeneraLog)
                                    DAauditoria.InsertarLog(pArqueoCaja, "ARQUEOCAJA", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Generación de Arqueo"); 
                                    //DAauditoria.InsertarLog(pArqueoCaja, pUsuario, pArqueoCaja.cod_caja, "ARQUEOCAJA", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA
                                    
                                //se cambia el estado de la caja auxiliar ya q se hace el cierre de Activa a Inactiva

                                cmdTransaccionFactory.Parameters.Clear();

                                DbParameter pcodi_caja = cmdTransaccionFactory.CreateParameter();
                                pcodi_caja.ParameterName = "pcodigocaja";
                                pcodi_caja.Value = pArqueoCaja.cod_caja;

                                cmdTransaccionFactory.Parameters.Add(pcod_caja);

                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_CAJA_ESTADO_U";
                                cmdTransaccionFactory.ExecuteNonQuery();

                                // se realiza el traslado de un cajero auxiliar a un cajero ppal

                                //en esta porcion de codigo se crea el Traslado y si tiene permisos el usuario
                                //se guarda la auditoria de ese registro
                                if (efectivo > 0)
                                {// el cajero principal no debe hacerse traslados a si mismo
                                    if (pArqueoCaja.cod_cajero_ppal != pArqueoCaja.cod_cajero)
                                    {

                                        cmdTransaccionFactory.Parameters.Clear();

                                        DbParameter pcod_traslado = cmdTransaccionFactory.CreateParameter();
                                        pcod_traslado.ParameterName = "pcod_traslado";
                                        pcod_traslado.Value = 0;
                                        pcod_traslado.Direction = ParameterDirection.Output;

                                        DbParameter pcodex_opera = cmdTransaccionFactory.CreateParameter();
                                        pcodex_opera.ParameterName = "pcodigooper";
                                        pcodex_opera.Value = pArqueoCaja.cod_ope;

                                        DbParameter pfecha_traslado = cmdTransaccionFactory.CreateParameter();
                                        pfecha_traslado.ParameterName = "pfechatraslado";
                                        pfecha_traslado.Value = pArqueoCaja.fecha_cierre;

                                        DbParameter pcod_tipo_traslado = cmdTransaccionFactory.CreateParameter();
                                        pcod_tipo_traslado.ParameterName = "ptipotraslado";
                                        pcod_tipo_traslado.Value = 1;

                                        DbParameter pcod_caja_ori = cmdTransaccionFactory.CreateParameter();
                                        pcod_caja_ori.ParameterName = "pcodigocajaori";
                                        pcod_caja_ori.Value = pArqueoCaja.cod_caja;

                                        DbParameter pcod_cajero_ori = cmdTransaccionFactory.CreateParameter();
                                        pcod_cajero_ori.ParameterName = "pcodigocajeroori";
                                        pcod_cajero_ori.Value = pArqueoCaja.cod_cajero;

                                        DbParameter pcod_caja_des = cmdTransaccionFactory.CreateParameter();
                                        pcod_caja_des.ParameterName = "pcodigocajades";
                                        pcod_caja_des.Value = pArqueoCaja.cod_caja_ppal;
                                        pcod_caja_des.Size = 8;
                                        pcod_caja_des.DbType = DbType.Int16;
                                        pcod_caja_des.Direction = ParameterDirection.Input;

                                        DbParameter pcod_cajero_des = cmdTransaccionFactory.CreateParameter();
                                        pcod_cajero_des.ParameterName = "pcodigocajerodes";
                                        pcod_cajero_des.Value = pArqueoCaja.cod_cajero_ppal;

                                        DbParameter pcods_moneda = cmdTransaccionFactory.CreateParameter();
                                        pcods_moneda.ParameterName = "pcodigomoneda";
                                        pcods_moneda.Value = codMoneda;

                                        DbParameter pval_traslado = cmdTransaccionFactory.CreateParameter();
                                        pval_traslado.ParameterName = "pvalortraslado";
                                        pval_traslado.Value = efectivo;

                                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                                        pestado.ParameterName = "pestado";
                                        pestado.Value = 0;

                                        cmdTransaccionFactory.Parameters.Add(pcod_traslado);
                                        cmdTransaccionFactory.Parameters.Add(pcodex_opera);
                                        cmdTransaccionFactory.Parameters.Add(pfecha_traslado);
                                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_traslado);
                                        cmdTransaccionFactory.Parameters.Add(pcod_caja_ori);
                                        cmdTransaccionFactory.Parameters.Add(pcod_cajero_ori);
                                        cmdTransaccionFactory.Parameters.Add(pcod_caja_des);
                                        cmdTransaccionFactory.Parameters.Add(pcod_cajero_des);
                                        cmdTransaccionFactory.Parameters.Add(pcods_moneda);
                                        cmdTransaccionFactory.Parameters.Add(pval_traslado);
                                        cmdTransaccionFactory.Parameters.Add(pestado);

                                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_TRASLADOS_C";
                                        cmdTransaccionFactory.ExecuteNonQuery();


                                        if (pUsuario.programaGeneraLog)
                                            DAauditoria.InsertarLog(pArqueoCaja, "TRASLADO", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Traslado de Caja");
                                            //DAauditoria.InsertarLog(pArqueoCaja, pUsuario, 0, "TRASLADO", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                                        //==================================================================
                                        // Aqui insertamos la Transaccion de la caja en "TransaccionCaja"

                                        cmdTransaccionFactory.Parameters.Clear();

                                        DbParameter pfecha_movimientoTran = cmdTransaccionFactory.CreateParameter();
                                        pfecha_movimientoTran.ParameterName = "pfechamov";
                                        pfecha_movimientoTran.Value = pArqueoCaja.fecha_cierre;
                                        pfecha_movimientoTran.Direction = ParameterDirection.Input;
                                        pfecha_movimientoTran.DbType = DbType.DateTime;
                                        cmdTransaccionFactory.Parameters.Add(pfecha_movimientoTran);

                                        DbParameter ptipo_movimiento = cmdTransaccionFactory.CreateParameter();
                                        ptipo_movimiento.ParameterName = "ptipomov";
                                        ptipo_movimiento.Value = pArqueoCaja.tipo_movimiento;
                                        ptipo_movimiento.Direction = ParameterDirection.Input;
                                        ptipo_movimiento.DbType = DbType.String;
                                        cmdTransaccionFactory.Parameters.Add(ptipo_movimiento);

                                        DbParameter pcod_cajaRecep = cmdTransaccionFactory.CreateParameter();
                                        pcod_cajaRecep.ParameterName = "pcodigocaja";
                                        pcod_cajaRecep.Value = pArqueoCaja.cod_caja;
                                        pcod_cajaRecep.Direction = ParameterDirection.Input;
                                        cmdTransaccionFactory.Parameters.Add(pcod_cajaRecep);

                                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                                        pcod_ope.ParameterName = "pcodigooper";
                                        pcod_ope.Value = pArqueoCaja.cod_ope;
                                        pcod_ope.Direction = ParameterDirection.Input;
                                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                                        DbParameter ptipo_tranTranCaja = cmdTransaccionFactory.CreateParameter();
                                        ptipo_tranTranCaja.ParameterName = "pcodigotipotran";
                                        ptipo_tranTranCaja.Value = 902;
                                        ptipo_tranTranCaja.Direction = ParameterDirection.Input;
                                        cmdTransaccionFactory.Parameters.Add(ptipo_tranTranCaja);

                                        DbParameter pcod_cajeroTras = cmdTransaccionFactory.CreateParameter();
                                        pcod_cajeroTras.ParameterName = "pcodigocajero";
                                        if (pArqueoCaja.cod_cajero == 0)
                                            pcod_cajeroTras.Value = DBNull.Value;
                                        else
                                            pcod_cajeroTras.Value = pArqueoCaja.cod_cajero;
                                        pcod_cajeroTras.Direction = ParameterDirection.Input;
                                        cmdTransaccionFactory.Parameters.Add(pcod_cajeroTras);

                                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                                        pcod_persona.ParameterName = "pcodigousuario";
                                        pcod_persona.Value = pUsuario.codusuario;
                                        pcod_persona.Direction = ParameterDirection.Input;
                                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                                        DbParameter pcod_monedaTransaccion = cmdTransaccionFactory.CreateParameter();
                                        pcod_monedaTransaccion.ParameterName = "pcodigomoneda";
                                        pcod_monedaTransaccion.Value = codMoneda;
                                        pcod_monedaTransaccion.Direction = ParameterDirection.Input;
                                        pcod_monedaTransaccion.DbType = DbType.Int32;
                                        cmdTransaccionFactory.Parameters.Add(pcod_monedaTransaccion);

                                        DbParameter pvalor_pago = cmdTransaccionFactory.CreateParameter();
                                        pvalor_pago.ParameterName = "pvalorpago";
                                        pvalor_pago.Value = efectivo;
                                        pvalor_pago.Direction = ParameterDirection.Input;
                                        pvalor_pago.DbType = DbType.Decimal;
                                        cmdTransaccionFactory.Parameters.Add(pvalor_pago);

                                        DbParameter ptipo_pagoTran = cmdTransaccionFactory.CreateParameter();
                                        ptipo_pagoTran.ParameterName = "pformapago";
                                        ptipo_pagoTran.Value = "1";
                                        ptipo_pagoTran.Direction = ParameterDirection.Input;
                                        cmdTransaccionFactory.Parameters.Add(ptipo_pagoTran);

                                        DbParameter pnum_productoTran = cmdTransaccionFactory.CreateParameter();
                                        pnum_productoTran.ParameterName = "pnroprod";
                                        pnum_productoTran.Value = pcod_traslado.Value.ToString();
                                        pnum_productoTran.Direction = ParameterDirection.Input;
                                        cmdTransaccionFactory.Parameters.Add(pnum_productoTran);

                                        cmdTransaccionFactory.Connection = connection;
                                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_TRAN_OPE_CAJA_C";
                                        cmdTransaccionFactory.ExecuteNonQuery();

                                        //==================================================================
                                        // Aqui insertamos la Transaccion de la caja en "MOVIMIENTOCAJA"

                                        cmdTransaccionFactory.Parameters.Clear();

                                        DbParameter pcode_ope = cmdTransaccionFactory.CreateParameter();
                                        pcode_ope.ParameterName = "pcodigoope";
                                        pcode_ope.Value = pArqueoCaja.cod_ope;
                                        pcode_ope.Direction = ParameterDirection.Input;

                                        DbParameter pfecha_movMoviento = cmdTransaccionFactory.CreateParameter();
                                        pfecha_movMoviento.ParameterName = "pfechaope";
                                        pfecha_movMoviento.Value = pArqueoCaja.fecha_cierre;
                                        pfecha_movMoviento.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_cajaMoviento = cmdTransaccionFactory.CreateParameter();
                                        pcodx_cajaMoviento.ParameterName = "pcodigocaja";
                                        pcodx_cajaMoviento.Value = pArqueoCaja.cod_caja;
                                        pcodx_cajaMoviento.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_cajeroMoviento = cmdTransaccionFactory.CreateParameter();
                                        pcodx_cajeroMoviento.ParameterName = "pcodigocajero";
                                        pcodx_cajeroMoviento.Value = pArqueoCaja.cod_cajero;
                                        pcodx_cajeroMoviento.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_banco = cmdTransaccionFactory.CreateParameter();
                                        pcodx_banco.ParameterName = "pcodigobanco";
                                        pcodx_banco.Value = 0;
                                        pcodx_banco.Direction = ParameterDirection.Input;

                                        DbParameter pnrox_cheque = cmdTransaccionFactory.CreateParameter();
                                        pnrox_cheque.ParameterName = "pnumdoc";
                                        pnrox_cheque.Value = DBNull.Value;
                                        pnrox_cheque.Direction = ParameterDirection.Input;

                                        DbParameter pcod_personaMoviento = cmdTransaccionFactory.CreateParameter();
                                        pcod_personaMoviento.ParameterName = "pcodpersona";
                                        if (pUsuario.cod_persona == null)
                                            pcod_personaMoviento.Value = DBNull.Value;
                                        else
                                            pcod_personaMoviento.Value = pUsuario.cod_persona;
                                        pcod_personaMoviento.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_monedaMoviento = cmdTransaccionFactory.CreateParameter();
                                        pcodx_monedaMoviento.ParameterName = "pcodigomoneda";
                                        pcodx_monedaMoviento.Value = codMoneda;
                                        pcodx_monedaMoviento.Direction = ParameterDirection.Input;

                                        DbParameter pvalorx = cmdTransaccionFactory.CreateParameter();
                                        pvalorx.ParameterName = "pvalor";
                                        pvalorx.Value = efectivo;
                                        pvalorx.Direction = ParameterDirection.Input;

                                        DbParameter pcodi_tipopay = cmdTransaccionFactory.CreateParameter();
                                        pcodi_tipopay.ParameterName = "ptipopago";
                                        pcodi_tipopay.Value = "1";
                                        pcodi_tipopay.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_tipomov = cmdTransaccionFactory.CreateParameter();
                                        pcodx_tipomov.ParameterName = "ptipomov";
                                        pcodx_tipomov.Value = "2"; // Ingreso
                                        pcodx_tipomov.Direction = ParameterDirection.Input;

                                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                                        pidctabancaria.ParameterName = "pidctabancaria";
                                        pidctabancaria.Value = DBNull.Value;
                                        pidctabancaria.Direction = ParameterDirection.Input;

                                        cmdTransaccionFactory.Parameters.Add(pcode_ope);
                                        cmdTransaccionFactory.Parameters.Add(pfecha_movMoviento);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_cajaMoviento);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_cajeroMoviento);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_banco);
                                        cmdTransaccionFactory.Parameters.Add(pnrox_cheque);
                                        cmdTransaccionFactory.Parameters.Add(pcod_personaMoviento);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_monedaMoviento);
                                        cmdTransaccionFactory.Parameters.Add(pvalorx);
                                        cmdTransaccionFactory.Parameters.Add(pcodi_tipopay);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_tipomov);
                                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_MOVIMIENTOCAJA_C";
                                        cmdTransaccionFactory.ExecuteNonQuery();
                                    }                                    

                                    //en esta porcion de codigo se va a enviar los saldos realiados 
                                    //por el cajero en la caja especifica en una fecha
                                    cmdTransaccionFactory.Parameters.Clear();

                                    DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                                    pfecha.ParameterName = "pfecha";
                                    pfecha.Value = pArqueoCaja.fecha_cierre;

                                    DbParameter pcodx_caja = cmdTransaccionFactory.CreateParameter();
                                    pcodx_caja.ParameterName = "pcodigocaja";
                                    pcodx_caja.Value = pArqueoCaja.cod_caja;

                                    DbParameter pcodz_cajero = cmdTransaccionFactory.CreateParameter();
                                    pcodz_cajero.ParameterName = "pcodigocajero";
                                    pcodz_cajero.Value = pArqueoCaja.cod_cajero;

                                    DbParameter pcodi_moneda = cmdTransaccionFactory.CreateParameter();
                                    pcodi_moneda.ParameterName = "pcodigomoneda";
                                    pcodi_moneda.Value = codMoneda;

                                    DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                                    pvalor.ParameterName = "pvalor";
                                    pvalor.Value = efectivo;

                                    cmdTransaccionFactory.Parameters.Add(pfecha);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_caja);
                                    cmdTransaccionFactory.Parameters.Add(pcodz_cajero);
                                    cmdTransaccionFactory.Parameters.Add(pcodi_moneda);
                                    cmdTransaccionFactory.Parameters.Add(pvalor);

                                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                    cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_SALDO_CAJA_ARQUEO";
                                    cmdTransaccionFactory.ExecuteNonQuery();

                                }

                            }


                        }
                        int codIngegr = 0;

                        foreach (GridViewRow fila1 in cheques.Rows)
                        {
                            codIngegr = int.Parse(fila1.Cells[0].Text);
                            Int64 parametro = 0;

                            cmdTransaccionFactory.Parameters.Clear();
                            DbParameter pcod_parametro = cmdTransaccionFactory.CreateParameter();
                            pcod_parametro.ParameterName = "pcod_parametro";
                            pcod_parametro.Value =  pArqueoCaja.codigo_parametro ;
                            parametro = pArqueoCaja.codigo_parametro;

                            //se actualiza el cambio de cajero aux a principal
                            cmdTransaccionFactory.Parameters.Clear();
                            DbParameter pcod_ingegr = cmdTransaccionFactory.CreateParameter();
                            pcod_ingegr.ParameterName = "pcodigoingegr";
                            pcod_ingegr.Value = codIngegr;


                            DbParameter pcody_caja = cmdTransaccionFactory.CreateParameter();
                            pcody_caja.ParameterName = "pcodigocaja";
                            pcody_caja.Value = pArqueoCaja.cod_caja_ppal;

                            DbParameter pcody_cajero = cmdTransaccionFactory.CreateParameter();
                            pcody_cajero.ParameterName = "pcodigocajero";
                            pcody_cajero.Value = pArqueoCaja.cod_cajero_ppal;

                            DbParameter pcod_estado = cmdTransaccionFactory.CreateParameter();
                            pcod_estado.ParameterName = "pestado";
                            pcod_estado.Value = 1;
                            if (parametro == 0)
                            {
                                cmdTransaccionFactory.Parameters.Clear();

                                cmdTransaccionFactory.Parameters.Add(pcod_ingegr);
                                cmdTransaccionFactory.Parameters.Add(pcody_caja);
                                cmdTransaccionFactory.Parameters.Add(pcody_cajero);
                                cmdTransaccionFactory.Parameters.Add(pcod_estado);
                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_MOVCAJA_CAJERO_U";
                                cmdTransaccionFactory.ExecuteNonQuery();
                            }
                            /////////////////////////////////////////////
                           
                        }
                        ////////////////detalle arqueo

                        dbConnectionFactory.CerrarConexion(connection);
                        return pArqueoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArqueoCajaData", "CrearArqueoCaja", ex);
                        return null;
                    }
                }
            }
        }


        public ArqueoCaja ArqueoCajadetalle(ArqueoCaja pArqueoCaja, long codMoneda, string concepto, double efectivo, double total, double cheque, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        DbParameter PFECHA = cmdTransaccionFactory.CreateParameter();
                        PFECHA.ParameterName = "PFECHA";
                        PFECHA.Value = pArqueoCaja.fecha_cierre;

                        DbParameter PCOD_CAJA = cmdTransaccionFactory.CreateParameter();
                        PCOD_CAJA.ParameterName = "PCOD_CAJA";
                        PCOD_CAJA.Value = pArqueoCaja.cod_caja;

                        DbParameter PCOD_CAJERO = cmdTransaccionFactory.CreateParameter();
                        PCOD_CAJERO.ParameterName = "PCOD_CAJERO";
                        PCOD_CAJERO.Value = pArqueoCaja.cod_cajero;

                        DbParameter PMONEDA = cmdTransaccionFactory.CreateParameter();
                        PMONEDA.ParameterName = "PMONEDA";
                        PMONEDA.Value = codMoneda;

                        DbParameter PCONCEPTO = cmdTransaccionFactory.CreateParameter();
                        PCONCEPTO.ParameterName = "PCONCEPTO";
                        PCONCEPTO.Value = concepto;

                        DbParameter PEFECTIVO = cmdTransaccionFactory.CreateParameter();
                        PEFECTIVO.ParameterName = "PEFECTIVO";
                        PEFECTIVO.Value = efectivo;

                        DbParameter PCHEQUE = cmdTransaccionFactory.CreateParameter();
                        PCHEQUE.ParameterName = "PCHEQUE";
                        PCHEQUE.Value = cheque;

                        DbParameter PTOTAL = cmdTransaccionFactory.CreateParameter();
                        PTOTAL.ParameterName = "PTOTAL";
                        PTOTAL.Value = total;



                        cmdTransaccionFactory.Parameters.Add(PFECHA);
                        cmdTransaccionFactory.Parameters.Add(PCOD_CAJA);
                        cmdTransaccionFactory.Parameters.Add(PCOD_CAJERO);
                        cmdTransaccionFactory.Parameters.Add(PMONEDA);
                        cmdTransaccionFactory.Parameters.Add(PCONCEPTO);
                        cmdTransaccionFactory.Parameters.Add(PEFECTIVO);
                        cmdTransaccionFactory.Parameters.Add(PCHEQUE);
                        cmdTransaccionFactory.Parameters.Add(PTOTAL);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_ARQUEOCAJA_DETALLE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pArqueoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArqueoCajaData", "ModificarArqueoCaja", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla ARQUEOCAJA de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ArqueoCaja modificada</returns>
        public ArqueoCaja ModificarArqueoCaja(ArqueoCaja pArqueoCaja, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_CAJA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CAJA.ParameterName = param + "COD_CAJA";
                        pCOD_CAJA.Value = pArqueoCaja.cod_caja;

                        DbParameter pCOD_CAJERO = cmdTransaccionFactory.CreateParameter();
                        pCOD_CAJERO.ParameterName = param + "COD_CAJERO";
                        pCOD_CAJERO.Value = pArqueoCaja.cod_cajero;

                        DbParameter pFECHA_ARQUEO = cmdTransaccionFactory.CreateParameter();
                        pFECHA_ARQUEO.ParameterName = param + "FECHA_ARQUEO";
                        pFECHA_ARQUEO.Value = pArqueoCaja.fecha_arqueo;

                        DbParameter pCOD_MONEDA = cmdTransaccionFactory.CreateParameter();
                        pCOD_MONEDA.ParameterName = param + "COD_MONEDA";
                        pCOD_MONEDA.Value = pArqueoCaja.cod_moneda;

                        DbParameter pVALOR_EFECTIVO = cmdTransaccionFactory.CreateParameter();
                        pVALOR_EFECTIVO.ParameterName = param + "VALOR_EFECTIVO";
                        pVALOR_EFECTIVO.Value = pArqueoCaja.valor_efectivo;

                        DbParameter pVALOR_CHEQUE = cmdTransaccionFactory.CreateParameter();
                        pVALOR_CHEQUE.ParameterName = param + "VALOR_CHEQUE";
                        pVALOR_CHEQUE.Value = pArqueoCaja.valor_cheque;

                        cmdTransaccionFactory.Parameters.Add(pCOD_CAJA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CAJERO);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_ARQUEO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MONEDA);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_EFECTIVO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_CHEQUE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_Caja_ARQUEOCAJA_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pArqueoCaja, pUsuario, pArqueoCaja.cod_caja, "ARQUEOCAJA", Accion.Modificar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pArqueoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArqueoCajaData", "ModificarArqueoCaja", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla ARQUEOCAJA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de ARQUEOCAJA</param>
        public void EliminarArqueoCaja(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ArqueoCaja pArqueoCaja = new ArqueoCaja();

                        if (pUsuario.programaGeneraLog)
                            pArqueoCaja = ConsultarArqueoCaja(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_CAJA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CAJA.ParameterName = param + "COD_CAJA";
                        pCOD_CAJA.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_CAJA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_Caja_ARQUEOCAJA_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pArqueoCaja, pUsuario, pArqueoCaja.cod_caja, "ARQUEOCAJA", Accion.Eliminar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArqueoCajaData", "InsertarArqueoCaja", ex);
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro en la tabla ARQUEOCAJA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ARQUEOCAJA</param>
        /// <returns>Entidad ArqueoCaja consultado</returns>
        public ArqueoCaja ConsultarArqueoCaja(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ArqueoCaja entidad = new ArqueoCaja();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_CAJA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CAJA.ParameterName = param + "COD_CAJA";
                        pCOD_CAJA.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_CAJA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_&Modulo&_ARQUEOCAJA_R";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CAJA"] == DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                            if (resultado["COD_CAJERO"] == DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["COD_CAJERO"]);
                            if (resultado["FECHA_ARQUEO"] == DBNull.Value) entidad.fecha_arqueo = Convert.ToDateTime(resultado["FECHA_ARQUEO"]);
                            if (resultado["COD_MONEDA"] == DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["VALOR_EFECTIVO"] == DBNull.Value) entidad.valor_efectivo = Convert.ToInt64(resultado["VALOR_EFECTIVO"]);
                            if (resultado["VALOR_CHEQUE"] == DBNull.Value) entidad.valor_cheque = Convert.ToInt64(resultado["VALOR_CHEQUE"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArqueoCajaData", "ConsultarArqueoCaja", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla ARQUEOCAJA dados unos filtros
        /// </summary>
        /// <param name="pARQUEOCAJA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ArqueoCaja obtenidos</returns>
        public List<ArqueoCaja> ListarArqueoCaja(ArqueoCaja pArqueoCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ArqueoCaja> lstArqueoCaja = new List<ArqueoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  ARQUEOCAJA " + ObtenerFiltro(pArqueoCaja);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ArqueoCaja entidad = new ArqueoCaja();

                            if (resultado["COD_CAJA"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                            if (resultado["COD_CAJERO"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["COD_CAJERO"]);
                            if (resultado["FECHA_ARQUEO"] != DBNull.Value) entidad.fecha_arqueo = Convert.ToDateTime(resultado["FECHA_ARQUEO"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["VALOR_EFECTIVO"] != DBNull.Value) entidad.valor_efectivo = Convert.ToInt64(resultado["VALOR_EFECTIVO"]);
                            if (resultado["VALOR_CHEQUE"] != DBNull.Value) entidad.valor_cheque = Convert.ToInt64(resultado["VALOR_CHEQUE"]);

                            lstArqueoCaja.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstArqueoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArqueoCajaData", "ListarArqueoCaja", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla Cajero, Caja y Oficina de la base de datos
        /// </summary>
        /// <param name="pUsuario">identificador del usuario</param>
        /// <returns>Oficina consultada</returns>
        public ArqueoCaja ConsultarCajero(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            ArqueoCaja entidad = new ArqueoCaja();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT b.cod_cajero codcajero, a.nombre nomcajero,  c.cod_caja codcaja, c.nombre nomcaja,c.esprincipal principal, d.cod_oficina codoficina,d.nombre nomoficina,(select decode(max(x.fecha_proceso),null,sysdate,max(x.fecha_proceso)) from procesooficina x where x.cod_oficina=d.cod_oficina) fechaproceso, (select max(tipo_horario) from procesooficina x where x.cod_oficina=d.cod_oficina) horario FROM USUARIOS a, CAJERO b, CAJA c,  OFICINA d WHERE B.cod_cajero=" + pUsuario.codusuario.ToString() + " and a.codusuario=b.cod_persona and b.cod_caja=c.cod_caja and C.COD_OFICINA=d.cod_oficina";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["nomcajero"] != DBNull.Value) entidad.nom_cajero = Convert.ToString(resultado["nomcajero"]);
                            if (resultado["codcajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["codcajero"]);
                            if (resultado["codcaja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["codcaja"]);
                            if (resultado["nomcaja"] != DBNull.Value) entidad.nom_caja = Convert.ToString(resultado["nomcaja"]);
                            if (resultado["codoficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["codoficina"]);
                            if (resultado["nomoficina"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["nomoficina"]);
                            if (resultado["fechaproceso"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["fechaproceso"]);
                            if (resultado["horario"] != DBNull.Value) entidad.tipo_horario = Convert.ToInt64(resultado["horario"]);

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArqueoCajaData", "ConsultarCajero", ex);
                        return null;
                    }

                }

            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla Cajero, Caja y Oficina de la base de datos
        /// </summary>
        /// <param name="pUsuario">identificador del usuario</param>
        /// <returns>Oficina consultada</returns>
        public ArqueoCaja ConsultarUltFechaArqueoTesoreria(ArqueoCaja pArqueo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            ArqueoCaja entidad = new ArqueoCaja();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT max(fecha_arqueo) fechaarqueo FROM ARQUEOCAJA WHERE cod_cajero=" + pArqueo.cod_cajero;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["fechaarqueo"] != DBNull.Value) entidad.fecha_arqueo = Convert.ToDateTime(resultado["fechaarqueo"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArqueoCajaData", "ConsultarUltFechaArqueoTesoreria", ex);
                        return null;
                    }

                }

            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla Cajero, Caja y Oficina de la base de datos
        /// </summary>
        /// <param name="pUsuario">identificador del usuario</param>
        /// <returns>Oficina consultada</returns>
        public ArqueoCaja ConsultarUltFechaArqueoCaja(ArqueoCaja pArqueo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            ArqueoCaja entidad = new ArqueoCaja();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT max(fecha_arqueo) fechaarqueo FROM ARQUEOCAJA WHERE cod_caja=" + pArqueo.cod_caja + " and cod_cajero=" + pArqueo.cod_cajero;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["fechaarqueo"] != DBNull.Value) entidad.fecha_arqueo = Convert.ToDateTime(resultado["fechaarqueo"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArqueoCajaData", "ConsultarCajero", ex);
                        return null;
                    }

                }

            }
        }

      

        /// <summary>
        /// Obtiene un registro de la tabla Cajero, Caja y Oficina de la base de datos
        /// </summary>
        /// <param name="pUsuario">identificador del usuario</param>
        /// <returns>Oficina consultada</returns>
        public ArqueoCaja Consultarparametrotraslados(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            ArqueoCaja entidad = new ArqueoCaja();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  valor FROM GENERAL WHERE CODIGO=405";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.codigo_parametro = Convert.ToInt64(resultado["valor"]);


                        }


                       
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArqueoCajaData", "Consultarparametrotraslados", ex);
                        return null;
                    }

                }

            }
        }

        public Boolean ValidarArqueo(ArqueoCaja pArqueoCaja, Usuario pUsuario, ref string Error)
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

                        DbParameter pcode_opera = cmdTransaccionFactory.CreateParameter();
                        pcode_opera.ParameterName = "pcodigooper";
                        pcode_opera.Value = pArqueoCaja.cod_ope;

                        DbParameter pcode_tope = cmdTransaccionFactory.CreateParameter();
                        pcode_tope.ParameterName = "pcodigotipoope";
                        pcode_tope.Value = pArqueoCaja.tipo_ope;

                        DbParameter pcode_usuari = cmdTransaccionFactory.CreateParameter();
                        pcode_usuari.ParameterName = "pcodigousuario";
                        pcode_usuari.Value = pUsuario.codusuario;

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "pcodigooficina";
                        pcode_oficina.Value = pArqueoCaja.cod_oficina;

                        DbParameter pcodz_caja = cmdTransaccionFactory.CreateParameter();
                        pcodz_caja.ParameterName = "pcodigocaja";
                        pcodz_caja.Value = pArqueoCaja.cod_caja;

                        DbParameter pcodi_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodi_cajero.ParameterName = "pcodigocajero";
                        pcodi_cajero.Value = pArqueoCaja.cod_cajero;

                        DbParameter pfecha_cal = cmdTransaccionFactory.CreateParameter();
                        pfecha_cal.ParameterName = "pfechaoper";
                        pfecha_cal.DbType = DbType.DateTime;
                        pfecha_cal.Value = pArqueoCaja.fecha_cierre;

                        cmdTransaccionFactory.Parameters.Add(pcode_opera);
                        cmdTransaccionFactory.Parameters.Add(pcode_tope);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuari);
                        cmdTransaccionFactory.Parameters.Add(pcode_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcodz_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodi_cajero);
                        cmdTransaccionFactory.Parameters.Add(pfecha_cal);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_VALIDARARQUEO";
                        cmdTransaccionFactory.ExecuteNonQuery(); 

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        return false;
                    }
                }
            }
        }

        ///agregado para la consulta de arqueos en tesoreria
        ///
        public List<MovimientoCaja> consultararqueolista(MovimientoCaja pMovimientoCaja, Usuario pUsuario, string filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();
            string moneda = "01/01/2000";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = " select ARQUEOCAJA_DETALLE.*, case ARQUEOCAJA_DETALLE.moneda when 1 then 'Pesos' end  as nom_moneda  ,usuarios.cod_oficina,Arqueocaja.* from arqueocaja_detalle inner join usuarios on arqueocaja_detalle.cod_cajero=usuarios.codusuario left join arqueocaja on arqueocaja.cod_cajero=arqueocaja_detalle.cod_cajero Where 1=1 " + filtro + " Order by 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["id_arqueo"] != DBNull.Value) entidad.id_arqueo = Convert.ToInt64(resultado["id_arqueo"]);
                            if (resultado["moneda"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["moneda"]);
                            if (resultado["concepto"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["concepto"]);
                            if (resultado["efectivo"] != DBNull.Value) entidad.efectivo = Convert.ToInt64(resultado["efectivo"]);
                            if (resultado["cheque"] != DBNull.Value) entidad.cheque = Convert.ToInt64(resultado["cheque"]);
                            if (resultado["valor_consignacion"] != DBNull.Value) entidad.consignacion = Convert.ToInt64(resultado["valor_consignacion"]);
                            if (resultado["valor_datafono"] != DBNull.Value) entidad.datafono = Convert.ToInt64(resultado["valor_datafono"]);
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToInt64(resultado["total"]);
                            if (resultado["fecha"] != DBNull.Value) entidad.fec_ope = Convert.ToDateTime(resultado["fecha"]);
                            if (entidad.concepto!="Saldo Inicial")
                            {
                                entidad.nom_moneda = " ";
                            }
                            else 
                            {
                                if (resultado["nom_moneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nom_moneda"]);
                            }
                            moneda= entidad.fec_ope.ToString();
                            
                            

                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarSaldosTesoreria", ex);
                        return null;
                    }
                }
            }
        }



        public ArqueoCaja ArqueosGuardarEnDetalle(ArqueoCaja pArqueoCaja, long codMoneda, string concepto, double efectivo, double total, double cheque, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        DbParameter PFECHA = cmdTransaccionFactory.CreateParameter();
                        PFECHA.ParameterName = "PFECHA";
                        PFECHA.Value = pArqueoCaja.fecha_cierre;

                        DbParameter PCOD_CAJA = cmdTransaccionFactory.CreateParameter();
                        PCOD_CAJA.ParameterName = "PCOD_CAJA";
                        PCOD_CAJA.Value = pArqueoCaja.cod_caja;

                        DbParameter PCOD_CAJERO = cmdTransaccionFactory.CreateParameter();
                        PCOD_CAJERO.ParameterName = "PCOD_CAJERO";
                        PCOD_CAJERO.Value = pArqueoCaja.cod_cajero;

                        DbParameter PMONEDA = cmdTransaccionFactory.CreateParameter();
                        PMONEDA.ParameterName = "PMONEDA";
                        PMONEDA.Value = codMoneda;

                        DbParameter PCONCEPTO = cmdTransaccionFactory.CreateParameter();
                        PCONCEPTO.ParameterName = "PCONCEPTO";
                        PCONCEPTO.Value = concepto;

                        DbParameter PEFECTIVO = cmdTransaccionFactory.CreateParameter();
                        PEFECTIVO.ParameterName = "PEFECTIVO";
                        PEFECTIVO.Value = efectivo;

                        DbParameter PCHEQUE = cmdTransaccionFactory.CreateParameter();
                        PCHEQUE.ParameterName = "PCHEQUE";
                        PCHEQUE.Value = cheque;

                        DbParameter PTOTAL = cmdTransaccionFactory.CreateParameter();
                        PTOTAL.ParameterName = "PTOTAL";
                        PTOTAL.Value = total;



                        cmdTransaccionFactory.Parameters.Add(PFECHA);
                        cmdTransaccionFactory.Parameters.Add(PCOD_CAJA);
                        cmdTransaccionFactory.Parameters.Add(PCOD_CAJERO);
                        cmdTransaccionFactory.Parameters.Add(PMONEDA);
                        cmdTransaccionFactory.Parameters.Add(PCONCEPTO);
                        cmdTransaccionFactory.Parameters.Add(PEFECTIVO);
                        cmdTransaccionFactory.Parameters.Add(PCHEQUE);
                        cmdTransaccionFactory.Parameters.Add(PTOTAL);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_ARQUEOCAJA_DETALLE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pArqueoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArqueoCajaData", "ArqueosGuardarEnDetalle", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarArqueo(DateTime pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ArqueoCaja pArqueo = new ArqueoCaja();
                        pArqueo = ConsultarArqueo(Convert.ToDateTime(pId), vUsuario);

                        DbParameter pid_arqueo = cmdTransaccionFactory.CreateParameter();
                        pid_arqueo.ParameterName = "p_fecha";
                        pid_arqueo.Value = pArqueo.fecha_arqueo;
                        pid_arqueo.Direction = ParameterDirection.Input;
                        pid_arqueo.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pid_arqueo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ARQUEOCAJA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        public ArqueoCaja ConsultarArqueo(DateTime pId, Usuario vUsuario)
        {
            Configuracion conf = new Configuracion();
            DbDataReader resultado;
            ArqueoCaja entidad = new ArqueoCaja();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ARQUEOCAJA_DETALLE WHERE FECHA = To_Date('" + pId.ToShortDateString() +"','"+conf.ObtenerFormatoFecha()+"')";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["ID_ARQUEO"] != DBNull.Value) entidad.id_arqueo = Convert.ToInt32(resultado["ID_ARQUEO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha_arqueo = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["COD_CAJA"] != DBNull.Value) entidad.cod_caja = Convert.ToInt32(resultado["COD_CAJA"]);
                            if (resultado["COD_CAJERO"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt32(resultado["COD_CAJERO"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["MONEDA"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.tipo_movimiento = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["EFECTIVO"] != DBNull.Value) entidad.valor_efectivo = Convert.ToInt32(resultado["EFECTIVO"]);
                            if (resultado["CHEQUE"] != DBNull.Value) entidad.valor_cheque = Convert.ToInt32(resultado["CHEQUE"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch 
                    {
                        return null;
                    }
                }
            }
        }



        public ArqueoCaja ModificarArqueo(ArqueoCaja pArqueo, long codMoneda, string concepto, double efectivo, double total, double cheque, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pid_arqueo = cmdTransaccionFactory.CreateParameter();
                        pid_arqueo.ParameterName = "p_id_arqueo";
                        pid_arqueo.Value = pArqueo.id_arqueo;
                        pid_arqueo.Direction = ParameterDirection.Input;
                        pid_arqueo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pid_arqueo);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        if (pArqueo.fecha_cierre == null)
                            pfecha.Value = DBNull.Value;
                        else
                            pfecha.Value = pArqueo.fecha_cierre;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "p_cod_caja";
                        if (pArqueo.cod_caja == null)
                            pcod_caja.Value = DBNull.Value;
                        else
                            pcod_caja.Value = pArqueo.cod_caja;
                        pcod_caja.Direction = ParameterDirection.Input;
                        pcod_caja.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_caja);

                        DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero.ParameterName = "p_cod_cajero";
                        if (pArqueo.cod_cajero == null)
                            pcod_cajero.Value = DBNull.Value;
                        else
                            pcod_cajero.Value = pArqueo.cod_cajero;
                        pcod_cajero.Direction = ParameterDirection.Input;
                        pcod_cajero.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_cajero);

                        DbParameter pmoneda = cmdTransaccionFactory.CreateParameter();
                        pmoneda.ParameterName = "p_moneda";
                        if (codMoneda == null)
                            pmoneda.Value = DBNull.Value;
                        else
                            pmoneda.Value = codMoneda;
                        pmoneda.Direction = ParameterDirection.Input;
                        pmoneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmoneda);

                        DbParameter pconcepto = cmdTransaccionFactory.CreateParameter();
                        pconcepto.ParameterName = "p_concepto";
                        if (concepto == null)
                            pconcepto.Value = DBNull.Value;
                        else
                            pconcepto.Value = concepto;
                        pconcepto.Direction = ParameterDirection.Input;
                        pconcepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pconcepto);

                        DbParameter pefectivo = cmdTransaccionFactory.CreateParameter();
                        pefectivo.ParameterName = "p_efectivo";
                        if (efectivo == null)
                            pefectivo.Value = DBNull.Value;
                        else
                            pefectivo.Value = efectivo;
                        pefectivo.Direction = ParameterDirection.Input;
                        pefectivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pefectivo);

                        DbParameter pcheque = cmdTransaccionFactory.CreateParameter();
                        pcheque.ParameterName = "p_cheque";
                        if (cheque == null)
                            pcheque.Value = DBNull.Value;
                        else
                            pcheque.Value = cheque;
                        pcheque.Direction = ParameterDirection.Input;
                        pcheque.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcheque);

                        DbParameter ptotal = cmdTransaccionFactory.CreateParameter();
                        ptotal.ParameterName = "p_total";
                        if (total == null)
                            ptotal.Value = DBNull.Value;
                        else
                            ptotal.Value = total;
                        ptotal.Direction = ParameterDirection.Input;
                        ptotal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptotal);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ARQUEOCAJA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pArqueo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ArqueoData", "ModificarArqueo", ex);
                        return null;
                    }
                }
            }
        }

    }
}