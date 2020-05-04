using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Traslado
    /// </summary>    
    public class TrasladoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Traslado
        /// </summary>
        public TrasladoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea una entidad Traslado en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Traslado</param>
        /// <returns>Entidad creada</returns>
        public Traslado InsertarTraslado(Traslado pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        // en esta porcion de codigo se inserta primero la operacion realizada con el fin de ir alimentar la operacion cod_ope
                        // en la tabla TransaccionesCaja

                        DbParameter pcode_opera = cmdTransaccionFactory.CreateParameter();
                        pcode_opera.ParameterName = "pcodigooper";
                        pcode_opera.Value = pEntidad.cod_ope;
                        pcode_opera.Direction = ParameterDirection.Output;

                        DbParameter pip = cmdTransaccionFactory.CreateParameter();
                        pip.ParameterName = "p_ip";
                        pip.Value = pUsuario.IP;
                        pip.Direction = ParameterDirection.Input;

                        DbParameter pcode_tope = cmdTransaccionFactory.CreateParameter();
                        pcode_tope.ParameterName = "pcodigotipoope";
                        pcode_tope.Value = pEntidad.tipo_ope;

                        DbParameter pcode_usuari = cmdTransaccionFactory.CreateParameter();
                        pcode_usuari.ParameterName = "pcodigousuario";
                        pcode_usuari.Value = pUsuario.codusuario;

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "pcodigooficina";
                        pcode_oficina.Value = pEntidad.cod_oficina_ori;


                        DbParameter pcodi_caja = cmdTransaccionFactory.CreateParameter();
                        pcodi_caja.ParameterName = "pcodigocaja";
                        pcodi_caja.Value = pEntidad.cod_caja_ori;

                        DbParameter pcodi_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodi_cajero.ParameterName = "pcodigocajero";
                        pcodi_cajero.Value = pEntidad.cod_cajero_ori;

                        DbParameter pfecha_cal = cmdTransaccionFactory.CreateParameter();
                        pfecha_cal.ParameterName = "pfechaoper";
                        pfecha_cal.Value = pEntidad.fecha_traslado;

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "pobservaciones";
                        pobservaciones.Value = "  ";

                        cmdTransaccionFactory.Parameters.Add(pcode_opera);
                        cmdTransaccionFactory.Parameters.Add(pip);
                        cmdTransaccionFactory.Parameters.Add(pcode_tope);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuari);
                        cmdTransaccionFactory.Parameters.Add(pcode_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcodi_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodi_cajero);
                        cmdTransaccionFactory.Parameters.Add(pfecha_cal);
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_OPERACION_CAJA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEntidad.cod_ope = pcode_opera.Value != DBNull.Value ? Convert.ToInt64(pcode_opera.Value) : 0;

                        //en esta porcion de codigo se crea el Traslado y si tiene permisos el usuario
                        //se guarda la auditoria de ese registro

                        cmdTransaccionFactory.Parameters.Clear();

                        DbParameter pcod_traslado = cmdTransaccionFactory.CreateParameter();
                        pcod_traslado.ParameterName = "pcod_traslado";
                        pcod_traslado.Value = pEntidad.cod_traslado;
                        pcod_traslado.Direction = ParameterDirection.Output;

                        DbParameter pcodes_opera = cmdTransaccionFactory.CreateParameter();
                        pcodes_opera.ParameterName = "pcodigooper";
                        pcodes_opera.Value = pEntidad.cod_ope;
                        pcodes_opera.Direction = ParameterDirection.Input;

                        DbParameter pfecha_traslado = cmdTransaccionFactory.CreateParameter();
                        pfecha_traslado.ParameterName = "pfechatraslado";
                        pfecha_traslado.Value = pEntidad.fecha_traslado;

                        DbParameter pcod_tipo_traslado = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_traslado.ParameterName = "ptipotraslado";
                        pcod_tipo_traslado.Value = pEntidad.tipo_traslado;

                        DbParameter pcod_caja_ori = cmdTransaccionFactory.CreateParameter();
                        pcod_caja_ori.ParameterName = "pcodigocajaori";
                        pcod_caja_ori.Value = pEntidad.cod_caja_ori;

                        DbParameter pcod_cajero_ori = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero_ori.ParameterName = "pcodigocajeroori";
                        pcod_cajero_ori.Value = pEntidad.cod_cajero_ori;

                        DbParameter pcod_caja_des = cmdTransaccionFactory.CreateParameter();
                        pcod_caja_des.ParameterName = "pcodigocajades";
                        pcod_caja_des.Value = pEntidad.cod_caja_des;

                        DbParameter pcod_cajero_des = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero_des.ParameterName = "pcodigocajerodes";
                        pcod_cajero_des.Value = pEntidad.cod_cajero_des;

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "pcodigomoneda";
                        pcod_moneda.Value = pEntidad.cod_moneda;

                        DbParameter pval_traslado = cmdTransaccionFactory.CreateParameter();
                        pval_traslado.ParameterName = "pvalortraslado";
                        pval_traslado.Value = pEntidad.valor;

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Value = pEntidad.estado;

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
                        cmdTransaccionFactory.Parameters.Add(pcod_traslado);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_TRASLADOS_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEntidad.cod_traslado = pcod_traslado.Value != DBNull.Value ? Convert.ToInt64(pcod_traslado.Value) : 0;

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "TRASLADO", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Registrar Traslado Caja");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.cod_traslado.ToString()), "TRASLADO", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA


                        ////--------------------------------------------------------------------------------------//                       

                        //en esta porcion de codigo se va a enviar los saldos realiados 
                        //por el cajero en la caja especifica en una fecha
                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Value = pEntidad.fecha_traslado;

                        DbParameter pcodx_caja = cmdTransaccionFactory.CreateParameter();
                        pcodx_caja.ParameterName = "pcodigocaja";
                        pcodx_caja.Value = pEntidad.cod_caja_ori;

                        DbParameter pcodx_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodx_cajero.ParameterName = "pcodigocajero";
                        pcodx_cajero.Value = pEntidad.cod_cajero_ori;

                        DbParameter pcodi_moneda = cmdTransaccionFactory.CreateParameter();
                        pcodi_moneda.ParameterName = "pcodigomoneda";
                        pcodi_moneda.Value = pEntidad.cod_moneda;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pEntidad.valor;

                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        cmdTransaccionFactory.Parameters.Add(pcodi_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodi_cajero);
                        cmdTransaccionFactory.Parameters.Add(pcodi_moneda);
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_SALDO_CAJA_TRASLA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //==================================================================
                        // Aqui insertamos la Transaccion de la caja en "TRANSACCIONESCAJA"

                        cmdTransaccionFactory.Parameters.Clear();

                        DbParameter pfecha_movimientoTran = cmdTransaccionFactory.CreateParameter();
                        pfecha_movimientoTran.ParameterName = "pfechamov";
                        pfecha_movimientoTran.Value = pEntidad.fecha_traslado;
                        pfecha_movimientoTran.Direction = ParameterDirection.Input;
                        pfecha_movimientoTran.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_movimientoTran);

                        DbParameter ptipo_movimiento = cmdTransaccionFactory.CreateParameter();
                        ptipo_movimiento.ParameterName = "ptipomov";
                        ptipo_movimiento.Value = "EGRESO";
                        ptipo_movimiento.Direction = ParameterDirection.Input;
                        ptipo_movimiento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_movimiento);

                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = pEntidad.cod_caja_ori;
                        pcod_caja.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_caja);

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
                        if (pEntidad.cod_cajero_ori == 0)
                            pcod_cajero.Value = DBNull.Value;
                        else
                            pcod_cajero.Value = pEntidad.cod_cajero_ori;
                        pcod_cajero.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_cajero);
 
                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "pcodigousuario";
                        pcod_persona.Value = pUsuario.codusuario;
                        pcod_persona.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_monedaTransaccion = cmdTransaccionFactory.CreateParameter();
                        pcod_monedaTransaccion.ParameterName = "pcodigomoneda";
                        pcod_monedaTransaccion.Value = pEntidad.cod_moneda;
                        pcod_monedaTransaccion.Direction = ParameterDirection.Input;
                        pcod_monedaTransaccion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_monedaTransaccion);

                        DbParameter pvalor_pago = cmdTransaccionFactory.CreateParameter();
                        pvalor_pago.ParameterName = "pvalorpago";
                        pvalor_pago.Value = pEntidad.valor;
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
                        pnum_productoTran.Value = pEntidad.cod_traslado.ToString();
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
                        pfecha_movMoviento.Value = pEntidad.fecha_traslado;
                        pfecha_movMoviento.Direction = ParameterDirection.Input;

                        DbParameter pcodx_cajaMoviento = cmdTransaccionFactory.CreateParameter();
                        pcodx_cajaMoviento.ParameterName = "pcodigocaja";
                        pcodx_cajaMoviento.Value = pEntidad.cod_caja_ori;
                        pcodx_cajaMoviento.Direction = ParameterDirection.Input;

                        DbParameter pcodx_cajeroMoviento = cmdTransaccionFactory.CreateParameter();
                        pcodx_cajeroMoviento.ParameterName = "pcodigocajero";
                        pcodx_cajeroMoviento.Value = pEntidad.cod_cajero_ori;
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
                        if (pUsuario.cod_persona != null)
                            pcod_personaMoviento.Value = pUsuario.cod_persona;
                        else
                            pcod_personaMoviento.Value = DBNull.Value;
                        pcod_personaMoviento.Direction = ParameterDirection.Input;

                        DbParameter pcodx_monedaMoviento = cmdTransaccionFactory.CreateParameter();
                        pcodx_monedaMoviento.ParameterName = "pcodigomoneda";
                        pcodx_monedaMoviento.Value = pEntidad.cod_moneda;
                        pcodx_monedaMoviento.Direction = ParameterDirection.Input;

                        DbParameter pvalorx = cmdTransaccionFactory.CreateParameter();
                        pvalorx.ParameterName = "pvalor";
                        pvalorx.Value = pEntidad.valor;
                        pvalorx.Direction = ParameterDirection.Input;

                        DbParameter pcodi_tipopay = cmdTransaccionFactory.CreateParameter();
                        pcodi_tipopay.ParameterName = "ptipopago";
                        pcodi_tipopay.Value = "1";
                        pcodi_tipopay.Direction = ParameterDirection.Input;

                        DbParameter pcodx_tipomov = cmdTransaccionFactory.CreateParameter();
                        pcodx_tipomov.ParameterName = "ptipomov";
                        pcodx_tipomov.Value = "1"; // Egreso
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

                        dbConnectionFactory.CerrarConexion(connection);

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoData", "InsertarTraslado", ex);
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
        public Traslado ConsultarCajero(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Traslado entidad = new Traslado();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT b.cod_cajero codcajero, a.nombre nomcajero, c.cod_caja codcaja, c.nombre nomcaja, 
                                        d.cod_oficina codoficina, d.nombre nomoficina,
                                        (select decode(max(x.fecha_proceso), null, sysdate, max(x.fecha_proceso)) from procesooficina x where x.cod_oficina = d.cod_oficina) fechaproceso
                                        FROM USUARIOS a, CAJERO b, CAJA c, OFICINA d 
                                        WHERE a.CODUSUARIO = " + pUsuario.codusuario.ToString() + 
                                        " and a.codusuario = b.cod_persona " +
                                        "and b.cod_caja = c.cod_caja " +
                                        "and C.COD_OFICINA = d.cod_oficina";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["nomcajero"] != DBNull.Value) entidad.nomcajero_ori = Convert.ToString(resultado["nomcajero"]);
                            if (resultado["codcajero"] != DBNull.Value) entidad.cod_cajero_ori = Convert.ToInt64(resultado["codcajero"]);
                            if (resultado["codcaja"] != DBNull.Value) entidad.cod_caja_ori = Convert.ToInt64(resultado["codcaja"]);
                            if (resultado["nomcaja"] != DBNull.Value) entidad.nomcaja_ori = Convert.ToString(resultado["nomcaja"]);
                            if (resultado["codoficina"] != DBNull.Value) entidad.cod_oficina_ori = Convert.ToInt64(resultado["codoficina"]);
                            if (resultado["nomoficina"] != DBNull.Value) entidad.nomoficina_ori = Convert.ToString(resultado["nomoficina"]);
                            if (resultado["fechaproceso"] != DBNull.Value) entidad.fecha_traslado = Convert.ToDateTime(resultado["fechaproceso"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoData", "ConsultarTraslado", ex);
                        return null;
                    }

                }

            }
        }


        public void EliminarTraslado(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //if (pUsuario.programaGeneraLog)
                        //    pEntidad = ConsultarOficina(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter p_idTraslado = cmdTransaccionFactory.CreateParameter();
                        p_idTraslado.ParameterName = "P_IDTRASLADO";
                        p_idTraslado.Value = pId;
                        //p_idaprobador.DbType = DbType.Int32;
                        //p_idaprobador.Size = 8;
                        p_idTraslado.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_idTraslado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_TRASLADOS_E";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoData", "EliminarTraslado", ex);
                    }

                }
            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla Cajero, Caja y Oficina de la base de datos
        /// </summary>
        /// <param name="pUsuario">identificador del usuario</param>
        /// <returns>Oficina consultada</returns>
        public Cajero ConsultarCajaXCajero(Cajero pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Cajero entidad = new Cajero();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM CAJERO where COD_CAJERO=" + pEntidad.cod_cajero;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["cod_caja"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoData", "ConsultarCajaXCajero", ex);
                        return null;
                    }

                }

            }
        }

    }
}
