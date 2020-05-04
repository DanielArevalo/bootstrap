using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Programado.Entities;

namespace Xpinn.Programado.Data
{
    public class LineasProgramadoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public LineasProgramadoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }



        public LineasProgramado CrearMod_LineasProgramado(LineasProgramado pLineas, Usuario vUsuario, Int32 opcion)
        {
            if (opcion == 1) //CREAR
                return CrearLineasProgramado(pLineas, vUsuario);
            else  //MODIFICAR
                return ModificarLineasProgramado(pLineas, vUsuario);
        }

        public LineasProgramado CrearLineasProgramado(LineasProgramado pLineasProgramado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_programado = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_programado.ParameterName = "p_cod_linea_programado";
                        pcod_linea_programado.Value = pLineasProgramado.cod_linea_programado;
                        pcod_linea_programado.Direction = ParameterDirection.Input;
                        pcod_linea_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_programado);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pLineasProgramado.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        //DbParameter ptipo_liquidacion = cmdTransaccionFactory.CreateParameter();
                        //ptipo_liquidacion.ParameterName = "p_tipo_liquidacion";
                        //ptipo_liquidacion.Value = pLineasProgramado.tipo_liquidacion;
                        //ptipo_liquidacion.Direction = ParameterDirection.Input;
                        //ptipo_liquidacion.DbType = DbType.Int32;
                        //cmdTransaccionFactory.Parameters.Add(ptipo_liquidacion);

                        DbParameter pcuota_minima = cmdTransaccionFactory.CreateParameter();
                        pcuota_minima.ParameterName = "p_cuota_minima";
                        pcuota_minima.Value = pLineasProgramado.cuota_minima;
                        pcuota_minima.Direction = ParameterDirection.Input;
                        pcuota_minima.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuota_minima);

                        DbParameter pplazo_minimo = cmdTransaccionFactory.CreateParameter();
                        pplazo_minimo.ParameterName = "p_plazo_minimo";
                        pplazo_minimo.Value = pLineasProgramado.plazo_minimo;
                        pplazo_minimo.Direction = ParameterDirection.Input;
                        pplazo_minimo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pplazo_minimo);

                        DbParameter pcalculo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcalculo_tasa.ParameterName = "p_calculo_tasa";
                        if (pLineasProgramado.calculo_tasa == null)
                            pcalculo_tasa.Value = DBNull.Value;
                        else
                            pcalculo_tasa.Value = pLineasProgramado.calculo_tasa;
                        pcalculo_tasa.Direction = ParameterDirection.Input;
                        pcalculo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcalculo_tasa);

                        DbParameter ptasa_interes = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes.ParameterName = "p_tasa_interes";
                        if (pLineasProgramado.tasa_interes == null)
                            ptasa_interes.Value = DBNull.Value;
                        else
                            ptasa_interes.Value = pLineasProgramado.tasa_interes;
                        ptasa_interes.Direction = ParameterDirection.Input;
                        ptasa_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes);

                        DbParameter pcod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        if (pLineasProgramado.cod_tipo_tasa == null)
                            pcod_tipo_tasa.Value = DBNull.Value;
                        else
                            pcod_tipo_tasa.Value = pLineasProgramado.cod_tipo_tasa;
                        pcod_tipo_tasa.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pLineasProgramado.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pLineasProgramado.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pLineasProgramado.desviacion == null)
                            pdesviacion.Value = DBNull.Value;
                        else
                            pdesviacion.Value = pLineasProgramado.desviacion;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter pretiro_parcial = cmdTransaccionFactory.CreateParameter();
                        pretiro_parcial.ParameterName = "p_retiro_parcial";
                        if (pLineasProgramado.retiro_parcial == null)
                            pretiro_parcial.Value = DBNull.Value;
                        else
                            pretiro_parcial.Value = pLineasProgramado.retiro_parcial;
                        pretiro_parcial.Direction = ParameterDirection.Input;
                        pretiro_parcial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pretiro_parcial);

                        DbParameter ppor_retiro_maximo = cmdTransaccionFactory.CreateParameter();
                        ppor_retiro_maximo.ParameterName = "p_por_retiro_maximo";
                        if (pLineasProgramado.por_retiro_maximo == null)
                            ppor_retiro_maximo.Value = DBNull.Value;
                        else
                            ppor_retiro_maximo.Value = pLineasProgramado.por_retiro_maximo;
                        ppor_retiro_maximo.Direction = ParameterDirection.Input;
                        ppor_retiro_maximo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ppor_retiro_maximo);

                        DbParameter psaldo_minimo = cmdTransaccionFactory.CreateParameter();
                        psaldo_minimo.ParameterName = "p_saldo_minimo";
                        if (pLineasProgramado.saldo_minimo == null)
                            psaldo_minimo.Value = DBNull.Value;
                        else
                            psaldo_minimo.Value = pLineasProgramado.saldo_minimo;
                        psaldo_minimo.Direction = ParameterDirection.Input;
                        psaldo_minimo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_minimo);

                        DbParameter pcruza = cmdTransaccionFactory.CreateParameter();
                        pcruza.ParameterName = "p_cruza";
                        if (pLineasProgramado.cruza == null)
                            pcruza.Value = DBNull.Value;
                        else
                            pcruza.Value = pLineasProgramado.cruza;
                        pcruza.Direction = ParameterDirection.Input;
                        pcruza.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcruza);

                        DbParameter pporcentaje_cruce = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_cruce.ParameterName = "p_porcentaje_cruce";
                        if (pLineasProgramado.porcentaje_cruce == null)
                            pporcentaje_cruce.Value = DBNull.Value;
                        else
                            pporcentaje_cruce.Value = pLineasProgramado.porcentaje_cruce;
                        pporcentaje_cruce.Direction = ParameterDirection.Input;
                        pporcentaje_cruce.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_cruce);

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "p_retencion";
                        if (pLineasProgramado.retencion == null)
                            pretencion.Value = DBNull.Value;
                        else
                            pretencion.Value = pLineasProgramado.retencion;
                        pretencion.Direction = ParameterDirection.Input;
                        pretencion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pretencion);

                        DbParameter pcuota_nomina = cmdTransaccionFactory.CreateParameter();
                        pcuota_nomina.ParameterName = "p_cuota_nomina";
                        if (pLineasProgramado.cuota_nomina == null)
                            pcuota_nomina.Value = DBNull.Value;
                        else
                            pcuota_nomina.Value = pLineasProgramado.cuota_nomina;
                        pcuota_nomina.Direction = ParameterDirection.Input;
                        pcuota_nomina.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuota_nomina);

                        DbParameter ppor_retiro_plazo = cmdTransaccionFactory.CreateParameter();
                        ppor_retiro_plazo.ParameterName = "p_por_retiro_plazo";
                        if (pLineasProgramado.por_retiro_plazo == null)
                            ppor_retiro_plazo.Value = DBNull.Value;
                        else
                            ppor_retiro_plazo.Value = pLineasProgramado.por_retiro_plazo;
                        ppor_retiro_plazo.Direction = ParameterDirection.Input;
                        ppor_retiro_plazo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppor_retiro_plazo);

                        DbParameter popcion_saldo = cmdTransaccionFactory.CreateParameter();
                        popcion_saldo.ParameterName = "p_opcion_saldo";
                        if (pLineasProgramado.opcion_saldo == null)
                            popcion_saldo.Value = DBNull.Value;
                        else
                            popcion_saldo.Value = pLineasProgramado.opcion_saldo;
                        popcion_saldo.Direction = ParameterDirection.Input;
                        popcion_saldo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(popcion_saldo);

                        DbParameter ppor_retiro_minimo = cmdTransaccionFactory.CreateParameter();
                        ppor_retiro_minimo.ParameterName = "p_por_retiro_minimo";
                        if (pLineasProgramado.por_retiro_minimo == null)
                            ppor_retiro_minimo.Value = DBNull.Value;
                        else
                            ppor_retiro_minimo.Value = pLineasProgramado.por_retiro_minimo;
                        ppor_retiro_minimo.Direction = ParameterDirection.Input;
                        ppor_retiro_minimo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ppor_retiro_minimo);

                        DbParameter pvalor_maximo_retiro = cmdTransaccionFactory.CreateParameter();
                        pvalor_maximo_retiro.ParameterName = "p_valor_maximo_retiro";
                        if (pLineasProgramado.valor_maximo_retiro == null)
                            pvalor_maximo_retiro.Value = DBNull.Value;
                        else
                            pvalor_maximo_retiro.Value = pLineasProgramado.valor_maximo_retiro;
                        pvalor_maximo_retiro.Direction = ParameterDirection.Input;
                        pvalor_maximo_retiro.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_maximo_retiro);

                        DbParameter ppor_int_dism = cmdTransaccionFactory.CreateParameter();
                        ppor_int_dism.ParameterName = "p_por_int_dism";
                        if (pLineasProgramado.por_int_dism == null)
                            ppor_int_dism.Value = DBNull.Value;
                        else
                            ppor_int_dism.Value = pLineasProgramado.por_int_dism;
                        ppor_int_dism.Direction = ParameterDirection.Input;
                        ppor_int_dism.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ppor_int_dism);

                        DbParameter pporpla_ret_t = cmdTransaccionFactory.CreateParameter();
                        pporpla_ret_t.ParameterName = "p_porpla_ret_t";
                        if (pLineasProgramado.porpla_ret_t == null)
                            pporpla_ret_t.Value = DBNull.Value;
                        else
                            pporpla_ret_t.Value = pLineasProgramado.porpla_ret_t;
                        pporpla_ret_t.Direction = ParameterDirection.Input;
                        pporpla_ret_t.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pporpla_ret_t);

                        DbParameter ppormont_ret_t = cmdTransaccionFactory.CreateParameter();
                        ppormont_ret_t.ParameterName = "p_pormont_ret_t";
                        if (pLineasProgramado.pormont_ret_t == null)
                            ppormont_ret_t.Value = DBNull.Value;
                        else
                            ppormont_ret_t.Value = pLineasProgramado.pormont_ret_t;
                        ppormont_ret_t.Direction = ParameterDirection.Input;
                        ppormont_ret_t.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppormont_ret_t);

                        DbParameter pdias_gracia = cmdTransaccionFactory.CreateParameter();
                        pdias_gracia.ParameterName = "p_dias_gracia";
                        if (pLineasProgramado.dias_gracia == null)
                            pdias_gracia.Value = DBNull.Value;
                        else
                            pdias_gracia.Value = pLineasProgramado.dias_gracia;
                        pdias_gracia.Direction = ParameterDirection.Input;
                        pdias_gracia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_gracia);

                        DbParameter pprioridad = cmdTransaccionFactory.CreateParameter();
                        pprioridad.ParameterName = "p_prioridad";
                        if (pLineasProgramado.prioridad == null)
                            pprioridad.Value = DBNull.Value;
                        else
                            pprioridad.Value = pLineasProgramado.prioridad;
                        pprioridad.Direction = ParameterDirection.Input;
                        pprioridad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprioridad);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pLineasProgramado.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter paplica_retencion = cmdTransaccionFactory.CreateParameter();
                        paplica_retencion.ParameterName = "p_aplica_retencion";
                        if (pLineasProgramado.aplica_retencion == null)
                            paplica_retencion.Value = DBNull.Value;
                        else
                            paplica_retencion.Value = pLineasProgramado.aplica_retencion;
                        paplica_retencion.Direction = ParameterDirection.Input;
                        paplica_retencion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(paplica_retencion);   

                        DbParameter pinteres_por_cuenta = cmdTransaccionFactory.CreateParameter();
                        pinteres_por_cuenta.ParameterName = "p_interes_por_cuenta";
                        if (pLineasProgramado.interes_por_cuenta == null)
                            pinteres_por_cuenta.Value = System.DBNull.Value;
                        else
                            pinteres_por_cuenta.Value = pLineasProgramado.interes_por_cuenta;
                        pinteres_por_cuenta.Direction = ParameterDirection.Input;
                        pinteres_por_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pinteres_por_cuenta);

                        DbParameter p_tipo_saldo_int = cmdTransaccionFactory.CreateParameter();
                        p_tipo_saldo_int.ParameterName = "p_tipo_saldo_int";
                        p_tipo_saldo_int.Value = pLineasProgramado.tipo_saldo_int;
                        p_tipo_saldo_int.Direction = ParameterDirection.Input;
                        p_tipo_saldo_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_saldo_int);

                        DbParameter p_periodiidad_int = cmdTransaccionFactory.CreateParameter();
                        p_periodiidad_int.ParameterName = "p_periodicidad_int";
                        p_periodiidad_int.Value = pLineasProgramado.periodicidad_int;
                        p_periodiidad_int.Direction = ParameterDirection.Input;
                        p_periodiidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_periodiidad_int);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        pcod_moneda.Value = pLineasProgramado.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);


                        DbParameter p_maneja_cuota_extra = cmdTransaccionFactory.CreateParameter();
                        p_maneja_cuota_extra.ParameterName = "P_MANEJA_CUOTA_EXTRA";
                        p_maneja_cuota_extra.Value = pLineasProgramado.maneja_cuota_extra;
                        p_maneja_cuota_extra.Direction = ParameterDirection.Input;
                        p_maneja_cuota_extra.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_maneja_cuota_extra);



                        DbParameter p_cuota_ext_min = cmdTransaccionFactory.CreateParameter();
                        p_cuota_ext_min.ParameterName = "P_CUOTA_MIN_EXTRA";
                        p_cuota_ext_min.Value = pLineasProgramado.cuota_extra_min;
                        p_cuota_ext_min.Direction = ParameterDirection.Input;
                        p_cuota_ext_min.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_cuota_ext_min);




                        DbParameter p_cuota_ext_max = cmdTransaccionFactory.CreateParameter();
                        p_cuota_ext_max.ParameterName = "P_CUOTA_MAX_EXTRA";
                        p_cuota_ext_max.Value = pLineasProgramado.cuota_extra_max;
                        p_cuota_ext_max.Direction = ParameterDirection.Input;
                        p_cuota_ext_max.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_cuota_ext_max);



                        DbParameter P_INTERES_RENOVACION = cmdTransaccionFactory.CreateParameter();
                        P_INTERES_RENOVACION.ParameterName = "P_INTERES_RENOVACION";
                        P_INTERES_RENOVACION.Value = pLineasProgramado.interes_renovacion;
                        P_INTERES_RENOVACION.Direction = ParameterDirection.Input;
                        P_INTERES_RENOVACION.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_INTERES_RENOVACION);




                        DbParameter p_calculo_tasa_ren = cmdTransaccionFactory.CreateParameter();
                        p_calculo_tasa_ren.ParameterName = "p_calculo_tasa_ren";
                        if (pLineasProgramado.calculo_tasa_ren == null)
                            p_calculo_tasa_ren.Value = DBNull.Value;
                        else
                            p_calculo_tasa_ren.Value = pLineasProgramado.calculo_tasa_ren;
                        p_calculo_tasa_ren.Direction = ParameterDirection.Input;
                        p_calculo_tasa_ren.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_calculo_tasa_ren);

                        DbParameter ptasa_interes_ren = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes_ren.ParameterName = "ptasa_interes_ren";
                        if (pLineasProgramado.tasa_interes_ren == null)
                            ptasa_interes_ren.Value = DBNull.Value;
                        else
                            ptasa_interes_ren.Value = pLineasProgramado.tasa_interes_ren;
                        ptasa_interes_ren.Direction = ParameterDirection.Input;
                        ptasa_interes_ren.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes_ren);

                        DbParameter pcod_tipo_tasa_ren = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa_ren.ParameterName = "pcod_tipo_tasa_ren";
                        if (pLineasProgramado.cod_tipo_tasa_ren == null)
                            pcod_tipo_tasa_ren.Value = DBNull.Value;
                        else
                            pcod_tipo_tasa_ren.Value = pLineasProgramado.cod_tipo_tasa_ren;
                        pcod_tipo_tasa_ren.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa_ren.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa_ren);

                        DbParameter ptipo_historico_ren = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico_ren.ParameterName = "ptipo_historico_ren";
                        if (pLineasProgramado.tipo_historico_ren == null)
                            ptipo_historico_ren.Value = DBNull.Value;
                        else
                            ptipo_historico_ren.Value = pLineasProgramado.tipo_historico_ren;
                        ptipo_historico_ren.Direction = ParameterDirection.Input;
                        ptipo_historico_ren.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico_ren);

                        DbParameter pdesviacion_ren = cmdTransaccionFactory.CreateParameter();
                        pdesviacion_ren.ParameterName = "pdesviacion_ren";
                        if (pLineasProgramado.desviacion_ren == null)
                            pdesviacion_ren.Value = DBNull.Value;
                        else
                            pdesviacion_ren.Value = pLineasProgramado.desviacion_ren;
                        pdesviacion_ren.Direction = ParameterDirection.Input;
                        pdesviacion_ren.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion_ren);






                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LINEAPROGR_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLineasProgramado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasProgramadoData", "CrearLineasProgramado", ex);
                        return null;
                    }
                }
            }
        }

        public LineasProgramado ModificarLineasProgramado(LineasProgramado pLineasProgramado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_programado = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_programado.ParameterName = "p_cod_linea_programado";
                        pcod_linea_programado.Value = pLineasProgramado.cod_linea_programado;
                        pcod_linea_programado.Direction = ParameterDirection.Input;
                        pcod_linea_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_programado);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pLineasProgramado.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        //DbParameter ptipo_liquidacion = cmdTransaccionFactory.CreateParameter();
                        //ptipo_liquidacion.ParameterName = "p_tipo_liquidacion";
                        //ptipo_liquidacion.Value = pLineasProgramado.tipo_liquidacion;
                        //ptipo_liquidacion.Direction = ParameterDirection.Input;
                        //ptipo_liquidacion.DbType = DbType.Int32;
                        //cmdTransaccionFactory.Parameters.Add(ptipo_liquidacion);

                        DbParameter pcuota_minima = cmdTransaccionFactory.CreateParameter();
                        pcuota_minima.ParameterName = "p_cuota_minima";
                        pcuota_minima.Value = pLineasProgramado.cuota_minima;
                        pcuota_minima.Direction = ParameterDirection.Input;
                        pcuota_minima.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuota_minima);

                        DbParameter pplazo_minimo = cmdTransaccionFactory.CreateParameter();
                        pplazo_minimo.ParameterName = "p_plazo_minimo";
                        pplazo_minimo.Value = pLineasProgramado.plazo_minimo;
                        pplazo_minimo.Direction = ParameterDirection.Input;
                        pplazo_minimo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pplazo_minimo);

                        DbParameter pcalculo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcalculo_tasa.ParameterName = "p_calculo_tasa";
                        if (pLineasProgramado.calculo_tasa == null)
                            pcalculo_tasa.Value = DBNull.Value;
                        else
                            pcalculo_tasa.Value = pLineasProgramado.calculo_tasa;
                        pcalculo_tasa.Direction = ParameterDirection.Input;
                        pcalculo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcalculo_tasa);

                        DbParameter ptasa_interes = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes.ParameterName = "p_tasa_interes";
                        if (pLineasProgramado.tasa_interes == null)
                            ptasa_interes.Value = DBNull.Value;
                        else
                            ptasa_interes.Value = pLineasProgramado.tasa_interes;
                        ptasa_interes.Direction = ParameterDirection.Input;
                        ptasa_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes);

                        DbParameter pcod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        if (pLineasProgramado.cod_tipo_tasa == null)
                            pcod_tipo_tasa.Value = DBNull.Value;
                        else
                            pcod_tipo_tasa.Value = pLineasProgramado.cod_tipo_tasa;
                        pcod_tipo_tasa.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pLineasProgramado.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pLineasProgramado.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pLineasProgramado.desviacion == null)
                            pdesviacion.Value = DBNull.Value;
                        else
                            pdesviacion.Value = pLineasProgramado.desviacion;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter pretiro_parcial = cmdTransaccionFactory.CreateParameter();
                        pretiro_parcial.ParameterName = "p_retiro_parcial";
                        if (pLineasProgramado.retiro_parcial == null)
                            pretiro_parcial.Value = DBNull.Value;
                        else
                            pretiro_parcial.Value = pLineasProgramado.retiro_parcial;
                        pretiro_parcial.Direction = ParameterDirection.Input;
                        pretiro_parcial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pretiro_parcial);

                        DbParameter ppor_retiro_maximo = cmdTransaccionFactory.CreateParameter();
                        ppor_retiro_maximo.ParameterName = "p_por_retiro_maximo";
                        if (pLineasProgramado.por_retiro_maximo == null)
                            ppor_retiro_maximo.Value = DBNull.Value;
                        else
                            ppor_retiro_maximo.Value = pLineasProgramado.por_retiro_maximo;
                        ppor_retiro_maximo.Direction = ParameterDirection.Input;
                        ppor_retiro_maximo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ppor_retiro_maximo);

                        DbParameter psaldo_minimo = cmdTransaccionFactory.CreateParameter();
                        psaldo_minimo.ParameterName = "p_saldo_minimo";
                        if (pLineasProgramado.saldo_minimo == null)
                            psaldo_minimo.Value = DBNull.Value;
                        else
                            psaldo_minimo.Value = pLineasProgramado.saldo_minimo;
                        psaldo_minimo.Direction = ParameterDirection.Input;
                        psaldo_minimo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_minimo);

                        DbParameter pcruza = cmdTransaccionFactory.CreateParameter();
                        pcruza.ParameterName = "p_cruza";
                        if (pLineasProgramado.cruza == null)
                            pcruza.Value = DBNull.Value;
                        else
                            pcruza.Value = pLineasProgramado.cruza;
                        pcruza.Direction = ParameterDirection.Input;
                        pcruza.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcruza);

                        DbParameter pporcentaje_cruce = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_cruce.ParameterName = "p_porcentaje_cruce";
                        if (pLineasProgramado.porcentaje_cruce == null)
                            pporcentaje_cruce.Value = DBNull.Value;
                        else
                            pporcentaje_cruce.Value = pLineasProgramado.porcentaje_cruce;
                        pporcentaje_cruce.Direction = ParameterDirection.Input;
                        pporcentaje_cruce.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_cruce);

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "p_retencion";
                        if (pLineasProgramado.retencion == null)
                            pretencion.Value = DBNull.Value;
                        else
                            pretencion.Value = pLineasProgramado.retencion;
                        pretencion.Direction = ParameterDirection.Input;
                        pretencion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pretencion);

                        DbParameter pcuota_nomina = cmdTransaccionFactory.CreateParameter();
                        pcuota_nomina.ParameterName = "p_cuota_nomina";
                        if (pLineasProgramado.cuota_nomina == null)
                            pcuota_nomina.Value = DBNull.Value;
                        else
                            pcuota_nomina.Value = pLineasProgramado.cuota_nomina;
                        pcuota_nomina.Direction = ParameterDirection.Input;
                        pcuota_nomina.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuota_nomina);

                        DbParameter ppor_retiro_plazo = cmdTransaccionFactory.CreateParameter();
                        ppor_retiro_plazo.ParameterName = "p_por_retiro_plazo";
                        if (pLineasProgramado.por_retiro_plazo == null)
                            ppor_retiro_plazo.Value = DBNull.Value;
                        else
                            ppor_retiro_plazo.Value = pLineasProgramado.por_retiro_plazo;
                        ppor_retiro_plazo.Direction = ParameterDirection.Input;
                        ppor_retiro_plazo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppor_retiro_plazo);

                        DbParameter popcion_saldo = cmdTransaccionFactory.CreateParameter();
                        popcion_saldo.ParameterName = "p_opcion_saldo";
                        if (pLineasProgramado.opcion_saldo == null)
                            popcion_saldo.Value = DBNull.Value;
                        else
                            popcion_saldo.Value = pLineasProgramado.opcion_saldo;
                        popcion_saldo.Direction = ParameterDirection.Input;
                        popcion_saldo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(popcion_saldo);

                        DbParameter ppor_retiro_minimo = cmdTransaccionFactory.CreateParameter();
                        ppor_retiro_minimo.ParameterName = "p_por_retiro_minimo";
                        if (pLineasProgramado.por_retiro_minimo == null)
                            ppor_retiro_minimo.Value = DBNull.Value;
                        else
                            ppor_retiro_minimo.Value = pLineasProgramado.por_retiro_minimo;
                        ppor_retiro_minimo.Direction = ParameterDirection.Input;
                        ppor_retiro_minimo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ppor_retiro_minimo);

                        DbParameter pvalor_maximo_retiro = cmdTransaccionFactory.CreateParameter();
                        pvalor_maximo_retiro.ParameterName = "p_valor_maximo_retiro";
                        if (pLineasProgramado.valor_maximo_retiro == null)
                            pvalor_maximo_retiro.Value = DBNull.Value;
                        else
                            pvalor_maximo_retiro.Value = pLineasProgramado.valor_maximo_retiro;
                        pvalor_maximo_retiro.Direction = ParameterDirection.Input;
                        pvalor_maximo_retiro.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_maximo_retiro);

                        DbParameter ppor_int_dism = cmdTransaccionFactory.CreateParameter();
                        ppor_int_dism.ParameterName = "p_por_int_dism";
                        if (pLineasProgramado.por_int_dism == null)
                            ppor_int_dism.Value = DBNull.Value;
                        else
                            ppor_int_dism.Value = pLineasProgramado.por_int_dism;
                        ppor_int_dism.Direction = ParameterDirection.Input;
                        ppor_int_dism.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ppor_int_dism);

                        DbParameter pporpla_ret_t = cmdTransaccionFactory.CreateParameter();
                        pporpla_ret_t.ParameterName = "p_porpla_ret_t";
                        if (pLineasProgramado.porpla_ret_t == null)
                            pporpla_ret_t.Value = DBNull.Value;
                        else
                            pporpla_ret_t.Value = pLineasProgramado.porpla_ret_t;
                        pporpla_ret_t.Direction = ParameterDirection.Input;
                        pporpla_ret_t.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pporpla_ret_t);

                        DbParameter ppormont_ret_t = cmdTransaccionFactory.CreateParameter();
                        ppormont_ret_t.ParameterName = "p_pormont_ret_t";
                        if (pLineasProgramado.pormont_ret_t == null)
                            ppormont_ret_t.Value = DBNull.Value;
                        else
                            ppormont_ret_t.Value = pLineasProgramado.pormont_ret_t;
                        ppormont_ret_t.Direction = ParameterDirection.Input;
                        ppormont_ret_t.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppormont_ret_t);

                        DbParameter pdias_gracia = cmdTransaccionFactory.CreateParameter();
                        pdias_gracia.ParameterName = "p_dias_gracia";
                        if (pLineasProgramado.dias_gracia == null)
                            pdias_gracia.Value = DBNull.Value;
                        else
                            pdias_gracia.Value = pLineasProgramado.dias_gracia;
                        pdias_gracia.Direction = ParameterDirection.Input;
                        pdias_gracia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_gracia);

                        DbParameter pprioridad = cmdTransaccionFactory.CreateParameter();
                        pprioridad.ParameterName = "p_prioridad";
                        if (pLineasProgramado.prioridad == null)
                            pprioridad.Value = DBNull.Value;
                        else
                            pprioridad.Value = pLineasProgramado.prioridad;
                        pprioridad.Direction = ParameterDirection.Input;
                        pprioridad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprioridad);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pLineasProgramado.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter paplica_retencion = cmdTransaccionFactory.CreateParameter();
                        paplica_retencion.ParameterName = "p_aplica_retencion";
                        if (pLineasProgramado.aplica_retencion == null)
                            paplica_retencion.Value = DBNull.Value;
                        else
                            paplica_retencion.Value = pLineasProgramado.aplica_retencion;
                        paplica_retencion.Direction = ParameterDirection.Input;
                        paplica_retencion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(paplica_retencion);


                        DbParameter pinteres_por_cuenta = cmdTransaccionFactory.CreateParameter();
                        pinteres_por_cuenta.ParameterName = "p_interes_por_cuenta";
                        if (pLineasProgramado.interes_por_cuenta == null)
                            pinteres_por_cuenta.Value = System.DBNull.Value;
                        else
                            pinteres_por_cuenta.Value = pLineasProgramado.interes_por_cuenta;
                        pinteres_por_cuenta.Direction = ParameterDirection.Input;
                        pinteres_por_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pinteres_por_cuenta);
                        
                        DbParameter p_tipo_saldo_int = cmdTransaccionFactory.CreateParameter();
                        p_tipo_saldo_int.ParameterName = "p_tipo_saldo_int";
                        p_tipo_saldo_int.Value = pLineasProgramado.tipo_saldo_int;
                        p_tipo_saldo_int.Direction = ParameterDirection.Input;
                        p_tipo_saldo_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_saldo_int);

                        DbParameter p_periodiidad_int = cmdTransaccionFactory.CreateParameter();
                        p_periodiidad_int.ParameterName = "p_periodicidad_int";
                        p_periodiidad_int.Value = pLineasProgramado.periodicidad_int;
                        p_periodiidad_int.Direction = ParameterDirection.Input;
                        p_periodiidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_periodiidad_int);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        pcod_moneda.Value = pLineasProgramado.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);


                        DbParameter p_maneja_cuota_extra = cmdTransaccionFactory.CreateParameter();
                        p_maneja_cuota_extra.ParameterName = "P_MANEJA_CUOTA_EXTRA";
                        p_maneja_cuota_extra.Value = pLineasProgramado.maneja_cuota_extra;
                        p_maneja_cuota_extra.Direction = ParameterDirection.Input;
                        p_maneja_cuota_extra.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_maneja_cuota_extra);



                        DbParameter p_cuota_ext_min = cmdTransaccionFactory.CreateParameter();
                        p_cuota_ext_min.ParameterName = "P_CUOTA_MIN_EXTRA";
                        p_cuota_ext_min.Value = pLineasProgramado.cuota_extra_min;
                        p_cuota_ext_min.Direction = ParameterDirection.Input;
                        p_cuota_ext_min.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_cuota_ext_min);




                        DbParameter p_cuota_ext_max = cmdTransaccionFactory.CreateParameter();
                        p_cuota_ext_max.ParameterName = "P_CUOTA_MAX_EXTRA";
                        p_cuota_ext_max.Value = pLineasProgramado.cuota_extra_max;
                        p_cuota_ext_max.Direction = ParameterDirection.Input;
                        p_cuota_ext_max.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_cuota_ext_max);






                        DbParameter P_INTERES_RENOVACION = cmdTransaccionFactory.CreateParameter();
                        P_INTERES_RENOVACION.ParameterName = "P_INTERES_RENOVACION";
                        P_INTERES_RENOVACION.Value = pLineasProgramado.interes_renovacion;
                        P_INTERES_RENOVACION.Direction = ParameterDirection.Input;
                        P_INTERES_RENOVACION.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_INTERES_RENOVACION);




                        DbParameter p_calculo_tasa_ren = cmdTransaccionFactory.CreateParameter();
                        p_calculo_tasa_ren.ParameterName = "p_calculo_tasa_ren";
                        if (pLineasProgramado.calculo_tasa_ren == null)
                            p_calculo_tasa_ren.Value = DBNull.Value;
                        else
                            p_calculo_tasa_ren.Value = pLineasProgramado.calculo_tasa_ren;
                        p_calculo_tasa_ren.Direction = ParameterDirection.Input;
                        p_calculo_tasa_ren.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_calculo_tasa_ren);

                        DbParameter ptasa_interes_ren = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes_ren.ParameterName = "ptasa_interes_ren";
                        if (pLineasProgramado.tasa_interes_ren == null)
                            ptasa_interes_ren.Value = DBNull.Value;
                        else
                            ptasa_interes_ren.Value = pLineasProgramado.tasa_interes_ren;
                        ptasa_interes_ren.Direction = ParameterDirection.Input;
                        ptasa_interes_ren.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes_ren);

                        DbParameter pcod_tipo_tasa_ren = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa_ren.ParameterName = "pcod_tipo_tasa_ren";
                        if (pLineasProgramado.cod_tipo_tasa_ren == null)
                            pcod_tipo_tasa_ren.Value = DBNull.Value;
                        else
                            pcod_tipo_tasa_ren.Value = pLineasProgramado.cod_tipo_tasa_ren;
                        pcod_tipo_tasa_ren.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa_ren.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa_ren);

                        DbParameter ptipo_historico_ren = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico_ren.ParameterName = "ptipo_historico_ren";
                        if (pLineasProgramado.tipo_historico_ren == null)
                            ptipo_historico_ren.Value = DBNull.Value;
                        else
                            ptipo_historico_ren.Value = pLineasProgramado.tipo_historico_ren;
                        ptipo_historico_ren.Direction = ParameterDirection.Input;
                        ptipo_historico_ren.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico_ren);

                        DbParameter pdesviacion_ren = cmdTransaccionFactory.CreateParameter();
                        pdesviacion_ren.ParameterName = "pdesviacion_ren";
                        if (pLineasProgramado.desviacion_ren == null)
                            pdesviacion_ren.Value = DBNull.Value;
                        else
                            pdesviacion_ren.Value = pLineasProgramado.desviacion_ren;
                        pdesviacion_ren.Direction = ParameterDirection.Input;
                        pdesviacion_ren.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion_ren);





                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LINEAPROGR_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLineasProgramado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasProgramadoData", "ModificarLineasProgramado", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarLineaProgramado(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_cod_linea_programado = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_programado.ParameterName = "p_cod_linea_programado";
                        p_cod_linea_programado.Value = pId.ToString();
                        p_cod_linea_programado.Direction = ParameterDirection.Input;
                        p_cod_linea_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_programado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LINEAPRO_ELI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaProgramadoData", "EliminarLineaProgramado", ex);
                    }
                }
            }
        }


        public List<LineasProgramado> ListarLineasProgramado(String pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LineasProgramado> lstConsulta = new List<LineasProgramado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT L.*, CASE L.estado WHEN 0 THEN 'INACTIVO' WHEN 1 THEN 'ACTIVO' END AS nomestado, r.minimo, r.maximo
                                        FROM lineaprogramado L 
                                        LEFT JOIN LineaProgramado_Requisito R ON l.cod_linea_programado = r.cod_linea_programado AND r.tipo_tope = 2 
                                        WHERE 1=1 " + pFiltro.ToString() + @" ORDER BY L.cod_linea_programado ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineasProgramado entidad = new LineasProgramado();
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);                           
                            if (resultado["CUOTA_MINIMA"] != DBNull.Value) entidad.cuota_minima = Convert.ToDecimal(resultado["CUOTA_MINIMA"]);                            
                            if (resultado["CALCULO_TASA"] != DBNull.Value) entidad.calculo_tasa = Convert.ToInt32(resultado["CALCULO_TASA"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["RETIRO_PARCIAL"] != DBNull.Value) entidad.retiro_parcial = Convert.ToInt32(resultado["RETIRO_PARCIAL"]);
                            if (resultado["POR_RETIRO_MAXIMO"] != DBNull.Value) entidad.por_retiro_maximo = Convert.ToDecimal(resultado["POR_RETIRO_MAXIMO"]);
                            if (resultado["SALDO_MINIMO"] != DBNull.Value) entidad.saldo_minimo = Convert.ToDecimal(resultado["SALDO_MINIMO"]);
                            if (resultado["CRUZA"] != DBNull.Value) entidad.cruza = Convert.ToInt32(resultado["CRUZA"]);
                            if (resultado["PORCENTAJE_CRUCE"] != DBNull.Value) entidad.porcentaje_cruce = Convert.ToDecimal(resultado["PORCENTAJE_CRUCE"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToDecimal(resultado["RETENCION"]);
                            if (resultado["CUOTA_NOMINA"] != DBNull.Value) entidad.cuota_nomina = Convert.ToDecimal(resultado["CUOTA_NOMINA"]);
                            if (resultado["POR_RETIRO_PLAZO"] != DBNull.Value) entidad.por_retiro_plazo = Convert.ToInt32(resultado["POR_RETIRO_PLAZO"]);
                            if (resultado["OPCION_SALDO"] != DBNull.Value) entidad.opcion_saldo = Convert.ToString(resultado["OPCION_SALDO"]);
                            if (resultado["POR_RETIRO_MINIMO"] != DBNull.Value) entidad.por_retiro_minimo = Convert.ToDecimal(resultado["POR_RETIRO_MINIMO"]);
                            if (resultado["VALOR_MAXIMO_RETIRO"] != DBNull.Value) entidad.valor_maximo_retiro = Convert.ToDecimal(resultado["VALOR_MAXIMO_RETIRO"]);
                            if (resultado["POR_INT_DISM"] != DBNull.Value) entidad.por_int_dism = Convert.ToDecimal(resultado["POR_INT_DISM"]);
                            if (resultado["PORPLA_RET_T"] != DBNull.Value) entidad.porpla_ret_t = Convert.ToInt32(resultado["PORPLA_RET_T"]);
                            if (resultado["PORMONT_RET_T"] != DBNull.Value) entidad.pormont_ret_t = Convert.ToInt32(resultado["PORMONT_RET_T"]);
                            if (resultado["DIAS_GRACIA"] != DBNull.Value) entidad.dias_gracia = Convert.ToInt32(resultado["DIAS_GRACIA"]);
                            if (resultado["PRIORIDAD"] != DBNull.Value) entidad.prioridad = Convert.ToInt32(resultado["PRIORIDAD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["NOMESTADO"]);                       
                            if (resultado["INTERES_POR_CUENTA"] != DBNull.Value) entidad.interes_por_cuenta = Convert.ToInt32(resultado["INTERES_POR_CUENTA"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.plazo_minimo = Convert.ToInt32(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.plazo_maximo = Convert.ToInt32(resultado["MAXIMO"]);
                            //manejo de cuotas extras 
                            if (resultado["MANEJA_CUOTA_EXTRA"] != DBNull.Value) entidad.maneja_cuota_extra = Convert.ToInt32(resultado["MANEJA_CUOTA_EXTRA"]);
                            if (resultado["CUOTA_MIN_EXTRA"] != DBNull.Value) entidad.cuota_extra_min = Convert.ToDecimal(resultado["CUOTA_MIN_EXTRA"]);
                            if (resultado["CUOTA_MAX_EXTRA"] != DBNull.Value) entidad.cuota_extra_max = Convert.ToDecimal(resultado["CUOTA_MAX_EXTRA"]);

                            //Tasa Renovacion

                            if (resultado["INTERES_RENOVACION"] != DBNull.Value) entidad.interes_renovacion = Convert.ToInt32(resultado["INTERES_RENOVACION"]);
                            if (resultado["CALCULO_TASA_REN"] != DBNull.Value) entidad.calculo_tasa_ren = Convert.ToInt32(resultado["CALCULO_TASA_REN"]);
                            if (resultado["TASA_INTERES_REN"] != DBNull.Value) entidad.tasa_interes_ren = Convert.ToDecimal(resultado["TASA_INTERES_REN"]);
                            if (resultado["COD_TIPO_TASA_REN"] != DBNull.Value) entidad.cod_tipo_tasa_ren = Convert.ToInt32(resultado["COD_TIPO_TASA_REN"]);
                            if (resultado["TIPO_HISTORICO_REN"] != DBNull.Value) entidad.tipo_historico_ren = Convert.ToInt32(resultado["TIPO_HISTORICO_REN"]);
                            if (resultado["DESVIACION_REN"] != DBNull.Value) entidad.desviacion_ren = Convert.ToDecimal(resultado["DESVIACION_REN"]);

                            lstConsulta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasProgramadoData", "ListarLineasProgramado", ex);
                        return null;
                    }
                }
            }
        }


        public LineasProgramado ConsultarLineasProgramado(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LineasProgramado entidad = new LineasProgramado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM lineaprogramado WHERE COD_LINEA_PROGRAMADO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["CUOTA_MINIMA"] != DBNull.Value) entidad.cuota_minima = Convert.ToDecimal(resultado["CUOTA_MINIMA"]);
                            if (resultado["PLAZO_MINIMO"] != DBNull.Value) entidad.plazo_minimo = Convert.ToInt32(resultado["PLAZO_MINIMO"]);
                            if (resultado["CALCULO_TASA"] != DBNull.Value) entidad.calculo_tasa = Convert.ToInt32(resultado["CALCULO_TASA"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["RETIRO_PARCIAL"] != DBNull.Value) entidad.retiro_parcial = Convert.ToInt32(resultado["RETIRO_PARCIAL"]);
                            if (resultado["POR_RETIRO_MAXIMO"] != DBNull.Value) entidad.por_retiro_maximo = Convert.ToDecimal(resultado["POR_RETIRO_MAXIMO"]);
                            if (resultado["SALDO_MINIMO"] != DBNull.Value) entidad.saldo_minimo = Convert.ToDecimal(resultado["SALDO_MINIMO"]);
                            if (resultado["CRUZA"] != DBNull.Value) entidad.cruza = Convert.ToInt32(resultado["CRUZA"]);
                            if (resultado["PORCENTAJE_CRUCE"] != DBNull.Value) entidad.porcentaje_cruce = Convert.ToDecimal(resultado["PORCENTAJE_CRUCE"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToDecimal(resultado["RETENCION"]);
                            if (resultado["CUOTA_NOMINA"] != DBNull.Value) entidad.cuota_nomina = Convert.ToDecimal(resultado["CUOTA_NOMINA"]);
                            if (resultado["POR_RETIRO_PLAZO"] != DBNull.Value) entidad.por_retiro_plazo = Convert.ToInt32(resultado["POR_RETIRO_PLAZO"]);
                            if (resultado["OPCION_SALDO"] != DBNull.Value) entidad.opcion_saldo = Convert.ToString(resultado["OPCION_SALDO"]);
                            if (resultado["POR_RETIRO_MINIMO"] != DBNull.Value) entidad.por_retiro_minimo = Convert.ToDecimal(resultado["POR_RETIRO_MINIMO"]);
                            if (resultado["VALOR_MAXIMO_RETIRO"] != DBNull.Value) entidad.valor_maximo_retiro = Convert.ToDecimal(resultado["VALOR_MAXIMO_RETIRO"]);
                            if (resultado["POR_INT_DISM"] != DBNull.Value) entidad.por_int_dism = Convert.ToDecimal(resultado["POR_INT_DISM"]);
                            if (resultado["PORPLA_RET_T"] != DBNull.Value) entidad.porpla_ret_t = Convert.ToInt32(resultado["PORPLA_RET_T"]);
                            if (resultado["PORMONT_RET_T"] != DBNull.Value) entidad.pormont_ret_t = Convert.ToInt32(resultado["PORMONT_RET_T"]);
                            if (resultado["DIAS_GRACIA"] != DBNull.Value) entidad.dias_gracia = Convert.ToInt32(resultado["DIAS_GRACIA"]);
                            if (resultado["PRIORIDAD"] != DBNull.Value) entidad.prioridad = Convert.ToInt32(resultado["PRIORIDAD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["APLICA_RETENCION"] != DBNull.Value) entidad.aplica_retencion = Convert.ToInt32(resultado["APLICA_RETENCION"]);
                            if (resultado["INTERES_POR_CUENTA"] != DBNull.Value) entidad.interes_por_cuenta = Convert.ToInt32(resultado["INTERES_POR_CUENTA"]);

                            if (resultado["TIPO_SALDO_INT"] != DBNull.Value) entidad.tipo_saldo_int = Convert.ToInt32(resultado["TIPO_SALDO_INT"]);
                            if (resultado["PERIODICIDAD_INT"] != DBNull.Value) entidad.periodicidad_int = Convert.ToInt32(resultado["PERIODICIDAD_INT"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            //manejo de cuotas extras 
                            if (resultado["MANEJA_CUOTA_EXTRA"] != DBNull.Value) entidad.maneja_cuota_extra = Convert.ToInt32(resultado["MANEJA_CUOTA_EXTRA"]);
                            if (resultado["CUOTA_MIN_EXTRA"] != DBNull.Value) entidad.cuota_extra_min = Convert.ToDecimal(resultado["CUOTA_MIN_EXTRA"]);
                            if (resultado["CUOTA_MAX_EXTRA"] != DBNull.Value) entidad.cuota_extra_max = Convert.ToDecimal(resultado["CUOTA_MAX_EXTRA"]);
                            //Tasa Renovacion
                            if (resultado["INTERES_RENOVACION"] != DBNull.Value) entidad.interes_renovacion = Convert.ToInt32(resultado["INTERES_RENOVACION"]);
                            if (resultado["CALCULO_TASA_REN"] != DBNull.Value) entidad.calculo_tasa_ren = Convert.ToInt32(resultado["CALCULO_TASA_REN"]);
                            if (resultado["TASA_INTERES_REN"] != DBNull.Value) entidad.tasa_interes_ren = Convert.ToDecimal(resultado["TASA_INTERES_REN"]);
                            if (resultado["COD_TIPO_TASA_REN"] != DBNull.Value) entidad.cod_tipo_tasa_ren = Convert.ToInt32(resultado["COD_TIPO_TASA_REN"]);
                            if (resultado["TIPO_HISTORICO_REN"] != DBNull.Value) entidad.tipo_historico_ren = Convert.ToInt32(resultado["TIPO_HISTORICO_REN"]);
                            if (resultado["DESVIACION_REN"] != DBNull.Value) entidad.desviacion_ren = Convert.ToDecimal(resultado["DESVIACION_REN"]);




                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasProgramadoData", "ConsultarLineasProgramado", ex);
                        return null;
                    }
                }
            }
        }



        public List<LineasProgramado> ListarComboLineas(LineasProgramado pLineas, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LineasProgramado> lstPRO = new List<LineasProgramado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COD_LINEA_PROGRAMADO,NOMBRE FROM LINEAPROGRAMADO " + ObtenerFiltro(pLineas) + " ORDER BY COD_LINEA_PROGRAMADO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineasProgramado entidad = new LineasProgramado();
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstPRO.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPRO;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasProgramadoData", "ListarComboLineas", ex);
                        return null;
                    }
                }
            }
        }


    }
}
