using System;
using System.Data;
using System.Data.Common;
using Xpinn.Caja.Entities;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Data
{
    public class SyncOperacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        public SyncOperacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public bool RegistrarPagoOperacion(SyncTransaccionCaja pTransac, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pn_radic = cmdTransaccionFactory.CreateParameter();
                        pn_radic.ParameterName = "PN_NUM_PRODUCTO";
                        pn_radic.Value = pTransac.num_producto;
                        pn_radic.DbType = DbType.Int64;
                        pn_radic.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        pn_cod_cliente.ParameterName = "PN_COD_CLIENTE";
                        pn_cod_cliente.Value = pTransac.cod_persona;
                        pn_cod_cliente.DbType = DbType.Int64;
                        pn_cod_cliente.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_ope = cmdTransaccionFactory.CreateParameter();
                        pn_cod_ope.ParameterName = "PN_COD_OPE";
                        pn_cod_ope.Value = pTransac.cod_ope;
                        pn_cod_ope.DbType = DbType.Int64;
                        pn_cod_ope.Direction = ParameterDirection.Input;

                        DbParameter pf_fecha_pago = cmdTransaccionFactory.CreateParameter();
                        pf_fecha_pago.ParameterName = "PF_FECHA_PAGO";
                        pf_fecha_pago.Value = pTransac.fecha_aplica;
                        pf_fecha_pago.DbType = DbType.Date;
                        pf_fecha_pago.Direction = ParameterDirection.Input;

                        DbParameter pn_valor_pago = cmdTransaccionFactory.CreateParameter();
                        pn_valor_pago.ParameterName = "PN_VALOR_PAGO";
                        pn_valor_pago.Value = pTransac.valor_pago;
                        pn_valor_pago.DbType = DbType.Decimal;
                        pn_valor_pago.Direction = ParameterDirection.Input;

                        DbParameter ps_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        ps_tipo_tran.ParameterName = "PN_TIPO_TRAN";
                        if (pTransac.tipo_tran > 0) ps_tipo_tran.Value = pTransac.tipo_tran; else ps_tipo_tran.Value = -999;
                        ps_tipo_tran.DbType = DbType.Int64;
                        ps_tipo_tran.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_usu = cmdTransaccionFactory.CreateParameter();
                        pn_cod_usu.ParameterName = "PN_COD_USU";
                        pn_cod_usu.Value = pTransac.cod_usuario;
                        pn_cod_usu.DbType = DbType.Int64;
                        pn_cod_usu.Direction = ParameterDirection.Input;

                        DbParameter rn_sobrante = cmdTransaccionFactory.CreateParameter();
                        rn_sobrante.ParameterName = "RN_SOBRANTE";
                        rn_sobrante.Value = -1;
                        rn_sobrante.DbType = DbType.Int64;
                        rn_sobrante.Direction = ParameterDirection.InputOutput;

                        DbParameter n_error = cmdTransaccionFactory.CreateParameter();
                        n_error.ParameterName = "N_ERROR";
                        n_error.Value = 0;
                        n_error.DbType = DbType.Int64;
                        n_error.Direction = ParameterDirection.InputOutput;

                        DbParameter pn_documento = cmdTransaccionFactory.CreateParameter();
                        pn_documento.ParameterName = "PN_DOCUMENTO";
                        if (!string.IsNullOrEmpty(pTransac.referencia))
                            pn_documento.Value = pTransac.referencia;
                        else
                            pn_documento.Value = DBNull.Value;
                        pn_documento.DbType = DbType.String;
                        pn_documento.Direction = ParameterDirection.Input;

                        DbParameter ppagorotativo = cmdTransaccionFactory.CreateParameter();
                        ppagorotativo.ParameterName = "PPAGOROTATIVO";
                        ppagorotativo.Value = 1;
                        ppagorotativo.DbType = DbType.Int16;
                        ppagorotativo.Direction = ParameterDirection.Input;

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

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_REGISOPER";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        string sobrante = rn_sobrante.Value != DBNull.Value ? Convert.ToString(rn_sobrante.Value) : string.Empty;
                        string error = n_error.Value != DBNull.Value ? Convert.ToString(n_error) : string.Empty;
                        DetallePagos detalle = new DetallePagos
                        {
                            NumeroProducto = pTransac.num_producto,
                            CodigoCliente = pTransac.cod_persona,
                            CodigoOperacion = pTransac.cod_ope,
                            FechaPago = pTransac.fecha_aplica,
                            ValorPago = pTransac.valor_pago,
                            TipoTran = pTransac.tipo_tran != null ? Convert.ToInt64(pTransac.tipo_tran) : 0,
                            CodigoUsuarioRealizoTransaccion = pTransac.cod_usuario,
                            Documento = pTransac.referencia,
                            Sobrante = sobrante,
                            Error = error
                        };

                        DAauditoria.InsertarLog(detalle, "Todos los productos", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera,
                                "Creacion de transaccion para producto con numero " + pTransac.num_producto);

                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOperacionData", "RegistrarPagoOperacion", ex);
                        return false;
                    }
                }
            }
        }

        public SyncOperacion CrearSyncOperacion(SyncOperacion pOperacion, ref string pError, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcode_opera = cmdTransaccionFactory.CreateParameter();
                        pcode_opera.ParameterName = "pcodigooper";
                        pcode_opera.Value = pOperacion.cod_ope_principal;
                        pcode_opera.Direction = ParameterDirection.Output;

                        DbParameter pcode_tope = cmdTransaccionFactory.CreateParameter();
                        pcode_tope.ParameterName = "pcodigotipoope";
                        pcode_tope.Value = pOperacion.tipo_ope;
                        pcode_tope.Direction = ParameterDirection.Input;

                        DbParameter pcode_usuari = cmdTransaccionFactory.CreateParameter();
                        pcode_usuari.ParameterName = "pcodigousuario";
                        pcode_usuari.Value = pOperacion.cod_usuario;
                        pcode_usuari.Direction = ParameterDirection.Input;

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "pcodigooficina";
                        pcode_oficina.Value = pOperacion.cod_oficina;
                        pcode_oficina.Direction = ParameterDirection.Input;

                        DbParameter pcodi_caja = cmdTransaccionFactory.CreateParameter();
                        pcodi_caja.ParameterName = "pcodigocaja";
                        pcodi_caja.Value = pOperacion.cod_caja;
                        pcodi_caja.Direction = ParameterDirection.Input;

                        DbParameter pcodi_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodi_cajero.ParameterName = "pcodigocajero";
                        pcodi_cajero.Value = pOperacion.cod_cajero;
                        pcodi_cajero.Direction = ParameterDirection.Input;

                        DbParameter pfecha_cal = cmdTransaccionFactory.CreateParameter();
                        pfecha_cal.ParameterName = "pfechaoper";
                        pfecha_cal.Value = pOperacion.fecha_oper;
                        pfecha_cal.Direction = ParameterDirection.Input;

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "pobservaciones";
                        if (string.IsNullOrWhiteSpace(pOperacion.observacion))
                            pobservaciones.Value = DBNull.Value;
                        else
                            pobservaciones.Value = pOperacion.observacion;
                        pobservaciones.Direction = ParameterDirection.Input;

                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        if (!string.IsNullOrEmpty(pOperacion.ip))
                            p_ip.Value = pOperacion.ip;
                        else
                        {
                            if (!string.IsNullOrEmpty(pUsuario.IP))
                                p_ip.Value = pUsuario.IP;
                            else
                                p_ip.Value = DBNull.Value;
                        }
                        p_ip.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcode_opera);
                        cmdTransaccionFactory.Parameters.Add(pcode_tope);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuari);
                        cmdTransaccionFactory.Parameters.Add(pcode_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcodi_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodi_cajero);
                        cmdTransaccionFactory.Parameters.Add(pfecha_cal);
                        cmdTransaccionFactory.Parameters.Add(p_ip);
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_OPERACION_CAJA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pOperacion.cod_ope_principal = Convert.ToInt64(pcode_opera.Value);

                        return pOperacion;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }


        public TipoOperacion InsertarFactura(TipoOperacion pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pnum_factu = cmdTransaccionFactory.CreateParameter();
                        pnum_factu.ParameterName = "pnumfactu";
                        pnum_factu.Value = "0000000000";
                        pnum_factu.DbType = DbType.String;
                        pnum_factu.Direction = ParameterDirection.InputOutput;

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcodigoope";
                        pcod_ope.Value = pEntidad.cod_operacion;
                        pcod_ope.Direction = ParameterDirection.Input;

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "pcodigopersona";
                        pcod_usuario.Value = pEntidad.cod_persona;
                        pcod_usuario.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pnum_factu);
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_INSERTAR_FACTURA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pEntidad.num_factura = pnum_factu.Value.ToString();

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOperacionData", "InsertarFactura", ex);
                        return null;
                    }

                }
            }
        }

        public SyncOperacion GenerarAnulacionOperacion(SyncOperacion pOperacion, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pn_cod_ope";
                        pcod_ope.Value = pOperacion.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "pn_usuario";
                        pusuario.Value = pOperacion.cod_usuario;
                        pusuario.Direction = ParameterDirection.Input;

                        DbParameter pValor_Retorno = cmdTransaccionFactory.CreateParameter();
                        pValor_Retorno.ParameterName = "pb_resultado";
                        pValor_Retorno.Value = 0;
                        pValor_Retorno.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(pcod_ope);
                        cmdTransaccionFactory.Parameters.Add(pusuario);
                        cmdTransaccionFactory.Parameters.Add(pValor_Retorno);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_REVERSION";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pOperacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOperacionData", "GenerarAnulacionOperacion", ex);
                        return null;
                    }
                }
            }
        }


        #region METODOS DE HOMOLOACION DE OPERACIONES Y OTROS

        public SyncHomologaOperacion CrearSyncHomologaOperacion(SyncHomologaOperacion pSyncHomologaOperacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pSyncHomologaOperacion.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        if (pSyncHomologaOperacion.fecha == null)
                            pfecha.Value = DBNull.Value;
                        else
                            pfecha.Value = pSyncHomologaOperacion.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter ptabla = cmdTransaccionFactory.CreateParameter();
                        ptabla.ParameterName = "p_tabla";
                        ptabla.Value = pSyncHomologaOperacion.tabla;
                        ptabla.Direction = ParameterDirection.Input;
                        ptabla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla);

                        DbParameter pcampo_tabla = cmdTransaccionFactory.CreateParameter();
                        pcampo_tabla.ParameterName = "p_campo_tabla";
                        if (pSyncHomologaOperacion.campo_tabla == null)
                            pcampo_tabla.Value = DBNull.Value;
                        else
                            pcampo_tabla.Value = pSyncHomologaOperacion.campo_tabla;
                        pcampo_tabla.Direction = ParameterDirection.Input;
                        pcampo_tabla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcampo_tabla);

                        DbParameter pcodigo_principal = cmdTransaccionFactory.CreateParameter();
                        pcodigo_principal.ParameterName = "p_codigo_principal";
                        if (pSyncHomologaOperacion.codigo_principal == null)
                            pcodigo_principal.Value = DBNull.Value;
                        else
                            pcodigo_principal.Value = pSyncHomologaOperacion.codigo_principal;
                        pcodigo_principal.Direction = ParameterDirection.Input;
                        pcodigo_principal.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_principal);

                        DbParameter pcodigo_local = cmdTransaccionFactory.CreateParameter();
                        pcodigo_local.ParameterName = "p_codigo_local";
                        if (pSyncHomologaOperacion.codigo_local == null)
                            pcodigo_local.Value = DBNull.Value;
                        else
                            pcodigo_local.Value = pSyncHomologaOperacion.codigo_local;
                        pcodigo_local.Direction = ParameterDirection.Input;
                        pcodigo_local.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_local);

                        DbParameter pproceso = cmdTransaccionFactory.CreateParameter();
                        pproceso.ParameterName = "p_proceso";
                        if (pSyncHomologaOperacion.proceso == null)
                            pproceso.Value = DBNull.Value;
                        else
                            pproceso.Value = pSyncHomologaOperacion.proceso;
                        pproceso.Direction = ParameterDirection.Input;
                        pproceso.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pproceso);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SYN_HOMOLOGACI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pSyncHomologaOperacion.consecutivo = Convert.ToInt64(pconsecutivo.Value);
                        return pSyncHomologaOperacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOperacionData", "CrearSyncHomologaOperacion", ex);
                        return null;
                    }
                }
            }
        }


        public SyncHomologaOperacion ConsultarHomologacionOperacion(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            SyncHomologaOperacion entidad = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT H.*, O.NUM_COMP, O.TIPO_COMP FROM HOMOLOGACION_OPERACIONES H LEFT JOIN OPERACION O ON TO_NUMBER(H.CODIGO_PRINCIPAL) = O.COD_OPE " + pFiltro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.HasRows)
                        {
                            if (resultado.Read())
                            {
                                entidad = new SyncHomologaOperacion();
                                if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                                if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                                if (resultado["TABLA"] != DBNull.Value) entidad.tabla = Convert.ToString(resultado["TABLA"]);
                                if (resultado["CAMPO_TABLA"] != DBNull.Value) entidad.campo_tabla = Convert.ToString(resultado["CAMPO_TABLA"]);
                                if (resultado["CODIGO_PRINCIPAL"] != DBNull.Value) entidad.codigo_principal = Convert.ToString(resultado["CODIGO_PRINCIPAL"]);
                                if (resultado["CODIGO_LOCAL"] != DBNull.Value) entidad.codigo_local = Convert.ToString(resultado["CODIGO_LOCAL"]);
                                if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                                if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["TIPO_COMP"]);
                            }
                            resultado.Close();
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOperacionData", "ConsultarHomologacionOperacion", ex);
                        return null;
                    }
                }
            }
        }


        public SyncHomologaOperacionDeta CrearHomologacionOperacionDetalle(SyncHomologaOperacionDeta pHomologa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pid_detalle = cmdTransaccionFactory.CreateParameter();
                        pid_detalle.ParameterName = "p_id_detalle";
                        pid_detalle.Value = pHomologa.id_detalle;
                        pid_detalle.Direction = ParameterDirection.Output;
                        pid_detalle.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_detalle);

                        DbParameter pcod_ope_principal = cmdTransaccionFactory.CreateParameter();
                        pcod_ope_principal.ParameterName = "p_cod_ope_principal";
                        if (pHomologa.cod_ope_principal == null)
                            pcod_ope_principal.Value = DBNull.Value;
                        else
                            pcod_ope_principal.Value = pHomologa.cod_ope_principal;
                        pcod_ope_principal.Direction = ParameterDirection.Input;
                        pcod_ope_principal.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope_principal);

                        DbParameter pcod_ope_local = cmdTransaccionFactory.CreateParameter();
                        pcod_ope_local.ParameterName = "p_cod_ope_local";
                        if (pHomologa.cod_ope_local == null)
                            pcod_ope_local.Value = DBNull.Value;
                        else
                            pcod_ope_local.Value = pHomologa.cod_ope_local;
                        pcod_ope_local.Direction = ParameterDirection.Input;
                        pcod_ope_local.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope_local);

                        DbParameter ptabla_detalle = cmdTransaccionFactory.CreateParameter();
                        ptabla_detalle.ParameterName = "p_tabla_detalle";
                        ptabla_detalle.Value = pHomologa.tabla_detalle;
                        ptabla_detalle.Direction = ParameterDirection.Input;
                        ptabla_detalle.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla_detalle);

                        DbParameter pcampo_tabla = cmdTransaccionFactory.CreateParameter();
                        pcampo_tabla.ParameterName = "p_campo_tabla";
                        if (pHomologa.campo_tabla == null)
                            pcampo_tabla.Value = DBNull.Value;
                        else
                            pcampo_tabla.Value = pHomologa.campo_tabla;
                        pcampo_tabla.Direction = ParameterDirection.Input;
                        pcampo_tabla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcampo_tabla);

                        DbParameter pcodigo_principal = cmdTransaccionFactory.CreateParameter();
                        pcodigo_principal.ParameterName = "p_codigo_principal";
                        if (pHomologa.codigo_principal == null)
                            pcodigo_principal.Value = DBNull.Value;
                        else
                            pcodigo_principal.Value = pHomologa.codigo_principal;
                        pcodigo_principal.Direction = ParameterDirection.Input;
                        pcodigo_principal.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_principal);

                        DbParameter pcodigo_local = cmdTransaccionFactory.CreateParameter();
                        pcodigo_local.ParameterName = "p_codigo_local";
                        if (pHomologa.codigo_local == null)
                            pcodigo_local.Value = DBNull.Value;
                        else
                            pcodigo_local.Value = pHomologa.codigo_local;
                        pcodigo_local.Direction = ParameterDirection.Input;
                        pcodigo_local.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_local);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SYN_HOMOLOGAOPE_DETA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pHomologa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOperacionData", "CrearHomologacionOperacionDetalle", ex);
                        return null;
                    }
                }
            }
        }


        public SyncHomologaOperacionDeta ConsultarSyncHomologaOperacionDetalle(SyncHomologaOperacionDeta pSyncHomologaOperacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            SyncHomologaOperacionDeta entidad = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM HOMOLOGACION_OPERACIONES_DETA " + ObtenerFiltro(pSyncHomologaOperacion) + " ORDER BY ID_DETALLE ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.HasRows)
                        {
                            if (resultado.Read())
                            {
                                entidad = new SyncHomologaOperacionDeta();
                                if (resultado["ID_DETALLE"] != DBNull.Value) entidad.id_detalle = Convert.ToInt64(resultado["ID_DETALLE"]);
                                if (resultado["COD_OPE_PRINCIPAL"] != DBNull.Value) entidad.cod_ope_principal = Convert.ToInt64(resultado["COD_OPE_PRINCIPAL"]);
                                if (resultado["COD_OPE_LOCAL"] != DBNull.Value) entidad.cod_ope_local = Convert.ToInt64(resultado["COD_OPE_LOCAL"]);
                                if (resultado["TABLA_DETALLE"] != DBNull.Value) entidad.tabla_detalle = Convert.ToString(resultado["TABLA_DETALLE"]);
                                if (resultado["CAMPO_TABLA"] != DBNull.Value) entidad.campo_tabla = Convert.ToString(resultado["CAMPO_TABLA"]);
                                if (resultado["CODIGO_PRINCIPAL"] != DBNull.Value) entidad.codigo_principal = Convert.ToString(resultado["CODIGO_PRINCIPAL"]);
                                if (resultado["CODIGO_LOCAL"] != DBNull.Value) entidad.codigo_local = Convert.ToString(resultado["CODIGO_LOCAL"]);
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOperacionData", "ConsultarSyncHomologaOperacionDetalle", ex);
                        return null;
                    }
                }
            }
        }

        #endregion

    }
}
