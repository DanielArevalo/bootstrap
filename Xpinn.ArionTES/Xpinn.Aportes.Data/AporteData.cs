using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla AporteS
    /// </summary>
    public class AporteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla AporteS
        /// </summary>
        public AporteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla AporteS de la base de datos
        /// </summary>
        /// <param name="pAporte">Entidad Aporte</param>
        /// <returns>Entidad Aporte creada</returns>
        public Aporte CrearAporte(Aporte pAporte, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_aporte = cmdTransaccionFactory.CreateParameter();
                        p_numero_aporte.ParameterName = "p_numero_aporte";
                        p_numero_aporte.Value = pAporte.numero_aporte;
                        p_numero_aporte.Direction = ParameterDirection.Output;
                        p_numero_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numero_aporte);

                        DbParameter p_cod_linea_aporte = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_aporte.ParameterName = "p_cod_linea_aporte";
                        p_cod_linea_aporte.Value = pAporte.cod_linea_aporte;
                        p_cod_linea_aporte.Direction = ParameterDirection.Input;
                        p_cod_linea_aporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_aporte);

                        DbParameter p_cod_oficina = cmdTransaccionFactory.CreateParameter();
                        p_cod_oficina.ParameterName = "p_cod_oficina";
                        if (pAporte.cod_oficina == 0)
                            p_cod_oficina.Value = DBNull.Value;
                        else
                            p_cod_oficina.Value = pAporte.cod_oficina;
                        p_cod_oficina.Direction = ParameterDirection.Input;
                        p_cod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_oficina);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pAporte.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pAporte.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter p_fecha_apertura = cmdTransaccionFactory.CreateParameter();
                        p_fecha_apertura.ParameterName = "p_fecha_apertura";
                        p_fecha_apertura.Value = pAporte.fecha_apertura;
                        p_fecha_apertura.Direction = ParameterDirection.Input;
                        p_fecha_apertura.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_apertura);

                        DbParameter p_cuota = cmdTransaccionFactory.CreateParameter();
                        p_cuota.ParameterName = "p_cuota";
                        p_cuota.Value = pAporte.cuota;
                        p_cuota.Direction = ParameterDirection.Input;
                        p_cuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_cuota);

                        DbParameter p_porcentaje_apo = cmdTransaccionFactory.CreateParameter();
                        p_porcentaje_apo.ParameterName = "p_porcentaje_apo";
                        if (pAporte.porcentaje_apo == null)
                            p_porcentaje_apo.Value = DBNull.Value;
                        else
                            p_porcentaje_apo.Value = pAporte.porcentaje_apo;
                        p_porcentaje_apo.Direction = ParameterDirection.Input;
                        p_porcentaje_apo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_porcentaje_apo);

                        DbParameter p_cod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        p_cod_periodicidad.ParameterName = "p_cod_periodicidad";
                        p_cod_periodicidad.Value = pAporte.cod_periodicidad;
                        p_cod_periodicidad.Direction = ParameterDirection.Input;
                        p_cod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_periodicidad);

                        DbParameter p_forma_pago = cmdTransaccionFactory.CreateParameter();
                        p_forma_pago.ParameterName = "p_forma_pago";
                        p_forma_pago.Value = pAporte.forma_pago;
                        p_forma_pago.Direction = ParameterDirection.Input;
                        p_forma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_forma_pago);

                        DbParameter p_fecha_proximo_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_proximo_pago.ParameterName = "p_fecha_proximo_pago";
                        p_fecha_proximo_pago.Value = pAporte.fecha_proximo_pago;
                        p_fecha_proximo_pago.Direction = ParameterDirection.Input;
                        p_fecha_proximo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_proximo_pago);

                        DbParameter p_fecha_ultimo_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_ultimo_pago.ParameterName = "p_fecha_ultimo_pago";
                        p_fecha_ultimo_pago.Value = pAporte.fecha_ultimo_pago;
                        p_fecha_ultimo_pago.Direction = ParameterDirection.Input;
                        p_fecha_ultimo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_ultimo_pago);

                        DbParameter p_fecha_cierre = cmdTransaccionFactory.CreateParameter();
                        p_fecha_cierre.ParameterName = "p_fecha_cierre";
                        p_fecha_cierre.Value = pAporte.fecha_cierre;
                        p_fecha_cierre.Direction = ParameterDirection.Input;
                        p_fecha_cierre.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_cierre);

                        DbParameter p_saldo = cmdTransaccionFactory.CreateParameter();
                        p_saldo.ParameterName = "p_saldo";
                        p_saldo.Value = pAporte.Saldo;
                        p_saldo.Direction = ParameterDirection.Input;
                        p_saldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_saldo);

                        DbParameter p_fecha_interes = cmdTransaccionFactory.CreateParameter();
                        p_fecha_interes.ParameterName = "p_fecha_interes";
                        p_fecha_interes.Value = pAporte.fecha_interes;
                        p_fecha_interes.Direction = ParameterDirection.Input;
                        p_fecha_interes.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_interes);

                        DbParameter p_total_intereses = cmdTransaccionFactory.CreateParameter();
                        p_total_intereses.ParameterName = "p_total_intereses";
                        p_total_intereses.Value = pAporte.total_intereses;
                        p_total_intereses.Direction = ParameterDirection.Input;
                        p_total_intereses.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_total_intereses);

                        DbParameter p_total_retencion = cmdTransaccionFactory.CreateParameter();
                        p_total_retencion.ParameterName = "p_total_retencion";
                        p_total_retencion.Value = pAporte.total_retencion;
                        p_total_retencion.Direction = ParameterDirection.Input;
                        p_total_retencion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_total_retencion);

                        DbParameter p_saldo_intereses = cmdTransaccionFactory.CreateParameter();
                        p_saldo_intereses.ParameterName = "p_saldo_intereses";
                        p_saldo_intereses.Value = pAporte.saldo_intereses;
                        p_saldo_intereses.Direction = ParameterDirection.Input;
                        p_saldo_intereses.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_saldo_intereses);

                        DbParameter p_porcentaje_distribucion = cmdTransaccionFactory.CreateParameter();
                        p_porcentaje_distribucion.ParameterName = "p_porcentaje_distribucion";
                        p_porcentaje_distribucion.Value = pAporte.porcentaje_distribucion;
                        p_porcentaje_distribucion.Direction = ParameterDirection.Input;
                        p_porcentaje_distribucion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_porcentaje_distribucion);

                        DbParameter p_base_cuota = cmdTransaccionFactory.CreateParameter();
                        p_base_cuota.ParameterName = "p_base_cuota";
                        p_base_cuota.Value = pAporte.base_cuota;
                        p_base_cuota.Direction = ParameterDirection.Input;
                        p_base_cuota.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_base_cuota);

                        DbParameter p_valor_base = cmdTransaccionFactory.CreateParameter();
                        p_valor_base.ParameterName = "p_valor_base";
                        p_valor_base.Value = pAporte.valor_base;
                        p_valor_base.Direction = ParameterDirection.Input;
                        p_valor_base.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor_base);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pAporte.estado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = pAporte.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter p_fecha_crea = cmdTransaccionFactory.CreateParameter();
                        p_fecha_crea.ParameterName = "p_fecha_crea";
                        p_fecha_crea.Value = pAporte.fecha_crea;
                        p_fecha_crea.Direction = ParameterDirection.Input;
                        p_fecha_crea.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_crea);

                        DbParameter p_cod_empresa = cmdTransaccionFactory.CreateParameter();
                        p_cod_empresa.ParameterName = "p_cod_empresa";
                        if (pAporte.cod_empresa != null)
                            p_cod_empresa.Value = pAporte.cod_empresa;
                        else
                            p_cod_empresa.Value = DBNull.Value;
                        p_cod_empresa.Direction = ParameterDirection.Input;
                        p_cod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_empresa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_APORTE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (p_numero_aporte.Value != null)
                            pAporte.numero_aporte = Convert.ToInt64(p_numero_aporte.Value);

                        DAauditoria.InsertarLog(pAporte, "APORTE", vUsuario, Accion.Crear.ToString(), TipoAuditoria.Aportes, "Creacion de cuenta de aportes para la persona con codigo " + pAporte.cod_persona + " y numero de aporte " + pAporte.numero_aporte); //REGISTRO DE AUDITORIA

                        return pAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "CrearAporte", ex);
                        return null;
                    }
                }
            }
        }


        public void ModificarNovedadCuotaAporte(Aporte vAporte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idnovedad = cmdTransaccionFactory.CreateParameter();
                        p_idnovedad.ParameterName = "p_idnovedad";
                        p_idnovedad.Value = vAporte.id_novedad_cambio;
                        p_idnovedad.Direction = ParameterDirection.Input;
                        p_idnovedad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idnovedad);

                        DbParameter p_numero_aporte = cmdTransaccionFactory.CreateParameter();
                        p_numero_aporte.ParameterName = "p_numero_aporte";
                        p_numero_aporte.Value = vAporte.numero_aporte;
                        p_numero_aporte.Direction = ParameterDirection.Input;
                        p_numero_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numero_aporte);

                        DbParameter p_fecha = cmdTransaccionFactory.CreateParameter();
                        p_fecha.ParameterName = "p_fecha_cambio_deseada";

                        if (vAporte.fecha_empieza_cambio == null || vAporte.fecha_empieza_cambio == DateTime.MinValue)
                        {
                            p_fecha.Value = DBNull.Value;
                        }
                        else
                        {
                            p_fecha.Value = vAporte.fecha_empieza_cambio;
                        }

                        p_fecha.Direction = ParameterDirection.Input;
                        p_fecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha);

                        DbParameter p_val_nuevo = cmdTransaccionFactory.CreateParameter();
                        p_val_nuevo.ParameterName = "p_val_nuevo";
                        p_val_nuevo.Value = vAporte.nuevo_valor_cuota;
                        p_val_nuevo.Direction = ParameterDirection.Input;
                        p_val_nuevo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_val_nuevo);

                        DbParameter p_val_anterior = cmdTransaccionFactory.CreateParameter();
                        p_val_anterior.ParameterName = "p_val_anterior";
                        p_val_anterior.Value = vAporte.cuota;
                        p_val_anterior.Direction = ParameterDirection.Input;
                        p_val_anterior.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_val_anterior);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = vAporte.estado_modificacion; // Aprobado - Negado (1 - 2)
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        DbParameter p_ultima_mod = cmdTransaccionFactory.CreateParameter();
                        p_ultima_mod.ParameterName = "p_ultima_mod";
                        p_ultima_mod.Value = DateTime.Today;
                        p_ultima_mod.Direction = ParameterDirection.Input;
                        p_ultima_mod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_ultima_mod);

                        DbParameter p_usuario = cmdTransaccionFactory.CreateParameter();
                        p_usuario.ParameterName = "p_usuario";
                        p_usuario.Value = pUsuario.codusuario;
                        p_usuario.Direction = ParameterDirection.Input;
                        p_usuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_usuario);

                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_NOVEDAD_CA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ModificarNovedadCuotaAporte", ex);
                    }
                }
            }
        }

        public List<Aporte> ListarTiposConNovedad(Usuario usuario)
        {
            DbDataReader resultado;
            List<Aporte> lstTipos = new List<Aporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select DISTINCT nov.COD_TIPO_PRODUCTO, tp.DESCRIPCION
                                       From v_aportes
                                       Inner Join lineaaporte On v_aportes.cod_linea_aporte = lineaaporte.cod_linea_aporte 
                                       Join novedad_cambio nov on v_aportes.numero_aporte = nov.numero_aporte
                                       Join tipoproducto tp on tp.COD_TIPO_PRODUCTO = nov.COD_TIPO_PRODUCTO
                                       Where v_aportes.estado = 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        //Agrega un elemento por default
                        Aporte defaultValue = new Aporte();
                        defaultValue.cod_tipo_producto = 0;
                        defaultValue.descripcion_tipo_prod = "Todos";
                        lstTipos.Add(defaultValue);

                        //LLena el objeto con los datos obtenidos
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["cod_tipo_producto"] != DBNull.Value) entidad.cod_tipo_producto = Convert.ToInt32(resultado["cod_tipo_producto"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion_tipo_prod = Convert.ToString(resultado["descripcion"]);
                            lstTipos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarTiposConNovedades", ex);
                        return null;
                    }
                }
            }
        }

        public List<Aporte> ListarAportesNovedadesCambio(string filtro, Usuario usuario)
        {
            DbDataReader resultado;
            List<Aporte> lstEntidad = new List<Aporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"select nov.IDNOVEDAD, nov.FECHA_NOVEDAD, nov.NUMERO_APORTE, p.IDENTIFICACION, p.NOMBREYAPELLIDO, p.COD_NOMINA, nov.COD_TIPO_PRODUCTO,
                                       CASE nov.COD_TIPO_PRODUCTO WHEN 1 THEN 'Aportes' WHEN 3 THEN 'Ahorro' WHEN 9 THEN 'Programado' ELSE ' ' end as descripcion,
                                       nov.VAL_ANTERIOR, nov.VAL_NUEVO, nov.FECHA_CAMBIO_DESEADA, nov.OBSERVACIONES,
                                       CASE nov.estado WHEN '0' THEN 'Solicitado' WHEN '1' THEN 'Aprobado' WHEN '2' THEN 'Negado' ELSE ' ' end as Estado_modificacion
                                       ,aj.snombre1||' '||aj.Sapellido1||' '||aj.Sapellido2 as NOMBREEJE
                                       from novedad_cambio nov
                                       inner join v_persona p on nov.COD_PERSONA = p.COD_PERSONA
                                       left join Asejecutivos aj on Aj.Icodigo = P.Cod_Asesor
                                       Where nov.IDNOVEDAD is not null " + filtro + " order by nov.FECHA_NOVEDAD desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["IDNOVEDAD"] != DBNull.Value) entidad.id_novedad_cambio = Convert.ToInt64(resultado["IDNOVEDAD"]);
                            if (resultado["FECHA_NOVEDAD"] != DBNull.Value) entidad.fecha_novedad_cambio = Convert.ToDateTime(resultado["FECHA_NOVEDAD"]);
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBREYAPELLIDO"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBREYAPELLIDO"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["COD_TIPO_PRODUCTO"] != DBNull.Value) entidad.cod_tipo_producto = Convert.ToInt32(resultado["COD_TIPO_PRODUCTO"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion_tipo_prod = Convert.ToString(resultado["descripcion"]);
                            if (resultado["VAL_ANTERIOR"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(resultado["VAL_ANTERIOR"]);
                            if (resultado["VAL_NUEVO"] != DBNull.Value) entidad.nuevo_valor_cuota = Convert.ToDecimal(resultado["VAL_NUEVO"]);
                            if (resultado["FECHA_CAMBIO_DESEADA"] != DBNull.Value) entidad.fecha_empieza_cambio = Convert.ToDateTime(resultado["FECHA_CAMBIO_DESEADA"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["Estado_modificacion"] != DBNull.Value) entidad.estado_modificacion = Convert.ToString(resultado["Estado_modificacion"]);
                            if (resultado["NOMBREEJE"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NOMBREEJE"]);
                            lstEntidad.Add(entidad);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarAportesNovedadesCambio", ex);
                        return null;
                    }
                }
            }
        }

        public Aporte CrearNovedadCambio(Aporte vAporte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idnovedad = cmdTransaccionFactory.CreateParameter();
                        p_idnovedad.ParameterName = "p_idnovedad";
                        p_idnovedad.Value = vAporte.id_novedad_cambio;
                        p_idnovedad.Direction = ParameterDirection.Output;
                        p_idnovedad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idnovedad);

                        DbParameter p_numero_aporte = cmdTransaccionFactory.CreateParameter();
                        p_numero_aporte.ParameterName = "p_numero_aporte";
                        p_numero_aporte.Value = vAporte.numero_aporte;
                        p_numero_aporte.Direction = ParameterDirection.Input;
                        p_numero_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numero_aporte);

                        DbParameter p_fecha = cmdTransaccionFactory.CreateParameter();
                        p_fecha.ParameterName = "p_fecha_cambio_des";
                        if (vAporte.fecha_empieza_cambio == null)
                            p_fecha.Value = DBNull.Value;
                        else
                            p_fecha.Value = vAporte.fecha_empieza_cambio;
                        p_fecha.Direction = ParameterDirection.Input;
                        p_fecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha);

                        DbParameter p_val_anterior = cmdTransaccionFactory.CreateParameter();
                        p_val_anterior.ParameterName = "p_val_anterior";
                        p_val_anterior.Value = vAporte.cuota;
                        p_val_anterior.Direction = ParameterDirection.Input;
                        p_val_anterior.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_val_anterior);

                        DbParameter p_val_nuevo = cmdTransaccionFactory.CreateParameter();
                        p_val_nuevo.ParameterName = "p_val_nuevo";
                        p_val_nuevo.Value = vAporte.nuevo_valor_cuota;
                        p_val_nuevo.Direction = ParameterDirection.Input;
                        p_val_nuevo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_val_nuevo);

                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";

                        if (!string.IsNullOrWhiteSpace(vAporte.observaciones))
                        {
                            p_observaciones.Value = vAporte.observaciones;
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
                        p_cod_persona.Value = vAporte.cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_NOVEDAD_CA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        vAporte.id_novedad_cambio = p_idnovedad.Value != DBNull.Value ? Convert.ToInt64(p_idnovedad.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);
                        return vAporte;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "CrearNovedadCambio", ex);
                        return null;
                    }
                }
            }
        }

        public string ValidarAporte(Aporte pAporte, Usuario usuario)
        {
            DbDataReader resultado;
            string respuesta = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select VALOR_CUOTA_MINIMA from LINEAAPORTE where COD_LINEA_APORTE = 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        Aporte lineaAporte = new Aporte();
                        if (resultado.Read())
                        {
                            if (resultado["VALOR_CUOTA_MINIMA"] != DBNull.Value) lineaAporte.valor_cuota_minima = Convert.ToInt64(resultado["VALOR_CUOTA_MINIMA"]);
                            else
                            {
                                respuesta = "No se encontró parametrazación de cuota mínima";
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        if (string.IsNullOrWhiteSpace(respuesta))
                        {
                            if (lineaAporte.valor_cuota_minima > pAporte.nuevo_valor_cuota)
                            {
                                respuesta = "El valor de la cuota mínima es: " + Convert.ToString(lineaAporte.valor_cuota_minima);
                            }
                            else
                            {
                                respuesta = "OK";
                            }
                        }
                        return respuesta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ValidarAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla AporteS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Aporte modificada</returns>
        public Aporte ModificarAporte(Aporte pAporte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Aporte aporteAnterior = ConsultarAporte(pAporte.numero_aporte, pUsuario);

                        DbParameter pnumero_aporte = cmdTransaccionFactory.CreateParameter();
                        pnumero_aporte.ParameterName = "p_numero_aporte";
                        pnumero_aporte.Value = pAporte.numero_aporte;
                        pnumero_aporte.Direction = ParameterDirection.Input;
                        pnumero_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_aporte);


                        DbParameter pcuota = cmdTransaccionFactory.CreateParameter();
                        pcuota.ParameterName = "p_cuota";
                        pcuota.Value = pAporte.cuota;
                        pcuota.Direction = ParameterDirection.Input;
                        pcuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuota);

                        DbParameter p_porcentaje_apo = cmdTransaccionFactory.CreateParameter();
                        p_porcentaje_apo.ParameterName = "p_porcentaje_apo";
                        if (pAporte.porcentaje_apo == null)
                            p_porcentaje_apo.Value = DBNull.Value;
                        else
                            p_porcentaje_apo.Value = pAporte.porcentaje_apo;
                        p_porcentaje_apo.Direction = ParameterDirection.Input;
                        p_porcentaje_apo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_porcentaje_apo);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        pcod_periodicidad.Value = pAporte.cod_periodicidad;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pAporte.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);


                        DbParameter pporcentaje_distribucion = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_distribucion.ParameterName = "p_porcentaje_distribucion";
                        pporcentaje_distribucion.Value = pAporte.porcentaje_distribucion;
                        pporcentaje_distribucion.Direction = ParameterDirection.Input;
                        pporcentaje_distribucion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_distribucion);

                        DbParameter pbase_cuota = cmdTransaccionFactory.CreateParameter();
                        pbase_cuota.ParameterName = "p_base_cuota";
                        pbase_cuota.Value = pAporte.base_cuota;
                        pbase_cuota.Direction = ParameterDirection.Input;
                        pbase_cuota.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pbase_cuota);

                        DbParameter pvalor_base = cmdTransaccionFactory.CreateParameter();
                        pvalor_base.ParameterName = "p_valor_base";
                        pvalor_base.Value = pAporte.valor_base;
                        pvalor_base.Direction = ParameterDirection.Input;
                        // pvalor_base.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_base);


                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = pAporte.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pfecha_crea = cmdTransaccionFactory.CreateParameter();
                        pfecha_crea.ParameterName = "p_fecha_crea";
                        pfecha_crea.Value = pAporte.fecha_crea;
                        pfecha_crea.Direction = ParameterDirection.Input;
                        pfecha_crea.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_crea);

                        DbParameter p_FECHA_APERTURA = cmdTransaccionFactory.CreateParameter();
                        p_FECHA_APERTURA.ParameterName = "p_FECHA_APERTURA";
                        p_FECHA_APERTURA.Value = pAporte.fecha_apertura;
                        p_FECHA_APERTURA.Direction = ParameterDirection.Input;
                        p_FECHA_APERTURA.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_FECHA_APERTURA);

                        DbParameter p_fecha_proximo_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_proximo_pago.ParameterName = "p_fecha_proximo_pago";
                        p_fecha_proximo_pago.Value = pAporte.fecha_proximo_pago;
                        p_fecha_proximo_pago.Direction = ParameterDirection.Input;
                        p_fecha_proximo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_proximo_pago);

                        DbParameter p_cod_empresa = cmdTransaccionFactory.CreateParameter();
                        p_cod_empresa.ParameterName = "p_cod_empresa";
                        if (pAporte.cod_empresa != null)
                            p_cod_empresa.Value = pAporte.cod_empresa;
                        else
                            p_cod_empresa.Value = DBNull.Value;
                        p_cod_empresa.Direction = ParameterDirection.Input;
                        p_cod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_empresa);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pAporte.estado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_APORTE_MODIFICAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pAporte, "APORTE", pUsuario, Accion.Modificar.ToString(), TipoAuditoria.Aportes, "Modificacion de cuenta de aportes para la persona con codigo " + pAporte.cod_persona + " y numero de aporte " + pAporte.numero_aporte, aporteAnterior); //REGISTRO DE AUDITORIA

                        return pAporte;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ModificarAporte", ex);
                        return null;
                    }
                }
            }
        }

        public bool? ValidarFechaSolicitudCambio(Aporte pAporte, Usuario usuario)
        {
            DbDataReader resultado;
            bool valido = true;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select MAX(periodo_corte) as periodo_corte from empresa_novedad where cod_empresa = " + pAporte.cod_empresa + " and estado in (1,2)";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            DateTime periodoCorte = DateTime.MinValue;
                            if (resultado["periodo_corte"] != DBNull.Value) periodoCorte = Convert.ToDateTime(resultado["periodo_corte"]);

                            if (periodoCorte >= pAporte.fecha_empieza_cambio)
                            {
                                valido = false;
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return valido;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ValidarFechaSolicitudCambio", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla AporteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de AporteS</param>
        public void EliminarAporte(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_aporte = cmdTransaccionFactory.CreateParameter();
                        pnumero_aporte.ParameterName = "p_numero_aporte";
                        pnumero_aporte.Value = pId;
                        pnumero_aporte.Direction = ParameterDirection.Input;
                        pnumero_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_aporte);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_APORTE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pId, "APORTE", vUsuario, Accion.Eliminar.ToString(), TipoAuditoria.Aportes, "Eliminacion de cuenta de aportes " + pId); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "EliminarConcepto", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla AporteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla AporteS</param>
        /// <returns>Entidad Aporte consultado</returns>
        public Aporte ConsultarAporte(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Aporte entidad = new Aporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM v_aportes WHERE NUMERO_APORTE = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOM_LINEA_APORTE"] != DBNull.Value) entidad.nom_linea_aporte = Convert.ToString(resultado["NOM_LINEA_APORTE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["TOTAL_INTERESES"] != DBNull.Value) entidad.total_intereses = Convert.ToDecimal(resultado["TOTAL_INTERESES"]);
                            if (resultado["TOTAL_RETENCION"] != DBNull.Value) entidad.total_retencion = Convert.ToDecimal(resultado["TOTAL_RETENCION"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["PORCENTAJE_DISTRIBUCION"] != DBNull.Value) entidad.porcentaje_distribucion = Convert.ToDecimal(resultado["PORCENTAJE_DISTRIBUCION"]);
                            if (resultado["BASE_CUOTA"] != DBNull.Value) entidad.base_cuota = Convert.ToInt32(resultado["BASE_CUOTA"]);
                            if (resultado["VALOR_BASE"] != DBNull.Value) entidad.valor_base = Convert.ToDecimal(resultado["VALOR_BASE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (entidad.estado == 1)
                            {
                                entidad.estado_Linea = "ACTIVO";
                            }
                            if (entidad.estado == 2)
                            {
                                entidad.estado_Linea = "INACTIVO";
                            }
                            if (entidad.estado == 3)
                            {
                                entidad.estado_Linea = "CERRADA";
                            }
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["FECHA_CREA"] != DBNull.Value) entidad.fecha_crea = Convert.ToDateTime(resultado["FECHA_CREA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (entidad.direccion == null)
                                entidad.direccion = "0";
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (entidad.telefono == null)
                                entidad.telefono = "0";
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["PAGO_INTERESES"] != DBNull.Value) entidad.pago_intereses = Convert.ToInt32(resultado["PAGO_INTERESES"]);

                            if (resultado["PORCENTAJE_SMLMV"] != DBNull.Value) entidad.porcentaje_apo = Convert.ToDecimal(resultado["PORCENTAJE_SMLMV"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ConsultarAporte", ex);
                        return null;
                    }
                }
            }
        }


        public Aporte ConsultarCuentaAporte(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Aporte entidad = new Aporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT A.*, L.PAGO_INTERESES FROM APORTE A LEFT JOIN LINEAAPORTE L ON A.COD_LINEA_APORTE = L.COD_LINEA_APORTE WHERE NUMERO_APORTE = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["TOTAL_INTERESES"] != DBNull.Value) entidad.total_intereses = Convert.ToDecimal(resultado["TOTAL_INTERESES"]);
                            if (resultado["TOTAL_RETENCION"] != DBNull.Value) entidad.total_retencion = Convert.ToDecimal(resultado["TOTAL_RETENCION"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["PORCENTAJE_DISTRIBUCION"] != DBNull.Value) entidad.porcentaje_distribucion = Convert.ToDecimal(resultado["PORCENTAJE_DISTRIBUCION"]);
                            if (resultado["BASE_CUOTA"] != DBNull.Value) entidad.base_cuota = Convert.ToInt32(resultado["BASE_CUOTA"]);
                            if (resultado["VALOR_BASE"] != DBNull.Value) entidad.valor_base = Convert.ToDecimal(resultado["VALOR_BASE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (entidad.estado == 1)
                            {
                                entidad.estado_Linea = "ACTIVO";
                            }
                            if (entidad.estado == 2)
                            {
                                entidad.estado_Linea = "INACTIVO";
                            }
                            if (entidad.estado == 3)
                            {
                                entidad.estado_Linea = "CERRADA";
                            }
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["FECHA_CREA"] != DBNull.Value) entidad.fecha_crea = Convert.ToDateTime(resultado["FECHA_CREA"]);
                            if (entidad.direccion == null)
                                entidad.direccion = "0";
                            if (entidad.telefono == null)
                                entidad.telefono = "0";
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["PAGO_INTERESES"] != DBNull.Value) entidad.pago_intereses = Convert.ToInt32(resultado["PAGO_INTERESES"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ConsultarCuentaAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla AporteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla AporteS</param>
        /// <returns>Entidad Aporte consultado</returns>
        public Aporte ConsultarCruceCuentas(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Aporte entidad = new Aporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @" select a.identificacion, a.primer_nombre || ' ' || a.segundo_nombre|| ' ' ||a.primer_apellido || ' ' || a.segundo_apellido as nombre,
                                        b.idretiro,b.fecha_retiro,b.observaciones,b.saldo,c.descripcion,b.cod_usuario as estado,    c.descripcion as motivo_retiro,b.acta,
                                        b.cod_motivo,a.tipo_identificacion 
                                        from persona_retiro b 
                                        inner join persona  a on a.cod_persona=b.cod_persona 
                                        inner join motivo_retiro c on c.cod_motivo= b.cod_motivo where a.identificacion= " + "'" + pId.ToString() + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["nombre"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["nombre"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["idretiro"] != DBNull.Value) entidad.idretiro = Convert.ToInt64(resultado["idretiro"]);
                            if (resultado["fecha_retiro"] != DBNull.Value) entidad.fecha_retiro = Convert.ToDateTime(resultado["fecha_retiro"]);
                            if (resultado["observaciones"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["observaciones"]);
                            if (resultado["saldo"] != DBNull.Value) entidad.Saldos = Convert.ToDecimal(resultado["saldo"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estadocruce = Convert.ToString(resultado["estado"]);
                            if (resultado["motivo_retiro"] != DBNull.Value) entidad.motivoretiro = Convert.ToString(resultado["motivo_retiro"]);
                            if (resultado["acta"] != DBNull.Value) entidad.acta = Convert.ToString(resultado["acta"]);
                            if (resultado["cod_motivo"] != DBNull.Value) entidad.cod_motivo = Convert.ToInt64(resultado["cod_motivo"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.tipo_iden = Convert.ToInt64(resultado["tipo_identificacion"]);

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
                        BOExcepcion.Throw("AporteData", "ConsultarCruceCuentas", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla AporteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla AporteS</param>
        /// <returns>Entidad Aporte consultado</returns>
        public Aporte ConsultarClienteAporte(String pId, Int32 cod_Linea, Usuario vUsuario)
        {
            DbDataReader resultado;
            Aporte entidad = new Aporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM v_aportes WHERE IDENTIFICACION = " + "'" + pId.ToString() + "'";
                        if (cod_Linea != 0)
                            sql += " AND Cod_Linea_Aporte = " + cod_Linea;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOM_LINEA_APORTE"] != DBNull.Value) entidad.nom_linea_aporte = Convert.ToString(resultado["NOM_LINEA_APORTE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["TOTAL_INTERESES"] != DBNull.Value) entidad.total_intereses = Convert.ToDecimal(resultado["TOTAL_INTERESES"]);
                            if (resultado["TOTAL_RETENCION"] != DBNull.Value) entidad.total_retencion = Convert.ToDecimal(resultado["TOTAL_RETENCION"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["PORCENTAJE_DISTRIBUCION"] != DBNull.Value) entidad.porcentaje_distribucion = Convert.ToDecimal(resultado["PORCENTAJE_DISTRIBUCION"]);
                            if (resultado["BASE_CUOTA"] != DBNull.Value) entidad.base_cuota = Convert.ToInt32(resultado["BASE_CUOTA"]);
                            if (resultado["VALOR_BASE"] != DBNull.Value) entidad.valor_base = Convert.ToDecimal(resultado["VALOR_BASE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (entidad.estado == 1)
                            {
                                entidad.estado_Linea = "ACTIVO";
                            }
                            if (entidad.estado == 2)
                            {
                                entidad.estado_Linea = "INACTIVO";
                            }
                            if (entidad.estado == 3)
                            {
                                entidad.estado_Linea = "CERRADA";
                            }
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["FECHA_CREA"] != DBNull.Value) entidad.fecha_crea = Convert.ToDateTime(resultado["FECHA_CREA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["PAGO_INTERESES"] != DBNull.Value) entidad.pago_intereses = Convert.ToInt32(resultado["PAGO_INTERESES"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ConsultarAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla AporteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla AporteS</param>
        /// <returns>Entidad Aporte consultado</returns>
        public Aporte ConsultarGrupoAporte(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Aporte entidad = new Aporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT IDGRUPO FROM grupo_lineaaporte WHERE cod_linea_aporte = " + pId.ToString(); ;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDGRUPO"] != DBNull.Value) entidad.grupo = Convert.ToInt64(resultado["IDGRUPO"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ConsultarAporte", ex);
                        return null;
                    }
                }
            }
        }

        public List<Aporte> ConsultarCuentasPorGrupoAporte(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Aporte> lstAporte = new List<Aporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT g.COD_LINEA_APORTE, g.porcentaje_dist, g.PRINCIPAL, l.Nombre FROM grupo_lineaaporte g JOIN lineaaporte l on l.cod_linea_aporte = g.cod_linea_aporte WHERE IDGRUPO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Aporte aporte = new Aporte();

                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) aporte.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["porcentaje_dist"] != DBNull.Value) aporte.porcentaje = Convert.ToInt64(resultado["porcentaje_dist"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) aporte.principal = Convert.ToInt64(resultado["PRINCIPAL"]);
                            if (resultado["Nombre"] != DBNull.Value) aporte.nom_linea_aporte = Convert.ToString(resultado["Nombre"]);

                            lstAporte.Add(aporte);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ConsultarCuentasPorGrupoAporte", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla LineaAporte dados unos filtros
        /// </summary>
        /// <param name="pAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarLineaAporte(Aporte pAporte, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM LineaAporte " + " ORDER BY COD_LINEA_APORTE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);//+ "-" + Convert.ToString(resultado["NOMBRE"]));
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_linea_aporte = Convert.ToString(resultado["COD_LINEA_APORTE"] + "-" + Convert.ToString(resultado["NOMBRE"]));
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarLineaAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla LineaAporte dados unos filtros
        /// </summary>
        /// <param name="pAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarTipoIdentificacion(Aporte pAporte, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select CODTIPOIDENTIFICACION as ListaId, descripcion as ListaDescripcion from TIPOIDENTIFICACION";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["ListaId"] != DBNull.Value) entidad.ListaId = Convert.ToInt64(resultado["ListaId"]);
                            if (resultado["ListaDescripcion"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["ListaDescripcion"]);
                            lstAporte.Add(entidad); ;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarTipoIdentificacion", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla LineaAporte dados unos filtros
        /// </summary>
        /// <param name="pAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarTipoRetiro(Aporte pAporte, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select cod_motivo as ListaId, descripcion as ListaDescripcion from MOTIVO_RETIRO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["ListaId"] != DBNull.Value) entidad.ListaId = Convert.ToInt64(resultado["ListaId"]);
                            if (resultado["ListaDescripcion"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["ListaDescripcion"]);
                            lstAporte.Add(entidad); ;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarTipoIdentificacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<Aporte> ListarCuentasPersona(Int64 pCod_Persona, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        //string sql = @"SELECT A.COD_PERSONA, A.NUMERO_APORTE FROM APORTE A WHERE A.COD_PERSONA = " + pCod_Persona;

                        //string sql = @"SELECT A.COD_PERSONA, A.NUMERO_APORTE FROM APORTE A WHERE A.COD_PERSONA = "+pCod_Persona;

                        string sql = @"SELECT A.COD_PERSONA, A.NUMERO_APORTE, L.NOMBRE FROM APORTE A INNER JOIN LINEAAPORTE L ON A.COD_LINEA_APORTE = L.COD_LINEA_APORTE WHERE A.COD_PERSONA = " + pCod_Persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_linea_aporte = Convert.ToString(resultado["NOMBRE"]);

                            lstAporte.Add(entidad); ;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarCuentasPersona", ex);
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
        public List<MovimientoAporte> ListarMovimiento(Int64 pNumeroAporte, DateTime? pfechaInicial, DateTime? pfechaFinal, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoAporte> lstMovimientosProducto = new List<MovimientoAporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_NUMERO_APORTE = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_APORTE.ParameterName = "P_NUMERO_APORTE";
                        P_NUMERO_APORTE.Direction = ParameterDirection.Input;
                        P_NUMERO_APORTE.Value = pNumeroAporte;
                        P_NUMERO_APORTE.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_APORTE);

                        DbParameter P_FECHA_INICIAL = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_INICIAL.ParameterName = "P_FECHA_INICIAL";
                        P_FECHA_INICIAL.Direction = ParameterDirection.Input;
                        if (pfechaInicial == null)
                            P_FECHA_INICIAL.Value = DBNull.Value;
                        else
                            P_FECHA_INICIAL.Value = pfechaInicial;
                        P_FECHA_INICIAL.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_INICIAL);

                        DbParameter P_FECHA_FINAL = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_FINAL.ParameterName = "P_FECHA_FINAL";
                        P_FECHA_FINAL.Direction = ParameterDirection.Input;
                        if (pfechaFinal == null)
                            P_FECHA_FINAL.Value = DBNull.Value;
                        else
                            P_FECHA_FINAL.Value = pfechaFinal;
                        P_FECHA_FINAL.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_FINAL);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_MOVIVIMIENTO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select ac.*, tc.descripcion As nom_tipo_comp From temp_movimientos ac Left Join tipo_comp tc On ac.tipo_comp = tc.tipo_comp Where ac.numero_radicacion = " + pNumeroAporte.ToString() + " Order by ac.fecha_pago, ac.fecha_cuota, ac.cod_ope";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoAporte entidad = new MovimientoAporte();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["numero_radicacion"].ToString());
                            if (resultado["num_cuota"] != DBNull.Value) entidad.NumCuota = Convert.ToInt64(resultado["num_cuota"].ToString());
                            if (resultado["fecha_cuota"] != DBNull.Value) entidad.FechaCuota = Convert.ToDateTime(resultado["fecha_cuota"].ToString());
                            if (resultado["fecha_pago"] != DBNull.Value) entidad.FechaPago = Convert.ToDateTime(resultado["fecha_pago"].ToString());
                            if (resultado["dias_mora"] != DBNull.Value) entidad.DiasMora = Convert.ToInt64(resultado["dias_mora"].ToString());
                            if (resultado["cod_ope"] != DBNull.Value) entidad.CodOperacion = Convert.ToInt64(resultado["cod_ope"].ToString());
                            if (resultado["tipo_ope"] != DBNull.Value) entidad.TipoOperacion = resultado["tipo_ope"].ToString();
                            if (resultado["tipo_mov"] != DBNull.Value) entidad.TipoMovimiento = resultado["tipo_mov"].ToString();
                            if (resultado["Saldo"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["Saldo"].ToString());
                            if (resultado["num_comp"] != DBNull.Value) entidad.num_comp = resultado["num_comp"].ToString();
                            if (resultado["tipo_comp"] != DBNull.Value) entidad.tipo_comp = resultado["tipo_comp"].ToString();
                            if (resultado["nom_tipo_comp"] != DBNull.Value) entidad.nom_tipo_comp = resultado["nom_tipo_comp"].ToString();
                            if (resultado["Capital"] != DBNull.Value) entidad.Capital = Convert.ToDouble(resultado["Capital"].ToString());

                            lstMovimientosProducto.Add(entidad);
                        }
                        return lstMovimientosProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "Listar", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla AporteS dados unos filtros
        /// </summary>
        /// <param name="pAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarAporte(Aporte pAporte, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM V_APORTES " + ObtenerFiltro(pAporte) + " ORDER BY NUMERO_APORTE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOM_LINEA_APORTE"] != DBNull.Value) entidad.nom_linea_aporte = Convert.ToString(resultado["NOM_LINEA_APORTE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["TOTAL_INTERESES"] != DBNull.Value) entidad.total_intereses = Convert.ToDecimal(resultado["TOTAL_INTERESES"]);
                            if (resultado["TOTAL_RETENCION"] != DBNull.Value) entidad.total_retencion = Convert.ToDecimal(resultado["TOTAL_RETENCION"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["PORCENTAJE_DISTRIBUCION"] != DBNull.Value) entidad.porcentaje_distribucion = Convert.ToDecimal(resultado["PORCENTAJE_DISTRIBUCION"]);
                            if (resultado["BASE_CUOTA"] != DBNull.Value) entidad.base_cuota = Convert.ToInt32(resultado["BASE_CUOTA"]);
                            if (resultado["VALOR_BASE"] != DBNull.Value) entidad.valor_base = Convert.ToDecimal(resultado["VALOR_BASE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["FECHA_CREA"] != DBNull.Value) entidad.fecha_crea = Convert.ToDateTime(resultado["FECHA_CREA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarAporte", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla AporteS dados unos filtros
        /// </summary>
        /// <param name="pAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<MovimientoAporte> ListarMovAporte(MovimientoAporte pAporte, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<MovimientoAporte> lstAporte = new List<MovimientoAporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM tran_aporte " + ObtenerFiltro(pAporte) + " ORDER BY NUMERO_APORTE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            MovimientoAporte entidad = new MovimientoAporte();
                            if (resultado["NUMERO_TRANSACCION"] != DBNull.Value) entidad.num_transaccion = Convert.ToInt64(resultado["NUMERO_TRANSACCION"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atributo = Convert.ToInt64(resultado["COD_ATR"]);
                            if (resultado["COD_DET_LIST"] != DBNull.Value) entidad.cod_lista = Convert.ToInt64(resultado["COD_DET_LIST"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["TIPO_TRAN"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR_BASE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);

                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarMovAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla AporteS dados unos filtros
        /// </summary>
        /// <param name="pAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarAperturaAporte(Aporte pAporte, Usuario vUsuario, String Orden)
        {
            DbDataReader resultado;
            String ordenado = Orden;
            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql1 = @"SELECT V_APORTES.*,lineaaporte.cod_clasifica FROM V_APORTES join lineaaporte on lineaaporte.cod_linea_aporte=v_aportes.cod_linea_aporte " + ObtenerFiltro(pAporte, "V_APORTES.");
                        String sql2 = @" ORDER BY ";
                        string sql3 = ordenado;
                        string sql = sql1 + sql2 + sql3;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOM_LINEA_APORTE"] != DBNull.Value) entidad.nom_linea_aporte = Convert.ToString(resultado["NOM_LINEA_APORTE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["TOTAL_INTERESES"] != DBNull.Value) entidad.total_intereses = Convert.ToDecimal(resultado["TOTAL_INTERESES"]);
                            if (resultado["TOTAL_RETENCION"] != DBNull.Value) entidad.total_retencion = Convert.ToDecimal(resultado["TOTAL_RETENCION"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["PORCENTAJE_DISTRIBUCION"] != DBNull.Value) entidad.porcentaje_distribucion = Convert.ToDecimal(resultado["PORCENTAJE_DISTRIBUCION"]);
                            if (resultado["BASE_CUOTA"] != DBNull.Value) entidad.base_cuota = Convert.ToInt32(resultado["BASE_CUOTA"]);
                            if (resultado["VALOR_BASE"] != DBNull.Value) entidad.valor_base = Convert.ToDecimal(resultado["VALOR_BASE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt64(resultado["PRINCIPAL"]);
                            if (entidad.principal == 1)
                            {
                                if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            }
                            if (entidad.principal == null)
                                if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            if (entidad.estado == 1)
                            {
                                entidad.estado_Linea = "ACTIVO";
                            }
                            if (entidad.estado == 2)
                            {
                                entidad.estado_Linea = "INACTIVO";
                            }
                            if (entidad.estado == 3)
                            {
                                entidad.estado_Linea = "CERRADA";
                            }
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["FECHA_CREA"] != DBNull.Value) entidad.fecha_crea = Convert.ToDateTime(resultado["FECHA_CREA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["cod_clasifica"] != DBNull.Value) entidad.Cod_Clasificacion = Convert.ToInt32(resultado["cod_clasifica"]);
                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarAporte", ex);
                        return null;
                    }
                }
            }
        }
        public List<Aporte> ListarDiasCategoria(int cod_clasifica, Usuario vUsuario)
        {
            DbDataReader resultado;

            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from dias_categorias where COD_CLASIFICA=" + cod_clasifica;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["DIAS_MINIMO"] != DBNull.Value) entidad.Dias_Minimo = Convert.ToInt32(resultado["DIAS_MINIMO"]);
                            if (resultado["DIAS_MAXIMO"] != DBNull.Value) entidad.Dias_Maximo = Convert.ToInt32(resultado["DIAS_MAXIMO"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.Cod_Categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarDiasCategoria", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla AporteS dados unos filtros
        /// </summary>
        /// <param name="pAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarRetiros(string pFiltro, DateTime pFechaReti, Usuario vUsuario)
        {
            DbDataReader resultado;
            //String ordenado = Orden;
            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT B.IDRETIRO,A.IDENTIFICACION, A.COD_NOMINA , A.NOMBRES,A.APELLIDOS,B.FECHA_RETIRO,C.DESCRIPCION AS MOTRETIRO,B.ACTA, "
                                        + "B.SALDO,B.OBSERVACIONES, "
                                        + "CASE P.ESTADO WHEN 'A' THEN 'Activo' WHEN 'R' THEN 'Retirado' WHEN 'I' THEN 'Inactivo' END AS NOMESTADO "
                                        + "FROM PERSONA_RETIRO B INNER JOIN V_PERSONA A ON A.COD_PERSONA = B.COD_PERSONA "
                                        + "LEFT JOIN MOTIVO_RETIRO C ON C.COD_MOTIVO= B.COD_MOTIVO "
                                        + "LEFT JOIN PERSONA_AFILIACION P ON P.COD_PERSONA = B.COD_PERSONA" + pFiltro;
                        if (pFechaReti != null && pFechaReti != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " B.FECHA_RETIRO = To_Date('" + Convert.ToDateTime(pFechaReti).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " B.FECHA_RETIRO = '" + Convert.ToDateTime(pFechaReti).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql += " ORDER BY 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["IDRETIRO"] != DBNull.Value) entidad.idretiro = Convert.ToInt64(resultado["IDRETIRO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.Apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["FECHA_RETIRO"] != DBNull.Value) entidad.fecha_retiro = Convert.ToDateTime(resultado["FECHA_RETIRO"]);
                            if (resultado["MOTRETIRO"] != DBNull.Value) entidad.motivoretiro = Convert.ToString(resultado["MOTRETIRO"]);
                            if (resultado["ACTA"] != DBNull.Value) entidad.acta = Convert.ToString(resultado["ACTA"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldos = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.estadocruce = Convert.ToString(resultado["NOMESTADO"]);
                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarAporte", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene una lista de Solicitudes de retiro de asociado dados unos filtros
        /// </summary>
        /// <param name="pAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Solicitudes obtenidos</returns>
        public List<Aporte> ListarSolicitudRetiro(string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado;
            //String ordenado = Orden;
            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"select s.ID_SOL_RETIRO, p.IDENTIFICACION, p.PRIMER_NOMBRE||' '||p.SEGUNDO_NOMBRE NOMBRES, p.PRIMER_APELLIDO||' '||p.SEGUNDO_APELLIDO APELLIDOS,
                                       s.FECHA_SOLICITUD, s.COD_MOTIVO, m.DESCRIPCION, s.OBSERVACIONES,
                                       case s.ESTADO WHEN '0' THEN 'Solicitado' WHEN '1' THEN 'Autorizado' WHEN '2' THEN 'Rechazado' end ESTADO_SOLICITUD,
                                       p.COD_PERSONA, case p.TIPO_PERSONA WHEN 'N' THEN 'Natural' WHEN 'J' THEN 'Juridica' end TIPO_PERSONA, p.TIPO_IDENTIFICACION
                                       ,aj.snombre1||' '||aj.Sapellido1||' '||aj.Sapellido2 as NOMBREEJE
                                       from SOLICITUD_RETIRO s
                                       inner join PERSONA p on p.COD_PERSONA = s.COD_PERSONA
                                       inner join MOTIVO_RETIRO m on m.COD_MOTIVO = s.COD_MOTIVO
                                       left join Asejecutivos aj on Aj.Icodigo = p.Cod_Asesor
                                       where s.ID_SOL_RETIRO > 0 " + pFiltro + " order by 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["ID_SOL_RETIRO"] != DBNull.Value) entidad.idretiro = Convert.ToInt64(resultado["ID_SOL_RETIRO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.Apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_retiro = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["COD_MOTIVO"] != DBNull.Value) entidad.cod_motivo = Convert.ToInt64(resultado["COD_MOTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.motivoretiro = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["ESTADO_SOLICITUD"] != DBNull.Value) entidad.estado_modificacion = Convert.ToString(resultado["ESTADO_SOLICITUD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBREEJE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBREEJE"]);

                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Aportes dados unos filtros
        /// </summary>
        /// <param name="pAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarEstadoCuentaAporte(Int64 pcliente, int? pEstadoAporte, DateTime pFecha, Usuario vUsuario)
        {
            DbDataReader resultado;

            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select DISTINCT v_aportes.*, lineaaporte.cod_linea_aporte, lineaaporte.permite_pagoprod, (Select e.nom_empresa From empresa_recaudo e Where e.cod_empresa = v_aportes.cod_empresa) As nom_empresa, Calcular_VrAPagarAporte(v_aportes.numero_aporte, To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"')) As ValorAPagar,
                                        CASE nov.estado WHEN '0' THEN 'Solicitado' WHEN '1' THEN 'Aprobado' WHEN '2' THEN 'Negado' ELSE ' ' end as Estado_modificacion ,Dias_Mora
                                        From v_aportes 
                                        Inner Join lineaaporte On v_aportes.cod_linea_aporte = lineaaporte.cod_linea_aporte 
                                        Left Join novedad_cambio nov on v_aportes.numero_aporte = nov.numero_aporte
                                        Where v_aportes.cod_persona = " + pcliente;
                        if (pEstadoAporte != null)
                            sql += " And v_aportes.estado = " + pEstadoAporte.ToString() + " order by lineaaporte.cod_linea_aporte";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOM_LINEA_APORTE"] != DBNull.Value) entidad.nom_linea_aporte = Convert.ToString(resultado["NOM_LINEA_APORTE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (entidad.forma_pago == 1)
                                entidad.nom_forma_pago = "Caja";
                            if (entidad.forma_pago == 2)
                                entidad.nom_forma_pago = "Nomina";
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMA_MOD"] != DBNull.Value) entidad.fecha_ultima_mod = Convert.ToDateTime(resultado["FECHA_ULTIMA_MOD"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["TOTAL_INTERESES"] != DBNull.Value) entidad.total_intereses = Convert.ToDecimal(resultado["TOTAL_INTERESES"]);
                            if (resultado["TOTAL_RETENCION"] != DBNull.Value) entidad.total_retencion = Convert.ToDecimal(resultado["TOTAL_RETENCION"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["PORCENTAJE_DISTRIBUCION"] != DBNull.Value) entidad.porcentaje_distribucion = Convert.ToDecimal(resultado["PORCENTAJE_DISTRIBUCION"]);
                            if (resultado["BASE_CUOTA"] != DBNull.Value) entidad.base_cuota = Convert.ToInt32(resultado["BASE_CUOTA"]);
                            if (resultado["VALOR_BASE"] != DBNull.Value) entidad.valor_base = Convert.ToDecimal(resultado["VALOR_BASE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["Estado_Modificacion"] != DBNull.Value) entidad.estado_modificacion = Convert.ToString(resultado["Estado_Modificacion"]);
                            if (resultado["Dias_Mora"] != DBNull.Value) entidad.DiasMora = Convert.ToInt32(resultado["Dias_Mora"]);

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
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["FECHA_CREA"] != DBNull.Value) entidad.fecha_crea = Convert.ToDateTime(resultado["FECHA_CREA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["VALORAPAGAR"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["VALORAPAGAR"]);
                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarEstadoCuentaAporte", ex);
                        return null;
                    }
                }
            }
        }
        public string ClasificacionAporte(Int64 idpersona, Usuario vUsuario)
        {
            DbDataReader resultado;
            string entidad = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT categoria FROM pendiente_aporte pa JOIN v_aportes A ON pa.numero_aporte=A.numero_aporte WHERE pa.tipo=1 AND A.cod_linea_aporte=1
                                 and a.cod_persona = " + idpersona;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["categoria"] != DBNull.Value) entidad = Convert.ToString(resultado["categoria"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ClasificacionAporte", ex);
                        return null;
                    }
                }
            }
        }


        public List<Aporte> ListarEstadoCuentaAporteTodos(Int64 pcliente, string pEstadoAporte, DateTime pFecha, Usuario vUsuario, int EstadoCuenta = 0)
        {
            DbDataReader resultado;
            string sql = "";

            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();


                        if (EstadoCuenta != 0)
                        {
                            //Consulta para El historico de Estados de cuenta. 
                            sql = @"Select h.Numero_aporte,h.cod_linea_aporte, v.nom_linea_aporte, h.cod_oficina , h.cod_persona, h.fecha_apertura
                            ,h.cuota, h.cod_periodicidad, h.forma_pago, h.fecha_proximo_pago, h.fecha_ultimo_pago,v.fecha_ultima_mod,v.fecha_cierre, h.saldo, 
                            v.fecha_interes, v.total_intereses, v.total_retencion, v.saldo_intereses, v.porcentaje_distribucion, v.base_cuota, v.valor_base, 
                            h.estado, v.cod_persona,v.fecha_crea, v.identificacion, v.nombre, v.cod_empresa, v.nom_oficina, Calcular_VrAPagarAporte(v.numero_aporte, To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"')) ValorAPagar,
                            l.cruzar, SALDO_ACUMULADO(1,v.numero_aporte) AS VALOR_ACUMULADO
                            from historico_aporte h 
                            inner join v_aportes v on v.numero_aporte = h.numero_aporte
                            inner join lineaAporte l on l.cod_linea_aporte = h.cod_linea_aporte 
                            Where h.Fecha_historico  = To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') And h.Cod_persona= " + pcliente + @"and l.tipo_aporte NOT IN (4)";
                            if (pEstadoAporte != null)
                                sql += "   And v.estado in(" + pEstadoAporte.ToString() + ")" + " order by l.cod_linea_aporte";
                        }
                        else
                        {
                            sql = @"Select DISTINCT v_aportes.*, lineaaporte.cod_linea_aporte, lineaaporte.permite_pagoprod, (Select e.nom_empresa From empresa_recaudo e Where e.cod_empresa = v_aportes.cod_empresa) As nom_empresa, Calcular_VrAPagarAporte(v_aportes.numero_aporte, To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"')) As ValorAPagar,
                                        CASE nov.estado WHEN '0' THEN 'Solicitado' WHEN '1' THEN 'Aprobado' WHEN '2' THEN 'Negado' ELSE ' ' end as Estado_modificacion, lineaaporte.cruzar,SALDO_ACUMULADO(1,v_aportes.numero_aporte) AS VALOR_ACUMULADO,Dias_Mora
                                        From v_aportes 
                                        Inner Join lineaaporte On v_aportes.cod_linea_aporte = lineaaporte.cod_linea_aporte 
                                        Left Join novedad_cambio nov on v_aportes.numero_aporte = nov.numero_aporte
                                        Where v_aportes.cod_persona = " + pcliente + "and lineaaporte.tipo_aporte NOT IN (4)";
                            if (pEstadoAporte != null)
                                sql += " And v_aportes.estado in(" + pEstadoAporte.ToString() + ")" + " order by lineaaporte.cod_linea_aporte";
                        }


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOM_LINEA_APORTE"] != DBNull.Value) entidad.nom_linea_aporte = Convert.ToString(resultado["NOM_LINEA_APORTE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (entidad.forma_pago == 1)
                                entidad.nom_forma_pago = "Caja";
                            if (entidad.forma_pago == 2)
                                entidad.nom_forma_pago = "Nomina";
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMA_MOD"] != DBNull.Value) entidad.fecha_ultima_mod = Convert.ToDateTime(resultado["FECHA_ULTIMA_MOD"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["TOTAL_INTERESES"] != DBNull.Value) entidad.total_intereses = Convert.ToDecimal(resultado["TOTAL_INTERESES"]);
                            if (resultado["TOTAL_RETENCION"] != DBNull.Value) entidad.total_retencion = Convert.ToDecimal(resultado["TOTAL_RETENCION"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["PORCENTAJE_DISTRIBUCION"] != DBNull.Value) entidad.porcentaje_distribucion = Convert.ToDecimal(resultado["PORCENTAJE_DISTRIBUCION"]);
                            if (resultado["BASE_CUOTA"] != DBNull.Value) entidad.base_cuota = Convert.ToInt32(resultado["BASE_CUOTA"]);
                            if (resultado["VALOR_BASE"] != DBNull.Value) entidad.valor_base = Convert.ToDecimal(resultado["VALOR_BASE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
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
                            if (resultado["FECHA_CREA"] != DBNull.Value) entidad.fecha_crea = Convert.ToDateTime(resultado["FECHA_CREA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["VALORAPAGAR"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["VALORAPAGAR"]);
                            // Si el aporte no se cruza no mostrar el saldo
                            string cruzar = "";
                            if (resultado["CRUZAR"] != DBNull.Value) cruzar = Convert.ToString(resultado["CRUZAR"]);
                            if (cruzar != "1")
                                entidad.Saldo = 0;

                            if (resultado["VALOR_ACUMULADO"] != DBNull.Value) entidad.valor_acumulado = Convert.ToDecimal(resultado["VALOR_ACUMULADO"]);
                            if (entidad.valor_acumulado > 0)
                            {
                                entidad.valor_total_acumu = entidad.Saldo + entidad.valor_acumulado;
                            }

                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarEstadoCuentaAporteTodos", ex);
                        return null;
                    }
                }
            }
        }

        public List<Aporte> ListarEstadoCuentaAportePermitePago(Int64 pcliente, DateTime pFecha, Usuario vUsuario)
        {
            DbDataReader resultado;

            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select V_APORTES.*, lineaaporte.PERMITE_PAGOPROD,(Select e.nom_empresa From empresa_recaudo e Where e.cod_empresa = V_APORTES.cod_empresa) As nom_empresa, Calcular_VrAPagarAporte(V_APORTES.numero_aporte, To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"'))As ValorAPagar 
                        From V_APORTES inner join lineaaporte on v_aportes.cod_linea_aporte=lineaaporte.cod_linea_aporte Where cod_persona= " + pcliente + " And PERMITE_PAGOPROD = 1 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOM_LINEA_APORTE"] != DBNull.Value) entidad.nom_linea_aporte = Convert.ToString(resultado["NOM_LINEA_APORTE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (entidad.forma_pago == 1)
                                entidad.nom_forma_pago = "Caja";
                            if (entidad.forma_pago == 2)
                                entidad.nom_forma_pago = "Nomina";
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["TOTAL_INTERESES"] != DBNull.Value) entidad.total_intereses = Convert.ToDecimal(resultado["TOTAL_INTERESES"]);
                            if (resultado["TOTAL_RETENCION"] != DBNull.Value) entidad.total_retencion = Convert.ToDecimal(resultado["TOTAL_RETENCION"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["PORCENTAJE_DISTRIBUCION"] != DBNull.Value) entidad.porcentaje_distribucion = Convert.ToDecimal(resultado["PORCENTAJE_DISTRIBUCION"]);
                            if (resultado["BASE_CUOTA"] != DBNull.Value) entidad.base_cuota = Convert.ToInt32(resultado["BASE_CUOTA"]);
                            if (resultado["VALOR_BASE"] != DBNull.Value) entidad.valor_base = Convert.ToDecimal(resultado["VALOR_BASE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
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
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["FECHA_CREA"] != DBNull.Value) entidad.fecha_crea = Convert.ToDateTime(resultado["FECHA_CREA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["VALORAPAGAR"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["VALORAPAGAR"]);
                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarEstadoCuentaAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla AporteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla AporteS</param>
        /// <returns>Entidad Aporte consultado</returns>
        public Aporte ConsultarCliente(String pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Aporte entidad = new Aporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT cod_persona, identificacion, tipo_identificacion, primer_nombre  || ' ' || segundo_nombre  || ' ' || primer_apellido  || ' ' || segundo_apellido as nombre 
                                        FROM PERSONA WHERE identificacion = " + "'" + pId.ToString() + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                        }
                        //  else
                        //   {
                        //  throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        //  }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ConsultarAporte", ex);
                        return null;
                    }
                }
            }
        }

        public decimal ConsultarClienteSalario(Int64 pId, Usuario vUsuario)
        {
            decimal salario = 0;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT sueldo_persona
                                        FROM INFORMACION_INGRE_EGRE WHERE cod_persona = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["sueldo_persona"] != DBNull.Value) salario = Convert.ToDecimal(resultado["sueldo_persona"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return salario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ConsultarClienteSalario", ex);
                        return salario;
                    }
                }
            }
        }

        public String[] getRegistro(Usuario pUsuario, String pIdentificacion)
        {
            DbDataReader resultado;
            String[] arreglo = new String[5];
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select f.fecha_afiliacion, estado_persona.estado as estado, f.fecha_retiro, f.idafiliacion ,p.cod_persona "
                                     + " From Persona p left Join Persona_Afiliacion f On p.Cod_Persona = f.Cod_Persona left join estado_persona on estado_persona.estado = p.estado"
                                     + " where p.identificacion = '" + pIdentificacion + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["fecha_afiliacion"] != DBNull.Value) arreglo[0] = Convert.ToString(resultado["fecha_afiliacion"]);
                            else arreglo[0] = "No se encontraron Registros";

                            if (resultado["estado"] != DBNull.Value) arreglo[1] = Convert.ToString(resultado["estado"]);
                            else arreglo[1] = "No se encontraron Registros";

                            if (resultado["fecha_retiro"] != DBNull.Value) arreglo[2] = Convert.ToString(resultado["fecha_retiro"]);
                            else arreglo[2] = "No se encontraron Registros";

                            if (resultado["idafiliacion"] != DBNull.Value) arreglo[3] = Convert.ToString(resultado["idafiliacion"]);
                            else arreglo[3] = "No se encontraron Registros";

                            if (resultado["cod_persona"] != DBNull.Value) arreglo[4] = Convert.ToString(resultado["cod_persona"]);
                            else arreglo[4] = "No se encontraron Registros";
                        }
                        else
                        {
                            arreglo[0] = "No se encontraron Registros";
                            arreglo[1] = "No se encontraron Registros";
                            arreglo[2] = "No se encontraron Registros";
                            arreglo[3] = "No se encontraron Registros";
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return arreglo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ConsultarClienteSalario", ex);
                        return null;
                    }
                }
            }
        }


        public decimal Calcular_Cuota(decimal Salario, decimal Porcentaje, decimal Periodicidad, Usuario vUsuario)
        {
            decimal cuota = 0;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select ROUND(((" + Porcentaje.ToString().Replace(",", ".") + "  / 100 * (" + Salario.ToString().Replace(",", ".") + ")) / 30) * NUMERO_DIAS) cuota  from PERIODICIDAD WHERE COD_PERIODICIDAD = " + Periodicidad;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["cuota"] != DBNull.Value) cuota = Convert.ToDecimal(resultado["cuota"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return cuota;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ConsultarClienteSalario", ex);
                        return cuota;
                    }
                }
            }
        }




        public void UpdateEstadoPersonaF(ref Int64 codMoAnterior, Int64 idAfilacion, Int64 CodPerson, String estado, Int64 codmotivo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_codpersona = cmdTransaccionFactory.CreateParameter();
                        p_codpersona.ParameterName = "p_codpersona";
                        p_codpersona.Value = CodPerson;
                        p_codpersona.Direction = ParameterDirection.Input;
                        p_codpersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codpersona);

                        DbParameter p_idafiliacion = cmdTransaccionFactory.CreateParameter();
                        p_idafiliacion.ParameterName = "p_idafiliacion";
                        p_idafiliacion.Value = idAfilacion;
                        p_idafiliacion.Direction = ParameterDirection.Input;
                        p_idafiliacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idafiliacion);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = estado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        DbParameter p_cod_motivo = cmdTransaccionFactory.CreateParameter();
                        p_cod_motivo.ParameterName = "p_cod_motivo";
                        p_cod_motivo.Value = codmotivo;
                        p_cod_motivo.Direction = ParameterDirection.Input;
                        p_cod_motivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_motivo);

                        DbParameter p_codMotivoAnterior = cmdTransaccionFactory.CreateParameter();
                        p_codMotivoAnterior.ParameterName = "p_codMotivoAnterior";
                        p_codMotivoAnterior.Value = codMoAnterior;
                        p_codMotivoAnterior.Direction = ParameterDirection.Output;
                        p_codMotivoAnterior.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codMotivoAnterior);

                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_PERSONA_AF_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "UpdateEstadoPersonaF", ex);
                    }
                }
            }
        }

        public Xpinn.Aportes.Entities.HistoricoCamEstado CrearHistorioCamEstado(Xpinn.Aportes.Entities.HistoricoCamEstado pHistorioCamEstado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pidconsecutivo.ParameterName = "p_idconsecutivo";
                        pidconsecutivo.Value = pHistorioCamEstado.idconsecutivo;
                        pidconsecutivo.Direction = ParameterDirection.Input;
                        pidconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidconsecutivo);

                        DbParameter pidafiliacion = cmdTransaccionFactory.CreateParameter();
                        pidafiliacion.ParameterName = "p_idafiliacion";
                        pidafiliacion.Value = pHistorioCamEstado.idafiliacion;
                        pidafiliacion.Direction = ParameterDirection.Input;
                        pidafiliacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidafiliacion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pHistorioCamEstado.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pestado_anterior = cmdTransaccionFactory.CreateParameter();
                        pestado_anterior.ParameterName = "p_estado_anterior";
                        if (pHistorioCamEstado.estado_anterior == null)
                            pestado_anterior.Value = DBNull.Value;
                        else
                            pestado_anterior.Value = pHistorioCamEstado.estado_anterior;
                        pestado_anterior.Direction = ParameterDirection.Input;
                        pestado_anterior.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado_anterior);

                        DbParameter pcod_motivo_anterior = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo_anterior.ParameterName = "p_cod_motivo_anterior";
                        if (pHistorioCamEstado.cod_motivo_anterior == null)
                            pcod_motivo_anterior.Value = DBNull.Value;
                        else
                            pcod_motivo_anterior.Value = pHistorioCamEstado.cod_motivo_anterior;
                        pcod_motivo_anterior.Direction = ParameterDirection.Input;
                        pcod_motivo_anterior.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo_anterior);

                        DbParameter pfecha_cambio = cmdTransaccionFactory.CreateParameter();
                        pfecha_cambio.ParameterName = "p_fecha_cambio";
                        pfecha_cambio.Value = pHistorioCamEstado.fecha_cambio;
                        pfecha_cambio.Direction = ParameterDirection.Input;
                        pfecha_cambio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_cambio);

                        DbParameter pestado_nuevo = cmdTransaccionFactory.CreateParameter();
                        pestado_nuevo.ParameterName = "p_estado_nuevo";
                        pestado_nuevo.Value = pHistorioCamEstado.estado_nuevo;
                        pestado_nuevo.Direction = ParameterDirection.Input;
                        pestado_nuevo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado_nuevo);

                        DbParameter pcod_motivo_nuevo = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo_nuevo.ParameterName = "p_cod_motivo_nuevo";
                        if (pHistorioCamEstado.cod_motivo_nuevo == null)
                            pcod_motivo_nuevo.Value = DBNull.Value;
                        else
                            pcod_motivo_nuevo.Value = pHistorioCamEstado.cod_motivo_nuevo;
                        pcod_motivo_nuevo.Direction = ParameterDirection.Input;
                        pcod_motivo_nuevo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo_nuevo);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pHistorioCamEstado.observaciones == null)
                            pobservaciones.Value = DBNull.Value;
                        else
                            pobservaciones.Value = pHistorioCamEstado.observaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pHistorioCamEstado.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = pHistorioCamEstado.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_HISTEST_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pHistorioCamEstado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HistorioCamEstadoData", "CrearHistorioCamEstado", ex);
                        return null;
                    }
                }
            }
        }


        public decimal ConsultarSMLMV(Usuario vUsuario)
        {
            decimal smlmv = 0;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT valor
                                        FROM GENERAL WHERE codigo = 10";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) smlmv = Convert.ToDecimal(resultado["valor"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return smlmv;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ConsultarSMLMV", ex);
                        return smlmv;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla AporteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla AporteS</param>
        /// <returns>Entidad Aporte consultado</returns>
        public Aporte ConsultarMaxAporte(Usuario vUsuario)
        {
            DbDataReader resultado;
            Aporte entidad = new Aporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT max(numero_aporte)  as NUMERO_APORTE FROM aporte";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
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
                        BOExcepcion.Throw("AporteData", "ConsultarAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla AporteS dados unos filtros
        /// </summary>
        /// <param name="pAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarDistribucionAporte(Usuario vUsuario, Int64 cliente, string pCod_linea_aporte)
        {
            DbDataReader resultado;

            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT a.cod_linea_aporte,a.nombre,g.porcentaje_dist as PORCENTAJE_CRUCE,b.cod_persona,b.valor_base as CUOTA,g.principal,b.numero_aporte,g.idgrupo FROM lineaaporte a inner join aporte b on a.cod_linea_aporte=b.cod_linea_aporte left join grupo_lineaaporte g on a.cod_linea_aporte=g.cod_linea_aporte 
                                        where b.cod_persona = ";
                        sql += cliente;
                        if (pCod_linea_aporte != null)
                        {
                            sql += " and g.idgrupo = ( select z.idgrupo from grupo_lineaaporte z where z.cod_linea_aporte = " + pCod_linea_aporte + ")"; ;
                        }
                        sql += " order by a.COD_LINEA_APORTE asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_linea_aporte = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["PORCENTAJE_CRUCE"] != DBNull.Value) entidad.porcentaje = Convert.ToInt64(resultado["PORCENTAJE_CRUCE"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["CUOTA"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt64(resultado["PRINCIPAL"]);
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["idgrupo"] != DBNull.Value) entidad.grupo = Convert.ToInt64(resultado["idgrupo"]);
                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarDistribucionAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla AporteS dados unos filtros
        /// </summary>
        /// <param name="pAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarDistrAporCambiarCuota(Usuario vUsuario, Int64 cliente)
        {
            DbDataReader resultado;

            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql1 = @"SELECT a.cod_linea_aporte, a.nombre, g.porcentaje_dist as PORCENTAJE_CRUCE, b.cod_persona, b.valor_base as CUOTA, b.numero_aporte, g.principal
                                        FROM lineaaporte a 
                                        INNER JOIN aporte b on a.cod_linea_aporte = b.cod_linea_aporte 
                                        INNER JOIN grupo_lineaaporte g on a.cod_linea_aporte = g.cod_linea_aporte ";

                        String sql2 = @" where b.cod_persona = ";
                        Int64 sql3 = cliente;
                        string sql = sql1 + sql2 + sql3 + " order by a.COD_LINEA_APORTE asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_linea_aporte = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["PORCENTAJE_CRUCE"] != DBNull.Value) entidad.porcentaje = Convert.ToInt64(resultado["PORCENTAJE_CRUCE"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["CUOTA"]);
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt64(resultado["PRINCIPAL"]);

                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarDistrAporCambiarCuota", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla AporteS dados unos filtros
        /// </summary>
        /// <param name="pAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aporte obtenidos</returns>
        public List<Aporte> ListarDistribucionAporteNuevo(Usuario vUsuario, Int64 pGrupo)
        {
            DbDataReader resultado;

            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql1 = @"SELECT a.COD_LINEA_APORTE, a.NOMBRE, B.PORCENTAJE_DIST, B.PRINCIPAL FROM lineaaporte a inner join grupo_lineaaporte B on a.cod_linea_aporte=b.cod_linea_aporte WHERE b.idgrupo = " + "'" + pGrupo.ToString() + "'";
                        String sql2 = @"  order by a.COD_LINEA_APORTE";

                        string sql = sql1 + sql2;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_linea_aporte = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["PORCENTAJE_DIST"] != DBNull.Value) entidad.porcentaje = Convert.ToInt64(resultado["PORCENTAJE_DIST"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt64(resultado["PRINCIPAL"]);
                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarDistribucionAporteNuevo", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Crea un registro en la tabla AporteS de la base de datos
        /// </summary>
        /// <param name="pAporte">Entidad Aporte</param>
        /// <returns>Entidad Aporte creada</returns>
        public Aporte CrearRetiroAporte(Aporte pAporte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Cod_Ope = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Ope.ParameterName = "pcod_ope";
                        p_Cod_Ope.Value = pAporte.cod_ope;
                        p_Cod_Ope.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Ope);

                        DbParameter p_numero_aporte = cmdTransaccionFactory.CreateParameter();
                        p_numero_aporte.ParameterName = "pnumero_aporte";
                        p_numero_aporte.Value = pAporte.numero_aporte;
                        p_numero_aporte.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_numero_aporte);

                        DbParameter p_fecha_retiro = cmdTransaccionFactory.CreateParameter();
                        p_fecha_retiro.ParameterName = "pfechaRetiro";
                        p_fecha_retiro.Value = pAporte.fecha_retiro;
                        p_fecha_retiro.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_retiro);

                        DbParameter p_valor_retiro = cmdTransaccionFactory.CreateParameter();
                        p_valor_retiro.ParameterName = "pvalorRetiro";
                        p_valor_retiro.Value = pAporte.valor_retiro;
                        p_valor_retiro.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_valor_retiro);

                        DbParameter p_autorizacion = cmdTransaccionFactory.CreateParameter();
                        p_autorizacion.ParameterName = "p_autorizacion";
                        p_autorizacion.Value = pAporte.autorizacion;
                        p_autorizacion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_autorizacion);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "pcodusuario";
                        pcod_usuario.Value = pAporte.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_RETIRO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "CrearAporte", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Crea un registro en la tabla AporteS de la base de datos
        /// </summary>
        /// <param name="pAporte">Entidad Aporte</param>
        /// <returns>Entidad Aporte creada</returns>
        public Aporte AplicarCruceCuentas(Aporte pAPORTE, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha_retiro = cmdTransaccionFactory.CreateParameter();
                        pfecha_retiro.ParameterName = "pFecha";
                        pfecha_retiro.Value = pAPORTE.fecha_retiro;
                        pfecha_retiro.Direction = ParameterDirection.Input;
                        pfecha_retiro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_retiro);

                        DbParameter pCod_Ope = cmdTransaccionFactory.CreateParameter();
                        pCod_Ope.ParameterName = "pCod_Ope";
                        pCod_Ope.Value = pAPORTE.cod_ope;
                        pCod_Ope.Direction = ParameterDirection.Input;
                        pCod_Ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCod_Ope);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "pCod_Persona";
                        pcod_persona.Value = pAPORTE.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "pSaldo";
                        psaldo.Value = pAPORTE.Saldo;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "pCod_Usuario";
                        pcod_usuario.Value = pUsuario.codusuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter ptipo_cruce = cmdTransaccionFactory.CreateParameter();
                        ptipo_cruce.ParameterName = "ptipo_cruce";
                        ptipo_cruce.Value = pAPORTE.tipo_cruce;
                        ptipo_cruce.Direction = ParameterDirection.Input;
                        ptipo_cruce.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cruce);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        if (pAPORTE.tipo_pago_cruce == 1)
                        { cmdTransaccionFactory.CommandText = "USP_XPINN_APO_CRUCEAPLICAR2"; }
                        else
                        { cmdTransaccionFactory.CommandText = "USP_XPINN_APO_CRUCEAPLICAR"; }

                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAPORTE;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("APORTEData", "AplicarCruceCuentas", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Crea un registro en la tabla AporteS de la base de datos
        /// </summary>
        /// <param name="pAporte">Entidad Aporte</param>
        /// <returns>Entidad Aporte creada</returns>
        public Aporte CrearCruceCuentas(Aporte pAPORTE, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidretiro = cmdTransaccionFactory.CreateParameter();
                        pidretiro.ParameterName = "p_idretiro";
                        pidretiro.Value = pAPORTE.idretiro;
                        pidretiro.Direction = ParameterDirection.Output;
                        pidretiro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidretiro);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pAPORTE.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha_retiro = cmdTransaccionFactory.CreateParameter();
                        pfecha_retiro.ParameterName = "p_fecha_retiro";
                        //pfecha_retiro.Value = pAPORTE.fecha_retiro; //Cambiado para que la fecha quedara con la indicada
                        if (pAPORTE.fecha_ultima_mod == null)
                            pfecha_retiro.Value = DBNull.Value;
                        else
                            pfecha_retiro.Value = pAPORTE.fecha_ultima_mod;
                        pfecha_retiro.Direction = ParameterDirection.Input;
                        pfecha_retiro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_retiro);

                        DbParameter pcod_motivo = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo.ParameterName = "p_cod_motivo";
                        pcod_motivo.Value = pAPORTE.cod_motivo;
                        pcod_motivo.Direction = ParameterDirection.Input;
                        pcod_motivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pAPORTE.observaciones != null) pobservaciones.Value = pAPORTE.observaciones; else pobservaciones.Value = DBNull.Value;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = pAPORTE.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        psaldo.Value = pAPORTE.Saldo;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        DbParameter pacta = cmdTransaccionFactory.CreateParameter();
                        pacta.ParameterName = "p_acta";
                        if (pAPORTE.acta != null) pacta.Value = pAPORTE.acta; else pacta.Value = DBNull.Value;
                        pacta.Direction = ParameterDirection.Input;
                        pacta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pacta);

                        DbParameter pcodope = cmdTransaccionFactory.CreateParameter();
                        pcodope.ParameterName = "p_cod_ope";
                        if (pAPORTE.cod_ope != 0) pcodope.Value = pAPORTE.cod_ope; else throw new Exception("Codigo de Operacion no puede ser 0 o nulo!.");
                        pcodope.Direction = ParameterDirection.Input;
                        pcodope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodope);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONARETIRO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pAPORTE, "PERSONA_RETIRO", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CruceDeCuentas, "Creacion de cruce de cuentas para la persona con codigo " + pAPORTE.cod_persona); //REGISTRO DE AUDITORIA

                        if (pidretiro.Value != null)
                            pAPORTE.idretiro = Convert.ToInt64(pidretiro.Value);
                        return pAPORTE;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "CrearCruceCuentas", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de detalles para el extracto
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Lineas de credito obtenidos</returns>
        public List<Aporte> ListarCruceCuentas(Aporte pAporte, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Aporte> lstResumen = new List<Aporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                connection.Open();
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pAporte.fecha_retiro;
                        pfecha.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "PCOD_PERSONA";
                        pcod_persona.Value = pAporte.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        //pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter ptipo_cruce = cmdTransaccionFactory.CreateParameter();
                        ptipo_cruce.ParameterName = "PTIPO_CRUCE";
                        ptipo_cruce.Value = pAporte.tipo_cruce;
                        ptipo_cruce.Direction = ParameterDirection.Input;
                        ptipo_cruce.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cruce);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "PCOD_USUARIO";
                        pcod_usuario.Value = pUsuario.codusuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (pAporte.tipo_pago_cruce == 1)
                        { cmdTransaccionFactory.CommandText = "USP_XPINN_APO_CRUCEGENERAR2"; }
                        else
                        { cmdTransaccionFactory.CommandText = "USP_XPINN_APO_CRUCEGENERAR"; }

                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarCruce", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        cmdTransaccionFactory.Connection = connection;
                        string sql = @"SELECT * FROM Temp_Cruce WHERE cod_persona = " + pAporte.cod_persona.ToString();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {

                            Aporte entidad = new Aporte();
                            //Asociar todos los valores a la entidad
                            if (resultado["concepto"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["concepto"]);
                            if (resultado["capital"] != DBNull.Value) entidad.capital = Convert.ToDecimal(resultado["capital"]);
                            if (resultado["interes"] != DBNull.Value) entidad.interes = Convert.ToDecimal(resultado["interes"]);
                            if (resultado["intmora"] != DBNull.Value) entidad.intmora = Convert.ToDecimal(resultado["intmora"]);
                            if (resultado["otros"] != DBNull.Value) entidad.otros = Convert.ToDecimal(resultado["otros"]);
                            if (resultado["retencion"] != DBNull.Value) entidad.retencion = Convert.ToDecimal(resultado["retencion"]);
                            if (resultado["signo"] != DBNull.Value) entidad.signo = Convert.ToString(resultado["signo"]);
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["total"]);
                            if (resultado["saldo"] != DBNull.Value) entidad.saldo_cruce = Convert.ToDecimal(resultado["saldo"]);
                            if (resultado["numero_producto"] != DBNull.Value) entidad.numproducto = Convert.ToInt64(resultado["numero_producto"]);
                            if (resultado["linea_producto"] != DBNull.Value) entidad.linea_producto = Convert.ToString(resultado["linea_producto"]);
                            if (resultado["interes_causado"] != DBNull.Value) entidad.interes_causado = Convert.ToDecimal(resultado["interes_causado"]);
                            if (resultado["retencioncausada"] != DBNull.Value) entidad.rentecioncausada = Convert.ToDecimal(resultado["retencioncausada"]);

                            lstResumen.Add(entidad);
                        }

                        return lstResumen;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarCruceCuentas", ex);
                        return null;
                    }
                }

            }
        }
        public List<Aporte> ListarSaldos(Aporte pAporte, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Aporte> lstResumen = new List<Aporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT c.numero_cdat as Numero, c.valor, 'CDAT' as TIPO FROM cdat c JOIN cdat_titular ct ON c.codigo_cdat=ct.codigo_cdat JOIN persona p ON p.cod_persona=ct.cod_persona
                                        WHERE c.estado != 3 AND c.valor > 0 and p.cod_persona = " + pAporte.cod_persona.ToString() + @"
                                        UNION ALL
                                        SELECT to_char(d.num_devolucion), d.saldo, 'DEVOLUCION' FROM devolucion d JOIN persona p ON p.cod_persona=d.cod_persona
                                        where d.saldo > 0 and p.cod_persona = " + pAporte.cod_persona.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {

                            Aporte entidad = new Aporte();
                            //Asociar todos los valores a la entidad
                            if (resultado["Numero"] != DBNull.Value) entidad.numero = Convert.ToString(resultado["Numero"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor_en = Convert.ToInt64(resultado["valor"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);

                            lstResumen.Add(entidad);
                        }

                        return lstResumen;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarSaldos", ex);
                        return null;
                    }
                }

            }
        }

        /// <summary>
        /// Crear el giro
        /// </summary>
        /// <param name="numero_radicacion"></param>
        /// <param name="cod_ope"></param>
        /// <param name="formadesembolso"></param>
        /// <param name="fecha_desembolso"></param>
        /// <param name="monto"></param>
        /// <param name="idCtaBancaria"></param>
        /// <param name="cod_banco"></param>
        /// <param name="numerocuenta"></param>
        /// <param name="tipo_cuenta"></param>
        /// <param name="codperson"></param>
        /// <param name="usuario"></param>
        /// <param name="pUsuario"></param>
        public void GuardarGiro(Int64 numero_radicacion, Int64 cod_ope, long formadesembolso, DateTime fecha_desembolso, double monto,
            int idCtaBancaria, int cod_banco, string numerocuenta, int tipo_cuenta, Int64 codperson, string usuario, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    Configuracion Configuracion = new Configuracion();
                    try
                    {
                        DbParameter pcodpersona = cmdTransaccionFactory.CreateParameter();
                        pcodpersona.ParameterName = "pcodpersona";
                        pcodpersona.Value = codperson;

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "pforma_pago";
                        pforma_pago.Value = formadesembolso;

                        DbParameter pfec_reg = cmdTransaccionFactory.CreateParameter();
                        pfec_reg.ParameterName = "pfec_reg";
                        pfec_reg.Value = fecha_desembolso;

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "pnumero_radicacion";
                        pnumero_radicacion.Value = numero_radicacion;

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcod_ope";
                        pcod_ope.Value = cod_ope;

                        DbParameter pusu_gen = cmdTransaccionFactory.CreateParameter();
                        pusu_gen.ParameterName = "pusu_gen";
                        pusu_gen.Value = usuario;
                        pusu_gen.DbType = DbType.AnsiString;
                        pusu_gen.Direction = ParameterDirection.Input;
                        pusu_gen.Size = 50;

                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                        pidctabancaria.ParameterName = "p_idctabancaria";
                        if (idCtaBancaria != 0) pidctabancaria.Value = idCtaBancaria; else pidctabancaria.Value = DBNull.Value;

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        if (cod_banco != 0) pcod_banco.Value = cod_banco; else pcod_banco.Value = DBNull.Value;

                        DbParameter pnum_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnum_cuenta.ParameterName = "p_num_cuenta";
                        if (numerocuenta != null) pnum_cuenta.Value = numerocuenta; else pnum_cuenta.Value = DBNull.Value;

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "p_tipo_cuenta";
                        if (tipo_cuenta != -1) ptipo_cuenta.Value = tipo_cuenta; else ptipo_cuenta.Value = DBNull.Value;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = monto;

                        cmdTransaccionFactory.Parameters.Add(pcodpersona);
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);
                        cmdTransaccionFactory.Parameters.Add(pfec_reg);
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);
                        cmdTransaccionFactory.Parameters.Add(pusu_gen);
                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);
                        cmdTransaccionFactory.Parameters.Add(pnum_cuenta);
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_DESEM_GIRO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "GuardarGiro", ex);
                        return;
                    }
                }
            }
        }


        public List<Aporte> ListarAportesControl(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT v.numero_aporte,v.cod_persona,v.identificacion,t.descripcion as nomtipo_identificacion,v.nombre "
                                        + "FROM V_APORTES v left join tipoidentificacion t on v.tipo_identificacion = t.codtipoidentificacion where 1 = 1" + filtro
                                        + "  ORDER BY 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMTIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["NOMTIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarAporte", ex);
                        return null;
                    }
                }
            }
        }


        public Aporte ConsultarPersonaRetiro(Int32 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Aporte entidad = new Aporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT P.*,V.NOMBRES,V.APELLIDOS,V.TIPO_IDENTIFICACION,V.IDENTIFICACION "
                                    + "FROM PERSONA_RETIRO P LEFT JOIN V_PERSONA V ON V.COD_PERSONA = P.COD_PERSONA WHERE P.IDRETIRO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDRETIRO"] != DBNull.Value) entidad.idretiro = Convert.ToInt64(resultado["IDRETIRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_RETIRO"] != DBNull.Value) entidad.fecha_retiro = Convert.ToDateTime(resultado["FECHA_RETIRO"]);
                            if (resultado["COD_MOTIVO"] != DBNull.Value) entidad.cod_motivo = Convert.ToInt64(resultado["COD_MOTIVO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["ACTA"] != DBNull.Value) entidad.acta = Convert.ToString(resultado["ACTA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.Apellidos = Convert.ToString(resultado["APELLIDOS"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ConsultarPersonaRetiro", ex);
                        return null;
                    }
                }
            }
        }


        public Aporte ConsultarTotalAportes(Int64 pId, DateTime pFecha, Usuario vUsuario)
        {
            DbDataReader resultado;
            Aporte entidad = new Aporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select sum(saldo) as saldo From aporte inner join lineaaporte on aporte.cod_linea_aporte = lineaaporte.cod_linea_aporte where  lineaaporte.cruzar = 1 and aporte.estado = 1 and cod_persona = " + pId.ToString() + " and (fecha_ultimo_pago is null or trunc(fecha_ultimo_pago) <= TO_DATE('" + pFecha.ToShortDateString() + "','dd/MM/yyyy'))";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["saldo"] != DBNull.Value) entidad.Saldo = Convert.ToInt64(resultado["saldo"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ConsultarTotalAportes", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionInteres> getListaAportesLiquidar(DateTime pfechaLiquidacion, String pCodLinea, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionInteres> listaEntidad = new List<LiquidacionInteres>();

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

                        DbParameter pcod_linea_programado = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_programado.ParameterName = "pcod_linea_aporte";
                        pcod_linea_programado.Value = pCodLinea;
                        pcod_linea_programado.Direction = ParameterDirection.Input;
                        pcod_linea_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_programado);

                        DbParameter pcodigo_usuario = cmdTransaccionFactory.CreateParameter();
                        pcodigo_usuario.ParameterName = "pcodigo_usuario";
                        pcodigo_usuario.Value = vUsuario.codusuario;
                        pcodigo_usuario.Direction = ParameterDirection.Input;
                        pcodigo_usuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_usuario);

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_LIQUIDACION";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "getListaAportesLiquidar", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.Connection = connection;

                        string sql = "select tem.*, vp.nombre, vp.identificacion from TEMP_LIQUIDAPRO tem inner join v_persona vp on tem.cod_persona = vp.cod_persona ";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LiquidacionInteres entidad = new LiquidacionInteres();

                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.Linea = Convert.ToInt64(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.Cod_Usuario = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["COD_OFICINA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.Fecha_Apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.Tasa_interes = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["DIAS"] != DBNull.Value) entidad.dias = Convert.ToInt32(resultado["DIAS"]);
                            if (resultado["INTERES"] != DBNull.Value) entidad.Interes = Convert.ToDecimal(resultado["INTERES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.Retefuente = Convert.ToDecimal(resultado["RETENCION"]);
                            if (resultado["VALOR_NETO"] != DBNull.Value) entidad.valor_Neto = Convert.ToDecimal(resultado["VALOR_NETO"]);

                            listaEntidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listaEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "getListaAportesLiquidar", ex);
                        return null;
                    }
                };
            }
        }

        // inserta la liquidacion de intereses 
        public Int64 InsertLiquidacion(Int64 pcod_op, String pnum_programado, Int64 pcod_Cliente, Int64 ptipo_tran, Decimal pvalor, Usuario pusuario, Int64 pestado, DateTime fecha)
        {
            Int64 pnum_tran = 0;
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
                        //p_num_tran.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(p_num_tran);

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        p_cod_ope.Value = pcod_op;
                        p_cod_ope.Direction = ParameterDirection.Input;
                        // p_cod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        DbParameter p_numero_programado = cmdTransaccionFactory.CreateParameter();
                        p_numero_programado.ParameterName = "p_numero_aporte";
                        p_numero_programado.Value = pnum_programado;
                        p_numero_programado.Direction = ParameterDirection.Input;
                        //  p_numero_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_numero_programado);

                        DbParameter p_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        p_cod_cliente.ParameterName = "p_cod_cliente";
                        p_cod_cliente.Value = pcod_Cliente;
                        p_cod_cliente.Direction = ParameterDirection.Input;
                        //p_cod_cliente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cliente);

                        DbParameter p_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tran.ParameterName = "p_tipo_tran";
                        p_tipo_tran.Value = ptipo_tran;
                        p_tipo_tran.Direction = ParameterDirection.Input;
                        // p_tipo_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tran);

                        DbParameter p_cod_det_lis = cmdTransaccionFactory.CreateParameter();
                        p_cod_det_lis.ParameterName = "p_cod_det_lis";
                        p_cod_det_lis.Value = DBNull.Value;
                        p_cod_det_lis.Direction = ParameterDirection.Input;
                        //p_cod_det_lis.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_det_lis);

                        DbParameter p_documento_soporte = cmdTransaccionFactory.CreateParameter();
                        p_documento_soporte.ParameterName = "p_documento_soporte";
                        p_documento_soporte.Value = DBNull.Value;
                        p_documento_soporte.Direction = ParameterDirection.Input;
                        // p_documento_soporte.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_documento_soporte);

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.Value = pvalor;
                        p_valor.Direction = ParameterDirection.Input;
                        //p_valor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pestado;
                        p_estado.Direction = ParameterDirection.Input;
                        //p_estado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        DbParameter p_num_tran_anula = cmdTransaccionFactory.CreateParameter();
                        p_num_tran_anula.ParameterName = "p_num_tran_anula";
                        p_num_tran_anula.Value = DBNull.Value;
                        p_num_tran_anula.Direction = ParameterDirection.Input;
                        // p_num_tran_anula.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_num_tran_anula);

                        DbParameter pn_fecha_interes = cmdTransaccionFactory.CreateParameter();
                        pn_fecha_interes.ParameterName = "pn_fecha_interes";
                        pn_fecha_interes.Value = fecha;
                        pn_fecha_interes.Direction = ParameterDirection.Input;
                        //  pn_fecha_interes.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pn_fecha_interes);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_TRANAPORTE_CREAR";

                        cmdTransaccionFactory.ExecuteNonQuery();
                        if (p_num_tran.Value != null)
                            pnum_tran = Convert.ToInt64(p_num_tran.Value);
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "InsertLiquidacion", ex);
                    }
                }
            }
            return pnum_tran;
        }


        public LiquidacionInteres CrearLiquidacionAportes(LiquidacionInteres pLiqui, Usuario vUsuario)
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
                        pcodigo_cdat.Value = pLiqui.NumeroCuenta;
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
                        if (pLiqui.interes_capitalizado != 0) pinteres_causado.Value = pLiqui.interes_capitalizado; else pinteres_causado.Value = DBNull.Value;
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

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_LIQUIDACIO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pLiqui.cod_interes = Convert.ToInt32(pcod_interes.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLiqui;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "CrearLiquidacionAportes", ex);
                        return null;
                    }
                }
            }
        }

        public void CrearTransaccionInteres(Pago_IntPermanente pInteres, Usuario pusuario)
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
                        p_num_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_num_tran);

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        p_cod_ope.Value = pInteres.cod_ope;
                        p_cod_ope.Direction = ParameterDirection.Input;
                        p_cod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        DbParameter p_numero_aporte = cmdTransaccionFactory.CreateParameter();
                        p_numero_aporte.ParameterName = "p_numero_aporte";
                        p_numero_aporte.Value = pInteres.numero_aporte;
                        p_numero_aporte.Direction = ParameterDirection.Input;
                        p_numero_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numero_aporte);

                        DbParameter p_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        p_cod_cliente.ParameterName = "p_cod_cliente";
                        p_cod_cliente.Value = pInteres.cod_persona;
                        p_cod_cliente.Direction = ParameterDirection.Input;
                        p_cod_cliente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cliente);

                        DbParameter p_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tran.ParameterName = "p_tipo_tran";
                        p_tipo_tran.Value = pInteres.tipo_tran;
                        p_tipo_tran.Direction = ParameterDirection.Input;
                        p_tipo_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tran);

                        DbParameter p_cod_atr = cmdTransaccionFactory.CreateParameter();
                        p_cod_atr.ParameterName = "p_cod_atr";
                        p_cod_atr.Value = pInteres.cod_atr;
                        p_cod_atr.Direction = ParameterDirection.Input;
                        p_cod_atr.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_atr);

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.Value = pInteres.valor;
                        p_valor.Direction = ParameterDirection.Input;
                        p_valor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = 1;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        DbParameter p_valor_causado = cmdTransaccionFactory.CreateParameter();
                        p_valor_causado.ParameterName = "p_valor_causado";
                        p_valor_causado.Value = 0;
                        p_valor_causado.Direction = ParameterDirection.Input;
                        p_valor_causado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valor_causado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_TRANINT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "CrearTransaccionInteres", ex);
                    }
                }
            }

        }

        public void GuardarLiquidacionAportes(LiquidacionInteres pLiqui, Usuario pusuario)
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


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_LIQU_INT_AHO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "GuardarLiquidacionAportes", ex);
                    }
                }
            }

        }

        public List<Aporte> ListarAporteReporteCierre(DateTime pFecha, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT a.cod_oficina,p.cod_persona,p.Identificacion,p.apellidos,p.nombres,h.numero_aporte,"
                                    + "l.cod_linea_aporte,l.nombre as nom_linea_aporte,h.cuota,h.saldo,h.fecha_proximo_pago,"
                                    + "h.fecha_ultimo_pago,d.descripcion periodicidad,a.fecha_interes,a.estado "
                                    + "from historico_aporte h inner join aporte a on h.numero_aporte=a.numero_aporte "
                                    + "inner join v_persona p on a.cod_persona = p.cod_persona "
                                    + "Left join lineaaporte l on a.cod_linea_aporte=l.cod_linea_aporte "
                                    + "Left join periodicidad d on a.cod_periodicidad = d.cod_periodicidad ";
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
                            Aporte entidad = new Aporte();
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.Apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOM_LINEA_APORTE"] != DBNull.Value) entidad.nom_linea_aporte = Convert.ToString(resultado["NOM_LINEA_APORTE"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            entidad.saldo_intereses = 0;
                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarAporteReporteCierre", ex);
                        return null;
                    }
                }
            }
        }


        public List<Aporte> ListarAportesClubAhorradores(Int64 pcliente, Boolean pResult, string pFiltroAdd, DateTime pFecha, Usuario vUsuario)
        {
            DbDataReader resultado;

            List<Aporte> lstAporte = new List<Aporte>();
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
                            sql = @"Select DISTINCT v_aportes.*, lineaaporte.cod_linea_aporte, lineaaporte.permite_pagoprod, (Select e.nom_empresa From empresa_recaudo e Where e.cod_empresa = v_aportes.cod_empresa) As nom_empresa,
                                        Calcular_VrAPagarAporte(v_aportes.numero_aporte, To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"')) As ValorAPagar,
                                        CASE nov.estado WHEN '0' THEN 'Solicitado' WHEN '1' THEN 'Aprobado' WHEN '2' THEN 'Negado' ELSE ' ' end as Estado_modificacion, 'PROPIO' AS TIPO_APORTE,
                                        SALDO_ACUMULADO(1,v_aportes.numero_aporte) AS VALOR_ACUMULADO, lineaaporte.cruzar
                                        From v_aportes 
                                        Inner Join lineaaporte On v_aportes.cod_linea_aporte = lineaaporte.cod_linea_aporte 
                                        Left Join novedad_cambio nov on v_aportes.numero_aporte = nov.numero_aporte
                                        and nov.IDNOVEDAD = ObtenerIdNovedadCambio(v_aportes.numero_aporte)
                                        Where v_aportes.cod_persona = " + pcliente;
                            if (!string.IsNullOrWhiteSpace(pFiltroAdd))
                                sql += " " + pFiltroAdd;
                            sql += " order by v_aportes.FECHA_APERTURA, lineaaporte.cod_linea_aporte";
                        }
                        else
                        {
                            sql = @"Select DISTINCT v_aportes.*, lineaaporte.cod_linea_aporte, lineaaporte.permite_pagoprod,
                                        (SELECT E.NOM_EMPRESA FROM EMPRESA_RECAUDO E WHERE E.COD_EMPRESA = V_APORTES.COD_EMPRESA) AS NOM_EMPRESA,
                                        CALCULAR_VRAPAGARAPORTE(V_APORTES.NUMERO_APORTE, TO_DATE('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"')) AS VALORAPAGAR,
                                        CASE nov.estado WHEN '0' THEN 'Solicitado' WHEN '1' THEN 'Aprobado' WHEN '2' THEN 'Negado' ELSE ' ' end as Estado_modificacion ,'PROPIO' AS TIPO_APORTE,
                                        SALDO_ACUMULADO(1,v_aportes.numero_aporte) AS VALOR_ACUMULADO, lineaaporte.cruzar
                                        From v_aportes
                                        Inner Join lineaaporte On v_aportes.cod_linea_aporte = lineaaporte.cod_linea_aporte
                                        LEFT JOIN NOVEDAD_CAMBIO NOV ON V_APORTES.NUMERO_APORTE = NOV.NUMERO_APORTE
                                        and nov.IDNOVEDAD = ObtenerIdNovedadCambio(v_aportes.numero_aporte)
                                        WHERE V_APORTES.COD_PERSONA = " + pcliente;
                            if (!string.IsNullOrWhiteSpace(pFiltroAdd))
                                sql += " " + pFiltroAdd;
                            sql += @"UNION ALL
                                        Select DISTINCT v_aportes.*, lineaaporte.cod_linea_aporte, lineaaporte.permite_pagoprod,
                                        (SELECT E.NOM_EMPRESA FROM EMPRESA_RECAUDO E WHERE E.COD_EMPRESA = V_APORTES.COD_EMPRESA) AS NOM_EMPRESA,
                                        CALCULAR_VRAPAGARAPORTE(V_APORTES.NUMERO_APORTE, TO_DATE('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"')) AS VALORAPAGAR,
                                        CASE nov.estado WHEN '0' THEN 'Solicitado' WHEN '1' THEN 'Aprobado' WHEN '2' THEN 'Negado' ELSE ' ' end as Estado_modificacion ,'CLUB AHORRADOR' AS TIPO_APORTE,
                                        SALDO_ACUMULADO(1,v_aportes.numero_aporte) AS VALOR_ACUMULADO, lineaaporte.cruzar
                                        From v_aportes
                                        Inner Join lineaaporte On v_aportes.cod_linea_aporte = lineaaporte.cod_linea_aporte
                                        LEFT JOIN NOVEDAD_CAMBIO NOV ON V_APORTES.NUMERO_APORTE = NOV.NUMERO_APORTE
                                        and nov.IDNOVEDAD = ObtenerIdNovedadCambio(v_aportes.numero_aporte)
                                        WHERE V_APORTES.COD_PERSONA IN (SELECT R.COD_PERSONA FROM PERSONA_RESPONSABLE R WHERE R.COD_PERSONA_TUTOR = " + pcliente + ")";
                            if (!string.IsNullOrWhiteSpace(pFiltroAdd))
                                sql += " " + pFiltroAdd;
                            sql += " order by v_aportes.FECHA_APERTURA desc, lineaaporte.cod_linea_aporte desc";
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
                            Aporte entidad = new Aporte();
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOM_LINEA_APORTE"] != DBNull.Value) entidad.nom_linea_aporte = Convert.ToString(resultado["NOM_LINEA_APORTE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (entidad.forma_pago == 1)
                                entidad.nom_forma_pago = "Caja";
                            if (entidad.forma_pago == 2)
                                entidad.nom_forma_pago = "Nómina";
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMA_MOD"] != DBNull.Value) entidad.fecha_ultima_mod = Convert.ToDateTime(resultado["FECHA_ULTIMA_MOD"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["TOTAL_INTERESES"] != DBNull.Value) entidad.total_intereses = Convert.ToDecimal(resultado["TOTAL_INTERESES"]);
                            if (resultado["TOTAL_RETENCION"] != DBNull.Value) entidad.total_retencion = Convert.ToDecimal(resultado["TOTAL_RETENCION"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["PORCENTAJE_DISTRIBUCION"] != DBNull.Value) entidad.porcentaje_distribucion = Convert.ToDecimal(resultado["PORCENTAJE_DISTRIBUCION"]);
                            if (resultado["BASE_CUOTA"] != DBNull.Value) entidad.base_cuota = Convert.ToInt32(resultado["BASE_CUOTA"]);
                            if (resultado["VALOR_BASE"] != DBNull.Value) entidad.valor_base = Convert.ToDecimal(resultado["VALOR_BASE"]);
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
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["FECHA_CREA"] != DBNull.Value) entidad.fecha_crea = Convert.ToDateTime(resultado["FECHA_CREA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["VALORAPAGAR"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["VALORAPAGAR"]);
                            if (resultado["TIPO_APORTE"] != DBNull.Value) entidad.tipo_registro = Convert.ToString(resultado["TIPO_APORTE"]);
                            if (resultado["VALOR_ACUMULADO"] != DBNull.Value) entidad.valor_acumulado = Convert.ToDecimal(resultado["VALOR_ACUMULADO"]);
                            // Si el aporte no se cruza no mostrar el saldo
                            string cruzar = "";
                            if (resultado["CRUZAR"] != DBNull.Value) cruzar = Convert.ToString(resultado["CRUZAR"]);
                            if (cruzar != "1")
                                entidad.Saldo = 0;
                            if (entidad.valor_acumulado > 0)
                                entidad.valor_total_acumu = entidad.Saldo + entidad.valor_acumulado;
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);
                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarEstadoCuentaAporteTodos", ex);
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

        public Xpinn.Comun.Entities.Cierea FechaUltimoCierre(Xpinn.Comun.Entities.Cierea pCierea, String filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from cierea" + ObtenerFiltro(pCierea) + " Order by fecha desc";

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

        public List<provision_aportes> ListarProvision(DateTime pFechaIni, provision_aportes pProvision, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<provision_aportes> lstProvision = new List<provision_aportes>();

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


                        //  DbParameter cod_linea_ahorro = cmdTransaccionFactory.CreateParameter();
                        //  cod_linea_ahorro.ParameterName = "PCODLINEAPROGRAMADO";
                        //  cod_linea_ahorro.Direction = ParameterDirection.Input;
                        //  cod_linea_ahorro.Value = pProvision.cod_linea_aporte;
                        //  cod_linea_ahorro.DbType = DbType.String;
                        //  cmdTransaccionFactory.Parameters.Add(cod_linea_ahorro);

                        DbParameter cod_oficina = cmdTransaccionFactory.CreateParameter();
                        cod_oficina.ParameterName = "PCODOFICINA";
                        cod_oficina.Direction = ParameterDirection.Input;
                        cod_oficina.Value = pProvision.cod_oficina;
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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_CAUSACION";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select * From TEMP_PROVISIONAPO ";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            provision_aportes entidad = new provision_aportes();

                            if (resultado["numero_aporte"] != DBNull.Value) entidad.numero_aporte = Convert.ToString(resultado["numero_aporte"].ToString());
                            if (resultado["fecha_apertura"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["fecha_apertura"].ToString());
                            if (resultado["saldo"] != DBNull.Value) entidad.saldo_total = Convert.ToInt32(resultado["saldo"]);
                            if (resultado["saldo_base"] != DBNull.Value) entidad.saldo_base = Convert.ToInt32(resultado["saldo_base"]);
                            if (resultado["provision_interes"] != DBNull.Value) entidad.intereses = Convert.ToInt32(resultado["provision_interes"]);
                            if (resultado["retencion"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["retencion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["cod_oficina"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["nombre"]);
                            if (resultado["cod_linea_aporte"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToString(resultado["cod_linea_aporte"]);
                            if (resultado["tasa"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["tasa"]);
                            if (resultado["dias"] != DBNull.Value) entidad.dias = Convert.ToInt32(resultado["dias"]);
                            if (resultado["valor_acumulado"] != DBNull.Value) entidad.valor_acumulado = Convert.ToInt32(resultado["valor_acumulado"]);

                            lstProvision.Add(entidad);
                        }
                        return lstProvision;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ListarProvision", ex);
                        return null;
                    }
                }
            }
        }

        public provision_aportes InsertarDatos(provision_aportes Insertar_cuenta, Usuario vUsuario)
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
                        numero_cuenta.Value = Insertar_cuenta.numero_aporte;
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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_CAUS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        //Insertar_cuenta.idprovision = Convert.ToInt32(pidprovision.Value);

                        return Insertar_cuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ciereaData", "Crearcierea", ex);
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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_CIEREA_CREAR";
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

        public List<Aporte> RptLibroSocios(string pFecha, bool incluir_retirados, Usuario pUsusario)
        {
            DbDataReader resultado;
            List<Aporte> lstAporte = new List<Aporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsusario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"WITH RESULTADO AS (

                                Select
                                p.COD_NOMINA,
                                P.IDENTIFICACION,
                                P.PRIMER_APELLIDO || ' ' || P.SEGUNDO_APELLIDO || ' ' || P.PRIMER_NOMBRE || ' ' || P.SEGUNDO_NOMBRE AS NOMBRE,
                                P.FECHANACIMIENTO,
                                p.SEXO,
                                p.CELULAR,
                                P.DIRECCION,
                                P.FECHA_AFILIACION,
                                p.telefono,
                                ER.NOM_EMPRESA,
                                ER.COD_EMPRESA,
                                NVL(ah.AHORRO_PERMANENTE, 0)AHORRO_PERMANENTE,
                                NVL(ap.APORTES_SOCIALES, 0) AS APORTES_SOCIALES,
                                NVL(sc.SALDO_CARTERA, 0) AS SALDO_CARTERA,
                                hp.FECHA_RETIRO,
                                hp.ESTADO
                                FROM
                                v_persona P
                                Left join historico_persona hp on hp.cod_persona = p.cod_persona and hp.fecha_historico = to_date('" +
                                     pFecha + @"', 'dd/MM/yyyy')
                                Left join historico_aporte ha on ha.cod_persona = hp.cod_persona and ha.fecha_historico = to_date('" +
                                     pFecha + @"', 'dd/MM/yyyy')
                                Left join empresa_recaudo ER ON P.Cod_Empresa = ER.Cod_Empresa
                                Left join persona_afiliacion PA ON P.cod_persona = PA.cod_persona
                                Left join (SELECT sum(H2.SALDO) AHORRO_PERMANENTE, h2.cod_persona FROM HISTORICO_APORTE H2 INNER JOIN LINEAAPORTE L ON H2.COD_LINEA_APORTE = L.COD_LINEA_APORTE WHERE L.TIPO_APORTE = 2 AND H2.FECHA_HISTORICO = to_date('" +
                                     pFecha + @"', 'dd/MM/yyyy')  Group by h2.cod_persona ) ah on ah.cod_persona = ha.cod_persona
                                Left join (SELECT SUM(H2.SALDO) APORTES_SOCIALES, h2.cod_persona FROM HISTORICO_APORTE H2 INNER JOIN LINEAAPORTE L ON H2.COD_LINEA_APORTE = L.COD_LINEA_APORTE WHERE L.TIPO_APORTE = 1 AND H2.FECHA_HISTORICO = to_date('" +
                                     pFecha + @"', 'dd/MM/yyyy')  Group by h2.cod_persona ) ap on ap.Cod_persona = ha.cod_persona
                                Left join (SELECT SUM(H3.SALDO_CAPITAL) SALDO_CARTERA, h3.cod_cliente FROM HISTORICO_CRE H3 WHERE  H3.SALDO_CAPITAL > 0 AND  h3.cod_linea_credito Not In(Select cod_linea_credito from parametros_linea where cod_parametro = 320) AND H3.FECHA_HISTORICO = to_date('" +
                                     pFecha + @"', 'dd/MM/yyyy')  Group by h3.cod_cliente) Sc on sc.cod_cliente = ha.cod_persona
                                Group by
                                p.COD_NOMINA, p.TELEFONO, p.SEXO, p.CELULAR, ER.COD_EMPRESA, p.IDENTIFICACION,P.PRIMER_APELLIDO || ' ' || P.SEGUNDO_APELLIDO || ' ' || P.PRIMER_NOMBRE || ' ' || P.SEGUNDO_NOMBRE, hp.ESTADO,
                                P.FECHANACIMIENTO,P.DIRECCION,P.FECHA_AFILIACION,ER.NOM_EMPRESA,hp.FECHA_RETIRO,ah.AHORRO_PERMANENTE,ap.APORTES_SOCIALES, sc.SALDO_CARTERA)
                                SELECT COD_NOMINA, IDENTIFICACION, NOMBRE,FECHANACIMIENTO,  DIRECCION,
                                FECHA_AFILIACION,AHORRO_PERMANENTE, APORTES_SOCIALES, AHORRO_PERMANENTE + APORTES_SOCIALES AS TOTAL_AH_Y_AP,
                                ESTADO, FECHA_RETIRO,SALDO_CARTERA, TELEFONO, CELULAR, SEXO, COD_EMPRESA, NOM_EMPRESA
                                FROM RESULTADO "
                            ;

                        if (!incluir_retirados)
                        {
                            sql += " WHERE (AHORRO_PERMANENTE + APORTES_SOCIALES > 0) and estado != 'R' ";
                        }
                        else
                        {
                            sql += "  WHERE (AHORRO_PERMANENTE + APORTES_SOCIALES > 0) OR(AHORRO_PERMANENTE + APORTES_SOCIALES = 0 AND ESTADO = 'R') OR  SALDO_CARTERA > 0 ";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Aporte entidad = new Aporte();
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["FECHA_AFILIACION"] != DBNull.Value) entidad.fecha_crea = Convert.ToDateTime(resultado["FECHA_AFILIACION"]);
                            if (resultado["AHORRO_PERMANENTE"] != DBNull.Value) entidad.otros = Convert.ToDecimal(resultado["AHORRO_PERMANENTE"]);
                            if (resultado["APORTES_SOCIALES"] != DBNull.Value) entidad.valor_acumulado = Convert.ToDecimal(resultado["APORTES_SOCIALES"]);
                            if (resultado["TOTAL_AH_Y_AP"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["TOTAL_AH_Y_AP"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado_modificacion = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHA_RETIRO"] != DBNull.Value) entidad.fecha_ultima_mod = Convert.ToDateTime(resultado["FECHA_RETIRO"]);
                            if (resultado["SALDO_CARTERA"] != DBNull.Value) entidad.cartera = Convert.ToDecimal(resultado["SALDO_CARTERA"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            lstAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "RptLibroSocios", ex);
                        return null;
                    }
                }
            }
        }
        public Aporte ClasificarPorDiasMora(Aporte vAporte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_aporte = cmdTransaccionFactory.CreateParameter();
                        p_numero_aporte.ParameterName = "p_Numero_Aporte";
                        p_numero_aporte.Value = vAporte.numero_aporte;
                        p_numero_aporte.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_numero_aporte);

                        DbParameter p_DiasMora = cmdTransaccionFactory.CreateParameter();
                        p_DiasMora.ParameterName = "p_DiasMora";
                        p_DiasMora.Value = vAporte.DiasMora;

                        cmdTransaccionFactory.Parameters.Add(p_DiasMora);

                        DbParameter p_Categoria = cmdTransaccionFactory.CreateParameter();
                        p_Categoria.ParameterName = "p_Categoria";
                        p_Categoria.Value = vAporte.Cod_Categoria;
                        cmdTransaccionFactory.Parameters.Add(p_Categoria);

                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_CATAPORTE";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        dbConnectionFactory.CerrarConexion(connection);
                        return vAporte;


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ClasificarPorDiasMora", ex);
                        return null;
                    }
                }
            }
        }

        public Aporte ConsultarCierreAportes(Usuario vUsuario)
        {
            DbDataReader resultado;
            Aporte entidad = new Aporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT MAX(FECHA) as fecha,estado FROM CIEREA WHERE TIPO = 'A' AND ESTADO = 'D'   group by estado";
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
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ClasificarPorDiasMora", ex);
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
        public bool ModificarEstadoSolicitud(Aporte pAporte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_ID_SOL_RETIRO = cmdTransaccionFactory.CreateParameter();
                        P_ID_SOL_RETIRO.ParameterName = "P_ID_SOL_RETIRO";
                        P_ID_SOL_RETIRO.Value = pAporte.idretiro;
                        P_ID_SOL_RETIRO.Direction = ParameterDirection.Input;
                        P_ID_SOL_RETIRO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ID_SOL_RETIRO);

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = pAporte.estado_modificacion;
                        P_ESTADO.Direction = ParameterDirection.Input;
                        P_ESTADO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLRETIRO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "Listar", ex);
                        return false;
                    }
                }
            }
        }

        public bool EsAfiancol(Int64 pCodPersona, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            bool _tieneAfiancol = false;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COUNT(S.REQAFIANCOL) AS REQAFIANCOL FROM CREDITO C 
                                        INNER JOIN SOLICITUDCRED S ON C.NUMERO_OBLIGACION = S.NUMEROSOLICITUD
                                        WHERE C.COD_DEUDOR = " + pCodPersona.ToString() + " AND S.REQAFIANCOL = 1 AND C.ESTADO = 'C' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            int _reqafiancol = 0;
                            if (resultado["REQAFIANCOL"] != DBNull.Value) _reqafiancol = Convert.ToInt32(resultado["REQAFIANCOL"]);
                            if (_reqafiancol == 0)
                                _tieneAfiancol = false;
                            else
                                _tieneAfiancol = true;
                        }

                        return _tieneAfiancol;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "EsAfiancol", ex);
                        return _tieneAfiancol;
                    }
                }

            }
        }



    }
}

