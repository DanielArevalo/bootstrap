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
    /// Objeto de acceso a datos para la tabla LineaAporteS
    /// </summary>
    public class GrupoLineaAporteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        //List<int> lst_Num_Apor = new List<int>();
        

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla LineaAporteS
        /// </summary>
        public GrupoLineaAporteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla LineaAporteS de la base de datos
        /// </summary>
        /// <param name="pLineaAporte">Entidad LineaAporte</param>
        /// <returns>Entidad LineaAporte creada</returns>
        public GrupoLineaAporte CrearLineaAporte(GrupoLineaAporte pLineaAporte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_aporte = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_aporte.ParameterName = "pcod_linea_aporte";
                        pcod_linea_aporte.Value = pLineaAporte.cod_linea_aporte;
                        pcod_linea_aporte.Direction = ParameterDirection.Input;
                        // pcod_linea_aporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_aporte);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "pnombre";
                        pnombre.Value = pLineaAporte.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        // pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo_aporte = cmdTransaccionFactory.CreateParameter();
                        ptipo_aporte.ParameterName = "ptipo_aporte";
                        ptipo_aporte.Value = pLineaAporte.tipo_aporte;
                        ptipo_aporte.Direction = ParameterDirection.Input;
                        // ptipo_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_aporte);

                        DbParameter ptipo_cuota = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuota.ParameterName = "ptipo_cuota";
                        ptipo_cuota.Value = pLineaAporte.tipo_cuota;
                        ptipo_cuota.Direction = ParameterDirection.Input;
                        //ptipo_cuota.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuota);

                        DbParameter pvalor_cuota_minima = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota_minima.ParameterName = "pvalor_cuota_minima";
                        pvalor_cuota_minima.Value = pLineaAporte.valor_cuota_minima;
                        pvalor_cuota_minima.Direction = ParameterDirection.Input;
                        // pvalor_cuota_minima.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota_minima);

                        DbParameter pvalor_cuota_maximo = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota_maximo.ParameterName = "pvalor_cuota_maximo";
                        pvalor_cuota_maximo.Value = pLineaAporte.valor_cuota_maximo;
                        pvalor_cuota_maximo.Direction = ParameterDirection.Input;
                        pvalor_cuota_maximo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota_maximo);

                        DbParameter pobligatorio = cmdTransaccionFactory.CreateParameter();
                        pobligatorio.ParameterName = "pobligatorio";
                        pobligatorio.Value = pLineaAporte.obligatorio;
                        pobligatorio.Direction = ParameterDirection.Input;
                        // pobligatorio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pobligatorio);

                        DbParameter ptipo_liquidacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_liquidacion.ParameterName = "ptipo_liquidacion";
                        ptipo_liquidacion.Value = pLineaAporte.tipo_liquidacion;
                        ptipo_liquidacion.Direction = ParameterDirection.Input;
                        // ptipo_liquidacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_liquidacion);

                        DbParameter pmin_valor_pago = cmdTransaccionFactory.CreateParameter();
                        pmin_valor_pago.ParameterName = "pmin_valor_pago";
                        pmin_valor_pago.Value = pLineaAporte.min_valor_pago;
                        pmin_valor_pago.Direction = ParameterDirection.Input;
                        // pmin_valor_pago.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pmin_valor_pago);

                        DbParameter pmin_valor_retiro = cmdTransaccionFactory.CreateParameter();
                        pmin_valor_retiro.ParameterName = "pmin_valor_retiro";
                        pmin_valor_retiro.Value = pLineaAporte.min_valor_retiro;
                        pmin_valor_retiro.Direction = ParameterDirection.Input;
                        //  pmin_valor_retiro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pmin_valor_retiro);

                        DbParameter pmax_valor_retiro = cmdTransaccionFactory.CreateParameter();
                        pmax_valor_retiro.ParameterName = "pmax_valor_retiro";
                        pmax_valor_retiro.Value = pLineaAporte.max_valor_retiro;
                        pmax_valor_retiro.Direction = ParameterDirection.Input;
                        pmax_valor_retiro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pmax_valor_retiro);

                        DbParameter psaldo_minimo = cmdTransaccionFactory.CreateParameter();
                        psaldo_minimo.ParameterName = "psaldo_minimo";
                        psaldo_minimo.Value = pLineaAporte.saldo_minimo;
                        psaldo_minimo.Direction = ParameterDirection.Input;
                        //psaldo_minimo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(psaldo_minimo);

                        //DbParameter psaldo_minimo_liquid = cmdTransaccionFactory.CreateParameter();
                        //psaldo_minimo_liquid.ParameterName = "psaldo_minimo_liquid";
                        //psaldo_minimo_liquid.Value = pLineaAporte.saldo_minimo_Liqui;
                        //psaldo_minimo_liquid.Direction = ParameterDirection.Input;
                        //psaldo_minimo_liquid.DbType = DbType.Int64;
                        //cmdTransaccionFactory.Parameters.Add(psaldo_minimo_liquid);

                        DbParameter pvalor_cierre = cmdTransaccionFactory.CreateParameter();
                        pvalor_cierre.ParameterName = "pvalor_cierre";
                        pvalor_cierre.Value = pLineaAporte.valor_cierre;
                        pvalor_cierre.Direction = ParameterDirection.Input;
                        //pvalor_cierre.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cierre);

                        DbParameter pdias_cierre = cmdTransaccionFactory.CreateParameter();
                        pdias_cierre.ParameterName = "pdias_cierre";
                        pdias_cierre.Value = pLineaAporte.dias_cierre;
                        pdias_cierre.Direction = ParameterDirection.Input;
                        // pdias_cierre.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pdias_cierre);

                        DbParameter pcruzar = cmdTransaccionFactory.CreateParameter();
                        pcruzar.ParameterName = "pcruzar";
                        pcruzar.Value = pLineaAporte.cruzar;
                        pcruzar.Direction = ParameterDirection.Input;
                        //pcruzar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcruzar);

                        DbParameter pporcentaje_cruce = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_cruce.ParameterName = "pporcentaje_cruce";
                        pporcentaje_cruce.Value = pLineaAporte.porcentaje_cruce;
                        pporcentaje_cruce.Direction = ParameterDirection.Input;
                        pporcentaje_cruce.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_cruce);

                        DbParameter pcobra_mora = cmdTransaccionFactory.CreateParameter();
                        pcobra_mora.ParameterName = "pcobra_mora";
                        pcobra_mora.Value = pLineaAporte.cobra_mora;
                        pcobra_mora.Direction = ParameterDirection.Input;
                        //  pcobra_mora.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_mora);

                        DbParameter pprovisionar = cmdTransaccionFactory.CreateParameter();
                        pprovisionar.ParameterName = "pprovisionar";
                        pprovisionar.Value = pLineaAporte.provisionar;
                        pprovisionar.Direction = ParameterDirection.Input;
                        // pprovisionar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprovisionar);

                        DbParameter ppermite_retiros = cmdTransaccionFactory.CreateParameter();
                        ppermite_retiros.ParameterName = "ppermite_retiros";
                        ppermite_retiros.Value = pLineaAporte.permite_retiros;
                        ppermite_retiros.Direction = ParameterDirection.Input;
                        // ppermite_retiros.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppermite_retiros);

                        DbParameter ppermite_traslados = cmdTransaccionFactory.CreateParameter();
                        ppermite_traslados.ParameterName = "ppermite_traslados";
                        ppermite_traslados.Value = pLineaAporte.permite_traslados;
                        ppermite_traslados.Direction = ParameterDirection.Input;
                        ppermite_traslados.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppermite_traslados);

                        DbParameter pporcentaje_minimo = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_minimo.ParameterName = "pporcentaje_minimo";
                        pporcentaje_minimo.Value = pLineaAporte.porcentaje_minimo;
                        pporcentaje_minimo.Direction = ParameterDirection.Input;
                        // pporcentaje_minimo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_minimo);

                        DbParameter pporcentaje_maximo = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_maximo.ParameterName = "pporcentaje_maximo";
                        pporcentaje_maximo.Value = pLineaAporte.porcentaje_maximo;
                        pporcentaje_maximo.Direction = ParameterDirection.Input;
                        // pporcentaje_maximo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_maximo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Value = pLineaAporte.estado;
                        pestado.Direction = ParameterDirection.Input;
                        // pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pdistribuye = cmdTransaccionFactory.CreateParameter();
                        pdistribuye.ParameterName = "pdistribuye";
                        pdistribuye.Value = pLineaAporte.distribuye;
                        pdistribuye.Direction = ParameterDirection.Input;
                        //pdistribuye.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdistribuye);

                        DbParameter pporcentaje_distr = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_distr.ParameterName = "pporcentaje_distr";
                        pporcentaje_distr.Value = pLineaAporte.porcentaje;
                        pporcentaje_distr.Direction = ParameterDirection.Input;
                        // pporcentaje_distr.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_distr);

                        DbParameter pcap_minimo_irreduptible = cmdTransaccionFactory.CreateParameter();
                        pcap_minimo_irreduptible.ParameterName = "pcap_minimo_irreduptible";
                        pcap_minimo_irreduptible.Value = pLineaAporte.cap_minimo_irreduptible;
                        pcap_minimo_irreduptible.Direction = ParameterDirection.Input;
                        //pcap_minimo_irreduptible.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcap_minimo_irreduptible);

                        DbParameter p_permite_pagoprod = cmdTransaccionFactory.CreateParameter();
                        p_permite_pagoprod.ParameterName = "p_permite_pagoprod";
                        if (pLineaAporte.permite_pagoprod == null)
                            p_permite_pagoprod.Value = DBNull.Value;
                        else
                            p_permite_pagoprod.Value = pLineaAporte.permite_pagoprod;
                        p_permite_pagoprod.Direction = ParameterDirection.Input;
                        // p_permite_pagoprod.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_permite_pagoprod);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "pforma_pago";
                        pforma_pago.Value = pLineaAporte.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        //  pforma_pago.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter p_maximo_retiro_diario = cmdTransaccionFactory.CreateParameter();
                        p_maximo_retiro_diario.ParameterName = "p_maximo_retiro_diario";
                        p_maximo_retiro_diario.Value = pLineaAporte.max_valor_retiro;
                        p_maximo_retiro_diario.Direction = ParameterDirection.Input;
                        //  p_maximo_retiro_diario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_maximo_retiro_diario);

                        //añadido para liquidación de interes 

                        DbParameter ptipo_saldo_int = cmdTransaccionFactory.CreateParameter();
                        ptipo_saldo_int.ParameterName = "p_tipo_saldo_int";
                        if (pLineaAporte.tipo_saldo_int == null)
                            ptipo_saldo_int.Value = System.DBNull.Value;
                        else
                            ptipo_saldo_int.Value = pLineaAporte.tipo_saldo_int;
                        ptipo_saldo_int.Direction = ParameterDirection.Input;
                        //  ptipo_saldo_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_saldo_int);

                        DbParameter pcod_periodicidad_int = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad_int.ParameterName = "p_cod_periodicidad_int";
                        if (pLineaAporte.cod_periodicidad_int == null)
                            pcod_periodicidad_int.Value = System.DBNull.Value;
                        else
                            pcod_periodicidad_int.Value = pLineaAporte.cod_periodicidad_int;
                        pcod_periodicidad_int.Direction = ParameterDirection.Input;
                        //  pcod_periodicidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad_int);

                        DbParameter pdias_gracia = cmdTransaccionFactory.CreateParameter();
                        pdias_gracia.ParameterName = "p_dias_gracia";
                        if (pLineaAporte.dias_gracia == null)
                            pdias_gracia.Value = System.DBNull.Value;
                        else
                            pdias_gracia.Value = pLineaAporte.dias_gracia;
                        pdias_gracia.Direction = ParameterDirection.Input;
                        // pdias_gracia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_gracia);

                        DbParameter prealiza_provision = cmdTransaccionFactory.CreateParameter();
                        prealiza_provision.ParameterName = "p_realiza_provision";
                        if (pLineaAporte.realiza_provision == null)
                            prealiza_provision.Value = System.DBNull.Value;
                        else
                            prealiza_provision.Value = pLineaAporte.realiza_provision;
                        prealiza_provision.Direction = ParameterDirection.Input;
                        // prealiza_provision.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(prealiza_provision);

                        DbParameter pinteres_dia_retencion = cmdTransaccionFactory.CreateParameter();
                        pinteres_dia_retencion.ParameterName = "p_interes_dia_retencion";
                        if (pLineaAporte.interes_dia_retencion == null)
                            pinteres_dia_retencion.Value = System.DBNull.Value;
                        else
                            pinteres_dia_retencion.Value = pLineaAporte.interes_dia_retencion;
                        pinteres_dia_retencion.Direction = ParameterDirection.Input;
                        //  pinteres_dia_retencion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pinteres_dia_retencion);

                        DbParameter pinteres_por_cuenta = cmdTransaccionFactory.CreateParameter();
                        pinteres_por_cuenta.ParameterName = "p_interes_por_cuenta";
                        if (pLineaAporte.interes_por_cuenta == null)
                            pinteres_por_cuenta.Value = System.DBNull.Value;
                        else
                            pinteres_por_cuenta.Value = pLineaAporte.interes_por_cuenta;
                        pinteres_por_cuenta.Direction = ParameterDirection.Input;
                        //  pinteres_por_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pinteres_por_cuenta);

                        DbParameter pforma_tasa = cmdTransaccionFactory.CreateParameter();
                        pforma_tasa.ParameterName = "p_forma_tasa";
                        if (pLineaAporte.forma_tasa == null)
                            pforma_tasa.Value = System.DBNull.Value;
                        else
                            pforma_tasa.Value = pLineaAporte.forma_tasa;
                        pforma_tasa.Direction = ParameterDirection.Input;
                        //  pforma_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pLineaAporte.tipo_historico == null)
                            ptipo_historico.Value = System.DBNull.Value;
                        else
                            ptipo_historico.Value = pLineaAporte.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        // ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pLineaAporte.desviacion == null)
                            pdesviacion.Value = System.DBNull.Value;
                        else
                            pdesviacion.Value = pLineaAporte.desviacion;
                        pdesviacion.Direction = ParameterDirection.Input;
                        // pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter ptipo_tasa = cmdTransaccionFactory.CreateParameter();
                        ptipo_tasa.ParameterName = "p_tipo_tasa";
                        if (pLineaAporte.tipo_tasa == null)
                            ptipo_tasa.Value = System.DBNull.Value;
                        else
                            ptipo_tasa.Value = pLineaAporte.tipo_tasa;
                        ptipo_tasa.Direction = ParameterDirection.Input;
                        //  ptipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tasa);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        if (pLineaAporte.tasa == null)
                            ptasa.Value = System.DBNull.Value;
                        else
                            ptasa.Value = pLineaAporte.tasa;
                        ptasa.Direction = ParameterDirection.Input;
                        // ptasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa);


                        DbParameter pretencion_por_cuenta = cmdTransaccionFactory.CreateParameter();
                        pretencion_por_cuenta.ParameterName = "pretencion_por_cuenta";
                        if (pLineaAporte.retencion_por_cuenta == null)
                            pinteres_por_cuenta.Value = System.DBNull.Value;
                        else
                            pretencion_por_cuenta.Value = pLineaAporte.retencion_por_cuenta;
                        pretencion_por_cuenta.Direction = ParameterDirection.Input;
                        // pretencion_por_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pretencion_por_cuenta);



                        DbParameter p_saldo_minimo_liq = cmdTransaccionFactory.CreateParameter();
                        p_saldo_minimo_liq.ParameterName = "p_saldo_minimo_liq";
                        if (pLineaAporte.saldo_minimo_liq == null)
                            p_saldo_minimo_liq.Value = System.DBNull.Value;
                        else
                            p_saldo_minimo_liq.Value = pLineaAporte.saldo_minimo_liq;
                        p_saldo_minimo_liq.Direction = ParameterDirection.Input;
                        // p_saldo_minimo_liq.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_saldo_minimo_liq);

                        DbParameter p_Pago_Intereses = cmdTransaccionFactory.CreateParameter();
                        p_Pago_Intereses.ParameterName = "p_pago_intereses";
                        if (pLineaAporte.Pago_Intereses == null)
                            p_Pago_Intereses.Value = System.DBNull.Value;
                        else
                            p_Pago_Intereses.Value = pLineaAporte.Pago_Intereses;
                        p_Pago_Intereses.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_Pago_Intereses);

                        DbParameter pcod_linea_liqui_rev = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_liqui_rev.ParameterName = "p_cod_linea_liqui_rev";
                        if (pLineaAporte.cod_linea_liqui_rev == null || pLineaAporte.cod_linea_liqui_rev == 0)
                            pcod_linea_liqui_rev.Value = System.DBNull.Value;
                        else
                            pcod_linea_liqui_rev.Value = pLineaAporte.cod_linea_liqui_rev;
                        pcod_linea_liqui_rev.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_liqui_rev);

                        DbParameter p_cod_clasifica = cmdTransaccionFactory.CreateParameter();
                        p_cod_clasifica.ParameterName = "p_cod_clasifica";
                        if (pLineaAporte.cod_clasificacion == null || pLineaAporte.cod_clasificacion == 0)
                            p_cod_clasifica.Value = System.DBNull.Value;
                        else
                            p_cod_clasifica.Value = pLineaAporte.cod_clasificacion;
                        p_cod_clasifica.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_cod_clasifica);


                        DbParameter p_max_porcentaje_saldo = cmdTransaccionFactory.CreateParameter();
                        p_max_porcentaje_saldo.ParameterName = "p_max_porcentaje_saldo";
                        if (pLineaAporte.max_porcentaje_saldo == null || pLineaAporte.max_porcentaje_saldo == 0)
                            p_max_porcentaje_saldo.Value = DBNull.Value;
                        else
                            p_max_porcentaje_saldo.Value = pLineaAporte.max_porcentaje_saldo;
                        p_max_porcentaje_saldo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_max_porcentaje_saldo);
                        
                        DbParameter p_beneficiarios = cmdTransaccionFactory.CreateParameter();
                        p_beneficiarios.ParameterName = "p_beneficiarios";
                        p_beneficiarios.Value = pLineaAporte.beneficiarios;
                        p_beneficiarios.Direction = ParameterDirection.Input;                       
                        cmdTransaccionFactory.Parameters.Add(p_beneficiarios);
                        
                        DbParameter p_alerta = cmdTransaccionFactory.CreateParameter();
                        p_alerta.ParameterName = "p_alerta";
                        p_alerta.Value = pLineaAporte.alerta;
                        p_alerta.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_alerta);


                        DbParameter PPRIORIDAD = cmdTransaccionFactory.CreateParameter();
                        PPRIORIDAD.ParameterName = "P_PRIORIDAD";
                        PPRIORIDAD.Value = pLineaAporte.prioridad;
                        PPRIORIDAD.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PPRIORIDAD);



                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_LIN_APORTE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLineaAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAporteData", "CrearLineaAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla LineaAporteS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad LineaAporte modificada</returns>
        public GrupoLineaAporte ModificarLineaAporte(GrupoLineaAporte pLineaAporte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pcod_linea_aporte = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_aporte.ParameterName = "pcod_linea_aporte";
                        pcod_linea_aporte.Value = pLineaAporte.cod_linea_aporte;
                        pcod_linea_aporte.Direction = ParameterDirection.Input;
                        // pcod_linea_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_aporte);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "pnombre";
                        pnombre.Value = pLineaAporte.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        // pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo_aporte = cmdTransaccionFactory.CreateParameter();
                        ptipo_aporte.ParameterName = "ptipo_aporte";
                        ptipo_aporte.Value = pLineaAporte.tipo_aporte;
                        ptipo_aporte.Direction = ParameterDirection.Input;
                        // ptipo_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_aporte);

                        DbParameter ptipo_cuota = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuota.ParameterName = "ptipo_cuota";
                        ptipo_cuota.Value = pLineaAporte.tipo_cuota;
                        ptipo_cuota.Direction = ParameterDirection.Input;
                        //ptipo_cuota.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuota);

                        DbParameter pvalor_cuota_minima = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota_minima.ParameterName = "pvalor_cuota_minima";
                        pvalor_cuota_minima.Value = pLineaAporte.valor_cuota_minima;
                        pvalor_cuota_minima.Direction = ParameterDirection.Input;
                        // pvalor_cuota_minima.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota_minima);

                        DbParameter pvalor_cuota_maximo = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota_maximo.ParameterName = "pvalor_cuota_maximo";
                        pvalor_cuota_maximo.Value = pLineaAporte.valor_cuota_maximo;
                        pvalor_cuota_maximo.Direction = ParameterDirection.Input;
                        pvalor_cuota_maximo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota_maximo);

                        DbParameter pobligatorio = cmdTransaccionFactory.CreateParameter();
                        pobligatorio.ParameterName = "pobligatorio";
                        pobligatorio.Value = pLineaAporte.obligatorio;
                        pobligatorio.Direction = ParameterDirection.Input;
                        // pobligatorio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pobligatorio);

                        DbParameter ptipo_liquidacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_liquidacion.ParameterName = "ptipo_liquidacion";
                        ptipo_liquidacion.Value = pLineaAporte.tipo_liquidacion;
                        ptipo_liquidacion.Direction = ParameterDirection.Input;
                        // ptipo_liquidacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_liquidacion);

                        DbParameter pmin_valor_pago = cmdTransaccionFactory.CreateParameter();
                        pmin_valor_pago.ParameterName = "pmin_valor_pago";
                        pmin_valor_pago.Value = pLineaAporte.min_valor_pago;
                        pmin_valor_pago.Direction = ParameterDirection.Input;
                        // pmin_valor_pago.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pmin_valor_pago);

                        DbParameter pmin_valor_retiro = cmdTransaccionFactory.CreateParameter();
                        pmin_valor_retiro.ParameterName = "pmin_valor_retiro";
                        pmin_valor_retiro.Value = pLineaAporte.min_valor_retiro;
                        pmin_valor_retiro.Direction = ParameterDirection.Input;
                        //  pmin_valor_retiro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pmin_valor_retiro);

                        DbParameter pmax_valor_retiro = cmdTransaccionFactory.CreateParameter();
                        pmax_valor_retiro.ParameterName = "pmax_valor_retiro";
                        pmax_valor_retiro.Value = pLineaAporte.max_valor_retiro;
                        pmax_valor_retiro.Direction = ParameterDirection.Input;
                        pmax_valor_retiro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pmax_valor_retiro);

                        DbParameter psaldo_minimo = cmdTransaccionFactory.CreateParameter();
                        psaldo_minimo.ParameterName = "psaldo_minimo";
                        psaldo_minimo.Value = pLineaAporte.saldo_minimo;
                        psaldo_minimo.Direction = ParameterDirection.Input;
                        //psaldo_minimo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(psaldo_minimo);

                        //DbParameter psaldo_minimo_liquid = cmdTransaccionFactory.CreateParameter();
                        //psaldo_minimo_liquid.ParameterName = "psaldo_minimo_liquid";
                        //psaldo_minimo_liquid.Value = pLineaAporte.saldo_minimo_Liqui;
                        //psaldo_minimo_liquid.Direction = ParameterDirection.Input;
                        //psaldo_minimo_liquid.DbType = DbType.Int64;
                        //cmdTransaccionFactory.Parameters.Add(psaldo_minimo_liquid);

                        DbParameter pvalor_cierre = cmdTransaccionFactory.CreateParameter();
                        pvalor_cierre.ParameterName = "pvalor_cierre";
                        pvalor_cierre.Value = pLineaAporte.valor_cierre;
                        pvalor_cierre.Direction = ParameterDirection.Input;
                        //pvalor_cierre.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cierre);

                        DbParameter pdias_cierre = cmdTransaccionFactory.CreateParameter();
                        pdias_cierre.ParameterName = "pdias_cierre";
                        pdias_cierre.Value = pLineaAporte.dias_cierre;
                        pdias_cierre.Direction = ParameterDirection.Input;
                        // pdias_cierre.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pdias_cierre);

                        DbParameter pcruzar = cmdTransaccionFactory.CreateParameter();
                        pcruzar.ParameterName = "pcruzar";
                        pcruzar.Value = pLineaAporte.cruzar;
                        pcruzar.Direction = ParameterDirection.Input;
                        //pcruzar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcruzar);

                        DbParameter pporcentaje_cruce = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_cruce.ParameterName = "pporcentaje_cruce";
                        pporcentaje_cruce.Value = pLineaAporte.porcentaje_cruce;
                        pporcentaje_cruce.Direction = ParameterDirection.Input;
                        pporcentaje_cruce.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_cruce);

                        DbParameter pcobra_mora = cmdTransaccionFactory.CreateParameter();
                        pcobra_mora.ParameterName = "pcobra_mora";
                        pcobra_mora.Value = pLineaAporte.cobra_mora;
                        pcobra_mora.Direction = ParameterDirection.Input;
                        //  pcobra_mora.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_mora);

                        DbParameter pprovisionar = cmdTransaccionFactory.CreateParameter();
                        pprovisionar.ParameterName = "pprovisionar";
                        pprovisionar.Value = pLineaAporte.provisionar;
                        pprovisionar.Direction = ParameterDirection.Input;
                        // pprovisionar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprovisionar);

                        DbParameter ppermite_retiros = cmdTransaccionFactory.CreateParameter();
                        ppermite_retiros.ParameterName = "ppermite_retiros";
                        ppermite_retiros.Value = pLineaAporte.permite_retiros;
                        ppermite_retiros.Direction = ParameterDirection.Input;
                        // ppermite_retiros.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppermite_retiros);

                        DbParameter ppermite_traslados = cmdTransaccionFactory.CreateParameter();
                        ppermite_traslados.ParameterName = "ppermite_traslados";
                        ppermite_traslados.Value = pLineaAporte.permite_traslados;
                        ppermite_traslados.Direction = ParameterDirection.Input;
                        ppermite_traslados.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppermite_traslados);

                        DbParameter pporcentaje_minimo = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_minimo.ParameterName = "pporcentaje_minimo";
                        pporcentaje_minimo.Value = pLineaAporte.porcentaje_minimo;
                        pporcentaje_minimo.Direction = ParameterDirection.Input;
                        // pporcentaje_minimo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_minimo);

                        DbParameter pporcentaje_maximo = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_maximo.ParameterName = "pporcentaje_maximo";
                        pporcentaje_maximo.Value = pLineaAporte.porcentaje_maximo;
                        pporcentaje_maximo.Direction = ParameterDirection.Input;
                        // pporcentaje_maximo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_maximo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Value = pLineaAporte.estado;
                        pestado.Direction = ParameterDirection.Input;
                        // pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pdistribuye = cmdTransaccionFactory.CreateParameter();
                        pdistribuye.ParameterName = "pdistribuye";
                        pdistribuye.Value = pLineaAporte.distribuye;
                        pdistribuye.Direction = ParameterDirection.Input;
                        //pdistribuye.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdistribuye);

                        DbParameter pporcentaje_distr = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_distr.ParameterName = "pporcentaje_distr";
                        pporcentaje_distr.Value = pLineaAporte.porcentaje;
                        pporcentaje_distr.Direction = ParameterDirection.Input;
                        // pporcentaje_distr.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_distr);

                        DbParameter pcap_minimo_irreduptible = cmdTransaccionFactory.CreateParameter();
                        pcap_minimo_irreduptible.ParameterName = "pcap_minimo_irreduptible";
                        pcap_minimo_irreduptible.Value = pLineaAporte.cap_minimo_irreduptible;
                        pcap_minimo_irreduptible.Direction = ParameterDirection.Input;
                        //pcap_minimo_irreduptible.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcap_minimo_irreduptible);

                        DbParameter p_permite_pagoprod = cmdTransaccionFactory.CreateParameter();
                        p_permite_pagoprod.ParameterName = "p_permite_pagoprod";
                        if (pLineaAporte.permite_pagoprod == null)
                            p_permite_pagoprod.Value = DBNull.Value;
                        else
                            p_permite_pagoprod.Value = pLineaAporte.permite_pagoprod;
                        p_permite_pagoprod.Direction = ParameterDirection.Input;
                        // p_permite_pagoprod.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_permite_pagoprod);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "pforma_pago";
                        pforma_pago.Value = pLineaAporte.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        //  pforma_pago.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter p_maximo_retiro_diario = cmdTransaccionFactory.CreateParameter();
                        p_maximo_retiro_diario.ParameterName = "p_maximo_retiro_diario";
                        p_maximo_retiro_diario.Value = pLineaAporte.max_valor_retiro;
                        p_maximo_retiro_diario.Direction = ParameterDirection.Input;
                        //  p_maximo_retiro_diario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_maximo_retiro_diario);

                        //añadido para liquidación de interes 

                        DbParameter ptipo_saldo_int = cmdTransaccionFactory.CreateParameter();
                        ptipo_saldo_int.ParameterName = "p_tipo_saldo_int";
                        if (pLineaAporte.tipo_saldo_int == null)
                            ptipo_saldo_int.Value = System.DBNull.Value;
                        else
                            ptipo_saldo_int.Value = pLineaAporte.tipo_saldo_int;
                        ptipo_saldo_int.Direction = ParameterDirection.Input;
                        //  ptipo_saldo_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_saldo_int);

                        DbParameter pcod_periodicidad_int = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad_int.ParameterName = "p_cod_periodicidad_int";
                        if (pLineaAporte.cod_periodicidad_int == null)
                            pcod_periodicidad_int.Value = System.DBNull.Value;
                        else
                            pcod_periodicidad_int.Value = pLineaAporte.cod_periodicidad_int;
                        pcod_periodicidad_int.Direction = ParameterDirection.Input;
                        //  pcod_periodicidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad_int);

                        DbParameter pdias_gracia = cmdTransaccionFactory.CreateParameter();
                        pdias_gracia.ParameterName = "p_dias_gracia";
                        if (pLineaAporte.dias_gracia == null)
                            pdias_gracia.Value = System.DBNull.Value;
                        else
                            pdias_gracia.Value = pLineaAporte.dias_gracia;
                        pdias_gracia.Direction = ParameterDirection.Input;
                        // pdias_gracia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_gracia);

                        DbParameter prealiza_provision = cmdTransaccionFactory.CreateParameter();
                        prealiza_provision.ParameterName = "p_realiza_provision";
                        if (pLineaAporte.realiza_provision == null)
                            prealiza_provision.Value = System.DBNull.Value;
                        else
                            prealiza_provision.Value = pLineaAporte.realiza_provision;
                        prealiza_provision.Direction = ParameterDirection.Input;
                        // prealiza_provision.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(prealiza_provision);

                        DbParameter pinteres_dia_retencion = cmdTransaccionFactory.CreateParameter();
                        pinteres_dia_retencion.ParameterName = "p_interes_dia_retencion";
                        if (pLineaAporte.interes_dia_retencion == null)
                            pinteres_dia_retencion.Value = System.DBNull.Value;
                        else
                            pinteres_dia_retencion.Value = pLineaAporte.interes_dia_retencion;
                        pinteres_dia_retencion.Direction = ParameterDirection.Input;
                        //  pinteres_dia_retencion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pinteres_dia_retencion);

                        DbParameter pinteres_por_cuenta = cmdTransaccionFactory.CreateParameter();
                        pinteres_por_cuenta.ParameterName = "p_interes_por_cuenta";
                        if (pLineaAporte.interes_por_cuenta == null)
                            pinteres_por_cuenta.Value = System.DBNull.Value;
                        else
                            pinteres_por_cuenta.Value = pLineaAporte.interes_por_cuenta;
                        pinteres_por_cuenta.Direction = ParameterDirection.Input;
                        //  pinteres_por_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pinteres_por_cuenta);

                        DbParameter pforma_tasa = cmdTransaccionFactory.CreateParameter();
                        pforma_tasa.ParameterName = "p_forma_tasa";
                        if (pLineaAporte.forma_tasa == null)
                            pforma_tasa.Value = System.DBNull.Value;
                        else
                            pforma_tasa.Value = pLineaAporte.forma_tasa;
                        pforma_tasa.Direction = ParameterDirection.Input;
                        //  pforma_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pLineaAporte.tipo_historico == null)
                            ptipo_historico.Value = System.DBNull.Value;
                        else
                            ptipo_historico.Value = pLineaAporte.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        // ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pLineaAporte.desviacion == null)
                            pdesviacion.Value = System.DBNull.Value;
                        else
                            pdesviacion.Value = pLineaAporte.desviacion;
                        pdesviacion.Direction = ParameterDirection.Input;
                        // pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter ptipo_tasa = cmdTransaccionFactory.CreateParameter();
                        ptipo_tasa.ParameterName = "p_tipo_tasa";
                        if (pLineaAporte.tipo_tasa == null)
                            ptipo_tasa.Value = System.DBNull.Value;
                        else
                            ptipo_tasa.Value = pLineaAporte.tipo_tasa;
                        ptipo_tasa.Direction = ParameterDirection.Input;
                        //  ptipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tasa);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        if (pLineaAporte.tasa == null)
                            ptasa.Value = System.DBNull.Value;
                        else
                            ptasa.Value = pLineaAporte.tasa;
                        ptasa.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter pretencion_por_cuenta = cmdTransaccionFactory.CreateParameter();
                        pretencion_por_cuenta.ParameterName = "pretencion_por_cuenta";
                        if (pLineaAporte.retencion_por_cuenta == null)
                            pinteres_por_cuenta.Value = System.DBNull.Value;
                        else
                            pretencion_por_cuenta.Value = pLineaAporte.retencion_por_cuenta;
                        pretencion_por_cuenta.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pretencion_por_cuenta);

                        DbParameter p_saldo_minimo_liq = cmdTransaccionFactory.CreateParameter();
                        p_saldo_minimo_liq.ParameterName = "p_saldo_minimo_liq";
                        if (pLineaAporte.saldo_minimo_liq == null)
                            p_saldo_minimo_liq.Value = System.DBNull.Value;
                        else
                            p_saldo_minimo_liq.Value = pLineaAporte.saldo_minimo_liq;
                        p_saldo_minimo_liq.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_saldo_minimo_liq);

                        DbParameter p_Pago_Intereses = cmdTransaccionFactory.CreateParameter();
                        p_Pago_Intereses.ParameterName = "p_pago_intereses";
                        if (pLineaAporte.Pago_Intereses == null)
                            p_Pago_Intereses.Value = System.DBNull.Value;
                        else
                            p_Pago_Intereses.Value = pLineaAporte.Pago_Intereses;
                        p_Pago_Intereses.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_Pago_Intereses);

                        DbParameter pcod_linea_liqui_rev = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_liqui_rev.ParameterName = "p_cod_linea_liqui_rev";
                        if (pLineaAporte.cod_linea_liqui_rev == null || pLineaAporte.cod_linea_liqui_rev == 0)
                            pcod_linea_liqui_rev.Value = System.DBNull.Value;
                        else
                            pcod_linea_liqui_rev.Value = pLineaAporte.cod_linea_liqui_rev;
                        pcod_linea_liqui_rev.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_liqui_rev);


                        DbParameter p_cod_clasifica = cmdTransaccionFactory.CreateParameter();
                        p_cod_clasifica.ParameterName = "p_cod_califica";
                        if (pLineaAporte.cod_clasificacion == null || pLineaAporte.cod_clasificacion == 0)
                            p_cod_clasifica.Value = System.DBNull.Value;
                        else
                            p_cod_clasifica.Value = pLineaAporte.cod_clasificacion;
                        p_cod_clasifica.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_cod_clasifica);


                        DbParameter p_max_porcentaje_saldo = cmdTransaccionFactory.CreateParameter();
                        p_max_porcentaje_saldo.ParameterName = "p_max_porcentaje_saldo";
                        if (pLineaAporte.max_porcentaje_saldo == null || pLineaAporte.max_porcentaje_saldo == 0)
                            p_max_porcentaje_saldo.Value = DBNull.Value;
                        else
                            p_max_porcentaje_saldo.Value = pLineaAporte.max_porcentaje_saldo;
                        p_max_porcentaje_saldo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_max_porcentaje_saldo);

                        DbParameter p_beneficiarios = cmdTransaccionFactory.CreateParameter();
                        p_beneficiarios.ParameterName = "p_beneficiarios";
                        p_beneficiarios.Value = pLineaAporte.beneficiarios;
                        p_beneficiarios.Direction = ParameterDirection.Input;                    
                        cmdTransaccionFactory.Parameters.Add(p_beneficiarios);

                        DbParameter p_alerta = cmdTransaccionFactory.CreateParameter();
                        p_alerta.ParameterName = "p_alerta";
                        p_alerta.Value = pLineaAporte.alerta;
                        p_alerta.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_alerta);



                        DbParameter PPRIORIDAD = cmdTransaccionFactory.CreateParameter();
                        PPRIORIDAD.ParameterName = "P_PRIORIDAD";
                        PPRIORIDAD.Value = pLineaAporte.prioridad;
                        PPRIORIDAD.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PPRIORIDAD);



                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_LINEAAPO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pLineaAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAporteData", "ModificarLineaAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla LineaAporteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de LineaAporteS</param>
        public void EliminarLineaAporte(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        GrupoLineaAporte pLineaAporte = new GrupoLineaAporte();

                        DbParameter pcod_linea_aporte = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_aporte.ParameterName = "p_cod_linea_aporte";
                        pcod_linea_aporte.Value = pLineaAporte.cod_linea_aporte;
                        pcod_linea_aporte.Direction = ParameterDirection.Input;
                        pcod_linea_aporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_aporte);

                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_LINEAAPORTE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "EliminarConcepto", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla LineaAporteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla LineaAporteS</param>
        /// <returns>Entidad LineaAporte consultado</returns>
        public GrupoLineaAporte ConsultarLineaAporte(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            GrupoLineaAporte entidad = new GrupoLineaAporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM LineaAporte WHERE COD_LINEA_APORTE = " + pId.ToString();
                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_APORTE"] != DBNull.Value) entidad.tipo_aporte = Convert.ToInt32(resultado["TIPO_APORTE"]);
                            if (resultado["TIPO_CUOTA"] != DBNull.Value) entidad.tipo_cuota = Convert.ToInt32(resultado["TIPO_CUOTA"]);
                            if (resultado["VALOR_CUOTA_MINIMA"] != DBNull.Value) entidad.valor_cuota_minima = Convert.ToInt64(resultado["VALOR_CUOTA_MINIMA"]);
                            if (resultado["VALOR_CUOTA_MAXIMO"] != DBNull.Value) entidad.valor_cuota_maximo = Convert.ToInt64(resultado["VALOR_CUOTA_MAXIMO"]);
                            if (resultado["OBLIGATORIO"] != DBNull.Value) entidad.obligatorio = Convert.ToInt32(resultado["OBLIGATORIO"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["MIN_VALOR_PAGO"] != DBNull.Value) entidad.min_valor_pago = Convert.ToInt64(resultado["MIN_VALOR_PAGO"]);
                            if (resultado["MIN_VALOR_RETIRO"] != DBNull.Value) entidad.min_valor_retiro = Convert.ToInt64(resultado["MIN_VALOR_RETIRO"]);
                            if (resultado["MAX_VALOR_RETIRO"] != DBNull.Value) entidad.max_valor_retiro = Convert.ToInt64(resultado["MAX_VALOR_RETIRO"]);
                            if (resultado["SALDO_MINIMO"] != DBNull.Value) entidad.saldo_minimo = Convert.ToInt64(resultado["SALDO_MINIMO"]);
                            //if (resultado["saldo_minimo_liquid"] != DBNull.Value) entidad.saldo_minimo_Liqui= Convert.ToInt64(resultado["saldo_minimo_liquid"]);
                            if (resultado["VALOR_CIERRE"] != DBNull.Value) entidad.valor_cierre = Convert.ToInt64(resultado["VALOR_CIERRE"]);
                            if (resultado["DIAS_CIERRE"] != DBNull.Value) entidad.dias_cierre = Convert.ToInt64(resultado["DIAS_CIERRE"]);
                            if (resultado["CRUZAR"] != DBNull.Value) entidad.cruzar = Convert.ToInt32(resultado["CRUZAR"]);
                            if (resultado["PORCENTAJE_CRUCE"] != DBNull.Value) entidad.porcentaje_cruce = Convert.ToDecimal(resultado["PORCENTAJE_CRUCE"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt32(resultado["COBRA_MORA"]);
                            if (resultado["PROVISIONAR"] != DBNull.Value) entidad.provisionar = Convert.ToInt32(resultado["PROVISIONAR"]);
                            if (resultado["PERMITE_RETIROS"] != DBNull.Value) entidad.permite_retiros = Convert.ToInt32(resultado["PERMITE_RETIROS"]);
                            if (resultado["PERMITE_TRASLADOS"] != DBNull.Value) entidad.permite_traslados = Convert.ToInt32(resultado["PERMITE_TRASLADOS"]);
                            if (resultado["PERMITE_PAGOPROD"] != DBNull.Value) entidad.permite_pagoprod = Convert.ToInt32(resultado["PERMITE_PAGOPROD"]);
                            if (resultado["PORCENTAJE_MINIMO"] != DBNull.Value) entidad.porcentaje_minimo = Convert.ToDecimal(resultado["PORCENTAJE_MINIMO"]);
                            if (resultado["PORCENTAJE_MAXIMO"] != DBNull.Value) entidad.porcentaje_maximo = Convert.ToDecimal(resultado["PORCENTAJE_MAXIMO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["DISTRIBUYE"] != DBNull.Value) entidad.distribuye = Convert.ToInt32(resultado["DISTRIBUYE"]);
                            if (resultado["porcentaje_dist"] != DBNull.Value) entidad.porcentaje = Convert.ToInt32(resultado["porcentaje_dist"]);
                            if (resultado["CAP_MINIMO_IRREDUPTIBLE"] != DBNull.Value) entidad.cap_minimo_irreduptible = Convert.ToDecimal(resultado["CAP_MINIMO_IRREDUPTIBLE"]);
                            // if (resultado["COD_PERIODICIDAD_INT"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["COD_PERIODICIDAD_INT"]);
                            if (resultado["FORMA_PAGO_INT"] != DBNull.Value) entidad.forma_pago = Convert.ToInt64(resultado["FORMA_PAGO_INT"]);

                            if (resultado["cod_periodicidad_int"] != DBNull.Value) entidad.cod_periodicidad_int = Convert.ToInt32(resultado["cod_periodicidad_int"]);
                            if (resultado["tipo_saldo_int"] != DBNull.Value) entidad.tipo_saldo_int = Convert.ToInt32(resultado["tipo_saldo_int"]);
                            if (resultado["dias_gracia"] != DBNull.Value) entidad.dias_gracia = Convert.ToInt32(resultado["dias_gracia"]);
                            if (resultado["realiza_provision"] != DBNull.Value) entidad.realiza_provision = Convert.ToInt32(resultado["realiza_provision"]);
                            if (resultado["interes_dia_retencion"] != DBNull.Value) entidad.interes_dia_retencion = Convert.ToInt32(resultado["interes_dia_retencion"]);
                            if (resultado["interes_por_cuenta"] != DBNull.Value) entidad.interes_por_cuenta = Convert.ToInt32(resultado["interes_por_cuenta"]);
                            if (resultado["forma_tasa"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["forma_tasa"]);
                            if (resultado["tipo_historico"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["tipo_historico"]);
                            if (resultado["desviacion"] != DBNull.Value) entidad.desviacion = Convert.ToInt32(resultado["desviacion"]);
                            if (resultado["tipo_tasa"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["tipo_tasa"]);
                            if (resultado["retencion_por_cuenta"] != DBNull.Value) entidad.retencion_por_cuenta = Convert.ToInt32(resultado["retencion_por_cuenta"]);
                            if (resultado["tasa"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["tasa"]);
                            if (resultado["SALDO_MINIMO_LIQ"] != DBNull.Value) entidad.saldo_minimo_liq = Convert.ToInt32(resultado["SALDO_MINIMO_LIQ"]);
                            if (resultado["PAGO_INTERESES"] != DBNull.Value) entidad.Pago_Intereses = Convert.ToInt32(resultado["PAGO_INTERESES"]);
                            if (resultado["COD_LINEA_LIQUI_REV"] != DBNull.Value) entidad.cod_linea_liqui_rev = Convert.ToInt64(resultado["COD_LINEA_LIQUI_REV"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasificacion = Convert.ToInt64(resultado["COD_CLASIFICA"]);                            if (resultado["MAX_PORCENTAJE_SALDO"] != DBNull.Value) entidad.max_porcentaje_saldo = Convert.ToInt64(resultado["MAX_PORCENTAJE_SALDO"]);
                            if (resultado["BENEFICIARIOS"] != DBNull.Value) entidad.beneficiarios = Convert.ToInt32(resultado["BENEFICIARIOS"]);
                            if (resultado["ALERTA"] != DBNull.Value) entidad.alerta = Convert.ToInt32(resultado["ALERTA"]);
                            if (resultado["PRIORIDAD"] != DBNull.Value) entidad.prioridad = Convert.ToInt32(resultado["PRIORIDAD"]);
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
                        BOExcepcion.Throw("LineaAporteData", "ConsultarLineaAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla LineaAporteS dados unos filtros
        /// </summary>
        /// <param name="pLineaAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de LineaAporte obtenidos</returns>
        public List<GrupoLineaAporte> ListarLineaAporte(GrupoLineaAporte pLineaAporte, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<GrupoLineaAporte> lstLineaAporte = new List<GrupoLineaAporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM lineaaporte " + ObtenerFiltro(pLineaAporte) + " ORDER BY COD_LINEA_APORTE ";
                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            GrupoLineaAporte entidad = new GrupoLineaAporte();
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_APORTE"] != DBNull.Value) entidad.tipo_aporte = Convert.ToInt32(resultado["TIPO_APORTE"]);
                            if (resultado["TIPO_CUOTA"] != DBNull.Value) entidad.tipo_cuota = Convert.ToInt32(resultado["TIPO_CUOTA"]);
                            if (resultado["VALOR_CUOTA_MINIMA"] != DBNull.Value) entidad.valor_cuota_minima = Convert.ToInt64(resultado["VALOR_CUOTA_MINIMA"]);
                            if (resultado["VALOR_CUOTA_MAXIMO"] != DBNull.Value) entidad.valor_cuota_maximo = Convert.ToInt64(resultado["VALOR_CUOTA_MAXIMO"]);
                            if (resultado["OBLIGATORIO"] != DBNull.Value) entidad.obligatorio = Convert.ToInt32(resultado["OBLIGATORIO"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["MIN_VALOR_PAGO"] != DBNull.Value) entidad.min_valor_pago = Convert.ToInt64(resultado["MIN_VALOR_PAGO"]);
                            if (resultado["MIN_VALOR_RETIRO"] != DBNull.Value) entidad.min_valor_retiro = Convert.ToInt64(resultado["MIN_VALOR_RETIRO"]);
                            if (resultado["SALDO_MINIMO"] != DBNull.Value) entidad.saldo_minimo = Convert.ToInt64(resultado["SALDO_MINIMO"]);
                            if (resultado["VALOR_CIERRE"] != DBNull.Value) entidad.valor_cierre = Convert.ToInt64(resultado["VALOR_CIERRE"]);
                            if (resultado["DIAS_CIERRE"] != DBNull.Value) entidad.dias_cierre = Convert.ToInt64(resultado["DIAS_CIERRE"]);
                            if (resultado["CRUZAR"] != DBNull.Value) entidad.cruzar = Convert.ToInt32(resultado["CRUZAR"]);
                            if (resultado["PORCENTAJE_CRUCE"] != DBNull.Value) entidad.porcentaje_cruce = Convert.ToDecimal(resultado["PORCENTAJE_CRUCE"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt32(resultado["COBRA_MORA"]);
                            if (resultado["PROVISIONAR"] != DBNull.Value) entidad.provisionar = Convert.ToInt32(resultado["PROVISIONAR"]);
                            if (resultado["PERMITE_RETIROS"] != DBNull.Value) entidad.permite_retiros = Convert.ToInt32(resultado["PERMITE_RETIROS"]);
                            if (resultado["PERMITE_TRASLADOS"] != DBNull.Value) entidad.permite_traslados = Convert.ToInt32(resultado["PERMITE_TRASLADOS"]);
                            if (resultado["PORCENTAJE_MINIMO"] != DBNull.Value) entidad.porcentaje_minimo = Convert.ToDecimal(resultado["PORCENTAJE_MINIMO"]);
                            if (resultado["PORCENTAJE_MAXIMO"] != DBNull.Value) entidad.porcentaje_maximo = Convert.ToDecimal(resultado["PORCENTAJE_MAXIMO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            //AÑADIDO PARA LIQUIDACIÓN DE INTERES

                            if (resultado["cod_periodicidad_int"] != DBNull.Value) entidad.cod_periodicidad_int = Convert.ToInt32(resultado["cod_periodicidad_int"]);
                            if (resultado["tipo_saldo_int"] != DBNull.Value) entidad.tipo_saldo_int = Convert.ToInt32(resultado["tipo_saldo_int"]);
                            if (resultado["dias_gracia"] != DBNull.Value) entidad.dias_gracia = Convert.ToInt32(resultado["dias_gracia"]);
                            if (resultado["realiza_provision"] != DBNull.Value) entidad.realiza_provision = Convert.ToInt32(resultado["realiza_provision"]);
                            if (resultado["interes_dia_retencion"] != DBNull.Value) entidad.interes_dia_retencion = Convert.ToInt32(resultado["interes_dia_retencion"]);
                            if (resultado["interes_por_cuenta"] != DBNull.Value) entidad.interes_por_cuenta = Convert.ToInt32(resultado["interes_por_cuenta"]);
                            if (resultado["forma_tasa"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["forma_tasa"]);
                            if (resultado["tipo_historico"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["tipo_historico"]);
                            if (resultado["desviacion"] != DBNull.Value) entidad.desviacion = Convert.ToInt32(resultado["desviacion"]);
                            if (resultado["tipo_tasa"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["tipo_tasa"]);
                            if (resultado["retencion_por_cuenta"] != DBNull.Value) entidad.retencion_por_cuenta = Convert.ToInt32(resultado["retencion_por_cuenta"]);
                            if (resultado["tasa"] != DBNull.Value) entidad.tasa = Convert.ToInt32(resultado["tasa"]);
                            if (resultado["SALDO_MINIMO_LIQ"] != DBNull.Value) entidad.saldo_minimo_liq = Convert.ToInt32(resultado["SALDO_MINIMO_LIQ"]);


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
                            if (resultado["PORCENTAJE_DIST"] != DBNull.Value) entidad.porcentaje = Convert.ToInt32(resultado["PORCENTAJE_DIST"]);
                            if (resultado["MAX_PORCENTAJE_SALDO"] != DBNull.Value) entidad.max_porcentaje_saldo = Convert.ToInt64(resultado["MAX_PORCENTAJE_SALDO"]);
                            lstLineaAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineaAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAporteData", "ListarLineaAporte", ex);
                        return null;
                    }
                }
            }
        }


        //****


        /// <summary>
        /// Crea un registro en la tabla GrupoAporteS de la base de datos
        /// </summary>
        /// <param name="pGrupoAporte">Entidad GrupoAporte</param>
        /// <returns>Entidad GrupoAporte creada</returns>
        public GrupoLineaAporte CrearGrupoAporte(GrupoLineaAporte pGrupoAporte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pid_grupo = cmdTransaccionFactory.CreateParameter();
                        pid_grupo.ParameterName = "pid_grupo";
                        pid_grupo.Value = pGrupoAporte.idgrupo;
                        pid_grupo.Direction = ParameterDirection.Input;
                        pid_grupo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_grupo);

                        DbParameter pcodlinea_aporte = cmdTransaccionFactory.CreateParameter();
                        pcodlinea_aporte.ParameterName = "pcodlinea_aporte";
                        pcodlinea_aporte.Value = pGrupoAporte.cod_linea_aporte;
                        pcodlinea_aporte.Direction = ParameterDirection.Input;
                        pcodlinea_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodlinea_aporte);


                        DbParameter pporcentaje_dist = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_dist.ParameterName = "pporcentaje_dist";
                        pporcentaje_dist.Value = pGrupoAporte.porcentaje;
                        pporcentaje_dist.Direction = ParameterDirection.Input;
                        pporcentaje_dist.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_dist);

                        DbParameter pprincipal = cmdTransaccionFactory.CreateParameter();
                        pprincipal.ParameterName = "pprincipal";
                        pprincipal.Value = pGrupoAporte.principal;
                        pprincipal.Direction = ParameterDirection.Input;
                        pprincipal.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pprincipal);

                        DbParameter ptipo_distribucion = cmdTransaccionFactory.CreateParameter();
                        ptipo_distribucion.ParameterName = "ptipo_distribucion";
                        ptipo_distribucion.Value = pGrupoAporte.tipo_distribucion;
                        ptipo_distribucion.Direction = ParameterDirection.Input;
                        ptipo_distribucion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_distribucion);

                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_GRUPOAPO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pGrupoAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GrupoAporteData", "CrearGrupoAporte", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Modifica un registro en la tabla GrupoAporteS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad GrupoAporte modificada</returns>
        public GrupoLineaAporte ModificarGrupoAporte(GrupoLineaAporte pGrupoAporte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pid_grupo = cmdTransaccionFactory.CreateParameter();
                        pid_grupo.ParameterName = "pid_grupo";
                        pid_grupo.Value = pGrupoAporte.idgrupo;
                        pid_grupo.Direction = ParameterDirection.Input;
                        //zpid_grupo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_grupo);

                        DbParameter p_cod_linea_aporte = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_aporte.ParameterName = "p_cod_linea_aporte";
                        p_cod_linea_aporte.Value = pGrupoAporte.cod_linea_aporte;
                        p_cod_linea_aporte.Direction = ParameterDirection.Input;
                        //zp_cod_linea_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_aporte);


                        DbParameter p_porcentaje_dist = cmdTransaccionFactory.CreateParameter();
                        p_porcentaje_dist.ParameterName = "p_porcentaje_dist";
                        p_porcentaje_dist.Value = pGrupoAporte.porcentaje;
                        p_porcentaje_dist.Direction = ParameterDirection.Input;
                        //p_porcentaje_dist.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_porcentaje_dist);

                        DbParameter p_principal = cmdTransaccionFactory.CreateParameter();
                        p_principal.ParameterName = "p_principal";
                        p_principal.Value = pGrupoAporte.principal;
                        p_principal.Direction = ParameterDirection.Input;
                        //p_principal.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_principal);

                        DbParameter ptipo_distribucion = cmdTransaccionFactory.CreateParameter();
                        ptipo_distribucion.ParameterName = "ptipo_distribucion";
                        ptipo_distribucion.Value = pGrupoAporte.tipo_distribucion;
                        ptipo_distribucion.Direction = ParameterDirection.Input;
                        //ptipo_distribucion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_distribucion);

                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_GRUPOAPO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pGrupoAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GrupoAporteData", "ModificarGrupoAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla GrupoAporteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de GrupoAporteS</param>
        public void EliminarGrupoAporte(long pId, long pCod_linea, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idgrupo = cmdTransaccionFactory.CreateParameter();
                        p_idgrupo.ParameterName = "p_idgrupo";
                        p_idgrupo.Value = pId;
                        p_idgrupo.Direction = ParameterDirection.Input;
                        //p_idgrupo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_idgrupo);

                        DbParameter p_cod_linea = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea.ParameterName = "p_cod_linea";
                        p_cod_linea.Value = pCod_linea;
                        p_cod_linea.Direction = ParameterDirection.Input;
                        //p_cod_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea);

                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_GrupoAp_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GrupoAporteData", "EliminarGrupoAporte", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla GrupoAportes de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla GrupoAporteS</param>
        /// <returns>Entidad GrupoAporte consultado</returns>
        public GrupoLineaAporte ConsultarGrupoAporte(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            GrupoLineaAporte entidad = new GrupoLineaAporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM v_aportes WHERE COD_LINEA_APORTE = " + pId.ToString();
                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDGRUPO"] != DBNull.Value) entidad.idgrupo = Convert.ToInt64(resultado["IDGRUPO"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt64(resultado["COD_LINEA_APORTE"]);
                            if (resultado["PORCENTAJE_DISTRIBUCION"] != DBNull.Value) entidad.porcentaje = Convert.ToInt64(resultado["PORCENTAJE_DISTRIBUCION"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);

                        }
                        /*else
                        {
                        //    throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }*/
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GrupoAporteData", "ConsultarGrupoAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla GrupoAporteS dados unos filtros
        /// </summary>
        /// <param name="pGrupoAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de GrupoAporte obtenidos</returns>
        public List<GrupoLineaAporte> ListarGrupoAporte(GrupoLineaAporte pGrupoAporte, Usuario pUsuario)
        {

            DbDataReader resultado;
            List<GrupoLineaAporte> lstGrupoAporte = new List<GrupoLineaAporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select  * from GRUPO_APORTE " + ObtenerFiltro(pGrupoAporte) + " ORDER BY IDGRUPO ";
                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            GrupoLineaAporte entidad = new GrupoLineaAporte();
                            if (resultado["IDGRUPO"] != DBNull.Value) entidad.idgrupo = Convert.ToInt64(resultado["IDGRUPO"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt64(resultado["COD_LINEA_APORTE"]);
                            if (resultado["PORCENTAJE_DIST"] != DBNull.Value) entidad.porcentaje = Convert.ToInt64(resultado["PORCENTAJE_DIST"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            // if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToInt32(resultado["NOMBRE"]);// +"-" + Convert.ToString(resultado["NOMBRE"]);

                            lstGrupoAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGrupoAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GrupoAporteData", "ListarGrupoAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla GrupoAporteS dados unos filtros
        /// </summary>
        /// <param name="pGrupoAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de GrupoAporte obtenidos</returns>
        public List<GrupoLineaAporte> ListarGrupoAporteEdit(GrupoLineaAporte pGrupoAporte, Usuario pUsuario)
        {

            DbDataReader resultado;
            List<GrupoLineaAporte> lstGrupoAporte = new List<GrupoLineaAporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select  * from GRUPO_APORTE " + ObtenerFiltro(pGrupoAporte) + " ORDER BY IDGRUPO ";
                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            GrupoLineaAporte entidad = new GrupoLineaAporte();
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt64(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);

                            lstGrupoAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGrupoAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GrupoAporteData", ";", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla GrupoAporteS dados unos filtros
        /// </summary>
        /// <param name="pGrupoAporteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de GrupoAporte obtenidos</returns>
        public List<GrupoLineaAporte> ListarGrupoAporteDetalle(GrupoLineaAporte pGrupoAporte, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<GrupoLineaAporte> lstGrupoAporte = new List<GrupoLineaAporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"select * from v_grupo_aportes" + ObtenerFiltro(pGrupoAporte) + " ORDER BY IDGRUPO ";


                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            GrupoLineaAporte entidad = new GrupoLineaAporte();
                            if (resultado["IDGRUPO"] != DBNull.Value) entidad.idgrupo = Convert.ToInt64(resultado["IDGRUPO"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt64(resultado["COD_LINEA_APORTE"]);
                            if (resultado["PORCENTAJE_DIST"] != DBNull.Value) entidad.porcentaje = Convert.ToInt64(resultado["PORCENTAJE_DIST"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_DISTRIBUCION"] != DBNull.Value) entidad.tipo_distribucion = Convert.ToInt32(resultado["TIPO_DISTRIBUCION"]);
                            lstGrupoAporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGrupoAporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GrupoAporteData", "ListarGrupoAporte", ex);
                        return null;
                    }
                }
            }
        }


        public GrupoLineaAporte ConsultarGrupoAporteDetalle(GrupoLineaAporte pGrupoAporte, Usuario pUsuario)
        {
            DbDataReader resultado;
            GrupoLineaAporte entidad = new GrupoLineaAporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"select * from v_grupo_aportes " + ObtenerFiltro(pGrupoAporte) + " ORDER BY IDGRUPO ";


                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDGRUPO"] != DBNull.Value) entidad.idgrupo = Convert.ToInt64(resultado["IDGRUPO"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt64(resultado["COD_LINEA_APORTE"]);
                            if (resultado["PORCENTAJE_DIST"] != DBNull.Value) entidad.porcentaje = Convert.ToInt64(resultado["PORCENTAJE_DIST"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_DISTRIBUCION"] != DBNull.Value) entidad.tipo_distribucion = Convert.ToInt32(resultado["TIPO_DISTRIBUCION"]);
                            entidad.rpta = true;
                        }
                        else
                            entidad.rpta = false;
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GrupoAporteData", "ConsultarGrupoAporteDetalle", ex);
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
        public GrupoLineaAporte ConsultarMaxGrupoAporte(Usuario pUsuario)
        {
            DbDataReader resultado;
            GrupoLineaAporte entidad = new GrupoLineaAporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT max(idgrupo)  as idgrupo FROM grupo_lineaaporte";
                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["idgrupo"] != DBNull.Value) entidad.idgrupo = Convert.ToInt64(resultado["idgrupo"]);
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
                        BOExcepcion.Throw("AporteData", "ConsultarMaxGrupoAporte", ex);
                        return null;
                    }
                }
            }
        }


        public List<GrupoLineaAporte> ListaGrupoLineaAporte(string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<GrupoLineaAporte> lstLineas = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM grupo_lineaaporte " + pFiltro;
                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.HasRows)
                        {
                            lstLineas = new List<GrupoLineaAporte>();
                            GrupoLineaAporte entidad;
                            while (resultado.Read())
                            {
                                entidad = new GrupoLineaAporte();
                                if (resultado["idgrupo"] != DBNull.Value) entidad.idgrupo = Convert.ToInt64(resultado["idgrupo"]);
                                if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                                if (resultado["PORCENTAJE_DIST"] != DBNull.Value) entidad.porcentaje_distrib = Convert.ToDecimal(resultado["PORCENTAJE_DIST"]);
                                if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);
                                if (resultado["TIPO_DISTRIBUCION"] != DBNull.Value) entidad.tipo_distribucion = Convert.ToInt32(resultado["TIPO_DISTRIBUCION"]);
                                lstLineas.Add(entidad);
                            }
                        }
                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ConsultarMaxGrupoAporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica el valor de cuota a la tabla aportes y deja un registro en la tabla novedades_aporte
        /// </summary>
        /// <param name="pAporte">Entidad GAporte</param>S
        /// /// <param name="lst_Num_Apor">Entidad GAporte</param>S
        /// <returns>Entidad Aporte creada</returns>
        public GrupoLineaAporte CrearAportes(GrupoLineaAporte pAporte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_identificacion = cmdTransaccionFactory.CreateParameter();
                        p_identificacion.ParameterName = "p_identificacion";
                        p_identificacion.Value = pAporte.identificacion;
                        p_identificacion.Direction = ParameterDirection.Input;
                        p_identificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_identificacion);

                        DbParameter p_numero_aporte = cmdTransaccionFactory.CreateParameter();
                        p_numero_aporte.ParameterName = "p_numero_aporte";
                        p_numero_aporte.Value = pAporte.numero_aporte;
                        p_numero_aporte.Direction = ParameterDirection.Input;
                        p_numero_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numero_aporte);

                        DbParameter p_cuota = cmdTransaccionFactory.CreateParameter();
                        p_cuota.ParameterName = "p_cuota";
                        p_cuota.Value = pAporte.cuota;
                        p_cuota.Direction = ParameterDirection.Input;
                        p_cuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_cuota);

                        DbParameter p_cod_linea_aporte = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_aporte.ParameterName = "p_cod_linea_aporte";
                        p_cod_linea_aporte.Value = pAporte.cod_linea_aporte;
                        p_cod_linea_aporte.Direction = ParameterDirection.Input;
                        p_cod_linea_aporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_aporte);

                        DbParameter p_fechaAct = cmdTransaccionFactory.CreateParameter();
                        p_fechaAct.ParameterName = "p_fechaAct";
                        p_fechaAct.Value = pAporte.fechaAct;
                        p_fechaAct.Direction = ParameterDirection.Input;
                        p_fechaAct.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fechaAct);

                        DbParameter P_NUMERO_APORTER = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_APORTER.ParameterName = "P_NUMERO_APORTER";
                        P_NUMERO_APORTER.Value = pAporte.numero_aporteR;
                        P_NUMERO_APORTER.Direction = ParameterDirection.Output;
                        P_NUMERO_APORTER.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_APORTER);

                        connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_LIN_CARMASIVO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);               

                        
                        if (p_numero_aporte.Value != null)
                        {

                            if (P_NUMERO_APORTER.Value != DBNull.Value)
                            {                                
                                pAporte.numero_aporteR = Convert.ToInt64(P_NUMERO_APORTER.Value); 
                            }

                            pAporte.numero_aporte = Convert.ToInt64(p_numero_aporte.Value);                         

                        }

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



    }
}