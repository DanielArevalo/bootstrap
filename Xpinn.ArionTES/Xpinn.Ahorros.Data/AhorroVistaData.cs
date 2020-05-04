using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Ahorros.Entities;
using Xpinn.Imagenes.Data;
using Xpinn.Comun.Entities;
using Xpinn.Caja.Entities;

namespace Xpinn.Ahorros.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Ahorro_Vista
    /// </summary>
    public class AhorroVistaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Ahorro_Vista
        /// </summary>
        public AhorroVistaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public AhorroVista CrearAhorroVista(AhorroVista pAhorroVista, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "p_numero_cuenta";
                        pnumero_cuenta.Value = pAhorroVista.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter pcod_linea_ahorro = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_ahorro.ParameterName = "p_cod_linea_ahorro";
                        pcod_linea_ahorro.Value = pAhorroVista.cod_linea_ahorro;
                        pcod_linea_ahorro.Direction = ParameterDirection.Input;
                        pcod_linea_ahorro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_ahorro);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pAhorroVista.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pAhorroVista.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pcod_destino = cmdTransaccionFactory.CreateParameter();
                        pcod_destino.ParameterName = "p_cod_destino";
                        if (pAhorroVista.cod_destino == null)
                            pcod_destino.Value = DBNull.Value;
                        else
                            pcod_destino.Value = pAhorroVista.cod_destino;
                        pcod_destino.Direction = ParameterDirection.Input;
                        pcod_destino.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_destino);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pAhorroVista.observaciones == null)
                            pobservaciones.Value = DBNull.Value;
                        else
                            pobservaciones.Value = pAhorroVista.observaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pmodalidad = cmdTransaccionFactory.CreateParameter();
                        pmodalidad.ParameterName = "p_modalidad";
                        if (pAhorroVista.modalidad != null)
                            pmodalidad.Value = pAhorroVista.modalidad;
                        else
                            pmodalidad.Value = DBNull.Value;
                        pmodalidad.Direction = ParameterDirection.Input;
                        pmodalidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmodalidad);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pAhorroVista.estado != null)
                            pestado.Value = pAhorroVista.estado;
                        else
                            pestado.Value = DBNull.Value;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pfecha_apertura = cmdTransaccionFactory.CreateParameter();
                        pfecha_apertura.ParameterName = "p_fecha_apertura";
                        if (pAhorroVista.fecha_apertura != null || pAhorroVista.fecha_apertura == DateTime.MinValue)
                            pfecha_apertura.Value = pAhorroVista.fecha_apertura;
                        else
                            pfecha_apertura.Value = DBNull.Value;
                        pfecha_apertura.Direction = ParameterDirection.Input;
                        pfecha_apertura.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_apertura);

                        DbParameter pfecha_cierre = cmdTransaccionFactory.CreateParameter();
                        pfecha_cierre.ParameterName = "p_fecha_cierre";
                        if (pAhorroVista.fecha_cierre == null)
                            pfecha_cierre.Value = DBNull.Value;
                        else
                            pfecha_cierre.Value = pAhorroVista.fecha_cierre;
                        pfecha_cierre.Direction = ParameterDirection.Input;
                        pfecha_cierre.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_cierre);

                        DbParameter psaldo_total = cmdTransaccionFactory.CreateParameter();
                        psaldo_total.ParameterName = "p_saldo_total";
                        psaldo_total.Value = pAhorroVista.saldo_total;
                        psaldo_total.Direction = ParameterDirection.Input;
                        psaldo_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_total);

                        DbParameter psaldo_canje = cmdTransaccionFactory.CreateParameter();
                        psaldo_canje.ParameterName = "p_saldo_canje";
                        if (pAhorroVista.saldo_canje != null)
                            psaldo_canje.Value = pAhorroVista.saldo_canje;
                        else
                            psaldo_canje.Value = DBNull.Value;
                        psaldo_canje.Direction = ParameterDirection.Input;
                        psaldo_canje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_canje);

                        DbParameter pforma_tasa = cmdTransaccionFactory.CreateParameter();
                        pforma_tasa.ParameterName = "p_forma_tasa";
                        if (pAhorroVista.forma_tasa != 0 && pAhorroVista.forma_tasa != null)
                            pforma_tasa.Value = pAhorroVista.forma_tasa;
                        else
                            pforma_tasa.Value = DBNull.Value;
                        pforma_tasa.Direction = ParameterDirection.Input;
                        pforma_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pAhorroVista.tipo_historico != 0 && pAhorroVista.tipo_historico != null)
                            ptipo_historico.Value = pAhorroVista.tipo_historico;

                        else
                            ptipo_historico.Value = DBNull.Value;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pAhorroVista.desviacion != 0 && pAhorroVista.desviacion != null)
                            pdesviacion.Value = pAhorroVista.desviacion;
                        else
                            pdesviacion.Value = DBNull.Value;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter ptipo_tasa = cmdTransaccionFactory.CreateParameter();
                        ptipo_tasa.ParameterName = "p_tipo_tasa";
                        if (pAhorroVista.tipo_tasa != 0 && pAhorroVista.tipo_tasa != null)
                            ptipo_tasa.Value = pAhorroVista.tipo_tasa;
                        else
                            ptipo_tasa.Value = DBNull.Value;
                        ptipo_tasa.Direction = ParameterDirection.Input;
                        ptipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tasa);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        if (pAhorroVista.tasa != 0 && pAhorroVista.tasa != null)
                            ptasa.Value = pAhorroVista.tasa;
                        else
                            ptasa.Value = DBNull.Value;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter pfecha_interes = cmdTransaccionFactory.CreateParameter();
                        pfecha_interes.ParameterName = "p_fecha_interes";
                        if (pAhorroVista.fecha_interes == null)
                            pfecha_interes.Value = DBNull.Value;
                        else
                            pfecha_interes.Value = pAhorroVista.fecha_interes;
                        pfecha_interes.Direction = ParameterDirection.Input;
                        pfecha_interes.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_interes);

                        DbParameter psaldo_intereses = cmdTransaccionFactory.CreateParameter();
                        psaldo_intereses.ParameterName = "p_saldo_intereses";
                        if (pAhorroVista.saldo_intereses == null)
                            psaldo_intereses.Value = DBNull.Value;
                        else
                            psaldo_intereses.Value = pAhorroVista.saldo_intereses;
                        psaldo_intereses.Direction = ParameterDirection.Input;
                        psaldo_intereses.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_intereses);

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "p_retencion";
                        if (pAhorroVista.retencion == null)
                            pretencion.Value = DBNull.Value;
                        else
                            pretencion.Value = pAhorroVista.retencion;
                        pretencion.Direction = ParameterDirection.Input;
                        pretencion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pretencion);

                        DbParameter pcod_forma_pago = cmdTransaccionFactory.CreateParameter();
                        pcod_forma_pago.ParameterName = "p_cod_forma_pago";
                        if (pAhorroVista.cod_forma_pago == null)
                            pcod_forma_pago.Value = 1;
                        else
                            pcod_forma_pago.Value = pAhorroVista.cod_forma_pago;
                        pcod_forma_pago.Direction = ParameterDirection.Input;
                        pcod_forma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_forma_pago);

                        DbParameter pfecha_proximo_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_proximo_pago.ParameterName = "p_fecha_proximo_pago";
                        if (pAhorroVista.fecha_proximo_pago == null)
                            pfecha_proximo_pago.Value = DBNull.Value;
                        else
                            pfecha_proximo_pago.Value = pAhorroVista.fecha_proximo_pago;
                        pfecha_proximo_pago.Direction = ParameterDirection.Input;
                        pfecha_proximo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_proximo_pago);

                        DbParameter pfecha_ultimo_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_ultimo_pago.ParameterName = "p_fecha_ultimo_pago";
                        if (pAhorroVista.fecha_ultimo_pago == null)
                            pfecha_ultimo_pago.Value = DBNull.Value;
                        else
                            pfecha_ultimo_pago.Value = pAhorroVista.fecha_ultimo_pago;
                        pfecha_ultimo_pago.Direction = ParameterDirection.Input;
                        pfecha_ultimo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ultimo_pago);

                        DbParameter pvalor_cuota = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota.ParameterName = "p_valor_cuota";
                        if (pAhorroVista.valor_cuota == null)
                            pvalor_cuota.Value = DBNull.Value;
                        else
                            pvalor_cuota.Value = pAhorroVista.valor_cuota;
                        pvalor_cuota.Direction = ParameterDirection.Input;
                        pvalor_cuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        if (pAhorroVista.cod_periodicidad == null || pAhorroVista.cod_periodicidad == 0)
                            pcod_periodicidad.Value = DBNull.Value;
                        else
                            pcod_periodicidad.Value = pAhorroVista.cod_periodicidad;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter p_cod_asesor = cmdTransaccionFactory.CreateParameter();
                        p_cod_asesor.ParameterName = "p_cod_asesor";
                        if (pAhorroVista.cod_asesor.HasValue && pAhorroVista.cod_asesor != 0)
                            p_cod_asesor.Value = pAhorroVista.cod_asesor;
                        else
                            p_cod_asesor.Value = DBNull.Value;
                        p_cod_asesor.Direction = ParameterDirection.Input;
                        p_cod_asesor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_asesor);

                        DbParameter p_cod_empresa = cmdTransaccionFactory.CreateParameter();
                        p_cod_empresa.ParameterName = "p_cod_empresa";
                        if (pAhorroVista.cod_empresa_reca.HasValue && pAhorroVista.cod_empresa_reca != 0)
                            p_cod_empresa.Value = pAhorroVista.cod_empresa_reca;
                        else
                            p_cod_empresa.Value = DBNull.Value;
                        p_cod_empresa.Direction = ParameterDirection.Input;
                        p_cod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_empresa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_AHORROVIS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pAhorroVista, "ahorro_vista", vUsuario, Accion.Crear.ToString(), TipoAuditoria.AhorroVista, "Creacion de ahorro vista con numero de cuenta " + pAhorroVista.numero_cuenta); //REGISTRO DE AUDITORIA

                        return pAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "CrearAhorroVista", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarBeneficiarioAhorro(long idbeneficiario, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidbeneficiario = cmdTransaccionFactory.CreateParameter();
                        pidbeneficiario.ParameterName = "p_idbeneficiario";
                        pidbeneficiario.Value = idbeneficiario;
                        pidbeneficiario.Direction = ParameterDirection.Input;
                        pidbeneficiario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidbeneficiario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_BENEFICIAR_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "EliminarBeneficiarioAhorro", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Modifica el estado de la solicitud de retiro de ahorros
        /// </summary>
        /// <param name="vista"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool ModificarEstadoSolicitud(AhorroVista vista, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_ID_SOLICITUD = cmdTransaccionFactory.CreateParameter();
                        P_ID_SOLICITUD.ParameterName = "P_ID_SOLICITUD";
                        P_ID_SOLICITUD.Value = vista.id_solicitud;
                        P_ID_SOLICITUD.Direction = ParameterDirection.Input;
                        P_ID_SOLICITUD.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ID_SOLICITUD);

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = vista.nom_estado;
                        P_ESTADO.Direction = ParameterDirection.Input;
                        P_ESTADO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        DbParameter P_APROBADOR = cmdTransaccionFactory.CreateParameter();
                        P_APROBADOR.ParameterName = "P_APROBADOR";
                        P_APROBADOR.Value = usuario.nombre;
                        P_APROBADOR.Direction = ParameterDirection.Input;
                        P_APROBADOR.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_APROBADOR);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_SOLRETIRO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ModificarEstadoSolicitud", ex);
                        return false;
                    }
                }
            }
        }


        public List<AhorroVista> ListarAhorrosBeneficiaros(string filtro, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AhorroVista> lstAhorros = new List<AhorroVista>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT ben.*, 
                                        aho.FECHA_APERTURA, aho.SALDO_TOTAL, aho.COD_LINEA_AHORRO | | ' - ' | | lin.Descripcion as desc_linea,
                                        CASE aho.ESTADO WHEN 0 THEN 'Apertura' WHEN 1 THEN 'Activa' WHEN 3 THEN 'Bloqueada' WHEN 4 THEN 'Cerrada' WHEN 5 THEN 'Embargada' END as Estado,
                                        per.IDENTIFICACION, per.PRIMER_NOMBRE | | ' ' | | per.PRIMER_APELLIDO as Nombre
                                        from AHORRO_VISTA aho
                                        JOIN BENEFICIARIO_AHORROVISTA ben on ben.NUMERO_CUENTA = aho.NUMERO_CUENTA
                                        JOIN LINEAAHORRO lin on lin.COD_LINEA_AHORRO = aho.COD_LINEA_AHORRO
                                        JOIN PERSONA per on per.COD_PERSONA = aho.COD_PERSONA "
                                        + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();

                            if (resultado["IDBENEFICIARIO"] != DBNull.Value) entidad.idbeneficiario = Convert.ToInt64(resultado["IDBENEFICIARIO"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["IDENTIFICACION_BEN"] != DBNull.Value) entidad.identificacion_ben = Convert.ToString(resultado["IDENTIFICACION_BEN"]);
                            if (resultado["NOMBRE_BEN"] != DBNull.Value) entidad.nombres_ben = Convert.ToString(resultado["NOMBRE_BEN"]);
                            if (resultado["FECHA_NACIMIENTO_BEN"] != DBNull.Value) entidad.fecha_nacimiento_ben = Convert.ToDateTime(resultado["FECHA_NACIMIENTO_BEN"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["desc_linea"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["desc_linea"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["Estado"] != DBNull.Value) entidad.estados = Convert.ToString(resultado["Estado"]);
                            if (resultado["desc_linea"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["desc_linea"]);

                            lstAhorros.Add(entidad);
                        }
                        return lstAhorros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarAhorrosBeneficiaros", ex);
                        return null;
                    }
                }
            }
        }

        public List<ReporteMovimiento> ListarDetalleExtracto(String codcuenta, DateTime pFechaPago, Usuario pUsuario, DateTime? fechaInicio = null, DateTime? fechaFinal = null, decimal saldoInicial = 0)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbDataReader resultado = default(DbDataReader);
                        List<ReporteMovimiento> lstExtracto = new List<ReporteMovimiento>();
                        Configuracion global = new Configuracion();
                        string sql1 = @"Select o.cod_ope AS COD_OPE, o.fecha_oper, o.tipo_ope, r.descripcion AS nomtipo_ope, o.cod_ofi, f.nombre, 
                        Case r.tipo_mov When 1 Then 'Débito' When 2 Then 'Crédito' End As tipo_mov, t.valor, t.documento_soporte
                        From tran_ahorro t 
                        Inner Join Operacion O On t.cod_ope = o.cod_ope 
                        Inner Join tipo_ope p On p.tipo_ope = o.tipo_ope
                        Inner Join oficina f On o.cod_ofi = f.cod_oficina
                        Inner Join tipo_tran r On t.tipo_tran = r.tipo_tran
                        Where t.numero_cuenta = '" + codcuenta + "'";

                        if (fechaInicio.HasValue)
                        {
                            sql1 += " AND Truncar(o.fecha_oper) >= to_date('" + fechaInicio.Value.ToString(global.ObtenerFormatoFecha()) + "', '" + global.ObtenerFormatoFecha() + "') ";
                        }
                        if (fechaFinal.HasValue)
                        {
                            sql1 += " AND Truncar(o.fecha_oper) <= to_date('" + fechaFinal.Value.ToString(global.ObtenerFormatoFecha()) + "', '" + global.ObtenerFormatoFecha() + "') ";
                        }

                        string sql = sql1 + " Order by o.fecha_oper ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        decimal saldo = saldoInicial;
                        while (resultado.Read())
                        {
                            ReporteMovimiento entidad = new ReporteMovimiento();
                            if (resultado["fecha_oper"] != DBNull.Value) entidad.fecha_oper = Convert.ToDateTime(resultado["fecha_oper"]);
                            if (resultado["tipo_ope"] != DBNull.Value) entidad.tipo_ope = Convert.ToString(resultado["tipo_ope"]);
                            if (resultado["nomtipo_ope"] != DBNull.Value) entidad.nomtipo_ope = Convert.ToString(resultado["nomtipo_ope"]);
                            if (resultado["cod_ofi"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["cod_ofi"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["tipo_mov"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["tipo_mov"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt32(resultado["cod_ope"]);
                            if (resultado["documento_soporte"] != DBNull.Value) entidad.soporte = Convert.ToString(resultado["documento_soporte"]);




                            // Calcular el saldo de la cuenta
                            if (entidad.tipo_mov == "1" || entidad.tipo_mov == "Débito" || entidad.tipo_mov == "Debito")
                                saldo -= entidad.valor;  // Si el tipo de movimiento es débito resta al saldo
                            else
                                saldo += entidad.valor;  // Si el tipo de movimiento es crédito suma al saldo
                            entidad.saldo = saldo;
                            lstExtracto.Add(entidad);
                        }
                        return lstExtracto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExtractoData", "ListarDetalleExtracto", ex);
                        return null;
                    }
                }
            }
        }

        public AhorroVista ConsultarAhorroVistaDatosOficina(string numeroCuenta, Usuario usuario)
        {
            DbDataReader resultado;
            AhorroVista entidad = new AhorroVista();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select ofi.DIRECCION, ofi.TELEFONO, ofi.NOMBRE, per.direccion as direccion_persona, ciu.nomciudad
                                        from ahorro_vista aho
                                        join persona per on per.COD_PERSONA = aho.COD_PERSONA
                                        join oficina ofi on ofi.COD_OFICINA = aho.COD_OFICINA
                                        join CIUDADES ciu on ciu.CODCIUDAD = per.CODCIUDADRESIDENCIA 
                                        where aho.NUMERO_CUENTA =  " + numeroCuenta;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion_oficina = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono_oficina = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_oficina = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["direccion_persona"] != DBNull.Value) entidad.direccion_persona = Convert.ToString(resultado["direccion_persona"]);
                            if (resultado["nomciudad"] != DBNull.Value) entidad.CiudadResidencia = Convert.ToString(resultado["nomciudad"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ConsultarAhorroVistaDatosOficina", ex);
                        return null;
                    }
                }
            }
        }


        public string ConsultarNombreLineaDeAhorroPorCodigo(string linea, Usuario usuario)
        {
            DbDataReader resultado;
            string nombre = string.Empty;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select descripcion from lineaahorro where cod_linea_ahorro = " + linea;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["descripcion"] != DBNull.Value) nombre = Convert.ToString(resultado["descripcion"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return nombre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ConsultarNombreLineaDeAhorroPorCodigo", ex);
                        return null;
                    }
                }
            }
        }

        public decimal ConsultarSaldoInicialPeriodoAhorroVista(string numeroCuenta, DateTime fechaInicio, Usuario usuario)
        {
            DbDataReader resultado;
            ReporteMovimiento entidad = new ReporteMovimiento();
            decimal saldoInicial = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT 
                                          NVL(A.saldo_total - (SELECT SUM(CASE tip.tipo_mov WHEN 1 THEN -t.valor WHEN 2 THEN t.valor END) 
                                          FROM tran_ahorro t JOIN operacion o ON t.cod_ope = o.cod_ope JOIN tipo_tran tip ON tip.tipo_tran = t.tipo_tran
                                          WHERE t.numero_cuenta = A.numero_cuenta AND TRUNC(o.fecha_oper) > to_date('" + fechaInicio.ToShortDateString() + @"', 'dd/MM/yyyy')), 0) AS saldo_inicial
                                        FROM ahorro_vista a WHERE a.numero_cuenta = '" + numeroCuenta + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["saldo_inicial"] != DBNull.Value) saldoInicial = Convert.ToDecimal(resultado["saldo_inicial"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return saldoInicial;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ConsultarSaldoInicialPeriodoAhorroVista", ex);
                        return 0;
                    }
                }
            }
        }


        public string CrearSolicitudAhorrosVista(AhorroVista pAhorros, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_ID_SOL_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_ID_SOL_PRODUCTO.ParameterName = "P_ID_SOL_PRODUCTO";
                        P_ID_SOL_PRODUCTO.Value = 0;
                        P_ID_SOL_PRODUCTO.Direction = ParameterDirection.Output;
                        P_ID_SOL_PRODUCTO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ID_SOL_PRODUCTO);

                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = Convert.ToInt32(pAhorros.cod_persona);
                        P_COD_PERSONA.Direction = ParameterDirection.Input;
                        P_COD_PERSONA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                        DbParameter P_COD_TIPO_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_COD_TIPO_PRODUCTO.ParameterName = "P_COD_TIPO_PRODUCTO";
                        P_COD_TIPO_PRODUCTO.Value = Convert.ToInt32(pAhorros.tipo_producto);
                        P_COD_TIPO_PRODUCTO.Direction = ParameterDirection.Input;
                        P_COD_TIPO_PRODUCTO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_TIPO_PRODUCTO);

                        DbParameter P_LINEA = cmdTransaccionFactory.CreateParameter();
                        P_LINEA.ParameterName = "P_LINEA";
                        P_LINEA.Value = pAhorros.cod_linea_ahorro;
                        P_LINEA.Direction = ParameterDirection.Input;
                        P_LINEA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_LINEA);

                        DbParameter P_PLAZO = cmdTransaccionFactory.CreateParameter();
                        P_PLAZO.ParameterName = "P_PLAZO";
                        P_PLAZO.Value = 0;
                        P_PLAZO.Direction = ParameterDirection.Input;
                        P_PLAZO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_PLAZO);

                        DbParameter P_NUM_CUOTAS = cmdTransaccionFactory.CreateParameter();
                        P_NUM_CUOTAS.ParameterName = "P_NUM_CUOTAS";
                        P_NUM_CUOTAS.Value = 0;
                        P_NUM_CUOTAS.Direction = ParameterDirection.Input;
                        P_NUM_CUOTAS.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_NUM_CUOTAS);

                        DbParameter P_VALOR = cmdTransaccionFactory.CreateParameter();
                        P_VALOR.ParameterName = "P_VALOR";
                        P_VALOR.Value = Convert.ToInt32(pAhorros.valor_cuota);
                        P_VALOR.Direction = ParameterDirection.Input;
                        P_VALOR.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR);

                        DbParameter P_PERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        P_PERIODICIDAD.ParameterName = "P_PERIODICIDAD";
                        P_PERIODICIDAD.Value = Convert.ToString(pAhorros.cod_periodicidad);
                        P_PERIODICIDAD.Direction = ParameterDirection.Input;
                        P_PERIODICIDAD.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_PERIODICIDAD);

                        DbParameter P_FORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FORMA_PAGO.ParameterName = "P_FORMA_PAGO";
                        P_FORMA_PAGO.Value = Convert.ToInt32(pAhorros.cod_forma_pago);
                        P_FORMA_PAGO.Direction = ParameterDirection.Input;
                        P_FORMA_PAGO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_FORMA_PAGO);

                        DbParameter P_COD_DESTINO = cmdTransaccionFactory.CreateParameter();
                        P_COD_DESTINO.ParameterName = "P_COD_DESTINO";
                        P_COD_DESTINO.Value = 0;
                        P_COD_DESTINO.Direction = ParameterDirection.Input;
                        P_COD_DESTINO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_DESTINO);

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = Convert.ToString(pAhorros.estado_modificacion1);
                        P_ESTADO.Direction = ParameterDirection.Input;
                        P_ESTADO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        // ADICIONADO PARA ALMACENAR BENEFICIARIO CLUB
                        DbParameter P_IDENTIFICACION_BEN = cmdTransaccionFactory.CreateParameter();
                        P_IDENTIFICACION_BEN.ParameterName = "P_IDENTIFICACION_BEN";
                        P_IDENTIFICACION_BEN.Value = pAhorros.identificacion_ben ?? (object)DBNull.Value;
                        P_IDENTIFICACION_BEN.Direction = ParameterDirection.Input;
                        P_IDENTIFICACION_BEN.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_IDENTIFICACION_BEN);

                        DbParameter P_NOMBRE_BEN = cmdTransaccionFactory.CreateParameter();
                        P_NOMBRE_BEN.ParameterName = "P_NOMBRE_BEN";
                        P_NOMBRE_BEN.Value = pAhorros.nombres_ben ?? (object)DBNull.Value;
                        P_NOMBRE_BEN.Direction = ParameterDirection.Input;
                        P_NOMBRE_BEN.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_NOMBRE_BEN);

                        DbParameter P_FECHA_NACIMIENTO_BEN = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_NACIMIENTO_BEN.ParameterName = "P_FECHA_NACIMIENTO_BEN";
                        if (pAhorros.fecha_nacimiento_ben.ToString() == "")
                        { P_FECHA_NACIMIENTO_BEN.Value = "";}
                        else { P_FECHA_NACIMIENTO_BEN.Value = pAhorros.fecha_nacimiento_ben; }
                        P_FECHA_NACIMIENTO_BEN.Direction = ParameterDirection.Input;
                        P_FECHA_NACIMIENTO_BEN.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_NACIMIENTO_BEN);


                        DbParameter P_PARENTESCO_BEN = cmdTransaccionFactory.CreateParameter();
                        P_PARENTESCO_BEN.ParameterName = "P_PARENTESCO_BEN";
                        if (pAhorros.parentesco_ben == 0)
                        { P_PARENTESCO_BEN.Value = 0; }
                        else { P_PARENTESCO_BEN.Value = pAhorros.parentesco_ben; }
                        P_PARENTESCO_BEN.Direction = ParameterDirection.Input;
                        P_PARENTESCO_BEN.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_PARENTESCO_BEN);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_SOLPRODUCTOB_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (Convert.ToInt32(P_ID_SOL_PRODUCTO.Value) != 0)
                        {
                            return P_ID_SOL_PRODUCTO.Value.ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "CrearSolicitudAhorros", ex);
                        return null;
                    }
                }
            }
        }

        public string CrearSolicitudAhorros(AhorroVista pAhorros, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_ID_SOL_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_ID_SOL_PRODUCTO.ParameterName = "P_ID_SOL_PRODUCTO";
                        P_ID_SOL_PRODUCTO.Value = 0;
                        P_ID_SOL_PRODUCTO.Direction = ParameterDirection.Output;
                        P_ID_SOL_PRODUCTO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ID_SOL_PRODUCTO);

                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = Convert.ToInt32(pAhorros.cod_persona);
                        P_COD_PERSONA.Direction = ParameterDirection.Input;
                        P_COD_PERSONA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                        DbParameter P_COD_TIPO_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_COD_TIPO_PRODUCTO.ParameterName = "P_COD_TIPO_PRODUCTO";
                        P_COD_TIPO_PRODUCTO.Value = Convert.ToInt32(pAhorros.tipo_producto);
                        P_COD_TIPO_PRODUCTO.Direction = ParameterDirection.Input;
                        P_COD_TIPO_PRODUCTO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_TIPO_PRODUCTO);

                        DbParameter P_LINEA = cmdTransaccionFactory.CreateParameter();
                        P_LINEA.ParameterName = "P_LINEA";
                        P_LINEA.Value = pAhorros.cod_linea_ahorro;
                        P_LINEA.Direction = ParameterDirection.Input;
                        P_LINEA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_LINEA);

                        DbParameter P_PLAZO = cmdTransaccionFactory.CreateParameter();
                        P_PLAZO.ParameterName = "P_PLAZO";
                        P_PLAZO.Value = 0;
                        P_PLAZO.Direction = ParameterDirection.Input;
                        P_PLAZO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_PLAZO);

                        DbParameter P_NUM_CUOTAS = cmdTransaccionFactory.CreateParameter();
                        P_NUM_CUOTAS.ParameterName = "P_NUM_CUOTAS";
                        P_NUM_CUOTAS.Value = 0;
                        P_NUM_CUOTAS.Direction = ParameterDirection.Input;
                        P_NUM_CUOTAS.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_NUM_CUOTAS);

                        DbParameter P_VALOR = cmdTransaccionFactory.CreateParameter();
                        P_VALOR.ParameterName = "P_VALOR";
                        P_VALOR.Value = Convert.ToInt32(pAhorros.valor_cuota);
                        P_VALOR.Direction = ParameterDirection.Input;
                        P_VALOR.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR);

                        DbParameter P_PERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        P_PERIODICIDAD.ParameterName = "P_PERIODICIDAD";
                        P_PERIODICIDAD.Value = Convert.ToString(pAhorros.cod_periodicidad);
                        P_PERIODICIDAD.Direction = ParameterDirection.Input;
                        P_PERIODICIDAD.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_PERIODICIDAD);

                        DbParameter P_FORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FORMA_PAGO.ParameterName = "P_FORMA_PAGO";
                        P_FORMA_PAGO.Value = Convert.ToInt32(pAhorros.cod_forma_pago);
                        P_FORMA_PAGO.Direction = ParameterDirection.Input;
                        P_FORMA_PAGO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_FORMA_PAGO);

                        DbParameter P_COD_DESTINO = cmdTransaccionFactory.CreateParameter();
                        P_COD_DESTINO.ParameterName = "P_COD_DESTINO";
                        P_COD_DESTINO.Value = 0;
                        P_COD_DESTINO.Direction = ParameterDirection.Input;
                        P_COD_DESTINO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_DESTINO);

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = Convert.ToString(pAhorros.estado_modificacion1);
                        P_ESTADO.Direction = ParameterDirection.Input;
                        P_ESTADO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_SOLPRODUCTO_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (Convert.ToInt32(P_ID_SOL_PRODUCTO.Value) != 0)
                        {
                            return P_ID_SOL_PRODUCTO.Value.ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "CrearSolicitudAhorros", ex);
                        return null;
                    }
                }
            }
        }

        public AhorroVista ConsultarCuentaBancaria(string pCodPersona, Usuario pUsuario)
        {
            DbDataReader resultado;
            AhorroVista ahorroVista = new AhorroVista();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select c.IDCUENTABANCARIA, c.COD_BANCO, c.NUMERO_CUENTA, C.TIPO_CUENTA
                                       from persona_cuentasbancarias c where cod_persona = " + pCodPersona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["IDCUENTABANCARIA"] != DBNull.Value)
                            {
                                if (resultado["COD_BANCO"] != DBNull.Value) ahorroVista.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                                if (resultado["NUMERO_CUENTA"] != DBNull.Value) ahorroVista.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                                if (resultado["TIPO_CUENTA"] != DBNull.Value) ahorroVista.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return ahorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarCuentaAhorroVista", ex);
                        return null;
                    }
                }
            }
        }

        public ReporteMovimiento ConsultarExtractoAhorroVista(string numeroCuenta, DateTime fechaCorte, DateTime fechaInicio, Usuario usuario)
        {
            DbDataReader resultado;
            ReporteMovimiento entidad = new ReporteMovimiento();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT 
                                          ESNULO((SELECT Sum(aho.valor) FROM tran_ahorro aho JOIN tipo_tran tipo ON tipo.tipo_tran = aho.tipo_tran JOIN operacion ope ON aho.cod_ope = ope.cod_ope WHERE tipo.tipo_mov = 2 AND tipo.tipo_tran != 207 AND aho.numero_cuenta = '" + numeroCuenta + @"' AND TRUNC(ope.fecha_oper) <= to_date('" + fechaCorte.ToShortDateString() + @"', 'dd/MM/yyyy') AND TRUNC(ope.fecha_oper) >= to_date('" + fechaInicio.ToShortDateString() + @"', 'dd/MM/yyyy')), 0) AS Abonos,
                                          ESNULO((SELECT Count(*) FROM tran_ahorro aho JOIN tipo_tran tipo ON tipo.tipo_tran = aho.tipo_tran JOIN operacion ope ON aho.cod_ope = ope.cod_ope WHERE tipo.tipo_mov = 2 AND tipo.tipo_tran != 207 AND aho.numero_cuenta = '" + numeroCuenta + @"' AND TRUNC(ope.fecha_oper) <= to_date('" + fechaCorte.ToShortDateString() + @"', 'dd/MM/yyyy') AND TRUNC(ope.fecha_oper) >= to_date('" + fechaInicio.ToShortDateString() + @"', 'dd/MM/yyyy')),0) AS Contador_Abonos,
                                          ESNULO((SELECT Sum(aho.valor) FROM tran_ahorro aho JOIN tipo_tran tipo ON tipo.tipo_tran = aho.tipo_tran JOIN operacion ope ON aho.cod_ope = ope.cod_ope WHERE tipo.tipo_mov = 1 AND tipo.tipo_tran NOT IN (225,208) AND aho.numero_cuenta = '" + numeroCuenta + @"' AND TRUNC(ope.fecha_oper) <= to_date('" + fechaCorte.ToShortDateString() + @"', 'dd/MM/yyyy') AND TRUNC(ope.fecha_oper) >= to_date('" + fechaInicio.ToShortDateString() + @"', 'dd/MM/yyyy')),0) AS Cargos,
                                          ESNULO((SELECT COUNT(*) FROM tran_ahorro aho JOIN tipo_tran tipo ON tipo.tipo_tran = aho.tipo_tran JOIN operacion ope ON aho.cod_ope = ope.cod_ope WHERE tipo.tipo_mov = 1 AND tipo.tipo_tran NOT IN (225,208) AND aho.numero_cuenta = '" + numeroCuenta + @"' AND TRUNC(ope.fecha_oper) <= to_date('" + fechaCorte.ToShortDateString() + @"', 'dd/MM/yyyy') AND TRUNC(ope.fecha_oper) >= to_date('" + fechaInicio.ToShortDateString() + @"', 'dd/MM/yyyy')),0) AS Contador_Cargos,
                                          ESNULO(SUM(CASE t.tipo_tran WHEN 207 THEN t.valor END),0) AS Intereses,
                                          ESNULO(COUNT(CASE t.tipo_tran WHEN 207 THEN t.valor END),0) AS Contador_Intereses,
                                          ESNULO(SUM(CASE t.tipo_tran WHEN 208 THEN t.valor END),0) AS Retencion,
                                          ESNULO(COUNT(CASE t.tipo_tran WHEN 208 THEN t.valor END),0) AS Contador_Retencion,
                                          ESNULO(SUM(CASE t.tipo_tran WHEN 225 THEN t.valor END),0) AS GMF,
                                          ESNULO(COUNT(CASE t.tipo_tran WHEN 225 THEN t.valor END),0) AS Contador_GMF
                                        FROM tran_ahorro t
                                        JOIN operacion o ON t.cod_ope = o.cod_ope
                                        JOIN tipo_tran tip on tip.tipo_tran = t.tipo_tran
                                        WHERE t.numero_cuenta = '" + numeroCuenta + "'"
                                        + @" AND TRUNC(o.fecha_oper) <= to_date('" + fechaCorte.ToShortDateString() + "', 'dd/MM/yyyy') AND TRUNC(o.fecha_oper) >= to_date('" + fechaInicio.ToShortDateString() + "', 'dd/MM/yyyy') GROUP BY 1 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["Abonos"] != DBNull.Value) entidad.Abonos = Convert.ToDecimal(resultado["Abonos"]);
                            if (resultado["Contador_Abonos"] != DBNull.Value) entidad.Contador_Abonos = Convert.ToDecimal(resultado["Contador_Abonos"]);
                            if (resultado["Cargos"] != DBNull.Value) entidad.Cargos = Convert.ToDecimal(resultado["Cargos"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Contador_Cargos"] != DBNull.Value) entidad.Contador_Cargos = Convert.ToDecimal(resultado["Contador_Cargos"]);
                            if (resultado["Intereses"] != DBNull.Value) entidad.Intereses = Convert.ToDecimal(resultado["Intereses"]);
                            if (resultado["Contador_Intereses"] != DBNull.Value) entidad.Contador_Intereses = Convert.ToDecimal(resultado["Contador_Intereses"]);
                            if (resultado["Retencion"] != DBNull.Value) entidad.Retencion = Convert.ToDecimal(resultado["Retencion"]);
                            if (resultado["Contador_Retencion"] != DBNull.Value) entidad.Contador_Retencion = Convert.ToDecimal(resultado["Contador_Retencion"]);
                            if (resultado["GMF"] != DBNull.Value) entidad.GMF = Convert.ToDecimal(resultado["GMF"]);
                            if (resultado["Contador_GMF"] != DBNull.Value) entidad.Contador_GMF = Convert.ToDecimal(resultado["Contador_GMF"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ConsultarExtractoAhorroVista", ex);
                        return null;
                    }
                }
            }
        }

        public List<AhorroVista> ListarCuentaAhorroVista(long cod, Usuario usuario)
        {
            DbDataReader resultado;
            List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select NUMERO_CUENTA from ahorro_vista where cod_persona = " + cod;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();

                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);

                            lstAhorroVista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarCuentaAhorroVista", ex);
                        return null;
                    }
                }
            }
        }


        public AhorroVista ModificarAhorroVista(AhorroVista pAhorroVista, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        AhorroVista ahorroAnterior = ConsultarAhorroVista(pAhorroVista.numero_cuenta, vUsuario);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "p_numero_cuenta";
                        pnumero_cuenta.Value = pAhorroVista.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter pcod_linea_ahorro = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_ahorro.ParameterName = "p_cod_linea_ahorro";
                        pcod_linea_ahorro.Value = pAhorroVista.cod_linea_ahorro;
                        pcod_linea_ahorro.Direction = ParameterDirection.Input;
                        pcod_linea_ahorro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_ahorro);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pAhorroVista.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pAhorroVista.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pcod_destino = cmdTransaccionFactory.CreateParameter();
                        pcod_destino.ParameterName = "p_cod_destino";
                        if (pAhorroVista.cod_destino != null) pcod_destino.Value = pAhorroVista.cod_destino; else pcod_destino.Value = DBNull.Value;
                        pcod_destino.Direction = ParameterDirection.Input;
                        pcod_destino.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_destino);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pAhorroVista.observaciones == null)
                            pobservaciones.Value = DBNull.Value;
                        else
                            pobservaciones.Value = pAhorroVista.observaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pmodalidad = cmdTransaccionFactory.CreateParameter();
                        pmodalidad.ParameterName = "p_modalidad";
                        pmodalidad.Value = pAhorroVista.modalidad;
                        pmodalidad.Direction = ParameterDirection.Input;
                        pmodalidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmodalidad);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pAhorroVista.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pfecha_apertura = cmdTransaccionFactory.CreateParameter();
                        pfecha_apertura.ParameterName = "p_fecha_apertura";
                        pfecha_apertura.Value = pAhorroVista.fecha_apertura;
                        pfecha_apertura.Direction = ParameterDirection.Input;
                        pfecha_apertura.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_apertura);

                        DbParameter pfecha_cierre = cmdTransaccionFactory.CreateParameter();
                        pfecha_cierre.ParameterName = "p_fecha_cierre";
                        if (pAhorroVista.fecha_cierre == null)
                            pfecha_cierre.Value = DBNull.Value;
                        else
                            pfecha_cierre.Value = pAhorroVista.fecha_cierre;
                        pfecha_cierre.Direction = ParameterDirection.Input;
                        pfecha_cierre.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_cierre);

                        DbParameter psaldo_total = cmdTransaccionFactory.CreateParameter();
                        psaldo_total.ParameterName = "p_saldo_total";
                        psaldo_total.Value = pAhorroVista.saldo_total;
                        psaldo_total.Direction = ParameterDirection.Input;
                        psaldo_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_total);

                        DbParameter psaldo_canje = cmdTransaccionFactory.CreateParameter();
                        psaldo_canje.ParameterName = "p_saldo_canje";
                        psaldo_canje.Value = pAhorroVista.saldo_canje;
                        psaldo_canje.Direction = ParameterDirection.Input;
                        psaldo_canje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_canje);

                        DbParameter pforma_tasa = cmdTransaccionFactory.CreateParameter();
                        pforma_tasa.ParameterName = "p_forma_tasa";
                        if (pAhorroVista.forma_tasa == null)
                            pforma_tasa.Value = DBNull.Value;
                        else
                            pforma_tasa.Value = pAhorroVista.forma_tasa;
                        pforma_tasa.Direction = ParameterDirection.Input;
                        pforma_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pAhorroVista.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pAhorroVista.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pAhorroVista.desviacion == null)
                            pdesviacion.Value = DBNull.Value;
                        else
                            pdesviacion.Value = pAhorroVista.desviacion;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter ptipo_tasa = cmdTransaccionFactory.CreateParameter();
                        ptipo_tasa.ParameterName = "p_tipo_tasa";
                        if (pAhorroVista.tipo_tasa == null || pAhorroVista.tipo_tasa == 0)
                            ptipo_tasa.Value = DBNull.Value;
                        else
                            ptipo_tasa.Value = pAhorroVista.tipo_tasa;
                        ptipo_tasa.Direction = ParameterDirection.Input;
                        ptipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tasa);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        if (pAhorroVista.tasa == null)
                            ptasa.Value = DBNull.Value;
                        else
                            ptasa.Value = pAhorroVista.tasa;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter pfecha_interes = cmdTransaccionFactory.CreateParameter();
                        pfecha_interes.ParameterName = "p_fecha_interes";
                        if (pAhorroVista.fecha_interes == null)
                            pfecha_interes.Value = DBNull.Value;
                        else
                            pfecha_interes.Value = pAhorroVista.fecha_interes;
                        pfecha_interes.Direction = ParameterDirection.Input;
                        pfecha_interes.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_interes);

                        DbParameter psaldo_intereses = cmdTransaccionFactory.CreateParameter();
                        psaldo_intereses.ParameterName = "p_saldo_intereses";
                        if (pAhorroVista.saldo_intereses == null)
                            psaldo_intereses.Value = DBNull.Value;
                        else
                            psaldo_intereses.Value = pAhorroVista.saldo_intereses;
                        psaldo_intereses.Direction = ParameterDirection.Input;
                        psaldo_intereses.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_intereses);

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "p_retencion";
                        if (pAhorroVista.retencion == null)
                            pretencion.Value = DBNull.Value;
                        else
                            pretencion.Value = pAhorroVista.retencion;
                        pretencion.Direction = ParameterDirection.Input;
                        pretencion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pretencion);

                        DbParameter pcod_forma_pago = cmdTransaccionFactory.CreateParameter();
                        pcod_forma_pago.ParameterName = "p_cod_forma_pago";
                        if (pAhorroVista.cod_forma_pago == null)
                            pcod_forma_pago.Value = DBNull.Value;
                        else
                            pcod_forma_pago.Value = pAhorroVista.cod_forma_pago;
                        pcod_forma_pago.Direction = ParameterDirection.Input;
                        pcod_forma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_forma_pago);

                        DbParameter pfecha_proximo_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_proximo_pago.ParameterName = "p_fecha_proximo_pago";
                        if (pAhorroVista.fecha_proximo_pago == null)
                            pfecha_proximo_pago.Value = DBNull.Value;
                        else
                            pfecha_proximo_pago.Value = pAhorroVista.fecha_proximo_pago;
                        pfecha_proximo_pago.Direction = ParameterDirection.Input;
                        pfecha_proximo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_proximo_pago);

                        DbParameter pfecha_ultimo_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_ultimo_pago.ParameterName = "p_fecha_ultimo_pago";
                        if (pAhorroVista.fecha_ultimo_pago == null)
                            pfecha_ultimo_pago.Value = DBNull.Value;
                        else
                            pfecha_ultimo_pago.Value = pAhorroVista.fecha_ultimo_pago;
                        pfecha_ultimo_pago.Direction = ParameterDirection.Input;
                        pfecha_ultimo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ultimo_pago);

                        DbParameter pvalor_cuota = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota.ParameterName = "p_valor_cuota";
                        if (pAhorroVista.valor_cuota == null)
                            pvalor_cuota.Value = DBNull.Value;
                        else
                            pvalor_cuota.Value = pAhorroVista.valor_cuota;
                        pvalor_cuota.Direction = ParameterDirection.Input;
                        pvalor_cuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        if (pAhorroVista.cod_periodicidad != null && pAhorroVista.cod_periodicidad != 0)
                            pcod_periodicidad.Value = pAhorroVista.cod_periodicidad;
                        if (pAhorroVista.cod_periodicidad == 0 || pAhorroVista.cod_periodicidad == null)
                            pcod_periodicidad.Value = DBNull.Value;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter p_cod_asesor = cmdTransaccionFactory.CreateParameter();
                        p_cod_asesor.ParameterName = "p_cod_asesor";
                        if (pAhorroVista.cod_asesor.HasValue && pAhorroVista.cod_asesor != 0)
                            p_cod_asesor.Value = pAhorroVista.cod_asesor;
                        else
                            p_cod_asesor.Value = DBNull.Value;
                        p_cod_asesor.Direction = ParameterDirection.Input;
                        p_cod_asesor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_asesor);

                        DbParameter p_cod_empresa = cmdTransaccionFactory.CreateParameter();
                        p_cod_empresa.ParameterName = "p_cod_empresa";
                        if (pAhorroVista.cod_empresa_reca.HasValue && pAhorroVista.cod_empresa_reca != 0)
                            p_cod_empresa.Value = pAhorroVista.cod_empresa_reca;
                        else
                            p_cod_empresa.Value = DBNull.Value;
                        p_cod_empresa.Direction = ParameterDirection.Input;
                        p_cod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_empresa);




                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_AHORROVIS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pAhorroVista, "ahorro_vista", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.AhorroVista, "Modificacion de ahorro vista con numero de cuenta " + pAhorroVista.numero_cuenta, ahorroAnterior); //REGISTRO DE AUDITORIA

                        return pAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ModificarAhorroVista", ex);
                        return null;
                    }
                }
            }
        }
        public AhorroVista ModificarCambioEstados(AhorroVista pAhorroVista, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter numero = cmdTransaccionFactory.CreateParameter();
                        numero.ParameterName = "PNUMERO_CUENTA";
                        numero.Value = pAhorroVista.numero_cuenta;
                        numero.Direction = ParameterDirection.Input;
                        numero.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(numero);

                        DbParameter fecha = cmdTransaccionFactory.CreateParameter();
                        fecha.ParameterName = "PFECHA";
                        fecha.Value = pAhorroVista.fecha;
                        fecha.Direction = ParameterDirection.Input;
                        fecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(fecha);

                        DbParameter estado = cmdTransaccionFactory.CreateParameter();
                        estado.ParameterName = "PESTADO";
                        estado.Value = pAhorroVista.estado;
                        estado.Direction = ParameterDirection.Input;
                        estado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(estado);

                        DbParameter motivos = cmdTransaccionFactory.CreateParameter();
                        motivos.ParameterName = "PMOTIVO";
                        motivos.Value = pAhorroVista.motivos != null ? (object)pAhorroVista.motivos : DBNull.Value;
                        motivos.Direction = ParameterDirection.Input;
                        motivos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(motivos);

                        DbParameter codusuario = cmdTransaccionFactory.CreateParameter();
                        codusuario.ParameterName = "PCODUSUARIO";
                        codusuario.Value = pAhorroVista.codusuario;
                        codusuario.Direction = ParameterDirection.Input;
                        codusuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(codusuario);

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        p_cod_ope.Value = pAhorroVista.cod_ope;
                        p_cod_ope.Direction = ParameterDirection.Input;
                        p_cod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_CAMBIOESTADO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);



                        return pAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ModificarCambioEstados", ex);
                        return null;
                    }
                }
            }
        }

        public AhorroVista ModificarCuota(AhorroVista pAhorroVista, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter numero = cmdTransaccionFactory.CreateParameter();
                        numero.ParameterName = "PNUMERO_CUENTA";
                        numero.Value = pAhorroVista.numero_cuenta;
                        numero.Direction = ParameterDirection.Input;
                        numero.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(numero);

                        DbParameter fecha = cmdTransaccionFactory.CreateParameter();
                        fecha.ParameterName = "PFECHA";
                        fecha.Value = pAhorroVista.fecha;
                        fecha.Direction = ParameterDirection.Input;
                        fecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(fecha);

                        DbParameter codusuario = cmdTransaccionFactory.CreateParameter();
                        codusuario.ParameterName = "PCODUSUARIO";
                        codusuario.Value = vUsuario.codusuario;
                        codusuario.Direction = ParameterDirection.Input;
                        codusuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(codusuario);

                        DbParameter p_val_anterior = cmdTransaccionFactory.CreateParameter();
                        p_val_anterior.ParameterName = "PVAL_ANTERIOR";
                        p_val_anterior.Value = pAhorroVista.valor_cuota_anterior.HasValue ? pAhorroVista.valor_cuota_anterior : (object)DBNull.Value;
                        p_val_anterior.Direction = ParameterDirection.Input;
                        p_val_anterior.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_val_anterior);

                        DbParameter p_val_nuevo = cmdTransaccionFactory.CreateParameter();
                        p_val_nuevo.ParameterName = "PVAL_NUEVO";
                        p_val_nuevo.Value = pAhorroVista.valor_cuota_anterior.HasValue ? pAhorroVista.valor_cuota : (object)DBNull.Value; ;
                        p_val_nuevo.Direction = ParameterDirection.Input;
                        p_val_nuevo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_val_nuevo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_AHOVIS_MOD_CUOTA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ModificarCuota", ex);
                        return null;
                    }
                }
            }
        }

        public AhorroVista TrasladoCuentas(AhorroVista pAhorroVista, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter numero_cuenta = cmdTransaccionFactory.CreateParameter();
                        numero_cuenta.ParameterName = "PNUMERO_CUENTA";
                        numero_cuenta.Value = pAhorroVista.numero_cuenta;
                        numero_cuenta.Direction = ParameterDirection.Input;
                        numero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(numero_cuenta);

                        DbParameter cod_persona = cmdTransaccionFactory.CreateParameter();
                        cod_persona.ParameterName = "PCOD_CLIENTE";
                        cod_persona.Value = pAhorroVista.cod_persona;
                        cod_persona.Direction = ParameterDirection.Input;
                        cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(cod_persona);

                        DbParameter cod_ope = cmdTransaccionFactory.CreateParameter();
                        cod_ope.ParameterName = "PCOD_OPE";
                        cod_ope.Value = pAhorroVista.cod_ope;
                        cod_ope.Direction = ParameterDirection.Input;
                        cod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(cod_ope);

                        DbParameter fecha_cierre = cmdTransaccionFactory.CreateParameter();
                        fecha_cierre.ParameterName = "PFECHA_RETIRO";
                        fecha_cierre.Value = pAhorroVista.fecha_cierre;
                        fecha_cierre.Direction = ParameterDirection.Input;
                        fecha_cierre.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(fecha_cierre);

                        DbParameter V_Traslado = cmdTransaccionFactory.CreateParameter();
                        V_Traslado.ParameterName = "PVALOR_RETIRO";
                        V_Traslado.Value = pAhorroVista.V_Traslado;
                        V_Traslado.Direction = ParameterDirection.Input;
                        V_Traslado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(V_Traslado);

                        DbParameter codusuario = cmdTransaccionFactory.CreateParameter();
                        codusuario.ParameterName = "PCOD_USUARIO";
                        codusuario.Value = pAhorroVista.codusuario;
                        codusuario.Direction = ParameterDirection.Input;
                        codusuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(codusuario);


                        DbParameter pformpago = cmdTransaccionFactory.CreateParameter();
                        pformpago.ParameterName = "PFORMA_PAGO";
                        pformpago.Value = 0;
                        pformpago.Direction = ParameterDirection.Input;
                        pformpago.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pformpago);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_RETIRO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "TrasladoCuentas", ex);
                        return null;
                    }
                }
            }
        }
        public AhorroVista IngresoCuenta(AhorroVista pAhorroVista, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter numero_cuenta = cmdTransaccionFactory.CreateParameter();
                        numero_cuenta.ParameterName = "PNUMERO_CUENTA";
                        numero_cuenta.Value = pAhorroVista.numero_cuenta;
                        numero_cuenta.Direction = ParameterDirection.Input;
                        numero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(numero_cuenta);

                        DbParameter cod_persona = cmdTransaccionFactory.CreateParameter();
                        cod_persona.ParameterName = "PCOD_CLIENTE";
                        cod_persona.Value = pAhorroVista.cod_persona;
                        cod_persona.Direction = ParameterDirection.Input;
                        cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(cod_persona);

                        DbParameter cod_ope = cmdTransaccionFactory.CreateParameter();
                        cod_ope.ParameterName = "PCOD_OPE";
                        cod_ope.Value = pAhorroVista.cod_ope;
                        cod_ope.Direction = ParameterDirection.Input;
                        cod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(cod_ope);

                        DbParameter fecha_cierre = cmdTransaccionFactory.CreateParameter();
                        fecha_cierre.ParameterName = "PFECHA_TRASLADO";
                        fecha_cierre.Value = pAhorroVista.fecha_cierre;
                        fecha_cierre.Direction = ParameterDirection.Input;
                        fecha_cierre.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(fecha_cierre);

                        DbParameter V_Traslado = cmdTransaccionFactory.CreateParameter();
                        V_Traslado.ParameterName = "PVALOR_TRASLADO";
                        V_Traslado.Value = pAhorroVista.V_Traslado;
                        V_Traslado.Direction = ParameterDirection.Input;
                        V_Traslado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(V_Traslado);

                        DbParameter codusuario = cmdTransaccionFactory.CreateParameter();
                        codusuario.ParameterName = "PCOD_USUARIO";
                        codusuario.Value = pAhorroVista.codusuario;
                        codusuario.Direction = ParameterDirection.Input;
                        codusuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(codusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_DEPOSITO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "IngresoCuenta", ex);
                        return null;
                    }
                }
            }
        }

        public AhorroVista retiroahorros(AhorroVista pAhorroVista, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter numero_cuenta = cmdTransaccionFactory.CreateParameter();
                        numero_cuenta.ParameterName = "PNUMERO_CUENTA";
                        numero_cuenta.Value = pAhorroVista.numero_cuenta;
                        numero_cuenta.Direction = ParameterDirection.Input;
                        numero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(numero_cuenta);

                        DbParameter cod_persona = cmdTransaccionFactory.CreateParameter();
                        cod_persona.ParameterName = "PCOD_CLIENTE";
                        cod_persona.Value = pAhorroVista.cod_persona;
                        cod_persona.Direction = ParameterDirection.Input;
                        cod_persona.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(cod_persona);

                        DbParameter cod_ope = cmdTransaccionFactory.CreateParameter();
                        cod_ope.ParameterName = "PCOD_OPE";
                        cod_ope.Value = pAhorroVista.cod_ope;
                        cod_ope.Direction = ParameterDirection.Input;
                        cod_ope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(cod_ope);

                        DbParameter fecha_cierre = cmdTransaccionFactory.CreateParameter();
                        fecha_cierre.ParameterName = "PFECHA_RETIRO";
                        fecha_cierre.Value = pAhorroVista.fecha_cierre;
                        fecha_cierre.Direction = ParameterDirection.Input;
                        fecha_cierre.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(fecha_cierre);

                        DbParameter V_Traslado = cmdTransaccionFactory.CreateParameter();
                        V_Traslado.ParameterName = "PVALOR_RETIRO";
                        V_Traslado.Value = pAhorroVista.V_Traslado;
                        V_Traslado.Direction = ParameterDirection.Input;
                        V_Traslado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(V_Traslado);

                        DbParameter codusuario = cmdTransaccionFactory.CreateParameter();
                        codusuario.ParameterName = "PCOD_USUARIO";
                        codusuario.Value = pAhorroVista.codusuario;
                        codusuario.Direction = ParameterDirection.Input;
                        codusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(codusuario);

                        DbParameter pformpago = cmdTransaccionFactory.CreateParameter();
                        pformpago.ParameterName = "PFORMA_PAGO";
                        pformpago.Value = pAhorroVista.forma_giro;
                        pformpago.Direction = ParameterDirection.Input;
                        pformpago.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pformpago);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_RETIRO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pAhorroVista, "ahorro_vista", vUsuario, Accion.Crear.ToString(), TipoAuditoria.AhorroVista, "Creacion de retiro de ahorros vista de cuenta con numero " + pAhorroVista.numero_cuenta); //REGISTRO DE AUDITORIA

                        return pAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "retiroahorros", ex);
                        return null;
                    }
                }
            }
        }

        public AhorroVista depositoAhorros(AhorroVista pAhorroVista, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter numero_cuenta = cmdTransaccionFactory.CreateParameter();
                        numero_cuenta.ParameterName = "PNUMERO_CUENTA";
                        numero_cuenta.Value = pAhorroVista.numero_cuenta;
                        numero_cuenta.Direction = ParameterDirection.Input;
                        numero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(numero_cuenta);

                        DbParameter cod_persona = cmdTransaccionFactory.CreateParameter();
                        cod_persona.ParameterName = "PCOD_CLIENTE";
                        cod_persona.Value = pAhorroVista.cod_persona;
                        cod_persona.Direction = ParameterDirection.Input;
                        cod_persona.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(cod_persona);

                        DbParameter cod_ope = cmdTransaccionFactory.CreateParameter();
                        cod_ope.ParameterName = "PCOD_OPE";
                        cod_ope.Value = pAhorroVista.cod_ope;
                        cod_ope.Direction = ParameterDirection.Input;
                        cod_ope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(cod_ope);

                        DbParameter fecha_cierre = cmdTransaccionFactory.CreateParameter();
                        fecha_cierre.ParameterName = "PFECHA_TRASLADO";
                        fecha_cierre.Value = pAhorroVista.fecha_cierre;
                        fecha_cierre.Direction = ParameterDirection.Input;
                        fecha_cierre.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(fecha_cierre);

                        DbParameter V_Traslado = cmdTransaccionFactory.CreateParameter();
                        V_Traslado.ParameterName = "PVALOR_TRASLADO";
                        V_Traslado.Value = pAhorroVista.V_Traslado;
                        V_Traslado.Direction = ParameterDirection.Input;
                        V_Traslado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(V_Traslado);

                        DbParameter codusuario = cmdTransaccionFactory.CreateParameter();
                        codusuario.ParameterName = "PCOD_USUARIO";
                        codusuario.Value = pAhorroVista.codusuario;
                        codusuario.Direction = ParameterDirection.Input;
                        codusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(codusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_DEPOSITO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "depositoAhorros", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarAhorroVista(String pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        AhorroVista pAhorroVista = new AhorroVista();
                        pAhorroVista = ConsultarAhorroVista(pId, vUsuario);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "p_numero_cuenta";
                        pnumero_cuenta.Value = pAhorroVista.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_AHORROVIS_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "EliminarAhorroVista", ex);
                    }
                }
            }
        }

        public AhorroVista ConsultarAhorroVista(String pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            AhorroVista entidad = new AhorroVista();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT T.NUMERO_TARJETA,Ahorro_Vista.*,Case Ahorro_vista.Estado when 0 then 'Apertura' when 1 then 'Activa' when 2 then 'Inactiva' "
                        + " when 3 then 'Bloqueada' when 4 then 'Cerrada'when 5 then 'Embargada' end as nomEstado,CALCULAR_CUOTA_AHORRO_VISTA( Ahorro_Vista.NUMERO_CUENTA) AS CUOTA_CALCULADA, v_persona.nombre,oficina.nombre as nom_oficina, "
                        + " lineaahorro.descripcion as nom_linea_ahorro, tipoidentificacion.descripcion ,v_persona.identificacion,V_PERSONA.TIPO_IDENTIFICACION, cuentas_exentas.idexenta as exenta,cuentas_exentas.fecha_exenta as fecha_exenta,cuentas_exentas.Fecha as FechaNOExenta, tm.descripcion As moneda, p.descripcion as periodicidad,p.numero_dias as dias"
                        + " FROM Ahorro_Vista LEFT JOIN V_persona ON Ahorro_Vista.cod_persona = V_persona.cod_persona "
                        + " LEFT JOIN oficina on oficina.cod_oficina = v_persona.cod_oficina "
                        + " LEFT JOIN tipoidentificacion on  v_persona.tipo_identificacion = tipoidentificacion.codtipoidentificacion "
                        + " LEFT JOIN cuentas_exentas  on  Ahorro_Vista.numero_cuenta = cuentas_exentas.numero_cuenta "
                        + " LEFT JOIN lineaahorro on lineaahorro.cod_linea_ahorro = ahorro_vista.cod_linea_ahorro "
                        + " LEFT JOIN tipomoneda tm on tm.cod_moneda = lineaahorro.cod_moneda "
                        + " LEFT JOIN periodicidad p on Ahorro_Vista.cod_periodicidad = p.cod_periodicidad "
                        + " LEFT JOIN Tarjeta T on T.NUMERO_CUENTA=Ahorro_Vista.NUMERO_CUENTA "
                        + " WHERE Ahorro_Vista.NUMERO_CUENTA = '" + pId.ToString() + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToInt32(resultado["MODALIDAD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                            if (resultado["COD_FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["COD_FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["CUOTA_CALCULADA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["CUOTA_CALCULADA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.estados = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOM_LINEA_AHORRO"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA_AHORRO"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identifi = Convert.ToInt32(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["EXENTA"] != DBNull.Value) entidad.exenta = Convert.ToString(resultado["EXENTA"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["DIAS"] != DBNull.Value) entidad.pdias = Convert.ToInt64(resultado["DIAS"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa_reca = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["fecha_exenta"] != DBNull.Value) entidad.fecha_exenta = Convert.ToDateTime(resultado["fecha_exenta"]);
                            if (resultado["FechaNOExenta"] != DBNull.Value) entidad.fechaNoExcenta = Convert.ToDateTime(resultado["FechaNOExenta"]);
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
                        BOExcepcion.Throw("AhorroVistaData", "ConsultarAhorroVista", ex);
                        return null;
                    }
                }
            }
        }

        public AhorroVista ConsultarAhorroVistaTraslado(String pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            AhorroVista entidad = new AhorroVista();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Ahorro_Vista.*,Case Ahorro_vista.Estado when 0 then 'Apertura' when 1 then 'Activa' when 2 then 'Inactiva' "
                        + "when 3 then 'Bloqueada' when 4 then 'Cerrada'when 5 then 'Embargada' end as nomEstado,v_persona.nombre,oficina.nombre as nom_oficina, "
                        + "lineaahorro.descripcion as nom_linea_ahorro, tipoidentificacion.descripcion ,v_persona.identificacion,V_PERSONA.TIPO_IDENTIFICACION, cuentas_exentas.numero_cuenta as exenta "
                        + "FROM Ahorro_Vista  LEFT JOIN V_persona ON Ahorro_Vista.cod_persona = V_persona.cod_persona "
                        + "LEFT JOIN oficina on oficina.cod_oficina=v_persona.cod_oficina "
                        + "LEFT JOIN tipoidentificacion on  v_persona.tipo_identificacion=tipoidentificacion.codtipoidentificacion "
                        + "LEFT JOIN cuentas_exentas  on  Ahorro_Vista.numero_cuenta= cuentas_exentas.numero_cuenta "
                        + "left join lineaahorro on lineaahorro.cod_linea_ahorro=ahorro_vista.cod_linea_ahorro  WHERE Ahorro_Vista.estado in(0,1) and Ahorro_Vista.NUMERO_CUENTA =  " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToInt32(resultado["MODALIDAD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                            if (resultado["COD_FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["COD_FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.estados = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOM_LINEA_AHORRO"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA_AHORRO"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identifi = Convert.ToInt32(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["EXENTA"] != DBNull.Value) entidad.exenta = Convert.ToString(resultado["EXENTA"]);

                        }
                        else
                        {
                            //throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ConsultarAhorroVistaTraslado", ex);
                        return null;
                    }
                }
            }
        }
        public List<AhorroVista> ListarTarjetas(String pId, Usuario vUsuario)
        {
            DbDataReader resultado;

            List<AhorroVista> lstTiposDocumento = new List<AhorroVista>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @" SELECT * FROM  Tarjeta T  WHERE T.NUMERO_CUENTA = '" + pId + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();
                            if (resultado["NUMERO_TARJETA"] != DBNull.Value) entidad.Num_Tarjeta = Convert.ToString(resultado["NUMERO_TARJETA"]);

                            lstTiposDocumento.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTiposDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVista", "ListarTarjetas", ex);
                        return null;
                    }
                }
            }
        }
        public List<AhorroVista> ConsultarMovimientosMasivos(AhorroVista pAhorrovista, Usuario vUsuario)
        {
            DbDataReader resultado;
            AhorroVista entidad = new AhorroVista();
            List<AhorroVista> lstentidad = new List<AhorroVista>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Ahorro_Vista.*,Case Ahorro_vista.Estado  when 1 then 'Activa'  end as nomEstado,v_persona.nombre,oficina.nombre as nom_oficina  ,lineaahorro.descripcion as nom_linea_ahorro, tipoidentificacion.descripcion ,v_persona.identificacion FROM Ahorro_Vista  LEFT JOIN V_persona ON Ahorro_Vista.cod_persona = V_persona.cod_persona LEFT JOIN oficina on oficina.cod_oficina=v_persona.cod_oficina LEFT JOIN tipoidentificacion on  v_persona.tipo_identificacion=tipoidentificacion.codtipoidentificacion  left join lineaahorro on lineaahorro.cod_linea_ahorro=ahorro_vista.cod_linea_ahorro  WHERE NUMERO_CUENTA =  1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToInt32(resultado["MODALIDAD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                            if (resultado["COD_FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["COD_FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.estados = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOM_LINEA_AHORRO"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA_AHORRO"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);

                            lstentidad.Add(entidad);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstentidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ConsultarMovimientosMasivos", ex);
                        return null;
                    }
                }
            }
        }

        public List<AhorroVista> ReporteGMF(AhorroVista pAhorroVista, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<AhorroVista> lstentidad = new List<AhorroVista>();
            Configuracion conf = new Configuracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql1 = "SELECT * from V_TRANSACCIONES_GMF  where Estado=0  and total>0 ";

                        if (pFechaIni != null && pFechaIni != DateTime.MinValue)
                        {
                            if (sql1.ToUpper().Contains("WHERE"))
                                sql1 += " And ";
                            else
                                sql1 += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql1 += " trunc(fecha_oper) >= To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql1 += " trunc(fecha_oper) >= '" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        if (pFechaFin != null && pFechaFin != DateTime.MinValue)
                        {
                            if (sql1.ToUpper().Contains("WHERE"))
                                sql1 += " And ";
                            else
                                sql1 += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql1 += " trunc(fecha_oper) <= To_Date('" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql1 += " trunc(fecha_oper) <= '" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        string sql = sql1 + " order by  fecha_oper asc ";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();

                            if (resultado["COD_OPE"] != DBNull.Value) entidad.numero_operacion = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["NUMERO_TRANSACCION"] != DBNull.Value) entidad.numero_transaccion = Convert.ToInt32(resultado["NUMERO_TRANSACCION"]);
                            if (resultado["TOTAL"] != DBNull.Value) entidad.valor_gmf = Convert.ToInt64(resultado["TOTAL"]);
                            // if (resultado["estados"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["estados"]);
                            if (resultado["FECHA_OPER"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_OPER"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nombre_producto = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_TRANSACCION"] != DBNull.Value) entidad.tipo_transaccion = Convert.ToString(resultado["TIPO_TRANSACCION"]);
                            if (resultado["VALOR_BASE"] != DBNull.Value) entidad.valor_base = Convert.ToInt64(resultado["VALOR_BASE"]);
                            if (resultado["PRODUCTO"] != DBNull.Value) entidad.nombre_producto = Convert.ToString(resultado["PRODUCTO"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombres"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["nombres"]);
                            if (resultado["apellidos"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["apellidos"]);

                            lstentidad.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstentidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ReporteGmf", ex);
                        return null;
                    }
                }
            }
        }


        public List<AhorroVista> ListaAhorroExtractos(AhorroVista pAhorroVista, Usuario vUsuario, String filtro)
        {
            DbDataReader resultado;
            List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
            Configuracion conf = new Configuracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT a.*, l.descripcion AS nom_linea_ahorro, o.nombre AS nom_oficina, p.identificacion, p.nombre,p.email,
                                       h.Fecha_Consulta, h.fecha_envio
                                       FROM Ahorro_Vista a
                                       INNER JOIN LineaAhorro l On a.cod_linea_ahorro = l.cod_linea_ahorro
                                       INNER JOIN Oficina o ON a.cod_oficina = o.cod_oficina
                                       INNER JOIN v_persona p ON a.cod_persona = p.cod_persona
                                       left join Historial_Notificacion h on p.Cod_Persona = h.Cod_Persona and h.codigo = 5
                                       where 1=1 " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["NOM_LINEA_AHORRO"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA_AHORRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToInt32(resultado["MODALIDAD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                            if (resultado["COD_FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["COD_FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.Email = resultado["EMAIL"].ToString();
                            if (resultado["Fecha_Consulta"] != DBNull.Value) entidad.fecha_consulta = Convert.ToDateTime(resultado["Fecha_Consulta"]);
                            if (resultado["fecha_envio"] != DBNull.Value) entidad.fecha_envio = Convert.ToDateTime(resultado["fecha_envio"]);

                            lstAhorroVista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListaAhorroExtractos", ex);
                        return null;
                    }
                }
            }
        }
        public List<AhorroVista> ListarAhorroVista(string pFiltro, DateTime pFechaApe, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT DISTINCT A.*, L.DESCRIPCION AS NOM_LINEA_AHORRO, O.NOMBRE AS NOM_OFICINA, P.IDENTIFICACION, P.NOMBRE, P.cod_nomina, "
                                        + "CASE A.ESTADO WHEN 0 THEN 'Apertura' WHEN 1 THEN 'Activo' WHEN 2 THEN 'Inactiva' WHEN 3 THEN 'Cerrada' WHEN 4 THEN 'Bloqueada'  WHEN 5 THEN 'Embargada' END AS NOMESTADO, "
                                        + "  case when exists (select * from TARJETA T where T.NUMERO_CUENTA=a.NUMERO_CUENTA) then 'S' else 'N' end as Tarjeta "
                                        + "FROM Ahorro_Vista a INNER JOIN LineaAhorro l On a.cod_linea_ahorro = l.cod_linea_ahorro "
                                        + "INNER JOIN Oficina o ON a.cod_oficina = o.cod_oficina "
                                        + "INNER JOIN V_PERSONA P ON A.COD_PERSONA = P.COD_PERSONA "
                                        + " Left Join TARJETA T ON T.NUMERO_CUENTA=a.NUMERO_CUENTA "
                                        + pFiltro;

                        if (pFechaApe != null && pFechaApe != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " AND ";
                            else
                                sql += " WHERE ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " A.FECHA_APERTURA = To_Date('" + Convert.ToDateTime(pFechaApe).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " A.FECHA_APERTURA = '" + Convert.ToDateTime(pFechaApe).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        //sql += "  and rownum=1 ";
                        sql += " ORDER BY A.NUMERO_CUENTA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["NOM_LINEA_AHORRO"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA_AHORRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToInt32(resultado["MODALIDAD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                            if (resultado["COD_FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["COD_FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["Tarjeta"] != DBNull.Value) entidad.Tarjeta = Convert.ToString(resultado["Tarjeta"]);
                            //if (resultado["NUMERO_TARJETA"] != DBNull.Value) entidad.Num_Tarjeta = Convert.ToString(resultado["NUMERO_TARJETA"]);
                            lstAhorroVista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarAhorroVista", ex);
                        return null;
                    }
                }
            }
        }



        public List<AhorroVista> ReportePeriodico(AhorroVista pAhorroVista, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AhorroVista> lstProvision = new List<AhorroVista>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter cod_linea_ahorro = cmdTransaccionFactory.CreateParameter();
                        cod_linea_ahorro.ParameterName = "PCODLINEAAHORRO";
                        cod_linea_ahorro.Direction = ParameterDirection.Input;
                        cod_linea_ahorro.Value = pAhorroVista.cod_linea_ahorro;
                        cod_linea_ahorro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(cod_linea_ahorro);

                        DbParameter fecha_apertura = cmdTransaccionFactory.CreateParameter();
                        fecha_apertura.ParameterName = "PFECHAINICIO";
                        fecha_apertura.Value = pAhorroVista.fecha_apertura;
                        fecha_apertura.Direction = ParameterDirection.Input;
                        fecha_apertura.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(fecha_apertura);

                        DbParameter fecha_cierre = cmdTransaccionFactory.CreateParameter();
                        fecha_cierre.ParameterName = "PFECHAFINAL";
                        fecha_cierre.Direction = ParameterDirection.Input;
                        fecha_cierre.Value = pAhorroVista.fecha_cierre;
                        fecha_cierre.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(fecha_cierre);

                        DbParameter cod_oficina = cmdTransaccionFactory.CreateParameter();
                        cod_oficina.ParameterName = "PCODOFICINA";
                        cod_oficina.Direction = ParameterDirection.Input;
                        if (pAhorroVista.cod_oficina != null) cod_oficina.Value = pAhorroVista.cod_oficina; else cod_oficina.Value = DBNull.Value;
                        cod_oficina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(cod_oficina);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_REPORTEPER";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select * From TEMP_REPORTEAHO Order by numero_cuenta";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();

                            if (resultado["numero_cuenta"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["numero_cuenta"].ToString());
                            if (resultado["fecha_apertura"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["fecha_apertura"].ToString());
                            if (resultado["saldo_inicial"] != DBNull.Value) entidad.saldo_inicial = Convert.ToInt64(resultado["saldo_inicial"]);
                            if (resultado["saldo_final"] != DBNull.Value) entidad.saldo_final = Convert.ToInt64(resultado["saldo_final"]);
                            if (resultado["retencion"] != DBNull.Value) entidad.retencion = Convert.ToInt64(resultado["retencion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["nombre"]);
                            if (resultado["cod_linea_ahorro"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["cod_linea_ahorro"]);
                            if (resultado["intereses"] != DBNull.Value) entidad.interes = Convert.ToDecimal(resultado["intereses"]);
                            if (resultado["oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["oficina"]);
                            if (resultado["depositos"] != DBNull.Value) entidad.deposito = Convert.ToInt64(resultado["depositos"]);
                            if (resultado["retiros"] != DBNull.Value) entidad.retiro = Convert.ToInt64(resultado["retiros"]);



                            lstProvision.Add(entidad);
                        }
                        return lstProvision;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ReportePeriodico", ex);
                        return null;
                    }
                }
            }
        }



        public List<AhorroVista> FondoLiquidez(AhorroVista pAhorroVista, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AhorroVista> lstFondo = new List<AhorroVista>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {



                        DbParameter Mes = cmdTransaccionFactory.CreateParameter();
                        Mes.ParameterName = "PMES";
                        Mes.Direction = ParameterDirection.Input;
                        Mes.Value = pAhorroVista.Mes;
                        Mes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(Mes);


                        DbParameter Anio = cmdTransaccionFactory.CreateParameter();
                        Anio.ParameterName = "pANIO";
                        Anio.Direction = ParameterDirection.Input;
                        Anio.Value = pAhorroVista.Anio;
                        Anio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(Anio);


                        DbParameter Fondo = cmdTransaccionFactory.CreateParameter();
                        Fondo.ParameterName = "PPORCENTAJE";
                        Fondo.Direction = ParameterDirection.Input;
                        Fondo.Value = pAhorroVista.Fondo;
                        Fondo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(Fondo);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_FONDOLIQUIDEZ";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select * From TEMP_FONDOLIQUIDEZ";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo_total = Convert.ToInt32(resultado["SALDO"]);



                            lstFondo.Add(entidad);
                        }
                        return lstFondo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "FondoLiquidez", ex);
                        return null;
                    }
                }
            }
        }


        public List<provision_ahorro> ListarProvision(DateTime pFechaIni, provision_ahorro pAhorroVista, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<provision_ahorro> lstProvision = new List<provision_ahorro>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        DbParameter fecha_apertura = cmdTransaccionFactory.CreateParameter();
                        fecha_apertura.ParameterName = "PFECHA";
                        fecha_apertura.Direction = ParameterDirection.Input;
                        if (pFechaIni != DateTime.MinValue) fecha_apertura.Value = pFechaIni; else fecha_apertura.Value = DBNull.Value;
                        fecha_apertura.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(fecha_apertura);


                        DbParameter cod_linea_ahorro = cmdTransaccionFactory.CreateParameter();
                        cod_linea_ahorro.ParameterName = "PCODLINEAHORRO";
                        cod_linea_ahorro.Direction = ParameterDirection.Input;
                        cod_linea_ahorro.Value = pAhorroVista.cod_linea_ahorro;
                        cod_linea_ahorro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(cod_linea_ahorro);

                        DbParameter cod_oficina = cmdTransaccionFactory.CreateParameter();
                        cod_oficina.ParameterName = "PCODOFICINA";
                        cod_oficina.Direction = ParameterDirection.Input;
                        cod_oficina.Value = pAhorroVista.cod_oficina;
                        cod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(cod_oficina);

                        DbParameter codusuario = cmdTransaccionFactory.CreateParameter();
                        codusuario.ParameterName = "PCODUSUARIO";
                        codusuario.Direction = ParameterDirection.Input;
                        codusuario.Value = vUsuario.codusuario;
                        codusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(codusuario);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_PROVISION";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select * From TEMP_PROVISIONAHO ";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            provision_ahorro entidad = new provision_ahorro();

                            if (resultado["numero_cuenta"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["numero_cuenta"].ToString());
                            if (resultado["fecha_apertura"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["fecha_apertura"].ToString());
                            if (resultado["saldo_total"] != DBNull.Value) entidad.saldo_total = Convert.ToInt32(resultado["saldo_total"]);
                            if (resultado["saldo_base"] != DBNull.Value) entidad.saldo_base = Convert.ToInt32(resultado["saldo_base"]);
                            if (resultado["provision_interes"] != DBNull.Value) entidad.intereses = Convert.ToInt32(resultado["provision_interes"]);
                            if (resultado["retencion"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["retencion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["cod_oficina"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["nombre"]);
                            if (resultado["cod_linea_ahorro"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["cod_linea_ahorro"]);
                            if (resultado["tasa"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["tasa"]);
                            if (resultado["dias"] != DBNull.Value) entidad.dias = Convert.ToInt32(resultado["dias"]);
                            if (resultado["valor_acumulado"] != DBNull.Value) entidad.valor_acumulado = Convert.ToInt32(resultado["valor_acumulado"]);

                            lstProvision.Add(entidad);
                        }
                        return lstProvision;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarProvision", ex);
                        return null;
                    }
                }
            }
        }

        public Xpinn.Comun.Entities.Cierea Crearcierea(Xpinn.Comun.Entities.Cierea pcierea, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter fecha = cmdTransaccionFactory.CreateParameter();
                        fecha.ParameterName = "p_fecha";
                        fecha.Value = pcierea.fecha;
                        fecha.Direction = ParameterDirection.Input;
                        fecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(fecha);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pcierea.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pcierea.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcampo1 = cmdTransaccionFactory.CreateParameter();
                        pcampo1.ParameterName = "p_campo1";
                        pcampo1.Value = pcierea.campo1;
                        pcampo1.Direction = ParameterDirection.Input;
                        pcampo1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcampo1);

                        DbParameter pcampo2 = cmdTransaccionFactory.CreateParameter();
                        pcampo2.ParameterName = "p_campo2";
                        if (pcierea.campo2 == null)
                            pcampo2.Value = DBNull.Value;
                        else
                            pcampo2.Value = pcierea.campo2;
                        pcampo2.Direction = ParameterDirection.Input;
                        pcampo2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcampo2);

                        DbParameter pfecrea = cmdTransaccionFactory.CreateParameter();
                        pfecrea.ParameterName = "p_fecrea";
                        if (pcierea.fecrea == DateTime.MinValue)
                            pfecrea.Value = DBNull.Value;
                        else
                            pfecrea.Value = pcierea.fecrea;
                        pfecrea.Direction = ParameterDirection.Input;
                        pfecrea.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecrea);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        if (pcierea.codusuario == null)
                            pcodusuario.Value = DBNull.Value;
                        else
                            pcodusuario.Value = pcierea.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_CIEREA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pcierea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ciereaData", "Crearcierea", ex);
                        return null;
                    }
                }
            }
        }

        public AhorroVista enviarfondoliquidez(AhorroVista pahorrovista, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidfondoliq = cmdTransaccionFactory.CreateParameter();
                        pidfondoliq.ParameterName = "p_idfondoliq";
                        pidfondoliq.Value = pahorrovista.idfondoliq;
                        pidfondoliq.Direction = ParameterDirection.Input;
                        pidfondoliq.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidfondoliq);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pahorrovista.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pmes = cmdTransaccionFactory.CreateParameter();
                        pmes.ParameterName = "p_mes";
                        pmes.Value = pahorrovista.Mes;
                        pmes.Direction = ParameterDirection.Input;
                        pmes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmes);

                        DbParameter panio = cmdTransaccionFactory.CreateParameter();
                        panio.ParameterName = "p_anio";
                        panio.Value = pahorrovista.Anio;
                        panio.Direction = ParameterDirection.Input;
                        panio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(panio);

                        DbParameter pnumero_dias = cmdTransaccionFactory.CreateParameter();
                        pnumero_dias.ParameterName = "p_numero_dias";
                        if (pahorrovista.dias == null)
                            pnumero_dias.Value = DBNull.Value;
                        else
                            pnumero_dias.Value = pahorrovista.dias;
                        pnumero_dias.Direction = ParameterDirection.Input;
                        pnumero_dias.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_dias);

                        DbParameter psaldo_promedio = cmdTransaccionFactory.CreateParameter();
                        psaldo_promedio.ParameterName = "p_saldo_promedio";
                        if (pahorrovista.saldo_promedio == null)
                            psaldo_promedio.Value = DBNull.Value;
                        else
                            psaldo_promedio.Value = pahorrovista.saldo_promedio;
                        psaldo_promedio.Direction = ParameterDirection.Input;
                        psaldo_promedio.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_promedio);

                        DbParameter pvalor_fondo = cmdTransaccionFactory.CreateParameter();
                        pvalor_fondo.ParameterName = "p_valor_fondo";
                        if (pahorrovista.valor_fondo == null)
                            pvalor_fondo.Value = DBNull.Value;
                        else
                            pvalor_fondo.Value = pahorrovista.valor_fondo;
                        pvalor_fondo.Direction = ParameterDirection.Input;
                        pvalor_fondo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_fondo);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = vUsuario.codusuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_FONDOLIQUI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pahorrovista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ahorrovistaData", "enviarfondoliquidez", ex);
                        return null;
                    }
                }
            }
        }

        public provision_ahorro InsertarDatos(provision_ahorro Insertar_cuenta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        DbParameter fecha_cierre = cmdTransaccionFactory.CreateParameter();
                        fecha_cierre.ParameterName = "p_fecha_causacion";
                        fecha_cierre.Value = Insertar_cuenta.fecha;
                        fecha_cierre.Direction = ParameterDirection.Input;
                        fecha_cierre.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(fecha_cierre);

                        DbParameter cod_ope = cmdTransaccionFactory.CreateParameter();
                        cod_ope.ParameterName = "p_cod_ope";
                        cod_ope.Value = Insertar_cuenta.cod_ope;
                        cod_ope.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(cod_ope);


                        DbParameter numero_cuenta = cmdTransaccionFactory.CreateParameter();
                        numero_cuenta.ParameterName = "p_numero_cuenta";
                        numero_cuenta.Value = Insertar_cuenta.numero_cuenta;
                        numero_cuenta.Direction = ParameterDirection.Input;
                        numero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(numero_cuenta);

                        DbParameter saldo_base = cmdTransaccionFactory.CreateParameter();
                        saldo_base.ParameterName = "p_saldo_base";
                        saldo_base.Value = Insertar_cuenta.saldo_base;
                        saldo_base.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(saldo_base);

                        DbParameter interes = cmdTransaccionFactory.CreateParameter();
                        interes.ParameterName = "p_int_causados";
                        interes.Value = Insertar_cuenta.intereses;
                        interes.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(interes);


                        DbParameter retencion = cmdTransaccionFactory.CreateParameter();
                        retencion.ParameterName = "p_retencion";
                        retencion.Value = Insertar_cuenta.retencion;
                        retencion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(retencion);


                        DbParameter dias = cmdTransaccionFactory.CreateParameter();
                        dias.ParameterName = "p_dias_causados";
                        dias.Value = Insertar_cuenta.dias;
                        dias.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(dias);

                        DbParameter p_tasa = cmdTransaccionFactory.CreateParameter();
                        p_tasa.ParameterName = "p_tasa";
                        p_tasa.Value = Insertar_cuenta.tasa_interes;
                        p_tasa.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_tasa);


                        DbParameter p_valor_acumulado = cmdTransaccionFactory.CreateParameter();
                        p_valor_acumulado.ParameterName = "p_valor_acumulado";
                        p_valor_acumulado.Value = Insertar_cuenta.valor_acumulado;
                        p_valor_acumulado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_valor_acumulado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_CAUS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        //Insertar_cuenta.idprovision = Convert.ToInt32(pidprovision.Value);

                        return Insertar_cuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ciereaData", "InsertarDatos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Asignacion de libretas a cuentas que requeiren Libretas 
        /// </summary>
        public List<ELibretas> getAllLista(String pfiltro, Usuario pUsuario)
        {
            List<ELibretas> ListaLibreta = new List<ELibretas>();

            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select l.numero_libreta,l.desde,l.IDLIBRETA, l.valor_libreta,l.hasta,o.nombre as NombreP,l.fecha_asignacion,a.numero_cuenta, "
                                       + "p.identificacion, p.nombre, p.cod_nomina, a.saldo_total,l.estado,a.cod_oficina "
                                        + " from libreta_ahorros l Inner Join ahorro_vista a On l.numero_cuenta = a.numero_cuenta "
                                        + " Inner Join v_persona p On a.cod_persona = p.cod_persona inner join oficina o on a.cod_oficina = o.cod_oficina " + pfiltro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ELibretas entidad = new ELibretas();
                            if (resultado["IDLIBRETA"] != DBNull.Value) entidad.id_Libreta = Convert.ToInt64(resultado["IDLIBRETA"]);
                            if (resultado["numero_libreta"] != DBNull.Value) entidad.numero_libreta = Convert.ToInt64(resultado["numero_libreta"]);
                            if (resultado["desde"] != DBNull.Value) entidad.desde = Convert.ToInt64(resultado["desde"]);
                            if (resultado["valor_libreta"] != DBNull.Value) entidad.valor_libreta = Convert.ToInt64(resultado["valor_libreta"]);
                            if (resultado["hasta"] != DBNull.Value) entidad.hasta = Convert.ToInt64(resultado["hasta"]);
                            if (resultado["NombreP"] != DBNull.Value) entidad.nombreoficina = Convert.ToString(resultado["NombreP"]);
                            if (resultado["fecha_asignacion"] != DBNull.Value) entidad.fecha_asignacion = Convert.ToDateTime(resultado["fecha_asignacion"]);
                            if (resultado["numero_cuenta"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["numero_cuenta"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["cod_nomina"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["nombre"]);
                            if (resultado["saldo_total"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["saldo_total"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["estado"]);

                            ListaLibreta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return ListaLibreta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "getAllLista", ex);
                        return null;
                    }
                }
            }
        }

        public List<ELibretas> llenarListaNuevo(DateTime pfecha, String pfiltro, Usuario pUsuario)
        {
            List<ELibretas> ListaLibreta = new List<ELibretas>();

            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select a.numero_cuenta,a.fecha_apertura,l.descripcion,v.identificacion,v.nombre,  a.saldo_total,a.estado "
                                       + "from lineaahorro l inner join ahorro_vista a "
                                       + " on l.cod_linea_ahorro = a.cod_linea_ahorro inner join "
                                       + " v_persona v on a.cod_persona = v.cod_persona inner join oficina o on v.cod_oficina=o.cod_oficina "
                                       + " where l.requiere_libreta=1 and a.estado = 0 " + pfiltro;

                        Configuracion conf = new Configuracion();
                        if (pfecha != null && pfecha != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " a.fecha_apertura = To_Date('" + Convert.ToDateTime(pfecha).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " a.fecha_apertura = '" + Convert.ToDateTime(pfecha).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ELibretas entidad = new ELibretas();
                            if (resultado["numero_cuenta"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["numero_cuenta"]);
                            if (resultado["fecha_apertura"] != DBNull.Value) entidad.fecha_asignacion = Convert.ToDateTime(resultado["fecha_apertura"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["descripcion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["saldo_total"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["saldo_total"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["estado"]);

                            ListaLibreta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return ListaLibreta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "llenarListaNuevo", ex);
                        return null;
                    }
                }
            }
        }

        public ELibretas getLibretaByIdLibreta(Int64 idCodigo, Usuario pusuario)
        {
            ELibretas entidad = new ELibretas();

            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select a.numero_cuenta, a.fecha_apertura, la.descripcion, v.identificacion, v.nombre, a.saldo_total,Case a.estado when 0 then 'Apertura' when 1 then 'Activa' when 2 then 'Inactiva' "
                                    + " when 3 then 'Bloqueada' when 4 then 'Cerrada'when 5 then 'Embargada' end as nomEstado ,Case l.estado when 0 then 'Apertura' when 1 then 'Activa' when 2 then 'Inactiva' "
                                    + " when 3 then 'Bloqueada' when 4 then 'Cerrada'when 5 then 'Embargada' end as nomEstadoLibreta, "
                                   + "  ti.descripcion as tipoId, l.numero_libreta, l.estado,la.NUM_DESPRENDIBLES_LIB, l.IDLIBRETA ,l.fecha_asignacion,l.desde,l.hasta,l.estado as estado_libre"
                                   + " from  lineaahorro la  inner join ahorro_vista a "
                                   + " on la.cod_linea_ahorro = a.cod_linea_ahorro inner join libreta_ahorros l "
                                   + " On l.numero_cuenta = a.numero_cuenta inner join  v_persona v "
                                   + " on a.cod_persona = a.cod_persona inner join TIPOIDENTIFICACION ti "
                                   + " on v.tipo_identificacion = ti.codtipoidentificacion "
                                   + " where la.requiere_libreta =1 and a.estado = 0 and l.idlibreta =" + idCodigo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["numero_cuenta"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["numero_cuenta"]);
                            if (resultado["fecha_asignacion"] != DBNull.Value) entidad.fecha_asignacion = Convert.ToDateTime(resultado["fecha_asignacion"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["descripcion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["saldo_total"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["saldo_total"]);
                            if (resultado["nomEstado"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["nomEstado"]);
                            if (resultado["tipoId"] != DBNull.Value) entidad.TipoIdentific = Convert.ToString(resultado["tipoId"]);
                            if (resultado["numero_libreta"] != DBNull.Value) entidad.numero_libreta = Convert.ToInt64(resultado["numero_libreta"]);
                            if (resultado["NUM_DESPRENDIBLES_LIB"] != DBNull.Value) entidad.Num_Desprendible = Convert.ToInt64(resultado["NUM_DESPRENDIBLES_LIB"]);
                            if (resultado["IDLIBRETA"] != DBNull.Value) entidad.id_Libreta = Convert.ToInt64(resultado["IDLIBRETA"]);
                            if (resultado["fecha_apertura"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["fecha_apertura"]);
                            if (resultado["desde"] != DBNull.Value) entidad.desde = Convert.ToInt32(resultado["desde"]);
                            if (resultado["hasta"] != DBNull.Value) entidad.hasta = Convert.ToInt32(resultado["hasta"]);
                            if (resultado["nomEstadoLibreta"] != DBNull.Value) entidad.nom_estado_libreta = Convert.ToString(resultado["nomEstadoLibreta"]);
                            if (resultado["estado_libre"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["estado_libre"]);
                            if (resultado["tipoId"] != DBNull.Value) entidad.TipoIdentific = Convert.ToString(resultado["tipoId"]);



                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "getLibretaByIdLibreta", ex);
                        return null;
                    }
                }
            }
        }

        public ELibretas getLibretaByNumeroCuenta(String codigo, Usuario pusuario)
        {
            ELibretas entidad = new ELibretas();

            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"select a.numero_cuenta,a.fecha_apertura,la.descripcion,v.identificacion,v.nombre,  a.saldo_total,a.estado ,ti.descripcion as tipoId, "
                                       + "  l.numero_libreta, l.estado,la.NUM_DESPRENDIBLES_LIB,l.IDLIBRETA "
                                       + "from  lineaahorro la  inner join ahorro_vista a "
                                       + " on la.cod_linea_ahorro = a.cod_linea_ahorro left join libreta_ahorros l On l.numero_cuenta = a.numero_cuenta inner join "
                                       + " v_persona v on a.cod_persona = v.cod_persona inner join TIPOIDENTIFICACION ti on v.tipo_identificacion = ti.codtipoidentificacion "
                                       + " where la.requiere_libreta=1 and a.estado = 0  and a.numero_cuenta =" + codigo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["numero_cuenta"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["numero_cuenta"]);
                            if (resultado["fecha_apertura"] != DBNull.Value) entidad.fecha_asignacion = Convert.ToDateTime(resultado["fecha_apertura"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["descripcion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["saldo_total"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["saldo_total"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["estado"]);
                            if (resultado["tipoId"] != DBNull.Value) entidad.TipoIdentific = Convert.ToString(resultado["tipoId"]);
                            if (resultado["numero_libreta"] != DBNull.Value) entidad.numero_libreta = Convert.ToInt64(resultado["numero_libreta"]);
                            if (resultado["NUM_DESPRENDIBLES_LIB"] != DBNull.Value) entidad.Num_Desprendible = Convert.ToInt64(resultado["NUM_DESPRENDIBLES_LIB"]);
                            if (resultado["IDLIBRETA"] != DBNull.Value) entidad.id_Libreta = Convert.ToInt64(resultado["IDLIBRETA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "getLibretaByNumeroCuenta", ex);
                        return null;
                    }
                }
            }

        }

        public String validarEliminar(Usuario pUsuario, Int64 id)
        {
            String respuesta = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idlibreta = cmdTransaccionFactory.CreateParameter();
                        p_idlibreta.ParameterName = "p_idlibreta";
                        p_idlibreta.Value = id;
                        p_idlibreta.Direction = ParameterDirection.Input;
                        p_idlibreta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idlibreta);

                        DbParameter P_VALIDO = cmdTransaccionFactory.CreateParameter();
                        P_VALIDO.ParameterName = "P_VALIDO";
                        P_VALIDO.Value = respuesta;
                        P_VALIDO.Direction = ParameterDirection.Output;
                        P_VALIDO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_VALIDO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_VALIDA_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return respuesta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "validarEliminar", ex);
                        return null;
                    }
                }
            }
        }

        public void eliminarLibreta(Int64 pidLibreta, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idlibreta = cmdTransaccionFactory.CreateParameter();
                        p_idlibreta.ParameterName = "p_idlibreta";
                        p_idlibreta.Value = pidLibreta;
                        p_idlibreta.Direction = ParameterDirection.Input;
                        p_idlibreta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idlibreta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_LIBRETA_AH_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "eliminarLibreta", ex);
                    }
                }
            }
        }

        public void InsertarLibreta(Usuario pUsuario, ELibretas pElibreta, Int64 pidMotivo)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idlibreta = cmdTransaccionFactory.CreateParameter();
                        p_idlibreta.ParameterName = "p_idlibreta";
                        p_idlibreta.Value = pElibreta.id_Libreta;
                        p_idlibreta.Direction = ParameterDirection.Output;
                        p_idlibreta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idlibreta);

                        DbParameter p_numero_libreta = cmdTransaccionFactory.CreateParameter();
                        p_numero_libreta.ParameterName = "p_numero_libreta";
                        p_numero_libreta.Value = pElibreta.numero_libreta;
                        p_numero_libreta.Direction = ParameterDirection.Input;
                        p_numero_libreta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numero_libreta);

                        DbParameter p_numero_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_numero_cuenta.ParameterName = "p_numero_cuenta";
                        p_numero_cuenta.Value = pElibreta.numero_cuenta;
                        p_numero_cuenta.Direction = ParameterDirection.Input;
                        p_numero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_numero_cuenta);

                        DbParameter p_fecha_asignacion = cmdTransaccionFactory.CreateParameter();
                        p_fecha_asignacion.ParameterName = "p_fecha_asignacion";
                        p_fecha_asignacion.Value = pElibreta.fecha_asignacion;
                        p_fecha_asignacion.Direction = ParameterDirection.Input;
                        p_fecha_asignacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_asignacion);

                        DbParameter p_desde = cmdTransaccionFactory.CreateParameter();
                        p_desde.ParameterName = "p_desde";
                        p_desde.Value = pElibreta.desde;
                        p_desde.Direction = ParameterDirection.Input;
                        p_desde.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_desde);

                        DbParameter p_hasta = cmdTransaccionFactory.CreateParameter();
                        p_hasta.ParameterName = "p_hasta";
                        p_hasta.Value = pElibreta.hasta;
                        p_hasta.Direction = ParameterDirection.Input;
                        p_hasta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_hasta);

                        DbParameter p_actual = cmdTransaccionFactory.CreateParameter();
                        p_actual.ParameterName = "p_actual";
                        p_actual.Value = pElibreta.desde;
                        p_actual.Direction = ParameterDirection.Input;
                        p_actual.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_actual);

                        DbParameter p_cod_motivo = cmdTransaccionFactory.CreateParameter();
                        p_cod_motivo.ParameterName = "p_cod_motivo";
                        p_cod_motivo.Value = pidMotivo;
                        p_cod_motivo.Direction = ParameterDirection.Input;
                        p_cod_motivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_motivo);

                        DbParameter p_valor_libreta = cmdTransaccionFactory.CreateParameter();
                        p_valor_libreta.ParameterName = "p_valor_libreta";
                        p_valor_libreta.Value = pElibreta.valor_libreta;
                        p_valor_libreta.Direction = ParameterDirection.Input;
                        p_valor_libreta.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor_libreta);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pElibreta.estado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_LIBRETA_AH_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "InsertarLibreta", ex);
                    }
                }
            }
        }

        public void actEstadoAnterior(Usuario pUsuario, Int64 paIdLibreta)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdLibreta = cmdTransaccionFactory.CreateParameter();
                        pIdLibreta.ParameterName = "pIdLibreta";
                        pIdLibreta.Value = paIdLibreta;
                        pIdLibreta.Direction = ParameterDirection.Input;
                        pIdLibreta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pIdLibreta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_LIBRETA_AH_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "actEstadoAnterior", ex);
                    }
                }
            }
        }

        public decimal consultar(Int64 codigo, String numeroCuenta, Usuario pUsuario)
        {
            DbDataReader resultado;
            String query = "";
            decimal valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        switch (codigo)
                        {
                            case 0:
                                query = @"select l.valor_libreta  from ahorro_vista a inner join  lineaahorro l on a.cod_linea_ahorro = l.cod_linea_ahorro "
                                        + "where l.cobra_primera_libreta= " + codigo + " and a.numero_cuenta= " + numeroCuenta;
                                break;
                            case 1:
                                query = @"select l.valor_libreta  from ahorro_vista a inner join  lineaahorro l on a.cod_linea_ahorro = l.cod_linea_ahorro "
                                        + "where l.cobra_primera_libreta= " + codigo + " and a.numero_cuenta= " + numeroCuenta;
                                break;
                            case 2:
                                query = @"select l.valor_libreta  from ahorro_vista a inner join  lineaahorro l on a.cod_linea_ahorro = l.cod_linea_ahorro "
                                        + "where l.COBRA_PERDIDA_LIBRETA= " + codigo + " and a.numero_cuenta= " + numeroCuenta;
                                break;
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                            if (resultado["valor_libreta"] != DBNull.Value)
                                valor = Convert.ToDecimal(resultado["valor_libreta"]);

                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "consultar", ex);
                        return 0;
                    }
                }
            }
        }

        public Int64 getNumeroDesprendible(Usuario pUsuario, String numeroCuenta)
        {
            DbDataReader resultado;
            String query = "";
            Int64 valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        query = @"select l.num_desprendibles_lib "
                               + " from ahorro_vista a inner join  lineaahorro l on a.cod_linea_ahorro = l.cod_linea_ahorro where a.numero_cuenta = " + numeroCuenta;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                            if (resultado["num_desprendibles_lib"] != DBNull.Value)
                                valor = Convert.ToInt64(resultado["num_desprendibles_lib"]);

                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "getNumeroDesprendible", ex);
                        return -1;
                    }
                }
            }
        }

        public void updateLibreta(Usuario pUsuario, ELibretas pElibreta, Int64 idMotivo)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idlibreta = cmdTransaccionFactory.CreateParameter();
                        p_idlibreta.ParameterName = "p_idlibreta";
                        p_idlibreta.Value = pElibreta.id_Libreta;
                        p_idlibreta.Direction = ParameterDirection.Input;
                        p_idlibreta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idlibreta);

                        DbParameter p_numero_libreta = cmdTransaccionFactory.CreateParameter();
                        p_numero_libreta.ParameterName = "p_numero_libreta";
                        p_numero_libreta.Value = pElibreta.numero_libreta;
                        p_numero_libreta.Direction = ParameterDirection.Input;
                        p_numero_libreta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numero_libreta);

                        DbParameter p_numero_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_numero_cuenta.ParameterName = "p_numero_cuenta";
                        p_numero_cuenta.Value = pElibreta.numero_cuenta;
                        p_numero_cuenta.Direction = ParameterDirection.Input;
                        p_numero_cuenta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numero_cuenta);

                        DbParameter p_fecha_asignacion = cmdTransaccionFactory.CreateParameter();
                        p_fecha_asignacion.ParameterName = "p_fecha_asignacion";
                        p_fecha_asignacion.Value = pElibreta.fecha_asignacion;
                        p_fecha_asignacion.Direction = ParameterDirection.Input;
                        p_fecha_asignacion.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_asignacion);

                        DbParameter p_desde = cmdTransaccionFactory.CreateParameter();
                        p_desde.ParameterName = "p_desde";
                        p_desde.Value = pElibreta.desde;
                        p_desde.Direction = ParameterDirection.Input;
                        p_desde.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_desde);


                        DbParameter p_hasta = cmdTransaccionFactory.CreateParameter();
                        p_hasta.ParameterName = "p_hasta";
                        p_hasta.Value = pElibreta.hasta;
                        p_hasta.Direction = ParameterDirection.Input;
                        p_hasta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_hasta);

                        DbParameter p_actual = cmdTransaccionFactory.CreateParameter();
                        p_actual.ParameterName = "p_actual";
                        p_actual.Value = pElibreta.desde;
                        p_actual.Direction = ParameterDirection.Input;
                        p_actual.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_actual);

                        DbParameter p_cod_motivo = cmdTransaccionFactory.CreateParameter();
                        p_cod_motivo.ParameterName = "p_cod_motivo";
                        p_cod_motivo.Value = idMotivo;
                        p_cod_motivo.Direction = ParameterDirection.Input;
                        p_cod_motivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_motivo);

                        DbParameter p_valor_libreta = cmdTransaccionFactory.CreateParameter();
                        p_valor_libreta.ParameterName = "p_valor_libreta";
                        p_valor_libreta.Value = pElibreta.valor_libreta;
                        p_valor_libreta.Direction = ParameterDirection.Input;
                        p_valor_libreta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valor_libreta);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pElibreta.estado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_LIBRETA_AH_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "updateLibreta", ex);
                    }
                }
            }
        }


        public AhorroVista  ciCierreHistorico(AhorroVista pEntidad, string valor, DateTime fechas, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            serror = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter PFECHA = cmdTransaccionFactory.CreateParameter();
                        PFECHA.ParameterName = "PFECHA";
                        PFECHA.Value = fechas;
                        PFECHA.DbType = DbType.Date;

                        DbParameter PESTADO = cmdTransaccionFactory.CreateParameter();
                        PESTADO.ParameterName = "PESTADO";
                        PESTADO.Value = valor;

                        DbParameter PUSUARIO = cmdTransaccionFactory.CreateParameter();
                        PUSUARIO.ParameterName = "PUSUARIO";
                        PUSUARIO.Value = cod_usuario;

                        cmdTransaccionFactory.Parameters.Add(PFECHA);
                        cmdTransaccionFactory.Parameters.Add(PESTADO);
                        cmdTransaccionFactory.Parameters.Add(PUSUARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_CIERREHISTORICO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        // dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;

                    }   
                  
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreMensual", "CrearCierremensual", ex);
                        return null;

                    }
                }
            }
        }
        //public string ValidarCierre(string valor, DateTime fechas, int cod_usuario, Usuario pUsuario)
        //{


        //    using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
        //    {
        //        using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
        //        {
        //            try
        //            {

        //                DbParameter PFECHA = cmdTransaccionFactory.CreateParameter();
        //                PFECHA.ParameterName = "PFECHA";
        //                PFECHA.Value = fechas;
        //                PFECHA.DbType = DbType.Date;

        //                DbParameter PESTADO = cmdTransaccionFactory.CreateParameter();
        //                PESTADO.ParameterName = "PESTADO";
        //                PESTADO.Value = valor;

        //                DbParameter PUSUARIO = cmdTransaccionFactory.CreateParameter();
        //                PUSUARIO.ParameterName = "PUSUARIO";
        //                PUSUARIO.Value = cod_usuario;

        //                DbParameter p_mensajeerror = cmdTransaccionFactory.CreateParameter();
        //                p_mensajeerror.ParameterName = "pMensajeError";
        //                p_mensajeerror.Value = DBNull.Value;

        //                // No quitar, molesta si lo quitas
        //                p_mensajeerror.Size = 8000;
        //                p_mensajeerror.DbType = DbType.String;
        //                p_mensajeerror.Direction = ParameterDirection.Output;
        //                cmdTransaccionFactory.Parameters.Add(p_mensajeerror);

        //                cmdTransaccionFactory.Parameters.Add(PFECHA);
        //                cmdTransaccionFactory.Parameters.Add(PESTADO);
        //                cmdTransaccionFactory.Parameters.Add(PUSUARIO);

        //                connection.Open();
        //                cmdTransaccionFactory.Connection = connection;
        //                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
        //                cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_VALIDARCIERRE";
        //                cmdTransaccionFactory.ExecuteNonQuery();

        //                string error = p_mensajeerror.Value != DBNull.Value ? p_mensajeerror.Value.ToString() : string.Empty;

        //                return error;

        //            }
        //            catch (Exception ex)
        //            {
        //                connection.Close();
        //                BOExcepcion.Throw("CierreHistoricoData", "ValidarCierre", ex);
        //                return null;
        //            }
        //        }
        //    }
        //}
        public List<Cierea> ListaErrorCierreHistorico(string tipo, Usuario pUsuario, string FechaCierre)
        {
            DbDataReader resultado;
            List<Cierea> lsConsulta = new List<Cierea>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"Select Distinct Tipoproducto, Fechacierre, Tipocierre, Numeroproducto, Saldo, Descripcionerror From ErroresCierresHistoricos Where To_Char(fechacierre,'dd/mm/yyyy')='" + FechaCierre+"' and Tipocierre='"+tipo+ "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Cierea entidad = new Cierea();
                            if (resultado["NUMEROPRODUCTO"] != DBNull.Value) entidad.codproducto = Convert.ToString(resultado["NUMEROPRODUCTO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToString(resultado["SALDO"]);
                            if (resultado["DESCRIPCIONERROR"] != DBNull.Value) entidad.descripcionerror = Convert.ToString(resultado["DESCRIPCIONERROR"]);


                            lsConsulta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lsConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListaErrorCierreHistorico", ex);
                        return null;
                    }
                }
            }

        }
        public DateTime FecSumDia(DateTime fecha, int dias, int tipo_cal)
        {
            int dato = 0;
            if (fecha == null)
                return fecha;
            if (tipo_cal == 1)
            {
                int año_fec = fecha.Year;
                int mes_fec = fecha.Month;
                int dia_fec = fecha.Day;
                int dias_febrero = 28;
                if (año_fec % 4 == 0)
                    dias_febrero = 29;
                if (dia_fec > 30 || (mes_fec == 2 && dia_fec >= dias_febrero))
                    dia_fec = 30;
                dato = (dias / 360);
                if (dato > 1)
                {
                    año_fec = año_fec + dato;
                    dias = dias - (360 * dato);
                }
                dato = (dias / 30);
                if (dato > 1)
                {
                    mes_fec = mes_fec + dato;
                    if (mes_fec > 12)
                    {
                        año_fec = año_fec + 1;
                        mes_fec = mes_fec - 12;
                    }
                    dias = dias - (30 * dato);
                }
                if (dias > 0)
                {
                    if (30 - dia_fec < dias)
                    {
                        mes_fec = mes_fec + 1;
                        if (mes_fec > 12)
                        {
                            año_fec = año_fec + 1;
                            mes_fec = 1;
                        }
                        dia_fec = dias - (30 - dia_fec);
                    }
                    else
                    {
                        dia_fec = dia_fec + dias;
                    }
                }
                if (mes_fec == 2 && (dia_fec > dias_febrero && dia_fec <= 30))
                    dia_fec = dias_febrero;
                if (mes_fec == 2 && (dia_fec > 30 && dia_fec <= 59))
                    dia_fec = dia_fec - (30 - dias_febrero);
                if (mes_fec == 4 || mes_fec == 6 || mes_fec == 9 || mes_fec == 11)
                    if (dia_fec == 31)
                        dia_fec = 30;
                fecha = new DateTime(año_fec, mes_fec, dia_fec, 0, 0, 0);
            }
            else
            {
                fecha = fecha.AddDays(dias);
            }
            return fecha;
        }

        public Xpinn.Comun.Entities.Cierea FechaUltimoCierre(Xpinn.Comun.Entities.Cierea pCierea, String filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (filtro == "")
                        {
                            sql = "Select * from cierea" + ObtenerFiltro(pCierea) + " Order by fecha desc";
                        }
                        else
                        {
                            sql = "Select * from cierea c" + filtro + " Order by fecrea desc";
                        }


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            Xpinn.Comun.Entities.Cierea entidad = new Xpinn.Comun.Entities.Cierea();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["CAMPO1"] != DBNull.Value) entidad.campo1 = Convert.ToString(resultado["CAMPO1"]);
                            if (resultado["CAMPO2"] != DBNull.Value) entidad.campo2 = Convert.ToString(resultado["CAMPO2"]);
                            if (resultado["FECREA"] != DBNull.Value) entidad.fecrea = Convert.ToDateTime(resultado["FECREA"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);

                            dbConnectionFactory.CerrarConexion(connection);

                            return entidad;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "FechaUltimoCierre", ex);
                        return null;
                    }
                }
            }
        }

        public void PeriodicidadCierre(ref int dias_cierre, ref int tipo_calendario, Usuario pUsuario)
        {
            dias_cierre = 30;
            tipo_calendario = 1;
            int periodicidad = 0;
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string valor = "";
                        string sql = "Select valor From general Where codigo = 1911 ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToString(resultado["VALOR"].ToString().Trim());
                        }
                        try
                        {
                            periodicidad = Convert.ToInt16(valor);
                        }
                        catch
                        {
                            return;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "PeriodicidadCierre", ex);
                        return;
                    }
                }
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select numero_dias, tipo_calendario From periodicidad Where cod_periodicidad = " + periodicidad;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_DIAS"] != DBNull.Value) dias_cierre = Convert.ToInt16(resultado["NUMERO_DIAS"].ToString());
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) tipo_calendario = Convert.ToInt16(resultado["TIPO_CALENDARIO"].ToString());
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "PeriodicidadCierre", ex);
                        return;
                    }
                }

            }
        }

        /// <summary>
        /// Crea un registro en la tabla Imagenes de la base de datos
        /// </summary>
        /// <param name="pImagenes">Entidad Imagenes</param>
        /// <returns>Entidad Imagenes creada</returns>
        public Entities.Imagenes CrearImagenesAhorros(Entities.Imagenes pImagenes, Usuario pUsuario)
        {
            try
            {
                ImagenesORAData DAImagenes = new ImagenesORAData();
                return DAImagenes.CrearImagenesAhorros(pImagenes, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaData", "CrearImagenesAhorros", ex);
                return null;
            }
        }


        public bool ExisteImagenAhorros(Int64 numero_cuenta, int IdTipo, Usuario pUsuario)
        {
            DbDataReader resultado;
            bool bresultado = false;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  AHORRO_VISTA_IMAGENES WHERE NUMERO_CUENTA = " + numero_cuenta.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        if (resultado.Read())
                            bresultado = true;
                        else
                            bresultado = false;
                        dbConnectionFactory.CerrarConexion(connection);
                        return bresultado;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public Ahorros.Entities.Imagenes ModificarImagenesAhorros(Ahorros.Entities.Imagenes pImagenes, Usuario pUsuario)
        {
            try
            {
                ImagenesORAData DAImagen = new ImagenesORAData();
                return DAImagen.ModificarImagenesAhorros(pImagenes, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaData", "ModificarImagenesAhorros", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Imagenes de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Imagenes</param>
        /// <returns>Entidad Imagenes consultado</returns>
        public byte[] ConsultarImagenPersona(string pId, Int64 pTipoImagen, ref Int64 pIdImagen, Usuario pUsuario)
        {
            DbDataReader resultado;
            Ahorros.Entities.Imagenes entidad = new Ahorros.Entities.Imagenes();
            pIdImagen = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM AHORRO_VISTA_IMAGENES WHERE NUMERO_CUENTA = '" + pId.ToString() + "' AND tipo_documento = " + pTipoImagen;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        //System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        if (resultado.Read())
                        {
                            //if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = enc.GetBytes(Convert.ToString(resultado["IMAGEN"]));
                            if (resultado["IDIMAGEN"] != DBNull.Value) pIdImagen = Convert.ToInt64(resultado["IDIMAGEN"]);
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = (byte[])resultado["IMAGEN"];
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad.imagen;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Imagenes dados unos filtros
        /// </summary>
        /// <param name="pImagenes">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Imagenes obtenidos</returns>
        public List<Ahorros.Entities.Imagenes> Handler(Ahorros.Entities.Imagenes vImagenes, Usuario pUsuario)
        {
            List<Ahorros.Entities.Imagenes> lstImagenes = new List<Ahorros.Entities.Imagenes>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {   // Despues de poner los datos que no son imagen en el gridview, se selecciona la imagen y se raliza su respectiva relacion

                        string sql = "SELECT da.imagen FROM  AHORRO_VISTA_IMAGENES da WHERE da.idimagen = " + vImagenes.idimagen;
                        DbDataReader resultado = default(DbDataReader);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Ahorros.Entities.Imagenes entidad = new Ahorros.Entities.Imagenes();
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = (byte[])resultado["IMAGEN"];
                            lstImagenes.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstImagenes;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "Handler", ex);
                        return null;
                    }
                }
            }
        }
        // valida fechas para retiro parcial de ahorro Programado
        public DateTime getfechaUltimoCierreConta(Usuario pUsuario)
        {
            DbDataReader resultado;
            DateTime fecha = DateTime.MinValue;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select Max(fecha) as fecha From cierea Where tipo = 'C' And estado = 'D'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                            if (resultado["fecha"] != DBNull.Value) fecha = Convert.ToDateTime(resultado["fecha"]);

                        dbConnectionFactory.CerrarConexion(connection);
                        return fecha;

                        //if (pFecha <= fecha)
                        //    return false;
                        //else return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "getfechaUltimoCierreConta", ex);
                        return DateTime.MinValue;
                    }
                }
            }
        }
        public DateTime getfechaUltimaCierreAhorros(Usuario pUsuario)
        {
            DbDataReader resultado;
            DateTime fecha = DateTime.MinValue;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select Max(fecha) as fecha From cierea Where tipo = 'A' And estado = 'D'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                            if (resultado["fecha"] != DBNull.Value) fecha = Convert.ToDateTime(resultado["fecha"]);

                        dbConnectionFactory.CerrarConexion(connection);
                        return fecha;

                        //if (pFecha <= fecha)
                        //    return false;
                        //else return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "getfechaUltimaCierreAhorros", ex);
                        return DateTime.MinValue;
                    }
                }
            }
        }
        public List<ELiquidacionInteres> getListaCuentasLiquidar(DateTime pfechaLiquidacion, String pCodLinea, Usuario vUsuario, String cuenta)
        {
            DbDataReader resultado;
            List<ELiquidacionInteres> listaEntidad = new List<ELiquidacionInteres>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pfecha_liquida = cmdTransaccionFactory.CreateParameter();
                        pfecha_liquida.ParameterName = "pfecha_liquida";
                        pfecha_liquida.Value = pfechaLiquidacion;
                        pfecha_liquida.Direction = ParameterDirection.Input;
                        pfecha_liquida.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha_liquida);


                        DbParameter pcod_linea_ahorros = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_ahorros.ParameterName = "pcod_linea_ahorros";
                        pcod_linea_ahorros.Value = pCodLinea;
                        pcod_linea_ahorros.Direction = ParameterDirection.Input;
                        pcod_linea_ahorros.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_ahorros);


                        DbParameter pcodigo_usuario = cmdTransaccionFactory.CreateParameter();
                        pcodigo_usuario.ParameterName = "pcodigo_usuario";
                        pcodigo_usuario.Value = vUsuario.codusuario;
                        pcodigo_usuario.Direction = ParameterDirection.Input;
                        pcodigo_usuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_usuario);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "pnumero_cuenta";
                        pnumero_cuenta.Value = cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);


                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_LIQUIDACION";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "getListaCuentasLiquidar", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.Connection = connection;

                        string sql = @"select tem.*, vp.nombre, vp.identificacion, av.FECHA_INTERES 
                                        from TEMP_LIQUID_AHORRO tem
                                        inner join ahorro_vista av on tem.numero_cuenta = av.NUMERO_CUENTA
                                        inner join v_persona vp on tem.cod_persona = vp.cod_persona";
                        //sql = "select * from TEMP_LIQUIDAPRO";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ELiquidacionInteres entidad = new ELiquidacionInteres();

                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.NumeroCuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.Cod_Usuario = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["COD_OFICINA"]);
                            try { if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.Fecha_Apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]); }
                            catch { }
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.Tasa_interes = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["DIAS"] != DBNull.Value) entidad.dias = Convert.ToInt16(resultado["DIAS"]);
                            if (resultado["INTERES"] != DBNull.Value) entidad.Interes = Convert.ToDecimal(resultado["INTERES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.Retefuente = Convert.ToDecimal(resultado["RETENCION"]);
                            if (resultado["VALOR_NETO"] != DBNull.Value) entidad.valor_Neto = Convert.ToDecimal(resultado["VALOR_NETO"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.Retefuente = Convert.ToDecimal(resultado["RETENCION"]);
                            if (resultado["VALOR_NETO"] != DBNull.Value) entidad.valor_Neto = Convert.ToDecimal(resultado["VALOR_NETO"]);
                            if (resultado["VALOR_NETO"] != DBNull.Value) entidad.valor_Neto = Convert.ToDecimal(resultado["VALOR_NETO"]);
                            if (resultado["RETENCION_CAUSADO"] != DBNull.Value) entidad.retencion_causado = Convert.ToDecimal(resultado["RETENCION_CAUSADO"]);
                            if (resultado["INTERES_CAUSADO"] != DBNull.Value) entidad.Interescausado = Convert.ToDecimal(resultado["INTERES_CAUSADO"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_int = Convert.ToDateTime(resultado["FECHA_INTERES"]);   

                            listaEntidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listaEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ELiquidacionInteres", ex);
                        return null;
                    }
                };
            }
        }


        //CONSULTAR DETALLE TITULAR
        public List<CuentaHabientes> ListarDetalleTitulares(Int64 pCod, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CuentaHabientes> lstTitu = new List<CuentaHabientes>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select C.IDCUENTA_HABIENTE,C.COD_PERSONA,C.NUMERO_CUENTA,P.Identificacion, "
                                        + "P.Primer_Nombre ||' '|| P.Segundo_Nombre As Nombres, P.Primer_Apellido||' '|| P.Segundo_Apellido As Apellidos, "
                                        + "P.Codciudadresidencia, I.Nomciudad,P.Direccion,P.Telefono,C.Tipo_firma  "
                                        + "From cuenta_habiente C Inner Join Persona P On C.Cod_Persona = P.Cod_Persona "
                                        + "left join Ciudades i on I.Codciudad = P.Codciudadresidencia  where C.NUMERO_CUENTA = " + pCod.ToString() + " ORDER BY C.COD_PERSONA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentaHabientes entidad = new CuentaHabientes();
                            if (resultado["IDCUENTA_HABIENTE"] != DBNull.Value) entidad.idcuenta_habiente = Convert.ToInt64(resultado["IDCUENTA_HABIENTE"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);

                            //AGREGADO
                            if (resultado["IDCUENTA_HABIENTE"] != DBNull.Value) entidad.idcuenta_habiente = Convert.ToInt64(resultado["IDCUENTA_HABIENTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["Tipo_firma"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["Tipo_firma"]);

                            lstTitu.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTitu;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarDetalleTitulares", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarCtaHabiente(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idcuenta_habiente = cmdTransaccionFactory.CreateParameter();
                        p_idcuenta_habiente.ParameterName = "p_idcuenta_habiente";
                        p_idcuenta_habiente.Value = pId;
                        p_idcuenta_habiente.Direction = ParameterDirection.Input;
                        p_idcuenta_habiente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idcuenta_habiente);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_CUENTAHAB_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "EliminarCtaHabiente", ex);
                    }
                }
            }
        }

        public AhorroVista ConsultarAfiliacion(String pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            AhorroVista entidad = new AhorroVista();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select a.estado,a.cod_persona From persona_afiliacion a Where a.cod_persona= " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["estado"] != DBNull.Value) entidad.estadopersona = Convert.ToString(resultado["estado"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);


                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ConsultarAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public ELiquidacionInteres CalculoLiquidacionaHORRO(ELiquidacionInteres pLiqui, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha_liquidacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_liquidacion.ParameterName = "p_fecha_liquidacion";
                        pfecha_liquidacion.Value = pLiqui.fecha_liquidacion;
                        pfecha_liquidacion.Direction = ParameterDirection.Input;
                        pfecha_liquidacion.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha_liquidacion);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "pnumero_cuenta";
                        pnumero_cuenta.Value = pLiqui.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pLiqui.Saldo;
                        pvalor.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pinteres = cmdTransaccionFactory.CreateParameter();
                        pinteres.ParameterName = "pinteres";
                        pinteres.Value = pLiqui.Interes;
                        pinteres.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pinteres);

                        DbParameter pinteres_capitalizado = cmdTransaccionFactory.CreateParameter();
                        pinteres_capitalizado.ParameterName = "pinteres_capitalizado";
                        pinteres_capitalizado.Value = pLiqui.interes_capitalizado;
                        pinteres_capitalizado.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pinteres_capitalizado);

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "pretencion";
                        pretencion.Value = pLiqui.Retefuente;
                        pretencion.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pretencion);


                        DbParameter pgmf = cmdTransaccionFactory.CreateParameter();
                        pgmf.ParameterName = "pgmf";
                        pgmf.Value = pLiqui.valor_gmf;
                        pgmf.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pgmf);

                        DbParameter pvalor_a_pagar = cmdTransaccionFactory.CreateParameter();
                        pvalor_a_pagar.ParameterName = "pvalor_a_pagar";
                        pvalor_a_pagar.Value = pLiqui.valor_pagar;
                        pvalor_a_pagar.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pvalor_a_pagar);


                        DbParameter p_Interes_causado = cmdTransaccionFactory.CreateParameter();
                        p_Interes_causado.ParameterName = "p_Interes_causado";
                        p_Interes_causado.Value = pLiqui.Interescausado;
                        p_Interes_causado.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(p_Interes_causado);


                        DbParameter p_retencion_causado = cmdTransaccionFactory.CreateParameter();
                        p_retencion_causado.ParameterName = "p_retencion_causado";
                        p_retencion_causado.Value = pLiqui.retencion_causado;
                        p_retencion_causado.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(p_retencion_causado);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_LIQUIDAAHORRO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiqui.Saldo = pvalor.Value != DBNull.Value ? Convert.ToDecimal(pvalor.Value) : 0;
                        pLiqui.Interes = pinteres.Value != DBNull.Value ? Convert.ToDecimal(pinteres.Value) : 0;
                        pLiqui.interes_capitalizado = pinteres_capitalizado.Value != DBNull.Value ? Convert.ToDecimal(pinteres_capitalizado.Value) : 0;
                        pLiqui.Retefuente = pretencion.Value != DBNull.Value ? Convert.ToDecimal(pretencion.Value) : 0;
                        pLiqui.valor_pagar = pvalor_a_pagar.Value != DBNull.Value ? Convert.ToDecimal(pvalor_a_pagar.Value) : 0;
                        pLiqui.valor_gmf = pgmf.Value != DBNull.Value ? Convert.ToDecimal(pgmf.Value) : 0;
                        pLiqui.Interescausado = p_Interes_causado.Value != DBNull.Value ? Convert.ToDecimal(p_Interes_causado.Value) : 0;
                        pLiqui.retencion_causado = p_retencion_causado.Value != DBNull.Value ? Convert.ToDecimal(p_retencion_causado.Value) : 0;

                        return pLiqui;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "CalculoLiquidacionaHORRO", ex);
                        return null;
                    }
                }
            }
        }
        public ELiquidacionInteres CierreLiquidacionAhorro(ELiquidacionInteres pLiqui, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha_cierre = cmdTransaccionFactory.CreateParameter();
                        pfecha_cierre.ParameterName = "p_fecha_cierre";
                        pfecha_cierre.Value = pLiqui.fecha_liquidacion;
                        pfecha_cierre.Direction = ParameterDirection.Input;
                        pfecha_cierre.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha_cierre);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "pnumero_cuenta";
                        pnumero_cuenta.Value = pLiqui.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcod_ope";
                        pcod_ope.Value = pLiqui.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pLiqui.Saldo;
                        pvalor.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pinteres_capitalzado = cmdTransaccionFactory.CreateParameter();
                        pinteres_capitalzado.ParameterName = "pinteres_capitalzado";
                        pinteres_capitalzado.Value = pLiqui.interes_capitalizado;
                        pinteres_capitalzado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pinteres_capitalzado);

                        DbParameter pinteres = cmdTransaccionFactory.CreateParameter();
                        pinteres.ParameterName = "pinteres";
                        pinteres.Value = pLiqui.Interes;
                        pinteres.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pinteres);


                        DbParameter pinterescausado = cmdTransaccionFactory.CreateParameter();
                        pinterescausado.ParameterName = "pinterescausado";
                        pinterescausado.Value = pLiqui.Interescausado;
                        pinterescausado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pinterescausado);


                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "pretencion";
                        pretencion.Value = pLiqui.Retefuente;
                        pretencion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pretencion);


                        DbParameter pretencioncausado = cmdTransaccionFactory.CreateParameter();
                        pretencioncausado.ParameterName = "pretencioncausado";
                        pretencioncausado.Value = pLiqui.retencion_causado;
                        pretencioncausado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pretencioncausado);

                        DbParameter pgmf = cmdTransaccionFactory.CreateParameter();
                        pgmf.ParameterName = "pgmf";
                        pgmf.Value = pLiqui.valor_gmf;
                        pgmf.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pgmf);

                        DbParameter pvalor_a_pagar = cmdTransaccionFactory.CreateParameter();
                        pvalor_a_pagar.ParameterName = "pvalor_a_pagar";
                        pvalor_a_pagar.Value = pLiqui.valor_pagar;
                        pvalor_a_pagar.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pvalor_a_pagar);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_CIERREAHO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pLiqui, "ahorro_vista", pusuario, Accion.Crear.ToString(), TipoAuditoria.AhorroVista, "Creacion de cierre de ahorro vista de cuenta con numero " + pLiqui.numero_cuenta); //REGISTRO DE AUDITORIA
                        pLiqui.cod_ope = Convert.ToInt32(pcod_ope.Value);
                        return pLiqui;
                    }
                    catch (Exception ex)
                    {

                        BOExcepcion.Throw("LiquidacionCDATData", "CierreLiquidacionAhorro", ex);
                        return null;
                    }
                }
            }

        }

        public void GuardarLiquidacionAhorro(ELiquidacionInteres pLiqui, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_num_tran = cmdTransaccionFactory.CreateParameter();
                        p_num_tran.ParameterName = "p_num_tran";
                        p_num_tran.Value = 0;
                        p_num_tran.Direction = ParameterDirection.Output;
                        p_num_tran.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_num_tran);

                        DbParameter pn_fecha_interes = cmdTransaccionFactory.CreateParameter();
                        pn_fecha_interes.ParameterName = "pn_fecha_interes";
                        pn_fecha_interes.Value = pLiqui.fecha_liquidacion;
                        pn_fecha_interes.Direction = ParameterDirection.Input;
                        pn_fecha_interes.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pn_fecha_interes);


                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "pnumero_cuenta";
                        pnumero_cuenta.Value = pLiqui.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcod_ope";
                        pcod_ope.Value = pLiqui.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pLiqui.valor_pagar;
                        pvalor.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pvalor);


                        DbParameter pinteres = cmdTransaccionFactory.CreateParameter();
                        pinteres.ParameterName = "pinteres";
                        pinteres.Value = pLiqui.Interes;
                        pinteres.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pinteres);

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "pretencion";
                        pretencion.Value = pLiqui.Retefuente;
                        pretencion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pretencion);


                        DbParameter pgmf = cmdTransaccionFactory.CreateParameter();
                        pgmf.ParameterName = "pgmf";
                        pgmf.Value = pLiqui.valor_gmf;
                        pgmf.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pgmf);

                        DbParameter pinterescausado = cmdTransaccionFactory.CreateParameter();
                        pinterescausado.ParameterName = "pinterescausado";
                        pinterescausado.Value = pLiqui.Interescausado;
                        pinterescausado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pinterescausado);


                        DbParameter pretencioncausado = cmdTransaccionFactory.CreateParameter();
                        pretencioncausado.ParameterName = "pretencioncausado";
                        pretencioncausado.Value = pLiqui.retencion_causado;
                        pretencioncausado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pretencioncausado);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_LIQU_INT_AHO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "GuardarLiquidacionAhorro", ex);
                    }
                }
            }

        }

        public ELiquidacionInteres CrearLiquidacionAhorro(ELiquidacionInteres pLiqui, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_interes = cmdTransaccionFactory.CreateParameter();
                        pcod_interes.ParameterName = "p_cod_interes";
                        pcod_interes.Value = pLiqui.cod_interes;
                        pcod_interes.Direction = ParameterDirection.Output;
                        pcod_interes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_interes);

                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_numero_cuenta";
                        pcodigo_cdat.Value = pLiqui.numero_cuenta;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter pfecha_liquidacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_liquidacion.ParameterName = "p_fecha_liquidacion";
                        pfecha_liquidacion.Value = pLiqui.fecha_liquidacion;
                        pfecha_liquidacion.Direction = ParameterDirection.Input;
                        pfecha_liquidacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_liquidacion);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        if (pLiqui.Tasa_interes != 0) ptasa.Value = pLiqui.Tasa_interes; else ptasa.Value = DBNull.Value;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter pfecha_interes = cmdTransaccionFactory.CreateParameter();
                        pfecha_interes.ParameterName = "p_fecha_interes";
                        if (pLiqui.fecha_int != DateTime.MinValue) pfecha_interes.Value = pLiqui.fecha_int; else pfecha_interes.Value = DBNull.Value;
                        pfecha_interes.Direction = ParameterDirection.Input;
                        pfecha_interes.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_interes);

                        DbParameter pintereses = cmdTransaccionFactory.CreateParameter();
                        pintereses.ParameterName = "p_intereses";
                        if (pLiqui.Interes != 0) pintereses.Value = pLiqui.Interes; else pintereses.Value = DBNull.Value;
                        pintereses.Direction = ParameterDirection.Input;
                        pintereses.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pintereses);

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "p_retencion";
                        if (pLiqui.Retefuente != 0) pretencion.Value = pLiqui.Retefuente; else pretencion.Value = DBNull.Value;
                        pretencion.Direction = ParameterDirection.Input;
                        pretencion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pretencion);

                        DbParameter pvalor_gmf = cmdTransaccionFactory.CreateParameter();
                        pvalor_gmf.ParameterName = "p_valor_gmf";
                        if (pLiqui.valor_gmf != 0) pvalor_gmf.Value = pLiqui.valor_gmf; else pvalor_gmf.Value = DBNull.Value;
                        pvalor_gmf.Direction = ParameterDirection.Input;
                        pvalor_gmf.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_gmf);

                        DbParameter pvalor_neto = cmdTransaccionFactory.CreateParameter();
                        pvalor_neto.ParameterName = "p_valor_neto";
                        if (pLiqui.valor_Neto != 0) pvalor_neto.Value = pLiqui.valor_Neto; else pvalor_neto.Value = DBNull.Value;//VALOR NETO
                        pvalor_neto.Direction = ParameterDirection.Input;
                        pvalor_neto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_neto);

                        DbParameter pinteres_causado = cmdTransaccionFactory.CreateParameter();
                        pinteres_causado.ParameterName = "p_interes_causado";
                        if (pLiqui.Interescausado != 0) pinteres_causado.Value = pLiqui.Interescausado; else pinteres_causado.Value = DBNull.Value;
                        pinteres_causado.Direction = ParameterDirection.Input;
                        pinteres_causado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pinteres_causado);

                        DbParameter pretencion_causada = cmdTransaccionFactory.CreateParameter();
                        pretencion_causada.ParameterName = "p_retencion_causada";
                        if (pLiqui.retencion_causado != 0) pretencion_causada.Value = pLiqui.retencion_causado; else pretencion_causada.Value = DBNull.Value;
                        pretencion_causada.Direction = ParameterDirection.Input;
                        pretencion_causada.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pretencion_causada);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        if (pLiqui.forma_pago != null) pforma_pago.Value = pLiqui.forma_pago; else pforma_pago.Value = DBNull.Value;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter pcuenta_ahorros = cmdTransaccionFactory.CreateParameter();
                        pcuenta_ahorros.ParameterName = "p_cuenta_ahorros";
                        if (pLiqui.cta_ahorros != null) pcuenta_ahorros.Value = pLiqui.cta_ahorros; else pcuenta_ahorros.Value = DBNull.Value;
                        pcuenta_ahorros.Direction = ParameterDirection.Input;
                        pcuenta_ahorros.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcuenta_ahorros);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = 1;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_LIQUIDACIO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pLiqui.cod_interes = Convert.ToInt32(pcod_interes.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLiqui;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "CrearLiquidacionAhorro", ex);
                        return null;
                    }
                }
            }
        }




        public List<AhorroVista> ListarAhorroVistApPagos(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT A.*, L.DESCRIPCION AS NOM_LINEA_AHORRO, O.NOMBRE AS NOM_OFICINA, P.IDENTIFICACION, P.NOMBRE, "
                                        + "CASE A.ESTADO WHEN 0 THEN 'Apertura' WHEN 1 THEN 'Activo' WHEN 2 THEN 'Inactivo' WHEN 3 THEN 'Cerrado' END AS NOMESTADO "
                                        + "FROM Ahorro_Vista a INNER JOIN LineaAhorro l On a.cod_linea_ahorro = l.cod_linea_ahorro "
                                        + "INNER JOIN Oficina o ON a.cod_oficina = o.cod_oficina "
                                        + "INNER JOIN V_PERSONA P ON A.COD_PERSONA = P.COD_PERSONA " + pFiltro;

                        sql += " ORDER BY A.NUMERO_CUENTA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["NOM_LINEA_AHORRO"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA_AHORRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToInt32(resultado["MODALIDAD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                            if (resultado["COD_FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["COD_FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOMESTADO"]);
                            lstAhorroVista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarAhorroVistApPagos", ex);
                        return null;
                    }
                }
            }
        }

        public List<AhorroVista> ListarAhorroAptPagos(string pFiltro, int p_producto, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();


                        string sql = @"SELECT 1 AS TIPO_PRODUCTO,'AHORRO PERMANENTE' AS PRODUCTO, TO_CHAR(AHAP.NUMERO_APORTE) as NUM_CUENTA, AHAP.SALDO as SALDO
                                    FROM APORTE AHAP
                                    INNER JOIN PERSONA P ON AHAP.COD_PERSONA = P.COD_PERSONA
                                    WHERE AHAP.ESTADO = 1
                                    AND AHAP.COD_LINEA_APORTE = 2
                                    AND AHAP.SALDO > 0"
                                    + pFiltro;

                        if (p_producto != 3)
                        {
                            sql = sql + @"UNION
                                    SELECT 3 AS TIPO_PRODUCTO, 'AHORRO A LA VISTA' AS PRODUCTO, AHV.NUMERO_CUENTA as NUM_CUENTA, AHV.SALDO_TOTAL as SALDO
                                    FROM AHORRO_VISTA AHV
                                    INNER JOIN PERSONA P ON AHV.COD_PERSONA = P.COD_PERSONA
                                    WHERE AHV.ESTADO = 1 AND AHV.SALDO_TOTAL > 0"
                                    + pFiltro;
                        }
                        if (p_producto != 9)
                        {
                            sql = sql + @"UNION SELECT 9 AS TIPO_PRODUCTO, 'AHORRO PROGRAMADO' AS PRODUCTO, AHP.NUMERO_PROGRAMADO as NUM_CUENTA, AHP.SALDO as SALDO
                                    FROM AHORRO_PROGRAMADO AHP
                                    INNER JOIN PERSONA P ON AHP.COD_PERSONA = P.COD_PERSONA
                                    WHERE AHP.ESTADO = 1 AND AHP.SALDO > 0 "
                                   + pFiltro;
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();

                            if (resultado["PRODUCTO"] != DBNull.Value) entidad.nombre_producto = Convert.ToString(resultado["PRODUCTO"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt64(resultado["TIPO_PRODUCTO"]);
                            //if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            //if (resultado["NOM_LINEA_AHORRO"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA_AHORRO"]);
                            //if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            //if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            //if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            //if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            //if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            //if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            //if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            //if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToInt32(resultado["MODALIDAD"]);
                            //if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            //if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            //if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            //if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            //if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            //if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                            //if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            //if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            //if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            //if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            //if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            //if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            //if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                            //if (resultado["COD_FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["COD_FORMA_PAGO"]);
                            //if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            //if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            //if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            //if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            //if (resultado["NOMESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOMESTADO"]);
                            lstAhorroVista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarAhorroAptPagos", ex);
                        return null;
                    }
                }
            }
        }


        public void Aplicar(AhorroVista pAhorroVista, Int64 pCod_Ope, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        p_cod_ope.Value = pCod_Ope;
                        p_cod_ope.Direction = ParameterDirection.Input;
                        p_cod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        DbParameter p_num_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_num_cuenta.ParameterName = "p_num_cuenta";
                        p_num_cuenta.Value = pAhorroVista.numero_cuenta;
                        p_num_cuenta.Direction = ParameterDirection.Input;
                        p_num_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_num_cuenta);

                        DbParameter p_valor_aplica = cmdTransaccionFactory.CreateParameter();
                        p_valor_aplica.ParameterName = "p_valor_aplica";
                        p_valor_aplica.Value = pAhorroVista.valor_a_aplicar;
                        p_valor_aplica.Direction = ParameterDirection.Input;
                        p_valor_aplica.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor_aplica);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_CRUCE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "Aplicar", ex);
                    }
                }
            }
        }
        public List<CreditoDebAhorros> ListarCreditoDebAhorros(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CreditoDebAhorros> lstCreditoDebAhorros = new List<CreditoDebAhorros>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT C.NUMERO_RADICACION,C.COD_DEUDOR,(C.COD_LINEA_CREDITO ||' - '|| L.NOMBRE) LINEA,C.FECHA_APROBACION,P.IDENTIFICACION,"
                                        + "P.NOMBRE,C.SALDO_CAPITAL,C.VALOR_CUOTA,C.FECHA_PROXIMO_PAGO,C.FECHA_ULTIMO_PAGO,CD.NUMERO_CUENTA,AV.SALDO_TOTAL,"
                                        + "(AV.SALDO_TOTAL-AV.SALDO_CANJE-LA.SALDO_MINIMO) SALDO_DISPONIBLE "
                                        + "FROM CREDITO C INNER JOIN CREDITO_DEBAHORROS CD ON C.NUMERO_RADICACION=CD.NUMERO_RADICACION "
                                        + "INNER JOIN LINEASCREDITO L ON C.COD_LINEA_CREDITO= L.COD_LINEA_CREDITO "
                                        + "INNER JOIN AHORRO_VISTA AV ON CD.NUMERO_CUENTA = AV.NUMERO_CUENTA "
                                        + "INNER JOIN LINEAAHORRO LA ON AV.COD_LINEA_AHORRO = LA.COD_LINEA_AHORRO "
                                        + "LEFT JOIN V_PERSONA P ON C.COD_DEUDOR = P.COD_PERSONA WHERE C.ESTADO='C'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CreditoDebAhorros entidad = new CreditoDebAhorros();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_cliente = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_DISPONIBLE"] != DBNull.Value) entidad.saldo_disponible = Convert.ToDecimal(resultado["SALDO_DISPONIBLE"]);
                            lstCreditoDebAhorros.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCreditoDebAhorros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarCreditoDebAhorros", ex);
                        return null;
                    }
                }
            }
        }

        public Decimal? Calcular_VrAPagar(Int64 pNumRadicacion, String pFecha, Usuario pUsuario)

        {
            Configuracion conf = new Configuracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Int64? valor = 0;
                        string date_type = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            date_type = "TO_DATE('" + pFecha + "','" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            date_type = "'" + pFecha + "'";

                        string sql = @"select Calcular_VrAPagar(" + pNumRadicacion + ", " + date_type + ") from dual";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        var result = cmdTransaccionFactory.ExecuteScalar();

                        if (result != DBNull.Value)
                            valor = Convert.ToInt64(result.ToString());

                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "Calcular_VrAPagar", ex);
                        return null;
                    }
                }
            }
        }

        public Boolean AplicarCréditoDebAhorros(CreditoDebAhorros pCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pn_num_producto = cmdTransaccionFactory.CreateParameter();
                        pn_num_producto.ParameterName = "pn_num_producto";
                        pn_num_producto.Value = pCredito.numero_cuenta;
                        pn_num_producto.DbType = DbType.String;
                        pn_num_producto.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        pn_cod_cliente.ParameterName = "pn_cod_cliente";
                        pn_cod_cliente.Value = pCredito.cod_cliente;
                        pn_cod_cliente.DbType = DbType.Int64;
                        pn_cod_cliente.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_ope = cmdTransaccionFactory.CreateParameter();
                        pn_cod_ope.ParameterName = "pn_cod_ope";
                        pn_cod_ope.Value = pCredito.cod_ope;
                        pn_cod_ope.Direction = ParameterDirection.Input;
                        pn_cod_ope.DbType = DbType.Int64;

                        DbParameter pf_fecha_pago = cmdTransaccionFactory.CreateParameter();
                        pf_fecha_pago.ParameterName = "pf_fecha_pago";
                        pf_fecha_pago.Value = pCredito.fecha_proximo_pago;
                        pf_fecha_pago.Direction = ParameterDirection.Input;
                        pf_fecha_pago.DbType = DbType.DateTime;

                        DbParameter pn_valor_pago = cmdTransaccionFactory.CreateParameter();
                        pn_valor_pago.ParameterName = "pn_valor_pago";
                        pn_valor_pago.Value = pCredito.valor_pagar;
                        pn_valor_pago.Direction = ParameterDirection.Input;
                        pn_valor_pago.DbType = DbType.Decimal;


                        DbParameter pn_cod_usu = cmdTransaccionFactory.CreateParameter();
                        pn_cod_usu.ParameterName = "pn_cod_usu";
                        pn_cod_usu.Value = pUsuario.codusuario;
                        pn_cod_usu.Direction = ParameterDirection.Input;
                        pn_cod_usu.DbType = DbType.Int64;

                        DbParameter pn_documento = cmdTransaccionFactory.CreateParameter();
                        pn_documento.ParameterName = "pn_documento";
                        pn_documento.Value = pCredito.numero_radicacion.ToString();
                        pn_documento.Direction = ParameterDirection.Input;
                        pn_documento.DbType = DbType.String;

                        DbParameter p_VALOR_CLIENTE = cmdTransaccionFactory.CreateParameter();
                        p_VALOR_CLIENTE.ParameterName = "p_VALOR_CLIENTE";
                        p_VALOR_CLIENTE.Value = 0;
                        p_VALOR_CLIENTE.DbType = DbType.Decimal;
                        p_VALOR_CLIENTE.Direction = ParameterDirection.Output;

                        DbParameter p_VALOR_ENTIDAD = cmdTransaccionFactory.CreateParameter();
                        p_VALOR_ENTIDAD.ParameterName = "p_VALOR_ENTIDAD";
                        p_VALOR_ENTIDAD.Value = 0;
                        p_VALOR_ENTIDAD.DbType = DbType.Decimal;
                        p_VALOR_ENTIDAD.Direction = ParameterDirection.Output;

                        DbParameter rn_sobrante = cmdTransaccionFactory.CreateParameter();
                        rn_sobrante.ParameterName = "rn_sobrante";
                        rn_sobrante.Value = 0;
                        rn_sobrante.DbType = DbType.Decimal;
                        rn_sobrante.Direction = ParameterDirection.Output;

                        DbParameter n_error = cmdTransaccionFactory.CreateParameter();
                        n_error.ParameterName = "n_error";
                        n_error.Value = 0;
                        n_error.DbType = DbType.Decimal;
                        n_error.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pn_num_producto);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_cliente);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_ope);
                        cmdTransaccionFactory.Parameters.Add(pf_fecha_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_usu);
                        cmdTransaccionFactory.Parameters.Add(pn_documento);
                        cmdTransaccionFactory.Parameters.Add(p_VALOR_CLIENTE);
                        cmdTransaccionFactory.Parameters.Add(p_VALOR_ENTIDAD);
                        cmdTransaccionFactory.Parameters.Add(rn_sobrante);
                        cmdTransaccionFactory.Parameters.Add(n_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_CRE_PAGO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "AplicarCréditoDebAhorros", ex);
                        return false;
                    }
                }
            }
        }

        public List<AhorroVista> ListarAhorroVistaReporteCierre(DateTime pFecha, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT h.cod_oficina,p.cod_persona,p.Identificacion,p.apellidos,p.nombres,h.numero_cuenta,h.cod_linea_ahorro,l.descripcion as nom_linea_ahorro,"
                                    + "a.fecha_apertura,h.valor_cuota,h.saldo_total,h.fecha_proximo_pago,h.fecha_ultimo_pago,d.descripcion periodicidad,a.tasa,a.fecha_interes,"
                                    + "h.interes_causado,h.estado "
                                    + "from historico_ahorro h inner join ahorro_vista a on h.numero_cuenta=a.numero_cuenta "
                                    + "inner join v_persona p on a.cod_persona = p.cod_persona "
                                    + "Left join lineaahorro l on h.cod_linea_ahorro=l.cod_linea_ahorro "
                                    + "Left join periodicidad d on h.cod_periodicidad = d.cod_periodicidad ";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = sql + " where h.fecha_historico = to_date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                        else
                            sql = sql + " where h.fecha_historico = '" + pFecha + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["NOM_LINEA_AHORRO"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA_AHORRO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["INTERES_CAUSADO"] != DBNull.Value) entidad.interes = Convert.ToDecimal(resultado["INTERES_CAUSADO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            lstAhorroVista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarAhorroVistaReporteCierre", ex);
                        return null;
                    }
                }
            }
        }

        public AhorroVista ConsultarCuentaAhorroVista(string pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            AhorroVista entidad = new AhorroVista();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT AHORRO_VISTA.*, CALCULAR_CUOTA_AHORRO_VISTA(valor_cuota) as CUOTA_CALCULADA FROM AHORRO_VISTA WHERE NUMERO_CUENTA = '" + pId.ToString() + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToInt32(resultado["MODALIDAD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                            if (resultado["COD_FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["COD_FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["CUOTA_CALCULADA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["CUOTA_CALCULADA"]);
                            // if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToString(resultado["COD_PERIODICIDAD"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt32(resultado["COD_ASESOR"]);
                            // if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
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
                        BOExcepcion.Throw("AhorroVistaData", "ConsultarCuentaAhorroVista", ex);
                        return null;
                    }
                }
            }
        }


        public void AplicarCruce_Ahs_Pro(AhorroVista pAhorroVista, Int64 pCod_Ope, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter P_COD_OPE = cmdTransaccionFactory.CreateParameter();
                        P_COD_OPE.ParameterName = "P_COD_OPE";
                        P_COD_OPE.Value = pCod_Ope;
                        P_COD_OPE.Direction = ParameterDirection.Input;
                        P_COD_OPE.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_COD_OPE);

                        DbParameter P_NUM_CUENTA = cmdTransaccionFactory.CreateParameter();
                        P_NUM_CUENTA.ParameterName = "P_NUM_CUENTA";
                        P_NUM_CUENTA.Value = pAhorroVista.numero_cuenta;
                        P_NUM_CUENTA.Direction = ParameterDirection.Input;
                        P_NUM_CUENTA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_NUM_CUENTA);

                        DbParameter P_VALOR_APLICA = cmdTransaccionFactory.CreateParameter();
                        P_VALOR_APLICA.ParameterName = "P_VALOR_APLICA";
                        P_VALOR_APLICA.Value = pAhorroVista.valor_a_aplicar;
                        P_VALOR_APLICA.Direction = ParameterDirection.Input;
                        P_VALOR_APLICA.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR_APLICA);

                        DbParameter P_TIPO_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_PRODUCTO.ParameterName = "P_TIPO_PRODUCTO";
                        P_TIPO_PRODUCTO.Value = pAhorroVista.tipo_producto;
                        P_TIPO_PRODUCTO.Direction = ParameterDirection.Input;
                        P_TIPO_PRODUCTO.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_PRODUCTO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRUCE_AHO_PROD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "AplicarCruce_Ahs_Pro", ex);
                    }
                }
            }
        }


        public AhorroVista ConsultaAhorroVista(string pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            AhorroVista entidad = new AhorroVista();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT AHORRO_VISTA.* FROM AHORRO_VISTA WHERE NUMERO_CUENTA = '" + pId.ToString() + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToInt32(resultado["MODALIDAD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                            if (resultado["COD_FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["COD_FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt32(resultado["COD_ASESOR"]);
                        }
                        else
                        {
                            dbConnectionFactory.CerrarConexion(connection);
                            return null;
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

        public AhorroVista ConsultaAhorroVista(string pId, ref DbConnection pconnection, Usuario pUsuario)
        {
            DbDataReader resultado;
            AhorroVista entidad = new AhorroVista();
            using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
            {
                try
                {
                    string sql = @"SELECT AHORRO_VISTA.* FROM AHORRO_VISTA WHERE NUMERO_CUENTA = '" + pId.ToString() + "' ";
                    if (pconnection.State == ConnectionState.Closed)
                        pconnection.Open();
                    cmdTransaccionFactory.Connection = pconnection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();
                    if (resultado.Read())
                    {
                        if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                        if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                        if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                        if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                        if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                        if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                        if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToInt32(resultado["MODALIDAD"]);
                        if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                        if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                        if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                        if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                        if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                        if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                        if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                        if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                        if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                        if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                        if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                        if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                        if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                        if (resultado["COD_FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["COD_FORMA_PAGO"]);
                        if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                        if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                        if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt32(resultado["COD_ASESOR"]);
                    }
                    else
                    {
                        return null;
                    }
                    return entidad;
                }
                catch
                {
                    return null;
                }
            }
        }


        public List<AhorroVista> ListarCuentaAhorroVistaGiro(long cod, Usuario usuario)
        {
            DbDataReader resultado;
            List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select NUMERO_CUENTA from ahorro_vista where estado in(0,1) and  saldo_total >= 0  and cod_persona = " + cod;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();

                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);

                            lstAhorroVista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarCuentaAhorroVistaGiro", ex);
                        return null;
                    }
                }
            }
        }
        public List<AhorroVista> ListarAhorroClubAhorradores(Int64 pcliente, Boolean pResult, string pFiltroAdd, Usuario vUsuario)
        {
            DbDataReader resultado;

            List<AhorroVista> lstAporte = new List<AhorroVista>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = string.Empty;
                        if (pResult)
                        {
                            sql = @"  SELECT DISTINCT Ahorro_vista.*, lineaahorro.cod_linea_ahorro,lineaahorro.Descripcion as nom_linea,  (SELECT e.nom_empresa FROM empresa_recaudo e WHERE e.cod_empresa = Ahorro_vista.cod_empresa) AS nom_empresa,   
                                    CASE nov.estado WHEN '0' THEN 'Solicitado' WHEN '1' THEN 'Aprobado' WHEN '2' THEN 'Negado' ELSE ' ' END AS Estado_modificacion, 'PROPIO' AS TIPO_APORTE,SALDO_ACUMULADO(3,Ahorro_vista.NUMERO_CUENTA) AS VALOR_ACUMULADO
                                        FROM Ahorro_vista 
                                        INNER JOIN lineaahorro ON Ahorro_vista.cod_linea_ahorro = lineaahorro.cod_linea_ahorro
                                        LEFT JOIN Novedad_Cambio_Ahorro nov ON Ahorro_vista.NUMERO_CUENTA = nov.NUMERO_CUENTA
                                        WHERE Ahorro_vista.cod_persona = " + pcliente;
                            if (!string.IsNullOrWhiteSpace(pFiltroAdd))
                                sql += " " + pFiltroAdd;
                            sql += " order by lineaahorro.cod_linea_ahorro";
                        }


                        /*if (pFiltroAdd != null)
                            sql += " And v_aportes.estado in (" + pFiltroAdd.ToString() + ")" + " order by lineaaporte.cod_linea_aporte";
                            */
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["nom_linea"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["nom_linea"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["COD_FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["COD_FORMA_PAGO"]);
                            if (entidad.cod_forma_pago == 1)
                                entidad.nom_formapago = "Caja";
                            if (entidad.cod_forma_pago == 2)
                                entidad.nom_formapago = "Nomina";
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            //if (resultado["FECHA_ULTIMA_MOD"] != DBNull.Value) entidad.fecha_ultima_mod = Convert.ToDateTime(resultado["FECHA_ULTIMA_MOD"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                            //if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            //if (resultado["PORCENTAJE_DISTRIBUCION"] != DBNull.Value) entidad.dit = Convert.ToDecimal(resultado["PORCENTAJE_DISTRIBUCION"]);
                            //if (resultado["BASE_CUOTA"] != DBNull.Value) entidad.cuo = Convert.ToInt32(resultado["BASE_CUOTA"]);
                            //if (resultado["VALOR_BASE"] != DBNull.Value) entidad.valor_base = Convert.ToDecimal(resultado["VALOR_BASE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["Estado_Modificacion"] != DBNull.Value) entidad.estado_modificacion = Convert.ToString(resultado["Estado_Modificacion"]);
                            if (entidad.estado == 1)
                            {
                                entidad.estado_Linea = "ACTIVA";
                            }
                            if (entidad.estado == 2)
                            {
                                entidad.estado_Linea = "INACTIVA";
                            }
                            if (entidad.estado == 3)
                            {
                                entidad.estado_Linea = "CERRADA";
                            }
                            //if (resultado["COD_USUARIO"] != DBNull.Value) entidad. = Convert.ToInt32(resultado["COD_USUARIO"]);
                            //if (resultado["FECHA_CREA"] != DBNull.Value) entidad.fec_realiza = Convert.ToDateTime(resultado["FECHA_CREA"]);
                            //if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            //if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa_reca = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            //if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            //if (resultado["VALORAPAGAR"] != DBNull.Value) entidad.valor_a_aplicar = Convert.ToDecimal(resultado["VALORAPAGAR"]);
                            //if (resultado["TIPO_APORTE"] != DBNull.Value) entidad.tipo_registro = Convert.ToString(resultado["TIPO_APORTE"]);
                            //if (resultado["VALOR_ACUMULADO"] != DBNull.Value) entidad.valor_acumulado = Convert.ToDecimal(resultado["VALOR_ACUMULADO"]);
                            //if (entidad.Saldo > 0)
                            //{
                            //    entidad.valor_total_acumu = (entidad.valor_acumulado + entidad.Saldo);
                            //}
                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarAhorroClubAhorradores", ex);
                        return null;
                    }
                }
            }
        }
        public AhorroVista CrearNovedadCambio(AhorroVista vAhorro, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idnovedad = cmdTransaccionFactory.CreateParameter();
                        p_idnovedad.ParameterName = "p_idnovedad";
                        p_idnovedad.Value = vAhorro.id_novedad_cambio;
                        p_idnovedad.Direction = ParameterDirection.Output;
                        p_idnovedad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idnovedad);

                        DbParameter p_numero_aporte = cmdTransaccionFactory.CreateParameter();
                        p_numero_aporte.ParameterName = "p_numero_cuenta";
                        p_numero_aporte.Value = vAhorro.numero_cuenta;
                        p_numero_aporte.Direction = ParameterDirection.Input;
                        p_numero_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numero_aporte);

                        DbParameter p_fecha = cmdTransaccionFactory.CreateParameter();
                        p_fecha.ParameterName = "p_fecha_cambio_des";
                        if (vAhorro.fecha_empieza_cambio == null)
                            p_fecha.Value = DBNull.Value;
                        else
                            p_fecha.Value = vAhorro.fecha_empieza_cambio;
                        p_fecha.Direction = ParameterDirection.Input;
                        p_fecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha);

                        DbParameter p_val_anterior = cmdTransaccionFactory.CreateParameter();
                        p_val_anterior.ParameterName = "p_val_anterior";
                        p_val_anterior.Value = vAhorro.cuota;
                        p_val_anterior.Direction = ParameterDirection.Input;
                        p_val_anterior.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_val_anterior);

                        DbParameter p_val_nuevo = cmdTransaccionFactory.CreateParameter();
                        p_val_nuevo.ParameterName = "p_val_nuevo";
                        p_val_nuevo.Value = vAhorro.nuevo_valor_cuota;
                        p_val_nuevo.Direction = ParameterDirection.Input;
                        p_val_nuevo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_val_nuevo);

                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";

                        if (!string.IsNullOrWhiteSpace(vAhorro.observaciones))
                        {
                            p_observaciones.Value = vAhorro.observaciones;
                        }
                        else
                        {
                            p_observaciones.Value = DBNull.Value;
                        }
                        p_observaciones.Direction = ParameterDirection.Input;
                        p_observaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_observaciones);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = "0"; // Solicitado
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        DbParameter p_fecha_novedad = cmdTransaccionFactory.CreateParameter();
                        p_fecha_novedad.ParameterName = "p_fecha_novedad";
                        p_fecha_novedad.Value = DateTime.Today;
                        p_fecha_novedad.Direction = ParameterDirection.Input;
                        p_fecha_novedad.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_novedad);

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = vAhorro.cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_NOVEDAD_CA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        vAhorro.id_novedad_cambio = p_idnovedad.Value != DBNull.Value ? Convert.ToInt64(p_idnovedad.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);
                        return vAhorro;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "CrearNovedadCambio", ex);
                        return null;
                    }
                }
            }
        }


        public AhorroVista ConsultarCierreAhorroVista(Usuario vUsuario)
        {
            DbDataReader resultado;
            AhorroVista entidad = new AhorroVista();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT MAX(FECHA) as fecha,estado FROM CIEREA WHERE TIPO = 'H' AND ESTADO = 'D'   group by estado";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estadocierre = Convert.ToString(resultado["ESTADO"]);

                        }
                        else
                        {
                            dbConnectionFactory.CerrarConexion(connection);
                            return null;
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
        public void ModificarNovedadCuotaAhorro(AhorroVista vprogramado, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idnovedad = cmdTransaccionFactory.CreateParameter();
                        p_idnovedad.ParameterName = "p_idnovedad";
                        p_idnovedad.Value = vprogramado.id_novedad_cambio;
                        p_idnovedad.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_idnovedad);

                        DbParameter p_numero_aporte = cmdTransaccionFactory.CreateParameter();
                        p_numero_aporte.ParameterName = "p_numero_cuenta";
                        p_numero_aporte.Value = vprogramado.numero_cuenta;
                        p_numero_aporte.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_numero_aporte);

                        DbParameter p_fecha = cmdTransaccionFactory.CreateParameter();
                        p_fecha.ParameterName = "p_fecha_cambio_deseada";
                        if (vprogramado.fecha_empieza_cambio == null || vprogramado.fecha_empieza_cambio == DateTime.MinValue)
                        {
                            p_fecha.Value = DBNull.Value;
                        }
                        else
                        {
                            p_fecha.Value = vprogramado.fecha_empieza_cambio;
                        }

                        p_fecha.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_fecha);

                        DbParameter p_val_nuevo = cmdTransaccionFactory.CreateParameter();
                        p_val_nuevo.ParameterName = "p_val_nuevo";
                        p_val_nuevo.Value = vprogramado.nuevo_valor_cuota;
                        p_val_nuevo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_val_nuevo);

                        DbParameter p_val_anterior = cmdTransaccionFactory.CreateParameter();
                        p_val_anterior.ParameterName = "p_val_anterior";
                        p_val_anterior.Value = vprogramado.cuota;
                        p_val_anterior.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_val_anterior);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = vprogramado.estado_modificacion; // Aprobado - Negado (1 - 2)
                        p_estado.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";
                        p_observaciones.Value = vprogramado.observaciones;
                        p_observaciones.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_observaciones);


                        DbParameter p_usuario = cmdTransaccionFactory.CreateParameter();
                        p_usuario.ParameterName = "p_usuario";
                        p_usuario.Value = 0;
                        p_usuario.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_usuario);

                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_NOVEDAD_CA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ModificarNovedadCuotaAhorro", ex);
                    }
                }
            }
        }
        public bool? ValidarFechaSolicitudCambio(AhorroVista pProgramado, Usuario usuario)
        {
            DbDataReader resultado;
            bool valido = true;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select MAX(periodo_corte) as periodo_corte from empresa_novedad where cod_empresa = " + pProgramado.cod_empresa_reca;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            DateTime periodoCorte = DateTime.MinValue;

                            if (resultado["periodo_corte"] != DBNull.Value) periodoCorte = Convert.ToDateTime(resultado["periodo_corte"]);
                            string periododeCorte = periodoCorte.ToString("d");
                            sql = "SELECT Estado  FROM empresa_novedad WHERE periodo_corte = To_Date('" + periododeCorte + "','dd/MM/yyyy') and cod_empresa=" + pProgramado.cod_empresa_reca;
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();
                            if (resultado.Read())
                            {
                                int Estado = 0;

                                if (resultado["Estado"] != DBNull.Value) Estado = Convert.ToInt32(resultado["Estado"]);

                                if (periodoCorte >= pProgramado.fecha_empieza_cambio || Estado == 1)
                                {
                                    valido = false;
                                }
                            }

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return valido;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ValidarFechaSolicitudCambio", ex);
                        return null;
                    }
                }
            }
        }
        public List<AhorroVista> ListarAhorroNovedadesCambio(string filtro, Usuario usuario)
        {
            DbDataReader resultado;
            List<AhorroVista> lstEntidad = new List<AhorroVista>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @" SELECT DISTINCT ahorro_vista.*, nov.fecha_novedad, nov.idnovedad, nov.val_nuevo, nov.fecha_cambio_deseada, nov.observaciones as ob,
                                        CASE nov.estado WHEN '0' THEN 'Solicitado' WHEN '1' THEN 'Aprobado' WHEN '2' THEN 'Negado' ELSE ' ' END AS Estado_modificacion,
                                        lineaahorro.descripcion
                                        FROM ahorro_vista 
                                        INNER JOIN lineaahorro ON ahorro_vista.cod_linea_ahorro = lineaahorro.cod_linea_ahorro      
                                        JOIN novedad_cambio_ahorro nov ON ahorro_vista.numero_cuenta= nov.NUMERO_cuenta
                                        WHERE ahorro_vista.estado = 1 " + filtro + " order by nov.fecha_novedad desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();
                            if (resultado["idnovedad"] != DBNull.Value) entidad.id_novedad_cambio = Convert.ToInt64(resultado["idnovedad"]);
                            if (resultado["numero_cuenta"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["numero_cuenta"]);
                            if (resultado["cod_linea_ahorro"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["cod_linea_ahorro"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["descripcion"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["val_nuevo"] != DBNull.Value) entidad.nuevo_valor_cuota = Convert.ToDecimal(resultado["val_nuevo"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["fecha_cambio_deseada"] != DBNull.Value) entidad.fecha_empieza_cambio = Convert.ToDateTime(resultado["fecha_cambio_deseada"]);
                            if (resultado["fecha_novedad"] != DBNull.Value) entidad.fecha_novedad_cambio = Convert.ToDateTime(resultado["fecha_novedad"]);
                            //if (resultado["FECHA_ULTIMA_MOD"] != DBNull.Value) entidad.fecha_ultima_mod = Convert.ToDateTime(resultado["FECHA_ULTIMA_MOD"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["Estado_Modificacion"] != DBNull.Value) entidad.estado_modificacion = Convert.ToString(resultado["Estado_Modificacion"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa_reca = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            //if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            //if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            //if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["ob"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["ob"]);

                            lstEntidad.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ListarAhorroNovedadesCambio", ex);
                        return null;
                    }
                }
            }
        }

        public List<SolicitudProductosWeb> ListarSolicitudCreditoAAC(string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SolicitudProductosWeb> lstSolicitudCreditosRecogidos = new List<SolicitudProductosWeb>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT S.*,P.DESCRIPCION,CASE S.TIPOAHORRO WHEN '1' THEN LINEAAHORRO.DESCRIPCION WHEN '2' THEN LINEAPROGRAMADO.NOMBRE WHEN '3' THEN  LINEACDAT.DESCRIPCION END AS NOM_LINEA,
                                        CASE S.COD_PERSONA WHEN NULL THEN A.IDENTIFICACION  ELSE V.IDENTIFICACION END AS IDENTIFICACION,
                                        CASE S.COD_PERSONA WHEN NULL THEN (SELECT DESCRIPCION FROM TIPOIDENTIFICACION X WHERE X.CODTIPOIDENTIFICACION = A.TIPO_IDENTIFICACION)  
                                        ELSE (SELECT DESCRIPCION FROM TIPOIDENTIFICACION X WHERE X.CODTIPOIDENTIFICACION = V.TIPO_IDENTIFICACION) END AS TIPO_IDENTIFICACION,
                                        CASE S.COD_PERSONA WHEN NULL THEN TRIM(SUBSTR(A.PRIMER_NOMBRE || ' ' || A.SEGUNDO_NOMBRE || ' ' ||  A.PRIMER_APELLIDO || ' ' || A.SEGUNDO_APELLIDO, 0, 240)) 
                                        ELSE V.NOMBRE END AS NOM_PERSONA,case S.MODALIDAD WHEN 1 THEN 'I' WHEN 2 THEN 'C' WHEN 3 THEN 'A' ELSE '' END AS COD_MODALIDAD,
                                        CASE S.ESTADO WHEN 1 THEN 'APROBADO' ELSE 'PENDIENTE' END AS ESTADOS   
                                        FROM solicitudproductoweb S LEFT JOIN PERIODICIDAD P ON P.COD_PERIODICIDAD = S.PERIODICIDAD
                                        LEFT OUTER JOIN lineaahorro ON lineaahorro.COD_LINEA_AHORRO = S.COD_LINEAAHORRO
                                        LEFT OUTER JOIN lineaprogramado ON lineaprogramado.COD_LINEA_PROGRAMADO = S.COD_LINEAAHORRO
                                        LEFT OUTER JOIN LINEACDAT  ON LINEACDAT.COD_LINEACDAT = S.COD_LINEAAHORRO
                                        LEFT JOIN SOLICITUD_PERSONA_AFI A ON A.ID_PERSONA = S.COD_PERSONA
                                        LEFT JOIN V_PERSONA V ON V.COD_PERSONA = S.COD_PERSONA " + pFiltro + " ORDER BY 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            SolicitudProductosWeb entidad = new SolicitudProductosWeb();
                            if (resultado["IDSOLICITUD"] != DBNull.Value) entidad.IDSOLICITUD = Convert.ToInt32(resultado["IDSOLICITUD"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.FECHA = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.COD_PERSONA = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.VALOR_CUOTA = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.PLAZO = Convert.ToInt32(resultado["PLAZO"]);
                            //if (resultado["PERIODICIDAD"] != DBNull.Value) entidad. = convert.toint32(resultado["tipocredito"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.PERIODICIDAD = Convert.ToInt32(resultado["PERIODICIDAD"]);
                            if (resultado["TIPOAHORRO"] != DBNull.Value) entidad.TIPOAHORRO = Convert.ToString(resultado["TIPOAHORRO"]);
                            if (resultado["COD_MODALIDAD"] != DBNull.Value) entidad.COD_MODALIDA = Convert.ToString(resultado["COD_MODALIDAD"]);
                            //if (resultado["OTROMEDIO"] != DBNull.Value) entidad.otromedio = Convert.ToString(resultado["OTROMEDIO"]);
                            //if (resultado["USUARIO"] != DBNull.Value) entidad.usuario = Convert.ToString(resultado["USUARIO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.OFICINA = Convert.ToInt32(resultado["COD_OFICINA"]);
                            //if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);
                            //if (resultado["GARANTIA"] != DBNull.Value) entidad.garantia = Convert.ToInt32(resultado["GARANTIA"]);
                            //if (resultado["GARANTIA_COMUNITARIA"] != DBNull.Value) entidad.garantia_comunitaria = Convert.ToInt32(resultado["GARANTIA_COMUNITARIA"]);
                            //if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.FORMA_PAGO = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            //if (resultado["EMPRESA_RECAUDO"] != DBNull.Value) entidad.empresa_recaudo = Convert.ToInt32(resultado["EMPRESA_RECAUDO"]);
                            //if (resultado["DESTINO"] != DBNull.Value) entidad.destino = Convert.ToInt32(resultado["DESTINO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.COD_PERSONA = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_LINEAAHORRO"] != DBNull.Value) entidad.COD_LINEAAHORRO = Convert.ToString(resultado["COD_LINEAAHORRO"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.MODALIDAD = Convert.ToInt32(resultado["MODALIDAD"]);
                            //if (resultado["CODCIUDAD_PROPIEDAD"] != DBNull.Value) entidad.codciudad_propiedad = Convert.ToInt64(resultado["CODCIUDAD_PROPIEDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.DESCRIPCION = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.Nom_Linea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOM_PERSONA"] != DBNull.Value) entidad.nom_persona = Convert.ToString(resultado["NOM_PERSONA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.ESTADO = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["ESTADOS"] != DBNull.Value) entidad.ESTADOS = Convert.ToString(resultado["ESTADOS"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.COD_ASESOR = Convert.ToInt32(resultado["COD_ASESOR"]);
                            lstSolicitudCreditosRecogidos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSolicitudCreditosRecogidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCreditosRecogidosData", "ListarSolicitudCreditoAAC", ex);
                        return null;
                    }
                }
            }
        }


        //Consulta y retorna lista de solicitudes de retiro de ahorro a la vista
        public List<AhorroVista> ListarSolicitudRetiro(string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AhorroVista> lstSolicitudRetiros = new List<AhorroVista>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select
                                          s.ID_RETIRO_AHORROS SOLICITUD, s.FECHA_SOLICITUD, s.NUMERO_CUENTA, s.VALOR, p.COD_PERSONA,s.fecha_estado AS Fecha_estado,
                                          p.IDENTIFICACION, p.PRIMER_NOMBRE||' '||p.SEGUNDO_NOMBRE||' '||p.PRIMER_APELLIDO||' '||p.SEGUNDO_APELLIDO NOMBRE,
                                          l.DESCRIPCION, v.SALDO_TOTAL, v.SALDO_CANJE,
                                          case v.ESTADO WHEN 0 THEN 'APERTURA' WHEN 1 THEN 'ACTIVA' WHEN 2 THEN 'INACTIVO' WHEN 3 THEN 'CERRADA' end ESTADO_CUENTA,
                                          case s.FORMA_DESEMBOLSO WHEN 1 THEN 'EFECTIVO' WHEN 2 THEN 'CHEQUE' WHEN 3 THEN 'TRANFERENCIA' end FORM_GIRO, s.FORMA_DESEMBOLSO GIRO,
                                          b.NOMBREBANCO, b.COD_BANCO,s.CUENTA_BANCARIA,
                                          case s.TIPO_CUENTA WHEN 0 THEN 'AHORROS' WHEN 1 THEN 'CORRIENTE' end TIPO_CUENTA, s.TIPO_CUENTA CUENTA_BANCO,
                                          case s.ESTADO WHEN '0' THEN 'Solicitado' WHEN '1' THEN 'Autorizado' WHEN '2' THEN 'Rechazado' WHEN '4' THEN 'Pre-aprobado' end ESTADO_SOLICITUD,
                                          s.REQ_CANCELACION,
                                          tp.descripcion tipo_producto, tp.cod_tipo_producto, s.comentario, S.Aprobador
                                          ,aj.snombre1||' '||aj.Sapellido1||' '||aj.Sapellido2 as NOMBREEJE, sysdate as FECHA_APROBACION
                                       from
                                          persona p
                                          inner join solicitud_retiro_ahorros s on s.COD_PERSONA = p.COD_PERSONA
                                          Inner Join TIPOPRODUCTO tp On s.TIPO_PRODUCTO = tp.COD_TIPO_PRODUCTO
                                          left join ahorro_vista v on s.NUMERO_CUENTA = v.NUMERO_CUENTA
                                          left join lineaahorro l on v.COD_LINEA_AHORRO = l.COD_LINEA_AHORRO
                                          left join bancos b on s.COD_BANCO = b.COD_BANCO
                                          left join Asejecutivos aj on Aj.Icodigo = v.Cod_Asesor
                                       where s.VALOR > 0 " + pFiltro + " ORDER BY 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();                            
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["SOLICITUD"] != DBNull.Value) entidad.id_solicitud = Convert.ToInt32(resultado["SOLICITUD"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad. retiro = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);                           
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToInt64(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToInt64(resultado["SALDO_CANJE"]);                            
                            if (resultado["ESTADO_CUENTA"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["ESTADO_CUENTA"]);
                            if (resultado["FORM_GIRO"] != DBNull.Value) entidad.nom_giro = Convert.ToString(resultado["FORM_GIRO"]);
                            if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.nom_banco = Convert.ToString(resultado["NOMBREBANCO"]);
                            if (resultado["CUENTA_BANCARIA"] != DBNull.Value) entidad.numero_cuenta_final = Convert.ToString(resultado["CUENTA_BANCARIA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.nom_cuenta = Convert.ToString(resultado["TIPO_CUENTA"]);
                            if (resultado["ESTADO_SOLICITUD"] != DBNull.Value) entidad.estado_modificacion = Convert.ToString(resultado["ESTADO_SOLICITUD"]);
                            if (resultado["GIRO"] != DBNull.Value) entidad.forma_giro = Convert.ToInt32(resultado["GIRO"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["CUENTA_BANCO"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["CUENTA_BANCO"]);
                            if (resultado["REQ_CANCELACION"] != DBNull.Value) entidad.estadocierre = Convert.ToString(resultado["REQ_CANCELACION"]);
                            if (resultado["tipo_producto"] != DBNull.Value) entidad.nombre_producto = Convert.ToString(resultado["tipo_producto"]);
                            if (resultado["cod_tipo_producto"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt64(resultado["cod_tipo_producto"]);
                            if (resultado["comentario"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["comentario"]);
                            if (resultado["APROBADOR"] != DBNull.Value) entidad.aprobador = Convert.ToString(resultado["APROBADOR"]);
                            if (resultado["NOMBREEJE"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NOMBREEJE"]);
                            if (resultado["Fecha_estado"] != DBNull.Value) entidad.Fecha_estado = Convert.ToDateTime(resultado["Fecha_Estado"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            lstSolicitudRetiros.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSolicitudRetiros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudRetiroAhorrosData", "lstSolicitudRetiros", ex);
                        return null;
                    }
                }
            }
        }

        public SolicitudProductosWeb CrearConfirmacionProducto(SolicitudProductosWeb pProducto, Usuario vUsuario)
        { 
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_IDSOLICITUD = cmdTransaccionFactory.CreateParameter();
                        P_IDSOLICITUD.ParameterName = "P_IDSOLICITUD";
                        P_IDSOLICITUD.Value = pProducto.IDSOLICITUD;
                        P_IDSOLICITUD.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_IDSOLICITUD);

                        DbParameter P_TIPOAHORRO = cmdTransaccionFactory.CreateParameter();
                        P_TIPOAHORRO.ParameterName = "P_TIPOAHORRO";
                        P_TIPOAHORRO.Value = pProducto.TIPOAHORRO;
                        P_TIPOAHORRO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_TIPOAHORRO);

                        DbParameter P_NUMERO_AHORRO = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_AHORRO.ParameterName = "P_NUMERO_AHORRO";
                        P_NUMERO_AHORRO.Value = pProducto.NUM_CUENTA;
                        P_NUMERO_AHORRO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_AHORRO);

                        DbParameter P_COD_LINEA_AHORRO = cmdTransaccionFactory.CreateParameter();
                        P_COD_LINEA_AHORRO.ParameterName = "P_COD_LINEA_AHORRO";
                        P_COD_LINEA_AHORRO.Value = pProducto.COD_LINEAAHORRO;
                        P_COD_LINEA_AHORRO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_LINEA_AHORRO);

                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = pProducto.COD_PERSONA;
                        P_COD_PERSONA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                        DbParameter P_FECHA = cmdTransaccionFactory.CreateParameter();
                        P_FECHA.ParameterName = "P_FECHA";
                        P_FECHA.Value = pProducto.FECHA;
                        P_FECHA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA);

                        DbParameter P_VALOR_CUOTA = cmdTransaccionFactory.CreateParameter();
                        P_VALOR_CUOTA.ParameterName = "P_VALOR_CUOTA";
                        P_VALOR_CUOTA.Value = pProducto.VALOR_CUOTA;
                        P_VALOR_CUOTA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR_CUOTA);

                        DbParameter P_PERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        P_PERIODICIDAD.ParameterName = "P_PERIODICIDAD";
                        P_PERIODICIDAD.Value = pProducto.PERIODICIDAD;
                        P_PERIODICIDAD.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_PERIODICIDAD);

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = pProducto.ESTADO;
                        P_ESTADO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        DbParameter P_FORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FORMA_PAGO.ParameterName = "P_FORMA_PAGO";
                        P_FORMA_PAGO.Value = pProducto.FORMA_PAGO;
                        P_FORMA_PAGO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_FORMA_PAGO);

                        DbParameter P_MODALIDAD = cmdTransaccionFactory.CreateParameter();
                        P_MODALIDAD.ParameterName = "P_MODALIDAD";
                        P_MODALIDAD.Value = pProducto.MODALIDAD;
                        P_MODALIDAD.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_MODALIDAD);

                        DbParameter P_COD_OFICINA = cmdTransaccionFactory.CreateParameter();
                        P_COD_OFICINA.ParameterName = "P_COD_OFICINA";
                        P_COD_OFICINA.Value = pProducto.OFICINA;
                        P_COD_OFICINA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_OFICINA);

                        DbParameter P_PLAZO = cmdTransaccionFactory.CreateParameter();
                        P_PLAZO.ParameterName = "P_PLAZO";
                        P_PLAZO.Value = pProducto.PLAZO;
                        P_PLAZO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_PLAZO);

                        DbParameter P_FECHA_CIERRE = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_CIERRE.ParameterName = "P_FECHA_CIERRE";
                        P_FECHA_CIERRE.Value = pProducto.FECHA_CIERRE;
                        P_FECHA_CIERRE.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_CIERRE);

                        DbParameter P_SALDO_TOTAL = cmdTransaccionFactory.CreateParameter();
                        P_SALDO_TOTAL.ParameterName = "P_SALDO_TOTAL";
                        P_SALDO_TOTAL.Value = pProducto.SALDO_TOTAL;
                        P_SALDO_TOTAL.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_SALDO_TOTAL);

                        DbParameter P_SALDO_CANJE = cmdTransaccionFactory.CreateParameter();
                        P_SALDO_CANJE.ParameterName = "P_SALDO_CANJE";
                        P_SALDO_CANJE.Value = pProducto.SALDO_CANJE;
                        P_SALDO_CANJE.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_SALDO_CANJE);

                        //DbParameter P_FORMA_TASA = cmdTransaccionFactory.CreateParameter();
                        //P_FORMA_TASA.ParameterName = "P_FORMA_TASA";
                        //P_FORMA_TASA.Value = pProducto.FORMA_TASA;
                        //P_FORMA_TASA.Direction = ParameterDirection.Input;
                        //cmdTransaccionFactory.Parameters.Add(P_FORMA_TASA);

                        //DbParameter P_TIPO_TASA = cmdTransaccionFactory.CreateParameter();
                        //P_TIPO_TASA.ParameterName = "P_TIPO_TASA";
                        //P_TIPO_TASA.Value = pProducto.TIPO_TASA;
                        //P_TIPO_TASA.Direction = ParameterDirection.Input;
                        //cmdTransaccionFactory.Parameters.Add(P_TIPO_TASA);

                        //DbParameter P_TASA = cmdTransaccionFactory.CreateParameter();
                        //P_TASA.ParameterName = "P_TASA";
                        //P_TASA.Value = pProducto.TASA;
                        //P_TASA.Direction = ParameterDirection.Input;
                        //cmdTransaccionFactory.Parameters.Add(P_TASA);

                        DbParameter P_SALDO_INTERESES = cmdTransaccionFactory.CreateParameter();
                        P_SALDO_INTERESES.ParameterName = "P_SALDO_INTERESES";
                        P_SALDO_INTERESES.Value = pProducto.SALDO_INTERESES;
                        P_SALDO_INTERESES.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_SALDO_INTERESES);

                        DbParameter P_RETENCION = cmdTransaccionFactory.CreateParameter();
                        P_RETENCION.ParameterName = "P_RETENCION";
                        P_RETENCION.Value = pProducto.RETENCION;
                        P_RETENCION.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_RETENCION);

                        DbParameter P_COD_ASESOR = cmdTransaccionFactory.CreateParameter();
                        P_COD_ASESOR.ParameterName = "P_COD_ASESOR";
                        P_COD_ASESOR.Value = pProducto.COD_ASESOR;
                        P_COD_ASESOR.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_ASESOR);

                        DbParameter P_COD_EMPRESA = cmdTransaccionFactory.CreateParameter();
                        P_COD_EMPRESA.ParameterName = "P_COD_EMPRESA";
                        P_COD_EMPRESA.Value = pProducto.COD_EMPRESA;
                        P_COD_EMPRESA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_EMPRESA);

                        DbParameter P_TASA_INTERES = cmdTransaccionFactory.CreateParameter();
                        P_TASA_INTERES.ParameterName = "P_TASA_INTERES";
                        P_TASA_INTERES.Value = pProducto.TASA_INTERES;
                        P_TASA_INTERES.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_TASA_INTERES);

                        DbParameter P_FECHA_INTERES = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_INTERES.ParameterName = "P_FECHA_INTERES";
                        P_FECHA_INTERES.Value = pProducto.FECHA_INTERES;
                        P_FECHA_INTERES.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_INTERES);

                        DbParameter P_TOTAL_RETENCION = cmdTransaccionFactory.CreateParameter();
                        P_TOTAL_RETENCION.ParameterName = "P_TOTAL_RETENCION";
                        P_TOTAL_RETENCION.Value = pProducto.TOTAL_RETENCION;
                        P_TOTAL_RETENCION.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_TOTAL_RETENCION);

                        DbParameter p_CODIGO_CDAT = cmdTransaccionFactory.CreateParameter();
                        p_CODIGO_CDAT.ParameterName = "p_CODIGO_CDAT";
                        p_CODIGO_CDAT.Value = 0;
                        p_CODIGO_CDAT.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_CODIGO_CDAT);

                        DbParameter P_COD_PERIODICIDAD_INT = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERIODICIDAD_INT.ParameterName = "P_COD_PERIODICIDAD_INT";
                        P_COD_PERIODICIDAD_INT.Value = pProducto.COD_PERIODICIDAD_INT;
                        P_COD_PERIODICIDAD_INT.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERIODICIDAD_INT);

                        DbParameter P_COD_MODALIDAD = cmdTransaccionFactory.CreateParameter();
                        P_COD_MODALIDAD.ParameterName = "P_COD_MODALIDAD";
                        P_COD_MODALIDAD.Value = pProducto.COD_MODALIDA;
                        P_COD_MODALIDAD.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_MODALIDAD);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_DEP_CONFIRSOLICITUD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        //DAauditoria.InsertarLog(pAhorroVista, "ahorro_vista", vUsuario, Accion.Crear.ToString(), TipoAuditoria.AhorroVista, "Creacion de ahorro vista con numero de cuenta " + pAhorroVista.numero_cuenta); //REGISTRO DE AUDITORIA

                        return pProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "CrearConfirmacionProducto", ex);
                        return null;
                    }
                }
            }
        }
        public string MaxRegistro(string TipoAhorro, Usuario vUsuario)
        {
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = "";
                        if (TipoAhorro == "1")
                        {
                            sql = @"select max(TO_NUMBER(NUMERO_CUENTA))+1 as Maximo FROM AHORRO_VISTA";
                        }
                        if (TipoAhorro == "2")
                        {
                            sql = @"SELECT MAX(TO_NUMBER(numero_programado))+1 AS Maximo FROM ahorro_programado";
                        }
                        if (TipoAhorro == "3")
                        {
                            sql = @"select max(TO_NUMBER(Replace(numero_cdat,'AGREGAR AUTOGENERADO','0')))+1 as Maximo FROM cdat";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        string Numero_Producto = null;
                        if (resultado.Read())
                        {

                            if (resultado["Maximo"] != DBNull.Value) Numero_Producto = Convert.ToString(resultado["Maximo"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return Numero_Producto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "MaxRegistro", ex);
                        return null;
                    }
                }
            }
        }
        public List<AhorroVista> ListarAprobaciones(Usuario usuario, DateTime Fecha)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AhorroVista> lstAhorros = new List<AhorroVista>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT NUMERO_CUENTA,OBSERVACIONES,FECHA_APERTURA,'Ahorro a la Vista' as TipoProducto FROM AHORRO_VISTA WHERE OBSERVACIONES='APERTURA DE DEPOSITO APROBADA' and (SYSDATE -5)<FECHA_APERTURA
                                        UNION ALL
                                        SELECT NUMERO_PROGRAMADO,M.DESCRIPCION,FECHA_APERTURA,'Ahorro Programado' AS TipoProducto FROM AHORRO_PROGRAMADO A JOIN MOTIVO_PROGRAMADO M ON A.COD_MOTIVO_APERTURA=M.COD_MOTIVO_APERTURA WHERE A.COD_MOTIVO_APERTURA=101 and (SYSDATE -5)<FECHA_APERTURA
                                        UNION ALL 
                                        SELECT NUMERO_CDAT,OBSERVACION,FECHA_APERTURA,'CDAT' AS TipoProducto FROM CDAT WHERE OBSERVACION='APERTURA DE DEPOSITO APROBADA'
                                         and (SYSDATE -5)<FECHA_APERTURA ORDER BY fecha_apertura DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();

                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["TipoProducto"] != DBNull.Value) entidad.tipo_registro = Convert.ToString(resultado["TipoProducto"]);


                            lstAhorros.Add(entidad);
                        }
                        return lstAhorros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarAprobaciones", ex);
                        return null;
                    }
                }
            }
        }
        public List<AhorroVista> ListarAprobacionesCuota(Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AhorroVista> lstAhorros = new List<AhorroVista>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT To_char(Numero_aporte) as Numero_cuenta,fecha_novedad,estado,'Aporte' as TipoProducto FROM novedad_cambio WHERE (SYSDATE -10)<fecha_novedad and estado='1'
                                    UNION ALL
                                    SELECT Numero_cuenta,fecha_novedad,estado,'Ahorro a la Vista' as TipoProducto FROM novedad_cambio_ahorro WHERE (SYSDATE -10)<fecha_novedad and estado='1'
                                    UNION ALL
                                    select Numero_programado,fecha_novedad,estado,'Ahorro Programado' from novedad_cambio_progra WHERE (SYSDATE -5)<fecha_novedad and estado='1'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();

                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["Estado"] != DBNull.Value) entidad.estados = Convert.ToString(resultado["Estado"]);
                            if (resultado["fecha_novedad"] != DBNull.Value) entidad.fecha_novedad_cambio = Convert.ToDateTime(resultado["fecha_novedad"]);
                            if (resultado["TipoProducto"] != DBNull.Value) entidad.tipo_registro = Convert.ToString(resultado["TipoProducto"]);


                            lstAhorros.Add(entidad);
                        }
                        return lstAhorros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarAprobacionesCuota", ex);
                        return null;
                    }
                }
            }
        }


        //Consultar Cuentas por cobrar
        public List<CuentasCobrar> ListarCuentasCobrar(string numCuenta, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CuentasCobrar> lscuentasCobrar = new List<CuentasCobrar>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CUENTAPORCOBRAR_AHORRO WHERE NUMERO_CUENTA = '" + numCuenta + "' AND ESTADO = 0";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuentasCobrar entidad = new CuentasCobrar();
                            if (resultado["IDCUENTA"] != DBNull.Value) entidad.id_cuenta_cobrar = Convert.ToString(resultado["IDCUENTA"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.num_cuenta_producto = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToString(resultado["COD_OPE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            lscuentasCobrar.Add(entidad);
                        }
                        return lscuentasCobrar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarCuentasCobrar", ex);
                        return null;
                    }
                }
            }
        }

        //Procesos cuentas por cobrar/ crear, pagar
        public CuentasCobrar ProcesoCuentasCobrar(TransaccionCaja pTransaccionCaja, CuentasCobrar pCuentaCobrar, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_numero_producto = cmdTransaccionFactory.CreateParameter();
                        p_numero_producto.ParameterName = "p_num_producto";
                        p_numero_producto.Value = pCuentaCobrar.num_cuenta_producto;
                        p_numero_producto.Direction = ParameterDirection.Input;
                        p_numero_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_numero_producto);

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        p_cod_ope.Value = pTransaccionCaja.cod_ope;
                        p_cod_ope.Direction = ParameterDirection.Input;
                        p_cod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        DbParameter p_fecha_proceso = cmdTransaccionFactory.CreateParameter();
                        p_fecha_proceso.ParameterName = "p_fecha_proceso";
                        p_fecha_proceso.Value = pTransaccionCaja.fecha_movimiento;
                        p_fecha_proceso.Direction = ParameterDirection.Input;
                        p_fecha_proceso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_proceso);

                        DbParameter p_valor_proceso = cmdTransaccionFactory.CreateParameter();
                        p_valor_proceso.ParameterName = "p_valor_proceso";
                        p_valor_proceso.Value = pCuentaCobrar.saldo;
                        p_valor_proceso.Direction = ParameterDirection.Input;
                        p_valor_proceso.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor_proceso);

                        DbParameter p_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tran.ParameterName = "p_tipo_tran";
                        p_tipo_tran.Value = pCuentaCobrar.tipo_tran;
                        p_tipo_tran.Direction = ParameterDirection.Input;
                        p_tipo_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tran);

                        DbParameter p_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        p_cod_cliente.ParameterName = "p_cod_cliente";
                        p_cod_cliente.Value = pTransaccionCaja.cod_persona;
                        p_cod_cliente.Direction = ParameterDirection.Input;
                        p_cod_cliente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cliente);

                        DbParameter p_num_cta_cobrar = cmdTransaccionFactory.CreateParameter();
                        p_num_cta_cobrar.ParameterName = "p_num_cta_cobrar";
                        if (pCuentaCobrar.id_cuenta_cobrar == null)
                            p_num_cta_cobrar.Value = DBNull.Value;
                        else
                            p_num_cta_cobrar.Value = Convert.ToInt64(pCuentaCobrar.id_cuenta_cobrar);
                        p_num_cta_cobrar.Direction = ParameterDirection.Input;
                        p_num_cta_cobrar.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_num_cta_cobrar);

                        DbParameter p_num_tran_tarjeta = cmdTransaccionFactory.CreateParameter();
                        p_num_tran_tarjeta.ParameterName = "p_num_tran_tarjeta";
                        if (pCuentaCobrar.num_tran_tarjeta == null)
                            p_num_tran_tarjeta.Value = DBNull.Value;
                        else
                            p_num_tran_tarjeta.Value = Convert.ToInt64(pCuentaCobrar.num_tran_tarjeta);
                        p_num_tran_tarjeta.Direction = ParameterDirection.Input;
                        p_num_tran_tarjeta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_num_tran_tarjeta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_CTA_COBRAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pCuentaCobrar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ProcesoCuentasCobrar", ex);
                        return null;
                    }
                }
            }

        }



        public decimal ConsultarSaldoUtlPeriodoAhorroVista(string numeroCuenta, DateTime fechaInicio, Usuario usuario)
        {
            DbDataReader resultado;
            ReporteMovimiento entidad = new ReporteMovimiento();
            decimal saldoInicial = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT 
                                         (SELECT SUM(CASE tip.tipo_mov WHEN 1 THEN -t.valor WHEN 2 THEN t.valor END) 
                                          FROM tran_ahorro t JOIN operacion o ON t.cod_ope = o.cod_ope JOIN tipo_tran tip ON tip.tipo_tran = t.tipo_tran
                                          WHERE t.numero_cuenta = A.numero_cuenta AND TRUNC(o.fecha_oper) < to_date('" + fechaInicio.ToShortDateString() + @"', 'dd/MM/yyyy')) AS saldo_inicial
                                        FROM ahorro_vista a WHERE a.numero_cuenta = '" + numeroCuenta + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["saldo_inicial"] != DBNull.Value) saldoInicial = Convert.ToDecimal(resultado["saldo_inicial"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return saldoInicial;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ConsultarSaldoInicialPeriodoAhorroVista", ex);
                        return 0;
                    }
                }
            }
        }
        public decimal ConsultarSaldoFinalPeriodoAhorroVista(string numeroCuenta,Decimal valorinicial, DateTime fechaInicio, DateTime fechaFinal, Usuario usuario)
        {
            DbDataReader resultado;
            ReporteMovimiento entidad = new ReporteMovimiento();
            decimal saldoFinal = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"SELECT 
                                          NVL((SELECT SUM(CASE tip.tipo_mov WHEN 1 THEN -t.valor WHEN 2 THEN t.valor END) 
                                          FROM tran_ahorro t JOIN operacion o ON t.cod_ope = o.cod_ope JOIN tipo_tran tip ON tip.tipo_tran = t.tipo_tran
                                          WHERE t.numero_cuenta = A.numero_cuenta
                                          AND TRUNC(o.fecha_oper) >= to_date('" + fechaInicio.ToShortDateString() + @"', 'dd/MM/yyyy')
                                           AND TRUNC(o.fecha_oper) <= to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy')), 0)  AS saldo_inicial
                                          FROM ahorro_vista a WHERE a.numero_cuenta = '" + numeroCuenta + "'";

                    

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["saldo_inicial"] != DBNull.Value) saldoFinal = Convert.ToDecimal(resultado["saldo_inicial"]);
                            saldoFinal = saldoFinal + valorinicial;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return saldoFinal;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ConsultarSaldoFinalPeriodoAhorroVista", ex);
                        return 0;
                    }
                }
            }
        }

        public int CrearSolicitudRetiroAhorros(AhorroVista pAhorro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                                               
                        DbParameter P_ID_SOL_RETIRO = cmdTransaccionFactory.CreateParameter();
                        P_ID_SOL_RETIRO.ParameterName = "P_ID_SOL_RETIRO";
                        P_ID_SOL_RETIRO.Value = 0;
                        P_ID_SOL_RETIRO.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(P_ID_SOL_RETIRO);

                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = pAhorro.cod_persona;
                        P_COD_PERSONA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                        DbParameter P_NUMERO_CUENTA = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_CUENTA.ParameterName = "P_NUMERO_CUENTA";
                        P_NUMERO_CUENTA.Value = pAhorro.numero_cuenta;
                        P_NUMERO_CUENTA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_CUENTA);

                        DbParameter P_VALOR = cmdTransaccionFactory.CreateParameter();
                        P_VALOR.ParameterName = "P_VALOR";
                        P_VALOR.Value = pAhorro.retiro;
                        P_VALOR.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR);

                        DbParameter P_FORMA_DESEMBOLSO = cmdTransaccionFactory.CreateParameter();
                        P_FORMA_DESEMBOLSO.ParameterName = "P_FORMA_DESEMBOLSO";
                        P_FORMA_DESEMBOLSO.Value = pAhorro.forma_giro;
                        P_FORMA_DESEMBOLSO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_FORMA_DESEMBOLSO);
                                                
                        DbParameter P_COD_BANCO = cmdTransaccionFactory.CreateParameter();
                        P_COD_BANCO.ParameterName = "P_COD_BANCO";
                        P_COD_BANCO.Value = pAhorro.cod_banco;
                        P_COD_BANCO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_BANCO);

                        DbParameter P_CUENTA_BANCARIA = cmdTransaccionFactory.CreateParameter();
                        P_CUENTA_BANCARIA.ParameterName = "P_CUENTA_BANCARIA";
                        P_CUENTA_BANCARIA.Value = pAhorro.numero_cuenta_final;
                        P_CUENTA_BANCARIA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_CUENTA_BANCARIA);

                        DbParameter P_TIPO_CUENTA = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_CUENTA.ParameterName = "P_TIPO_CUENTA";
                        P_TIPO_CUENTA.Value = pAhorro.tipo_cuenta;
                        P_TIPO_CUENTA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_CUENTA);                        

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = pAhorro.estado;
                        P_ESTADO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        DbParameter P_REQ_CANCELACION = cmdTransaccionFactory.CreateParameter();
                        P_REQ_CANCELACION.ParameterName = "P_REQ_CANCELACION";
                        P_REQ_CANCELACION.Value = pAhorro.estadocierre;
                        P_REQ_CANCELACION.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_REQ_CANCELACION);

                        DbParameter P_TIPO_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_PRODUCTO.ParameterName = "P_TIPO_PRODUCTO";
                        P_TIPO_PRODUCTO.Value = pAhorro.tipo_producto;
                        P_TIPO_PRODUCTO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_PRODUCTO);

                        DbParameter P_COMENTARIO = cmdTransaccionFactory.CreateParameter();
                        P_COMENTARIO.ParameterName = "P_COMENTARIO";
                        P_COMENTARIO.Value = pAhorro.observaciones;
                        P_COMENTARIO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COMENTARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_SOLRETIRO_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if(P_ID_SOL_RETIRO != null)
                        {
                            return Convert.ToInt32(P_ID_SOL_RETIRO.Value);
                        }

                        return 0;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "CrearSolicitudRetiroAhorros", ex);
                        return 0;
                    }
                }
            }
        }

        public List<AhorroVista> ListarSolicitudProducto(string filtro, Usuario usuario)
        {
            DbDataReader resultado;
            List<AhorroVista> lstEntidad = new List<AhorroVista>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT
                                        S.ID_SOL_PRODUCTO,      S.COD_PERSONA,
                                        S.COD_TIPO_PRODUCTO,    S.LINEA,
                                        S.PLAZO,                S.NUM_CUOTAS,
                                        S.VALOR,                 S.PERIODICIDAD,
                                        S.FORMA_PAGO,            S.COD_DESTINO,
                                        S.FECHA_SOLICITUD,       S.ESTADO,
                                        S.CONSIGNACION,          S.DECLARACION,
                                        S.IDENTIFICACION_BEN,     S.NOMBRE_BEN,
                                        S.FECHA_NACIMIENTO_BEN,     S.PARENTESCO_BEN,
                                        CASE WHEN S.TIPO_PAGO=1 THEN 'Pago consignacion'
                                                WHEN S.TIPO_PAGO=2 THEN 'Pago PSE' ELSE '' END AS PAGO_REALIZADO_POR,
                                        S.ID_PAYMENT,
                                        CASE s.ESTADO WHEN '0' then 'Solicitado' WHEN '1' then 'Aprobado' WHEN '2' then 'Negado' WHEN '4' then 'Pre-aprobado' ELSE ' ' END AS Estado_solicitud,
                                        p.PRIMER_NOMBRE||' '||p.SEGUNDO_NOMBRE||' '||p.PRIMER_APELLIDO||' '||p.SEGUNDO_APELLIDO Nombres, p.IDENTIFICACION, t.DESCRIPCION
                                        ,aj.snombre1||' '||aj.Sapellido1||' '||aj.Sapellido2 as NOMBREEJE
                                        FROM SOLICITUD_PRODUCTO_WEB s
                                        INNER JOIN PERSONA p ON s.COD_PERSONA = p.COD_PERSONA
                                        INNER JOIN TIPOPRODUCTO t ON s.COD_TIPO_PRODUCTO = t.COD_TIPO_PRODUCTO
                                        LEFT JOIN Asejecutivos aj ON Aj.Icodigo = P.Cod_Asesor
                                       where s.COD_TIPO_PRODUCTO is not null " + filtro + "order by s.FECHA_SOLICITUD desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();
                            if (resultado["ID_SOL_PRODUCTO"] != DBNull.Value) entidad.id_solicitud = Convert.ToInt32(resultado["ID_SOL_PRODUCTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt64(resultado["COD_TIPO_PRODUCTO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["LINEA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["NUM_CUOTAS"] != DBNull.Value) entidad.num_cuotas = Convert.ToInt32(resultado["NUM_CUOTAS"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["PERIODICIDAD"]); 
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado_modificacion1 = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["ESTADO_SOLICITUD"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["ESTADO_SOLICITUD"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nombre_producto = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["IDENTIFICACION_BEN"] != DBNull.Value) entidad.identificacion_ben = Convert.ToString(resultado["IDENTIFICACION_BEN"]);
                            if (resultado["NOMBRE_BEN"] != DBNull.Value) entidad.nombres_ben = Convert.ToString(resultado["NOMBRE_BEN"]);
                            if (resultado["FECHA_NACIMIENTO_BEN"] != DBNull.Value) entidad.fecha_nacimiento_ben = Convert.ToDateTime(resultado["FECHA_NACIMIENTO_BEN"]);
                            if (resultado["PARENTESCO_BEN"] != DBNull.Value) entidad.parentesco_ben = Convert.ToInt32(resultado["PARENTESCO_BEN"]);
                            if (resultado["NOMBREEJE"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NOMBREEJE"]);
                            if (resultado["ID_PAYMENT"] != DBNull.Value) entidad.IdPayment = Convert.ToInt64(resultado["ID_PAYMENT"]);
                            if (resultado["PAGO_REALIZADO_POR"] != DBNull.Value) entidad.PagoRealizadoPor = Convert.ToString(resultado["PAGO_REALIZADO_POR"]);

                            lstEntidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarSolicitudProducto", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Lista los tipos de producto con al menos una solicitud de creación
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public List<AhorroVista> ListarTipoProductoConSolicitud(Usuario usuario)
        {
            DbDataReader resultado;
            List<AhorroVista> lstTipos = new List<AhorroVista>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select DISTINCT s.COD_TIPO_PRODUCTO, tp.DESCRIPCION
                                       From SOLICITUD_PRODUCTO_WEB s
                                       Inner Join TIPOPRODUCTO tp On s.COD_TIPO_PRODUCTO = tp.COD_TIPO_PRODUCTO
                                       Where s.ESTADO = 0";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        //Agrega un elemento por default
                        AhorroVista item = new AhorroVista();
                        item.tipo_producto = 0;
                        item.descripcion = "Todos";
                        lstTipos.Add(item);

                        //LLena el objeto con los datos obtenidos
                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();
                            if (resultado["COD_TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["COD_TIPO_PRODUCTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTipos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarTipoProductoConSolicitud", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Lista los tipos de producto con al menos una solicitud de creación
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public List<AhorroVista> ListarTipoProductoConSolicitudRetiro(Usuario usuario)
        {
            DbDataReader resultado;
            List<AhorroVista> lstTipos = new List<AhorroVista>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select DISTINCT s.TIPO_PRODUCTO COD_TIPO_PRODUCTO, tp.DESCRIPCION
                                       From solicitud_retiro_ahorros s
                                       Inner Join TIPOPRODUCTO tp On s.TIPO_PRODUCTO = tp.COD_TIPO_PRODUCTO
                                       Where s.ESTADO = 0";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        //Agrega un elemento por default
                        AhorroVista item = new AhorroVista();
                        item.tipo_producto = 0;
                        item.descripcion = "Todos";
                        lstTipos.Add(item);

                        //LLena el objeto con los datos obtenidos
                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();
                            if (resultado["COD_TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["COD_TIPO_PRODUCTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTipos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarTipoProductoConSolicitud", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Muestra los movimientos de una cuenta de aporte
        /// </summary>
        /// <param name="pNumeroAporte"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public bool ModificarEstadoSolicitudProducto(AhorroVista pAhorro, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_ID_SOL_RETIRO = cmdTransaccionFactory.CreateParameter();
                        P_ID_SOL_RETIRO.ParameterName = "P_ID_SOL_PRODUCTO";
                        P_ID_SOL_RETIRO.Value = pAhorro.id_solicitud;
                        P_ID_SOL_RETIRO.Direction = ParameterDirection.Input;
                        P_ID_SOL_RETIRO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ID_SOL_RETIRO);

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = pAhorro.estado_modificacion;
                        P_ESTADO.Direction = ParameterDirection.Input;
                        P_ESTADO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_SOLPRODUCTO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ModificarEstadoSolicitudProducto", ex);
                        return false;
                    }
                }
            }
        }

        
    }
}

