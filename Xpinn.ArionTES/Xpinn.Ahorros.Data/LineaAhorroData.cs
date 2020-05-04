using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla LineaAhorro
    /// </summary>
    public class LineaAhorroData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla LineaAhorro
        /// </summary>
        public LineaAhorroData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public LineaAhorro CrearLineaAhorro(LineaAhorro plineaahorro, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_ahorro = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_ahorro.ParameterName = "p_cod_linea_ahorro";
                        pcod_linea_ahorro.Value = plineaahorro.cod_linea_ahorro;
                        pcod_linea_ahorro.Direction = ParameterDirection.Input;
                        pcod_linea_ahorro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_ahorro);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = plineaahorro.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = plineaahorro.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        if (plineaahorro.cod_moneda == null)
                            pcod_moneda.Value = System.DBNull.Value;                            
                        else
                            pcod_moneda.Value = plineaahorro.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter pprioridad = cmdTransaccionFactory.CreateParameter();
                        pprioridad.ParameterName = "p_prioridad";
                        if (plineaahorro.prioridad == null)
                            pprioridad.Value = System.DBNull.Value;
                        else
                            pprioridad.Value = plineaahorro.prioridad;
                        pprioridad.Direction = ParameterDirection.Input;
                        pprioridad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprioridad);

                        DbParameter pvalor_apertura = cmdTransaccionFactory.CreateParameter();
                        pvalor_apertura.ParameterName = "p_valor_apertura";
                        if (plineaahorro.valor_apertura == null)
                            pvalor_apertura.Value = System.DBNull.Value;
                        else
                            pvalor_apertura.Value = plineaahorro.valor_apertura;
                        pvalor_apertura.Direction = ParameterDirection.Input;
                        pvalor_apertura.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_apertura);

                        DbParameter psaldo_minimo = cmdTransaccionFactory.CreateParameter();
                        psaldo_minimo.ParameterName = "p_saldo_minimo";
                        if (plineaahorro.saldo_minimo == null)
                            psaldo_minimo.Value = System.DBNull.Value;
                        else
                            psaldo_minimo.Value = plineaahorro.saldo_minimo;
                        psaldo_minimo.Direction = ParameterDirection.Input;
                        psaldo_minimo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_minimo);

                        DbParameter pmovimiento_minimo = cmdTransaccionFactory.CreateParameter();
                        pmovimiento_minimo.ParameterName = "p_movimiento_minimo";
                        if (plineaahorro.movimiento_minimo == null)
                            pmovimiento_minimo.Value = System.DBNull.Value;
                        else
                            pmovimiento_minimo.Value = plineaahorro.movimiento_minimo;
                        pmovimiento_minimo.Direction = ParameterDirection.Input;
                        pmovimiento_minimo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmovimiento_minimo);

                        DbParameter pmaximo_retiro_diario = cmdTransaccionFactory.CreateParameter();
                        pmaximo_retiro_diario.ParameterName = "p_maximo_retiro_diario";
                        if (plineaahorro.maximo_retiro_diario == null)
                            pmaximo_retiro_diario.Value = System.DBNull.Value;
                        else
                            pmaximo_retiro_diario.Value = plineaahorro.maximo_retiro_diario;
                        pmaximo_retiro_diario.Direction = ParameterDirection.Input;
                        pmaximo_retiro_diario.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmaximo_retiro_diario);

                        DbParameter pretiro_max_efectivo = cmdTransaccionFactory.CreateParameter();
                        pretiro_max_efectivo.ParameterName = "p_retiro_max_efectivo";
                        if (plineaahorro.retiro_max_efectivo == null)
                            pretiro_max_efectivo.Value = System.DBNull.Value;
                        else
                            pretiro_max_efectivo.Value = plineaahorro.retiro_max_efectivo;
                        pretiro_max_efectivo.Direction = ParameterDirection.Input;
                        pretiro_max_efectivo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pretiro_max_efectivo);

                        DbParameter pretiro_min_cheque = cmdTransaccionFactory.CreateParameter();
                        pretiro_min_cheque.ParameterName = "p_retiro_min_cheque";
                        if (plineaahorro.retiro_min_cheque == null)
                            pretiro_min_cheque.Value = System.DBNull.Value;
                        else
                            pretiro_min_cheque.Value = plineaahorro.retiro_min_cheque;
                        pretiro_min_cheque.Direction = ParameterDirection.Input;
                        pretiro_min_cheque.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pretiro_min_cheque);

                        DbParameter prequiere_libreta = cmdTransaccionFactory.CreateParameter();
                        prequiere_libreta.ParameterName = "p_requiere_libreta";
                        if (plineaahorro.requiere_libreta == null)
                            prequiere_libreta.Value = System.DBNull.Value;
                        else
                            prequiere_libreta.Value = plineaahorro.requiere_libreta;
                        prequiere_libreta.Direction = ParameterDirection.Input;
                        prequiere_libreta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(prequiere_libreta);

                        DbParameter pvalor_libreta = cmdTransaccionFactory.CreateParameter();
                        pvalor_libreta.ParameterName = "p_valor_libreta";
                        if (plineaahorro.valor_libreta == null)
                            pvalor_libreta.Value = System.DBNull.Value;
                        else
                            pvalor_libreta.Value = plineaahorro.valor_libreta;
                        pvalor_libreta.Direction = ParameterDirection.Input;
                        pvalor_libreta.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_libreta);

                        DbParameter pnum_desprendibles_lib = cmdTransaccionFactory.CreateParameter();
                        pnum_desprendibles_lib.ParameterName = "p_num_desprendibles_lib";
                        if (plineaahorro.num_desprendibles_lib == null)
                            pnum_desprendibles_lib.Value = System.DBNull.Value;
                        else
                            pnum_desprendibles_lib.Value = plineaahorro.num_desprendibles_lib;
                        pnum_desprendibles_lib.Direction = ParameterDirection.Input;
                        pnum_desprendibles_lib.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_desprendibles_lib);

                        DbParameter pcobra_primera_libreta = cmdTransaccionFactory.CreateParameter();
                        pcobra_primera_libreta.ParameterName = "p_cobra_primera_libreta";
                        if (plineaahorro.cobra_primera_libreta == null)
                            pcobra_primera_libreta.Value = System.DBNull.Value;
                        else
                            pcobra_primera_libreta.Value = plineaahorro.cobra_primera_libreta;
                        pcobra_primera_libreta.Direction = ParameterDirection.Input;
                        pcobra_primera_libreta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_primera_libreta);

                        DbParameter pcobra_perdida_libreta = cmdTransaccionFactory.CreateParameter();
                        pcobra_perdida_libreta.ParameterName = "p_cobra_perdida_libreta";
                        if (plineaahorro.cobra_perdida_libreta == null)
                            pcobra_perdida_libreta.Value = System.DBNull.Value;
                        else
                            pcobra_perdida_libreta.Value = plineaahorro.cobra_perdida_libreta;
                        pcobra_perdida_libreta.Direction = ParameterDirection.Input;
                        pcobra_perdida_libreta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_perdida_libreta);

                        DbParameter pcanje_automatico = cmdTransaccionFactory.CreateParameter();
                        pcanje_automatico.ParameterName = "p_canje_automatico";
                        if (plineaahorro.canje_automatico == null)
                            pcanje_automatico.Value = System.DBNull.Value;
                        else
                            pcanje_automatico.Value = plineaahorro.canje_automatico;
                        pcanje_automatico.Direction = ParameterDirection.Input;
                        pcanje_automatico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcanje_automatico);

                        DbParameter pdias_canje = cmdTransaccionFactory.CreateParameter();
                        pdias_canje.ParameterName = "p_dias_canje";
                        if (plineaahorro.dias_canje == null)
                            pdias_canje.Value = System.DBNull.Value;
                        else
                            pdias_canje.Value = plineaahorro.dias_canje;
                        pdias_canje.Direction = ParameterDirection.Input;
                        pdias_canje.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_canje);

                        DbParameter pinactivacion_automatica = cmdTransaccionFactory.CreateParameter();
                        pinactivacion_automatica.ParameterName = "p_inactivacion_automatica";
                        if (plineaahorro.inactivacion_automatica == null)
                            pinactivacion_automatica.Value = System.DBNull.Value;
                        else
                            pinactivacion_automatica.Value = plineaahorro.inactivacion_automatica;
                        pinactivacion_automatica.Direction = ParameterDirection.Input;
                        pinactivacion_automatica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pinactivacion_automatica);

                        DbParameter pdias_inactiva = cmdTransaccionFactory.CreateParameter();
                        pdias_inactiva.ParameterName = "p_dias_inactiva";
                        if (plineaahorro.dias_inactiva == null)
                            pdias_inactiva.Value = System.DBNull.Value;
                        else
                            pdias_inactiva.Value = plineaahorro.dias_inactiva;
                        pdias_inactiva.Direction = ParameterDirection.Input;
                        pdias_inactiva.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_inactiva);

                        DbParameter pcobro_cierre = cmdTransaccionFactory.CreateParameter();
                        pcobro_cierre.ParameterName = "p_cobro_cierre";
                        if (plineaahorro.cobro_cierre == null)
                            pcobro_cierre.Value = System.DBNull.Value;
                        else
                            pcobro_cierre.Value = plineaahorro.cobro_cierre;
                        pcobro_cierre.Direction = ParameterDirection.Input;
                        pcobro_cierre.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobro_cierre);

                        DbParameter pcierre_valor = cmdTransaccionFactory.CreateParameter();
                        pcierre_valor.ParameterName = "p_cierre_valor";
                        if (plineaahorro.cierre_valor == null)
                            pcierre_valor.Value = System.DBNull.Value;
                        else
                            pcierre_valor.Value = plineaahorro.cierre_valor;
                        pcierre_valor.Direction = ParameterDirection.Input;
                        pcierre_valor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcierre_valor);

                        DbParameter pcierre_dias = cmdTransaccionFactory.CreateParameter();
                        pcierre_dias.ParameterName = "p_cierre_dias";
                        if (plineaahorro.cierre_dias == null)
                            pcierre_dias.Value = System.DBNull.Value;
                        else
                            pcierre_dias.Value = plineaahorro.cierre_dias;
                        pcierre_dias.Direction = ParameterDirection.Input;
                        pcierre_dias.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcierre_dias);

                        DbParameter ptipo_saldo_int = cmdTransaccionFactory.CreateParameter();
                        ptipo_saldo_int.ParameterName = "p_tipo_saldo_int";
                        if (plineaahorro.tipo_saldo_int == null)
                            ptipo_saldo_int.Value = System.DBNull.Value;
                        else
                            ptipo_saldo_int.Value = plineaahorro.tipo_saldo_int;
                        ptipo_saldo_int.Direction = ParameterDirection.Input;
                        ptipo_saldo_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_saldo_int);

                        DbParameter pcod_periodicidad_int = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad_int.ParameterName = "p_cod_periodicidad_int";
                        if (plineaahorro.cod_periodicidad_int == null)
                            pcod_periodicidad_int.Value = System.DBNull.Value;
                        else
                            pcod_periodicidad_int.Value = plineaahorro.cod_periodicidad_int;
                        pcod_periodicidad_int.Direction = ParameterDirection.Input;
                        pcod_periodicidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad_int);

                        DbParameter pdias_gracia = cmdTransaccionFactory.CreateParameter();
                        pdias_gracia.ParameterName = "p_dias_gracia";
                        if (plineaahorro.dias_gracia == null)
                            pdias_gracia.Value = System.DBNull.Value;
                        else
                            pdias_gracia.Value = plineaahorro.dias_gracia;
                        pdias_gracia.Direction = ParameterDirection.Input;
                        pdias_gracia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_gracia);

                        DbParameter prealiza_provision = cmdTransaccionFactory.CreateParameter();
                        prealiza_provision.ParameterName = "p_realiza_provision";
                        if (plineaahorro.realiza_provision == null)
                            prealiza_provision.Value = System.DBNull.Value;
                        else
                            prealiza_provision.Value = plineaahorro.realiza_provision;
                        prealiza_provision.Direction = ParameterDirection.Input;
                        prealiza_provision.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(prealiza_provision);

                        DbParameter pinteres_dia_retencion = cmdTransaccionFactory.CreateParameter();
                        pinteres_dia_retencion.ParameterName = "p_interes_dia_retencion";
                        if (plineaahorro.interes_dia_retencion == null)
                            pinteres_dia_retencion.Value = System.DBNull.Value;
                        else
                            pinteres_dia_retencion.Value = plineaahorro.interes_dia_retencion;
                        pinteres_dia_retencion.Direction = ParameterDirection.Input;
                        pinteres_dia_retencion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pinteres_dia_retencion);

                        DbParameter pinteres_por_cuenta = cmdTransaccionFactory.CreateParameter();
                        pinteres_por_cuenta.ParameterName = "p_interes_por_cuenta";
                        if (plineaahorro.interes_por_cuenta == null)
                            pinteres_por_cuenta.Value = System.DBNull.Value;
                        else
                            pinteres_por_cuenta.Value = plineaahorro.interes_por_cuenta;
                        pinteres_por_cuenta.Direction = ParameterDirection.Input;
                        pinteres_por_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pinteres_por_cuenta);

                        DbParameter pforma_tasa = cmdTransaccionFactory.CreateParameter();
                        pforma_tasa.ParameterName = "p_forma_tasa";
                        if (plineaahorro.forma_tasa == null)
                            pforma_tasa.Value = System.DBNull.Value;
                        else
                            pforma_tasa.Value = plineaahorro.forma_tasa;
                        pforma_tasa.Direction = ParameterDirection.Input;
                        pforma_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (plineaahorro.tipo_historico == null)
                            ptipo_historico.Value = System.DBNull.Value;
                        else
                            ptipo_historico.Value = plineaahorro.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (plineaahorro.desviacion == null)
                            pdesviacion.Value = System.DBNull.Value;
                        else
                            pdesviacion.Value = plineaahorro.desviacion;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter ptipo_tasa = cmdTransaccionFactory.CreateParameter();
                        ptipo_tasa.ParameterName = "p_tipo_tasa";
                        if (plineaahorro.tipo_tasa == null)
                            ptipo_tasa.Value = System.DBNull.Value;
                        else
                            ptipo_tasa.Value = plineaahorro.tipo_tasa;
                        ptipo_tasa.Direction = ParameterDirection.Input;
                        ptipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tasa);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        if (plineaahorro.tasa == null)
                            ptasa.Value = System.DBNull.Value;
                        else
                            ptasa.Value = plineaahorro.tasa;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = plineaahorro.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        pusuariocreacion.Value = plineaahorro.usuariocreacion;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        pfecultmod.Value = plineaahorro.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "p_usuultmod";
                        pusuultmod.Value = plineaahorro.usuultmod;
                        pusuultmod.Direction = ParameterDirection.Input;
                        pusuultmod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);




                        DbParameter pretencion_por_cuenta = cmdTransaccionFactory.CreateParameter();
                        pretencion_por_cuenta.ParameterName = "pretencion_por_cuenta";
                        if (plineaahorro.retencion_por_cuenta == null)
                            pinteres_por_cuenta.Value = System.DBNull.Value;
                        else
                            pretencion_por_cuenta.Value = plineaahorro.retencion_por_cuenta;
                        pretencion_por_cuenta.Direction = ParameterDirection.Input;
                        pretencion_por_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pretencion_por_cuenta);


                        DbParameter p_saldo_minimo_liq = cmdTransaccionFactory.CreateParameter();
                        p_saldo_minimo_liq.ParameterName = "p_saldo_minimo_liq";
                        if (plineaahorro.saldo_minimo_liq == null)
                            p_saldo_minimo_liq.Value = System.DBNull.Value;
                        else
                            p_saldo_minimo_liq.Value = plineaahorro.saldo_minimo_liq;
                        p_saldo_minimo_liq.Direction = ParameterDirection.Input;
                        p_saldo_minimo_liq.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_saldo_minimo_liq);




                        DbParameter p_debitoautomatico = cmdTransaccionFactory.CreateParameter();
                        p_debitoautomatico.ParameterName = "p_debito_automatico";
                        p_debitoautomatico.Value = plineaahorro.debito_automatico;
                        p_debitoautomatico.Direction = ParameterDirection.Input;
                        p_debitoautomatico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_debitoautomatico);



                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_LINEAAHO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        return plineaahorro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("lineaahorroData", "Crearlineaahorro", ex);
                        return null;
                    }
                }
            }
        }

        public LineaAhorro ModificarLineaAhorro(LineaAhorro pLineaAhorro, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_ahorro = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_ahorro.ParameterName = "p_cod_linea_ahorro";
                        pcod_linea_ahorro.Value = pLineaAhorro.cod_linea_ahorro;
                        pcod_linea_ahorro.Direction = ParameterDirection.Input;
                        pcod_linea_ahorro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_ahorro);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pLineaAhorro.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pLineaAhorro.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        if (pLineaAhorro.cod_moneda == null)
                            pcod_moneda.Value = DBNull.Value;
                        else
                            pcod_moneda.Value = pLineaAhorro.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter pprioridad = cmdTransaccionFactory.CreateParameter();
                        pprioridad.ParameterName = "p_prioridad";
                        if (pLineaAhorro.prioridad == null)
                            pprioridad.Value = DBNull.Value;
                        else
                            pprioridad.Value = pLineaAhorro.prioridad;
                        pprioridad.Direction = ParameterDirection.Input;
                        pprioridad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprioridad);

                        DbParameter pvalor_apertura = cmdTransaccionFactory.CreateParameter();
                        pvalor_apertura.ParameterName = "p_valor_apertura";
                        pvalor_apertura.Value = pLineaAhorro.valor_apertura;
                        pvalor_apertura.Direction = ParameterDirection.Input;
                        pvalor_apertura.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_apertura);

                        DbParameter psaldo_minimo = cmdTransaccionFactory.CreateParameter();
                        psaldo_minimo.ParameterName = "p_saldo_minimo";
                        psaldo_minimo.Value = pLineaAhorro.saldo_minimo;
                        psaldo_minimo.Direction = ParameterDirection.Input;
                        psaldo_minimo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_minimo);

                        DbParameter pmovimiento_minimo = cmdTransaccionFactory.CreateParameter();
                        pmovimiento_minimo.ParameterName = "p_movimiento_minimo";
                        pmovimiento_minimo.Value = pLineaAhorro.movimiento_minimo;
                        pmovimiento_minimo.Direction = ParameterDirection.Input;
                        pmovimiento_minimo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmovimiento_minimo);

                        DbParameter pmaximo_retiro_diario = cmdTransaccionFactory.CreateParameter();
                        pmaximo_retiro_diario.ParameterName = "p_maximo_retiro_diario";
                        pmaximo_retiro_diario.Value = pLineaAhorro.maximo_retiro_diario;
                        pmaximo_retiro_diario.Direction = ParameterDirection.Input;
                        pmaximo_retiro_diario.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmaximo_retiro_diario);

                        DbParameter pretiro_max_efectivo = cmdTransaccionFactory.CreateParameter();
                        pretiro_max_efectivo.ParameterName = "p_retiro_max_efectivo";
                        if (pLineaAhorro.retiro_max_efectivo == null)
                            pretiro_max_efectivo.Value = DBNull.Value;
                        else
                            pretiro_max_efectivo.Value = pLineaAhorro.retiro_max_efectivo;
                        pretiro_max_efectivo.Direction = ParameterDirection.Input;
                        pretiro_max_efectivo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pretiro_max_efectivo);

                        DbParameter pretiro_min_cheque = cmdTransaccionFactory.CreateParameter();
                        pretiro_min_cheque.ParameterName = "p_retiro_min_cheque";
                        if (pLineaAhorro.retiro_min_cheque == null)
                            pretiro_min_cheque.Value = DBNull.Value;
                        else
                            pretiro_min_cheque.Value = pLineaAhorro.retiro_min_cheque;
                        pretiro_min_cheque.Direction = ParameterDirection.Input;
                        pretiro_min_cheque.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pretiro_min_cheque);

                        DbParameter prequiere_libreta = cmdTransaccionFactory.CreateParameter();
                        prequiere_libreta.ParameterName = "p_requiere_libreta";
                        if (pLineaAhorro.requiere_libreta == null)
                            prequiere_libreta.Value = DBNull.Value;
                        else
                            prequiere_libreta.Value = pLineaAhorro.requiere_libreta;
                        prequiere_libreta.Direction = ParameterDirection.Input;
                        prequiere_libreta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(prequiere_libreta);

                        DbParameter pvalor_libreta = cmdTransaccionFactory.CreateParameter();
                        pvalor_libreta.ParameterName = "p_valor_libreta";
                        if (pLineaAhorro.valor_libreta == null)
                            pvalor_libreta.Value = DBNull.Value;
                        else
                            pvalor_libreta.Value = pLineaAhorro.valor_libreta;
                        pvalor_libreta.Direction = ParameterDirection.Input;
                        pvalor_libreta.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_libreta);

                        DbParameter pnum_desprendibles_lib = cmdTransaccionFactory.CreateParameter();
                        pnum_desprendibles_lib.ParameterName = "p_num_desprendibles_lib";
                        if (pLineaAhorro.num_desprendibles_lib == null)
                            pnum_desprendibles_lib.Value = DBNull.Value;
                        else
                            pnum_desprendibles_lib.Value = pLineaAhorro.num_desprendibles_lib;
                        pnum_desprendibles_lib.Direction = ParameterDirection.Input;
                        pnum_desprendibles_lib.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_desprendibles_lib);

                        DbParameter pcobra_primera_libreta = cmdTransaccionFactory.CreateParameter();
                        pcobra_primera_libreta.ParameterName = "p_cobra_primera_libreta";
                        if (pLineaAhorro.cobra_primera_libreta == null)
                            pcobra_primera_libreta.Value = DBNull.Value;
                        else
                            pcobra_primera_libreta.Value = pLineaAhorro.cobra_primera_libreta;
                        pcobra_primera_libreta.Direction = ParameterDirection.Input;
                        pcobra_primera_libreta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_primera_libreta);

                        DbParameter pcobra_perdida_libreta = cmdTransaccionFactory.CreateParameter();
                        pcobra_perdida_libreta.ParameterName = "p_cobra_perdida_libreta";
                        if (pLineaAhorro.cobra_perdida_libreta == null)
                            pcobra_perdida_libreta.Value = DBNull.Value;
                        else
                            pcobra_perdida_libreta.Value = pLineaAhorro.cobra_perdida_libreta;
                        pcobra_perdida_libreta.Direction = ParameterDirection.Input;
                        pcobra_perdida_libreta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_perdida_libreta);

                        DbParameter pcanje_automatico = cmdTransaccionFactory.CreateParameter();
                        pcanje_automatico.ParameterName = "p_canje_automatico";
                        if (pLineaAhorro.canje_automatico == null)
                            pcanje_automatico.Value = DBNull.Value;
                        else
                            pcanje_automatico.Value = pLineaAhorro.canje_automatico;
                        pcanje_automatico.Direction = ParameterDirection.Input;
                        pcanje_automatico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcanje_automatico);

                        DbParameter pdias_canje = cmdTransaccionFactory.CreateParameter();
                        pdias_canje.ParameterName = "p_dias_canje";
                        if (pLineaAhorro.dias_canje == null)
                            pdias_canje.Value = DBNull.Value;
                        else
                            pdias_canje.Value = pLineaAhorro.dias_canje;
                        pdias_canje.Direction = ParameterDirection.Input;
                        pdias_canje.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_canje);

                        DbParameter pinactivacion_automatica = cmdTransaccionFactory.CreateParameter();
                        pinactivacion_automatica.ParameterName = "p_inactivacion_automatica";
                        if (pLineaAhorro.inactivacion_automatica == null)
                            pinactivacion_automatica.Value = DBNull.Value;
                        else
                            pinactivacion_automatica.Value = pLineaAhorro.inactivacion_automatica;
                        pinactivacion_automatica.Direction = ParameterDirection.Input;
                        pinactivacion_automatica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pinactivacion_automatica);

                        DbParameter pdias_inactiva = cmdTransaccionFactory.CreateParameter();
                        pdias_inactiva.ParameterName = "p_dias_inactiva";
                        if (pLineaAhorro.dias_inactiva == null)
                            pdias_inactiva.Value = DBNull.Value;
                        else
                            pdias_inactiva.Value = pLineaAhorro.dias_inactiva;
                        pdias_inactiva.Direction = ParameterDirection.Input;
                        pdias_inactiva.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_inactiva);

                        DbParameter pcobro_cierre = cmdTransaccionFactory.CreateParameter();
                        pcobro_cierre.ParameterName = "p_cobro_cierre";
                        if (pLineaAhorro.cobro_cierre == null)
                            pcobro_cierre.Value = DBNull.Value;
                        else
                            pcobro_cierre.Value = pLineaAhorro.cobro_cierre;
                        pcobro_cierre.Direction = ParameterDirection.Input;
                        pcobro_cierre.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobro_cierre);

                        DbParameter pcierre_valor = cmdTransaccionFactory.CreateParameter();
                        pcierre_valor.ParameterName = "p_cierre_valor";
                        if (pLineaAhorro.cierre_valor == null)
                            pcierre_valor.Value = DBNull.Value;
                        else
                            pcierre_valor.Value = pLineaAhorro.cierre_valor;
                        pcierre_valor.Direction = ParameterDirection.Input;
                        pcierre_valor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcierre_valor);

                        DbParameter pcierre_dias = cmdTransaccionFactory.CreateParameter();
                        pcierre_dias.ParameterName = "p_cierre_dias";
                        if (pLineaAhorro.cierre_dias == null)
                            pcierre_dias.Value = DBNull.Value;
                        else
                            pcierre_dias.Value = pLineaAhorro.cierre_dias;
                        pcierre_dias.Direction = ParameterDirection.Input;
                        pcierre_dias.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcierre_dias);

                        DbParameter ptipo_saldo_int = cmdTransaccionFactory.CreateParameter();
                        ptipo_saldo_int.ParameterName = "p_tipo_saldo_int";
                        if (pLineaAhorro.tipo_saldo_int == null)
                            ptipo_saldo_int.Value = DBNull.Value;
                        else
                            ptipo_saldo_int.Value = pLineaAhorro.tipo_saldo_int;
                        ptipo_saldo_int.Direction = ParameterDirection.Input;
                        ptipo_saldo_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_saldo_int);

                        DbParameter pcod_periodicidad_int = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad_int.ParameterName = "p_cod_periodicidad_int";
                        pcod_periodicidad_int.Value = pLineaAhorro.cod_periodicidad_int;
                        pcod_periodicidad_int.Direction = ParameterDirection.Input;
                        pcod_periodicidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad_int);

                        DbParameter pdias_gracia = cmdTransaccionFactory.CreateParameter();
                        pdias_gracia.ParameterName = "p_dias_gracia";
                        if (pLineaAhorro.dias_gracia == null)
                            pdias_gracia.Value = DBNull.Value;
                        else
                            pdias_gracia.Value = pLineaAhorro.dias_gracia;
                        pdias_gracia.Direction = ParameterDirection.Input;
                        pdias_gracia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_gracia);

                        DbParameter prealiza_provision = cmdTransaccionFactory.CreateParameter();
                        prealiza_provision.ParameterName = "p_realiza_provision";
                        if (pLineaAhorro.realiza_provision == null)
                            prealiza_provision.Value = DBNull.Value;
                        else
                            prealiza_provision.Value = pLineaAhorro.realiza_provision;
                        prealiza_provision.Direction = ParameterDirection.Input;
                        prealiza_provision.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(prealiza_provision);

                        DbParameter pinteres_dia_retencion = cmdTransaccionFactory.CreateParameter();
                        pinteres_dia_retencion.ParameterName = "p_interes_dia_retencion";
                        if (pLineaAhorro.interes_dia_retencion == null)
                            pinteres_dia_retencion.Value = DBNull.Value;
                        else
                            pinteres_dia_retencion.Value = pLineaAhorro.interes_dia_retencion;
                        pinteres_dia_retencion.Direction = ParameterDirection.Input;
                        pinteres_dia_retencion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pinteres_dia_retencion);

                        DbParameter pinteres_por_cuenta = cmdTransaccionFactory.CreateParameter();
                        pinteres_por_cuenta.ParameterName = "p_interes_por_cuenta";
                        if (pLineaAhorro.interes_por_cuenta == null)
                            pinteres_por_cuenta.Value = DBNull.Value;
                        else
                            pinteres_por_cuenta.Value = pLineaAhorro.interes_por_cuenta;
                        pinteres_por_cuenta.Direction = ParameterDirection.Input;
                        pinteres_por_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pinteres_por_cuenta);

                        DbParameter pforma_tasa = cmdTransaccionFactory.CreateParameter();
                        pforma_tasa.ParameterName = "p_forma_tasa";
                        if (pLineaAhorro.forma_tasa == null)
                            pforma_tasa.Value = DBNull.Value;
                        else
                            pforma_tasa.Value = pLineaAhorro.forma_tasa;
                        pforma_tasa.Direction = ParameterDirection.Input;
                        pforma_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pLineaAhorro.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pLineaAhorro.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pLineaAhorro.desviacion == null)
                            pdesviacion.Value = DBNull.Value;
                        else
                            pdesviacion.Value = pLineaAhorro.desviacion;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter ptipo_tasa = cmdTransaccionFactory.CreateParameter();
                        ptipo_tasa.ParameterName = "p_tipo_tasa";
                        if (pLineaAhorro.tipo_tasa == null)
                            ptipo_tasa.Value = DBNull.Value;
                        else
                            ptipo_tasa.Value = pLineaAhorro.tipo_tasa;
                        ptipo_tasa.Direction = ParameterDirection.Input;
                        ptipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tasa);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        if (pLineaAhorro.tasa == null)
                            ptasa.Value = DBNull.Value;
                        else
                            ptasa.Value = pLineaAhorro.tasa;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = pLineaAhorro.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        pusuariocreacion.Value = pLineaAhorro.usuariocreacion;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        if (pLineaAhorro.fecultmod == null)
                            pfecultmod.Value = DBNull.Value;
                        else
                            pfecultmod.Value = pLineaAhorro.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "p_usuultmod";
                        if (pLineaAhorro.usuultmod == null)
                            pusuultmod.Value = DBNull.Value;
                        else
                            pusuultmod.Value = pLineaAhorro.usuultmod;
                        pusuultmod.Direction = ParameterDirection.Input;
                        pusuultmod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);

                        DbParameter pretencion_por_cuenta = cmdTransaccionFactory.CreateParameter();
                        pretencion_por_cuenta.ParameterName = "pretencion_por_cuenta";
                        if (pLineaAhorro.retencion_por_cuenta == null)
                            pinteres_por_cuenta.Value = System.DBNull.Value;
                        else
                            pretencion_por_cuenta.Value = pLineaAhorro.retencion_por_cuenta;
                        pretencion_por_cuenta.Direction = ParameterDirection.Input;
                        pretencion_por_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pretencion_por_cuenta);


                        DbParameter p_saldo_minimo_liq = cmdTransaccionFactory.CreateParameter();
                        p_saldo_minimo_liq.ParameterName = "p_saldo_minimo_liq";
                        if (pLineaAhorro.saldo_minimo_liq == null)
                            p_saldo_minimo_liq.Value = System.DBNull.Value;
                        else
                            p_saldo_minimo_liq.Value = pLineaAhorro.saldo_minimo_liq;
                        p_saldo_minimo_liq.Direction = ParameterDirection.Input;
                        p_saldo_minimo_liq.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_saldo_minimo_liq);





                        DbParameter p_debitoautomatico = cmdTransaccionFactory.CreateParameter();
                        p_debitoautomatico.ParameterName = "p_debito_automatico";
                        p_debitoautomatico.Value = pLineaAhorro.debito_automatico;
                        p_debitoautomatico.Direction = ParameterDirection.Input;
                        p_debitoautomatico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_debitoautomatico);



                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_LINEAAHO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLineaAhorro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAhorroData", "ModificarLineaAhorro", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarLineaAhorro(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LineaAhorro pLineaAhorro = new LineaAhorro();
                        pLineaAhorro = ConsultarLineaAhorro(pId, pUsuario);

                        DbParameter pcod_linea_ahorro = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_ahorro.ParameterName = "p_cod_linea_ahorro";
                        pcod_linea_ahorro.Value = pLineaAhorro.cod_linea_ahorro;
                        pcod_linea_ahorro.Direction = ParameterDirection.Input;
                        pcod_linea_ahorro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_ahorro);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_LINEAAHORRO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAhorroData", "EliminarLineaAhorro", ex);
                    }
                }
            }
        }

        public LineaAhorro ConsultarLineaAhorro(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            LineaAhorro entidad = new LineaAhorro();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM LineaAhorro WHERE COD_LINEA_AHORRO = " + pId.ToString();
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["PRIORIDAD"] != DBNull.Value) entidad.prioridad = Convert.ToInt32(resultado["PRIORIDAD"]);
                            if (resultado["VALOR_APERTURA"] != DBNull.Value) entidad.valor_apertura = Convert.ToDecimal(resultado["VALOR_APERTURA"]);
                            if (resultado["SALDO_MINIMO"] != DBNull.Value) entidad.saldo_minimo = Convert.ToDecimal(resultado["SALDO_MINIMO"]);
                            if (resultado["MOVIMIENTO_MINIMO"] != DBNull.Value) entidad.movimiento_minimo = Convert.ToDecimal(resultado["MOVIMIENTO_MINIMO"]);
                            if (resultado["MAXIMO_RETIRO_DIARIO"] != DBNull.Value) entidad.maximo_retiro_diario = Convert.ToDecimal(resultado["MAXIMO_RETIRO_DIARIO"]);
                            if (resultado["RETIRO_MAX_EFECTIVO"] != DBNull.Value) entidad.retiro_max_efectivo = Convert.ToDecimal(resultado["RETIRO_MAX_EFECTIVO"]);
                            if (resultado["RETIRO_MIN_CHEQUE"] != DBNull.Value) entidad.retiro_min_cheque = Convert.ToDecimal(resultado["RETIRO_MIN_CHEQUE"]);
                            if (resultado["REQUIERE_LIBRETA"] != DBNull.Value) entidad.requiere_libreta = Convert.ToInt32(resultado["REQUIERE_LIBRETA"]);
                            if (resultado["VALOR_LIBRETA"] != DBNull.Value) entidad.valor_libreta = Convert.ToDecimal(resultado["VALOR_LIBRETA"]);
                            if (resultado["NUM_DESPRENDIBLES_LIB"] != DBNull.Value) entidad.num_desprendibles_lib = Convert.ToInt32(resultado["NUM_DESPRENDIBLES_LIB"]);
                            if (resultado["COBRA_PRIMERA_LIBRETA"] != DBNull.Value) entidad.cobra_primera_libreta = Convert.ToInt32(resultado["COBRA_PRIMERA_LIBRETA"]);
                            if (resultado["COBRA_PERDIDA_LIBRETA"] != DBNull.Value) entidad.cobra_perdida_libreta = Convert.ToInt32(resultado["COBRA_PERDIDA_LIBRETA"]);
                            if (resultado["CANJE_AUTOMATICO"] != DBNull.Value) entidad.canje_automatico = Convert.ToInt32(resultado["CANJE_AUTOMATICO"]);
                            if (resultado["DIAS_CANJE"] != DBNull.Value) entidad.dias_canje = Convert.ToInt32(resultado["DIAS_CANJE"]);
                            if (resultado["INACTIVACION_AUTOMATICA"] != DBNull.Value) entidad.inactivacion_automatica = Convert.ToInt32(resultado["INACTIVACION_AUTOMATICA"]);
                            if (resultado["DIAS_INACTIVA"] != DBNull.Value) entidad.dias_inactiva = Convert.ToInt32(resultado["DIAS_INACTIVA"]);
                            if (resultado["COBRO_CIERRE"] != DBNull.Value) entidad.cobro_cierre = Convert.ToInt32(resultado["COBRO_CIERRE"]);
                            if (resultado["CIERRE_VALOR"] != DBNull.Value) entidad.cierre_valor = Convert.ToDecimal(resultado["CIERRE_VALOR"]);
                            if (resultado["CIERRE_DIAS"] != DBNull.Value) entidad.cierre_dias = Convert.ToInt32(resultado["CIERRE_DIAS"]);
                            if (resultado["TIPO_SALDO_INT"] != DBNull.Value) entidad.tipo_saldo_int = Convert.ToInt32(resultado["TIPO_SALDO_INT"]);
                            if (resultado["COD_PERIODICIDAD_INT"] != DBNull.Value) entidad.cod_periodicidad_int = Convert.ToInt32(resultado["COD_PERIODICIDAD_INT"]);
                            if (resultado["DIAS_GRACIA"] != DBNull.Value) entidad.dias_gracia = Convert.ToInt32(resultado["DIAS_GRACIA"]);
                            if (resultado["REALIZA_PROVISION"] != DBNull.Value) entidad.realiza_provision = Convert.ToInt32(resultado["REALIZA_PROVISION"]);
                            if (resultado["INTERES_DIA_RETENCION"] != DBNull.Value) entidad.interes_dia_retencion = Convert.ToDecimal(resultado["INTERES_DIA_RETENCION"]);
                            if (resultado["INTERES_POR_CUENTA"] != DBNull.Value) entidad.interes_por_cuenta = Convert.ToInt32(resultado["INTERES_POR_CUENTA"]);
                            if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["RETENCION_POR_CUENTA"] != DBNull.Value) entidad.retencion_por_cuenta = Convert.ToInt32(resultado["RETENCION_POR_CUENTA"]);
                            if (resultado["saldo_minimo_liq"] != DBNull.Value) entidad.saldo_minimo_liq = Convert.ToInt32(resultado["saldo_minimo_liq"]);
                            if (resultado["debito_automatico"] != DBNull.Value) entidad.debito_automatico = Convert.ToInt32(resultado["debito_automatico"]);

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
                        BOExcepcion.Throw("LineaAhorroData", "ConsultarLineaAhorro", ex);
                        return null;
                    }
                }
            }
        }

        public LineaAhorro ConsultarLineaAhorro(Int64 pId, ref DbConnection pconnection, Usuario pUsuario)
        {
            DbDataReader resultado;
            LineaAhorro entidad = new LineaAhorro();
            using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
            {
                try
                {
                    string sql = @"SELECT * FROM LineaAhorro WHERE COD_LINEA_AHORRO = " + pId.ToString();
                    if (pconnection.State == ConnectionState.Closed)
                        pconnection.Open();
                    cmdTransaccionFactory.Connection = pconnection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();
                    if (resultado.Read())
                    {
                        if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                        if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                        if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                        if (resultado["PRIORIDAD"] != DBNull.Value) entidad.prioridad = Convert.ToInt32(resultado["PRIORIDAD"]);
                        if (resultado["VALOR_APERTURA"] != DBNull.Value) entidad.valor_apertura = Convert.ToDecimal(resultado["VALOR_APERTURA"]);
                        if (resultado["SALDO_MINIMO"] != DBNull.Value) entidad.saldo_minimo = Convert.ToDecimal(resultado["SALDO_MINIMO"]);
                        if (resultado["MOVIMIENTO_MINIMO"] != DBNull.Value) entidad.movimiento_minimo = Convert.ToDecimal(resultado["MOVIMIENTO_MINIMO"]);
                        if (resultado["MAXIMO_RETIRO_DIARIO"] != DBNull.Value) entidad.maximo_retiro_diario = Convert.ToDecimal(resultado["MAXIMO_RETIRO_DIARIO"]);
                        if (resultado["RETIRO_MAX_EFECTIVO"] != DBNull.Value) entidad.retiro_max_efectivo = Convert.ToDecimal(resultado["RETIRO_MAX_EFECTIVO"]);
                        if (resultado["RETIRO_MIN_CHEQUE"] != DBNull.Value) entidad.retiro_min_cheque = Convert.ToDecimal(resultado["RETIRO_MIN_CHEQUE"]);
                        if (resultado["REQUIERE_LIBRETA"] != DBNull.Value) entidad.requiere_libreta = Convert.ToInt32(resultado["REQUIERE_LIBRETA"]);
                        if (resultado["VALOR_LIBRETA"] != DBNull.Value) entidad.valor_libreta = Convert.ToDecimal(resultado["VALOR_LIBRETA"]);
                        if (resultado["NUM_DESPRENDIBLES_LIB"] != DBNull.Value) entidad.num_desprendibles_lib = Convert.ToInt32(resultado["NUM_DESPRENDIBLES_LIB"]);
                        if (resultado["COBRA_PRIMERA_LIBRETA"] != DBNull.Value) entidad.cobra_primera_libreta = Convert.ToInt32(resultado["COBRA_PRIMERA_LIBRETA"]);
                        if (resultado["COBRA_PERDIDA_LIBRETA"] != DBNull.Value) entidad.cobra_perdida_libreta = Convert.ToInt32(resultado["COBRA_PERDIDA_LIBRETA"]);
                        if (resultado["CANJE_AUTOMATICO"] != DBNull.Value) entidad.canje_automatico = Convert.ToInt32(resultado["CANJE_AUTOMATICO"]);
                        if (resultado["DIAS_CANJE"] != DBNull.Value) entidad.dias_canje = Convert.ToInt32(resultado["DIAS_CANJE"]);
                        if (resultado["INACTIVACION_AUTOMATICA"] != DBNull.Value) entidad.inactivacion_automatica = Convert.ToInt32(resultado["INACTIVACION_AUTOMATICA"]);
                        if (resultado["DIAS_INACTIVA"] != DBNull.Value) entidad.dias_inactiva = Convert.ToInt32(resultado["DIAS_INACTIVA"]);
                        if (resultado["COBRO_CIERRE"] != DBNull.Value) entidad.cobro_cierre = Convert.ToInt32(resultado["COBRO_CIERRE"]);
                        if (resultado["CIERRE_VALOR"] != DBNull.Value) entidad.cierre_valor = Convert.ToDecimal(resultado["CIERRE_VALOR"]);
                        if (resultado["CIERRE_DIAS"] != DBNull.Value) entidad.cierre_dias = Convert.ToInt32(resultado["CIERRE_DIAS"]);
                        if (resultado["TIPO_SALDO_INT"] != DBNull.Value) entidad.tipo_saldo_int = Convert.ToInt32(resultado["TIPO_SALDO_INT"]);
                        if (resultado["COD_PERIODICIDAD_INT"] != DBNull.Value) entidad.cod_periodicidad_int = Convert.ToInt32(resultado["COD_PERIODICIDAD_INT"]);
                        if (resultado["DIAS_GRACIA"] != DBNull.Value) entidad.dias_gracia = Convert.ToInt32(resultado["DIAS_GRACIA"]);
                        if (resultado["REALIZA_PROVISION"] != DBNull.Value) entidad.realiza_provision = Convert.ToInt32(resultado["REALIZA_PROVISION"]);
                        if (resultado["INTERES_DIA_RETENCION"] != DBNull.Value) entidad.interes_dia_retencion = Convert.ToDecimal(resultado["INTERES_DIA_RETENCION"]);
                        if (resultado["INTERES_POR_CUENTA"] != DBNull.Value) entidad.interes_por_cuenta = Convert.ToInt32(resultado["INTERES_POR_CUENTA"]);
                        if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                        if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                        if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                        if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                        if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                        if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                        if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                        if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                        if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                        if (resultado["RETENCION_POR_CUENTA"] != DBNull.Value) entidad.retencion_por_cuenta = Convert.ToInt32(resultado["RETENCION_POR_CUENTA"]);
                        if (resultado["saldo_minimo_liq"] != DBNull.Value) entidad.saldo_minimo_liq = Convert.ToInt32(resultado["saldo_minimo_liq"]);
                        if (resultado["debito_automatico"] != DBNull.Value) entidad.debito_automatico = Convert.ToInt32(resultado["debito_automatico"]);

                    }
                    return entidad;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public List<LineaAhorro> ListarLineaAhorro(LineaAhorro pLineaAhorro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<LineaAhorro> lstLineaAhorro = new List<LineaAhorro>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM LineaAhorro " + ObtenerFiltro(pLineaAhorro) + " ORDER BY COD_LINEA_AHORRO ";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineaAhorro entidad = new LineaAhorro();
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["PRIORIDAD"] != DBNull.Value) entidad.prioridad = Convert.ToInt32(resultado["PRIORIDAD"]);
                            if (resultado["VALOR_APERTURA"] != DBNull.Value) entidad.valor_apertura = Convert.ToDecimal(resultado["VALOR_APERTURA"]);
                            if (resultado["SALDO_MINIMO"] != DBNull.Value) entidad.saldo_minimo = Convert.ToDecimal(resultado["SALDO_MINIMO"]);
                            if (resultado["MOVIMIENTO_MINIMO"] != DBNull.Value) entidad.movimiento_minimo = Convert.ToDecimal(resultado["MOVIMIENTO_MINIMO"]);
                            if (resultado["MAXIMO_RETIRO_DIARIO"] != DBNull.Value) entidad.maximo_retiro_diario = Convert.ToDecimal(resultado["MAXIMO_RETIRO_DIARIO"]);
                            if (resultado["RETIRO_MAX_EFECTIVO"] != DBNull.Value) entidad.retiro_max_efectivo = Convert.ToDecimal(resultado["RETIRO_MAX_EFECTIVO"]);
                            if (resultado["RETIRO_MIN_CHEQUE"] != DBNull.Value) entidad.retiro_min_cheque = Convert.ToDecimal(resultado["RETIRO_MIN_CHEQUE"]);
                            if (resultado["REQUIERE_LIBRETA"] != DBNull.Value) entidad.requiere_libreta = Convert.ToInt32(resultado["REQUIERE_LIBRETA"]);
                            if (resultado["VALOR_LIBRETA"] != DBNull.Value) entidad.valor_libreta = Convert.ToDecimal(resultado["VALOR_LIBRETA"]);
                            if (resultado["NUM_DESPRENDIBLES_LIB"] != DBNull.Value) entidad.num_desprendibles_lib = Convert.ToInt32(resultado["NUM_DESPRENDIBLES_LIB"]);
                            if (resultado["COBRA_PRIMERA_LIBRETA"] != DBNull.Value) entidad.cobra_primera_libreta = Convert.ToInt32(resultado["COBRA_PRIMERA_LIBRETA"]);
                            if (resultado["COBRA_PERDIDA_LIBRETA"] != DBNull.Value) entidad.cobra_perdida_libreta = Convert.ToInt32(resultado["COBRA_PERDIDA_LIBRETA"]);
                            if (resultado["CANJE_AUTOMATICO"] != DBNull.Value) entidad.canje_automatico = Convert.ToInt32(resultado["CANJE_AUTOMATICO"]);
                            if (resultado["DIAS_CANJE"] != DBNull.Value) entidad.dias_canje = Convert.ToInt32(resultado["DIAS_CANJE"]);
                            if (resultado["INACTIVACION_AUTOMATICA"] != DBNull.Value) entidad.inactivacion_automatica = Convert.ToInt32(resultado["INACTIVACION_AUTOMATICA"]);
                            if (resultado["DIAS_INACTIVA"] != DBNull.Value) entidad.dias_inactiva = Convert.ToInt32(resultado["DIAS_INACTIVA"]);
                            if (resultado["COBRO_CIERRE"] != DBNull.Value) entidad.cobro_cierre = Convert.ToInt32(resultado["COBRO_CIERRE"]);
                            if (resultado["CIERRE_VALOR"] != DBNull.Value) entidad.cierre_valor = Convert.ToDecimal(resultado["CIERRE_VALOR"]);
                            if (resultado["CIERRE_DIAS"] != DBNull.Value) entidad.cierre_dias = Convert.ToInt32(resultado["CIERRE_DIAS"]);
                            if (resultado["TIPO_SALDO_INT"] != DBNull.Value) entidad.tipo_saldo_int = Convert.ToInt32(resultado["TIPO_SALDO_INT"]);
                            if (resultado["COD_PERIODICIDAD_INT"] != DBNull.Value) entidad.cod_periodicidad_int = Convert.ToInt32(resultado["COD_PERIODICIDAD_INT"]);
                            if (resultado["DIAS_GRACIA"] != DBNull.Value) entidad.dias_gracia = Convert.ToInt32(resultado["DIAS_GRACIA"]);
                            if (resultado["REALIZA_PROVISION"] != DBNull.Value) entidad.realiza_provision = Convert.ToInt32(resultado["REALIZA_PROVISION"]);
                            if (resultado["INTERES_DIA_RETENCION"] != DBNull.Value) entidad.interes_dia_retencion = Convert.ToDecimal(resultado["INTERES_DIA_RETENCION"]);
                            if (resultado["INTERES_POR_CUENTA"] != DBNull.Value) entidad.interes_por_cuenta = Convert.ToInt32(resultado["INTERES_POR_CUENTA"]);
                            if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["saldo_minimo_liq"] != DBNull.Value) entidad.saldo_minimo_liq = Convert.ToInt32(resultado["saldo_minimo_liq"]);
                            if (resultado["debito_automatico"] != DBNull.Value) entidad.debito_automatico = Convert.ToInt32(resultado["debito_automatico"]);

                            lstLineaAhorro.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineaAhorro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAhorroData", "ListarLineaAhorro", ex);
                        return null;
                    }
                }
            }
        }

    }
}
