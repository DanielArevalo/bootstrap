using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.TarjetaDebito.Data
{
    public class CuentaData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public CuentaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Cuenta> ListarCuenta(Cuenta pCuenta, string pgeneral, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Cuenta> lstCuenta = new List<Cuenta>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";

                        if (pgeneral == "0" || pgeneral == "")
                        {
                            sql = @"SELECT * FROM V_TARDEB_CUENTA " + ObtenerFiltro(pCuenta) + " ORDER BY identificacion  ";
                        }
                        else
                        {
                            sql = @"WITH RESULTADO AS( 
                                    SELECT V.* FROM V_TARDEB_CUENTA V INNER JOIN tarjeta T ON V.NUMERO_CUENTA = T.NUMERO_CUENTA AND T.ESTADO != 2
                                    UNION ALL
                                    SELECT V.* FROM V_TARDEB_CUENTA V INNER JOIN  CUENTAS_ASIGNAR CR ON V.NUMERO_CUENTA = CR.NUMERO_CUENTA AND V.TIPO_CUENTA = CR.TIPO_CUENTA
                                    WHERE CR.NUMERO_CUENTA || '*' || CR.TIPO_CUENTA  not in(select T.NUMERO_CUENTA || '*' || CASE T.TIPO_CUENTA WHEN 1 THEN 'A' WHEN 2 THEN 'C' END AS TIPO_CUENTA from tarjeta T)
                                    ) SELECT V.* FROM RESULTADO V " + ObtenerFiltro(pCuenta, "V.");
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Cuenta entidad = new Cuenta();
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipocuenta = Convert.ToString(resultado["TIPO_CUENTA"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.nrocuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["SALDODISPONIBLE"] != DBNull.Value) entidad.saldodisponible = Convert.ToDecimal(resultado["SALDODISPONIBLE"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldototal = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["FECHASALDO"] != DBNull.Value) entidad.fechasaldo = Convert.ToDateTime(resultado["FECHASALDO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fechaapertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            lstCuenta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaData", "ListarCuenta", ex);
                        return null;
                    }
                }
            }
        }

        public int? CrearMovimiento(Movimiento pMovimiento, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnum_tran = cmdTransaccionFactory.CreateParameter();
                        pnum_tran.ParameterName = "p_num_tran";
                        pnum_tran.Value = 0;
                        pnum_tran.Direction = ParameterDirection.InputOutput;
                        pnum_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_tran);

                        DbParameter pfecha_transaccion = cmdTransaccionFactory.CreateParameter();
                        pfecha_transaccion.ParameterName = "p_fecha";
                        if (pMovimiento.fecha == null)
                            pfecha_transaccion.Value = DBNull.Value;
                        else
                            pfecha_transaccion.Value = pMovimiento.fecha;
                        pfecha_transaccion.Direction = ParameterDirection.Input;
                        pfecha_transaccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pfecha_transaccion);

                        DbParameter phora_transaccion = cmdTransaccionFactory.CreateParameter();
                        phora_transaccion.ParameterName = "p_hora";
                        if (pMovimiento.hora == null)
                            phora_transaccion.Value = DBNull.Value;
                        else
                            phora_transaccion.Value = pMovimiento.hora;
                        phora_transaccion.Direction = ParameterDirection.Input;
                        phora_transaccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(phora_transaccion);

                        DbParameter pdocumento = cmdTransaccionFactory.CreateParameter();
                        pdocumento.ParameterName = "p_documento";
                        if (pMovimiento.documento == null)
                            pdocumento.Value = DBNull.Value;
                        else
                            pdocumento.Value = pMovimiento.documento;
                        pdocumento.Direction = ParameterDirection.Input;
                        pdocumento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdocumento);

                        DbParameter pnrocuenta = cmdTransaccionFactory.CreateParameter();
                        pnrocuenta.ParameterName = "p_nrocuenta";
                        if (pMovimiento.nrocuenta == null)
                            pnrocuenta.Value = DBNull.Value;
                        else
                            pnrocuenta.Value = pMovimiento.nrocuenta;
                        pnrocuenta.Direction = ParameterDirection.Input;
                        pnrocuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnrocuenta);

                        DbParameter ptarjeta = cmdTransaccionFactory.CreateParameter();
                        ptarjeta.ParameterName = "p_tarjeta";
                        if (pMovimiento.tarjeta == null)
                            ptarjeta.Value = DBNull.Value;
                        else
                            ptarjeta.Value = pMovimiento.tarjeta;
                        ptarjeta.Direction = ParameterDirection.Input;
                        ptarjeta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptarjeta);

                        DbParameter ptipo_transaccion = cmdTransaccionFactory.CreateParameter();
                        ptipo_transaccion.ParameterName = "p_tipotransaccion";
                        if (pMovimiento.tipotransaccion == null || pMovimiento.tipotransaccion == "")
                            ptipo_transaccion.Value = DBNull.Value;
                        else
                            ptipo_transaccion.Value = pMovimiento.tipotransaccion;
                        ptipo_transaccion.Direction = ParameterDirection.Input;
                        ptipo_transaccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_transaccion);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pMovimiento.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pMovimiento.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pmonto = cmdTransaccionFactory.CreateParameter();
                        pmonto.ParameterName = "p_monto";
                        pmonto.Value = pMovimiento.monto;
                        pmonto.Direction = ParameterDirection.Input;
                        pmonto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmonto);

                        DbParameter pcomision = cmdTransaccionFactory.CreateParameter();
                        pcomision.ParameterName = "p_comision";
                        pcomision.Value = pMovimiento.comision;
                        pcomision.Direction = ParameterDirection.Input;
                        pcomision.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcomision);

                        DbParameter plugar = cmdTransaccionFactory.CreateParameter();
                        plugar.ParameterName = "p_lugar";
                        if (pMovimiento.lugar == null)
                            plugar.Value = DBNull.Value;
                        else
                            plugar.Value = pMovimiento.lugar;
                        plugar.Direction = ParameterDirection.Input;
                        plugar.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(plugar);

                        DbParameter poperacion = cmdTransaccionFactory.CreateParameter();
                        poperacion.ParameterName = "p_operacion";
                        if (pMovimiento.operacion == null)
                            poperacion.Value = DBNull.Value;
                        else
                            poperacion.Value = pMovimiento.operacion;
                        poperacion.Direction = ParameterDirection.Input;
                        poperacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(poperacion);

                        DbParameter pred = cmdTransaccionFactory.CreateParameter();
                        pred.ParameterName = "p_red";
                        if (pMovimiento.red == null)
                            pred.Value = DBNull.Value;
                        else
                            pred.Value = pMovimiento.red;
                        pred.Direction = ParameterDirection.Input;
                        pred.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pred);

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        if (pMovimiento.cod_ope == null)
                            p_cod_ope.Value = DBNull.Value;
                        else
                            p_cod_ope.Value = pMovimiento.cod_ope;
                        p_cod_ope.Direction = ParameterDirection.Input;
                        p_cod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        DbParameter p_saldo_total = cmdTransaccionFactory.CreateParameter();
                        p_saldo_total.ParameterName = "p_saldo_total";
                        if (pMovimiento.saldo_total == null)
                            p_saldo_total.Value = DBNull.Value;
                        else
                            p_saldo_total.Value = pMovimiento.saldo_total;
                        p_saldo_total.Direction = ParameterDirection.Input;
                        p_saldo_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_saldo_total);

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        if (pMovimiento.cod_cliente == null)
                            p_cod_persona.Value = DBNull.Value;
                        else
                            p_cod_persona.Value = pMovimiento.cod_cliente;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_TRANTARJE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pnum_tran.Value != null)
                            if (pnum_tran.Value.ToString().Trim() != "")
                                pMovimiento.num_tran = Convert.ToInt32(pnum_tran.Value.ToString());

                        dbConnectionFactory.CerrarConexion(connection);
                        return pMovimiento.num_tran;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaData", "CrearMovimiento", ex);
                        return null;
                    }
                }
            }
        }

        public bool VerificarSiTarjetaExiste(Tarjeta tarjeta, Usuario usuario)
        {
            DbDataReader resultado;
            bool existe = false;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT IDTARJETA FROM TARJETA WHERE numero_tarjeta = '" + tarjeta.numtarjeta + "' AND numero_cuenta = '" + tarjeta.numero_cuenta + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDTARJETA"] != DBNull.Value)
                            {
                                existe = true;
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return existe;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public Tarjeta CrearTarjeta(Tarjeta pTarjeta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidtarjeta = cmdTransaccionFactory.CreateParameter();
                        pidtarjeta.ParameterName = "P_IDTARJETA";
                        pidtarjeta.Value = pTarjeta.idtarjeta;
                        pidtarjeta.Direction = ParameterDirection.Output;
                        pidtarjeta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidtarjeta);

                        DbParameter pnumero_tarjeta = cmdTransaccionFactory.CreateParameter();
                        pnumero_tarjeta.ParameterName = "P_NUMERO_TARJETA";
                        pnumero_tarjeta.Value = pTarjeta.numtarjeta;
                        pnumero_tarjeta.Direction = ParameterDirection.Input;
                        pnumero_tarjeta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_tarjeta);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "P_NUMERO_CUENTA";
                        pnumero_cuenta.Value = pTarjeta.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_TARJWEBSER_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pTarjeta.idtarjeta = pidtarjeta.Value != DBNull.Value ? Convert.ToInt32(pidtarjeta.Value) : 0;

                        return pTarjeta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TarjetaData", "CrearTarjeta", ex);
                        return null;
                    }
                }
            }
        }

        public void AplicarMovimiento(DateTime pFecha, Movimiento pMovimiento, Int64 pcod_ope, Usuario pUsuario, ref string Error)
        {
            string seguimiento = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pn_num_producto = cmdTransaccionFactory.CreateParameter();
                        pn_num_producto.ParameterName = "pn_num_producto";
                        if (pMovimiento.nrocuenta == "") pn_num_producto.Value = DBNull.Value; else pn_num_producto.Value = pMovimiento.nrocuenta;
                        pn_num_producto.DbType = DbType.String;
                        pn_num_producto.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        pn_cod_cliente.ParameterName = "pn_cod_cliente";
                        if (pMovimiento.cod_cliente == null) pn_cod_cliente.Value = DBNull.Value; else pn_cod_cliente.Value = pMovimiento.cod_cliente;
                        pn_cod_cliente.DbType = DbType.Int64;
                        pn_cod_cliente.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_ope = cmdTransaccionFactory.CreateParameter();
                        pn_cod_ope.ParameterName = "pn_cod_ope";
                        pn_cod_ope.Value = pcod_ope;
                        pn_cod_ope.DbType = DbType.Int64;
                        pn_cod_ope.Direction = ParameterDirection.Input;

                        DbParameter pf_fecha_pago = cmdTransaccionFactory.CreateParameter();
                        pf_fecha_pago.ParameterName = "pf_fecha_pago";
                        pf_fecha_pago.Value = pFecha;
                        pf_fecha_pago.DbType = DbType.DateTime;
                        pf_fecha_pago.Direction = ParameterDirection.Input;

                        DbParameter pn_valor_pago = cmdTransaccionFactory.CreateParameter();
                        pn_valor_pago.ParameterName = "pn_valor_pago";
                        pn_valor_pago.Value = pMovimiento.monto;
                        pn_valor_pago.DbType = DbType.Double;
                        pn_valor_pago.Direction = ParameterDirection.Input;

                        DbParameter pn_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        pn_tipo_tran.ParameterName = "pn_tipo_tran";
                        if (pMovimiento.tipo_tran == null) pn_tipo_tran.Value = DBNull.Value; else pn_tipo_tran.Value = pMovimiento.tipo_tran;
                        pn_tipo_tran.DbType = DbType.Int32;
                        pn_tipo_tran.Direction = ParameterDirection.Input;

                        DbParameter pn_documento = cmdTransaccionFactory.CreateParameter();
                        pn_documento.ParameterName = "pn_documento";
                        if (pMovimiento.documento == null || pMovimiento.documento == "") pn_documento.Value = DBNull.Value; else pn_documento.Value = pMovimiento.documento;
                        pn_documento.DbType = DbType.String;
                        pn_documento.Direction = ParameterDirection.Input;

                        DbParameter pn_operacion_tarjeta = cmdTransaccionFactory.CreateParameter();
                        pn_operacion_tarjeta.ParameterName = "pn_operacion_tarjeta";
                        if (pMovimiento.operacion == null || pMovimiento.operacion == "") pn_operacion_tarjeta.Value = DBNull.Value; else pn_operacion_tarjeta.Value = pMovimiento.operacion;
                        pn_operacion_tarjeta.DbType = DbType.String;
                        pn_operacion_tarjeta.Direction = ParameterDirection.Input;

                        DbParameter pn_num_tran_tarjeta = cmdTransaccionFactory.CreateParameter();
                        pn_num_tran_tarjeta.ParameterName = "pn_num_tran_tarjeta";
                        if (pMovimiento.num_tran_tarjeta == null) pn_num_tran_tarjeta.Value = DBNull.Value; else pn_num_tran_tarjeta.Value = pMovimiento.num_tran_tarjeta;
                        pn_num_tran_tarjeta.DbType = DbType.Int64;
                        pn_num_tran_tarjeta.Direction = ParameterDirection.Input;

                        DbParameter p_valor_cliente = cmdTransaccionFactory.CreateParameter();
                        p_valor_cliente.ParameterName = "p_valor_cliente";
                        p_valor_cliente.Value = 0;
                        p_valor_cliente.DbType = DbType.Decimal;
                        p_valor_cliente.Direction = ParameterDirection.Output;

                        DbParameter p_valor_entidad = cmdTransaccionFactory.CreateParameter();
                        p_valor_entidad.ParameterName = "p_valor_entidad";
                        p_valor_entidad.Value = 0;
                        p_valor_entidad.DbType = DbType.Decimal;
                        p_valor_entidad.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pn_num_producto);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_cliente);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_ope);
                        cmdTransaccionFactory.Parameters.Add(pf_fecha_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_tipo_tran);
                        cmdTransaccionFactory.Parameters.Add(pn_documento);
                        cmdTransaccionFactory.Parameters.Add(pn_operacion_tarjeta);
                        cmdTransaccionFactory.Parameters.Add(pn_num_tran_tarjeta);
                        cmdTransaccionFactory.Parameters.Add(p_valor_cliente);
                        cmdTransaccionFactory.Parameters.Add(p_valor_entidad);

                        for (int i=0;i<cmdTransaccionFactory.Parameters.Count; i++)
                            seguimiento += i + "=>" + cmdTransaccionFactory.Parameters[i].ParameterName + ":" + (cmdTransaccionFactory.Parameters[i].Value != null ? cmdTransaccionFactory.Parameters[i].Value : "") + "  ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_TRANSACCION";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        if (pMovimiento.tipo_tran == 241)
                        {
                            if (connection.State == ConnectionState.Open)
                                dbConnectionFactory.CerrarConexion(connection);
                            return;
                        }
                        else
                        {
                            Error += seguimiento + " " + ex.Message;
                            BOExcepcion.Throw("", "", ex);
                        }
                    }

                    if (connection.State == ConnectionState.Open)
                        dbConnectionFactory.CerrarConexion(connection);

                }
            }
        }

        public Tarjeta ConsultarTarjeta(string pTarjeta, string pCuenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            Tarjeta entidad = new Tarjeta();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TARJETA WHERE numero_tarjeta = '" + pTarjeta + "' AND numero_cuenta = '" + pCuenta + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDTARJETA"] != DBNull.Value) entidad.idtarjeta = Convert.ToInt32(resultado["IDTARJETA"]);
                            if (resultado["NUMERO_TARJETA"] != DBNull.Value) entidad.numtarjeta = Convert.ToString(resultado["NUMERO_TARJETA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToString(resultado["TIPO_CUENTA"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["CUENTA_HOMOLOGA"] != DBNull.Value) entidad.cuenta_homologa = Convert.ToString(resultado["CUENTA_HOMOLOGA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_ASIGNACION"] != DBNull.Value) entidad.fecha_asignacion = Convert.ToDateTime(resultado["FECHA_ASIGNACION"]);
                            if (resultado["COD_CONVENIO"] != DBNull.Value) entidad.cod_convenio = Convert.ToInt32(resultado["COD_CONVENIO"]);
                            if (resultado["FECHA_PROCESO"] != DBNull.Value) entidad.fecha_proceso = Convert.ToDateTime(resultado["FECHA_PROCESO"]);
                            if (resultado["FECHA_ACTIVACION"] != DBNull.Value) entidad.fecha_activacion = Convert.ToDateTime(resultado["FECHA_ACTIVACION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_DISPONIBLE"] != DBNull.Value) entidad.saldo_disponible = Convert.ToDecimal(resultado["SALDO_DISPONIBLE"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            if (resultado["CUPO"] != DBNull.Value) entidad.cupo = Convert.ToDecimal(resultado["CUPO"]);
                            if (resultado["MAX_TRAN"] != DBNull.Value) entidad.max_tran = Convert.ToInt32(resultado["MAX_TRAN"]);
                            if (resultado["COBRA_CUOTA_MANEJO"] != DBNull.Value) entidad.cobra_cuota_manejo = Convert.ToInt32(resultado["COBRA_CUOTA_MANEJO"]);
                            if (resultado["CUOTA_MANEJO"] != DBNull.Value) entidad.cuota_manejo = Convert.ToDecimal(resultado["CUOTA_MANEJO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        if (connection.State == ConnectionState.Open)
                            dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                }
            }
        }

        public Tarjeta ConsultarTarjeta(string pTarjeta, string pCuenta, DbConnection connection, Usuario vUsuario)
        {
            DbDataReader resultado;
            Tarjeta entidad = new Tarjeta();
            using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
            {
                try
                {
                    string sql = @"SELECT * FROM TARJETA WHERE numero_tarjeta = '" + pTarjeta + "' AND numero_cuenta = '" + pCuenta + "' ";
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();
                    if (resultado.Read())
                    {
                        if (resultado["IDTARJETA"] != DBNull.Value) entidad.idtarjeta = Convert.ToInt32(resultado["IDTARJETA"]);
                        if (resultado["NUMERO_TARJETA"] != DBNull.Value) entidad.numtarjeta = Convert.ToString(resultado["NUMERO_TARJETA"]);
                        if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToString(resultado["TIPO_CUENTA"]);
                        if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                        if (resultado["CUENTA_HOMOLOGA"] != DBNull.Value) entidad.cuenta_homologa = Convert.ToString(resultado["CUENTA_HOMOLOGA"]);
                        if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                        if (resultado["FECHA_ASIGNACION"] != DBNull.Value) entidad.fecha_asignacion = Convert.ToDateTime(resultado["FECHA_ASIGNACION"]);
                        if (resultado["COD_CONVENIO"] != DBNull.Value) entidad.cod_convenio = Convert.ToInt32(resultado["COD_CONVENIO"]);
                        if (resultado["FECHA_PROCESO"] != DBNull.Value) entidad.fecha_proceso = Convert.ToDateTime(resultado["FECHA_PROCESO"]);
                        if (resultado["FECHA_ACTIVACION"] != DBNull.Value) entidad.fecha_activacion = Convert.ToDateTime(resultado["FECHA_ACTIVACION"]);
                        if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                        if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                        if (resultado["SALDO_DISPONIBLE"] != DBNull.Value) entidad.saldo_disponible = Convert.ToDecimal(resultado["SALDO_DISPONIBLE"]);
                        if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                        if (resultado["CUPO"] != DBNull.Value) entidad.cupo = Convert.ToDecimal(resultado["CUPO"]);
                        if (resultado["MAX_TRAN"] != DBNull.Value) entidad.max_tran = Convert.ToInt32(resultado["MAX_TRAN"]);
                        if (resultado["COBRA_CUOTA_MANEJO"] != DBNull.Value) entidad.cobra_cuota_manejo = Convert.ToInt32(resultado["COBRA_CUOTA_MANEJO"]);
                        if (resultado["CUOTA_MANEJO"] != DBNull.Value) entidad.cuota_manejo = Convert.ToDecimal(resultado["CUOTA_MANEJO"]);
                        if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                    }
                    return entidad;
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Consultar los movimientos realizados en tabla TRAN_TARJETA
        /// </summary>
        /// <param name="ptipo_convenio"></param>
        /// <param name="pTarjeta"></param>
        /// <param name="pOperacion"></param>
        /// <param name="ptipoTransaccion"></param>
        /// <param name="pDocumento"></param>
        /// <param name="pFecha"></param>
        /// <param name="pValor"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Movimiento ConsultarMovimiento(int ptipo_convenio, string pTarjeta, string pOperacion, string ptipoTransaccion, string pDocumento, string pFecha, decimal pValor, Usuario vUsuario)
        {
            DbDataReader resultado;
            Movimiento entidad = new Movimiento();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string _filtroTarjeta = (pTarjeta.Trim() != "" ? " AND TARJETA = '" + pTarjeta + "' " : " ");
                    string sql = "";
                    if (ptipoTransaccion == "5" || ptipoTransaccion == "6")
                        sql = @"SELECT * FROM TRAN_TARJETA WHERE OPERACION = '" + pOperacion.ToString() + "' AND TIPOTRANSACCION = '" + ptipoTransaccion + "' " + _filtroTarjeta;
                    else if (ptipoTransaccion == "M")
                        sql = @"SELECT * FROM TRAN_TARJETA WHERE OPERACION = '" + pOperacion.ToString() + "' AND DOCUMENTO = '" + pDocumento + "' " + _filtroTarjeta + " AND FECHA = '" + pFecha + "' ";
                    else if (ptipoTransaccion == "4")
                        sql = @"SELECT * FROM TRAN_TARJETA WHERE OPERACION = '" + pOperacion.ToString() + "' AND FECHA = '" + pFecha + "' AND TIPOTRANSACCION = '" + ptipoTransaccion + "' " + _filtroTarjeta;
                    else
                        sql = @"SELECT * FROM TRAN_TARJETA WHERE OPERACION = '" + pOperacion.ToString() + "' AND TIPOTRANSACCION NOT IN ('5', '6') AND FECHA = '" + pFecha + "' " + _filtroTarjeta;
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUM_TRAN"] != DBNull.Value) entidad.num_tran = Convert.ToInt32(resultado["NUM_TRAN"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToString(resultado["FECHA"]);
                            if (resultado["HORA"] != DBNull.Value) entidad.hora = Convert.ToString(resultado["HORA"]);
                            if (resultado["DOCUMENTO"] != DBNull.Value) entidad.documento = Convert.ToString(resultado["DOCUMENTO"]);
                            if (resultado["NROCUENTA"] != DBNull.Value) entidad.nrocuenta = Convert.ToString(resultado["NROCUENTA"]);
                            if (resultado["TARJETA"] != DBNull.Value) entidad.tarjeta = Convert.ToString(resultado["TARJETA"]);
                            if (resultado["TIPOTRANSACCION"] != DBNull.Value) entidad.tipotransaccion = Convert.ToString(resultado["TIPOTRANSACCION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["MONTO"]);
                            if (resultado["COMISION"] != DBNull.Value) entidad.comision = Convert.ToDecimal(resultado["COMISION"]);
                            if (resultado["LUGAR"] != DBNull.Value) entidad.lugar = Convert.ToString(resultado["LUGAR"]);
                            if (resultado["OPERACION"] != DBNull.Value) entidad.operacion = Convert.ToString(resultado["OPERACION"]);
                            if (resultado["RED"] != DBNull.Value) entidad.red = Convert.ToString(resultado["RED"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_cliente = Convert.ToInt64(resultado["COD_PERSONA"]);
                        }
                        else
                        {
                            dbConnectionFactory.CerrarConexion(connection);
                            return null;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        if (connection.State == ConnectionState.Open)
                            dbConnectionFactory.CerrarConexion(connection);
                        BOExcepcion.Throw("MovimientoData", "ConsultarMovimiento" + " TipoTransaccion: ->" + ptipoTransaccion + "<- " + sql, ex);
                        return null;
                    }
                }
            }
        }

        public Movimiento DatosDeAplicacion(Int32? pnum_tran, string pnumero_cuenta, Int64? pcod_ope, Int64? pcod_persona, DateTime pFecha, decimal pValor, int? pTipoTran, string pOperacionTarjeta, ref string pError, Usuario pUsuario)
        {
            DbDataReader resultado = null;
            Movimiento entidad = new Movimiento();
            string operacionTarjeta = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        Configuracion conf = new Configuracion();
                        string fecha = dbConnectionFactory.TipoConexion() == "ORACLE" ? " To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " : " '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' ";
                        string valor = "";
                        valor = Math.Abs(pValor).ToString().Replace(conf.ObtenerSeparadorDecimalConfig(), ".");
                        bool bEncontrado = false;
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        // Determinar si la aplicación fue por ahorro a la vista
                        if (EsAhorroVista(pnumero_cuenta, connection, pUsuario))
                        {
                            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            /// AHORRO A LA VISTA
                            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            // Buscar por número de transacción y tipo de transaccion (Aplica para buscar la comisión).
                            if (!bEncontrado && pnum_tran != null && pTipoTran != null && pTipoTran != 0 && pTipoTran != null)
                            {
                                if (pTipoTran == 243 || pTipoTran == 244)
                                    // Validar si ya fue aplicada la comisión
                                    cmdTransaccionFactory.CommandText = @"SELECT T.*, O.NUM_COMP, O.TIPO_COMP FROM TRAN_AHORRO T JOIN OPERACION O ON T.COD_OPE = O.COD_OPE 
                                                                        WHERE O.TIPO_OPE = 124 AND T.NUM_TRAN_TARJETA = " + pnum_tran.ToString() + " AND T.TIPO_TRAN = " + pTipoTran.ToString();
                                else
                                    // Validar si ya fue aplicada la transascción
                                    cmdTransaccionFactory.CommandText = @"SELECT T.*, O.NUM_COMP, O.TIPO_COMP FROM TRAN_AHORRO T JOIN OPERACION O ON T.COD_OPE = O.COD_OPE JOIN TIPO_TRAN R ON T.TIPO_TRAN = R.TIPO_TRAN 
                                                                            WHERE O.TIPO_OPE = 124 AND T.NUM_TRAN_TARJETA = " + pnum_tran.ToString() + " AND T.TIPO_TRAN NOT IN (225, 243, 244) AND R.TIPO_MOV IN (SELECT X.TIPO_MOV FROM TIPO_TRAN X WHERE X.TIPO_TRAN = " + pTipoTran.ToString() + ") ";
                                pError = cmdTransaccionFactory.CommandText;
                                resultado = cmdTransaccionFactory.ExecuteReader();
                                if (resultado.Read())
                                    bEncontrado = true;
                            }
                            // Buscar por código de operación, número de cuenta y valor
                            if (!bEncontrado)
                            {
                                if (pTipoTran == 241) // La cuota de manejo siempre trae el mismo número de operación de tarjeta asi que toca tambien buscar por la operación
                                    cmdTransaccionFactory.CommandText = @"SELECT T.*, O.NUM_COMP, O.TIPO_COMP FROM TRAN_AHORRO T JOIN OPERACION O ON T.COD_OPE = O.COD_OPE 
                                                                        WHERE T.NUMERO_CUENTA = '" + pnumero_cuenta.ToString() + "' AND T.TIPO_TRAN = " + Convert.ToInt32(pTipoTran).ToString() + " AND T.COD_OPE = " + (pcod_ope == null ? "0" : pcod_ope.ToString()) + @"
                                                                        AND T.OPERACION_TARJETA = '" + pOperacionTarjeta + "' ";
                                else if (pTipoTran == 238) // Pago de servicios públicos llega como compra o retiro en transacciones en línea
                                    cmdTransaccionFactory.CommandText = @"SELECT T.*, O.NUM_COMP, O.TIPO_COMP FROM TRAN_AHORRO T JOIN OPERACION O ON T.COD_OPE = O.COD_OPE 
                                                                        WHERE T.NUMERO_CUENTA = '" + pnumero_cuenta.ToString() + "' AND T.TIPO_TRAN IN (231, 235, 238) " + @"
                                                                        AND T.OPERACION_TARJETA = '" + pOperacionTarjeta + "' AND EXTRACT(YEAR FROM O.FECHA_OPER) = EXTRACT(YEAR FROM " + fecha + ") ";
                                else if (pTipoTran == 243) // Pago comisiones
                                    cmdTransaccionFactory.CommandText = @"SELECT T.*, O.NUM_COMP, O.TIPO_COMP FROM TRAN_AHORRO T JOIN OPERACION O ON T.COD_OPE = O.COD_OPE 
                                                                        WHERE T.NUMERO_CUENTA = '" + pnumero_cuenta.ToString() + "' AND T.TIPO_TRAN = " + Convert.ToInt32(pTipoTran).ToString() + @"
                                                                        AND T.OPERACION_TARJETA = '" + pOperacionTarjeta + "' AND EXTRACT(YEAR FROM O.FECHA_OPER) = EXTRACT(YEAR FROM " + fecha + ") AND EXTRACT(MONTH FROM O.FECHA_OPER) = EXTRACT(MONTH FROM " + fecha + ") ";
                                else
                                    cmdTransaccionFactory.CommandText = @"SELECT T.*, O.NUM_COMP, O.TIPO_COMP FROM TRAN_AHORRO T JOIN OPERACION O ON T.COD_OPE = O.COD_OPE 
                                                                        WHERE T.NUMERO_CUENTA = '" + pnumero_cuenta.ToString() + "' AND T.TIPO_TRAN = " + Convert.ToInt32(pTipoTran).ToString() + @"
                                                                        AND T.OPERACION_TARJETA = '" + pOperacionTarjeta + "' AND EXTRACT(YEAR FROM O.FECHA_OPER) = EXTRACT(YEAR FROM " + fecha + ") ";
                                pError = cmdTransaccionFactory.CommandText;
                                resultado = cmdTransaccionFactory.ExecuteReader();
                                if (resultado.Read())
                                    bEncontrado = true;
                            }
                            // Cargar los datos
                            if (bEncontrado)
                            {
                                if (resultado["NUM_TRAN"] != DBNull.Value) entidad.num_tran_apl = Convert.ToInt64(resultado["NUM_TRAN"]);
                                if (resultado["VALOR"] != DBNull.Value) entidad.valor_apl = Convert.ToDecimal(resultado["VALOR"]);
                                if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope_apl = Convert.ToInt64(resultado["COD_OPE"]);
                                if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp_apl = Convert.ToInt64(resultado["NUM_COMP"]);
                                if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp_apl = Convert.ToInt64(resultado["TIPO_COMP"]);
                                if (resultado["OPERACION_TARJETA"] != DBNull.Value) operacionTarjeta = Convert.ToString(resultado["OPERACION_TARJETA"]);
                                // Revisar si hay más registros y totalizar lo aplicado
                                decimal total_apl = 0;
                                while (resultado.Read())
                                {
                                    decimal valor_apl = 0;
                                    if (resultado["VALOR"] != DBNull.Value) valor_apl = Convert.ToDecimal(resultado["VALOR"]);
                                    total_apl += valor_apl;
                                }
                                entidad.valor_apl += total_apl;
                            }
                            /// Determinar si aplico algún valor a la cuenta por cobrar
                            decimal valor_cuenta_por_cobrar = 0;
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            if (pTipoTran == 243 || pTipoTran == 244)
                                cmdTransaccionFactory.CommandText = @"SELECT NVL(SUM(VALOR), 0) AS VALOR FROM CUENTAPORCOBRAR_AHORRO a WHERE a.NUM_TRAN_TARJETA = " + pnum_tran.ToString() + " AND a.NUMERO_CUENTA = '" + pnumero_cuenta + "' AND a.ESCOMISION = 1";
                            else
                                cmdTransaccionFactory.CommandText = @"SELECT NVL(SUM(VALOR), 0) AS VALOR FROM CUENTAPORCOBRAR_AHORRO a WHERE a.NUM_TRAN_TARJETA = " + pnum_tran.ToString() + " AND a.NUMERO_CUENTA = '" + pnumero_cuenta + "' AND (a.ESCOMISION != 1 OR a.ESCOMISION IS NULL)";
                            pError = cmdTransaccionFactory.CommandText;
                            resultado = cmdTransaccionFactory.ExecuteReader();
                            if (bEncontrado)
                            {
                                try { if (resultado["VALOR"] != DBNull.Value) valor_cuenta_por_cobrar = Convert.ToDecimal(resultado["VALOR"]); }
                                catch { }
                                if (bEncontrado)
                                    entidad.valor_apl += valor_cuenta_por_cobrar;
                            }
                        }
                        else
                        {
                            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            /// CREDITOS
                            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            if (connection.State == ConnectionState.Closed)
                                connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            // Buscar por número de transacción y tipo de transaccion (Aplica para buscar la comisión).
                            if (!bEncontrado && pnum_tran != null && pTipoTran != null && pTipoTran != 0 && pTipoTran != null)
                            {
                                cmdTransaccionFactory.CommandText = @"SELECT T.*, O.NUM_COMP, O.TIPO_COMP FROM TRAN_CRED T JOIN OPERACION O ON T.COD_OPE = O.COD_OPE 
                                                                        WHERE T.NUM_TRAN_TARJETA = " + (pnum_tran == null ? "0" : pnum_tran.ToString()) + " AND T.TIPO_TRAN = " + pTipoTran.ToString();
                                pError = cmdTransaccionFactory.CommandText;
                                resultado = cmdTransaccionFactory.ExecuteReader();
                                if (resultado.Read())
                                    bEncontrado = true;
                            }
                            // Buscar por código de operación, número de radicación y valor
                            if (!bEncontrado)
                            {
                                if (pTipoTran == 238) // Pago de servicios públicos llega como compra o retiro en transacciones en línea
                                    cmdTransaccionFactory.CommandText = @"SELECT T.*, O.NUM_COMP, O.TIPO_COMP FROM TRAN_CRED T JOIN OPERACION O ON T.COD_OPE = O.COD_OPE 
                                                                            WHERE T.NUMERO_RADICACION = " + pnumero_cuenta.ToString() + " AND T.TIPO_TRAN IN (231, 235, 238) AND T.COD_OPE = " + pcod_ope.ToString() + @" 
                                                                            AND T.OPERACION_TARJETA = '" + pOperacionTarjeta + "' ";
                                else
                                    cmdTransaccionFactory.CommandText = @"SELECT T.*, O.NUM_COMP, O.TIPO_COMP FROM TRAN_CRED T JOIN OPERACION O ON T.COD_OPE = O.COD_OPE 
                                                                            WHERE T.NUMERO_RADICACION = " + pnumero_cuenta.ToString() + " AND T.TIPO_TRAN = " + pTipoTran + " AND T.COD_OPE = " + pcod_ope.ToString() + @" 
                                                                            AND T.OPERACION_TARJETA = '" + pOperacionTarjeta + "' ";
                                pError = cmdTransaccionFactory.CommandText;
                                resultado = cmdTransaccionFactory.ExecuteReader();
                                if (resultado.Read())
                                    bEncontrado = true;
                            }
                            // Cargar los datos
                            if (bEncontrado)
                            {
                                if (resultado["NUM_TRAN"] != DBNull.Value) entidad.num_tran_apl = Convert.ToInt64(resultado["NUM_TRAN"]);
                                if (resultado["VALOR"] != DBNull.Value) entidad.valor_apl = Convert.ToDecimal(resultado["VALOR"]);
                                if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope_apl = Convert.ToInt64(resultado["COD_OPE"]);
                                if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp_apl = Convert.ToInt64(resultado["NUM_COMP"]);
                                if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp_apl = Convert.ToInt64(resultado["TIPO_COMP"]);
                                if (resultado["OPERACION_TARJETA"] != DBNull.Value) operacionTarjeta = Convert.ToString(resultado["OPERACION_TARJETA"]);
                                // Revisar si hay más registros y totalizar lo aplicado
                                decimal total_apl = 0;
                                while (resultado.Read())
                                {
                                    decimal valor_apl = 0;
                                    if (resultado["VALOR"] != DBNull.Value) valor_apl = Convert.ToDecimal(resultado["VALOR"]);
                                    total_apl += valor_apl;
                                }
                                entidad.valor_apl += total_apl;
                            }
                        }
                        // Determinar valor de la cuenta por cobrar
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"SELECT NVL(SUM(VALOR), 0) AS VALOR FROM cuentaporcobrar_ahorro c WHERE c.cod_ope = " + pcod_ope + " AND c.numero_cuenta = '" + pnumero_cuenta+ "' AND (c.num_tran_tarjeta = " + pnum_tran.ToString() + " Or c.num_tran_tarjeta Is Null) ";
                        pError = cmdTransaccionFactory.CommandText;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                            if (resultado["VALOR"] != DBNull.Value) entidad.cuenta_porcobrar = Convert.ToDecimal(resultado["VALOR"]); ;
                        // Cerrar la conexiòn
                        if (connection.State == ConnectionState.Open)
                            dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        if (connection.State == ConnectionState.Open)
                            dbConnectionFactory.CerrarConexion(connection);
                        BOExcepcion.Throw("MovimientoData", "ConsultarMovimiento", ex);
                        return null;
                    }
                }
            }
        }

        public int? HomologaTipoTran(string ptipotransaccion)
        {
            int? tipo_tran = null;
            if (ptipotransaccion == "1")  // Consulta.        Débito
                tipo_tran = 230;
            if (ptipotransaccion == "2")  // Retiro.          Débito
                tipo_tran = 231;
            if (ptipotransaccion == "3")  // Pago o Compra    Débito
                tipo_tran = 235;
            if (ptipotransaccion == "4")  // Declinada        Débito
                tipo_tran = 233;
            if (ptipotransaccion == "5")  // Ajuste Crédito   Crédito
                tipo_tran = 234;
            if (ptipotransaccion == "6")  // Ajuste Débito    Débito
                tipo_tran = 232;
            if (ptipotransaccion == "7")  // Otras            Débito
                tipo_tran = 236;
            if (ptipotransaccion == "8")  // Consignación     Crédito
                tipo_tran = 237;
            if (ptipotransaccion == "9")  // Pago Servicios P.Débito    
                tipo_tran = 238;
            if (ptipotransaccion == "A")  // Cambio Pin       Débito
                tipo_tran = 239;
            if (ptipotransaccion == "B")  // Consulta Costo   Débito
                tipo_tran = 240;
            if (ptipotransaccion == "M")  // Cuota Manejo     Débito
                tipo_tran = 241;
            if (ptipotransaccion == "P")  // Cobro Plástico   Débito
                tipo_tran = 242;
            return tipo_tran;
        }

        public bool EsAhorroVista(string pCuenta, DbConnection pconnection, Usuario pUsuario)
        {
            DbDataReader resultado;
            bool esAhorroVista = false;
            using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
            {
                try
                {
                    string sql;
                    sql = @"SELECT * FROM AHORRO_VISTA WHERE numero_cuenta = '" + pCuenta + "' ";
                    if (pconnection.State == ConnectionState.Closed)
                        pconnection.Open();
                    cmdTransaccionFactory.Connection = pconnection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();
                    if (resultado.Read())
                    {
                        esAhorroVista = true;
                    }
                    return esAhorroVista;
                }
                catch
                {
                    return esAhorroVista;
                }
            }
        }

        public bool EsRotativo(string pCuenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            bool esRotativo = false;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql;
                        sql = @"SELECT * FROM CREDITO WHERE numero_radicacion = " + pCuenta + " ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            esRotativo = true;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return esRotativo;
                    }
                    catch
                    {
                        return esRotativo;
                    }
                }
            }
        }

        public Tarjeta ConsultarCuenta(string pTipoCuenta, string pCuenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            Tarjeta entidad = new Tarjeta();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql;
                        if (pTipoCuenta == "C")
                            sql = @"SELECT numero_radicacion AS numero_cuenta, cod_deudor As cod_persona, monto_aprobado - saldo_capital As saldo_total, 0 As saldo_canje, estado 
                                      FROM CREDITO WHERE numero_radicacion = " + pCuenta + " ";
                        else
                            sql = @"SELECT * FROM AHORRO_VISTA WHERE numero_cuenta = '" + pCuenta + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);                            
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            entidad.saldo_disponible = entidad.saldo_total - entidad.saldo_canje;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch
                    {
                        if (connection.State == ConnectionState.Open)
                            dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                }
            }
        }

        public TDCreditoSolicitado ConsultarCreditoRotativo(Int64 pnumero_radicacion, Usuario pUsuario)
        {
            TDCreditoSolicitado entidad;
            DbDataReader resultado = default(DbDataReader);
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * From credito c Inner Join lineascredito l On c.cod_linea_credito = l.cod_linea_credito Where l.tipo_linea = 2 And c.numero_radicacion = " + pnumero_radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            entidad = new TDCreditoSolicitado();
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroCredito = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["cod_deudor"] != DBNull.Value) entidad.Cod_deudor = Convert.ToInt64(resultado["cod_deudor"]);
                            if (resultado["monto_solicitado"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["monto_solicitado"]);
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.MontoAprobado = Convert.ToDecimal(resultado["monto_aprobado"]);
                            if (resultado["numero_cuotas"] != DBNull.Value) entidad.Plazo = Convert.ToString(resultado["numero_cuotas"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.Cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["cod_periodicidad"] != DBNull.Value) entidad.cod_Periodicidad = Convert.ToInt32(resultado["cod_periodicidad"]);
                            if (resultado["fecha_solicitud"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha_solicitud"]);
                            if (resultado["forma_pago"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["forma_pago"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["cod_oficina"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["saldo_capital"]);
                            dbConnectionFactory.CerrarConexion(connection);
                            return entidad;
                        }
                        else
                        {
                            dbConnectionFactory.CerrarConexion(connection);
                            return null;
                        }
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public List<Tarjeta> ListarTarjetas(Tarjeta pTarjeta, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Tarjeta> lstTarjeta = new List<Tarjeta>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"SELECT t.*, p.identificacion, p.nombre, c.cupo_cajero, c.transacciones_cajero, c.cupo_datafono, c.transacciones_datafono 
                                        FROM tarjeta t Inner Join v_persona p On t.cod_persona = p.cod_persona Left Join tarjeta_convenio c On t.cod_convenio = c.cod_convenio
                                        " + ObtenerFiltroII(pTarjeta, "") + " and t.estado != 2 ORDER BY t.numero_tarjeta, t.numero_cuenta ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Tarjeta entidad = new Tarjeta();
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NUMERO_TARJETA"] != DBNull.Value) entidad.numtarjeta = Convert.ToString(resultado["NUMERO_TARJETA"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_DISPONIBLE"] != DBNull.Value) entidad.saldo_disponible = Convert.ToDecimal(resultado["SALDO_DISPONIBLE"]);
                            if (resultado["CUPO_CAJERO"] != DBNull.Value) entidad.cupo_cajero = Convert.ToDecimal(resultado["CUPO_CAJERO"]);
                            if (resultado["TRANSACCIONES_CAJERO"] != DBNull.Value) entidad.transacciones_cajero = Convert.ToDecimal(resultado["TRANSACCIONES_CAJERO"]);
                            if (resultado["CUPO_DATAFONO"] != DBNull.Value) entidad.cupo_datafono = Convert.ToDecimal(resultado["CUPO_DATAFONO"]);
                            if (resultado["TRANSACCIONES_DATAFONO"] != DBNull.Value) entidad.transacciones_datafono = Convert.ToDecimal(resultado["TRANSACCIONES_DATAFONO"]);
                            if (resultado["ESTADO_SALDO"] != DBNull.Value) entidad.estado_saldo = Convert.ToInt32(resultado["ESTADO_SALDO"]);
                            lstTarjeta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTarjeta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaData", "ListarTarjetas", ex);
                        return null;
                    }
                }
            }
        }

        public TDAvance CrearCreditoAvance(TDAvance pAvance, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidavance = cmdTransaccionFactory.CreateParameter();
                        pidavance.ParameterName = "p_idavance";
                        pidavance.Value = pAvance.idavance;
                        pidavance.Direction = ParameterDirection.Output;
                        pidavance.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidavance);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pAvance.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pfecha_solicitud = cmdTransaccionFactory.CreateParameter();
                        pfecha_solicitud.ParameterName = "p_fecha_solicitud";
                        pfecha_solicitud.Value = pAvance.fecha_solicitud;
                        pfecha_solicitud.Direction = ParameterDirection.Input;
                        pfecha_solicitud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_solicitud);

                        DbParameter pfecha_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                        if (pAvance.fecha_aprobacion != DateTime.MinValue) pfecha_aprobacion.Value = pAvance.fecha_aprobacion; else pfecha_aprobacion.Value = DBNull.Value;
                        pfecha_aprobacion.Direction = ParameterDirection.Input;
                        pfecha_aprobacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aprobacion);

                        DbParameter pfecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        pfecha_desembolso.ParameterName = "p_fecha_desembolso";
                        if (pAvance.fecha_desembolso != DateTime.MinValue) pfecha_desembolso.Value = pAvance.fecha_desembolso; else pfecha_desembolso.Value = DBNull.Value;
                        pfecha_desembolso.Direction = ParameterDirection.Input;
                        pfecha_desembolso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_desembolso);

                        DbParameter pvalor_solicitado = cmdTransaccionFactory.CreateParameter();
                        pvalor_solicitado.ParameterName = "p_valor_solicitado";
                        pvalor_solicitado.Value = pAvance.valor_solicitado;
                        pvalor_solicitado.Direction = ParameterDirection.Input;
                        pvalor_solicitado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_solicitado);

                        DbParameter pvalor_aprobado = cmdTransaccionFactory.CreateParameter();
                        pvalor_aprobado.ParameterName = "p_valor_aprobado";
                        if (pAvance.valor_aprobado != 0) pvalor_aprobado.Value = pAvance.valor_aprobado; else pvalor_aprobado.Value = DBNull.Value;
                        pvalor_aprobado.Direction = ParameterDirection.Input;
                        pvalor_aprobado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_aprobado);

                        DbParameter pvalor_desembolsado = cmdTransaccionFactory.CreateParameter();
                        pvalor_desembolsado.ParameterName = "p_valor_desembolsado";
                        if (pAvance.valor_desembolsado != 0) pvalor_desembolsado.Value = pAvance.valor_desembolsado; else pvalor_desembolsado.Value = DBNull.Value;
                        pvalor_desembolsado.Direction = ParameterDirection.Input;
                        pvalor_desembolsado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_desembolsado);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pAvance.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pAvance.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pobservacion = cmdTransaccionFactory.CreateParameter();
                        pobservacion.ParameterName = "p_observacion";
                        if (pAvance.observacion != null) pobservacion.Value = pAvance.observacion; else pobservacion.Value = DBNull.Value;
                        pobservacion.Direction = ParameterDirection.Input;
                        pobservacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservacion);

                        DbParameter P_VALOR_CUOTA = cmdTransaccionFactory.CreateParameter();
                        P_VALOR_CUOTA.ParameterName = "P_VALOR_CUOTA";
                        if (pAvance.valor_cuota != 0) P_VALOR_CUOTA.Value = pAvance.valor_cuota; else P_VALOR_CUOTA.Value = DBNull.Value;
                        P_VALOR_CUOTA.Direction = ParameterDirection.Input;
                        P_VALOR_CUOTA.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR_CUOTA);

                        DbParameter P_PLAZO = cmdTransaccionFactory.CreateParameter();
                        P_PLAZO.ParameterName = "P_PLAZO";
                        if (pAvance.plazo_diferir != 0) P_PLAZO.Value = pAvance.plazo_diferir; else P_VALOR_CUOTA.Value = DBNull.Value;
                        P_PLAZO.Direction = ParameterDirection.Input;
                        P_PLAZO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_PLAZO);

                        DbParameter P_FORMA_TASA = cmdTransaccionFactory.CreateParameter();
                        P_FORMA_TASA.ParameterName = "P_FORMA_TASA";
                        if (pAvance.forma_tasa != 0 && pAvance.forma_tasa != null)
                            P_FORMA_TASA.Value = pAvance.forma_tasa;
                        else
                            P_FORMA_TASA.Value = DBNull.Value;
                        P_FORMA_TASA.Direction = ParameterDirection.Input;
                        P_FORMA_TASA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_FORMA_TASA);

                        DbParameter P_TIPO_HISTORICO = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_HISTORICO.ParameterName = "P_TIPO_HISTORICO";
                        if (pAvance.tipo_historico != 0 && pAvance.tipo_historico != null)
                            P_TIPO_HISTORICO.Value = pAvance.tipo_historico;
                        else
                            P_TIPO_HISTORICO.Value = DBNull.Value;
                        P_TIPO_HISTORICO.Direction = ParameterDirection.Input;
                        P_TIPO_HISTORICO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_HISTORICO);

                        DbParameter P_DESVIACION = cmdTransaccionFactory.CreateParameter();
                        P_DESVIACION.ParameterName = "P_DESVIACION";
                        if (pAvance.desviacion != 0 && pAvance.desviacion != null)
                            P_DESVIACION.Value = pAvance.desviacion;
                        else
                            P_DESVIACION.Value = DBNull.Value;
                        P_DESVIACION.Direction = ParameterDirection.Input;
                        P_DESVIACION.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_DESVIACION);

                        DbParameter P_TIPO_TASA = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_TASA.ParameterName = "P_TIPO_TASA";
                        if (pAvance.tipo_tasa != 0 && pAvance.tipo_tasa != null)
                            P_TIPO_TASA.Value = pAvance.tipo_tasa;
                        else
                            P_TIPO_TASA.Value = DBNull.Value;
                        P_TIPO_TASA.Direction = ParameterDirection.Input;
                        P_TIPO_TASA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_TASA);

                        DbParameter P_TASA_INTERES = cmdTransaccionFactory.CreateParameter();
                        P_TASA_INTERES.ParameterName = "P_TASA_INTERES";
                        if (pAvance.tasa != 0 && pAvance.tasa != null)
                            P_TASA_INTERES.Value = pAvance.tasa;
                        else
                            P_TASA_INTERES.Value = DBNull.Value;
                        P_TASA_INTERES.Direction = ParameterDirection.Input;
                        P_TASA_INTERES.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_TASA_INTERES);

                        DbParameter P_SALDO_AVANCE = cmdTransaccionFactory.CreateParameter();
                        P_SALDO_AVANCE.ParameterName = "P_SALDO_AVANCE";
                        if (pAvance.saldo_avance != 0) P_SALDO_AVANCE.Value = pAvance.saldo_avance; else P_SALDO_AVANCE.Value = DBNull.Value;
                        P_SALDO_AVANCE.Direction = ParameterDirection.Input;
                        P_SALDO_AVANCE.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_SALDO_AVANCE);

                        DbParameter P_FECHA_PROXIMO_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_PROXIMO_PAGO.ParameterName = "P_FECHA_PROXIMO_PAGO";
                        P_FECHA_PROXIMO_PAGO.Value = pAvance.fecha_solicitud;
                        P_FECHA_PROXIMO_PAGO.Direction = ParameterDirection.Input;
                        P_FECHA_PROXIMO_PAGO.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_PROXIMO_PAGO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CREDAVANCE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAvance.idavance = Convert.ToInt32(pidavance.Value);
                        return pAvance;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "CrearCreditoAvance", ex);
                        return null;
                    }
                }
            }
        }

        public Int64 DesembolsarAvances(TDCredito pCredito, ref decimal pMontoDesembolso, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.Value = pCredito.numero_radicacion;

                        DbParameter p_idavance = cmdTransaccionFactory.CreateParameter();
                        p_idavance.ParameterName = "p_idavance";
                        p_idavance.Value = pCredito.numero_credito;
                        p_idavance.DbType = DbType.AnsiString;
                        p_idavance.Direction = ParameterDirection.Input;

                        DbParameter p_fecha_prim_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_prim_pago.ParameterName = "p_fecha_prim_pago";
                        if (pCredito.fecha_prim_pago == null)
                            p_fecha_prim_pago.Value = DBNull.Value;
                        else
                            p_fecha_prim_pago.Value = pCredito.fecha_prim_pago;
                        p_fecha_prim_pago.DbType = DbType.Date;

                        DbParameter p_fecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        p_fecha_desembolso.ParameterName = "p_fecha_desembolso";
                        p_fecha_desembolso.Value = pCredito.fecha_desembolso;

                        DbParameter p_Usuario = cmdTransaccionFactory.CreateParameter();
                        p_Usuario.ParameterName = "p_Usuario";
                        p_Usuario.Value = pUsuario.codusuario;
                        p_Usuario.Direction = ParameterDirection.Input;
                        p_Usuario.Size = 50;

                        DbParameter p_Cod_Ope = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Ope.ParameterName = "p_cod_ope";
                        p_Cod_Ope.Value = pCredito.cod_ope;
                        p_Cod_Ope.Direction = ParameterDirection.Output;

                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        p_ip.Value = pUsuario.IP;

                        DbParameter p_valor_desembolsado = cmdTransaccionFactory.CreateParameter();
                        p_valor_desembolsado.ParameterName = "p_valor_desembolsado";
                        p_valor_desembolsado.Value = pCredito.monto;
                        p_valor_desembolsado.Direction = ParameterDirection.Input;

                        DbParameter p_plazo = cmdTransaccionFactory.CreateParameter();
                        p_plazo.ParameterName = "p_plazo";
                        p_plazo.Value = pCredito.plazo;
                        p_plazo.Direction = ParameterDirection.Input;

                        DbParameter p_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tran.ParameterName = "p_tipo_tran";
                        p_tipo_tran.Value = 982;
                        p_tipo_tran.Direction = ParameterDirection.Input;

                        DbParameter p_operacion_tarjeta = cmdTransaccionFactory.CreateParameter();
                        p_operacion_tarjeta.ParameterName = "p_operacion_tarjeta";
                        p_operacion_tarjeta.Value = DBNull.Value;
                        p_operacion_tarjeta.DbType = DbType.String;
                        p_operacion_tarjeta.Direction = ParameterDirection.Input;

                        DbParameter p_num_tran_tarjeta = cmdTransaccionFactory.CreateParameter();
                        p_num_tran_tarjeta.ParameterName = "p_num_tran_tarjeta";
                        p_num_tran_tarjeta.Value = DBNull.Value;
                        p_num_tran_tarjeta.DbType = DbType.Int64;
                        p_num_tran_tarjeta.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_idavance);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_prim_pago);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_desembolso);
                        cmdTransaccionFactory.Parameters.Add(p_Usuario);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Ope);
                        cmdTransaccionFactory.Parameters.Add(p_ip);
                        cmdTransaccionFactory.Parameters.Add(p_valor_desembolsado);
                        cmdTransaccionFactory.Parameters.Add(p_plazo);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tran);
                        cmdTransaccionFactory.Parameters.Add(p_operacion_tarjeta);
                        cmdTransaccionFactory.Parameters.Add(p_num_tran_tarjeta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_AVANCETARJETA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (p_Cod_Ope.Value != null)
                            pCredito.cod_ope = Convert.ToInt64(p_Cod_Ope.Value);
                        else
                            pCredito.cod_ope = 0;

                        // pMontoDesembolso = Convert.ToDecimal(p_valor_desembolsado.Value);
                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pCredito, "Credito", pUsuario, Accion.Modificar.ToString());

                        dbConnectionFactory.CerrarConexion(connection);

                        return pCredito.cod_ope;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ModificarCredito", ex);
                        return 0;
                    }
                }
            }
        }

        public Int64 CrearCreditoAvanceTarjeta(String numero_radicacion, Int64? codcliente, Int64 codoperacion, DateTime fechaavance, decimal valoravance, Int64 plazo, Int32? tipo_tran, string poperacion, Int64? pnum_tran_tarjeta, Usuario pUsuario)
        {
            Int64 idavance = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.Value = numero_radicacion;
                        p_numero_radicacion.Direction = ParameterDirection.Input;

                        DbParameter p_idavance = cmdTransaccionFactory.CreateParameter();
                        p_idavance.ParameterName = "p_idavance";
                        p_idavance.Value = DBNull.Value;
                        p_idavance.DbType = DbType.Int64;
                        p_idavance.Direction = ParameterDirection.InputOutput;

                        DbParameter p_fecha_prim_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_prim_pago.ParameterName = "p_fecha_prim_pago";
                        if (fechaavance == null)
                            p_fecha_prim_pago.Value = DBNull.Value;
                        else
                            p_fecha_prim_pago.Value = fechaavance;
                        p_fecha_prim_pago.DbType = DbType.Date;
                        p_fecha_prim_pago.Direction = ParameterDirection.Input;

                        DbParameter p_fecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        p_fecha_desembolso.ParameterName = "p_fecha_desembolso";
                        p_fecha_desembolso.Value = fechaavance;
                        p_fecha_desembolso.DbType = DbType.Date;
                        p_fecha_desembolso.Direction = ParameterDirection.Input;

                        DbParameter p_Usuario = cmdTransaccionFactory.CreateParameter();
                        p_Usuario.ParameterName = "p_Usuario";
                        p_Usuario.Value = pUsuario.codusuario;
                        p_Usuario.Direction = ParameterDirection.Input;
                        p_Usuario.Size = 50;

                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        if (pUsuario.IP == "0" || pUsuario.IP == "" || pUsuario.IP == null)
                            p_ip.Value = 0;
                        else
                            p_ip.Value = pUsuario.IP;
                        p_ip.DbType = DbType.String;
                        p_ip.Direction = ParameterDirection.Input;

                        DbParameter p_valor_desembolsado = cmdTransaccionFactory.CreateParameter();
                        p_valor_desembolsado.ParameterName = "p_valor_desembolsado";
                        p_valor_desembolsado.Value = valoravance;
                        p_valor_desembolsado.Direction = ParameterDirection.Input;

                        DbParameter p_Cod_Ope = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Ope.ParameterName = "p_cod_ope";
                        p_Cod_Ope.Value = codoperacion;
                        p_Cod_Ope.Direction = ParameterDirection.InputOutput;

                        DbParameter p_plazo = cmdTransaccionFactory.CreateParameter();
                        p_plazo.ParameterName = "p_plazo";
                        p_plazo.Value = plazo;
                        p_plazo.Direction = ParameterDirection.Input;

                        DbParameter p_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tran.ParameterName = "p_tipo_tran";
                        if (tipo_tran == null)
                            p_tipo_tran.Value = DBNull.Value;
                        else
                            p_tipo_tran.Value = tipo_tran;
                        p_tipo_tran.Direction = ParameterDirection.Input;

                        DbParameter p_operacion_tarjeta = cmdTransaccionFactory.CreateParameter();
                        p_operacion_tarjeta.ParameterName = "p_operacion_tarjeta";
                        if (poperacion == "" || poperacion == null) p_operacion_tarjeta.Value = DBNull.Value; else p_operacion_tarjeta.Value = poperacion;
                        p_operacion_tarjeta.DbType = DbType.String;
                        p_operacion_tarjeta.Direction = ParameterDirection.Input;

                        DbParameter p_num_tran_tarjeta = cmdTransaccionFactory.CreateParameter();
                        p_num_tran_tarjeta.ParameterName = "p_num_tran_tarjeta";
                        if (pnum_tran_tarjeta == null || pnum_tran_tarjeta == null) p_num_tran_tarjeta.Value = DBNull.Value; else p_num_tran_tarjeta.Value = pnum_tran_tarjeta;
                        p_num_tran_tarjeta.DbType = DbType.Int64;
                        p_num_tran_tarjeta.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_idavance);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_prim_pago);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_desembolso);
                        cmdTransaccionFactory.Parameters.Add(p_Usuario);
                        cmdTransaccionFactory.Parameters.Add(p_ip);
                        cmdTransaccionFactory.Parameters.Add(p_valor_desembolsado);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Ope);
                        cmdTransaccionFactory.Parameters.Add(p_plazo);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tran);
                        cmdTransaccionFactory.Parameters.Add(p_operacion_tarjeta);
                        cmdTransaccionFactory.Parameters.Add(p_num_tran_tarjeta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_AVANCETARJETA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        if (p_idavance.Value != null)
                            idavance = Convert.ToInt64(p_idavance.Value);

                        return idavance;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaData", "CrearCreditoAvanceTarjeta", ex);
                        return idavance;
                    }
                }
            }
        }

        public bool ActualizarMovimiento(Int64 pnumTran, string pfechaCorte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnum_tran = cmdTransaccionFactory.CreateParameter();
                        pnum_tran.ParameterName = "p_num_tran";
                        pnum_tran.Value = pnumTran;
                        pnum_tran.Direction = ParameterDirection.Input;
                        pnum_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_tran);

                        DbParameter pfecha_corte = cmdTransaccionFactory.CreateParameter();
                        pfecha_corte.ParameterName = "p_num_tran";
                        pfecha_corte.Value = pfechaCorte;
                        pfecha_corte.Direction = ParameterDirection.Input;
                        pfecha_corte.DbType = DbType.AnsiStringFixedLength;
                        pfecha_corte.Size = 20;
                        cmdTransaccionFactory.Parameters.Add(pfecha_corte);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_TRANTARJE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public bool ComprobanteValorBanco(Int64 pNumComp, Int64 pTipoComp, ref decimal pValor, ref DateTime? pFecha, Usuario vUsuario)
        {
            DbDataReader resultado;
            pValor = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string fecha = "";
                        Configuracion conf = new Configuracion();
                        if (pFecha != null)
                        {
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                fecha = " TO_DATE('" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                            else
                                fecha = " '" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "'";
                        }
                        string sql = @"SELECT E.FECHA, SUM(CASE D.TIPO WHEN 'C' THEN D.VALOR ELSE -D.VALOR END) AS VALOR FROM D_COMPROBANTE D JOIN E_COMPROBANTE E ON D.NUM_COMP = E.NUM_COMP AND D.TIPO_COMP = E.TIPO_COMP 
                                        WHERE D.COD_CUENTA IN (Select cod_cuenta From proceso_contable Where tipo_ope = 124) "
                                    + (fecha.Trim() == "" ? "" : " AND TRUNC(E.FECHA) = " + fecha)
                                    + (pNumComp != 0 ? " AND D.NUM_COMP = " + pNumComp.ToString() : "") + (pTipoComp != 0 ? " AND D.TIPO_COMP = " + pTipoComp.ToString() : "") 
                                    + "  GROUP BY E.FECHA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) pFecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["VALOR"] != DBNull.Value) pValor = Convert.ToDecimal(resultado["VALOR"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch
                    {
                        if (connection.State == ConnectionState.Open)
                            dbConnectionFactory.CerrarConexion(connection);
                        return false;
                    }
                }
            }
        }

        public List<Cuenta> ListarCuentaAsignacion(Cuenta pCuenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Cuenta> lstCuenta = new List<Cuenta>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";

                        sql = @"SELECT V.* FROM V_TARDEB_CUENTA V  " + ObtenerFiltro(pCuenta, "V.");

                        if (ObtenerFiltro(pCuenta, "V.") != "")
                        {
                            sql += "AND";
                        }
                        else
                        {
                            sql += "WHERE";
                        }

                        sql += @" V.NUMERO_CUENTA || '*' || V.TIPO_CUENTA  not in( select T.NUMERO_CUENTA || '*' || CASE T.TIPO_CUENTA WHEN 1 THEN 'A' WHEN 2 THEN 'C' END AS TIPO_CUENTA from tarjeta T
                            UNION
                            select CR.NUMERO_CUENTA || '*' ||  CR.TIPO_CUENTA  from CUENTAS_ASIGNAR CR)                                
                            ORDER BY V.identificacion";



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Cuenta entidad = new Cuenta();
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipocuenta = Convert.ToString(resultado["TIPO_CUENTA"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.nrocuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["SALDODISPONIBLE"] != DBNull.Value) entidad.saldodisponible = Convert.ToDecimal(resultado["SALDODISPONIBLE"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldototal = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["FECHASALDO"] != DBNull.Value) entidad.fechasaldo = Convert.ToDateTime(resultado["FECHASALDO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fechaapertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            lstCuenta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaData", "ListarCuentaAsignacion", ex);
                        return null;
                    }
                }
            }
        }

        public Int64 AsignarCuenta(Cuenta pCuenta, Usuario pUsuario)
        {
            Int64 Consecutivo = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter PCONSECUTIVO = cmdTransaccionFactory.CreateParameter();
                        PCONSECUTIVO.ParameterName = "PCONSECUTIVO";
                        PCONSECUTIVO.Value = Consecutivo;
                        PCONSECUTIVO.Direction = ParameterDirection.Output;
                        PCONSECUTIVO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCONSECUTIVO);

                        DbParameter PCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        PCOD_PERSONA.ParameterName = "PCOD_PERSONA";
                        PCOD_PERSONA.Value = pCuenta.cod_persona;
                        PCOD_PERSONA.Direction = ParameterDirection.Input;
                        PCOD_PERSONA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PCOD_PERSONA);

                        DbParameter PTIPO_CUENTA = cmdTransaccionFactory.CreateParameter();
                        PTIPO_CUENTA.ParameterName = "PTIPO_CUENTA";
                        PTIPO_CUENTA.Value = pCuenta.tipocuenta;
                        PTIPO_CUENTA.Direction = ParameterDirection.Input;
                        PTIPO_CUENTA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PTIPO_CUENTA);

                        DbParameter PNUMERO_CUENTA = cmdTransaccionFactory.CreateParameter();
                        PNUMERO_CUENTA.ParameterName = "PNUMERO_CUENTA";
                        PNUMERO_CUENTA.Value = pCuenta.numero_cuenta;
                        PNUMERO_CUENTA.Direction = ParameterDirection.Input;
                        PNUMERO_CUENTA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PNUMERO_CUENTA);

                        DbParameter PUSUARIO_ASIGNACION = cmdTransaccionFactory.CreateParameter();
                        PUSUARIO_ASIGNACION.ParameterName = "PUSUARIO_ASIGNACION";
                        PUSUARIO_ASIGNACION.Value = pUsuario.nombre;
                        PUSUARIO_ASIGNACION.Direction = ParameterDirection.Input;
                        PUSUARIO_ASIGNACION.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PUSUARIO_ASIGNACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ENP_ASIGNAR_CUENTA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (PCONSECUTIVO.Value != null)
                            Consecutivo = Convert.ToInt64(PCONSECUTIVO.Value);

                        return Consecutivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TarjetaData", "AsignarCuenta", ex);
                        return Consecutivo;
                    }
                }
            }
        }

        public void ActualizarMovimiento(string pConvenio, Movimiento pMovimiento, Usuario pUsuario, ref string Error)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_num_tran_tarjeta = cmdTransaccionFactory.CreateParameter();
                        p_num_tran_tarjeta.ParameterName = "p_num_tran_tarjeta";
                        if (pMovimiento.num_tran_verifica == null) p_num_tran_tarjeta.Value = DBNull.Value; else p_num_tran_tarjeta.Value = pMovimiento.num_tran_verifica;
                        p_num_tran_tarjeta.DbType = DbType.Int64;
                        p_num_tran_tarjeta.Direction = ParameterDirection.Input;

                        DbParameter p_tipotransaccion = cmdTransaccionFactory.CreateParameter();
                        p_tipotransaccion.ParameterName = "p_tipotransaccion";
                        p_tipotransaccion.Value = pMovimiento.tipotransaccion;
                        p_tipotransaccion.DbType = DbType.String;
                        p_tipotransaccion.Direction = ParameterDirection.Input;

                        DbParameter p_monto = cmdTransaccionFactory.CreateParameter();
                        p_monto.ParameterName = "p_monto";
                        p_monto.Value = pMovimiento.monto;
                        p_monto.DbType = DbType.Decimal;
                        p_monto.Direction = ParameterDirection.Input;

                        DbParameter p_comision = cmdTransaccionFactory.CreateParameter();
                        p_comision.ParameterName = "p_comision";
                        p_comision.Value = pMovimiento.comision;
                        p_comision.DbType = DbType.Decimal;
                        p_comision.Direction = ParameterDirection.Input;

                        DbParameter p_fecha_corte = cmdTransaccionFactory.CreateParameter();
                        p_fecha_corte.ParameterName = "p_fecha_corte";
                        p_fecha_corte.Value = pMovimiento.fecha_corte;
                        p_fecha_corte.DbType = DbType.AnsiStringFixedLength;
                        p_fecha_corte.Size = 20;
                        p_fecha_corte.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_num_tran_tarjeta);
                        cmdTransaccionFactory.Parameters.Add(p_tipotransaccion);
                        cmdTransaccionFactory.Parameters.Add(p_monto);
                        cmdTransaccionFactory.Parameters.Add(p_comision);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_corte);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_ACTMOVIMIENTO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        BOExcepcion.Throw("", "", ex);
                    }

                    if (connection.State == ConnectionState.Open)
                        dbConnectionFactory.CerrarConexion(connection);

                }
            }
        }

        /// <summary>
        /// Consultar listado de transacciones pendientes de actualizar en ENPACTO en Switch Transaccional
        /// </summary>
        /// <param name="pCuenta"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<TransaccionFinancial> ListarTransaccionesPendientesAplicarEnpacto(int ptipo_convenio, string pConvenio, Usuario pUsuario)
        {
            string sql = "";
            DbDataReader resultado;
            List<TransaccionFinancial> lstCuenta = new List<TransaccionFinancial>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        sql = @"Select p.tipo_identificacion, p.identificacion, NombrePersona(p.tipo_persona, p.primer_nombre, p.segundo_nombre, p.primer_apellido, p.segundo_apellido, p.razon_social) As nombre,
                                    '2' As tipoproducto, To_Char(t.numero_radicacion) As numeroproducto, r.tipo_mov, Sum(t.valor) as valor, t.cod_ope, Nvl(o.observacion, tp.descripcion) As observacion, o.fecha_oper
                                    From tran_cred t
                                    Join tipo_tran r On r.tipo_tran = t.tipo_tran
                                    Join operacion o On o.cod_ope = t.cod_ope
                                    Join tipo_ope tp On tp.tipo_ope = o.tipo_ope
                                    Join credito c On c.numero_radicacion = t.numero_radicacion
                                    Join persona p On p.cod_persona = c.cod_deudor
                                    Where o.cod_ope = t.cod_ope And o.tipo_ope Not In (124)
                                    And t.numero_radicacion In (Select x.numero_cuenta From tarjeta x Where estado = 1)
                                    And t.cod_ope > Nvl((Select cod_ope_ultima From tarjeta_convenio Where codigo_bin = '" + pConvenio + @"'), 999999999)
                                    And Nvl((Select tipo_aplicacion From tarjeta_convenio Where codigo_bin = '" + pConvenio + @"'), 0) = 1
                                    Group by p.tipo_identificacion, p.identificacion, NombrePersona(p.tipo_persona, p.primer_nombre, p.segundo_nombre, p.primer_apellido, p.segundo_apellido, p.razon_social),
                                    t.numero_radicacion, r.tipo_mov, t.cod_ope, Nvl(o.observacion, tp.descripcion), o.fecha_oper
                                UNION ALL
                                    Select p.tipo_identificacion, p.identificacion, NombrePersona(p.tipo_persona, p.primer_nombre, p.segundo_nombre, p.primer_apellido, p.segundo_apellido, p.razon_social) As nombre,
                                    '2' As tipoproducto, t.numero_cuenta As numeroproducto, r.tipo_mov, Sum(t.valor) as valor, t.cod_ope, Nvl(o.observacion, tp.descripcion) As observacion, o.fecha_oper
                                    From tran_ahorro t
                                    Join tipo_tran r On r.tipo_tran = t.tipo_tran
                                    Join operacion o On o.cod_ope = t.cod_ope
                                    Join tipo_ope tp On tp.tipo_ope = o.tipo_ope
                                    Join ahorro_vista c On c.numero_cuenta = t.numero_cuenta
                                    Join persona p On p.cod_persona = c.cod_persona
                                    Where o.cod_ope = t.cod_ope And o.tipo_ope Not In (124)
                                    And t.numero_cuenta In (Select x.numero_cuenta From tarjeta x Where estado = 1)
                                    And t.cod_ope > Nvl((Select cod_ope_ultima From tarjeta_convenio Where codigo_bin = '" + pConvenio + @"'), 999999999)
                                    And Nvl((Select tipo_aplicacion From tarjeta_convenio Where codigo_bin = '" + pConvenio + @"'), 0) = 1
                                    Group by p.tipo_identificacion, p.identificacion, NombrePersona(p.tipo_persona, p.primer_nombre, p.segundo_nombre, p.primer_apellido, p.segundo_apellido, p.razon_social),
                                    t.numero_cuenta, r.tipo_mov, t.cod_ope, Nvl(o.observacion, tp.descripcion), o.fecha_oper
                                    Order by 8, 9";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TransaccionFinancial entidad = new TransaccionFinancial();
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.tipoIdentificacion = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["tipoproducto"] != DBNull.Value) entidad.tipoProducto = Convert.ToString(resultado["tipoproducto"]);
                            if (resultado["numeroproducto"] != DBNull.Value) entidad.numeroproducto = Convert.ToString(resultado["numeroproducto"]);
                            if (resultado["tipo_mov"] != DBNull.Value) entidad.tipoMov = Convert.ToString(resultado["tipo_mov"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["cod_ope"] != DBNull.Value) entidad.codOpe = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["observacion"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["observacion"]);
                            if (resultado["fecha_oper"] != DBNull.Value) entidad.fecha_oper = Convert.ToDateTime(resultado["fecha_oper"]);
                            lstCuenta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaData", "ListarTransaccionesPendientesAplicarEnpacto " + sql, ex);
                        return null;
                    }
                }
            }
        }

        public bool CrearControlOperacion(string pConvenio, TransaccionFinancial pMovimiento, ref string pError, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconvenio = cmdTransaccionFactory.CreateParameter();
                        pconvenio.ParameterName = "p_convenio";
                        if (pMovimiento.numero_tarjeta == null)
                            pconvenio.Value = DBNull.Value;
                        else
                            pconvenio.Value = pConvenio;
                        pconvenio.Direction = ParameterDirection.Input;
                        pconvenio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pconvenio);
                        DbParameter pfecha_transaccion = cmdTransaccionFactory.CreateParameter();

                        pfecha_transaccion.ParameterName = "p_fecha";
                        if (pMovimiento.fecha_oper == null)
                            pfecha_transaccion.Value = DBNull.Value;
                        else
                            pfecha_transaccion.Value = pMovimiento.fecha_oper;
                        pfecha_transaccion.Direction = ParameterDirection.Input;
                        pfecha_transaccion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_transaccion);

                        DbParameter ptarjeta = cmdTransaccionFactory.CreateParameter();
                        ptarjeta.ParameterName = "p_tarjeta";
                        if (pMovimiento.numero_tarjeta == null)
                            ptarjeta.Value = DBNull.Value;
                        else
                            ptarjeta.Value = pMovimiento.numero_tarjeta;
                        ptarjeta.Direction = ParameterDirection.Input;
                        ptarjeta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptarjeta);

                        DbParameter pnrocuenta = cmdTransaccionFactory.CreateParameter();
                        pnrocuenta.ParameterName = "p_nrocuenta";
                        if (pMovimiento.numeroproducto == null)
                            pnrocuenta.Value = DBNull.Value;
                        else
                            pnrocuenta.Value = pMovimiento.numeroproducto;
                        pnrocuenta.Direction = ParameterDirection.Input;
                        pnrocuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnrocuenta);

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        if (pMovimiento.codOpe == null)
                            p_cod_ope.Value = DBNull.Value;
                        else
                            p_cod_ope.Value = pMovimiento.codOpe;
                        p_cod_ope.Direction = ParameterDirection.Input;
                        p_cod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_CONTROLMOVTAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return false;
                    }
                }
            }
        }

        public int TiposAplicacionEnpacto(int ptipo_convenio, string pConvenio, ref string pError, Usuario pUsuario)
        {
            int tipoAplicacion = 0;
            DbDataReader resultado;
            List<TransaccionFinancial> lstCuenta = new List<TransaccionFinancial>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select tipo_aplicacion From tarjeta_convenio Where codigo_bin = '" + pConvenio + @"' And (tipo_convenio = " + ptipo_convenio + (ptipo_convenio == 0 ? " Or tipo_convenio Is Null) " : ") ");

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["tipo_aplicacion"] != DBNull.Value) tipoAplicacion = Convert.ToInt32(resultado["tipo_aplicacion"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return tipoAplicacion;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return 0;
                    }
                }
            }
        }

        public List<Movimiento> ListaTransaccionesSinConciliar(string pConvenio, Usuario pUsuario)
        {
            string sql = "";
            DbDataReader resultado;
            List<Movimiento> lstCuenta = new List<Movimiento>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        sql = @"Select o.tipo_ope, t.* From tran_tarjeta t Join operacion o On t.cod_ope = o.cod_ope 
                                    Where (t.conciliado Is Null Or t.conciliado != 1) And o.tipo_ope = 124 Order by t.num_tran";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Movimiento entidad = new Movimiento();
                            if (resultado["num_tran"] != DBNull.Value) entidad.num_tran_tarjeta = Convert.ToInt64(resultado["num_tran"]);
                            if (resultado["fecha"] != DBNull.Value) entidad.fecha = Convert.ToString(resultado["fecha"]);
                            if (resultado["hora"] != DBNull.Value) entidad.hora = Convert.ToString(resultado["hora"]);
                            if (resultado["documento"] != DBNull.Value) entidad.documento = Convert.ToString(resultado["documento"]);
                            if (resultado["nrocuenta"] != DBNull.Value) entidad.nrocuenta = Convert.ToString(resultado["nrocuenta"]);
                            if (resultado["tarjeta"] != DBNull.Value) entidad.tarjeta = Convert.ToString(resultado["tarjeta"]);
                            if (resultado["tipotransaccion"] != DBNull.Value) entidad.tipotransaccion = Convert.ToString(resultado["tipotransaccion"]);
                            if (resultado["monto"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["monto"]);
                            if (resultado["comision"] != DBNull.Value) entidad.comision = Convert.ToDecimal(resultado["comision"]);
                            if (resultado["lugar"] != DBNull.Value) entidad.lugar = Convert.ToString(resultado["lugar"]);
                            if (resultado["operacion"] != DBNull.Value) entidad.operacion = Convert.ToString(resultado["operacion"]);
                            if (resultado["red"] != DBNull.Value) entidad.red = Convert.ToString(resultado["red"]);
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["cod_ope"]);
                            lstCuenta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaData", "ListaTransaccionesSinConciliar " + sql, ex);
                        return null;
                    }
                }
            }
        }

        public List<CuentaCoopcentral> ListarCuentaCoopcentral(Cuenta pCuenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CuentaCoopcentral> lstCuenta = new List<CuentaCoopcentral>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = @"SELECT * FROM V_TARDEB_CUENTA_VISIONAMOS " + ObtenerFiltro(pCuenta) + " ORDER BY identificacion  ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentaCoopcentral entidad = new CuentaCoopcentral();
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["DIRECCION_CASA"] != DBNull.Value) entidad.direccion_casa = Convert.ToString(resultado["DIRECCION_CASA"]);
                            if (resultado["DIRECCION_TRABAJO"] != DBNull.Value) entidad.direccion_trabajo = Convert.ToString(resultado["DIRECCION_TRABAJO"]);
                            if (resultado["TELEFONO_CASA"] != DBNull.Value) entidad.telefono_casa = Convert.ToString(resultado["TELEFONO_CASA"]);
                            if (resultado["TELEFONO_TRABAJO"] != DBNull.Value) entidad.telefono_trabajo = Convert.ToString(resultado["TELEFONO_TRABAJO"]);
                            if (resultado["TELEFONO_MOVIL"] != DBNull.Value) entidad.telefono_movil = Convert.ToString(resultado["TELEFONO_MOVIL"]);
                            if (resultado["FECHA_NACIMIENTO"] != DBNull.Value) entidad.fecha_nacimiento = Convert.ToDateTime(resultado["FECHA_NACIMIENTO"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            if (resultado["CORREO_ELECTRONICO"] != DBNull.Value) entidad.correo_electronico = Convert.ToString(resultado["CORREO_ELECTRONICO"]);
                            if (resultado["PAIS_RESIDENCIA"] != DBNull.Value) entidad.pais_residencia = Convert.ToString(resultado["PAIS_RESIDENCIA"]);
                            if (resultado["DPTO_RESIDENCIA"] != DBNull.Value) entidad.dpto_residencia = Convert.ToString(resultado["DPTO_RESIDENCIA"]);
                            if (resultado["CIUDAD_RESIDENCIA"] != DBNull.Value) entidad.ciudad_residencia = Convert.ToString(resultado["CIUDAD_RESIDENCIA"]);
                            if (resultado["PAIS_NACIMIENTO"] != DBNull.Value) entidad.pais_nacimiento = Convert.ToString(resultado["PAIS_NACIMIENTO"]);
                            if (resultado["DPTO_NACIMIENTO"] != DBNull.Value) entidad.dpto_nacimiento = Convert.ToString(resultado["DPTO_NACIMIENTO"]);
                            if (resultado["CIUDAD_NACIMIENTO"] != DBNull.Value) entidad.ciudad_nacimiento = Convert.ToString(resultado["CIUDAD_NACIMIENTO"]);
                            if (resultado["CUENTA"] != DBNull.Value) entidad.cuenta = Convert.ToString(resultado["CUENTA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToString(resultado["TIPO_CUENTA"]);
                            if (resultado["SALDODISPONIBLE"] != DBNull.Value) entidad.saldodisponible = Convert.ToInt32(resultado["SALDODISPONIBLE"]);
                            if (resultado["SALDOTOTAL"] != DBNull.Value) entidad.saldototal = Convert.ToInt32(resultado["SALDOTOTAL"]);
                            if (resultado["FECHA_ACTUALIZACION"] != DBNull.Value) entidad.fecha_actualizacion = Convert.ToDateTime(resultado["FECHA_ACTUALIZACION"]);
                            if (resultado["FECHA_EXPEDICION"] != DBNull.Value) entidad.fecha_expedicion = Convert.ToDateTime(resultado["FECHA_EXPEDICION"]);
                            if (resultado["PAGOMINIMO"] != DBNull.Value) entidad.pagominimo = Convert.ToInt32(resultado["PAGOMINIMO"]);
                            if (resultado["PAGOTOTAL"] != DBNull.Value) entidad.pagototal = Convert.ToInt32(resultado["PAGOTOTAL"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            lstCuenta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaData", "ListarCuentaCoopcentral", ex);
                        return null;
                    }
                }
            }
        }

        public string HomologarCuentas(string pTarjeta, string pCuenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            string _cuentaHomologa = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT TARJETA.* FROM TARJETA WHERE NUMERO_TARJETA = '" + pTarjeta + "' AND NUMERO_CUENTA =  '" + pCuenta + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CUENTA_HOMOLOGA"] != DBNull.Value) _cuentaHomologa = Convert.ToString(resultado["CUENTA_HOMOLOGA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return _cuentaHomologa;
                    }
                    catch
                    {
                        if (connection.State == ConnectionState.Open)
                            dbConnectionFactory.CerrarConexion(connection);
                        return _cuentaHomologa;
                    }
                }
            }
        }

        public List<Movimiento> ListarTipoTran(Int64 pNumTranTarjeta, Usuario pUsuario)
        {
            string sql = "";
            DbDataReader resultado;
            List<Movimiento> lstCuenta = new List<Movimiento>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        sql = @"Select t.tipo_tran, o.descripcion From tran_ahorro t Join tipo_tran o On t.tipo_tran = o.tipo_tran 
                                    Where t.num_tran_tarjeta = " + pNumTranTarjeta.ToString() + @"
                                Union All
                                Select t.tipo_tran, o.descripcion From tran_cred t Join tipo_tran o On t.tipo_tran = o.tipo_tran 
                                    Where t.num_tran_tarjeta = " + pNumTranTarjeta.ToString() + @"    
                                Order by 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Movimiento entidad = new Movimiento();
                            if (resultado["tipo_tran"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["tipo_tran"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            lstCuenta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaData", "ListarTipoTran " + sql, ex);
                        return null;
                    }
                }
            }
        }

        public decimal ConsultarValor(Int64 pNumTranTarjeta, Int32 pTipoTran, Usuario pUsuario)
        {
            string sql = "";
            DbDataReader resultado;
            decimal _valor = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        sql = @"Select t.valor From tran_ahorro t Join tipo_tran o On t.tipo_tran = o.tipo_tran 
                                    Where t.num_tran_tarjeta = " + pNumTranTarjeta.ToString() + " And t.tipo_tran = " + pTipoTran.ToString() + @"
                                Union All
                                Select t.valor From tran_cred t Join tipo_tran o On t.tipo_tran = o.tipo_tran 
                                    Where t.num_tran_tarjeta = " + pNumTranTarjeta.ToString() + " And t.tipo_tran = " + pTipoTran.ToString() + @"    
                                Order by 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) _valor = Convert.ToInt32(resultado["valor"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return _valor;
                    }
                    catch (Exception ex)
                    {
                        return _valor;
                    }
                }
            }
        }

        public void AjustarMovimiento(string pConvenio, string pTipocuenta, Int64 pNumTranTarjeta, Int32 pTipoTran, decimal pValor, Usuario pUsuario, ref string Error)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_num_tran_tarjeta = cmdTransaccionFactory.CreateParameter();
                        p_num_tran_tarjeta.ParameterName = "p_num_tran_tarjeta";
                        if (pNumTranTarjeta == Int64.MinValue) p_num_tran_tarjeta.Value = DBNull.Value; else p_num_tran_tarjeta.Value = pNumTranTarjeta;
                        p_num_tran_tarjeta.DbType = DbType.Int64;
                        p_num_tran_tarjeta.Direction = ParameterDirection.Input;

                        DbParameter p_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tran.ParameterName = "p_tipo_tran";
                        p_tipo_tran.Value = pTipoTran;
                        p_tipo_tran.DbType = DbType.String;
                        p_tipo_tran.Direction = ParameterDirection.Input;

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.Value = pValor;
                        p_valor.DbType = DbType.Decimal;
                        p_valor.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_num_tran_tarjeta);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tran);
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (pTipocuenta == "C" || pTipocuenta == "2")
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_AJUSTAMOVTARJETA";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_AJUSTAMOVTARJETA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        BOExcepcion.Throw("", "", ex);
                    }

                    if (connection.State == ConnectionState.Open)
                        dbConnectionFactory.CerrarConexion(connection);

                }
            }
        }

        public Int64 VerificarSiGeneroTransacciones(Int64 pCodOpe, Usuario vUsuario)
        {
            DbDataReader resultado;
            Int64 cantidadCre = 0, cantidadAho = 0, cantidadCxC = 0, cantidadtCxC = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = @"SELECT COUNT(*) AS NUMERO FROM TRAN_CRED WHERE COD_OPE = " + pCodOpe.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        // Determinar cantidad de transacciones de crèditos
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO"] != DBNull.Value) cantidadCre = Convert.ToInt64(resultado["NUMERO"]);
                        }
                        // Determinar cantidad de transacciones de ahorros
                        sql = @"SELECT COUNT(*) AS NUMERO FROM TRAN_AHORRO WHERE COD_OPE = " + pCodOpe.ToString();
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO"] != DBNull.Value) cantidadAho = Convert.ToInt64(resultado["NUMERO"]);
                        }
                        // Determinar cantidad de transacciones de cuenta por cobrar ahorros
                        sql = @"SELECT COUNT(*) AS NUMERO FROM CUENTAPORCOBRAR_AHORRO WHERE COD_OPE = " + pCodOpe.ToString();
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO"] != DBNull.Value) cantidadCxC = Convert.ToInt64(resultado["NUMERO"]);
                        }
                        // Determinar cantidad de transacciones de cuenta por cobrar ahorros
                        sql = @"SELECT COUNT(*) AS NUMERO FROM TRAN_CTA_COBRAR WHERE COD_OPE = " + pCodOpe.ToString();
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO"] != DBNull.Value) cantidadtCxC = Convert.ToInt64(resultado["NUMERO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return cantidadCre + cantidadAho + cantidadCxC + cantidadtCxC;
                    }
                    catch
                    {
                        if (connection.State == ConnectionState.Open)
                            dbConnectionFactory.CerrarConexion(connection);
                        return cantidadCre + cantidadAho + cantidadCxC;
                    }
                }
            }
        }

        public List<TransaccionFinancial> ListarTransaccionesFinancial(string pConvenio, DateTime pFecha, Usuario pUsuario)
        {
            string sql = "";
            DbDataReader resultado;
            List<TransaccionFinancial> lstCuenta = new List<TransaccionFinancial>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        sql = @"Select p.cod_persona, p.tipo_identificacion, p.identificacion, NombrePersona(p.tipo_persona, p.primer_nombre, p.segundo_nombre, p.primer_apellido, p.segundo_apellido, p.razon_social) As nombre,
                                    '2' As tipoproducto, To_Char(t.numero_radicacion) As numeroproducto, r.tipo_mov, Sum(t.valor) as valor, t.cod_ope, Nvl(o.observacion, tp.descripcion) As observacion, o.fecha_oper,
                                    (Select Max(x.numero_tarjeta) From tarjeta x Where x.numero_cuenta = To_Char(t.numero_radicacion) And x.estado = 1) As numero_tarjeta
                                    From tran_cred t
                                    Join tipo_tran r On r.tipo_tran = t.tipo_tran
                                    Join operacion o On o.cod_ope = t.cod_ope
                                    Join tipo_ope tp On tp.tipo_ope = o.tipo_ope
                                    Join credito c On c.numero_radicacion = t.numero_radicacion
                                    Join persona p On p.cod_persona = c.cod_deudor
                                    Where o.cod_ope = t.cod_ope And o.tipo_ope Not In (124)
                                    And t.numero_radicacion In (Select x.numero_cuenta From tarjeta x Where estado = 1) And Trunc(o.fecha_oper) = To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " + @" 
                                    Group by p.cod_persona, p.tipo_identificacion, p.identificacion, NombrePersona(p.tipo_persona, p.primer_nombre, p.segundo_nombre, p.primer_apellido, p.segundo_apellido, p.razon_social),
                                    t.numero_radicacion, r.tipo_mov, t.cod_ope, Nvl(o.observacion, tp.descripcion), o.fecha_oper
                                UNION ALL
                                    Select p.cod_persona, p.tipo_identificacion, p.identificacion, NombrePersona(p.tipo_persona, p.primer_nombre, p.segundo_nombre, p.primer_apellido, p.segundo_apellido, p.razon_social) As nombre,
                                    '2' As tipoproducto, t.numero_cuenta As numeroproducto, r.tipo_mov, Sum(t.valor) as valor, t.cod_ope, Nvl(o.observacion, tp.descripcion) As observacion, o.fecha_oper,
                                    (Select Max(x.numero_tarjeta) From tarjeta x Where x.numero_cuenta = t.numero_cuenta And x.estado = 1) As numero_tarjeta
                                    From tran_ahorro t
                                    Join tipo_tran r On r.tipo_tran = t.tipo_tran
                                    Join operacion o On o.cod_ope = t.cod_ope
                                    Join tipo_ope tp On tp.tipo_ope = o.tipo_ope
                                    Join ahorro_vista c On c.numero_cuenta = t.numero_cuenta
                                    Join persona p On p.cod_persona = c.cod_persona
                                    Where o.cod_ope = t.cod_ope And o.tipo_ope Not In (124)
                                    And t.numero_cuenta In (Select x.numero_cuenta From tarjeta x Where estado = 1) And Trunc(o.fecha_oper) = To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " + @" 
                                    Group by p.cod_persona, p.tipo_identificacion, p.identificacion, NombrePersona(p.tipo_persona, p.primer_nombre, p.segundo_nombre, p.primer_apellido, p.segundo_apellido, p.razon_social),
                                    t.numero_cuenta, r.tipo_mov, t.cod_ope, Nvl(o.observacion, tp.descripcion), o.fecha_oper
                                    Order by 8, 9";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TransaccionFinancial entidad = new TransaccionFinancial();
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.tipoIdentificacion = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["tipoproducto"] != DBNull.Value) entidad.tipoProducto = Convert.ToString(resultado["tipoproducto"]);
                            if (resultado["numeroproducto"] != DBNull.Value) entidad.numeroproducto = Convert.ToString(resultado["numeroproducto"]);
                            if (resultado["tipo_mov"] != DBNull.Value) entidad.tipoMov = Convert.ToString(resultado["tipo_mov"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["cod_ope"] != DBNull.Value) entidad.codOpe = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["observacion"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["observacion"]);
                            if (resultado["fecha_oper"] != DBNull.Value) entidad.fecha_oper = Convert.ToDateTime(resultado["fecha_oper"]);
                            if (resultado["numero_tarjeta"] != DBNull.Value) entidad.numero_tarjeta = Convert.ToString(resultado["numero_tarjeta"]);
                            lstCuenta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaData", "ListarTransaccionesPendientesAplicarEnpacto " + sql, ex);
                        return null;
                    }
                }
            }
        }

        public int? CrearMovimientoCoopcentral(MovimientoCoopcentral pMovimiento, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnum_tran = cmdTransaccionFactory.CreateParameter();
                        pnum_tran.ParameterName = "p_num_tran";
                        pnum_tran.Value = 0;
                        pnum_tran.Direction = ParameterDirection.InputOutput;
                        pnum_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_tran);

                        DbParameter pfecha_transaccion = cmdTransaccionFactory.CreateParameter();
                        pfecha_transaccion.ParameterName = "p_fecha";
                        if (pMovimiento.fecha_contable == null)
                            pfecha_transaccion.Value = DBNull.Value;
                        else
                            pfecha_transaccion.Value = pMovimiento.fecha_contable;
                        pfecha_transaccion.Direction = ParameterDirection.Input;
                        pfecha_transaccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pfecha_transaccion);

                        DbParameter phora_transaccion = cmdTransaccionFactory.CreateParameter();
                        phora_transaccion.ParameterName = "p_hora";
                        if (pMovimiento.hora_transaccion == null)
                            phora_transaccion.Value = DBNull.Value;
                        else
                            phora_transaccion.Value = pMovimiento.hora_transaccion;
                        phora_transaccion.Direction = ParameterDirection.Input;
                        phora_transaccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(phora_transaccion);

                        DbParameter pdocumento = cmdTransaccionFactory.CreateParameter();
                        pdocumento.ParameterName = "p_documento";
                        if (pMovimiento.secuencia == null)
                            pdocumento.Value = DBNull.Value;
                        else
                            pdocumento.Value = pMovimiento.secuencia;
                        pdocumento.Direction = ParameterDirection.Input;
                        pdocumento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdocumento);

                        DbParameter pnrocuenta = cmdTransaccionFactory.CreateParameter();
                        pnrocuenta.ParameterName = "p_nrocuenta";
                        if (pMovimiento.cuenta_origen == null)
                            pnrocuenta.Value = DBNull.Value;
                        else
                            pnrocuenta.Value = pMovimiento.cuenta_origen;
                        pnrocuenta.Direction = ParameterDirection.Input;
                        pnrocuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnrocuenta);

                        DbParameter ptarjeta = cmdTransaccionFactory.CreateParameter();
                        ptarjeta.ParameterName = "p_tarjeta";
                        if (pMovimiento.tarjeta == null)
                            ptarjeta.Value = DBNull.Value;
                        else
                            ptarjeta.Value = pMovimiento.tarjeta;
                        ptarjeta.Direction = ParameterDirection.Input;
                        ptarjeta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptarjeta);

                        DbParameter ptipo_transaccion = cmdTransaccionFactory.CreateParameter();
                        ptipo_transaccion.ParameterName = "p_tipotransaccion";
                        if (pMovimiento.transaccion == null || pMovimiento.transaccion == "")
                            ptipo_transaccion.Value = DBNull.Value;
                        else
                            ptipo_transaccion.Value = pMovimiento.transaccion;
                        ptipo_transaccion.Direction = ParameterDirection.Input;
                        ptipo_transaccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_transaccion);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pMovimiento.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pMovimiento.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pmonto = cmdTransaccionFactory.CreateParameter();
                        pmonto.ParameterName = "p_monto";
                        pmonto.Value = pMovimiento.valor;
                        pmonto.Direction = ParameterDirection.Input;
                        pmonto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmonto);

                        DbParameter pcomision = cmdTransaccionFactory.CreateParameter();
                        pcomision.ParameterName = "p_comision";
                        pcomision.Value = pMovimiento.comision_asociado;
                        pcomision.Direction = ParameterDirection.Input;
                        pcomision.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcomision);

                        DbParameter plugar = cmdTransaccionFactory.CreateParameter();
                        plugar.ParameterName = "p_lugar";
                        if (pMovimiento.ubicacion_terminal == null)
                            plugar.Value = DBNull.Value;
                        else
                            plugar.Value = pMovimiento.ubicacion_terminal;
                        plugar.Direction = ParameterDirection.Input;
                        plugar.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(plugar);

                        DbParameter poperacion = cmdTransaccionFactory.CreateParameter();
                        poperacion.ParameterName = "p_operacion";
                        if (pMovimiento.secuencia == null)
                            poperacion.Value = DBNull.Value;
                        else
                            poperacion.Value = pMovimiento.secuencia;
                        poperacion.Direction = ParameterDirection.Input;
                        poperacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(poperacion);

                        DbParameter pred = cmdTransaccionFactory.CreateParameter();
                        pred.ParameterName = "p_red";
                        if (pMovimiento.tipo_terminal == null)
                            pred.Value = DBNull.Value;
                        else
                            pred.Value = pMovimiento.tipo_terminal;
                        pred.Direction = ParameterDirection.Input;
                        pred.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pred);

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        if (pMovimiento.cod_ope == null)
                            p_cod_ope.Value = DBNull.Value;
                        else
                            p_cod_ope.Value = pMovimiento.cod_ope;
                        p_cod_ope.Direction = ParameterDirection.Input;
                        p_cod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        DbParameter p_saldo_total = cmdTransaccionFactory.CreateParameter();
                        p_saldo_total.ParameterName = "p_saldo_total";
                        if (pMovimiento.saldo_total == null)
                            p_saldo_total.Value = DBNull.Value;
                        else
                            p_saldo_total.Value = pMovimiento.saldo_total;
                        p_saldo_total.Direction = ParameterDirection.Input;
                        p_saldo_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_saldo_total);

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        if (pMovimiento.cod_cliente == null)
                            p_cod_persona.Value = DBNull.Value;
                        else
                            p_cod_persona.Value = pMovimiento.cod_cliente;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_TRANTARJE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pnum_tran.Value != null)
                            if (pnum_tran.Value.ToString().Trim() != "")
                                pMovimiento.num_tran = Convert.ToInt32(pnum_tran.Value.ToString());

                        dbConnectionFactory.CerrarConexion(connection);
                        return pMovimiento.num_tran;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaData", "CrearMovimiento", ex);
                        return null;
                    }
                }
            }
        }

        public Movimiento ConsultarMovimientoCoopcentral(int ptipo_convenio, string pTarjeta, string pOperacion, string ptipoTransaccion, string pDocumento, string pFecha, decimal pValor, Usuario vUsuario)
        {
            string _error = "";
            return ConsultarMovimientoCoopcentral(ptipo_convenio, pTarjeta, pOperacion, ptipoTransaccion, pDocumento, pFecha, pValor, vUsuario, ref _error);
        }

        public Movimiento ConsultarMovimientoCoopcentral(int ptipo_convenio, string pTarjeta, string pOperacion, string ptipoTransaccion, string pDocumento, string pFecha, decimal pValor, Usuario vUsuario, ref string pError)
        {
            DbDataReader resultado;
            Movimiento entidad = new Movimiento();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string _filtroTarjeta = (pTarjeta.Trim() != "" ? " AND (TARJETA = '" + pTarjeta + "' OR TARJETA IS NULL) " : " ");
                    string sql = "";
                    if (ptipoTransaccion == "99")       // Cuota de Manejo
                        sql = @"SELECT * FROM TRAN_TARJETA WHERE OPERACION = '" + pOperacion.ToString() + "' AND DOCUMENTO = '" + pDocumento + "' " + _filtroTarjeta + " AND REPLACE(FECHA, '-', '') = '" + pFecha.Replace("-", "") + "' ";
                    else
                        sql = @"SELECT * FROM TRAN_TARJETA WHERE OPERACION = '" + pOperacion.ToString() + "' AND TIPOTRANSACCION NOT IN ('99') AND REPLACE(FECHA, '-', '') = '" + pFecha.Replace("-", "") + "' " + _filtroTarjeta;                    
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUM_TRAN"] != DBNull.Value) entidad.num_tran = Convert.ToInt32(resultado["NUM_TRAN"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToString(resultado["FECHA"]);
                            if (resultado["HORA"] != DBNull.Value) entidad.hora = Convert.ToString(resultado["HORA"]);
                            if (resultado["DOCUMENTO"] != DBNull.Value) entidad.documento = Convert.ToString(resultado["DOCUMENTO"]);
                            if (resultado["NROCUENTA"] != DBNull.Value) entidad.nrocuenta = Convert.ToString(resultado["NROCUENTA"]);
                            if (resultado["TARJETA"] != DBNull.Value) entidad.tarjeta = Convert.ToString(resultado["TARJETA"]);
                            if (resultado["TIPOTRANSACCION"] != DBNull.Value) entidad.tipotransaccion = Convert.ToString(resultado["TIPOTRANSACCION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["MONTO"]);
                            if (resultado["COMISION"] != DBNull.Value) entidad.comision = Convert.ToDecimal(resultado["COMISION"]);
                            if (resultado["LUGAR"] != DBNull.Value) entidad.lugar = Convert.ToString(resultado["LUGAR"]);
                            if (resultado["OPERACION"] != DBNull.Value) entidad.operacion = Convert.ToString(resultado["OPERACION"]);
                            if (resultado["RED"] != DBNull.Value) entidad.red = Convert.ToString(resultado["RED"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_cliente = Convert.ToInt64(resultado["COD_PERSONA"]);
                        }
                        else
                        {
                            pError = sql;
                            dbConnectionFactory.CerrarConexion(connection);
                            return null;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        if (connection.State == ConnectionState.Open)
                            dbConnectionFactory.CerrarConexion(connection);
                        BOExcepcion.Throw("CuentaData", "ConsultarMovimientoCoopcentral" + " TipoTransaccion: ->" + ptipoTransaccion + "<- " + sql, ex);
                        return null;
                    }
                }          
            }
        }


        /// <summary>
        /// Obtiene la lista de cajas que tenga datafono
        /// </summary>
        /// <returns>Conjunto de Caja obtenidas</returns>
        public List<Datafono> ListarDatafono(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Datafono> lstCaja = new List<Datafono>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT *  FROM CAJA Where cod_datafono is not null Order By cod_caja asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Datafono entidad = new Datafono();
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToString(resultado["cod_caja"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["cod_datafono"] != DBNull.Value) entidad.cod_datafono = Convert.ToString(resultado["cod_datafono"]);
                            lstCaja.Add(entidad);
                        }

                        return lstCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentaData", "ListarDatafono", ex);
                        return null;
                    }

                }
            }
        }




    }
}


