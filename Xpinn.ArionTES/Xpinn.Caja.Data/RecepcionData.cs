using System;
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
    /// Objeto de acceso a datos para la tabla Recepcion
    /// </summary>    
    public class RecepcionData:GlobalData  
    {
          protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Recepcion
        /// </summary>
        public RecepcionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

         /// <summary>
        /// Crea una entidad Recepcion en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Recepcion</param>
        /// <returns>Entidad creada</returns>
        public Recepcion InsertarRecepcion(Recepcion pEntidad, GridView gvTraslados, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                         //se inserta las opciones de la grilla en TipoOperacion
                        CheckBox chkRecibe;
                        
                        int codTraslado=0;//captura el valor del codigo de Traslado
                        int codMoneda = 0;
                        decimal valore = 0;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        foreach (GridViewRow fila in gvTraslados.Rows)
                        {
                            //se captura la opcion chequeda en el grid
                            codTraslado = int.Parse(fila.Cells[0].Text);
                            codMoneda = int.Parse(fila.Cells[1].Text);
                            valore = decimal.Parse(fila.Cells[7].Text);

                            chkRecibe = (CheckBox)fila.FindControl("chkRecibe");

                            if (chkRecibe.Checked == true)
                            {

                                // en esta porcion de codigo se inserta primero la operacion realizada con el fin de ir alimentar la operacion cod_ope
                                // en la tabla TransaccionesCaja

                                cmdTransaccionFactory.Parameters.Clear();
                                DbParameter pcode_opera = cmdTransaccionFactory.CreateParameter();
                                pcode_opera.ParameterName = "pcodigooper";
                                pcode_opera.Value = pEntidad.cod_ope;
                                pcode_opera.Direction = ParameterDirection.Output;

                                DbParameter pcode_tope = cmdTransaccionFactory.CreateParameter();
                                pcode_tope.ParameterName = "pcodigotipoope";
                                pcode_tope.Value = pEntidad.tipo_ope;

                                DbParameter pcode_usuari = cmdTransaccionFactory.CreateParameter();
                                pcode_usuari.ParameterName = "pcodigousuario";
                                pcode_usuari.Value = pUsuario.codusuario;

                                DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                                pcode_oficina.ParameterName = "pcodigooficina";
                                pcode_oficina.Value = pEntidad.cod_oficina;

                                DbParameter pcodi_caja = cmdTransaccionFactory.CreateParameter();
                                pcodi_caja.ParameterName = "pcodigocaja";
                                pcodi_caja.Value = pEntidad.cod_caja;

                                DbParameter pcodi_cajero = cmdTransaccionFactory.CreateParameter();
                                pcodi_cajero.ParameterName = "pcodigocajero";
                                pcodi_cajero.Value = pEntidad.cod_cajero;

                                DbParameter pfecha_cal = cmdTransaccionFactory.CreateParameter();
                                pfecha_cal.ParameterName = "pfechaoper";
                                pfecha_cal.Value = pEntidad.fecha_recepcion;
                                pfecha_cal.DbType = DbType.DateTime;
                                pfecha_cal.Direction = ParameterDirection.Input;

                                DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                                p_ip.ParameterName = "p_ip";
                                p_ip.Value = pUsuario.IP;

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

                                pEntidad.cod_ope = pcode_opera.Value != DBNull.Value ? Convert.ToInt64(pcode_opera.Value) : 0;

                                //en esta porcion de codigo se crea el Recepcion y si tiene permisos el usuario
                                //se guarda la auditoria de ese registro
                                cmdTransaccionFactory.Parameters.Clear();

                                DbParameter pcodec_opera = cmdTransaccionFactory.CreateParameter();
                                pcodec_opera.ParameterName = "pcodigooper";
                                pcodec_opera.Value = pEntidad.cod_ope;
                                pcodec_opera.DbType = DbType.Int64;
                                pcodec_opera.Direction = ParameterDirection.Input;

                                DbParameter pfecha_Recepcion = cmdTransaccionFactory.CreateParameter();
                                pfecha_Recepcion.ParameterName = "pfecha";
                                pfecha_Recepcion.Value = pEntidad.fecha_recepcion;
                                pfecha_Recepcion.DbType = DbType.DateTime;
                                pfecha_Recepcion.Direction = ParameterDirection.Input;

                                DbParameter pcod_traslado = cmdTransaccionFactory.CreateParameter();
                                pcod_traslado.ParameterName = "pcodigotraslado";
                                pcod_traslado.Value = codTraslado;
                                pcod_traslado.DbType = DbType.Int64;
                                pcod_traslado.Direction = ParameterDirection.Input;

                                DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                                pestado.ParameterName = "pestado";
                                pestado.Value = pEntidad.estado;
                                pestado.DbType = DbType.Int64;
                                pestado.Direction = ParameterDirection.Input;

                                cmdTransaccionFactory.Parameters.Add(pcodec_opera);
                                cmdTransaccionFactory.Parameters.Add(pfecha_Recepcion);
                                cmdTransaccionFactory.Parameters.Add(pcod_traslado);
                                cmdTransaccionFactory.Parameters.Add(pestado);

                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_RECEPCION_U";
                                cmdTransaccionFactory.ExecuteNonQuery();

                                if (pUsuario.programaGeneraLog)
                                    DAauditoria.InsertarLog(pEntidad, "TRASLADOCAJA", pUsuario, Accion.Modificar.ToString(), TipoAuditoria.CajaFinanciera, "Modificación Traslado");
                                    //DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.cod_recepcion.ToString()), "RECEPCION", Accion.Modificar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                                //--------------------------------------------------------------------------------------//

                                //en esta porcion de codigo se inserta la transaccion realizada
                                //cmdTransaccionFactory.Parameters.Clear();
                                //DbParameter pfecha_mov = cmdTransaccionFactory.CreateParameter();
                                //pfecha_mov.ParameterName = "pfechamov";
                                //pfecha_mov.Value = pEntidad.fecha_recepcion;

                                //DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                                //ptipo_mov.ParameterName = "ptipomov";
                                //ptipo_mov.Value = pEntidad.tipo_movimiento;

                                //DbParameter pcode_caja = cmdTransaccionFactory.CreateParameter();
                                //pcode_caja.ParameterName = "pcodigocaja";
                                //pcode_caja.Value = pEntidad.cod_caja;

                                //DbParameter pcode_oper = cmdTransaccionFactory.CreateParameter();
                                //pcode_oper.ParameterName = "pcodigooper";
                                //pcode_oper.Value = pcode_opera.Value;

                                //DbParameter pcode_cajero = cmdTransaccionFactory.CreateParameter();
                                //pcode_cajero.ParameterName = "pcodigocajero";
                                //pcode_cajero.Value = pEntidad.cod_cajero;

                                //DbParameter pcode_usuario = cmdTransaccionFactory.CreateParameter();
                                //pcode_usuario.ParameterName = "pcodigousuario";
                                //pcode_usuario.Value = pUsuario.codusuario;

                                //DbParameter pcode_moneda = cmdTransaccionFactory.CreateParameter();
                                //pcode_moneda.ParameterName = "pcodigomoneda";
                                //pcode_moneda.Value = codMoneda;

                                //DbParameter pval_pago = cmdTransaccionFactory.CreateParameter();
                                //pval_pago.ParameterName = "pvalorpago";
                                //pval_pago.Value = valore;

                                //cmdTransaccionFactory.Parameters.Add(pfecha_mov);
                                //cmdTransaccionFactory.Parameters.Add(ptipo_mov);
                                //cmdTransaccionFactory.Parameters.Add(pcode_caja);
                                //cmdTransaccionFactory.Parameters.Add(pcode_oper);
                                //cmdTransaccionFactory.Parameters.Add(pcode_cajero);
                                //cmdTransaccionFactory.Parameters.Add(pcode_usuario);
                                //cmdTransaccionFactory.Parameters.Add(pcode_moneda);
                                //cmdTransaccionFactory.Parameters.Add(pval_pago);

                                //cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                //cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_TRANSAC_CAJA_C";
                                //cmdTransaccionFactory.ExecuteNonQuery();

                                //==================================================================
                                // Aqui insertamos la Transaccion de la caja en "TRANSACCIONESCAJA"

                                cmdTransaccionFactory.Parameters.Clear();

                                DbParameter pfecha_movimientoTran = cmdTransaccionFactory.CreateParameter();
                                pfecha_movimientoTran.ParameterName = "pfechamov";
                                pfecha_movimientoTran.Value = pEntidad.fecha_recepcion;
                                pfecha_movimientoTran.Direction = ParameterDirection.Input;
                                pfecha_movimientoTran.DbType = DbType.DateTime;
                                cmdTransaccionFactory.Parameters.Add(pfecha_movimientoTran);

                                DbParameter ptipo_movimiento = cmdTransaccionFactory.CreateParameter();
                                ptipo_movimiento.ParameterName = "ptipomov";
                                ptipo_movimiento.Value = pEntidad.tipo_movimiento;
                                ptipo_movimiento.Direction = ParameterDirection.Input;
                                ptipo_movimiento.DbType = DbType.String;
                                cmdTransaccionFactory.Parameters.Add(ptipo_movimiento);

                                DbParameter pcod_cajaRecep = cmdTransaccionFactory.CreateParameter();
                                pcod_cajaRecep.ParameterName = "pcodigocaja";
                                pcod_cajaRecep.Value = pEntidad.cod_caja;
                                pcod_cajaRecep.Direction = ParameterDirection.Input;
                                cmdTransaccionFactory.Parameters.Add(pcod_cajaRecep);

                                DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                                pcod_ope.ParameterName = "pcodigooper";
                                pcod_ope.Value = pEntidad.cod_ope;
                                pcod_ope.Direction = ParameterDirection.Input;
                                cmdTransaccionFactory.Parameters.Add(pcod_ope);

                                DbParameter ptipo_tranTranCaja = cmdTransaccionFactory.CreateParameter();
                                ptipo_tranTranCaja.ParameterName = "pcodigotipotran";
                                ptipo_tranTranCaja.Value = 902;
                                ptipo_tranTranCaja.Direction = ParameterDirection.Input;
                                cmdTransaccionFactory.Parameters.Add(ptipo_tranTranCaja);

                                DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                                pcod_cajero.ParameterName = "pcodigocajero";
                                if (pEntidad.cod_cajero == 0)
                                    pcod_cajero.Value = DBNull.Value;
                                else
                                    pcod_cajero.Value = pEntidad.cod_cajero;
                                pcod_cajero.Direction = ParameterDirection.Input;
                                cmdTransaccionFactory.Parameters.Add(pcod_cajero);

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
                                pvalor_pago.Value = valore;
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
                                pnum_productoTran.Value = codTraslado.ToString();
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
                                pcode_ope.Value = pEntidad.cod_ope;
                                pcode_ope.Direction = ParameterDirection.Input;

                                DbParameter pfecha_movMoviento = cmdTransaccionFactory.CreateParameter();
                                pfecha_movMoviento.ParameterName = "pfechaope";
                                pfecha_movMoviento.Value = pEntidad.fecha_recepcion;
                                pfecha_movMoviento.Direction = ParameterDirection.Input;

                                DbParameter pcodx_cajaMoviento = cmdTransaccionFactory.CreateParameter();
                                pcodx_cajaMoviento.ParameterName = "pcodigocaja";
                                pcodx_cajaMoviento.Value = pEntidad.cod_caja;
                                pcodx_cajaMoviento.Direction = ParameterDirection.Input;

                                DbParameter pcodx_cajeroMoviento = cmdTransaccionFactory.CreateParameter();
                                pcodx_cajeroMoviento.ParameterName = "pcodigocajero";
                                pcodx_cajeroMoviento.Value = pEntidad.cod_cajero;
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
                                pvalorx.Value = valore;
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

                                //en esta porcion de codigo se va a enviar los saldos realiados 
                                //por el cajero en la caja especifica en una fecha
                                cmdTransaccionFactory.Parameters.Clear();
                                DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                                pfecha.ParameterName = "pfecha";
                                pfecha.Value = pEntidad.fecha_recepcion;
                                pfecha.DbType = DbType.DateTime;
                                pfecha.Direction = ParameterDirection.Input;

                                DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                                pcodi_caja.ParameterName = "pcodigocaja";
                                pcodi_caja.Value = pEntidad.cod_caja;
                                pcodi_caja.DbType = DbType.Int16;
                                pcodi_caja.Direction = ParameterDirection.Input;

                                DbParameter pcodx_cajero = cmdTransaccionFactory.CreateParameter();
                                pcodx_cajero.ParameterName = "pcodigocajero";
                                pcodx_cajero.Value = pEntidad.cod_cajero;
                                pcodx_cajero.DbType = DbType.Int16;
                                pcodx_cajero.Direction = ParameterDirection.Input;

                                DbParameter pcodi_moneda = cmdTransaccionFactory.CreateParameter();
                                pcodi_moneda.ParameterName = "pcodigomoneda";
                                pcodi_moneda.Value = codMoneda;
                                pcodi_moneda.DbType = DbType.Int16;
                                pcodi_moneda.Direction = ParameterDirection.Input;

                                DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                                pvalor.ParameterName = "pvalor";
                                pvalor.Value = valore;
                                pvalor.DbType = DbType.Decimal;
                                pvalor.Direction = ParameterDirection.Input;

                                cmdTransaccionFactory.Parameters.Add(pfecha);
                                cmdTransaccionFactory.Parameters.Add(pcodi_caja);
                                cmdTransaccionFactory.Parameters.Add(pcodi_cajero);
                                cmdTransaccionFactory.Parameters.Add(pcodi_moneda);
                                cmdTransaccionFactory.Parameters.Add(pvalor);

                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_SALDO_CAJA_RECEP";
                                cmdTransaccionFactory.ExecuteNonQuery();
                                
                            }
                            
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecepcionData", "InsertarRecepcion", ex);
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
        public Recepcion ConsultarCajero(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Recepcion entidad = new Recepcion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT b.cod_cajero codcajero, a.nombre nomcajero,  c.cod_caja codcaja, c.nombre nomcaja,c.esprincipal principal, d.cod_oficina codoficina,d.nombre nomoficina,(select decode(max(x.fecha_proceso),null,sysdate,max(x.fecha_proceso)) from procesooficina x where x.cod_oficina=d.cod_oficina) fechaproceso FROM USUARIOS a, CAJERO b, CAJA c, OFICINA d WHERE a.CODUSUARIO=" + pUsuario.codusuario.ToString() + " and a.codusuario=b.cod_persona and b.cod_caja=c.cod_caja and C.COD_OFICINA=d.cod_oficina";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["nomcajero"] != DBNull.Value) entidad.nomcajero   = Convert.ToString(resultado["nomcajero"]);
                            if (resultado["codcajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["codcajero"]);
                            if (resultado["codcaja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["codcaja"]);
                            if (resultado["nomcaja"] != DBNull.Value) entidad.nomcaja= Convert.ToString(resultado["nomcaja"]);
                            if (resultado["codoficina"] != DBNull.Value) entidad.cod_oficina= Convert.ToInt64(resultado["codoficina"]);
                            if (resultado["nomoficina"] != DBNull.Value) entidad.nomoficina= Convert.ToString(resultado["nomoficina"]);
                            if (resultado["fechaproceso"] != DBNull.Value) entidad.fecha_recepcion= Convert.ToDateTime(resultado["fechaproceso"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecepcionData", "ConsultarRecepcion", ex);
                        return null;
                    }

                }

            }
        }


        /// <summary>
        /// Obtiene la lista de cajeros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajas obtenidas</returns>
        public List<Traslado> ListarTraslado(Recepcion pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Traslado> lstTraslado = new List<Traslado>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " Select (select y.nombre from caja x, oficina y where x.cod_oficina=y.cod_oficina and a.cod_caja_ori=x.cod_caja) nomoficinaori, a.fecha_traslado fechatraslado,a.cod_traslado codtraslado ,a.cod_caja_ori codcajaori,(select x.nombre from caja x where x.cod_caja=a.cod_caja_ori) nomcajaori,a.cod_cajero_ori codcajeroori,(select y.nombre from cajero x, usuarios y where x.cod_persona=y.codusuario and x.cod_cajero=a.cod_cajero_ori and x.cod_caja=a.cod_caja_ori) nomcajeroori,a.cod_moneda codmoneda,(select x.descripcion from tipomoneda x where x.cod_moneda=a.cod_moneda) nommoneda,a.valor valor from TrasladoCaja a where a.tipo_traslado=1 and a.estado=0 and a.cod_caja_des=" + pEntidad.cod_caja + " and a.cod_cajero_des=" + pEntidad.cod_cajero;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Traslado entidad = new Traslado();
                            //Asociar todos los valores a la entidad
                            if (resultado["codtraslado"] != DBNull.Value) entidad.cod_traslado = Convert.ToInt64(resultado["codtraslado"]);
                            if (resultado["codcajeroori"] != DBNull.Value) entidad.cod_cajero_ori = Convert.ToInt64(resultado["codcajeroori"]);
                            if (resultado["codcajaori"] != DBNull.Value) entidad.cod_caja_ori = Convert.ToInt64(resultado["codcajaori"]);
                            if (resultado["fechatraslado"] != DBNull.Value) entidad.fecha_traslado = Convert.ToDateTime(resultado["fechatraslado"]);
                            if (resultado["nomcajaori"] != DBNull.Value) entidad.nomcaja_ori = Convert.ToString(resultado["nomcajaori"]);
                            if (resultado["nomcajeroori"] != DBNull.Value) entidad.nomcajero_ori = Convert.ToString(resultado["nomcajeroori"]);
                            if (resultado["codmoneda"] != DBNull.Value) entidad.cod_moneda = long.Parse(resultado["codmoneda"].ToString());
                            if (resultado["nommoneda"] != DBNull.Value) entidad.nom_moneda = resultado["nommoneda"].ToString();
                            if (resultado["valor"] != DBNull.Value) entidad.valor = decimal.Parse(resultado["valor"].ToString());
                            if (resultado["nomoficinaori"] != DBNull.Value) entidad.nomoficina_ori  = Convert.ToString(resultado["nomoficinaori"]);
                            lstTraslado.Add(entidad);
                        }

                        return lstTraslado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecepcionData", "ListarTraslado", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consulta un traslado segun la caja de origen/destino
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Traslado obtenido</returns>
        public Traslado ConsultarTraslado(Recepcion pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Traslado entidad = new Traslado();
            
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @" Select (select y.nombre from caja x, oficina y where x.cod_oficina=y.cod_oficina and a.cod_caja_ori=x.cod_caja) nomoficinaori, 
                                        a.fecha_traslado fechatraslado,a.cod_traslado codtraslado,a.cod_caja_ori codcajaori,(select x.nombre from caja x where x.cod_caja=a.cod_caja_ori) nomcajaori,
                                        a.cod_cajero_ori codcajeroori,(select y.nombre from cajero x, usuarios y where x.cod_persona=y.codusuario and x.cod_cajero=a.cod_cajero_ori and x.cod_caja=a.cod_caja_ori) nomcajeroori,
                                        a.cod_moneda codmoneda,(select x.descripcion from tipomoneda x where x.cod_moneda=a.cod_moneda) nommoneda,
                                        a.valor valor from TrasladoCaja a where a.tipo_traslado = 1 and a.estado = 0 and a.fecha_traslado = to_date('" + pEntidad.fecha_recepcion.ToShortDateString() + "','dd/MM/yyyy')";
                        
                        //Si se verifica que la caja tenga traslados pendientes por recibir
                        if(pEntidad.cod_recepcion == 1)
                            sql += "and a.cod_caja_des=" + pEntidad.cod_caja + " and a.cod_cajero_des=" + pEntidad.cod_cajero;
                        //si se verifica que los traslados de la caja hayan sido recibidos
                        else if(pEntidad.cod_recepcion == 2)
                            sql += "and a.cod_caja_ori=" + pEntidad.cod_caja + " and a.cod_cajero_ori=" + pEntidad.cod_cajero;

                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["codtraslado"] != DBNull.Value) entidad.cod_traslado = Convert.ToInt64(resultado["codtraslado"]);
                            if (resultado["codcajeroori"] != DBNull.Value) entidad.cod_cajero_ori = Convert.ToInt64(resultado["codcajeroori"]);
                            if (resultado["codcajaori"] != DBNull.Value) entidad.cod_caja_ori = Convert.ToInt64(resultado["codcajaori"]);
                            if (resultado["fechatraslado"] != DBNull.Value) entidad.fecha_traslado = Convert.ToDateTime(resultado["fechatraslado"]);
                            if (resultado["nomcajaori"] != DBNull.Value) entidad.nomcaja_ori = Convert.ToString(resultado["nomcajaori"]);
                            if (resultado["nomcajeroori"] != DBNull.Value) entidad.nomcajero_ori = Convert.ToString(resultado["nomcajeroori"]);
                            if (resultado["codmoneda"] != DBNull.Value) entidad.cod_moneda = long.Parse(resultado["codmoneda"].ToString());
                            if (resultado["nommoneda"] != DBNull.Value) entidad.nom_moneda = resultado["nommoneda"].ToString();
                            if (resultado["valor"] != DBNull.Value) entidad.valor = decimal.Parse(resultado["valor"].ToString());
                            if (resultado["nomoficinaori"] != DBNull.Value) entidad.nomoficina_ori = Convert.ToString(resultado["nomoficinaori"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecepcionData", "ConsultarTraslado", ex);
                        return null;
                    }
                }
            }
        }

    }
}
