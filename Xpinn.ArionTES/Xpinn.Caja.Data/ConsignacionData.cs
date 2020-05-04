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

namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla CONSIGNACION
    /// </summary>
    public class ConsignacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla CONSIGNACION
        /// </summary>
        public ConsignacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla CONSIGNACION de la base de datos
        /// </summary>
        /// <param name="pConsignacion">Entidad Consignacion</param>
        /// <returns>Entidad Consignacion creada</returns>
        public Consignacion CrearConsignacion(Consignacion pConsignacion, GridView gvConsignacion, Usuario pUsuario)
        {
          
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open(); GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        DbParameter pcode_opera = cmdTransaccionFactory.CreateParameter();
                        pcode_opera.ParameterName = "pcodigooper";
                        pcode_opera.Value = pConsignacion.cod_ope;
                        pcode_opera.Direction = ParameterDirection.Output;

                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        p_ip.Value = pUsuario.IP;

                        DbParameter pcode_tope = cmdTransaccionFactory.CreateParameter();
                        pcode_tope.ParameterName = "pcodigotipoope";
                        pcode_tope.Value = pConsignacion.tipo_ope;

                        DbParameter pcode_usuari = cmdTransaccionFactory.CreateParameter();
                        pcode_usuari.ParameterName = "pcodigousuario";
                        pcode_usuari.Value = pUsuario.codusuario;

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "pcodigooficina";
                        pcode_oficina.Value = pConsignacion.cod_oficina;

                        DbParameter pcodi_caja = cmdTransaccionFactory.CreateParameter();
                        pcodi_caja.ParameterName = "pcodigocaja";
                        pcodi_caja.Value = pConsignacion.cod_caja;

                        DbParameter pcodi_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodi_cajero.ParameterName = "pcodigocajero";
                        pcodi_cajero.Value = pConsignacion.cod_cajero;

                        DbParameter pfecha_cal = cmdTransaccionFactory.CreateParameter();
                        pfecha_cal.ParameterName = "pfechaoper";
                        pfecha_cal.Value = pConsignacion.fecha_consignacion;

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "pobservaciones";
                        pobservaciones.Value = "  ";

                        cmdTransaccionFactory.Parameters.Add(pcode_opera);
                        cmdTransaccionFactory.Parameters.Add(pcode_tope);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuari);
                        cmdTransaccionFactory.Parameters.Add(pcode_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcodi_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodi_cajero);
                        cmdTransaccionFactory.Parameters.Add(pfecha_cal);
                        cmdTransaccionFactory.Parameters.Add(p_ip);
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_OPERACION_CAJA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pConsignacion.cod_consignacion = Convert.ToInt64(pcode_opera.Value);

                        if (pConsignacion.valor_efectivo > 0)
                        {
                            cmdTransaccionFactory.Parameters.Clear();

                            DbParameter pcodey_opera = cmdTransaccionFactory.CreateParameter();
                            pcodey_opera.ParameterName = "pcodigooper";
                            pcodey_opera.Value = pcode_opera.Value;
                            pcodey_opera.Size = 8;
                            pcodey_opera.DbType = DbType.Int64;
                            pcodey_opera.Direction = ParameterDirection.Input;

                            DbParameter pcod_consignacion = cmdTransaccionFactory.CreateParameter();
                            pcod_consignacion.ParameterName = "pcodigoconsignacion";
                            pcod_consignacion.Value = pConsignacion.cod_consignacion;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Input;

                            DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                            pcod_caja.ParameterName = "pcodigocaja";
                            pcod_caja.Value = pConsignacion.cod_caja;
                            pcod_caja.Size = 8;
                            pcod_caja.DbType = DbType.Int64;
                            pcod_caja.Direction = ParameterDirection.Input;

                            DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                            pcod_cajero.ParameterName = "pcodigocajero";
                            pcod_cajero.Value = pConsignacion.cod_cajero;
                            pcod_cajero.Size = 8;
                            pcod_cajero.DbType = DbType.Int64;
                            pcod_cajero.Direction = ParameterDirection.Input;

                            DbParameter pfecha_consignacion = cmdTransaccionFactory.CreateParameter();
                            pfecha_consignacion.ParameterName = "pfechaconsignacion";
                            pfecha_consignacion.Value = pConsignacion.fecha_consignacion;
                            pfecha_consignacion.DbType = DbType.DateTime;
                            pfecha_consignacion.Direction = ParameterDirection.Input;
                            pfecha_consignacion.Size = 7;

                            DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                            pcod_banco.ParameterName = "pcodigobanco";
                            pcod_banco.Value = pConsignacion.cod_banco;
                            pcod_consignacion.Size = 8;
                            pcod_banco.DbType = DbType.Int64;
                            pcod_banco.Direction = ParameterDirection.Input;

                            DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                            pcod_moneda.ParameterName = "pcodigomoneda";
                            pcod_moneda.Value = pConsignacion.cod_moneda;
                            pcod_moneda.Size = 8;
                            pcod_moneda.DbType = DbType.Int64;
                            pcod_moneda.Direction = ParameterDirection.Input;

                            DbParameter pvalor_efectivo = cmdTransaccionFactory.CreateParameter();
                            pvalor_efectivo.ParameterName = "pvalorefectivo";
                            pvalor_efectivo.Value = pConsignacion.valor_efectivo;
                            pvalor_efectivo.DbType = DbType.Decimal;
                            pvalor_efectivo.Direction = ParameterDirection.Input;

                            DbParameter pvalor_cheque = cmdTransaccionFactory.CreateParameter();
                            pvalor_cheque.ParameterName = "pvalorcheque";
                            pvalor_cheque.Value = pConsignacion.valor_cheque;
                            pvalor_cheque.DbType = DbType.Decimal;
                            pvalor_cheque.Direction = ParameterDirection.Input;

                            DbParameter pobservacionescon = cmdTransaccionFactory.CreateParameter();
                            pobservacionescon.ParameterName = "pobservaciones";
                            pobservacionescon.Value = pConsignacion.observaciones;
                            pobservacionescon.DbType = DbType.AnsiString;
                            pobservacionescon.Direction = ParameterDirection.Input;
                            pobservacionescon.Size = 150;

                            DbParameter pfecha_salida = cmdTransaccionFactory.CreateParameter();
                            pfecha_salida.ParameterName = "pfechasalida";
                            pfecha_salida.Value = pConsignacion.fecha_salida;
                            pfecha_salida.DbType = DbType.DateTime;
                            pfecha_salida.Direction = ParameterDirection.Input;
                            pfecha_salida.Size = 7;

                            DbParameter pcodcuenta = cmdTransaccionFactory.CreateParameter();
                            pcodcuenta.ParameterName = "pcodcuenta";
                            pcodcuenta.Value = Convert.ToString(pConsignacion.Cuenta);
                            pobservaciones.DbType = DbType.AnsiString;
                            pobservaciones.Direction = ParameterDirection.Input;

                       
                            cmdTransaccionFactory.Parameters.Add(pcodey_opera);
                            cmdTransaccionFactory.Parameters.Add(pcod_consignacion);
                            cmdTransaccionFactory.Parameters.Add(pcod_caja);
                            cmdTransaccionFactory.Parameters.Add(pcod_cajero);
                            cmdTransaccionFactory.Parameters.Add(pfecha_consignacion);
                            cmdTransaccionFactory.Parameters.Add(pcod_banco);
                            cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                            cmdTransaccionFactory.Parameters.Add(pvalor_efectivo);
                            cmdTransaccionFactory.Parameters.Add(pvalor_cheque);
                            cmdTransaccionFactory.Parameters.Add(pobservacionescon);
                            cmdTransaccionFactory.Parameters.Add(pfecha_salida);
                            cmdTransaccionFactory.Parameters.Add(pcodcuenta);

                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_CONSIGNACION_C";
                            cmdTransaccionFactory.ExecuteNonQuery();

                            if (pUsuario.programaGeneraLog)
                                DAauditoria.InsertarLog(pConsignacion, "CONSIGNACION", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Registrar Consignación");
                                //DAauditoria.InsertarLog(pConsignacion, pUsuario, pConsignacion.cod_consignacion, "CONSIGNACION", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                       //en esta porcion de codigo se va a enviar los saldos realizados 
                            //por el cajero en la caja especifica en una fecha

                            cmdTransaccionFactory.Parameters.Clear();
                            DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                            pfecha.ParameterName = "pfecha";
                            pfecha.Value = pConsignacion.fecha_consignacion;
                            pfecha.DbType = DbType.DateTime;
                            pfecha.Direction = ParameterDirection.Input;
                            pfecha.Size = 7;

                            DbParameter pcoda_caja = cmdTransaccionFactory.CreateParameter();
                            pcoda_caja.ParameterName = "pcodigocaja";
                            pcoda_caja.Value = pConsignacion.cod_caja;
                            pcoda_caja.Size = 8;
                            pcoda_caja.DbType = DbType.Int32;
                            pcoda_caja.Direction = ParameterDirection.Input;

                            DbParameter pcoda_cajero = cmdTransaccionFactory.CreateParameter();
                            pcoda_cajero.ParameterName = "pcodigocajero";
                            pcoda_cajero.Value = pConsignacion.cod_cajero;
                            pcoda_cajero.Size = 8;
                            pcoda_cajero.DbType = DbType.Int32;
                            pcoda_cajero.Direction = ParameterDirection.Input;

                            DbParameter pcodi_moneda = cmdTransaccionFactory.CreateParameter();
                            pcodi_moneda.ParameterName = "pcodigomoneda";
                            pcodi_moneda.Value = pConsignacion.cod_moneda;
                            pcodi_moneda.Size = 8;
                            pcodi_moneda.DbType = DbType.Int32;
                            pcodi_moneda.Direction = ParameterDirection.Input;

                            DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                            pvalor.ParameterName = "pvalor";
                            pvalor.Value = pConsignacion.valor_efectivo;
                            pvalor.DbType = DbType.Decimal;
                            pvalor.Direction = ParameterDirection.Input;

                            cmdTransaccionFactory.Parameters.Add(pfecha);
                            cmdTransaccionFactory.Parameters.Add(pcoda_caja);
                            cmdTransaccionFactory.Parameters.Add(pcoda_cajero);
                            cmdTransaccionFactory.Parameters.Add(pcodi_moneda);
                            cmdTransaccionFactory.Parameters.Add(pvalor);

                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_SALDO_CAJA_CONSIG";
                            cmdTransaccionFactory.ExecuteNonQuery();

                        }
                        else
                        {

                            cmdTransaccionFactory.Parameters.Clear();

                            DbParameter pcodey_opera = cmdTransaccionFactory.CreateParameter();
                            pcodey_opera.ParameterName = "pcodigooper";
                            pcodey_opera.Value = pcode_opera.Value;
                            pcodey_opera.Size = 8;
                            pcodey_opera.DbType = DbType.Int64;
                            pcodey_opera.Direction = ParameterDirection.Input;

                            DbParameter pcod_consignacion = cmdTransaccionFactory.CreateParameter();
                            pcod_consignacion.ParameterName = "pcodigoconsignacion";
                            pcod_consignacion.Value = pConsignacion.cod_consignacion;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Output;

                            DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                            pcod_caja.ParameterName = "pcodigocaja";
                            pcod_caja.Value = pConsignacion.cod_caja;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Input;

                            DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                            pcod_cajero.ParameterName = "pcodigocajero";
                            pcod_cajero.Value = pConsignacion.cod_cajero;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Input;

                            DbParameter pfecha_consignacion = cmdTransaccionFactory.CreateParameter();
                            pfecha_consignacion.ParameterName = "pfechaconsignacion";
                            pfecha_consignacion.Value = pConsignacion.fecha_consignacion;
                            pfecha_consignacion.DbType = DbType.DateTime;
                            pfecha_consignacion.Direction = ParameterDirection.Input;
                            pfecha_consignacion.Size = 7;

                            DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                            pcod_banco.ParameterName = "pcodigobanco";
                            pcod_banco.Value = pConsignacion.cod_banco;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Input;

                            DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                            pcod_moneda.ParameterName = "pcodigomoneda";
                            pcod_moneda.Value = pConsignacion.cod_moneda;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Input;

                            DbParameter pvalor_efectivo = cmdTransaccionFactory.CreateParameter();
                            pvalor_efectivo.ParameterName = "pvalorefectivo";
                            pvalor_efectivo.Value = pConsignacion.valor_efectivo;
                            pvalor_efectivo.DbType = DbType.Decimal;
                            pvalor_efectivo.Direction = ParameterDirection.Input;

                            DbParameter pvalor_cheque = cmdTransaccionFactory.CreateParameter();
                            pvalor_cheque.ParameterName = "pvalorcheque";
                            pvalor_cheque.Value = pConsignacion.valor_cheque;
                            pvalor_cheque.DbType = DbType.Decimal;
                            pvalor_cheque.Direction = ParameterDirection.Input;

                            DbParameter pobservacionescon = cmdTransaccionFactory.CreateParameter();
                            pobservacionescon.ParameterName = "pobservaciones";
                            pobservacionescon.Value = pConsignacion.observaciones;
                            pobservacionescon.DbType = DbType.AnsiString;
                            pobservacionescon.Direction = ParameterDirection.Input;
                            pobservacionescon.Size = 150;

                            DbParameter pfecha_salida = cmdTransaccionFactory.CreateParameter();
                            pfecha_salida.ParameterName = "pfechasalida";
                            pfecha_salida.Value = pConsignacion.fecha_salida;
                            pfecha_salida.DbType = DbType.DateTime;
                            pfecha_salida.Direction = ParameterDirection.Input;
                            pfecha_salida.Size = 7;
                            DbParameter pcodcuenta = cmdTransaccionFactory.CreateParameter();
                            pcodcuenta.ParameterName = "pcodcuenta";
                            pcodcuenta.Value = Convert.ToString(pConsignacion.Cuenta);

                            cmdTransaccionFactory.Parameters.Add(pcodcuenta);
                            cmdTransaccionFactory.Parameters.Add(pcodey_opera);
                            cmdTransaccionFactory.Parameters.Add(pcod_consignacion);
                            cmdTransaccionFactory.Parameters.Add(pcod_caja);
                            cmdTransaccionFactory.Parameters.Add(pcod_cajero);
                            cmdTransaccionFactory.Parameters.Add(pfecha_consignacion);
                            cmdTransaccionFactory.Parameters.Add(pcod_banco);
                            cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                            cmdTransaccionFactory.Parameters.Add(pvalor_efectivo);
                            cmdTransaccionFactory.Parameters.Add(pvalor_cheque);
                            cmdTransaccionFactory.Parameters.Add(pobservacionescon);
                            cmdTransaccionFactory.Parameters.Add(pfecha_salida);

                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_CONSIGNACION_C";
                            cmdTransaccionFactory.ExecuteNonQuery();

                            if (pUsuario.programaGeneraLog)
                                DAauditoria.InsertarLog(pConsignacion, "CONSIGNACION", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Registrar Consignación");
                                //DAauditoria.InsertarLog(pConsignacion, pUsuario, pConsignacion.cod_consignacion, "CONSIGNACION", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                            //pConsignacion.cod_consignacion = Convert.ToInt64(pcod_consignacion.Value);

                        }
                     

                        // en esta porcion de codigo se inserta primero la operacion realizada con el fin de ir alimentar la operacion cod_ope
                        // en la tabla TransaccionesCaja

                        //--------------------------------------------------------------------------------------//

                        // se guarda la transaccion de la Consignacion 
                        //en esta porcion de codigo se inserta la transaccion realizada
                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pfecha_mov = cmdTransaccionFactory.CreateParameter();
                        pfecha_mov.ParameterName = "pfechamov";
                        pfecha_mov.Value = pConsignacion.fecha_consignacion;

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "ptipomov";
                        ptipo_mov.Value = "EGRESO";

                        DbParameter pcode_caja = cmdTransaccionFactory.CreateParameter();
                        pcode_caja.ParameterName = "pcodigocaja";
                        pcode_caja.Value = pConsignacion.cod_caja;

                        DbParameter pcode_oper = cmdTransaccionFactory.CreateParameter();
                        pcode_oper.ParameterName = "pcodigooper";
                        pcode_oper.Value = pcode_opera.Value;

                        DbParameter pcode_cajero = cmdTransaccionFactory.CreateParameter();
                        pcode_cajero.ParameterName = "pcodigocajero";
                        pcode_cajero.Value = pConsignacion.cod_cajero;

                        DbParameter pcode_usuario = cmdTransaccionFactory.CreateParameter();
                        pcode_usuario.ParameterName = "pcodigousuario";
                        pcode_usuario.Value = pUsuario.codusuario;

                        DbParameter pcode_moneda = cmdTransaccionFactory.CreateParameter();
                        pcode_moneda.ParameterName = "pcodigomoneda";
                        pcode_moneda.Value = pConsignacion.cod_moneda;

                        DbParameter pval_pago = cmdTransaccionFactory.CreateParameter();
                        pval_pago.ParameterName = "pvalorpago";
                        pval_pago.Value = pConsignacion.valor_consignacion_total;

                      
                        cmdTransaccionFactory.Parameters.Add(pfecha_mov);
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);
                        cmdTransaccionFactory.Parameters.Add(pcode_caja);
                        cmdTransaccionFactory.Parameters.Add(pcode_oper);
                        cmdTransaccionFactory.Parameters.Add(pcode_cajero);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuario);
                        cmdTransaccionFactory.Parameters.Add(pcode_moneda);
                        cmdTransaccionFactory.Parameters.Add(pval_pago);
                        

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_TRANSAC_CAJA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();
                       
                        //se inserta las opciones de la grilla en MovimientoCaja
                        CheckBox chkRecibe;
                        int codIngegr = 0;// codigo del movimientocaja para el tema de los cheques que ya se encuentran asociados a una caja
                       // Int64 codconsignacion = 0;// 
                        foreach (GridViewRow fila in gvConsignacion.Rows)
                        {

                            codIngegr = int.Parse(fila.Cells[0].Text);
                           
                            chkRecibe = (CheckBox)fila.FindControl("chkRecibe");
                            // si se escogio que el cheque se iba a consignar entonces lo ingresa en consignacioncheque y actualiza el estado del cheque de la caja en movimientocaja
                            // al final inserta el valor de la transaccion de cada uno de los cheques

                            if (chkRecibe.Checked == true)
                            {
                                // Actualizar el Cheque en MovimientoCaja esto nos dice de que el cheque fue consignado
                                cmdTransaccionFactory.Parameters.Clear();
                                DbParameter pcod_ingegr = cmdTransaccionFactory.CreateParameter();
                                pcod_ingegr.ParameterName = "pcodigoingegr";
                                pcod_ingegr.Value = codIngegr;
                                pcod_ingegr.DbType = DbType.Int64;
                                pcod_ingegr.Direction = ParameterDirection.Input;


                                DbParameter pcod_estado = cmdTransaccionFactory.CreateParameter();
                                pcod_estado.ParameterName = "pestado";
                                pcod_estado.Value = 1;
                                pcod_estado.DbType = DbType.Int64;
                                pcod_estado.Direction = ParameterDirection.Input;

                                cmdTransaccionFactory.Parameters.Add(pcod_ingegr);
                                cmdTransaccionFactory.Parameters.Add(pcod_estado);

                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_MOVIMIENTOCAJA_U";
                                cmdTransaccionFactory.ExecuteNonQuery();


                            }
                        }
                        
                        dbConnectionFactory.CerrarConexion(connection);

                        return pConsignacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsignacionData", "CrearConsignacion", ex);
                        return null;
                    }
                }
            }
        }

        /// <returns>Entidad Consignacion creada</returns>
        public Consignacion CrearConsignacionCheque(Consignacion pConsignacion, GridView gvConsignacion, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open(); GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;

          
                        //se inserta las opciones de la grilla en MovimientoCaja
                        CheckBox chkRecibe;
                        int codIngegr = 0;// codigo del movimientocaja para el tema de los cheques que ya se encuentran asociados a una caja
                        // Int64 codconsignacion = 0;// 
                        foreach (GridViewRow fila in gvConsignacion.Rows)
                        {

                            codIngegr = int.Parse(fila.Cells[0].Text);

                            chkRecibe = (CheckBox)fila.FindControl("chkRecibe");
                            // si se escogio que el cheque se iba a consignar entonces lo ingresa en consignacioncheque y actualiza el estado del cheque de la caja en movimientocaja
                            // al final inserta el valor de la transaccion de cada uno de los cheques

                            if (chkRecibe.Checked == true)
                            {
                               
                                //s eprocede a guaradar cada uno de los cheques seleccionados en la tabla ConsignacionCheque
                                cmdTransaccionFactory.Parameters.Clear();
                                DbParameter pcodi_ingegr = cmdTransaccionFactory.CreateParameter();
                                pcodi_ingegr.ParameterName = "pcodigoingegr";
                                pcodi_ingegr.Value = codIngegr;
                                pcodi_ingegr.DbType = DbType.Int64;
                                pcodi_ingegr.Direction = ParameterDirection.Input;

                                DbParameter pcodi_estado = cmdTransaccionFactory.CreateParameter();
                                pcodi_estado.ParameterName = "pestado";
                                pcodi_estado.Value = 1;
                                pcodi_estado.DbType = DbType.Int64;
                                pcodi_estado.Direction = ParameterDirection.Input;

                                DbParameter pconsignacion = cmdTransaccionFactory.CreateParameter();
                                pconsignacion.ParameterName = "pconsignacion";
                                pconsignacion.Value = pConsignacion.cod_consignacion;
                                pconsignacion.DbType = DbType.Int64;
                                pconsignacion.Direction = ParameterDirection.Input;



                                cmdTransaccionFactory.Parameters.Add(pcodi_ingegr);
                                cmdTransaccionFactory.Parameters.Add(pcodi_estado);
                                cmdTransaccionFactory.Parameters.Add(pconsignacion);


                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_CONSIGCHEQUE_C";
                                cmdTransaccionFactory.ExecuteNonQuery();

                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return pConsignacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsignacionData", "CrearConsignacion", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Crea un registro en la tabla CONSIGNACION de la base de datos
        /// </summary>
        /// <param name="pConsignacion">Entidad Consignacion</param>
        /// <returns>Entidad Consignacion creada</returns>
        public Consignacion CrearConsignacionTesoreria(Consignacion pConsignacion, GridView gvConsignacion,ref Int64 pCOD_OPE, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open(); GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        DbParameter pcode_opera = cmdTransaccionFactory.CreateParameter();
                        pcode_opera.ParameterName = "pcodigooper";
                        pcode_opera.Value = pConsignacion.cod_ope;
                        pcode_opera.Direction = ParameterDirection.Output;

                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        p_ip.Value = pUsuario.IP;

                        DbParameter pcode_tope = cmdTransaccionFactory.CreateParameter();
                        pcode_tope.ParameterName = "pcodigotipoope";
                        pcode_tope.Value = pConsignacion.tipo_ope;

                        DbParameter pcode_usuari = cmdTransaccionFactory.CreateParameter();
                        pcode_usuari.ParameterName = "pcodigousuario";
                        pcode_usuari.Value = pUsuario.codusuario;

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "pcodigooficina";
                        pcode_oficina.Value = pUsuario.cod_oficina;

                        DbParameter pcodi_caja = cmdTransaccionFactory.CreateParameter();
                        pcodi_caja.ParameterName = "pcodigocaja";
                        pcodi_caja.Value = 0;

                        DbParameter pcodi_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodi_cajero.ParameterName = "pcodigocajero";
                        pcodi_cajero.Value = pConsignacion.cod_cajero;

                        DbParameter pfecha_cal = cmdTransaccionFactory.CreateParameter();
                        pfecha_cal.ParameterName = "pfechaoper";
                        pfecha_cal.Value = pConsignacion.fecha_consignacion;

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "pobservaciones";
                        pobservaciones.Value = "  ";

                        cmdTransaccionFactory.Parameters.Add(pcode_opera);
                        cmdTransaccionFactory.Parameters.Add(pcode_tope);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuari);
                        cmdTransaccionFactory.Parameters.Add(pcode_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcodi_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodi_cajero);
                        cmdTransaccionFactory.Parameters.Add(pfecha_cal);
                        cmdTransaccionFactory.Parameters.Add(p_ip);
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_OPERACION_CAJA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pCOD_OPE = Convert.ToInt64(pcode_opera.Value);
                        Int64 pCod_Consignacion = 0;

                        if (pConsignacion.valor_efectivo > 0)
                        {
                            cmdTransaccionFactory.Parameters.Clear();

                            DbParameter pcodey_opera = cmdTransaccionFactory.CreateParameter();
                            pcodey_opera.ParameterName = "pcodigooper";
                            pcodey_opera.Value = pcode_opera.Value;
                            pcodey_opera.Size = 8;
                            pcodey_opera.DbType = DbType.Int64;
                            pcodey_opera.Direction = ParameterDirection.Input;

                            DbParameter pcod_consignacion = cmdTransaccionFactory.CreateParameter();
                            pcod_consignacion.ParameterName = "pcodigoconsignacion";
                            pcod_consignacion.Value = pConsignacion.cod_consignacion;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Input;

                            DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                            pcod_caja.ParameterName = "pcodigocaja";
                            pcod_caja.Value = 0;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Input;

                            DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                            pcod_cajero.ParameterName = "pcodigocajero";
                            pcod_cajero.Value = pConsignacion.cod_cajero;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Input;

                            DbParameter pfecha_consignacion = cmdTransaccionFactory.CreateParameter();
                            pfecha_consignacion.ParameterName = "pfechaconsignacion";
                            pfecha_consignacion.Value = pConsignacion.fecha_consignacion;
                            pfecha_consignacion.DbType = DbType.DateTime;
                            pfecha_consignacion.Direction = ParameterDirection.Input;
                            pfecha_consignacion.Size = 7;

                            DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                            pcod_banco.ParameterName = "pcodigobanco";
                            pcod_banco.Value = pConsignacion.cod_banco;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Input;

                            DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                            pcod_moneda.ParameterName = "pcodigomoneda";
                            pcod_moneda.Value = pConsignacion.cod_moneda;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Input;

                            DbParameter pvalor_efectivo = cmdTransaccionFactory.CreateParameter();
                            pvalor_efectivo.ParameterName = "pvalorefectivo";
                            pvalor_efectivo.Value = pConsignacion.valor_efectivo;
                            pvalor_efectivo.DbType = DbType.Decimal;
                            pvalor_efectivo.Direction = ParameterDirection.Input;

                            DbParameter pvalor_cheque = cmdTransaccionFactory.CreateParameter();
                            pvalor_cheque.ParameterName = "pvalorcheque";
                            pvalor_cheque.Value = pConsignacion.valor_cheque;
                            pvalor_cheque.DbType = DbType.Decimal;
                            pvalor_cheque.Direction = ParameterDirection.Input;

                            DbParameter pobservacionescon = cmdTransaccionFactory.CreateParameter();
                            pobservacionescon.ParameterName = "pobservaciones";
                            pobservacionescon.Value = pConsignacion.observaciones;
                            pobservacionescon.DbType = DbType.AnsiString;
                            pobservacionescon.Direction = ParameterDirection.Input;
                            pobservacionescon.Size = 150;

                            DbParameter pfecha_salida = cmdTransaccionFactory.CreateParameter();
                            pfecha_salida.ParameterName = "pfechasalida";
                            pfecha_salida.Value = pConsignacion.fecha_salida;
                            pfecha_salida.DbType = DbType.DateTime;
                            pfecha_salida.Direction = ParameterDirection.Input;
                            pfecha_salida.Size = 7;

                            DbParameter pcodcuenta = cmdTransaccionFactory.CreateParameter();
                            pcodcuenta.ParameterName = "pcodcuenta";
                            pcodcuenta.Value = Convert.ToString(pConsignacion.Cuenta);
                            pobservaciones.DbType = DbType.AnsiString;
                            pobservaciones.Direction = ParameterDirection.Input;


                            cmdTransaccionFactory.Parameters.Add(pcodey_opera);
                            cmdTransaccionFactory.Parameters.Add(pcod_consignacion);
                            cmdTransaccionFactory.Parameters.Add(pcod_caja);
                            cmdTransaccionFactory.Parameters.Add(pcod_cajero);
                            cmdTransaccionFactory.Parameters.Add(pfecha_consignacion);
                            cmdTransaccionFactory.Parameters.Add(pcod_banco);
                            cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                            cmdTransaccionFactory.Parameters.Add(pvalor_efectivo);
                            cmdTransaccionFactory.Parameters.Add(pvalor_cheque);
                            cmdTransaccionFactory.Parameters.Add(pobservacionescon);
                            cmdTransaccionFactory.Parameters.Add(pfecha_salida);
                            cmdTransaccionFactory.Parameters.Add(pcodcuenta);

                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_CONSIGNACION_C";
                            cmdTransaccionFactory.ExecuteNonQuery();

                            if (pUsuario.programaGeneraLog)
                                DAauditoria.InsertarLog(pConsignacion, "CONSIGNACION", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Registrar Consignación");
                                //DAauditoria.InsertarLog(pConsignacion, pUsuario, pConsignacion.cod_consignacion, "CONSIGNACION", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                            pConsignacion.cod_consignacion = Convert.ToInt64(pcod_consignacion.Value);
                            pCod_Consignacion = pConsignacion.cod_consignacion;
                            //en esta porcion de codigo se va a enviar los saldos realizados 
                            //por el cajero en la caja especifica en una fecha

                            cmdTransaccionFactory.Parameters.Clear();
                            DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                            pfecha.ParameterName = "pfecha";
                            pfecha.Value = pConsignacion.fecha_consignacion;
                            pfecha.DbType = DbType.DateTime;
                            pfecha.Direction = ParameterDirection.Input;
                            pfecha.Size = 7;

                            DbParameter pcoda_caja = cmdTransaccionFactory.CreateParameter();
                            pcoda_caja.ParameterName = "pcodigocaja";
                            pcoda_caja.Value = 0;
                            pcoda_caja.Size = 8;
                            pcoda_caja.DbType = DbType.Int32;
                            pcoda_caja.Direction = ParameterDirection.Input;

                            DbParameter pcoda_cajero = cmdTransaccionFactory.CreateParameter();
                            pcoda_cajero.ParameterName = "pcodigocajero";
                            pcoda_cajero.Value = pConsignacion.cod_cajero;
                            pcoda_cajero.Size = 8;
                            pcoda_cajero.DbType = DbType.Int32;
                            pcoda_cajero.Direction = ParameterDirection.Input;

                            DbParameter pcodi_moneda = cmdTransaccionFactory.CreateParameter();
                            pcodi_moneda.ParameterName = "pcodigomoneda";
                            pcodi_moneda.Value = pConsignacion.cod_moneda;
                            pcodi_moneda.Size = 8;
                            pcodi_moneda.DbType = DbType.Int32;
                            pcodi_moneda.Direction = ParameterDirection.Input;

                            DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                            pvalor.ParameterName = "pvalor";
                            pvalor.Value = pConsignacion.valor_efectivo;
                            pvalor.DbType = DbType.Decimal;
                            pvalor.Direction = ParameterDirection.Input;

                            cmdTransaccionFactory.Parameters.Add(pfecha);

                            cmdTransaccionFactory.Parameters.Add(pcoda_cajero);
                            cmdTransaccionFactory.Parameters.Add(pcodi_moneda);
                            cmdTransaccionFactory.Parameters.Add(pvalor);

                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_TES_SALDOCAJA";
                            cmdTransaccionFactory.ExecuteNonQuery();

                        }
                        else
                        {

                            cmdTransaccionFactory.Parameters.Clear();

                            DbParameter pcodey_opera = cmdTransaccionFactory.CreateParameter();
                            pcodey_opera.ParameterName = "pcodigooper";
                            pcodey_opera.Value = pcode_opera.Value;
                            pcodey_opera.Size = 8;
                            pcodey_opera.DbType = DbType.Int64;
                            pcodey_opera.Direction = ParameterDirection.Input;

                            DbParameter pcod_consignacion = cmdTransaccionFactory.CreateParameter();
                            pcod_consignacion.ParameterName = "pcodigoconsignacion";
                            pcod_consignacion.Value = pConsignacion.cod_consignacion;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Input;

                            DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                            pcod_caja.ParameterName = "pcodigocaja";
                            pcod_caja.Value = 0;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Input;

                            DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                            pcod_cajero.ParameterName = "pcodigocajero";
                            pcod_cajero.Value = pConsignacion.cod_cajero;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Input;

                            DbParameter pfecha_consignacion = cmdTransaccionFactory.CreateParameter();
                            pfecha_consignacion.ParameterName = "pfechaconsignacion";
                            pfecha_consignacion.Value = pConsignacion.fecha_consignacion;
                            pfecha_consignacion.DbType = DbType.DateTime;
                            pfecha_consignacion.Direction = ParameterDirection.Input;
                            pfecha_consignacion.Size = 7;

                            DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                            pcod_banco.ParameterName = "pcodigobanco";
                            pcod_banco.Value = pConsignacion.cod_banco;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Input;

                            DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                            pcod_moneda.ParameterName = "pcodigomoneda";
                            pcod_moneda.Value = pConsignacion.cod_moneda;
                            pcod_consignacion.Size = 8;
                            pcod_consignacion.DbType = DbType.Int64;
                            pcod_consignacion.Direction = ParameterDirection.Input;

                            DbParameter pvalor_efectivo = cmdTransaccionFactory.CreateParameter();
                            pvalor_efectivo.ParameterName = "pvalorefectivo";
                            pvalor_efectivo.Value = pConsignacion.valor_efectivo;
                            pvalor_efectivo.DbType = DbType.Decimal;
                            pvalor_efectivo.Direction = ParameterDirection.Input;

                            DbParameter pvalor_cheque = cmdTransaccionFactory.CreateParameter();
                            pvalor_cheque.ParameterName = "pvalorcheque";
                            pvalor_cheque.Value = pConsignacion.valor_cheque;
                            pvalor_cheque.DbType = DbType.Decimal;
                            pvalor_cheque.Direction = ParameterDirection.Input;

                            DbParameter pobservacionescon = cmdTransaccionFactory.CreateParameter();
                            pobservacionescon.ParameterName = "pobservaciones";
                            pobservacionescon.Value = pConsignacion.observaciones;
                            pobservacionescon.DbType = DbType.AnsiString;
                            pobservacionescon.Direction = ParameterDirection.Input;
                            pobservacionescon.Size = 150;

                            DbParameter pfecha_salida = cmdTransaccionFactory.CreateParameter();
                            pfecha_salida.ParameterName = "pfechasalida";
                            pfecha_salida.Value = pConsignacion.fecha_salida;
                            pfecha_salida.DbType = DbType.DateTime;
                            pfecha_salida.Direction = ParameterDirection.Input;
                            pfecha_salida.Size = 7;
                            DbParameter pcodcuenta = cmdTransaccionFactory.CreateParameter();
                            pcodcuenta.ParameterName = "pcodcuenta";
                            pcodcuenta.Value = Convert.ToString(pConsignacion.Cuenta);

                            cmdTransaccionFactory.Parameters.Add(pcodcuenta);
                            cmdTransaccionFactory.Parameters.Add(pcodey_opera);
                            cmdTransaccionFactory.Parameters.Add(pcod_consignacion);
                            cmdTransaccionFactory.Parameters.Add(pcod_caja);
                            cmdTransaccionFactory.Parameters.Add(pcod_cajero);
                            cmdTransaccionFactory.Parameters.Add(pfecha_consignacion);
                            cmdTransaccionFactory.Parameters.Add(pcod_banco);
                            cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                            cmdTransaccionFactory.Parameters.Add(pvalor_efectivo);
                            cmdTransaccionFactory.Parameters.Add(pvalor_cheque);
                            cmdTransaccionFactory.Parameters.Add(pobservacionescon);
                            cmdTransaccionFactory.Parameters.Add(pfecha_salida);

                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_CONSIGNACION_C";
                            cmdTransaccionFactory.ExecuteNonQuery();

                            if (pUsuario.programaGeneraLog)
                                DAauditoria.InsertarLog(pConsignacion, "CONSIGNACION", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Registrar Consignación");
                                //DAauditoria.InsertarLog(pConsignacion, pUsuario, pConsignacion.cod_consignacion, "CONSIGNACION", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                            pConsignacion.cod_consignacion = Convert.ToInt64(pcod_consignacion.Value);
                            pCod_Consignacion = pConsignacion.cod_consignacion;
                        }

                        // en esta porcion de codigo se inserta primero la operacion realizada con el fin de ir alimentar la operacion cod_ope
                        // en la tabla TransaccionesCaja

                        //--------------------------------------------------------------------------------------//

                        // se guarda la transaccion de la Consignacion 
                        //en esta porcion de codigo se inserta la transaccion realizada
                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pfecha_mov = cmdTransaccionFactory.CreateParameter();
                        pfecha_mov.ParameterName = "pfechamov";
                        pfecha_mov.Value = pConsignacion.fecha_consignacion;

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "ptipomov";
                        ptipo_mov.Value = "EGRESO";

                        DbParameter pcode_caja = cmdTransaccionFactory.CreateParameter();
                        pcode_caja.ParameterName = "pcodigocaja";
                        pcode_caja.Value =0;

                        DbParameter pcode_oper = cmdTransaccionFactory.CreateParameter();
                        pcode_oper.ParameterName = "pcodigooper";
                        pcode_oper.Value = pcode_opera.Value;

                        DbParameter pcode_cajero = cmdTransaccionFactory.CreateParameter();
                        pcode_cajero.ParameterName = "pcodigocajero";
                        pcode_cajero.Value = pConsignacion.cod_cajero;

                        DbParameter pcode_usuario = cmdTransaccionFactory.CreateParameter();
                        pcode_usuario.ParameterName = "pcodigousuario";
                        pcode_usuario.Value = pUsuario.codusuario;

                        DbParameter pcode_moneda = cmdTransaccionFactory.CreateParameter();
                        pcode_moneda.ParameterName = "pcodigomoneda";
                        pcode_moneda.Value = pConsignacion.cod_moneda;

                        DbParameter pval_pago = cmdTransaccionFactory.CreateParameter();
                        pval_pago.ParameterName = "pvalorpago";
                        pval_pago.Value = pConsignacion.valor_consignacion_total;

                        cmdTransaccionFactory.Parameters.Add(pfecha_mov);
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);
                        cmdTransaccionFactory.Parameters.Add(pcode_caja);
                        cmdTransaccionFactory.Parameters.Add(pcode_oper);
                        cmdTransaccionFactory.Parameters.Add(pcode_cajero);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuario);
                        cmdTransaccionFactory.Parameters.Add(pcode_moneda);
                        cmdTransaccionFactory.Parameters.Add(pval_pago);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_TRANSAC_CAJA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //se inserta las opciones de la grilla en MovimientoCaja
                        CheckBox chkRecibe;
                        int codIngegr = 0;// codigo del movimientocaja para el tema de los cheques que ya se encuentran asociados a una caja

                        foreach (GridViewRow fila in gvConsignacion.Rows)
                        {

                            codIngegr = int.Parse(fila.Cells[0].Text);

                            chkRecibe = (CheckBox)fila.FindControl("chkRecibe");
                            // si se escogio que el cheque se iba a consignar entonces lo ingresa en consignacioncheque y actualiza el estado del cheque de la caja en movimientocaja
                            // al final inserta el valor de la transaccion de cada uno de los cheques

                            if (chkRecibe.Checked == true)
                            {
                                // Actualizar el Cheque en MovimientoCaja esto nos dice de que el cheque fue consignado
                                cmdTransaccionFactory.Parameters.Clear();
                                DbParameter pcod_ingegr = cmdTransaccionFactory.CreateParameter();
                                pcod_ingegr.ParameterName = "pcodigoingegr";
                                pcod_ingegr.Value = codIngegr;
                                pcod_ingegr.DbType = DbType.Int64;
                                pcod_ingegr.Direction = ParameterDirection.Input;

                                DbParameter pcod_estado = cmdTransaccionFactory.CreateParameter();
                                pcod_estado.ParameterName = "pestado";
                                pcod_estado.Value = 1;
                                pcod_estado.DbType = DbType.Int64;
                                pcod_estado.Direction = ParameterDirection.Input;

                                cmdTransaccionFactory.Parameters.Add(pcod_ingegr);
                                cmdTransaccionFactory.Parameters.Add(pcod_estado);

                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_MOVIMIENTOCAJA_U";
                                cmdTransaccionFactory.ExecuteNonQuery();

                                //s eprocede a guaradar cada uno de los cheques seleccionados en la tabla ConsignacionCheque
                                cmdTransaccionFactory.Parameters.Clear();
                                DbParameter pcodi_ingegr = cmdTransaccionFactory.CreateParameter();
                                pcodi_ingegr.ParameterName = "pcodigoingegr";
                                pcodi_ingegr.Value = codIngegr;
                                pcodi_ingegr.DbType = DbType.Int64;
                                pcodi_ingegr.Direction = ParameterDirection.Input;

                                DbParameter pcodi_estado = cmdTransaccionFactory.CreateParameter();
                                pcodi_estado.ParameterName = "pestado";
                                pcodi_estado.Value = 0;
                                pcodi_estado.DbType = DbType.Int64;
                                pcodi_estado.Direction = ParameterDirection.Input;

                                DbParameter pconsignacion = cmdTransaccionFactory.CreateParameter();
                                pconsignacion.ParameterName = "pconsignacion";
                                pconsignacion.Value = pCod_Consignacion;
                                pconsignacion.DbType = DbType.Int64;
                                pconsignacion.Direction = ParameterDirection.Input;

                                cmdTransaccionFactory.Parameters.Add(pcodi_ingegr);
                                cmdTransaccionFactory.Parameters.Add(pcodi_estado);
                                cmdTransaccionFactory.Parameters.Add(pconsignacion);

                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_CONSIGCHEQUE_C";
                                cmdTransaccionFactory.ExecuteNonQuery();

                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return pConsignacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsignacionData", "CrearConsignacion", ex);
                        return null;
                    }
                }
            }
        }


        

        /// <summary>
        /// Modifica un registro en la tabla CONSIGNACION de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Consignacion modificada</returns>
        public Consignacion ModificarConsignacion(Consignacion pConsignacion, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_CONSIGNACION = cmdTransaccionFactory.CreateParameter();
                        pCOD_CONSIGNACION.ParameterName = param + "COD_CONSIGNACION";
                        pCOD_CONSIGNACION.Value = pConsignacion.cod_consignacion;

                        DbParameter pCOD_CAJA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CAJA.ParameterName = param + "COD_CAJA";
                        pCOD_CAJA.Value = pConsignacion.cod_caja;

                        DbParameter pCOD_CAJERO = cmdTransaccionFactory.CreateParameter();
                        pCOD_CAJERO.ParameterName = param + "COD_CAJERO";
                        pCOD_CAJERO.Value = pConsignacion.cod_cajero;

                        DbParameter pFECHA_CONSIGNACION = cmdTransaccionFactory.CreateParameter();
                        pFECHA_CONSIGNACION.ParameterName = param + "FECHA_CONSIGNACION";
                        pFECHA_CONSIGNACION.Value = pConsignacion.fecha_consignacion;

                        DbParameter pCOD_BANCO = cmdTransaccionFactory.CreateParameter();
                        pCOD_BANCO.ParameterName = param + "COD_BANCO";
                        pCOD_BANCO.Value = pConsignacion.cod_banco;

                        DbParameter pCOD_MONEDA = cmdTransaccionFactory.CreateParameter();
                        pCOD_MONEDA.ParameterName = param + "COD_MONEDA";
                        pCOD_MONEDA.Value = pConsignacion.cod_moneda;

                        DbParameter pVALOR_EFECTIVO = cmdTransaccionFactory.CreateParameter();
                        pVALOR_EFECTIVO.ParameterName = param + "VALOR_EFECTIVO";
                        pVALOR_EFECTIVO.Value = pConsignacion.valor_efectivo;

                        DbParameter pVALOR_CHEQUE = cmdTransaccionFactory.CreateParameter();
                        pVALOR_CHEQUE.ParameterName = param + "VALOR_CHEQUE";
                        pVALOR_CHEQUE.Value = pConsignacion.valor_cheque;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = param + "OBSERVACIONES";
                        pOBSERVACIONES.Value = pConsignacion.observaciones;

                        DbParameter pFECHA_SALIDA = cmdTransaccionFactory.CreateParameter();
                        pFECHA_SALIDA.ParameterName = param + "FECHA_SALIDA";
                        pFECHA_SALIDA.Value = pConsignacion.fecha_salida;

                        cmdTransaccionFactory.Parameters.Add(pCOD_CONSIGNACION);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CAJA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CAJERO);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_CONSIGNACION);
                        cmdTransaccionFactory.Parameters.Add(pCOD_BANCO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MONEDA);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_EFECTIVO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_CHEQUE);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_SALIDA);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_Caja_CONSIGNACION_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                          DAauditoria.InsertarLog(pConsignacion, pUsuario, pConsignacion.cod_consignacion, "CONSIGNACION", Accion.Modificar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);

                        return pConsignacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsignacionData", "ModificarConsignacion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla CONSIGNACION de la base de datos
        /// </summary>
        /// <param name="pId">identificador de CONSIGNACION</param>
        public void EliminarConsignacion(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Consignacion pConsignacion = new Consignacion();

                        if (pUsuario.programaGeneraLog)
                            pConsignacion = ConsultarConsignacion(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_CONSIGNACION = cmdTransaccionFactory.CreateParameter();
                        pCOD_CONSIGNACION.ParameterName = param + "COD_CONSIGNACION";
                        pCOD_CONSIGNACION.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_CONSIGNACION);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_Caja_CONSIGNACION_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pConsignacion, pUsuario, pConsignacion.cod_consignacion, "CONSIGNACION", Accion.Eliminar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsignacionData", "InsertarConsignacion", ex);
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro en la tabla CONSIGNACION de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla CONSIGNACION</param>
        /// <returns>Entidad Consignacion consultado</returns>
        public Consignacion ConsultarConsignacion_(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Consignacion entidad = new Consignacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_CONSIGNACION = cmdTransaccionFactory.CreateParameter();
                        pCOD_CONSIGNACION.ParameterName = param + "COD_CONSIGNACION";
                        pCOD_CONSIGNACION.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_CONSIGNACION);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_&Modulo&_CONSIGNACION_R";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CONSIGNACION"] == DBNull.Value) entidad.cod_consignacion = Convert.ToInt64(resultado["COD_CONSIGNACION"]);
                            if (resultado["COD_CAJA"] == DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                            if (resultado["COD_CAJERO"] == DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["COD_CAJERO"]);
                            if (resultado["FECHA_CONSIGNACION"] == DBNull.Value) entidad.fecha_consignacion = Convert.ToDateTime(resultado["FECHA_CONSIGNACION"]);
                            if (resultado["COD_BANCO"] == DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["COD_BANCO"]);
                            if (resultado["COD_MONEDA"] == DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["VALOR_EFECTIVO"] == DBNull.Value) entidad.valor_efectivo = Convert.ToInt64(resultado["VALOR_EFECTIVO"]);
                            if (resultado["VALOR_CHEQUE"] == DBNull.Value) entidad.valor_cheque = Convert.ToInt64(resultado["VALOR_CHEQUE"]);
                            if (resultado["OBSERVACIONES"] == DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["FECHA_SALIDA"] == DBNull.Value) entidad.fecha_salida = Convert.ToDateTime(resultado["FECHA_SALIDA"]);
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
                        BOExcepcion.Throw("ConsignacionData", "ConsultarConsignacion", ex);
                        return null;
                    }
                }
            }
        }


        /// Obtiene un registro de la tabla Cajero, Caja y Oficina de la base de datos
        /// </summary>
        /// <param name="pUsuario">identificador del usuario</param>
        /// <returns>Oficina consultada</returns>
        public Consignacion ConsultarCajero(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Consignacion entidad = new Consignacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT b.cod_cajero codcajero, a.nombre nomcajero, c.cod_caja codcaja, c.nombre nomcaja, d.cod_oficina codoficina, d.nombre nomoficina,
                                        (Select decode(max(x.fecha_proceso), null, sysdate, max(x.fecha_proceso)) From procesooficina x Where x.cod_oficina = d.cod_oficina) fechaproceso  
                                        FROM USUARIOS a INNER JOIN CAJERO b ON a.codusuario = b.cod_persona INNER JOIN CAJA c ON b.cod_caja = c.cod_caja, OFICINA d 
                                        WHERE a.CODUSUARIO = " + pUsuario.codusuario.ToString() + " AND C.COD_OFICINA = D.COD_OFICINA";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
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
                            if (resultado["fechaproceso"] != DBNull.Value) entidad.fecha_consignacion = Convert.ToDateTime(resultado["fechaproceso"]);
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
                        BOExcepcion.Throw("ConsignacionData", "ConsultarCajero", ex);
                        return null;
                    }

                }

            }
        }
        /// Obtiene un registro de la tabla Cajero, Caja y Oficina de la base de datos
        /// </summary>
        /// <param name="pUsuario">identificador del usuario</param>
        /// <returns>Oficina consultada</returns>
        public Consignacion ConsultarConsignacion(Int64 pId,Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Consignacion entidad = new Consignacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select * from consignacion where cod_ope=" + pId;
                        connection.Open(); GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["cod_consignacion"] != DBNull.Value) entidad.cod_consignacion = Convert.ToInt64(resultado["cod_consignacion"]);
                       
                        }
                        

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsignacionData", "ConsultarConsignacion", ex);
                        return null;
                    }

                }

            }
        }



        public Consignacion ConsultarUsuario(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Consignacion entidad = new Consignacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT a.codusuario codcajero, a.nombre nomcajero, 0 codcaja, '' nomcaja, d.cod_oficina codoficina, d.nombre nomoficina,
                                        (Select decode(max(x.fecha_proceso), null, sysdate, max(x.fecha_proceso)) From procesooficina x Where x.cod_oficina = d.cod_oficina) fechaproceso  
                                        FROM USUARIOS a INNER JOIN OFICINA d ON d.COD_OFICINA = a.COD_OFICINA
                                        WHERE a.CODUSUARIO = " + pUsuario.codusuario.ToString();
                        connection.Open(); GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
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
                            if (resultado["fechaproceso"] != DBNull.Value) entidad.fecha_consignacion = Convert.ToDateTime(resultado["fechaproceso"]);
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
                        BOExcepcion.Throw("ConsignacionData", "ConsultarCajero", ex);
                        return null;
                    }

                }

            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla CONSIGNACION dados unos filtros
        /// </summary>
        /// <param name="pCONSIGNACION">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Consignacion obtenidos</returns>
        public List<Consignacion> ListarConsignacion(Consignacion pConsignacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Consignacion> lstConsignacion = new List<Consignacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CONSIGNACION  where cod_ope = " + pConsignacion.cod_ope;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Consignacion entidad = new Consignacion();

                            if (resultado["COD_CONSIGNACION"] != DBNull.Value) entidad.cod_consignacion = Convert.ToInt64(resultado["COD_CONSIGNACION"]);
                            if (resultado["COD_CAJA"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                            if (resultado["COD_CAJERO"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["COD_CAJERO"]);
                            if (resultado["FECHA_CONSIGNACION"] != DBNull.Value) entidad.fecha_consignacion = Convert.ToDateTime(resultado["FECHA_CONSIGNACION"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["COD_BANCO"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["VALOR_EFECTIVO"] != DBNull.Value) entidad.valor_efectivo = Convert.ToInt64(resultado["VALOR_EFECTIVO"]);
                            if (resultado["VALOR_CHEQUE"] != DBNull.Value) entidad.valor_cheque = Convert.ToInt64(resultado["VALOR_CHEQUE"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["FECHA_SALIDA"] != DBNull.Value) entidad.fecha_salida = Convert.ToDateTime(resultado["FECHA_SALIDA"]);

                            lstConsignacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsignacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsignacionData", "ListarConsignacion", ex);
                        return null;
                    }
                }
            }
        }
        public List<Consignacion> ListarConsignacionCheque(Int64 pConsignacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Consignacion> lstConsignacion = new List<Consignacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select ch.*,m.num_documento as cheque,c.fecha_consignacion," +
                                      " m.cod_banco,b.nombrebanco,m.valor,tm.descripcion as moneda " +
                                      " from consignacioncheque ch "+ 
                                      " left join movimientocaja m on m.cod_movimiento=ch.cod_ingegr " +
                                      " left join consignacion c on c.cod_consignacion=ch.cod_consignacion "+
                                      " left join bancos b on b.cod_banco=m.cod_banco "+ 
                                      " left join tipomoneda tm on tm.cod_moneda=m.cod_moneda " +                        
                                      " where c.cod_consignacion  = " + pConsignacion;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Consignacion entidad = new Consignacion();

                            if (resultado["cheque"] != DBNull.Value) entidad.documento = Convert.ToString(resultado["cheque"]);
                            if (resultado["FECHA_CONSIGNACION"] != DBNull.Value) entidad.fecha_consignacion = Convert.ToDateTime(resultado["FECHA_CONSIGNACION"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["COD_BANCO"]);
                            if (resultado["nombrebanco"] != DBNull.Value) entidad.nom_banco = Convert.ToString(resultado["nombrebanco"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor_cheque = Convert.ToInt64(resultado["valor"]);
                          
                            lstConsignacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsignacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsignacionData", "ListarConsignacion", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Modifica un registro en la tabla CONSIGNACIONCHEQUE de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Consignacion modificada</returns>
        public Consignacion GrabarCanje(Consignacion pConsignacionCheque, MotivoDevChe pMotivoDevChe, Usuario pUsuario)
        {
            Int32 operacion = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open(); GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        DbParameter pCOD_MOVIMIENTO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MOVIMIENTO.ParameterName =  "p_cod_ingegr";
                        pCOD_MOVIMIENTO.Value = pConsignacionCheque.cod_consignacion;
                        pCOD_MOVIMIENTO.Direction = ParameterDirection.Input;
                        pCOD_MOVIMIENTO.DbType = DbType.Int64;

                        DbParameter pCOD_MOTIVO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MOTIVO.ParameterName =  "p_cod_motivo";
                        pCOD_MOTIVO.Value = Convert.ToInt64(pMotivoDevChe.cod_motivo);
                        pCOD_MOTIVO.Direction = ParameterDirection.Input;
                        pCOD_MOTIVO.DbType = DbType.Int64;

                        DbParameter pFECHA_CANJE = cmdTransaccionFactory.CreateParameter();
                        pFECHA_CANJE.ParameterName = "p_fecha_canje";
                        pFECHA_CANJE.Value = pConsignacionCheque.fecha_consignacion;
                        pFECHA_CANJE.Direction = ParameterDirection.Input;
                        pFECHA_CANJE.DbType = DbType.DateTime;

                        DbParameter pFECHA_DEVUELTO = cmdTransaccionFactory.CreateParameter();
                        pFECHA_DEVUELTO.ParameterName =  "p_fecha_devuelto";
                        pFECHA_DEVUELTO.Value = pConsignacionCheque.fecha_consignacion;
                        pFECHA_DEVUELTO.Direction = ParameterDirection.Input;
                        pFECHA_DEVUELTO.DbType = DbType.DateTime;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_estado";
                        pESTADO.Value = pConsignacionCheque.estado;
                        pESTADO.Direction = ParameterDirection.Input;
                        pESTADO.DbType = DbType.Int64;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "p_observaciones";
                        pOBSERVACIONES.Value = pConsignacionCheque.observaciones;
                        pOBSERVACIONES.Direction = ParameterDirection.Input;
                        pOBSERVACIONES.DbType = DbType.String;

                        DbParameter p_operacion = cmdTransaccionFactory.CreateParameter();
                        p_operacion.ParameterName = "p_operacion";
                        p_operacion.Value = pConsignacionCheque.cod_ope;
                        p_operacion.Direction = ParameterDirection.Input;
                        p_operacion.DbType = DbType.Int64;

                        DbParameter pUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pUSUARIO.ParameterName = "p_Usuario";
                        pUSUARIO.Value = pUsuario.codusuario;
                        pUSUARIO.Direction = ParameterDirection.Input;
                        pUSUARIO.DbType = DbType.Int64;

                        cmdTransaccionFactory.Parameters.Add(pCOD_MOVIMIENTO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MOTIVO);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_CANJE);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_DEVUELTO);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        cmdTransaccionFactory.Parameters.Add(p_operacion);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIO);
       
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CHE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pConsignacionCheque.cod_ope != Int64.MinValue)
                        {
                            operacion = Convert.ToInt32(pConsignacionCheque.cod_ope);                           
                            pConsignacionCheque.cod_ope = operacion;                            
                        }
                      
 
                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pConsignacionCheque, "CONSIGNACIONCHEQUE", pUsuario, Accion.Modificar.ToString(), TipoAuditoria.CajaFinanciera, "Modificar consignación cheque");

                        dbConnectionFactory.CerrarConexion(connection);                   


                        return pConsignacionCheque;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsignacionData", "ModificarConsignacion", ex);
                        return null;
                    }
                }
            }
        }

    }
}