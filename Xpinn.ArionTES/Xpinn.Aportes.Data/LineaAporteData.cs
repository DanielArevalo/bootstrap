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
    public class LineaAporteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla LineaAporteS
        /// </summary>
        public LineaAporteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla LineaAporteS de la base de datos
        /// </summary>
        /// <param name="pLineaAporte">Entidad LineaAporte</param>
        /// <returns>Entidad LineaAporte creada</returns>
        public LineaAporte CrearLineaAporte(LineaAporte pLineaAporte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_aporte = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_aporte.ParameterName = "p_cod_linea_aporte";
                        pcod_linea_aporte.Value = pLineaAporte.cod_linea_aporte;
                        pcod_linea_aporte.Direction = ParameterDirection.Output;
                        pcod_linea_aporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_aporte);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pLineaAporte.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo_aporte = cmdTransaccionFactory.CreateParameter();
                        ptipo_aporte.ParameterName = "p_tipo_aporte";
                        ptipo_aporte.Value = pLineaAporte.tipo_aporte;
                        ptipo_aporte.Direction = ParameterDirection.Input;
                        ptipo_aporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_aporte);

                        DbParameter ptipo_cuota = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuota.ParameterName = "p_tipo_cuota";
                        ptipo_cuota.Value = pLineaAporte.tipo_cuota;
                        ptipo_cuota.Direction = ParameterDirection.Input;
                        ptipo_cuota.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuota);

                        DbParameter pvalor_cuota_minima = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota_minima.ParameterName = "p_valor_cuota_minima";
                        pvalor_cuota_minima.Value = pLineaAporte.valor_cuota_minima;
                        pvalor_cuota_minima.Direction = ParameterDirection.Input;
                        pvalor_cuota_minima.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota_minima);

                        DbParameter pvalor_cuota_maximo = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota_maximo.ParameterName = "p_valor_cuota_maximo";
                        pvalor_cuota_maximo.Value = pLineaAporte.valor_cuota_maximo;
                        pvalor_cuota_maximo.Direction = ParameterDirection.Input;
                        pvalor_cuota_maximo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota_maximo);

                        DbParameter pobligatorio = cmdTransaccionFactory.CreateParameter();
                        pobligatorio.ParameterName = "p_obligatorio";
                        pobligatorio.Value = pLineaAporte.obligatorio;
                        pobligatorio.Direction = ParameterDirection.Input;
                        pobligatorio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pobligatorio);

                        DbParameter ptipo_liquidacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_liquidacion.ParameterName = "p_tipo_liquidacion";
                        ptipo_liquidacion.Value = pLineaAporte.tipo_liquidacion;
                        ptipo_liquidacion.Direction = ParameterDirection.Input;
                        ptipo_liquidacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_liquidacion);

                        DbParameter pmin_valor_pago = cmdTransaccionFactory.CreateParameter();
                        pmin_valor_pago.ParameterName = "p_min_valor_pago";
                        pmin_valor_pago.Value = pLineaAporte.min_valor_pago;
                        pmin_valor_pago.Direction = ParameterDirection.Input;
                        pmin_valor_pago.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmin_valor_pago);

                        DbParameter pmin_valor_retiro = cmdTransaccionFactory.CreateParameter();
                        pmin_valor_retiro.ParameterName = "p_min_valor_retiro";
                        pmin_valor_retiro.Value = pLineaAporte.min_valor_retiro;
                        pmin_valor_retiro.Direction = ParameterDirection.Input;
                        pmin_valor_retiro.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmin_valor_retiro);

                        DbParameter psaldo_minimo = cmdTransaccionFactory.CreateParameter();
                        psaldo_minimo.ParameterName = "p_saldo_minimo";
                        psaldo_minimo.Value = pLineaAporte.saldo_minimo;
                        psaldo_minimo.Direction = ParameterDirection.Input;
                        psaldo_minimo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(psaldo_minimo);

                        DbParameter pvalor_cierre = cmdTransaccionFactory.CreateParameter();
                        pvalor_cierre.ParameterName = "p_valor_cierre";
                        pvalor_cierre.Value = pLineaAporte.valor_cierre;
                        pvalor_cierre.Direction = ParameterDirection.Input;
                        pvalor_cierre.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cierre);

                        DbParameter pdias_cierre = cmdTransaccionFactory.CreateParameter();
                        pdias_cierre.ParameterName = "p_dias_cierre";
                        pdias_cierre.Value = pLineaAporte.dias_cierre;
                        pdias_cierre.Direction = ParameterDirection.Input;
                        pdias_cierre.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pdias_cierre);

                        DbParameter pcruzar = cmdTransaccionFactory.CreateParameter();
                        pcruzar.ParameterName = "p_cruzar";
                        pcruzar.Value = pLineaAporte.cruzar;
                        pcruzar.Direction = ParameterDirection.Input;
                        pcruzar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcruzar);

                        DbParameter pporcentaje_cruce = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_cruce.ParameterName = "p_porcentaje_cruce";
                        pporcentaje_cruce.Value = pLineaAporte.porcentaje_cruce;
                        pporcentaje_cruce.Direction = ParameterDirection.Input;
                        pporcentaje_cruce.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_cruce);

                        DbParameter pcobra_mora = cmdTransaccionFactory.CreateParameter();
                        pcobra_mora.ParameterName = "p_cobra_mora";
                        pcobra_mora.Value = pLineaAporte.cobra_mora;
                        pcobra_mora.Direction = ParameterDirection.Input;
                        pcobra_mora.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_mora);

                        DbParameter pprovisionar = cmdTransaccionFactory.CreateParameter();
                        pprovisionar.ParameterName = "p_provisionar";
                        pprovisionar.Value = pLineaAporte.provisionar;
                        pprovisionar.Direction = ParameterDirection.Input;
                        pprovisionar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprovisionar);

                        DbParameter ppermite_retiros = cmdTransaccionFactory.CreateParameter();
                        ppermite_retiros.ParameterName = "p_permite_retiros";
                        ppermite_retiros.Value = pLineaAporte.permite_retiros;
                        ppermite_retiros.Direction = ParameterDirection.Input;
                        ppermite_retiros.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppermite_retiros);

                        DbParameter ppermite_traslados = cmdTransaccionFactory.CreateParameter();
                        ppermite_traslados.ParameterName = "p_permite_traslados";
                        ppermite_traslados.Value = pLineaAporte.permite_traslados;
                        ppermite_traslados.Direction = ParameterDirection.Input;
                        ppermite_traslados.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppermite_traslados);

                        DbParameter pporcentaje_minimo = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_minimo.ParameterName = "p_porcentaje_minimo";
                        pporcentaje_minimo.Value = pLineaAporte.porcentaje_minimo;
                        pporcentaje_minimo.Direction = ParameterDirection.Input;
                        pporcentaje_minimo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_minimo);

                        DbParameter pporcentaje_maximo = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_maximo.ParameterName = "p_porcentaje_maximo";
                        pporcentaje_maximo.Value = pLineaAporte.porcentaje_maximo;
                        pporcentaje_maximo.Direction = ParameterDirection.Input;
                        pporcentaje_maximo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_maximo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pLineaAporte.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pdistribuye = cmdTransaccionFactory.CreateParameter();
                        pdistribuye.ParameterName = "pdistribuye";
                        if (pLineaAporte.distribuye == null)
                            pdistribuye.Value = DBNull.Value;
                        else
                            pdistribuye.Value = pLineaAporte.distribuye;
                        pdistribuye.Direction = ParameterDirection.Input;
                        pdistribuye.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdistribuye);

                        DbParameter pporcentaje_distr = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_distr.ParameterName = "pporcentaje_distr";
                        if (pLineaAporte.porcentaje_distr == null)
                            pporcentaje_distr.Value = DBNull.Value;
                        else
                            pporcentaje_distr.Value = pLineaAporte.porcentaje_distr;
                        pporcentaje_distr.Direction = ParameterDirection.Input;
                        pporcentaje_distr.DbType = DbType.Double;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_distr);

                        DbParameter pcap_minimo_irreduptible = cmdTransaccionFactory.CreateParameter();
                        pcap_minimo_irreduptible.ParameterName = "pcap_minimo_irreduptible";
                        if (pLineaAporte.cap_minimo_irreduptible == null)
                            pcap_minimo_irreduptible.Value = DBNull.Value;
                        else
                            pcap_minimo_irreduptible.Value = pLineaAporte.cap_minimo_irreduptible;
                        pcap_minimo_irreduptible.Direction = ParameterDirection.Input;
                        pcap_minimo_irreduptible.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcap_minimo_irreduptible);

                        DbParameter p_permite_pagoprod = cmdTransaccionFactory.CreateParameter();
                        p_permite_pagoprod.ParameterName = "p_permite_pagoprod";
                        if (pLineaAporte.permite_pagoprod == null)
                            p_permite_pagoprod.Value = DBNull.Value;
                        else
                            p_permite_pagoprod.Value = pLineaAporte.permite_pagoprod;
                        p_permite_pagoprod.Direction = ParameterDirection.Input;
                        p_permite_pagoprod.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_permite_pagoprod);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_LINEAAPORTE_CREAR";
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
        public LineaAporte ModificarLineaAporte(LineaAporte pLineaAporte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_aporte = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_aporte.ParameterName = "p_cod_linea_aporte";
                        pcod_linea_aporte.Value = pLineaAporte.cod_linea_aporte;
                        pcod_linea_aporte.Direction = ParameterDirection.Input;
                        pcod_linea_aporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_aporte);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pLineaAporte.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo_aporte = cmdTransaccionFactory.CreateParameter();
                        ptipo_aporte.ParameterName = "p_tipo_aporte";
                        ptipo_aporte.Value = pLineaAporte.tipo_aporte;
                        ptipo_aporte.Direction = ParameterDirection.Input;
                        ptipo_aporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_aporte);

                        DbParameter ptipo_cuota = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuota.ParameterName = "p_tipo_cuota";
                        ptipo_cuota.Value = pLineaAporte.tipo_cuota;
                        ptipo_cuota.Direction = ParameterDirection.Input;
                        ptipo_cuota.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuota);

                        DbParameter pvalor_cuota_minima = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota_minima.ParameterName = "p_valor_cuota_minima";
                        pvalor_cuota_minima.Value = pLineaAporte.valor_cuota_minima;
                        pvalor_cuota_minima.Direction = ParameterDirection.Input;
                        pvalor_cuota_minima.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota_minima);

                        DbParameter pvalor_cuota_maximo = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota_maximo.ParameterName = "p_valor_cuota_maximo";
                        pvalor_cuota_maximo.Value = pLineaAporte.valor_cuota_maximo;
                        pvalor_cuota_maximo.Direction = ParameterDirection.Input;
                        pvalor_cuota_maximo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota_maximo);

                        DbParameter pobligatorio = cmdTransaccionFactory.CreateParameter();
                        pobligatorio.ParameterName = "p_obligatorio";
                        pobligatorio.Value = pLineaAporte.obligatorio;
                        pobligatorio.Direction = ParameterDirection.Input;
                        pobligatorio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pobligatorio);

                        DbParameter ptipo_liquidacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_liquidacion.ParameterName = "p_tipo_liquidacion";
                        ptipo_liquidacion.Value = pLineaAporte.tipo_liquidacion;
                        ptipo_liquidacion.Direction = ParameterDirection.Input;
                        ptipo_liquidacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_liquidacion);

                        DbParameter pmin_valor_pago = cmdTransaccionFactory.CreateParameter();
                        pmin_valor_pago.ParameterName = "p_min_valor_pago";
                        pmin_valor_pago.Value = pLineaAporte.min_valor_pago;
                        pmin_valor_pago.Direction = ParameterDirection.Input;
                        pmin_valor_pago.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmin_valor_pago);

                        DbParameter pmin_valor_retiro = cmdTransaccionFactory.CreateParameter();
                        pmin_valor_retiro.ParameterName = "p_min_valor_retiro";
                        pmin_valor_retiro.Value = pLineaAporte.min_valor_retiro;
                        pmin_valor_retiro.Direction = ParameterDirection.Input;
                        pmin_valor_retiro.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmin_valor_retiro);

                        DbParameter psaldo_minimo = cmdTransaccionFactory.CreateParameter();
                        psaldo_minimo.ParameterName = "p_saldo_minimo";
                        psaldo_minimo.Value = pLineaAporte.saldo_minimo;
                        psaldo_minimo.Direction = ParameterDirection.Input;
                        psaldo_minimo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(psaldo_minimo);

                        DbParameter pvalor_cierre = cmdTransaccionFactory.CreateParameter();
                        pvalor_cierre.ParameterName = "p_valor_cierre";
                        pvalor_cierre.Value = pLineaAporte.valor_cierre;
                        pvalor_cierre.Direction = ParameterDirection.Input;
                        pvalor_cierre.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cierre);

                        DbParameter pdias_cierre = cmdTransaccionFactory.CreateParameter();
                        pdias_cierre.ParameterName = "p_dias_cierre";
                        pdias_cierre.Value = pLineaAporte.dias_cierre;
                        pdias_cierre.Direction = ParameterDirection.Input;
                        pdias_cierre.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pdias_cierre);

                        DbParameter pcruzar = cmdTransaccionFactory.CreateParameter();
                        pcruzar.ParameterName = "p_cruzar";
                        pcruzar.Value = pLineaAporte.cruzar;
                        pcruzar.Direction = ParameterDirection.Input;
                        pcruzar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcruzar);

                        DbParameter pporcentaje_cruce = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_cruce.ParameterName = "p_porcentaje_cruce";
                        pporcentaje_cruce.Value = pLineaAporte.porcentaje_cruce;
                        pporcentaje_cruce.Direction = ParameterDirection.Input;
                        pporcentaje_cruce.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_cruce);

                        DbParameter pcobra_mora = cmdTransaccionFactory.CreateParameter();
                        pcobra_mora.ParameterName = "p_cobra_mora";
                        pcobra_mora.Value = pLineaAporte.cobra_mora;
                        pcobra_mora.Direction = ParameterDirection.Input;
                        pcobra_mora.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_mora);

                        DbParameter pprovisionar = cmdTransaccionFactory.CreateParameter();
                        pprovisionar.ParameterName = "p_provisionar";
                        pprovisionar.Value = pLineaAporte.provisionar;
                        pprovisionar.Direction = ParameterDirection.Input;
                        pprovisionar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprovisionar);

                        DbParameter ppermite_retiros = cmdTransaccionFactory.CreateParameter();
                        ppermite_retiros.ParameterName = "p_permite_retiros";
                        ppermite_retiros.Value = pLineaAporte.permite_retiros;
                        ppermite_retiros.Direction = ParameterDirection.Input;
                        ppermite_retiros.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppermite_retiros);

                        DbParameter ppermite_traslados = cmdTransaccionFactory.CreateParameter();
                        ppermite_traslados.ParameterName = "p_permite_traslados";
                        ppermite_traslados.Value = pLineaAporte.permite_traslados;
                        ppermite_traslados.Direction = ParameterDirection.Input;
                        ppermite_traslados.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppermite_traslados);

                        DbParameter pporcentaje_minimo = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_minimo.ParameterName = "p_porcentaje_minimo";
                        pporcentaje_minimo.Value = pLineaAporte.porcentaje_minimo;
                        pporcentaje_minimo.Direction = ParameterDirection.Input;
                        pporcentaje_minimo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_minimo);

                        DbParameter pporcentaje_maximo = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_maximo.ParameterName = "p_porcentaje_maximo";
                        pporcentaje_maximo.Value = pLineaAporte.porcentaje_maximo;
                        pporcentaje_maximo.Direction = ParameterDirection.Input;
                        pporcentaje_maximo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_maximo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pLineaAporte.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pdistribuye = cmdTransaccionFactory.CreateParameter();
                        pdistribuye.ParameterName = "pdistribuye";
                        if (pLineaAporte.distribuye == null)
                            pdistribuye.Value = DBNull.Value;
                        else
                            pdistribuye.Value = pLineaAporte.distribuye;
                        pdistribuye.Direction = ParameterDirection.Input;
                        pdistribuye.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdistribuye);

                        DbParameter pporcentaje_distr = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_distr.ParameterName = "pporcentaje_distr";
                        if (pLineaAporte.porcentaje_distr == null)
                            pporcentaje_distr.Value = DBNull.Value;
                        else
                            pporcentaje_distr.Value = pLineaAporte.porcentaje_distr;
                        pporcentaje_distr.Direction = ParameterDirection.Input;
                        pporcentaje_distr.DbType = DbType.Double;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_distr);

                        DbParameter pcap_minimo_irreduptible = cmdTransaccionFactory.CreateParameter();
                        pcap_minimo_irreduptible.ParameterName = "pcap_minimo_irreduptible";
                        if (pLineaAporte.cap_minimo_irreduptible == null)
                            pcap_minimo_irreduptible.Value = DBNull.Value;
                        else
                            pcap_minimo_irreduptible.Value = pLineaAporte.cap_minimo_irreduptible;
                        pcap_minimo_irreduptible.Direction = ParameterDirection.Input;
                        pcap_minimo_irreduptible.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcap_minimo_irreduptible);

                        DbParameter p_permite_pagoprod = cmdTransaccionFactory.CreateParameter();
                        p_permite_pagoprod.ParameterName = "p_permite_pagoprod";
                        if (pLineaAporte.permite_pagoprod == null)
                            p_permite_pagoprod.Value = DBNull.Value;
                        else
                            p_permite_pagoprod.Value = pLineaAporte.permite_pagoprod;
                        p_permite_pagoprod.Direction = ParameterDirection.Input;
                        p_permite_pagoprod.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_permite_pagoprod);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_LINEAAPORTE_MODIF";
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
                        LineaAporte pLineaAporte = new LineaAporte();

                        DbParameter pcod_linea_aporte = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_aporte.ParameterName = "p_cod_linea_aporte";
                        pcod_linea_aporte.Value = pLineaAporte.cod_linea_aporte;
                        pcod_linea_aporte.Direction = ParameterDirection.Input;
                        pcod_linea_aporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_aporte);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
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
        public LineaAporte ConsultarLineaAporte(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            LineaAporte entidad = new LineaAporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM LineaAporte WHERE COD_LINEA_APORTE = " + pId.ToString();
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
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
                            if (resultado["VALOR_CUOTA_MINIMA"] != DBNull.Value) entidad.valor_cuota_minima = Convert.ToDecimal(resultado["VALOR_CUOTA_MINIMA"]);
                            if (resultado["VALOR_CUOTA_MAXIMO"] != DBNull.Value) entidad.valor_cuota_maximo = Convert.ToDecimal(resultado["VALOR_CUOTA_MAXIMO"]);
                            if (resultado["OBLIGATORIO"] != DBNull.Value) entidad.obligatorio = Convert.ToInt32(resultado["OBLIGATORIO"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["MIN_VALOR_PAGO"] != DBNull.Value) entidad.min_valor_pago = Convert.ToDecimal(resultado["MIN_VALOR_PAGO"]);
                            if (resultado["MIN_VALOR_RETIRO"] != DBNull.Value) entidad.min_valor_retiro = Convert.ToDecimal(resultado["MIN_VALOR_RETIRO"]);
                            if (resultado["SALDO_MINIMO"] != DBNull.Value) entidad.saldo_minimo = Convert.ToInt64(resultado["SALDO_MINIMO"]);
                            if (resultado["VALOR_CIERRE"] != DBNull.Value) entidad.valor_cierre = Convert.ToDecimal(resultado["VALOR_CIERRE"]);
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
                            if (resultado["DISTRIBUYE"] != DBNull.Value) entidad.distribuye = Convert.ToInt32(resultado["DISTRIBUYE"]);
                            if (resultado["PORCENTAJE_DIST"] != DBNull.Value) entidad.porcentaje_distr = Convert.ToDouble(resultado["PORCENTAJE_DIST"]);
                            if (resultado["CAP_MINIMO_IRREDUPTIBLE"] != DBNull.Value) entidad.cap_minimo_irreduptible = Convert.ToDecimal(resultado["CAP_MINIMO_IRREDUPTIBLE"]);
                            if (resultado["PERMITE_PAGOPROD"] != DBNull.Value) entidad.permite_pagoprod = Convert.ToInt32(resultado["PERMITE_PAGOPROD"]);
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
        public List<LineaAporte> ListarLineaAporte(LineaAporte pLineaAporte, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<LineaAporte> lstLineaAporte = new List<LineaAporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM lineaaporte " + ObtenerFiltro(pLineaAporte) + " ORDER BY COD_LINEA_APORTE ";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineaAporte entidad = new LineaAporte();
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_APORTE"] != DBNull.Value) entidad.tipo_aporte = Convert.ToInt32(resultado["TIPO_APORTE"]);
                            if (resultado["TIPO_CUOTA"] != DBNull.Value) entidad.tipo_cuota = Convert.ToInt32(resultado["TIPO_CUOTA"]);
                            if (resultado["VALOR_CUOTA_MINIMA"] != DBNull.Value) entidad.valor_cuota_minima = Convert.ToDecimal(resultado["VALOR_CUOTA_MINIMA"]);
                            if (resultado["VALOR_CUOTA_MAXIMO"] != DBNull.Value) entidad.valor_cuota_maximo = Convert.ToDecimal(resultado["VALOR_CUOTA_MAXIMO"]);
                            if (resultado["OBLIGATORIO"] != DBNull.Value) entidad.obligatorio = Convert.ToInt32(resultado["OBLIGATORIO"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["MIN_VALOR_PAGO"] != DBNull.Value) entidad.min_valor_pago = Convert.ToDecimal(resultado["MIN_VALOR_PAGO"]);
                            if (resultado["MIN_VALOR_RETIRO"] != DBNull.Value) entidad.min_valor_retiro = Convert.ToDecimal(resultado["MIN_VALOR_RETIRO"]);
                            if (resultado["SALDO_MINIMO"] != DBNull.Value) entidad.saldo_minimo = Convert.ToInt64(resultado["SALDO_MINIMO"]);
                            if (resultado["VALOR_CIERRE"] != DBNull.Value) entidad.valor_cierre = Convert.ToDecimal(resultado["VALOR_CIERRE"]);
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
                            if (resultado["DISTRIBUYE"] != DBNull.Value) entidad.distribuye = Convert.ToInt32(resultado["DISTRIBUYE"]);
                            if (resultado["PORCENTAJE_DIST"] != DBNull.Value) entidad.porcentaje_distr = Convert.ToDouble(resultado["PORCENTAJE_DIST"]);
                            if (resultado["CAP_MINIMO_IRREDUPTIBLE"] != DBNull.Value) entidad.cap_minimo_irreduptible = Convert.ToDecimal(resultado["CAP_MINIMO_IRREDUPTIBLE"]);
                            if (resultado["PERMITE_PAGOPROD"] != DBNull.Value) entidad.permite_pagoprod = Convert.ToInt32(resultado["PERMITE_PAGOPROD"]);
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


    }
}