using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Credito
    /// </summary>
    public class CreditoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Credito
        /// </summary>
        public CreditoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Credito de la base de datos
        /// </summary>
        /// <param name="pCredito">Entidad Credito</param>
        /// <returns>Entidad Credito creada</returns>
        public Credito CrearCredito(Credito pCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = param + "NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = 0;
                        pNUMERO_RADICACION.Direction = ParameterDirection.Output;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = param + "IDENTIFICACION";
                        pIDENTIFICACION.Value = pCredito.identificacion;

                        DbParameter pTIPO_IDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pTIPO_IDENTIFICACION.ParameterName = param + "TIPO_IDENTIFICACION";
                        pTIPO_IDENTIFICACION.Value = pCredito.tipo_identificacion;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = param + "NOMBRE";
                        pNOMBRE.Value = pCredito.nombre;

                        DbParameter pLINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        pLINEA_CREDITO.ParameterName = param + "LINEA_CREDITO";
                        pLINEA_CREDITO.Value = pCredito.linea_credito;

                        DbParameter pMONTO = cmdTransaccionFactory.CreateParameter();
                        pMONTO.ParameterName = param + "MONTO";
                        pMONTO.Value = pCredito.monto;

                        DbParameter pPLAZO = cmdTransaccionFactory.CreateParameter();
                        pPLAZO.ParameterName = param + "PLAZO";
                        pPLAZO.Value = pCredito.plazo;

                        DbParameter pPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pPERIODICIDAD.ParameterName = param + "PERIODICIDAD";
                        pPERIODICIDAD.Value = pCredito.periodicidad;

                        DbParameter pVALOR_CUOTA = cmdTransaccionFactory.CreateParameter();
                        pVALOR_CUOTA.ParameterName = param + "VALOR_CUOTA";
                        pVALOR_CUOTA.Value = pCredito.valor_cuota;

                        DbParameter pFORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        pFORMA_PAGO.ParameterName = param + "FORMA_PAGO";
                        pFORMA_PAGO.Value = pCredito.forma_pago;


                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_IDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pLINEA_CREDITO);
                        cmdTransaccionFactory.Parameters.Add(pMONTO);
                        cmdTransaccionFactory.Parameters.Add(pPLAZO);
                        cmdTransaccionFactory.Parameters.Add(pPERIODICIDAD);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_CUOTA);
                        cmdTransaccionFactory.Parameters.Add(pFORMA_PAGO);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_FabricaCreditos_Credito_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCredito, "Credito", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                        pCredito.numero_radicacion = Convert.ToInt64(pNUMERO_RADICACION.Value);
                        return pCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "CrearCredito", ex);
                        return null;
                    }
                }
            }
        }

        public AtributosCredito CrearAtributosCredito(AtributosCredito pAtributosCredito, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pAtributosCredito.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pAtributosCredito.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pcalculo_atr = cmdTransaccionFactory.CreateParameter();
                        pcalculo_atr.ParameterName = "p_calculo_atr";
                        if (pAtributosCredito.calculo_atr == null)
                            pcalculo_atr.Value = DBNull.Value;
                        else
                            pcalculo_atr.Value = pAtributosCredito.calculo_atr;
                        pcalculo_atr.Direction = ParameterDirection.Input;
                        pcalculo_atr.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcalculo_atr);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pAtributosCredito.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pAtributosCredito.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pAtributosCredito.desviacion == null)
                            pdesviacion.Value = DBNull.Value;
                        else
                            pdesviacion.Value = pAtributosCredito.desviacion;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter ptipo_tasa = cmdTransaccionFactory.CreateParameter();
                        ptipo_tasa.ParameterName = "p_tipo_tasa";
                        if (pAtributosCredito.tipo_tasa == null)
                            ptipo_tasa.Value = DBNull.Value;
                        else
                            ptipo_tasa.Value = pAtributosCredito.tipo_tasa;
                        ptipo_tasa.Direction = ParameterDirection.Input;
                        ptipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tasa);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        if (pAtributosCredito.tasa == null)
                            ptasa.Value = DBNull.Value;
                        else
                            ptasa.Value = pAtributosCredito.tasa;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter pcobra_mora = cmdTransaccionFactory.CreateParameter();
                        pcobra_mora.ParameterName = "p_cobra_mora";
                        if (pAtributosCredito.cobra_mora == null)
                            pcobra_mora.Value = DBNull.Value;
                        else
                            pcobra_mora.Value = pAtributosCredito.cobra_mora;
                        pcobra_mora.Direction = ParameterDirection.Input;
                        pcobra_mora.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_mora);

                        DbParameter psaldo_atributo = cmdTransaccionFactory.CreateParameter();
                        psaldo_atributo.ParameterName = "p_saldo_atributo";
                        if (pAtributosCredito.saldo_atributo == null)
                            psaldo_atributo.Value = DBNull.Value;
                        else
                            psaldo_atributo.Value = pAtributosCredito.saldo_atributo;
                        psaldo_atributo.Direction = ParameterDirection.Input;
                        psaldo_atributo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_atributo);

                        DbParameter pcausado_atributo = cmdTransaccionFactory.CreateParameter();
                        pcausado_atributo.ParameterName = "p_causado_atributo";
                        if (pAtributosCredito.causado_atributo == null)
                            pcausado_atributo.Value = DBNull.Value;
                        else
                            pcausado_atributo.Value = pAtributosCredito.causado_atributo;
                        pcausado_atributo.Direction = ParameterDirection.Input;
                        pcausado_atributo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcausado_atributo);

                        DbParameter porden_atributo = cmdTransaccionFactory.CreateParameter();
                        porden_atributo.ParameterName = "p_orden_atributo";
                        if (pAtributosCredito.orden_atributo == null)
                            porden_atributo.Value = DBNull.Value;
                        else
                            porden_atributo.Value = pAtributosCredito.orden_atributo;
                        porden_atributo.Direction = ParameterDirection.Input;
                        porden_atributo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(porden_atributo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ATRIBUTOSC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAtributosCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "CrearAtributosCredito", ex);
                        return null;
                    }
                }
            }
        }

        public Credito CrearCreditoDesdeFuncionImportacion(Credito pCREDITO, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pCREDITO.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pnumero_obligacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_obligacion.ParameterName = "p_numero_obligacion";
                        if (pCREDITO.numero_obligacion == null)
                            pnumero_obligacion.Value = DBNull.Value;
                        else
                            pnumero_obligacion.Value = pCREDITO.numero_obligacion;
                        pnumero_obligacion.Direction = ParameterDirection.Input;
                        pnumero_obligacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_obligacion);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pCREDITO.codigo_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pcod_deudor = cmdTransaccionFactory.CreateParameter();
                        pcod_deudor.ParameterName = "p_cod_deudor";
                        pcod_deudor.Value = pCREDITO.cod_deudor;
                        pcod_deudor.Direction = ParameterDirection.Input;
                        pcod_deudor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_deudor);

                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "p_cod_linea_credito";
                        pcod_linea_credito.Value = pCREDITO.cod_linea_credito;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        pcod_linea_credito.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        DbParameter pmonto_solicitado = cmdTransaccionFactory.CreateParameter();
                        pmonto_solicitado.ParameterName = "p_monto_solicitado";
                        pmonto_solicitado.Value = pCREDITO.monto_solicitado;
                        pmonto_solicitado.Direction = ParameterDirection.Input;
                        pmonto_solicitado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmonto_solicitado);

                        DbParameter pmonto_aprobado = cmdTransaccionFactory.CreateParameter();
                        pmonto_aprobado.ParameterName = "p_monto_aprobado";
                        if (pCREDITO.monto_aprobado == 0)
                            pmonto_aprobado.Value = DBNull.Value;
                        else
                            pmonto_aprobado.Value = pCREDITO.monto_aprobado;
                        pmonto_aprobado.Direction = ParameterDirection.Input;
                        pmonto_aprobado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmonto_aprobado);

                        DbParameter pmonto_desembolsado = cmdTransaccionFactory.CreateParameter();
                        pmonto_desembolsado.ParameterName = "p_monto_desembolsado";
                        if (pCREDITO.monto_desembolsado == null)
                            pmonto_desembolsado.Value = DBNull.Value;
                        else
                            pmonto_desembolsado.Value = pCREDITO.monto_desembolsado;
                        pmonto_desembolsado.Direction = ParameterDirection.Input;
                        pmonto_desembolsado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmonto_desembolsado);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        pcod_moneda.Value = pCREDITO.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter pfecha_solicitud = cmdTransaccionFactory.CreateParameter();
                        pfecha_solicitud.ParameterName = "p_fecha_solicitud";
                        pfecha_solicitud.Value = pCREDITO.fecha_solicitud;
                        pfecha_solicitud.Direction = ParameterDirection.Input;
                        pfecha_solicitud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_solicitud);

                        DbParameter pfecha_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                        if (pCREDITO.fecha_aprobacion == null)
                            pfecha_aprobacion.Value = DBNull.Value;
                        else
                            pfecha_aprobacion.Value = pCREDITO.fecha_aprobacion;
                        pfecha_aprobacion.Direction = ParameterDirection.Input;
                        pfecha_aprobacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aprobacion);

                        DbParameter pfecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        pfecha_desembolso.ParameterName = "p_fecha_desembolso";
                        if (pCREDITO.fecha_desembolso == null)
                            pfecha_desembolso.Value = DBNull.Value;
                        else
                            pfecha_desembolso.Value = pCREDITO.fecha_desembolso;
                        pfecha_desembolso.Direction = ParameterDirection.Input;
                        pfecha_desembolso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_desembolso);

                        DbParameter pfecha_primerpago = cmdTransaccionFactory.CreateParameter();
                        pfecha_primerpago.ParameterName = "p_fecha_primerpago";
                        if (pCREDITO.fecha_prim_pago == null)
                            pfecha_primerpago.Value = DBNull.Value;
                        else
                            pfecha_primerpago.Value = pCREDITO.fecha_prim_pago;
                        pfecha_primerpago.Direction = ParameterDirection.Input;
                        pfecha_primerpago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_primerpago);

                        DbParameter pnumero_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuotas.ParameterName = "p_numero_cuotas";
                        pnumero_cuotas.Value = pCREDITO.numero_cuotas;
                        pnumero_cuotas.Direction = ParameterDirection.Input;
                        pnumero_cuotas.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuotas);

                        DbParameter pcuotas_pagadas = cmdTransaccionFactory.CreateParameter();
                        pcuotas_pagadas.ParameterName = "p_cuotas_pagadas";
                        if (pCREDITO.cuotas_pagadas == 0)
                            pcuotas_pagadas.Value = DBNull.Value;
                        else
                            pcuotas_pagadas.Value = pCREDITO.cuotas_pagadas;
                        pcuotas_pagadas.Direction = ParameterDirection.Input;
                        pcuotas_pagadas.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcuotas_pagadas);

                        DbParameter pcuotas_pendientes = cmdTransaccionFactory.CreateParameter();
                        pcuotas_pendientes.ParameterName = "p_cuotas_pendientes";
                        if (pCREDITO.cuotas_pendientes == null)
                            pcuotas_pendientes.Value = DBNull.Value;
                        else
                            pcuotas_pendientes.Value = pCREDITO.cuotas_pendientes;
                        pcuotas_pendientes.Direction = ParameterDirection.Input;
                        pcuotas_pendientes.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcuotas_pendientes);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        pcod_periodicidad.Value = pCREDITO.periodicidad;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter ptipo_liquidacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_liquidacion.ParameterName = "p_tipo_liquidacion";
                        ptipo_liquidacion.Value = pCREDITO.tipo_liquidacion;
                        ptipo_liquidacion.Direction = ParameterDirection.Input;
                        ptipo_liquidacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_liquidacion);

                        DbParameter pvalor_cuota = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota.ParameterName = "p_valor_cuota";
                        if (pCREDITO.valor_cuota == 0)
                            pvalor_cuota.Value = DBNull.Value;
                        else
                            pvalor_cuota.Value = pCREDITO.valor_cuota;
                        pvalor_cuota.Direction = ParameterDirection.Input;
                        pvalor_cuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        if (pCREDITO.forma_pago == null)
                            pforma_pago.Value = DBNull.Value;
                        else
                            pforma_pago.Value = pCREDITO.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter pfecha_ultimo_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_ultimo_pago.ParameterName = "p_fecha_ultimo_pago";
                        if (pCREDITO.fecha_ultimo_pago == null)
                            pfecha_ultimo_pago.Value = DBNull.Value;
                        else
                            pfecha_ultimo_pago.Value = pCREDITO.fecha_ultimo_pago;
                        pfecha_ultimo_pago.Direction = ParameterDirection.Input;
                        pfecha_ultimo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ultimo_pago);

                        DbParameter pfecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        pfecha_vencimiento.ParameterName = "p_fecha_vencimiento";
                        if (pCREDITO.fecha_vencimiento == null)
                            pfecha_vencimiento.Value = DBNull.Value;
                        else
                            pfecha_vencimiento.Value = pCREDITO.fecha_vencimiento;
                        pfecha_vencimiento.Direction = ParameterDirection.Input;
                        pfecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_vencimiento);

                        DbParameter pfecha_proximo_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_proximo_pago.ParameterName = "p_fecha_proximo_pago";
                        if (pCREDITO.fecha_prox_pago == null)
                            pfecha_proximo_pago.Value = DBNull.Value;
                        else
                            pfecha_proximo_pago.Value = pCREDITO.fecha_prox_pago;
                        pfecha_proximo_pago.Direction = ParameterDirection.Input;
                        pfecha_proximo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_proximo_pago);

                        DbParameter ptipo_gracia = cmdTransaccionFactory.CreateParameter();
                        ptipo_gracia.ParameterName = "p_tipo_gracia";
                        if (pCREDITO.tipo_gracia == null)
                            ptipo_gracia.Value = DBNull.Value;
                        else
                            ptipo_gracia.Value = pCREDITO.tipo_gracia;
                        ptipo_gracia.Direction = ParameterDirection.Input;
                        ptipo_gracia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_gracia);

                        DbParameter pcod_atr_gra = cmdTransaccionFactory.CreateParameter();
                        pcod_atr_gra.ParameterName = "p_cod_atr_gra";
                        if (pCREDITO.cod_atr_gracia == null)
                            pcod_atr_gra.Value = DBNull.Value;
                        else
                            pcod_atr_gra.Value = pCREDITO.cod_atr_gracia;
                        pcod_atr_gra.Direction = ParameterDirection.Input;
                        pcod_atr_gra.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr_gra);

                        DbParameter pperiodo_gracia = cmdTransaccionFactory.CreateParameter();
                        pperiodo_gracia.ParameterName = "p_periodo_gracia";
                        if (pCREDITO.periodo_gracia == null)
                            pperiodo_gracia.Value = DBNull.Value;
                        else
                            pperiodo_gracia.Value = pCREDITO.periodo_gracia;
                        pperiodo_gracia.Direction = ParameterDirection.Input;
                        pperiodo_gracia.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pperiodo_gracia);

                        DbParameter pcod_clasifica = cmdTransaccionFactory.CreateParameter();
                        pcod_clasifica.ParameterName = "p_cod_clasifica";
                        if (pCREDITO.cod_clasifica == null)
                            pcod_clasifica.Value = DBNull.Value;
                        else
                            pcod_clasifica.Value = pCREDITO.cod_clasifica;
                        pcod_clasifica.Direction = ParameterDirection.Input;
                        pcod_clasifica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_clasifica);

                        DbParameter psaldo_capital = cmdTransaccionFactory.CreateParameter();
                        psaldo_capital.ParameterName = "p_saldo_capital";
                        if (pCREDITO.saldo_capital == 0)
                            psaldo_capital.Value = DBNull.Value;
                        else
                            psaldo_capital.Value = pCREDITO.saldo_capital;
                        psaldo_capital.Direction = ParameterDirection.Input;
                        psaldo_capital.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_capital);

                        DbParameter potros_saldos = cmdTransaccionFactory.CreateParameter();
                        potros_saldos.ParameterName = "p_otros_saldos";
                        potros_saldos.Value = pCREDITO.otros_saldos;
                        potros_saldos.Direction = ParameterDirection.Input;
                        potros_saldos.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(potros_saldos);

                        DbParameter pcod_asesor_com = cmdTransaccionFactory.CreateParameter();
                        pcod_asesor_com.ParameterName = "p_cod_asesor_com";
                        if (pCREDITO.CodigoAsesor == 0)
                            pcod_asesor_com.Value = DBNull.Value;
                        else
                            pcod_asesor_com.Value = pCREDITO.CodigoAsesor;
                        pcod_asesor_com.Direction = ParameterDirection.Input;
                        pcod_asesor_com.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_asesor_com);

                        DbParameter ptipo_credito = cmdTransaccionFactory.CreateParameter();
                        ptipo_credito.ParameterName = "p_tipo_credito";
                        if (pCREDITO.tipo_credito == null)
                            ptipo_credito.Value = DBNull.Value;
                        else
                            ptipo_credito.Value = pCREDITO.tipo_credito;
                        ptipo_credito.Direction = ParameterDirection.Input;
                        ptipo_credito.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_credito);

                        DbParameter pnum_radic_origen = cmdTransaccionFactory.CreateParameter();
                        pnum_radic_origen.ParameterName = "p_num_radic_origen";
                        if (pCREDITO.num_radic_origen == null)
                            pnum_radic_origen.Value = DBNull.Value;
                        else
                            pnum_radic_origen.Value = pCREDITO.num_radic_origen;
                        pnum_radic_origen.Direction = ParameterDirection.Input;
                        pnum_radic_origen.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_radic_origen);

                        DbParameter pvrreestructurado = cmdTransaccionFactory.CreateParameter();
                        pvrreestructurado.ParameterName = "p_vrreestructurado";
                        if (pCREDITO.valor_para_refinanciar == 0)
                            pvrreestructurado.Value = DBNull.Value;
                        else
                            pvrreestructurado.Value = pCREDITO.valor_para_refinanciar;
                        pvrreestructurado.Direction = ParameterDirection.Input;
                        pvrreestructurado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvrreestructurado);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        if (pCREDITO.cod_empresa == null)
                            pcod_empresa.Value = DBNull.Value;
                        else
                            pcod_empresa.Value = pCREDITO.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pcod_pagaduria = cmdTransaccionFactory.CreateParameter();
                        pcod_pagaduria.ParameterName = "p_cod_pagaduria";
                        if (pCREDITO.cod_pagaduria == null)
                            pcod_pagaduria.Value = DBNull.Value;
                        else
                            pcod_pagaduria.Value = pCREDITO.cod_pagaduria;
                        pcod_pagaduria.Direction = ParameterDirection.Input;
                        pcod_pagaduria.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_pagaduria);

                        DbParameter pgradiente = cmdTransaccionFactory.CreateParameter();
                        pgradiente.ParameterName = "p_gradiente";
                        pgradiente.Value = pCREDITO.gradiente;
                        pgradiente.Direction = ParameterDirection.Input;
                        pgradiente.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pgradiente);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_fecha_inicio";
                        if (pCREDITO.fecha_inicio == null)
                            pfecha_inicio.Value = DBNull.Value;
                        else
                            pfecha_inicio.Value = pCREDITO.fecha_inicio;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter pdias_ajuste = cmdTransaccionFactory.CreateParameter();
                        pdias_ajuste.ParameterName = "p_dias_ajuste";
                        if (pCREDITO.dias_ajuste == 0)
                            pdias_ajuste.Value = DBNull.Value;
                        else
                            pdias_ajuste.Value = pCREDITO.dias_ajuste;
                        pdias_ajuste.Direction = ParameterDirection.Input;
                        pdias_ajuste.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pdias_ajuste);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pCREDITO.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pCREDITO.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = pCREDITO.fecha_creacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        pusuariocreacion.Value = pCREDITO.cod_usuario;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        if (pCREDITO.fecha_ultima_mod == null)
                            pfecultmod.Value = DBNull.Value;
                        else
                            pfecultmod.Value = pCREDITO.fecha_ultima_mod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "p_usuultmod";
                        if (pCREDITO.cod_usuario_ultima_mod == null)
                            pusuultmod.Value = DBNull.Value;
                        else
                            pusuultmod.Value = pCREDITO.cod_usuario_ultima_mod;
                        pusuultmod.Direction = ParameterDirection.Input;
                        pusuultmod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);

                        // Segun porque ya no aplica, asi que se va nullo
                        DbParameter ptir = cmdTransaccionFactory.CreateParameter();
                        ptir.Value = DBNull.Value; ;
                        ptir.Direction = ParameterDirection.Input;
                        ptir.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptir);

                        DbParameter ppago_especial = cmdTransaccionFactory.CreateParameter();
                        ppago_especial.ParameterName = "p_pago_especial";
                        if (pCREDITO.pago_especial == null)
                            ppago_especial.Value = DBNull.Value;
                        else
                            ppago_especial.Value = pCREDITO.pago_especial;
                        ppago_especial.Direction = ParameterDirection.Input;
                        ppago_especial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppago_especial);

                        DbParameter puniversidad = cmdTransaccionFactory.CreateParameter();
                        puniversidad.ParameterName = "p_universidad";
                        if (pCREDITO.universidad == null)
                            puniversidad.Value = DBNull.Value;
                        else
                            puniversidad.Value = pCREDITO.universidad;
                        puniversidad.Direction = ParameterDirection.Input;
                        puniversidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(puniversidad);

                        DbParameter psemestre = cmdTransaccionFactory.CreateParameter();
                        psemestre.ParameterName = "p_semestre";
                        if (pCREDITO.semestre == null)
                            psemestre.Value = DBNull.Value;
                        else
                            psemestre.Value = pCREDITO.semestre;
                        psemestre.Direction = ParameterDirection.Input;
                        psemestre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psemestre);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CREDITO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCREDITO;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CREDITOData", "CrearCreditoDesdeFuncionImportacion", ex);
                        return null;
                    }
                }
            }
        }

        public void ActualizarEstadoCredito(long numero_radicacion, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_radicacion.ParameterName = "P_NUMERO_RADICACION";
                        p_radicacion.Value = numero_radicacion;
                        cmdTransaccionFactory.Parameters.Add(p_radicacion);

                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ESTADO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ActualizarEstadoCredito", ex);
                    }
                }
            }
        }

        public void cambiotasa(string radicacion, string calculo_atr, string tasa, string tipotasa, string desviacion, string tipoHisto, string CodArt, Usuario pUsuario, string op)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_radicacion.ParameterName = "p_radicacion";
                        p_radicacion.Value = Convert.ToInt64(radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_radicacion);

                        DbParameter p_calculo_atr = cmdTransaccionFactory.CreateParameter();
                        p_calculo_atr.ParameterName = "p_calculo_atr";
                        p_calculo_atr.Value = calculo_atr;
                        cmdTransaccionFactory.Parameters.Add(p_calculo_atr);

                        DbParameter p_tasa = cmdTransaccionFactory.CreateParameter();
                        p_tasa.ParameterName = "p_tasa";
                        if (tasa.ToString() != "") p_tasa.Value = Convert.ToDecimal(tasa.ToString()); else p_tasa.Value = DBNull.Value;
                        cmdTransaccionFactory.Parameters.Add(p_tasa);

                        DbParameter p_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tasa.ParameterName = "p_tipo_tasa";
                        if (tipotasa != "") p_tipo_tasa.Value = Convert.ToInt64(tipotasa); else p_tipo_tasa.Value = DBNull.Value;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tasa);

                        DbParameter p_tipo_historico = cmdTransaccionFactory.CreateParameter();
                        p_tipo_historico.ParameterName = "p_tipo_historico";
                        if (tipoHisto != "") p_tipo_historico.Value = Convert.ToInt64(tipoHisto); else p_tipo_historico.Value = DBNull.Value;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_historico);

                        DbParameter p_desviacion = cmdTransaccionFactory.CreateParameter();
                        p_desviacion.ParameterName = "p_desviacion";
                        if (desviacion != "") p_desviacion.Value = Convert.ToInt64(desviacion); else p_desviacion.Value = DBNull.Value;
                        cmdTransaccionFactory.Parameters.Add(p_desviacion);

                        DbParameter p_Cod_Art = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Art.ParameterName = "P_COD_ATR";
                        if (CodArt != "") p_Cod_Art.Value = Convert.ToInt64(CodArt); else p_Cod_Art.Value = DBNull.Value;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Art);

                        DbParameter P_Opcion = cmdTransaccionFactory.CreateParameter();
                        P_Opcion.ParameterName = "P_Opcion";
                        if (op != "") P_Opcion.Value = Convert.ToInt64(op); else P_Opcion.Value = DBNull.Value;
                        cmdTransaccionFactory.Parameters.Add(P_Opcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CAMBIOTASA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCredito, "Credito", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "CrearCredito", ex);

                    }
                }
            }
        }
        public CreditoSolicitado cambiotasaFija(CreditoSolicitado pCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        DbParameter p_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_radicacion.ParameterName = "P_RADICACION";
                        p_radicacion.Value = pCredito.NumeroCredito;
                        p_radicacion.Direction = ParameterDirection.Input;
                        p_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_radicacion);

                        DbParameter p_calculo_atr = cmdTransaccionFactory.CreateParameter();
                        p_calculo_atr.ParameterName = "P_CALCULO_ATR";
                        p_calculo_atr.Value = pCredito.calculo_atr;
                        p_calculo_atr.Direction = ParameterDirection.Input;
                        p_calculo_atr.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_calculo_atr);

                        //Se omitió validación para poder dejar la tasa en 0
                        DbParameter p_tasa = cmdTransaccionFactory.CreateParameter();
                        p_tasa.ParameterName = "P_TASA";
                        //if (pCredito.tasa == 0)
                        //    p_tasa.Value = DBNull.Value;
                        //else
                        p_tasa.Value = pCredito.tasa;
                        p_tasa.Direction = ParameterDirection.Input;
                        p_tasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_tasa);

                        DbParameter p_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tasa.ParameterName = "P_TIPO_TASA";
                        if (pCredito.TipoTasa == 0)
                            p_tipo_tasa.Value = DBNull.Value;
                        else
                            p_tipo_tasa.Value = pCredito.TipoTasa;
                        p_tipo_tasa.Direction = ParameterDirection.Input;
                        p_tipo_tasa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tasa);

                        DbParameter p_tipo_historico = cmdTransaccionFactory.CreateParameter();
                        p_tipo_historico.ParameterName = "P_TIPO_HISTORICO";
                        if (pCredito.TipoHistorico == null)
                            p_tipo_historico.Value = DBNull.Value;
                        else
                            p_tipo_historico.Value = pCredito.TipoHistorico;
                        p_tipo_historico.Direction = ParameterDirection.Input;
                        p_tipo_historico.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_historico);

                        DbParameter p_desviacion = cmdTransaccionFactory.CreateParameter();
                        p_desviacion.ParameterName = "P_DESVIACION";
                        if (pCredito.Desviacion == null)
                            p_desviacion.Value = DBNull.Value;
                        else
                            p_desviacion.Value = pCredito.Desviacion;
                        p_desviacion.Direction = ParameterDirection.Input;
                        p_desviacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_desviacion);

                        DbParameter p_Cod_Art = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Art.ParameterName = "P_COD_ATR";
                        if (pCredito.CodAtr == 0)
                            p_Cod_Art.Value = DBNull.Value;
                        else
                            p_Cod_Art.Value = pCredito.CodAtr;
                        p_Cod_Art.Direction = ParameterDirection.Input;
                        p_Cod_Art.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Art);

                        DbParameter P_Opcion = cmdTransaccionFactory.CreateParameter();
                        P_Opcion.ParameterName = "P_Opcion";
                        P_Opcion.Value = pCredito.Operacion;
                        cmdTransaccionFactory.Parameters.Add(P_Opcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CAMBIOTASA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCredito, "Credito", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        return pCredito;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "CrearCredito", ex);
                        return null;
                    }
                }
            }
        }

        public void cambiolinea(Int64 radicacion, string cod_linea, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "pnumero_radicacion";
                        pnumero_radicacion.Value = radicacion;

                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "pcod_linea_credito";
                        pcod_linea_credito.Value = cod_linea;

                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APR_CAMBIOLINEA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "cambioatributos", ex);

                    }
                }
            }
        }

        public void cambiotasa_fecha(string tasa, string tipotasa, string radicacion, DateTime fechaIni, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_radicacion.ParameterName = "p_radicacion";
                        p_radicacion.Value = radicacion;

                        DbParameter n_tasa = cmdTransaccionFactory.CreateParameter();
                        n_tasa.ParameterName = "n_tasa";
                        if (tasa == null)
                            n_tasa.Value = DBNull.Value;
                        else
                            n_tasa.Value = tasa;

                        DbParameter n_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        n_tipo_tasa.ParameterName = "n_tipo_tasa";
                        if (tipotasa == null)
                            n_tipo_tasa.Value = DBNull.Value;
                        else
                            n_tipo_tasa.Value = tipotasa;

                        DbParameter n_fecha_inicio = cmdTransaccionFactory.CreateParameter();
                        n_fecha_inicio.ParameterName = "n_fecha_inicio";
                        n_fecha_inicio.Value = fechaIni;


                        cmdTransaccionFactory.Parameters.Add(p_radicacion);
                        cmdTransaccionFactory.Parameters.Add(n_tasa);
                        cmdTransaccionFactory.Parameters.Add(n_tipo_tasa);
                        cmdTransaccionFactory.Parameters.Add(n_fecha_inicio);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CAMBIOTASA_FECHA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCredito, "Credito", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "CrearCredito", ex);

                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Credito de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Credito modificada</returns>
        public Credito ModificarCredito(Credito pCredito, Usuario pUsuario)
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

                        DbParameter p_fecha_inicial = cmdTransaccionFactory.CreateParameter();
                        p_fecha_inicial.ParameterName = "p_fecha_inicial";
                        p_fecha_inicial.Value = pCredito.fecha_inicio;

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pCredito.estado;
                        p_estado.DbType = DbType.AnsiString;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.Size = 1;

                        DbParameter p_fecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        p_fecha_desembolso.ParameterName = "p_fecha_desembolso";
                        p_fecha_desembolso.Value = pCredito.fecha_desembolso;

                        DbParameter p_Usuario = cmdTransaccionFactory.CreateParameter();
                        p_Usuario.ParameterName = "p_Usuario";
                        p_Usuario.Value = pUsuario.nombre;

                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_estado);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_inicial);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_desembolso);
                        cmdTransaccionFactory.Parameters.Add(p_Usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GENDOC_ACTESTADO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCredito, "Credito", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);

                        return pCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ModificarCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Credito de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Credito modificada</returns>
        public void ModificarFechaDesembolsoCredito(DateTime fechadesembolso, Credito pCredito, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            Configuracion conf = new Configuracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql1 = "";

                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql1 = "update credito set fecha_desembolso = To_Date('" + fechadesembolso.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql1 = "update credito set fecha_desembolso = '" + fechadesembolso.ToString(conf.ObtenerFormatoFecha()) + "' ";

                        string sql = sql1 + "  where numero_radicacion= " + pCredito.numero_credito;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ModificarFechaDesembolsoCredito", ex);

                    }
                }
            }
        }

        public List<Cliente> ListarCodeudores(Int64 numero_radicacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<Cliente> lstCodeudores = new List<Cliente>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from v_Codeudores where numero_radicacion = " + numero_radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cliente entidad = new Cliente();
                            if (resultado["cod_persona"] != DBNull.Value) entidad.IdCliente = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.TipoDocumento = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.NumeroDocumento = Convert.ToString(resultado["identificacion"]);
                            if (resultado["primer_nombre"] != DBNull.Value) entidad.PrimerNombre = Convert.ToString(resultado["primer_nombre"]);
                            if (resultado["segundo_nombre"] != DBNull.Value) entidad.SegundoNombre = Convert.ToString(resultado["segundo_nombre"]);
                            if (resultado["primer_apellido"] != DBNull.Value) entidad.PrimerApellido = Convert.ToString(resultado["primer_apellido"]);
                            if (resultado["segundo_apellido"] != DBNull.Value) entidad.SegundoApellido = Convert.ToString(resultado["segundo_apellido"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.Telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["barrio"] != DBNull.Value) entidad.Barrio = Convert.ToString(resultado["barrio"]);
                            if (resultado["email"] != DBNull.Value) entidad.Email = Convert.ToString(resultado["email"].ToString());
                            lstCodeudores.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCodeudores;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCodeudores", ex);
                        return null;
                    }
                }

            }
        }


        public Credito ModificarCreditoUlt(Credito pCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Credito creditoAnterior = ConsultarCredito(pCredito.numero_radicacion, pUsuario);

                        DbParameter P_NUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_RADICACION.ParameterName = "P_NUMERO_RADICACION";
                        P_NUMERO_RADICACION.Value = pCredito.numero_radicacion;
                        P_NUMERO_RADICACION.Direction = ParameterDirection.Input;

                        DbParameter P_FECHA_PROXIMO_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_PROXIMO_PAGO.ParameterName = "P_FECHA_PROXIMO_PAGO";
                        P_FECHA_PROXIMO_PAGO.Value = pCredito.fecha_prox_pago;
                        P_FECHA_PROXIMO_PAGO.Direction = ParameterDirection.Input;

                        DbParameter P_CUOTAS_PAGADAS = cmdTransaccionFactory.CreateParameter();
                        P_CUOTAS_PAGADAS.ParameterName = "P_CUOTAS_PAGADAS";
                        P_CUOTAS_PAGADAS.Value = pCredito.cuotas_pagadas;
                        P_CUOTAS_PAGADAS.Direction = ParameterDirection.Input;

                        DbParameter P_FECHA_VENCIMIENTO = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_VENCIMIENTO.ParameterName = "P_FECHA_VENCIMIENTO";
                        P_FECHA_VENCIMIENTO.Value = pCredito.fecha_vencimiento;
                        P_FECHA_VENCIMIENTO.Direction = ParameterDirection.Input;

                        DbParameter P_SALDO_CAPITAL = cmdTransaccionFactory.CreateParameter();
                        P_SALDO_CAPITAL.ParameterName = "P_SALDO_CAPITAL";
                        P_SALDO_CAPITAL.Value = pCredito.saldo_capital;
                        P_SALDO_CAPITAL.Direction = ParameterDirection.Input;

                        DbParameter P_VALOR_CUOTA = cmdTransaccionFactory.CreateParameter();
                        P_VALOR_CUOTA.ParameterName = "P_VALOR_CUOTA";
                        P_VALOR_CUOTA.Value = pCredito.valor_cuota;
                        P_VALOR_CUOTA.Direction = ParameterDirection.Input;

                        DbParameter P_DIAS_AJUSTE = cmdTransaccionFactory.CreateParameter();
                        P_DIAS_AJUSTE.ParameterName = "P_DIAS_AJUSTE";
                        P_DIAS_AJUSTE.Value = pCredito.dias_ajuste;
                        P_DIAS_AJUSTE.Direction = ParameterDirection.Input;

                        DbParameter P_FECHA_SOLICITUD = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_SOLICITUD.ParameterName = "P_FECHA_SOLICITUD";
                        if (pCredito.fecha_solicitud == null)
                            P_FECHA_SOLICITUD.Value = DBNull.Value;
                        else
                            P_FECHA_SOLICITUD.Value = pCredito.fecha_solicitud;
                        P_FECHA_SOLICITUD.Direction = ParameterDirection.Input;

                        DbParameter P_FECHA_APROBACION = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_APROBACION.ParameterName = "P_FECHA_APROBACION";
                        if (pCredito.fecha_aprobacion == null)
                            P_FECHA_APROBACION.Value = DBNull.Value;
                        else
                            P_FECHA_APROBACION.Value = pCredito.fecha_aprobacion;
                        P_FECHA_APROBACION.Direction = ParameterDirection.Input;

                        DbParameter P_FECHA_DESEMBOLSO = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_DESEMBOLSO.ParameterName = "P_FECHA_DESEMBOLSO";
                        if (pCredito.fecha_desembolso == null)
                            P_FECHA_DESEMBOLSO.Value = DBNull.Value;
                        else
                            P_FECHA_DESEMBOLSO.Value = pCredito.fecha_desembolso;
                        P_FECHA_DESEMBOLSO.Direction = ParameterDirection.Input;

                        DbParameter P_FECHA_INICIO = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_INICIO.ParameterName = "P_FECHA_INICIO";
                        if (pCredito.fecha_inicio == null)
                            P_FECHA_INICIO.Value = DBNull.Value;
                        else
                            P_FECHA_INICIO.Value = pCredito.fecha_inicio;
                        P_FECHA_INICIO.Direction = ParameterDirection.Input;

                        DbParameter P_USUARIO = cmdTransaccionFactory.CreateParameter();
                        P_USUARIO.ParameterName = "P_USUARIO";
                        P_USUARIO.Value = pUsuario.nombre;
                        P_USUARIO.Direction = ParameterDirection.Input;

                        DbParameter P_FECULTMOD = cmdTransaccionFactory.CreateParameter();
                        P_FECULTMOD.ParameterName = "P_FECULTMOD";
                        P_FECULTMOD.Value = DateTime.Now;
                        P_FECULTMOD.Direction = ParameterDirection.Input;

                        DbParameter P_FECHA_PRIMERPAGO = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_PRIMERPAGO.ParameterName = "P_FECHA_PRIMERPAGO";
                        if (pCredito.fecha_prim_pago == null)
                            P_FECHA_PRIMERPAGO.Value = DBNull.Value;
                        else
                            P_FECHA_PRIMERPAGO.Value = pCredito.fecha_prim_pago;
                        P_FECHA_PRIMERPAGO.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_PROXIMO_PAGO);
                        cmdTransaccionFactory.Parameters.Add(P_CUOTAS_PAGADAS);
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_VENCIMIENTO);
                        cmdTransaccionFactory.Parameters.Add(P_SALDO_CAPITAL);
                        cmdTransaccionFactory.Parameters.Add(P_VALOR_CUOTA);
                        cmdTransaccionFactory.Parameters.Add(P_DIAS_AJUSTE);
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_SOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_APROBACION);
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_DESEMBOLSO);
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_INICIO);
                        cmdTransaccionFactory.Parameters.Add(P_USUARIO);
                        cmdTransaccionFactory.Parameters.Add(P_FECULTMOD);
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_PRIMERPAGO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CREDITO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        DAauditoria.InsertarLog(pCredito, "CREDITO", pUsuario, Accion.Modificar.ToString(), TipoAuditoria.Creditos, "Modificacion de creditos con numero de radicacion " + pCredito.numero_radicacion, creditoAnterior); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);

                        return pCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ModificarCredito", ex);
                        return null;
                    }
                }
            }
        }


        public Credito ModificarCupoRotativo(Credito pCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Credito creditoAnterior = ConsultarCredito(pCredito.numero_radicacion, pUsuario);

                        DbParameter P_NUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_RADICACION.ParameterName = "P_NUMERO_RADICACION";
                        P_NUMERO_RADICACION.Value = pCredito.numero_radicacion;
                        P_NUMERO_RADICACION.Direction = ParameterDirection.Input;


                        DbParameter P_MONTO_APROBADO = cmdTransaccionFactory.CreateParameter();
                        P_MONTO_APROBADO.ParameterName = "P_MONTO_APROBADO";
                        P_MONTO_APROBADO.Value = pCredito.monto_aprobado;
                        P_MONTO_APROBADO.Direction = ParameterDirection.Input;


                        DbParameter P_USUARIO = cmdTransaccionFactory.CreateParameter();
                        P_USUARIO.ParameterName = "P_USUARIO";
                        P_USUARIO.Value = pUsuario.codusuario;
                        P_USUARIO.Direction = ParameterDirection.Input;

                        DbParameter P_FECULTMOD = cmdTransaccionFactory.CreateParameter();
                        P_FECULTMOD.ParameterName = "P_FECULTMOD";
                        P_FECULTMOD.Value = DateTime.Now;
                        P_FECULTMOD.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(P_MONTO_APROBADO);
                        cmdTransaccionFactory.Parameters.Add(P_USUARIO);
                        cmdTransaccionFactory.Parameters.Add(P_FECULTMOD);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ROTAT_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        DAauditoria.InsertarLog(pCredito, "CREDITO", pUsuario, Accion.Modificar.ToString(), TipoAuditoria.Creditos, "Modificacion de creditos con numero de radicacion " + pCredito.numero_radicacion, creditoAnterior); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);

                        return pCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ModificarCupoRotativo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Mètodo para realizar el llamado al PL del desembolso del crèdito
        /// </summary>
        /// <param name="pCredito"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Int64 DesembolsarCredito(Credito pCredito, ref decimal pMontoDesembolso, ref string perror, Usuario pUsuario)
        {
            perror = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.Value = pCredito.numero_radicacion;

                        DbParameter p_fecha_prim_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_prim_pago.ParameterName = "p_fecha_prim_pago";
                        if (pCredito.fecha_prim_pago == null)
                            p_fecha_prim_pago.Value = DBNull.Value;
                        else
                            p_fecha_prim_pago.Value = pCredito.fecha_prim_pago;
                        p_fecha_prim_pago.DbType = DbType.Date;

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pCredito.estado;
                        p_estado.DbType = DbType.AnsiString;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.Size = 1;

                        DbParameter p_fecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        p_fecha_desembolso.ParameterName = "p_fecha_desembolso";
                        p_fecha_desembolso.Value = pCredito.fecha_desembolso;

                        DbParameter p_Usuario = cmdTransaccionFactory.CreateParameter();
                        p_Usuario.ParameterName = "p_Usuario";
                        p_Usuario.Value = pUsuario.nombre;
                        p_estado.DbType = DbType.AnsiString;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.Size = 50;

                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        p_ip.Value = pUsuario.IP;

                        DbParameter p_Cod_Ope = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Ope.ParameterName = "p_cod_ope";
                        p_Cod_Ope.Value = pCredito.cod_ope;
                        p_Cod_Ope.Direction = ParameterDirection.Output;

                        DbParameter p_monto_desembolsado = cmdTransaccionFactory.CreateParameter();
                        p_monto_desembolsado.ParameterName = "p_monto_desembolsado";
                        p_monto_desembolsado.Value = pCredito.monto;
                        p_monto_desembolsado.Direction = ParameterDirection.Output;

                        DbParameter p_error = cmdTransaccionFactory.CreateParameter();
                        p_error.ParameterName = "p_error";
                        p_error.Value = "";
                        p_error.DbType = DbType.AnsiString;
                        p_error.Direction = ParameterDirection.Output;
                        p_error.Size = 500;

                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_estado);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_prim_pago);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_desembolso);
                        cmdTransaccionFactory.Parameters.Add(p_Usuario);
                        cmdTransaccionFactory.Parameters.Add(p_ip);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Ope);
                        cmdTransaccionFactory.Parameters.Add(p_monto_desembolsado);
                        cmdTransaccionFactory.Parameters.Add(p_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_DESEM_CREAR_INDIV";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (p_error.Value != DBNull.Value && p_error.Value != null)
                            perror = Convert.ToString(p_error.Value);
                        else
                            perror = "";
                        if (p_Cod_Ope.Value != DBNull.Value && p_error.Value != null)
                            pCredito.cod_ope = Convert.ToInt64(p_Cod_Ope.Value);
                        else
                            pCredito.cod_ope = 0;

                        pMontoDesembolso = p_monto_desembolsado.Value != DBNull.Value ? Convert.ToDecimal(p_monto_desembolsado.Value) : 0;
                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pCredito, "Credito", pUsuario, Accion.Modificar.ToString());

                        dbConnectionFactory.CerrarConexion(connection);

                        return pCredito.cod_ope;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ModificarCredito. " + perror, ex);
                        return 0;
                    }
                }
            }
        }


        /// <summary>
        /// Mètodo para realizar el llamado al PL del desembolso del crèdito
        /// </summary>
        /// <param name="pCredito"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Int64 DesembolsarCreditoMasivo(Credito pCredito, ref decimal pMontoDesembolso, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        p_ip.Value = pUsuario.IP;

                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.Value = pCredito.numero_radicacion;

                        DbParameter p_fecha_prim_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_prim_pago.ParameterName = "p_fecha_prim_pago";
                        if (pCredito.fecha_prim_pago == null)
                            p_fecha_prim_pago.Value = DBNull.Value;
                        else
                            p_fecha_prim_pago.Value = pCredito.fecha_prim_pago;
                        p_fecha_prim_pago.DbType = DbType.Date;

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pCredito.estado;
                        p_estado.DbType = DbType.AnsiString;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.Size = 1;

                        DbParameter p_fecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        p_fecha_desembolso.ParameterName = "p_fecha_desembolso";
                        p_fecha_desembolso.Value = pCredito.fecha_desembolso;

                        DbParameter p_Usuario = cmdTransaccionFactory.CreateParameter();
                        p_Usuario.ParameterName = "p_Usuario";
                        p_Usuario.Value = pUsuario.nombre;
                        p_estado.DbType = DbType.AnsiString;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.Size = 50;

                        DbParameter p_Cod_Ope = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Ope.ParameterName = "p_cod_ope";
                        p_Cod_Ope.Value = pCredito.cod_ope;
                        p_Cod_Ope.Direction = ParameterDirection.Output;

                        DbParameter p_monto_desembolsado = cmdTransaccionFactory.CreateParameter();
                        p_monto_desembolsado.ParameterName = "p_monto_desembolsado";
                        p_monto_desembolsado.Value = pCredito.monto;
                        p_monto_desembolsado.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_estado);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_prim_pago);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_desembolso);
                        cmdTransaccionFactory.Parameters.Add(p_Usuario);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Ope);
                        cmdTransaccionFactory.Parameters.Add(p_ip);
                        cmdTransaccionFactory.Parameters.Add(p_monto_desembolsado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_DESEM_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (p_Cod_Ope.Value != DBNull.Value && p_Cod_Ope.Value != null)
                            pCredito.cod_ope = Convert.ToInt64(p_Cod_Ope.Value);
                        else
                            pCredito.cod_ope = 0;

                        pMontoDesembolso = p_monto_desembolsado.Value != DBNull.Value ? Convert.ToDecimal(p_monto_desembolsado.Value) : 0;
                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pCredito, "Credito", pUsuario, Accion.Modificar.ToString());

                        dbConnectionFactory.CerrarConexion(connection);

                        return pCredito.cod_ope;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "DesembolsarCreditoMasivo", ex);
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// Guardar Datos del giro
        /// </summary>
        /// <param name="codperson"></param>
        /// <param name="fromasdesembolso"></param>
        /// <param name="fechas_desembolso"></param>
        /// <param name="NUMERO_RADICACION"></param>
        /// <param name="usuario"></param>
        /// <param name="monto"></param>
        /// <param name="numerocuenta"></param>
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

        /// <summary>
        /// Guardar los datos de la cuenta bancaria para giros
        /// </summary>
        /// <param name="codpersona"></param>
        /// <param name="numerocuenta"></param>
        /// <param name="tipocuenta"></param>
        /// <param name="cod_banco"></param>
        public void GuardarCuentaBancariaCliente(Int64 codpersona, string numerocuenta, Int64 tipocuenta, Int64 cod_banco, Usuario pUsuario)
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
                        pcodpersona.Value = codpersona;

                        DbParameter pnum_cuen = cmdTransaccionFactory.CreateParameter();
                        pnum_cuen.ParameterName = "pnum_cuen";
                        pnum_cuen.Value = numerocuenta;

                        DbParameter pcod_tipocta = cmdTransaccionFactory.CreateParameter();
                        pcod_tipocta.ParameterName = "pcod_tipocta";
                        pcod_tipocta.Value = tipocuenta;

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "pcod_banco";
                        pcod_banco.Value = cod_banco;

                        cmdTransaccionFactory.Parameters.Add(pcodpersona);
                        cmdTransaccionFactory.Parameters.Add(pnum_cuen);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipocta);
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_DESEM_CUENTA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "GuardarCuentaBancariaCliente", ex);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Credito de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Credito</param>
        /// <param name="idDeudor">identificador del deudor</param>
        public void EliminarCredito(Int64 pId, long idDeudor, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_numero_radicacion";
                        pNUMERO_RADICACION.Value = pId;

                        DbParameter paramIdDeudor = cmdTransaccionFactory.CreateParameter();
                        paramIdDeudor.ParameterName = "p_cod_persona";
                        paramIdDeudor.Value = idDeudor;

                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(paramIdDeudor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CREDITO_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        Credito pCredito = new Credito
                        {
                            numero_radicacion = pId,
                            cod_persona = idDeudor
                        };

                        DAauditoria.InsertarLog(pCredito, "CREDITO", pUsuario, Accion.Eliminar.ToString(), TipoAuditoria.Creditos, "Eliminacion de credito con numero de radicacion " + pCredito.numero_radicacion); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "EliminarCredito", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Credito de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Credito</param>
        /// <returns>Entidad Credito consultado</returns>
        public Credito ConsultarCredito(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select v.*,c.OBSERVACIONES 
                                        From v_creditos v                                       
                                        left join CONTROLCREDITOS c on v.numero_radicacion = c.NUMERO_RADICACION 
                                        where v.numero_radicacion = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["EDAD"] != DBNull.Value) entidad.edad = Convert.ToInt32(resultado["EDAD"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToDecimal(resultado["MONTO_APROBADO"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto_solicitado = Convert.ToDecimal(resultado["MONTO_SOLICITADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["NOMBRE_ESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["NOMBRE_ESTADO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["FECHA_PRIMER_PAGO"] != DBNull.Value) entidad.fecha_prim_pago = Convert.ToDateTime(resultado["FECHA_PRIMER_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_prox_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["CALCULO_ATR"] != DBNull.Value) entidad.calculo_atr = Convert.ToString(resultado["CALCULO_ATR"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt64(resultado["TIPO_TASA"]);
                            if (resultado["DESCRIPCION_TASA"] != DBNull.Value) entidad.desc_tasa = Convert.ToString(resultado["DESCRIPCION_TASA"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["COBRA_MORA"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["COD_CLASIFICA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["FECHASOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHASOLICITUD"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numero_obligacion = Convert.ToString(resultado["NUMEROSOLICITUD"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToString(resultado["COD_BANCO"]);
                            if (resultado["tipocuenta"] != DBNull.Value) entidad.tipocuenta = Convert.ToString(resultado["tipocuenta"]);
                            if (resultado["FORMA_DESEMBOLSO"] != DBNull.Value) entidad.forma_desembolso = Convert.ToString(resultado["FORMA_DESEMBOLSO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.tipo_credito = Convert.ToString(resultado["TIPO_LINEA"]);
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
                        BOExcepcion.Throw("CreditoData", "ConsultarCredito", ex);
                        return null;
                    }
                }
            }
        }



        public List<Credito> ListarCreditosActivos(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<Credito> lstentidad = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select v_creditos.*, v_creditos.nombre_estado as nomestado From v_creditos Where COD_DEUDOR = " + pId.ToString() + " And estado = 'C'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToDecimal(resultado["MONTO_APROBADO"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto_solicitado = Convert.ToDecimal(resultado["MONTO_SOLICITADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["FECHA_PRIMER_PAGO"] != DBNull.Value) entidad.fecha_prim_pago = Convert.ToDateTime(resultado["FECHA_PRIMER_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_prox_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fecha_desembolso_nullable = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["CALCULO_ATR"] != DBNull.Value) entidad.calculo_atr = Convert.ToString(resultado["CALCULO_ATR"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt64(resultado["TIPO_TASA"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["COBRA_MORA"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["COD_CLASIFICA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["FECHASOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHASOLICITUD"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numero_obligacion = Convert.ToString(resultado["NUMEROSOLICITUD"]);
                            if (resultado["TIPO_LIQU"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQU"]);
                            lstentidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstentidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarCredito", ex);
                        return null;
                    }
                }
            }
        }


        public List<Credito> ListarCarteraActiva(long codPersona, Usuario usuario)
        {
            DbDataReader resultado;
            List<Credito> lstentidad = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                        string sql = @"Select v_creditos.*,
                                    v_creditos.nombre_estado as nomestado,
                                    nvl(((SELECT Calcular_TotalAPagar(v_creditos.NUMERO_RADICACION, to_date('" + fecha + @"','dd/MM/yyyy')) FROM DUAL)-v_creditos.SALDO_CAPITAL), 0) as INTERES
                                    From v_creditos Where COD_DEUDOR = " + codPersona.ToString() + " And estado = 'C'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToDecimal(resultado["MONTO_APROBADO"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fecha_desembolso_nullable = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["INTERES"] != DBNull.Value) entidad.intcoriente = Convert.ToInt64(resultado["INTERES"]);

                            lstentidad.Add(entidad);
                        }

                        //Se comentareo consulta de servicios ya que solo se deben listar créditos

                        //sql = @"select ser.numero_servicio, ser.FECHA_APROBACION, ser.VALOR_TOTAL, ser.SALDO, ser.VALOR_CUOTA, lin.NOMBRE as NOMBRE_LINEA,
                        //nvl(((SELECT CALCULAR_TOTALAPAGAR_SERVICIO(ser.NUMERO_SERVICIO, to_date('" + fecha + @"', 'dd/MM/yyyy')) FROM DUAL)-ser.VALOR_TOTAL), 0) as INTERES
                        //from servicios ser
                        //join LINEASSERVICIOS lin on ser.COD_LINEA_SERVICIO = lin.COD_LINEA_SERVICIO
                        //where ser.cod_persona = " + codPersona.ToString() + " and ser.estado = 'C'";


                        //cmdTransaccionFactory.CommandText = sql;
                        //resultado = cmdTransaccionFactory.ExecuteReader();

                        //while (resultado.Read())
                        //{
                        //    Credito entidads = new Credito();

                        //    if (resultado["numero_servicio"] != DBNull.Value) entidads.numero_radicacion = Convert.ToInt64(resultado["numero_servicio"]);
                        //    if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidads.linea_credito = Convert.ToString(resultado["NOMBRE_LINEA"]);
                        //    if (resultado["VALOR_TOTAL"] != DBNull.Value) entidads.monto_aprobado = Convert.ToDecimal(resultado["VALOR_TOTAL"]);
                        //    if (resultado["FECHA_APROBACION"] != DBNull.Value) entidads.fecha_desembolso_nullable = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                        //    if (resultado["SALDO"] != DBNull.Value) entidads.saldo_capital = Convert.ToDecimal(resultado["SALDO"]);
                        //    if (resultado["VALOR_CUOTA"] != DBNull.Value) entidads.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                        //    if (resultado["INTERES"] != DBNull.Value) entidads.intcoriente = Convert.ToDecimal(resultado["INTERES"]);
                        //    lstentidad.Add(entidads);
                        //}

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstentidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCarteraActiva", ex);
                        return null;
                    }
                }
            }
        }


        public List<Credito> ConsultarCreditoTerminado(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<Credito> lstentidad = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select v_creditos.*, v_creditos.nombre_estado as nomestado From v_creditos where COD_DEUDOR = " + pId.ToString() + " and estado='T' Order by fecha_ultimo_pago desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToDecimal(resultado["MONTO_APROBADO"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto_solicitado = Convert.ToDecimal(resultado["MONTO_SOLICITADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["FECHA_PRIMER_PAGO"] != DBNull.Value) entidad.fecha_prim_pago = Convert.ToDateTime(resultado["FECHA_PRIMER_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_prox_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["CALCULO_ATR"] != DBNull.Value) entidad.calculo_atr = Convert.ToString(resultado["CALCULO_ATR"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt64(resultado["TIPO_TASA"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["COBRA_MORA"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["COD_CLASIFICA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["FECHASOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHASOLICITUD"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numero_obligacion = Convert.ToString(resultado["NUMEROSOLICITUD"]);
                            lstentidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstentidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarCreditoTerminado", ex);
                        return null;
                    }
                }
            }
        }

        public Credito ConsultarConsecutivo(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select v_creditos.* From v_creditos Where v_creditos.numero_radicacion = " + pId.ToString();


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarCredito", ex);
                        return null;
                    }
                }
            }
        }

        public List<AnalisisPromedio> ConsultarAnalisisPromedio(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<AnalisisPromedio> lstEntidad = new List<AnalisisPromedio>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select v.* from V_ANALISISPROMEDIOS v Where v.saldo != 0 and v.cod_persona = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AnalisisPromedio entidad = new AnalisisPromedio();
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["PRODUCTO"] != DBNull.Value) entidad.producto = Convert.ToString(resultado["PRODUCTO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["AFILIACION"] != DBNull.Value) entidad.afiliacion = Convert.ToInt64(resultado["AFILIACION"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO"]);
                            if (resultado["RECIPROCIDAD"] != DBNull.Value) entidad.reciprocidad = Convert.ToInt64(resultado["RECIPROCIDAD"]);
                            if (resultado["CUPO_DISPONIBLE"] != DBNull.Value) entidad.cupo_disponible = Convert.ToInt64(resultado["CUPO_DISPONIBLE"]);
                            lstEntidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarAnalisisPromedio", ex);
                        return null;
                    }
                }
            }
        }



        public List<AnalisisPromedio> ConsultarCalificacionHistorial(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<AnalisisPromedio> lstEntidad = new List<AnalisisPromedio>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;



                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "P_CLIENTE";
                        p_cod_persona.Value = Convert.ToInt64(pId);
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);


                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_HISTORIAL_CLIEN";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarDetalleMoraPersona", ex);
                        return null;
                    }
                };
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = @"select * from Historial_Calificacion";


                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AnalisisPromedio entidad = new AnalisisPromedio();
                            if (resultado["Ano"] != DBNull.Value) entidad.Año = Convert.ToString(resultado["Ano"]); else entidad.Año = "-";
                            if (resultado["Mes01"] != DBNull.Value) entidad.Ene = Convert.ToString(resultado["Mes01"]); else entidad.Ene = "-";
                            if (resultado["Mes02"] != DBNull.Value) entidad.Feb = Convert.ToString(resultado["Mes02"]); else entidad.Feb = "-";
                            if (resultado["Mes03"] != DBNull.Value) entidad.Mar = Convert.ToString(resultado["Mes03"]); else entidad.Mar = "-";
                            if (resultado["Mes04"] != DBNull.Value) entidad.Abr = Convert.ToString(resultado["Mes04"]); else entidad.Abr = "-";
                            if (resultado["Mes05"] != DBNull.Value) entidad.May = Convert.ToString(resultado["Mes05"]); else entidad.May = "-";
                            if (resultado["Mes06"] != DBNull.Value) entidad.Jun = Convert.ToString(resultado["Mes06"]); else entidad.Jun = "-";
                            if (resultado["Mes07"] != DBNull.Value) entidad.Jul = Convert.ToString(resultado["Mes07"]); else entidad.Jul = "-";
                            if (resultado["Mes08"] != DBNull.Value) entidad.Ago = Convert.ToString(resultado["Mes08"]); else entidad.Ago = "-";
                            if (resultado["Mes09"] != DBNull.Value) entidad.Sep = Convert.ToString(resultado["Mes09"]); else entidad.Sep = "-";
                            if (resultado["Mes10"] != DBNull.Value) entidad.Oct = Convert.ToString(resultado["Mes10"]); else entidad.Oct = "-";
                            if (resultado["Mes11"] != DBNull.Value) entidad.Nov = Convert.ToString(resultado["Mes11"]); else entidad.Nov = "-";
                            if (resultado["Mes12"] != DBNull.Value) entidad.Dic = Convert.ToString(resultado["Mes12"]); else entidad.Dic = "-";
                            lstEntidad.Add(entidad);
                        }


                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarAnalisisPromedio", ex);
                        return null;
                    }
                }
            }
        }


        public List<Credito> ListarCreditosPorFiltro(string filtroDefinido, string filtroGrilla, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCreditos = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(filtroDefinido) && !string.IsNullOrWhiteSpace(filtroGrilla))
                        {
                            if (filtroGrilla.StartsWith(" and "))
                            {
                                filtroGrilla = filtroGrilla.Remove(0, 4).Insert(0, " WHERE ");
                            }
                        }

                        string sql = @" Select p.cod_persona, c.valor_cuota, c.numero_cuotas, c.monto_solicitado, c.numero_radicacion, p.identificacion, p.nombreyapellido as nombres, p.cod_nomina , c.cod_linea_credito, p.cod_oficina, c.CUOTAS_PAGADAS, c.FECHA_PROXIMO_PAGO, est.descripcion as descripcion_estado, c.SALDO_CAPITAL,licre.NOMBRE as descripcion_linea,
                                        (select a.SNOMBRE1||' '||a.SAPELLIDO1 from asejecutivos a where a.ICODIGO=c.cod_asesor_com) AS Nombre_Asesor, 
                                        (select ofi.nombre from oficina ofi where p.cod_oficina=ofi.cod_oficina) as oficina  
                                        From v_persona p
                                        JOIN credito c on c.COD_DEUDOR = p.COD_PERSONA
                                        JOIN estado_credito est on est.estado = c.estado
                                        JOIN lineascredito licre on licre.cod_linea_credito = c.cod_linea_credito " + filtroDefinido + " " + filtroGrilla;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["Nombre_Asesor"] != DBNull.Value) entidad.NombreAsesor = Convert.ToString(resultado["Nombre_Asesor"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["MONTO_SOLICITADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_prox_pago_string = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]) != DateTime.MinValue ? (Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"])).ToShortDateString() : " ";
                            if (resultado["descripcion_estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["descripcion_estado"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["descripcion_linea"] != DBNull.Value) entidad.nom_linea_credito = Convert.ToString(resultado["descripcion_linea"]);

                            lstCreditos.Add(entidad);
                        }

                        return lstCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCreditosPorFiltro", ex);
                        return null;
                    }
                }
            }
        }

        public Credito ConsultarCreditoModSolicitud(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select v_creditos.*, solicitudcred.* From v_creditos Left Join solicitudcred On v_creditos.numero_radicacion = solicitudcred.numerosolicitud where v_creditos.numero_radicacion = " + pId.ToString();


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["FECHA_PRIMER_PAGO"] != DBNull.Value) entidad.fecha_prim_pago = Convert.ToDateTime(resultado["FECHA_PRIMER_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_prox_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["CALCULO_ATR"] != DBNull.Value) entidad.calculo_atr = Convert.ToString(resultado["CALCULO_ATR"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt64(resultado["TIPO_TASA"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["COBRA_MORA"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["COD_CLASIFICA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["FECHASOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHASOLICITUD"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numero_obligacion = Convert.ToString(resultado["NUMEROSOLICITUD"]);
                            if (resultado["EMPRESA_RECAUDO"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA_RECAUDO"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.descrpcion = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["OTROMEDIO"] != DBNull.Value) entidad.desc_tasa = Convert.ToString(resultado["OTROMEDIO"]);
                            if (resultado["IDPROVEEDOR"] != DBNull.Value) entidad.idenprov = Convert.ToString(resultado["IDPROVEEDOR"]);
                            if (resultado["CUOTASOLICITADA"] != DBNull.Value) entidad.cuota_solicitada = Convert.ToInt64(resultado["CUOTASOLICITADA"]);
                            if (resultado["DIAS_AJUSTE"] != DBNull.Value) entidad.dias_ajuste = Convert.ToInt64(resultado["DIAS_AJUSTE"]);
                            if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.Tipo_Linea = Convert.ToInt32(resultado["TIPO_LINEA"]);
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
                        BOExcepcion.Throw("CreditoData", "ConsultarCreditoModSolicitud", ex);
                        return null;
                    }
                }
            }
        }

        public Credito ConsultarCreditoModCupoRotativo(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select v_creditos.*, solicitudcred.* From v_creditos Left Join solicitudcred On v_creditos.numero_radicacion = solicitudcred.numerosolicitud where v_creditos.tipo_linea=2 and  v_creditos.numero_radicacion = " + pId.ToString();


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["FECHA_PRIMER_PAGO"] != DBNull.Value) entidad.fecha_prim_pago = Convert.ToDateTime(resultado["FECHA_PRIMER_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_prox_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["CALCULO_ATR"] != DBNull.Value) entidad.calculo_atr = Convert.ToString(resultado["CALCULO_ATR"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt64(resultado["TIPO_TASA"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["COBRA_MORA"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["COD_CLASIFICA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["FECHASOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHASOLICITUD"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numero_obligacion = Convert.ToString(resultado["NUMEROSOLICITUD"]);
                            if (resultado["EMPRESA_RECAUDO"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA_RECAUDO"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.descrpcion = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["OTROMEDIO"] != DBNull.Value) entidad.desc_tasa = Convert.ToString(resultado["OTROMEDIO"]);
                            if (resultado["IDPROVEEDOR"] != DBNull.Value) entidad.idenprov = Convert.ToString(resultado["IDPROVEEDOR"]);
                            if (resultado["CUOTASOLICITADA"] != DBNull.Value) entidad.cuota_solicitada = Convert.ToInt64(resultado["CUOTASOLICITADA"]);
                            if (resultado["DIAS_AJUSTE"] != DBNull.Value) entidad.dias_ajuste = Convert.ToInt64(resultado["DIAS_AJUSTE"]);
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
                        BOExcepcion.Throw("CreditoData", "ConsultarCreditoModCupoRotativo", ex);
                        return null;
                    }
                }
            }
        }


        public CreditoOrdenServicio ConsultarCreditoOrdenServicio(Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            CreditoOrdenServicio entidad = new CreditoOrdenServicio();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from Credito_Orden_Servicio where numero_radicacion = " + pNumeroRadicacion.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDPROVEEDOR"] != DBNull.Value) entidad.idproveedor = Convert.ToString(resultado["IDPROVEEDOR"]);
                            if (resultado["NOMPROVEEDOR"] != DBNull.Value) entidad.nomproveedor = Convert.ToString(resultado["NOMPROVEEDOR"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["NUMERO_PREIMPRESO"] != DBNull.Value) entidad.numero_preimpreso = Convert.ToInt64(resultado["NUMERO_PREIMPRESO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public CreditoOrdenServicio ConsultarCREDITO_OrdenServ(String pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado;
            CreditoOrdenServicio entidad = new CreditoOrdenServicio();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from Credito_Orden_Servicio " + pFiltro.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDORDENSERVICIO"] != DBNull.Value) entidad.idordenservicio = Convert.ToInt64(resultado["IDORDENSERVICIO"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDPROVEEDOR"] != DBNull.Value) entidad.idproveedor = Convert.ToString(resultado["IDPROVEEDOR"]);
                            if (resultado["NOMPROVEEDOR"] != DBNull.Value) entidad.nomproveedor = Convert.ToString(resultado["NOMPROVEEDOR"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["NUMERO_PREIMPRESO"] != DBNull.Value) entidad.numero_preimpreso = Convert.ToInt64(resultado["NUMERO_PREIMPRESO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public List<Credito> ConsultarCuotas(long radicacion, Usuario pUsuario)
        {
            DbDataReader resultado;

            List<Credito> lista = new List<Credito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select numero_radicacion, numerocuota, fechacuota, sum(valor) as valor from planpagos where numero_radicacion=" + radicacion.ToString() + " group by numero_radicacion, numerocuota, fechacuota  order by numero_radicacion,numerocuota";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();
                            if (resultado["numerocuota"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numerocuota"]);
                            if (resultado["fechacuota"] != DBNull.Value) entidad.fecha_corte_string = Convert.ToString(resultado["fechacuota"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToInt64(resultado["valor"]);
                            lista.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCredito(Credito pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from v_creditos " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["FECHASOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHASOLICITUD"]);
                            if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.tipo_credito = Convert.ToString(resultado["TIPO_LINEA"]);

                            if (entidad.cod_linea_credito != null && entidad.linea_credito != null)
                                entidad.cod_linea_credito = entidad.cod_linea_credito + " - " + entidad.linea_credito;
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }

        public List<Credito> ListarCreditoAsociados(Int64 pCodPersona, DateTime pFecha, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select c.numero_radicacion, c.fecha_solicitud, c.fecha_aprobacion, c.cod_deudor, c.cod_linea_credito, l.nombre as linea, c.saldo_Capital, c.monto_aprobado, 
                                        c.valor_cuota, Calcular_VrAPagar(c.numero_radicacion, TO_DATE('" + pFecha.ToShortDateString() + @"','" + conf.ObtenerFormatoFecha() + "')) as Valor_A_Pagar,Calcular_totalapagar(c.numero_radicacion,TO_DATE('" + pFecha.ToShortDateString() + @"','" + conf.ObtenerFormatoFecha() + @"')) as vrtotal, c.fecha_proximo_pago 
                                        ,CALCULAR_VR_CUOEXTRAS(c.numero_radicacion, TO_DATE('" + pFecha.ToShortDateString() + @"','" + conf.ObtenerFormatoFecha() + @"'))  as Valor_CE, c.Fecha_vencimiento
                                        From credito c inner join lineascredito l on l.cod_linea_Credito = c.cod_linea_credito where c.cod_deudor = " + pCodPersona.ToString() + " And c.saldo_capital != 0";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToInt64(resultado["VALOR_A_PAGAR"]);
                            if (resultado["VRTOTAL"] != DBNull.Value) entidad.total_a_pagar = Convert.ToDecimal(resultado["VRTOTAL"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_prox_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["Valor_CE"] != DBNull.Value) entidad.valor_CE = Convert.ToDecimal(resultado["Valor_CE"]);
                            
                            entidad.valor_a_pagar_CE = entidad.valor_a_pagar + entidad.valor_CE;
                            entidad.check = entidad.valor_a_pagar != 0 ? 1 : 0;
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }

        public List<LineasCredito> ListarAtributosFinanciados(Int64 pNumRadicacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstCredito = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select atricre.cod_atr,
                                                atr.nombre as descripcion_atributo,  
                                                atricre.calculo_atr as FormaCalculo,
                                                atricre.tasa,
                                                tas.cod_tipo_tasa  as tipotasa,
                                                hist.tipo_historico as tipohistorico,
                                                atricre.desviacion,
                                                atricre.cobra_mora 
                                                from ATRIBUTOSCREDITO atricre
                                                left join atributos atr on atr.cod_atr = atricre.cod_atr
                                                left join tipotasa tas on atricre.tipo_tasa = tas.cod_tipo_tasa
                                                left join tipotasahist hist on atricre.tipo_historico = hist.tipo_historico
                                                WHERE atricre.numero_radicacion = " + pNumRadicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["cod_atr"]);
                            if (resultado["descripcion_atributo"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion_atributo"]);
                            if (resultado["tipotasa"] != DBNull.Value) entidad.tipotasa = Convert.ToInt64(resultado["tipotasa"]);
                            if (resultado["tasa"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["tasa"]);
                            if (resultado["FormaCalculo"] != DBNull.Value) entidad.formacalculo = Convert.ToString(resultado["FormaCalculo"]);
                            if (resultado["desviacion"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["desviacion"]);
                            if (resultado["cobra_mora"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["cobra_mora"]);
                            if (resultado["tipohistorico"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["tipohistorico"]);

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarAtributosFinanciados", ex);
                        return null;
                    }
                }
            }
        }


        //Anderson Acuña  -- Reporte Creditos Desembolsados      
        public List<Credito> ListarCreditosDesembolsados(Credito pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (pCredito.check == 1)
                        {
                            sql = @"select DISTINCT T5.NOMBRE, t2.numero_radicacion, T1.identificacion, T1.primer_apellido, T3.NOMBRE AS LINEACRE,
                            T1.segundo_apellido, T1.primer_nombre, T1.segundo_nombre, T2.fecha_desembolso, T2.monto_desembolsado, T2.monto_aprobado
                            from persona T1 
                            inner join credito T2 on t2.cod_deudor = t1.cod_persona
                            inner join lineascredito T3 on T2.COD_LINEA_CREDITO = T3.COD_LINEA_CREDITO
                            inner join historico_cre T4 on T1.COD_PERSONA = T4.COD_CLIENTE
                            inner join oficina T5 on T2.COD_OFICINA = T5.COD_OFICINA 
                            where t2.monto_desembolsado > 0 and T2.estado IN ('C','T') " + filtro + " order by t2.fecha_desembolso desc";
                        }
                        else
                        {
                            sql = @"select DISTINCT T5.NOMBRE, t2.numero_radicacion, T1.identificacion, T1.primer_apellido, T3.NOMBRE AS LINEACRE,
                            T1.segundo_apellido, T1.primer_nombre, T1.segundo_nombre, T2.fecha_desembolso, T2.monto_desembolsado, T2.monto_aprobado
                            from persona T1 
                            inner join credito T2 on t2.cod_deudor = t1.cod_persona
                            inner join lineascredito T3 on T2.COD_LINEA_CREDITO = T3.COD_LINEA_CREDITO
                            inner join oficina T5 on T2.COD_OFICINA = T5.COD_OFICINA 
                            where t2.monto_desembolsado > 0 and T2.estado IN ('C','T') " + filtro + " order by t2.fecha_desembolso desc";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["LINEACRE"] != DBNull.Value) entidad.nom_linea_credito = Convert.ToString(resultado["LINEACRE"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["primer_apellido"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["primer_apellido"]);
                            if (resultado["segundo_apellido"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["segundo_apellido"]);
                            if (resultado["primer_nombre"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["primer_nombre"]);
                            if (resultado["segundo_nombre"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["segundo_nombre"]);
                            if (resultado["fecha_desembolso"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["fecha_desembolso"]);
                            if (resultado["monto_desembolsado"] != DBNull.Value) entidad.monto_desembolsado = Convert.ToInt64(resultado["monto_desembolsado"]);
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }

        //Anderson Acuña  -- Reporte Cartera     
        public List<Credito> ListarCartera(Credito pCredito, Usuario pUsuario, String filtro, String fechaAct)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT DISTINCT
                                    T9.NOMBRE as nom_oficina,
                                    t1.numero_radicacion,
                                    t2.nombre as nom_persona,
                                    T2.identificacion,
                                    t1.PAGARE as NUM_PAGARE,
                                    t1.fecha_aprobacion,
                                    t1.fecha_vencimiento,
                                    t1.saldo_capital,
                                    t1.fecha_ultimo_pago,
                                    t1.fecha_proximo_pago,
                                    CASE when Trunc(Sysdate-t1.Fecha_Proximo_Pago) < 0 then (Trunc(Sysdate-t1.Fecha_Proximo_Pago) * -1) else 0 end as dias_mora,
                                    t3.descripcion AS CLASIFICACION,
                                    T1.cod_linea_credito,
                                    T4.descripcion AS PERIODICIDAD,
                                    case t16.tipo_pago when 1 then 'Anticipado' when 2 then 'Vencido' else 'Vencido' end as modalidadpago_int, --- de las lineas de credito tabla
                                    t7.descripcion as tipo_garantia,
                                    T10.COD_CATEGORIA,
                                    nvl(t11.porc_provision, 0) as PROVISION,--%PROVISION -- tabla provision 
                                    nvl(t11.valor_provision, 0) as PROVISIONCAPITAL,--PROVISIONCAPITAL tabla provision
                                    CASE T13.CALCULO_ATR
                                    WHEN '1' THEN 'Tasa Fija'
                                    when '2' then 'Ponderada' 
                                    WHEN '3' THEN 'Tasa Historico' 
                                    WHEN '4' THEN 'Promedio Historico' 
                                    WHEN '5' THEN 'Tasa Variable' END AS TIPO_TASA,
                                    t1.tasa as tasa_int_corriente,--TASA INT CORRIENTE //
                                    T14.nombre AS FORMATO_DE_LA_TASA,--FORMATO DE LA TASA
                                    nvl(t15.dias_para_causar,0) AS dias_causados, --DIAS CAUSADOS
                                    nvl(t15.saldo_causado,0) as Interes_Causado,--INTERES CORRIENTE CAUSADOS 
                                    nvl(pr.valor_provision, 0) AS provision_interes,
                                    nvl(t15.saldo_orden,0) AS interes_orden, --INTERES DE ORDEN// tabla causacion
                                    nvl(t15.TASA,0) AS tasa_int_mora,--TASA INTERES MORA/tabla causacion
                                    T14.nombre AS formato_tasa_mora, --FORMATO DE LA TASA DE MORA//NI IDEA
                                    nvl(sum(t5.saldo),0) as aportes,
                                    t1.cod_deudor AS cod_cliente,
                                    t1.valor_cuota,
                                    t1.OTROS_SALDOS,
                                    0 as provision_otros, --PROVISION OTROS
                                    nvl(t2.TELEFONO,t2.CELULAR) as telefono,
                                    T1.FORMA_PAGO
                                    FROM credito T1 
                                    INNER JOIN v_Persona t2 on T1.cod_deudor = T2.cod_persona 
                                    INNER JOIN clasificacion T3 ON T1.cod_clasifica = T3.cod_clasifica
                                    INNER JOIN periodicidad T4 ON T1.cod_periodicidad = T4.cod_periodicidad
                                    left outer join Aporte t5 on t2.cod_persona = t5.cod_persona and t5.estado = 1
                                    left outer join garantias T6 on t1.NUMERO_RADICACION = T6.NUMERO_RADICACION
                                    LEFT join tipo_garantia T7 on T6.tipo_garantia = T7.COD_TIPO_GAR
                                    INNER JOIN oficina T9 ON T1.COD_OFICINA = T9.COD_OFICINA 
                                    LEFT JOIN historico_cre T10 ON T1.numero_radicacion = T10.numero_radicacion
                                    left outer join provision T11 on T1.NUMERO_RADICACION = T11.NUMERO_RADICACION
                                    LEFT outer JOIN provision pr ON t1.numero_radicacion = pr.numero_radicacion 
                                    left outer join provision po on t1.numero_radicacion = po.numero_radicacion
                                    LEFT JOIN LINEASCREDITO t12 ON t1.COD_LINEA_CREDITO = T12.COD_LINEA_CREDITO
                                    LEFT outer JOIN ATRIBUTOSLINEA T13 ON t12.COD_LINEA_CREDITO = T13.COD_LINEA_CREDITO 
                                    LEFT JOIN tipotasa T14 ON t13.tipo_tasa = t14.cod_tipo_tasa
                                    LEFT outer JOIN causacion t15 ON t1.numero_radicacion = t15.numero_radicacion 
                                    LEFT join tipoliquidacion t16 on t1.TIPO_LIQUIDACION = t16.tipo_liquidacion 
                                    WHERE
                                    --t15.fecha_corte = (select max(x.fecha) from cierea x where x.tipo = 'U' and x.estado = 'D' and x.fecha <= to_date('" + fechaAct + @"','dd/MM/yyyy'))
                                     t10.fecha_historico = (SELECT MAX(fecha) FROM cierea WHERE tipo = 'R' AND estado = 'D' AND fecha <= to_date('" + fechaAct + @"','dd/MM/yyyy'))
                                    and t15.FECHA_CORTE = (SELECT MAX(st15.FECHA_CORTE) FROM causacion st15 where t15.NUMERO_RADICACION = st15.NUMERO_RADICACION)
                                    and t11.cod_atr = 1  AND T11.FECHA_CORTE = (select max(ST11.FECHA_CORTE) from provision ST11 where T11.NUMERO_RADICACION = ST11.NUMERO_RADICACION)
                                    AND pr.cod_atr = 2 AND pr.FECHA_CORTE = (select max(st20.FECHA_CORTE) from provision st20 where pr.NUMERO_RADICACION = st20.NUMERO_RADICACION)
                                    --and po.cod_atr not in(1,2) AND po.FECHA_CORTE = (select max(ST21.FECHA_CORTE) from provision ST21 where po.NUMERO_RADICACION = ST21.NUMERO_RADICACION)
                                    --and T10.FECHA_HISTORICO = (select max(FECHA_HISTORICO) from historico_cre sT10 where T10.NUMERO_RADICACION = sT10.NUMERO_RADICACION)
                                    " + filtro + @"
                                    GROUP BY T9.NOMBRE,
                                    t1.numero_radicacion,
                                    t2.nombre,
                                    T2.identificacion,
                                    t1.PAGARE,
                                    t1.fecha_aprobacion,
                                    t1.fecha_vencimiento,
                                    t1.saldo_capital,
                                    t1.fecha_ultimo_pago,
                                    t1.fecha_proximo_pago,
                                    CASE when Trunc(Sysdate-t1.Fecha_Proximo_Pago) < 0 then (Trunc(Sysdate-t1.Fecha_Proximo_Pago) * -1) else 0 end,
                                    t3.descripcion,
                                    T1.cod_linea_credito,
                                    T4.descripcion,
                                    case t16.tipo_pago when 1 then 'Anticipado' when 2 then 'Vencido' else 'Vencido' end, --- de las lineas de credito tabla
                                    t7.descripcion,
                                    T10.COD_CATEGORIA,
                                    nvl(t11.porc_provision, 0),--%PROVISION -- tabla provision 
                                    nvl(t11.valor_provision, 0),--PROVISIONCAPITAL tabla provision
                                    CASE T13.CALCULO_ATR
                                    WHEN '1' THEN 'Tasa Fija'
                                    when '2' then 'Ponderada' 
                                    WHEN '3' THEN 'Tasa Historico' 
                                    WHEN '4' THEN 'Promedio Historico' 
                                    WHEN '5' THEN 'Tasa Variable' END,
                                    t1.tasa,--TASA INT CORRIENTE //
                                    T14.nombre,--FORMATO DE LA TASA
                                    nvl(t15.dias_para_causar,0), --DIAS CAUSADOS
                                    nvl(t15.saldo_causado,0),--INTERES CORRIENTE CAUSADOS 
                                    nvl(pr.valor_provision, 0),
                                    nvl(t15.saldo_orden,0), --INTERES DE ORDEN// tabla causacion
                                    nvl(t15.TASA,0),--TASA INTERES MORA/tabla causacion
                                    T14.nombre, --FORMATO DE LA TASA DE MORA//NI IDEA
                                    t1.cod_deudor,
                                    t1.valor_cuota,
                                    t1.OTROS_SALDOS,
                                    nvl(t2.TELEFONO,t2.CELULAR),
                                    T1.FORMA_PAGO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["nom_oficina"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nom_oficina"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["nom_persona"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["nom_persona"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["NUM_PAGARE"] != DBNull.Value) entidad.num_pagare = Convert.ToString(resultado["NUM_PAGARE"]);
                            if (resultado["fecha_aprobacion"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["fecha_aprobacion"]);
                            if (resultado["fecha_vencimiento"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["fecha_vencimiento"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["saldo_capital"]);
                            if (resultado["fecha_ultimo_pago"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["fecha_ultimo_pago"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_prox_pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["dias_mora"] != DBNull.Value) entidad.dias_mora = Convert.ToInt32(resultado["dias_mora"]);
                            if (resultado["CLASIFICACION"] != DBNull.Value) entidad.clasificacion = Convert.ToString(resultado["CLASIFICACION"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["modalidadpago_int"] != DBNull.Value) entidad.modalidad_pag_intereses = Convert.ToString(resultado["modalidadpago_int"]);
                            if (resultado["tipo_garantia"] != DBNull.Value) entidad.tipo_garantia = Convert.ToString(resultado["tipo_garantia"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["PROVISION"] != DBNull.Value) entidad.por_provision = Convert.ToDecimal(resultado["PROVISION"]);
                            if (resultado["PROVISIONCAPITAL"] != DBNull.Value) entidad.provision_capital = Convert.ToDecimal(resultado["PROVISIONCAPITAL"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.desc_tasa = Convert.ToString(resultado["TIPO_TASA"]);
                            if (resultado["tasa_int_corriente"] != DBNull.Value) entidad.tasa_int_corr = Convert.ToDecimal(resultado["tasa_int_corriente"]);
                            if (resultado["FORMATO_DE_LA_TASA"] != DBNull.Value) entidad.formato_tasa_int = Convert.ToString(resultado["FORMATO_DE_LA_TASA"]);
                            if (resultado["dias_causados"] != DBNull.Value) entidad.dias_causados = Convert.ToInt32(resultado["dias_causados"]);
                            if (resultado["Interes_Causado"] != DBNull.Value) entidad.int_cte_causado = Convert.ToDecimal(resultado["Interes_Causado"]);
                            if (resultado["provision_interes"] != DBNull.Value) entidad.provisio_int_cte = Convert.ToDecimal(resultado["provision_interes"]);
                            if (resultado["interes_orden"] != DBNull.Value) entidad.int_orden = Convert.ToDecimal(resultado["interes_orden"]);
                            if (resultado["tasa_int_mora"] != DBNull.Value) entidad.tasa_int_mora = Convert.ToDecimal(resultado["tasa_int_mora"]);
                            if (resultado["formato_tasa_mora"] != DBNull.Value) entidad.formato_tasa_int_mor = Convert.ToString(resultado["formato_tasa_mora"]);
                            if (resultado["aportes"] != DBNull.Value) entidad.aportes = Convert.ToDecimal(resultado["aportes"]);
                            if (resultado["cod_cliente"] != DBNull.Value) entidad.cod_cli = Convert.ToInt32(resultado["cod_cliente"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.valorCap = Convert.ToDecimal(resultado["valor_cuota"]);
                            if (resultado["OTROS_SALDOS"] != DBNull.Value) entidad.otros_cobros = Convert.ToDecimal(resultado["OTROS_SALDOS"]);
                            if (resultado["provision_otros"] != DBNull.Value) entidad.provision_otros = Convert.ToDecimal(resultado["provision_otros"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.telefonos = Convert.ToString(resultado["telefono"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCartera", ex);
                        return null;
                    }
                }
            }
        }


        public AtributosCredito ModificarAtributosFinanciados(AtributosCredito pLineasCredito, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pLineasCredito.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pLineasCredito.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pcalculo_atr = cmdTransaccionFactory.CreateParameter();
                        pcalculo_atr.ParameterName = "p_calculo_atr";
                        if (pLineasCredito.calculo_atr == null)
                            pcalculo_atr.Value = DBNull.Value;
                        else
                            pcalculo_atr.Value = pLineasCredito.calculo_atr;
                        pcalculo_atr.Direction = ParameterDirection.Input;
                        pcalculo_atr.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcalculo_atr);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pLineasCredito.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pLineasCredito.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pLineasCredito.desviacion == null)
                            pdesviacion.Value = DBNull.Value;
                        else
                            pdesviacion.Value = pLineasCredito.desviacion;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter ptipo_tasa = cmdTransaccionFactory.CreateParameter();
                        ptipo_tasa.ParameterName = "p_tipo_tasa";
                        if (pLineasCredito.tipo_tasa == null)
                            ptipo_tasa.Value = DBNull.Value;
                        else
                            ptipo_tasa.Value = pLineasCredito.tipo_tasa;
                        ptipo_tasa.Direction = ParameterDirection.Input;
                        ptipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tasa);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        if (pLineasCredito.tasa == null)
                            ptasa.Value = DBNull.Value;
                        else
                            ptasa.Value = pLineasCredito.tasa;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter pcobra_mora = cmdTransaccionFactory.CreateParameter();
                        pcobra_mora.ParameterName = "p_cobra_mora";
                        if (pLineasCredito.cobra_mora == null)
                            pcobra_mora.Value = DBNull.Value;
                        else
                            pcobra_mora.Value = pLineasCredito.cobra_mora;
                        pcobra_mora.Direction = ParameterDirection.Input;
                        pcobra_mora.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_mora);

                        DbParameter psaldo_atributo = cmdTransaccionFactory.CreateParameter();
                        psaldo_atributo.ParameterName = "p_saldo_atributo";
                        if (pLineasCredito.saldo_atributo == null)
                            psaldo_atributo.Value = DBNull.Value;
                        else
                            psaldo_atributo.Value = pLineasCredito.saldo_atributo;
                        psaldo_atributo.Direction = ParameterDirection.Input;
                        psaldo_atributo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_atributo);

                        DbParameter pcausado_atributo = cmdTransaccionFactory.CreateParameter();
                        pcausado_atributo.ParameterName = "p_causado_atributo";
                        if (pLineasCredito.causado_atributo == null)
                            pcausado_atributo.Value = DBNull.Value;
                        else
                            pcausado_atributo.Value = pLineasCredito.causado_atributo;
                        pcausado_atributo.Direction = ParameterDirection.Input;
                        pcausado_atributo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcausado_atributo);

                        DbParameter porden_atributo = cmdTransaccionFactory.CreateParameter();
                        porden_atributo.ParameterName = "p_orden_atributo";
                        if (pLineasCredito.orden_atributo == null)
                            porden_atributo.Value = DBNull.Value;
                        else
                            porden_atributo.Value = pLineasCredito.orden_atributo;
                        porden_atributo.Direction = ParameterDirection.Input;
                        porden_atributo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(porden_atributo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ATRIBUTOSC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLineasCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ModificarAtributosFinanciados", ex);
                        return null;
                    }
                }
            }
        }

        public Credito MODIFICARcredito(Credito pCREDITO, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pCREDITO.fecha_consulta_dat;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pidcontrol = cmdTransaccionFactory.CreateParameter();
                        pidcontrol.ParameterName = "PNUMERO_RADICACION";
                        pidcontrol.Value = pCREDITO.numero_radicacion;
                        pidcontrol.Direction = ParameterDirection.Input;
                        pidcontrol.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidcontrol);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "PTIPO";
                        ptipo.Value = pCREDITO.tipo_refinancia;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "POBSERVACIONES";
                        pobservaciones.Value = pCREDITO.observacion;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "PCODUSUARIO";
                        pcod_usuario.Value = vUsuario.codusuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CANCELAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pCREDITO, "CREDITO", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.Creditos, "Cancelacion de credito con numero de radicacion " + pCREDITO.numero_radicacion); //REGISTRO DE AUDITORIA

                        return pCREDITO;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CREDITOData", "Modificarcredito", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditoDocumRequeridos(Credito pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from v_creditos " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["FECHASOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHASOLICITUD"]);
                            if (resultado["numerosolicitud"] != DBNull.Value) entidad.numero_obligacion = Convert.ToString(resultado["numerosolicitud"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aproba = Convert.ToDateTime(resultado["FECHA_APROBACION"]);


                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCreditoDocumRequeridos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Credito de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Credito</param>
        /// <returns>Entidad Credito consultado</returns>
        public Credito ConsultarCreditoAsesor(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.*, L.NOMBRE AS LINEA, M.DESCRIPCION AS PERIODICIDAD, A.TASA AS TASA_ATRIB,
                                    Calcular_VraGirarAnalisis(C.NUMERO_RADICACION) valor_girar, (CASE WHEN C.Forma_Pago = 'C' OR C.Forma_Pago = '1' THEN 'Caja' ELSE 'Nomina' END) NOMFORMA_PAGO,
                                    P.IDENTIFICACION, ROUND((MONTHS_BETWEEN(SYSDATE, P.FECHANACIMIENTO) / 12),0) AS edad, E.NOM_EMPRESA, S.CONCEPTO, s.ReqAfiancol, D.DESCRIPCION DESTINACION_NOM
                                    FROM CREDITO C LEFT JOIN SOLICITUDCRED S ON C.NUMERO_OBLIGACION = S.NUMEROSOLICITUD                                    
                                    INNER JOIN PERSONA P ON C.COD_DEUDOR = P.COD_PERSONA
                                    INNER JOIN LINEASCREDITO L ON c.COD_LINEA_CREDITO = L.COD_LINEA_CREDITO
                                    INNER JOIN PERIODICIDAD M ON c.COD_PERIODICIDAD = M.COD_PERIODICIDAD
                                    LEFT JOIN EMPRESA_RECAUDO E ON C.COD_EMPRESA = E.COD_EMPRESA
                                    LEFT JOIN DESTINACION D ON D.COD_DESTINO = C.DESTINACION
                                    LEFT JOIN ATRIBUTOSCREDITO A On C.NUMERO_RADICACION = A.NUMERO_RADICACION And A.COD_ATR = 2
                                    where c.NUMERO_RADICACION = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NUMERO_OBLIGACION"] != DBNull.Value) entidad.numero_obligacion = Convert.ToString(resultado["NUMERO_OBLIGACION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.codigo_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["MONTO_SOLICITADO"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["FECHA_PRIMERPAGO"] != DBNull.Value) entidad.fecha_prim_pago = Convert.ToDateTime(resultado["FECHA_PRIMERPAGO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_prox_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["COD_CLASIFICA"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["OTROS_SALDOS"] != DBNull.Value) entidad.otros_saldos = Convert.ToInt64(resultado["OTROS_SALDOS"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.CodigoAsesor = Convert.ToInt64(resultado["COD_ASESOR_COM"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["UNIVERSIDAD"] != DBNull.Value) entidad.universidad = Convert.ToString(resultado["UNIVERSIDAD"]);
                            if (resultado["SEMESTRE"] != DBNull.Value) entidad.semestre = Convert.ToString(resultado["SEMESTRE"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["TASA_ATRIB"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA_ATRIB"]);
                            if (resultado["VALOR_GIRAR"] != DBNull.Value) entidad.monto_giro = Convert.ToDecimal(resultado["VALOR_GIRAR"]);
                            if (resultado["NOMFORMA_PAGO"] != DBNull.Value) entidad.nomforma_pago = Convert.ToString(resultado["NOMFORMA_PAGO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["EDAD"] != DBNull.Value) entidad.edad = Convert.ToInt32(resultado["EDAD"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["ReqAfiancol"] != DBNull.Value) entidad.ReqAfiancol = Convert.ToInt32(resultado["ReqAfiancol"]);
                            if (resultado["DESTINACION_NOM"] != DBNull.Value) entidad.NombreDestinacion = Convert.ToString(resultado["DESTINACION_NOM"]);
                            if (resultado["REESTRUCTURADO"] != DBNull.Value) entidad.Reestructurado = Convert.ToInt32(resultado["REESTRUCTURADO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarCreditoAsesor", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un dato de la tabla general para Habeas Data
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public Credito ConsultarParametroHabeas(Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=1050";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.dias_mora_habeas = Convert.ToInt64(resultado["valor"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarParametroHabeas", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un dato de la tabla general para Cobro Juridico
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public Credito ConsultarParametroCobroJuridico(Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=1052";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.dias_mora_habeas = Convert.ToInt64(resultado["valor"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarParametroCobroJuridico", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un dato de la tabla general para Cobro PreJuridico
        /// </summary>
        /// <param name="pId">identificador de General</param>
        /// <returns>Parametro consultada</returns>
        public Credito ConsultarParametroCobroPreJuridico(Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select valor FROM GENERAL WHERE CODIGO=1051";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.dias_mora_habeas = Convert.ToInt64(resultado["valor"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ConsultarParametroCobroPreJuridico", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditoAsesor(Credito pCredito, Usuario pUsuario, String filtro, String orden)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from v_informe_creditos " + filtro + orden;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();
                            if (resultado["idinforme"] != DBNull.Value) entidad.idinforme = Convert.ToInt64(resultado["idinforme"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.codigo_cliente = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["nombre_linea"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["nombre_linea"]);
                            if (resultado["fecha_solicitud"] != DBNull.Value) entidad.fecha_solicitud_string = Convert.ToDateTime(resultado["fecha_solicitud"]).ToString("dd/MM/yyyy");
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["saldo_capital"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["otros_saldos"] != DBNull.Value) entidad.otros_saldos = Convert.ToInt64(resultado["otros_saldos"]);
                            if (resultado["plazo"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["plazo"]);
                            if (resultado["cuotas_pagadas"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["cuotas_pagadas"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_prox_pago_string = Convert.ToDateTime(resultado["fecha_proximo_pago"]).ToString("dd/MM/yyyy");
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.codigo_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["oficina"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["oficina"]);
                            if (resultado["calificacion_promedio"] != DBNull.Value) entidad.calificacion_promedio = Convert.ToInt64(resultado["calificacion_promedio"]);
                            if (resultado["calificacion_cliente"] != DBNull.Value) entidad.calificacion_cliente = Convert.ToInt64(resultado["calificacion_cliente"]);
                            if (resultado["porc_renovacion_cuotas"] != DBNull.Value) entidad.porc_renovacion_cuotas = Convert.ToInt64(resultado["porc_renovacion_cuotas"]);
                            if (resultado["porc_renovacion_montos"] != DBNull.Value) entidad.porc_renovacion_montos = Convert.ToInt64(resultado["porc_renovacion_montos"]);
                            if (resultado["dias_mora"] != DBNull.Value) entidad.dias_mora = Convert.ToInt64(resultado["dias_mora"]);
                            if (resultado["saldo_mora"] != DBNull.Value) entidad.saldo_mora = Convert.ToInt64(resultado["saldo_mora"]);
                            if (resultado["saldo_atributos_mora"] != DBNull.Value) entidad.saldo_atributos_mora = Convert.ToInt64(resultado["saldo_atributos_mora"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["estado"]);
                            if (resultado["estado_juridico"] != DBNull.Value) entidad.estado_juridico = Convert.ToString(resultado["estado_juridico"]);
                            if (resultado["fecha_corte"] != DBNull.Value) entidad.fecha_corte_string = Convert.ToDateTime(resultado["fecha_corte"]).ToString("dd/MM/yyyy");
                            if (resultado["cod_zona"] != DBNull.Value) entidad.zona = Convert.ToInt64(resultado["cod_zona"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCreditoAsesor", ex);
                        return null;
                    }
                }
            }
        }

        public List<Credito> Consultardescuentos(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from tipo_tran where tipo_caja = 0 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descrpcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_movimiento = Convert.ToInt64(resultado["TIPO_MOV"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["TIPO_TRAN"]);


                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCreditoAsesor", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Credito de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Credito</param>
        /// <returns>Entidad Credito consultado</returns>
        public Credito ConsultarCreditoPorObligacion(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from credito where numero_obligacion=" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_SOLICITADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);

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
                        BOExcepcion.Throw("CreditoData", "ConsultarCredito", ex);
                        return null;
                    }
                }
            }
        }

        public Credito ConsultarCreditoSolicitud(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from solicitudcred where numerosolicitud = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMEROSOLICITUD"]);
                            if (resultado["MONTOSOLICITADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTOSOLICITADO"]);
                            if (resultado["PLAZOSOLICITADO"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt64(resultado["PLAZOSOLICITADO"]);
                            if (resultado["CUOTASOLICITADA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["CUOTASOLICITADA"]);

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
                        BOExcepcion.Throw("CreditoData", "ConsultarCreditoSolicitud", ex);
                        return null;
                    }
                }
            }
        }

        public Credito consultarinterescredito(Int64 pnumero_radicacion, DateTime pfecha_pago, Usuario pUsuario, long numeroRadicacion)
        {
            Credito entidad = new Credito();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "P_NUMRADICACION";
                        p_numero_radicacion.Value = pnumero_radicacion;

                        DbParameter P_NUMRADICACION = cmdTransaccionFactory.CreateParameter();
                        P_NUMRADICACION.ParameterName = "P_NUMRADICACIONPRIN";
                        P_NUMRADICACION.Value = numeroRadicacion;

                        DbParameter p_fecha_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_pago.ParameterName = "P_FECHAPAGO";
                        p_fecha_pago.DbType = DbType.DateTime;
                        p_fecha_pago.Value = pfecha_pago;

                        DbParameter capital = cmdTransaccionFactory.CreateParameter();
                        capital.ParameterName = "P_CAPITAL";
                        capital.Direction = ParameterDirection.Output;
                        capital.Value = entidad.capital;

                        DbParameter intcoriente = cmdTransaccionFactory.CreateParameter();
                        intcoriente.ParameterName = "P_INTCTE";
                        intcoriente.Direction = ParameterDirection.Output;
                        intcoriente.Value = 0;

                        DbParameter interesmora = cmdTransaccionFactory.CreateParameter();
                        interesmora.ParameterName = "P_INTMORA";
                        interesmora.Direction = ParameterDirection.Output;
                        interesmora.Value = 0;

                        DbParameter otros = cmdTransaccionFactory.CreateParameter();
                        otros.ParameterName = "P_OTROS";
                        otros.Direction = ParameterDirection.Output;
                        otros.Value = 0;

                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(P_NUMRADICACION);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_pago);
                        cmdTransaccionFactory.Parameters.Add(capital);
                        cmdTransaccionFactory.Parameters.Add(intcoriente);
                        cmdTransaccionFactory.Parameters.Add(interesmora);
                        cmdTransaccionFactory.Parameters.Add(otros);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DISTRIBUIRCRE";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        if (capital.Value != null)
                            entidad.saldo_capital = Convert.ToDecimal(capital.Value.ToString());
                        if (intcoriente.Value != null)
                            entidad.intcoriente = Convert.ToDecimal(intcoriente.Value.ToString());
                        if (interesmora.Value != null)
                            entidad.interesmora = Convert.ToDecimal(interesmora.Value.ToString());
                        if (otros.Value != null)
                            entidad.otros = Convert.ToDecimal(otros.Value.ToString());

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "consultarinterescredito", ex);
                        return null;
                    }
                }
            }
        }

        public Decimal AmortizarCredito(Int64 pnumero_radicacion, Int64 ptipo_pago, DateTime pfecha_pago, Usuario pUsuario)
        {
            Decimal valor_apagar = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "n_radic";
                        p_numero_radicacion.Value = pnumero_radicacion;

                        DbParameter p_fecha_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_pago.ParameterName = "f_fecha_pago";
                        p_fecha_pago.DbType = DbType.DateTime;
                        p_fecha_pago.Value = pfecha_pago;

                        DbParameter p_valor_apagar = cmdTransaccionFactory.CreateParameter();
                        p_valor_apagar.ParameterName = "rn_valor_cobrado";
                        p_valor_apagar.Direction = ParameterDirection.InputOutput;
                        p_valor_apagar.Value = valor_apagar;

                        DbParameter p_error = cmdTransaccionFactory.CreateParameter();
                        p_error.ParameterName = "n_error";
                        p_error.Direction = ParameterDirection.InputOutput;
                        p_error.Value = 0;

                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_pago);
                        cmdTransaccionFactory.Parameters.Add(p_valor_apagar);
                        cmdTransaccionFactory.Parameters.Add(p_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (ptipo_pago == 2)
                            cmdTransaccionFactory.CommandText = "CLSCREDITO.CALCULAR_TOTAL";
                        if (ptipo_pago == 11)
                            cmdTransaccionFactory.CommandText = "CLSCREDITO.CALCULAR_CUOEXT";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        valor_apagar = Convert.ToDecimal(p_valor_apagar.Value.ToString());
                        dbConnectionFactory.CerrarConexion(connection);

                        return valor_apagar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "AmortizarCredito", ex);
                        return 0;
                    }
                }
            }
        }


        public Decimal AmortizarCreditoNumCuotas(Int64 pnumero_radicacion, DateTime pfecha_pago, int pnum_cuotas, Usuario pUsuario)
        {
            Decimal valor_apagar = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "n_radic";
                        p_numero_radicacion.Value = pnumero_radicacion;

                        DbParameter p_fecha_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_pago.ParameterName = "f_fecha_pago";
                        p_fecha_pago.DbType = DbType.DateTime;
                        p_fecha_pago.Value = pfecha_pago;

                        DbParameter p_num_cuotas = cmdTransaccionFactory.CreateParameter();
                        p_num_cuotas.ParameterName = "n_num_cuotas";
                        p_num_cuotas.DbType = DbType.Int32;
                        p_num_cuotas.Value = pnum_cuotas;

                        DbParameter p_valor_apagar = cmdTransaccionFactory.CreateParameter();
                        p_valor_apagar.ParameterName = "rn_valor_cobrado";
                        p_valor_apagar.Direction = ParameterDirection.InputOutput;
                        p_valor_apagar.Value = valor_apagar;

                        DbParameter p_error = cmdTransaccionFactory.CreateParameter();
                        p_error.ParameterName = "n_error";
                        p_error.Direction = ParameterDirection.InputOutput;
                        p_error.Value = 0;

                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_pago);
                        cmdTransaccionFactory.Parameters.Add(p_num_cuotas);
                        cmdTransaccionFactory.Parameters.Add(p_valor_apagar);
                        cmdTransaccionFactory.Parameters.Add(p_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "CLSCREDITO.CALCULAR_PORCUOTAS";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        valor_apagar = Convert.ToDecimal(p_valor_apagar.Value.ToString());
                        dbConnectionFactory.CerrarConexion(connection);

                        return valor_apagar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "AmortizarCreditoNumCuotas", ex);
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// Método para consultar el detalle de pago de un crédito por atributo
        /// </summary>
        /// <param name="pnumero_radicacion">número de radicación</param>
        /// <param name="pfecha_pago">fecha de pago</param>
        /// <param name="pvalor_pago">valor del pago</param>
        /// <param name="ptipo_pago">tipo de pago 2=Fecha, 11=Cuotas extras, 6=Abono capital, 3=Valor</param>
        /// <param name="pError">Error</param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<Atributos> AmortizarCreditoDetalle(Int64 pnumero_radicacion, DateTime pfecha_pago, Double pvalor_pago, Int64 ptipo_pago, ref Int64 pError, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<Atributos> lstAtributos = new List<Atributos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_NUMRADICACION = cmdTransaccionFactory.CreateParameter();
                        P_NUMRADICACION.ParameterName = "P_NUMRADICACION";
                        P_NUMRADICACION.Value = pnumero_radicacion;

                        DbParameter P_FECHAPAGO = cmdTransaccionFactory.CreateParameter();
                        P_FECHAPAGO.ParameterName = "P_FECHAPAGO";
                        P_FECHAPAGO.DbType = DbType.DateTime;
                        P_FECHAPAGO.Value = pfecha_pago;

                        DbParameter P_VALORPAGO = cmdTransaccionFactory.CreateParameter();
                        P_VALORPAGO.ParameterName = "P_VALORPAGO";
                        P_VALORPAGO.DbType = DbType.Double;
                        P_VALORPAGO.Value = pvalor_pago;

                        DbParameter P_TIPOPAGO = cmdTransaccionFactory.CreateParameter();
                        P_TIPOPAGO.ParameterName = "P_TIPOPAGO";
                        P_VALORPAGO.DbType = DbType.Double;
                        P_TIPOPAGO.Value = ptipo_pago;

                        DbParameter P_ERROR = cmdTransaccionFactory.CreateParameter();
                        P_ERROR.ParameterName = "P_ERROR";
                        P_ERROR.Direction = ParameterDirection.InputOutput;
                        P_ERROR.Value = 0;

                        cmdTransaccionFactory.Parameters.Add(P_NUMRADICACION);
                        cmdTransaccionFactory.Parameters.Add(P_FECHAPAGO);
                        cmdTransaccionFactory.Parameters.Add(P_VALORPAGO);
                        cmdTransaccionFactory.Parameters.Add(P_TIPOPAGO);
                        cmdTransaccionFactory.Parameters.Add(P_ERROR);

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DETALLEPAGO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pError = Convert.ToInt64(P_ERROR.Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "AmortizarCreditoDetalle", ex);
                        return null;
                    }
                }

                DateTime? fechacausacion = FechaUltCausacion(pUsuario);

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            cmdTransaccionFactory.CommandText = @"SELECT t.cod_atr, a.nombre As nom_atr, SUM(t.valor) As valor FROM atributos a JOIN temp_pagar t On a.cod_atr = t.cod_atr 
                                                                    GROUP BY t.cod_atr, a.nombre 
                                                                  UNION ALL
                                                                  SELECT t.cod_atr, a.nombre As nom_atr, 0 FROM atributos a JOIN causacion t ON a.cod_atr = t.cod_atr
                                                                    WHERE t.fecha_corte = TO_DATE('" + Convert.ToDateTime(fechacausacion).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') AND t.numero_radicacion = " + pnumero_radicacion + @"
                                                                    AND t.cod_atr NOT IN (SELECT x.cod_atr FROM temp_pagar x)
                                                                  GROUP BY t.cod_atr, a.nombre 
                                                                  ORDER BY 1 ";
                        else
                            cmdTransaccionFactory.CommandText = @"SELECT t.cod_atr, a.nombre As nom_atr, SUM(t.valor) As valor FROM atributos a JOIN temp_pagar t On a.cod_atr = t.cod_atr 
                                                                    GROUP BY t.cod_atr, a.nombre 
                                                                  UNION ALL
                                                                  SELECT t.cod_atr, a.nombre As nom_atr, 0 FROM atributos a JOIN causacion t ON a.cod_atr = t.cod_atr
                                                                    WHERE t.fecha_corte = '" + Convert.ToDateTime(fechacausacion).ToString(conf.ObtenerFormatoFecha()) + "') AND t.numero_radicacion = " + pnumero_radicacion + @"
                                                                    AND t.cod_atr NOT IN (SELECT x.cod_atr FROM temp_pagar x)
                                                                  GROUP BY t.cod_atr, a.nombre 
                                                                  ORDER BY 1 ";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Atributos eAtributos = new Atributos();
                            if (resultado["COD_ATR"] != DBNull.Value) eAtributos.cod_atr = Convert.ToInt64(resultado["COD_ATR"]);
                            if (resultado["NOM_ATR"] != DBNull.Value) eAtributos.nom_atr = Convert.ToString(resultado["NOM_ATR"]);
                            if (resultado["VALOR"] != DBNull.Value) eAtributos.valor = Convert.ToDouble(resultado["VALOR"]);
                            if (eAtributos.cod_atr == 1)
                                eAtributos.causado = eAtributos.valor;
                            else
                                eAtributos.causado = ValorCausado(pnumero_radicacion, eAtributos.cod_atr, Convert.ToDateTime(fechacausacion), pUsuario);
                            lstAtributos.Add(eAtributos);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAtributos;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "AmortizarCreditoDetalle", ex);
                        return null;
                    }
                }

            }
        }

        public DateTime? FechaUltCausacion(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DateTime fecha = new DateTime();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select Max(fecha) As fecha From cierea Where tipo = 'U' And estado = 'D'  ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) fecha = Convert.ToDateTime(resultado["FECHA"]);

                            return fecha;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public double ValorCausado(Int64 pNumeroRadicacion, Int64 pCodAtr, DateTime pFecha, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            double saldo_causado = 0;
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select saldo_causado From causacion Where ";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql += " fecha_corte = To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql += " fecha_corte = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' ";

                        sql += " And numero_radicacion = " + pNumeroRadicacion.ToString() + " And cod_atr = " + pCodAtr.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO_CAUSADO"] != DBNull.Value) saldo_causado = Convert.ToDouble(resultado["SALDO_CAUSADO"]);

                            return saldo_causado;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return saldo_causado;
                    }
                    catch
                    {
                        return saldo_causado;
                    }
                }
            }
        }

        public void guardardescuento(DescuentosDesembolso variable, Usuario pUsuario)
        {

            Credito entidad = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_obligacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_obligacion.ParameterName = "p_numero_obligacion";
                        p_numero_obligacion.Direction = ParameterDirection.InputOutput;
                        p_numero_obligacion.Value = variable.numero_obligacion;

                        DbParameter p_cod_deudor = cmdTransaccionFactory.CreateParameter();
                        p_cod_deudor.ParameterName = "p_cod_deudor";
                        p_cod_deudor.Value = variable.cod_deudor;

                        DbParameter p_monto = cmdTransaccionFactory.CreateParameter();
                        p_monto.ParameterName = "p_monto";
                        p_monto.Direction = ParameterDirection.InputOutput;
                        p_monto.Value = variable.monto;

                        DbParameter p_descuento = cmdTransaccionFactory.CreateParameter();
                        p_descuento.ParameterName = "p_descuento";
                        p_descuento.Direction = ParameterDirection.InputOutput;
                        p_descuento.Value = variable.descuento;

                        DbParameter p_TIPO_TRAN = cmdTransaccionFactory.CreateParameter();
                        p_TIPO_TRAN.ParameterName = "p_TIPO_TRAN";
                        p_TIPO_TRAN.Direction = ParameterDirection.InputOutput;
                        p_TIPO_TRAN.Value = variable.tipo_tran;

                        DbParameter p_tipo_movimiento = cmdTransaccionFactory.CreateParameter();
                        p_tipo_movimiento.ParameterName = "p_tipo_movimiento";
                        p_tipo_movimiento.Direction = ParameterDirection.InputOutput;
                        p_tipo_movimiento.Value = variable.tipo_movimiento;

                        cmdTransaccionFactory.Parameters.Add(p_numero_obligacion);
                        cmdTransaccionFactory.Parameters.Add(p_cod_deudor);
                        cmdTransaccionFactory.Parameters.Add(p_monto);
                        cmdTransaccionFactory.Parameters.Add(p_descuento);
                        cmdTransaccionFactory.Parameters.Add(p_TIPO_TRAN);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_movimiento);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_DESEM_DESCUEN_GUAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "guardar_descuentos", ex);

                    }
                }
            }
        }

        public string ValidarCredito(Credito pEntidad, Usuario pUsuario)
        {
            string sError = " ";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "pn_num_radic";
                        pNUMERO_RADICACION.Value = pEntidad.numero_radicacion;

                        DbParameter pMENSAJE = cmdTransaccionFactory.CreateParameter();
                        pMENSAJE.ParameterName = "pmensaje";
                        pMENSAJE.Size = 200;
                        pMENSAJE.Direction = ParameterDirection.Output;
                        pMENSAJE.Value = sError;

                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pMENSAJE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_DESEM_VALIDAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        sError = pMENSAJE.Value.ToString();

                        dbConnectionFactory.CerrarConexion(connection);
                        return sError;

                    }
                    catch (Exception ex)
                    {
                        sError = ex.Message;
                        return sError;
                    }
                }
            }
        }

        public List<Credito> ListarCreditoMasivo(Credito pCreditoMasivo, DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCreditoMasivo = new List<Credito>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select persona.cod_persona,v_creditos.* from v_Creditos left join persona on v_creditos.identificacion=persona.identificacion " + filtro;
                        if (pFechaIni != null && pFechaIni != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " fecha_aprobacion >= To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " fecha_aprobacion >= '" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        if (pFechaFin != null && pFechaFin != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " fecha_aprobacion <= To_Date('" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " fecha_aprobacion <= '" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["EMPRESA"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["TIPOCUENTA"] != DBNull.Value) entidad.tipocuenta = Convert.ToString(resultado["TIPOCUENTA"]);
                            lstCreditoMasivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCreditoMasivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoMasivoData", "ListarCreditoMasivo", ex);
                        return null;
                    }
                }
            }
        }

        public CreditoEmpresaRecaudo CrearModEmpresa_Recaudo(CreditoEmpresaRecaudo pEmpresa, Usuario vUsuario, int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcrerecaudo = cmdTransaccionFactory.CreateParameter();
                        pidcrerecaudo.ParameterName = "p_idcrerecaudo";
                        pidcrerecaudo.Value = pEmpresa.idcrerecaudo;
                        if (opcion == 1)
                            pidcrerecaudo.Direction = ParameterDirection.Output;
                        else
                            pidcrerecaudo.Direction = ParameterDirection.Input;
                        pidcrerecaudo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcrerecaudo);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pEmpresa.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        if (pEmpresa.cod_empresa != 0)
                        {
                            pcod_empresa.Value = pEmpresa.cod_empresa;
                        }
                        else
                        {
                            pcod_empresa.Value = DBNull.Value;
                        }

                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        pporcentaje.Value = pEmpresa.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pEmpresa.valor != 0) pvalor.Value = pEmpresa.valor; else pvalor.Value = DBNull.Value;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CREDEMPREC_CREAR"; //Crear
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CREDEMPREC_MOD"; //Modificar
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (opcion == 1)
                            pEmpresa.idcrerecaudo = Convert.ToInt32(pidcrerecaudo.Value);
                        return pEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoMasivoData", "CrearModEmpresa_Recaudo", ex);
                        return null;
                    }
                }
            }
        }

        public Int64? AutorizarCredito(Int64 pnumero_radicacion, Int64 pcod_deudor, DateTime pfecha_desembolso, ref string pError, Usuario pUsuario)
        {
            Int64? pautorizacion = null;
            pError = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.Value = pnumero_radicacion;
                        p_numero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);

                        DbParameter p_cod_deudor = cmdTransaccionFactory.CreateParameter();
                        p_cod_deudor.ParameterName = "p_cod_deudor";
                        p_cod_deudor.Value = pcod_deudor;
                        p_cod_deudor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_deudor);

                        DbParameter p_fecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        p_fecha_desembolso.ParameterName = "p_fecha_desembolso";
                        p_fecha_desembolso.DbType = DbType.DateTime;
                        p_fecha_desembolso.Value = pfecha_desembolso;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_desembolso);

                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        p_ip.DbType = DbType.String;
                        p_ip.Value = pUsuario.IP;
                        cmdTransaccionFactory.Parameters.Add(p_ip);

                        DbParameter p_Usuario = cmdTransaccionFactory.CreateParameter();
                        p_Usuario.ParameterName = "p_Usuario";
                        p_Usuario.Value = pUsuario.codusuario;
                        p_Usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_Usuario);

                        DbParameter p_Autorizacion = cmdTransaccionFactory.CreateParameter();
                        p_Autorizacion.ParameterName = "p_Autorizacion";
                        p_Autorizacion.Value = 0;
                        p_Autorizacion.DbType = DbType.Int32;
                        p_Autorizacion.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(p_Autorizacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_DESEM_AUTORIZAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (p_Autorizacion.Value != null)
                            pautorizacion = Convert.ToInt64(p_Autorizacion.Value.ToString());
                        else
                            pautorizacion = null;

                        dbConnectionFactory.CerrarConexion(connection);

                        return pautorizacion;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }

        public Int32 VerificarAutorizacion(int pTipoProducto, string pNumeroProducto, DateTime pFecha, String pIp, String pAutorizacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Int32 estado = 0;
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * From Persona_Autorizacion Where idautorizacion = " + pAutorizacion + " And tipo_producto = " + pTipoProducto.ToString() + " And numero_producto = '" + pNumeroProducto + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ESTADO"] != DBNull.Value) estado = Convert.ToInt32(resultado["ESTADO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return estado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "VerificarAutorizacion", ex);
                        return estado;
                    }
                }
            }
        }

        public Int32 VerificarAutorizacion(String pAutorizacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Int32 estado = 0;
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * From Persona_Autorizacion Where idautorizacion = " + pAutorizacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ESTADO"] != DBNull.Value) estado = Convert.ToInt32(resultado["ESTADO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return estado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "VerificarAutorizacion", ex);
                        return estado;
                    }
                }
            }
        }

        public DateTime? FechaInicioDESEMBOLSO(Int64 pNumero_radicacion, Usuario vUsuario)
        {
            DateTime? pFechaInicio;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pNumero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_fecha_inicio";
                        pfecha_inicio.Value = DBNull.Value;
                        pfecha_inicio.Direction = ParameterDirection.Output;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_FECHAINICIO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pFechaInicio = Convert.ToDateTime(pfecha_inicio.Value);

                        return pFechaInicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "FechaInicioDESEMBOLSO", ex);
                        return null;
                    }
                }
            }
        }

        public DateTime? FechaInicioCredito(DateTime pFecha_desembolso, Int32 pCodperiodicidad, Int32 pFormapago, Int32? pCodEmpresa, string pCodLinea, ref string pError, ref Boolean pRpta, Usuario vUsuario)
        {
            DateTime? pFechaInicio;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        pfecha_desembolso.ParameterName = "p_fecha_desembolso";
                        pfecha_desembolso.Value = pFecha_desembolso;
                        pfecha_desembolso.Direction = ParameterDirection.Input;
                        pfecha_desembolso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_desembolso);

                        DbParameter pcodperiodicidad = cmdTransaccionFactory.CreateParameter();
                        pcodperiodicidad.ParameterName = "p_codperiodicidad";
                        pcodperiodicidad.Value = pCodperiodicidad;
                        pcodperiodicidad.Direction = ParameterDirection.Input;
                        pcodperiodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodperiodicidad);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pFormapago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter pcodmempresa = cmdTransaccionFactory.CreateParameter();
                        pcodmempresa.ParameterName = "p_codmempresa";
                        if (pCodEmpresa != null) pcodmempresa.Value = pCodEmpresa; else pcodmempresa.Value = DBNull.Value;
                        pcodmempresa.Direction = ParameterDirection.Input;
                        pcodmempresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodmempresa);

                        DbParameter pcod_linea = cmdTransaccionFactory.CreateParameter();
                        pcod_linea.ParameterName = "p_cod_linea";
                        pcod_linea.Value = pCodLinea;
                        pcod_linea.Direction = ParameterDirection.Input;
                        pcod_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_fecha_inicio";
                        pfecha_inicio.Value = DateTime.MinValue;
                        pfecha_inicio.Direction = ParameterDirection.InputOutput;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter pdias_aju = cmdTransaccionFactory.CreateParameter();
                        pdias_aju.ParameterName = "p_dias_aju";
                        pdias_aju.Value = 0;
                        pdias_aju.Direction = ParameterDirection.Output;
                        pdias_aju.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_aju);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_FECHAINICRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pFechaInicio = Convert.ToDateTime(pfecha_inicio.Value);
                        pRpta = true;
                        return pFechaInicio;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        pRpta = false;
                        return null;
                    }
                }
            }
        }

        public Int64 InsertSolicitudCredito(Usuario pUsuario, CreditoEducativoEntit creditoEduca)
        {
            Int64 Numero_Radicacion = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter PFECHA_SOLICITUD = cmdTransaccionFactory.CreateParameter();
                        PFECHA_SOLICITUD.ParameterName = "PFECHA_SOLICITUD";
                        if (creditoEduca.PFECHA_SOLICITUD != null)
                            PFECHA_SOLICITUD.Value = creditoEduca.PFECHA_SOLICITUD;
                        else
                            PFECHA_SOLICITUD.Value = DateTime.Now;
                        PFECHA_SOLICITUD.Direction = ParameterDirection.Input;
                        PFECHA_SOLICITUD.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PFECHA_SOLICITUD);

                        DbParameter PCOD_DEUDOR = cmdTransaccionFactory.CreateParameter();
                        PCOD_DEUDOR.ParameterName = "PCOD_DEUDOR";
                        PCOD_DEUDOR.Value = creditoEduca.PCOD_DEUDOR;
                        PCOD_DEUDOR.Direction = ParameterDirection.Input;
                        PCOD_DEUDOR.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCOD_DEUDOR);

                        DbParameter PVALOR_MATRICULA = cmdTransaccionFactory.CreateParameter();
                        PVALOR_MATRICULA.ParameterName = "PVALOR_MATRICULA";
                        PVALOR_MATRICULA.Value = creditoEduca.PVALOR_MATRICULA;
                        PVALOR_MATRICULA.Direction = ParameterDirection.Input;
                        PVALOR_MATRICULA.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(PVALOR_MATRICULA);

                        DbParameter PMONTO_SOLICITADO = cmdTransaccionFactory.CreateParameter();
                        PMONTO_SOLICITADO.ParameterName = "PMONTO_SOLICITADO";
                        PMONTO_SOLICITADO.Value = creditoEduca.PMONTO_SOLICITADO;
                        PMONTO_SOLICITADO.Direction = ParameterDirection.Input;
                        PMONTO_SOLICITADO.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(PMONTO_SOLICITADO);

                        DbParameter PNUMERO_CUOTAS = cmdTransaccionFactory.CreateParameter();
                        PNUMERO_CUOTAS.ParameterName = "PNUMERO_CUOTAS";
                        PNUMERO_CUOTAS.Value = creditoEduca.PNUMERO_CUOTAS;
                        PNUMERO_CUOTAS.Direction = ParameterDirection.Input;
                        PNUMERO_CUOTAS.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PNUMERO_CUOTAS);

                        DbParameter PCOD_LINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        PCOD_LINEA_CREDITO.ParameterName = "PCOD_LINEA_CREDITO";
                        PCOD_LINEA_CREDITO.Value = creditoEduca.PCOD_LINEA_CREDITO;
                        PCOD_LINEA_CREDITO.Direction = ParameterDirection.Input;
                        PCOD_LINEA_CREDITO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCOD_LINEA_CREDITO);

                        DbParameter PCOD_PERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        PCOD_PERIODICIDAD.ParameterName = "PCOD_PERIODICIDAD";
                        PCOD_PERIODICIDAD.Value = creditoEduca.PCOD_PERIODICIDAD;
                        PCOD_PERIODICIDAD.Direction = ParameterDirection.Input;
                        PCOD_PERIODICIDAD.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCOD_PERIODICIDAD);

                        DbParameter PCOD_OFICINA = cmdTransaccionFactory.CreateParameter();
                        PCOD_OFICINA.ParameterName = "PCOD_OFICINA";
                        PCOD_OFICINA.Value = creditoEduca.PCOD_OFICINA;
                        PCOD_OFICINA.Direction = ParameterDirection.Input;
                        PCOD_OFICINA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCOD_OFICINA);

                        DbParameter PFORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        PFORMA_PAGO.ParameterName = "PFORMA_PAGO";
                        PFORMA_PAGO.Value = creditoEduca.PFORMA_PAGO;
                        PFORMA_PAGO.Direction = ParameterDirection.Input;
                        PFORMA_PAGO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PFORMA_PAGO);

                        DbParameter PFECHA_PRIMERPAGO = cmdTransaccionFactory.CreateParameter();
                        PFECHA_PRIMERPAGO.ParameterName = "PFECHA_PRIMERPAGO";
                        if (creditoEduca.PFECHA_PRIMERPAGO != DateTime.MinValue) PFECHA_PRIMERPAGO.Value = creditoEduca.PFECHA_PRIMERPAGO; else PFECHA_PRIMERPAGO.Value = DBNull.Value;
                        PFECHA_PRIMERPAGO.Direction = ParameterDirection.Input;
                        PFECHA_PRIMERPAGO.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PFECHA_PRIMERPAGO);

                        DbParameter PCOD_ASESOR = cmdTransaccionFactory.CreateParameter();
                        PCOD_ASESOR.ParameterName = "PCOD_ASESOR";
                        PCOD_ASESOR.Value = creditoEduca.PCOD_ASESOR;
                        PCOD_ASESOR.Direction = ParameterDirection.Input;
                        PCOD_ASESOR.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCOD_ASESOR);

                        DbParameter PUSUARIO = cmdTransaccionFactory.CreateParameter();
                        PUSUARIO.ParameterName = "PUSUARIO";
                        PUSUARIO.Value = 1;
                        PUSUARIO.Direction = ParameterDirection.Input;
                        PUSUARIO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PUSUARIO);

                        DbParameter PCOD_EMPRESA = cmdTransaccionFactory.CreateParameter();
                        PCOD_EMPRESA.ParameterName = "PCOD_EMPRESA";
                        if (creditoEduca.PCOD_EMPRESA != 0) PCOD_EMPRESA.Value = creditoEduca.PCOD_EMPRESA; else PCOD_EMPRESA.Value = DBNull.Value;
                        PCOD_EMPRESA.Direction = ParameterDirection.Input;
                        PCOD_EMPRESA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCOD_EMPRESA);

                        DbParameter PUNIVERSIDAD = cmdTransaccionFactory.CreateParameter();
                        PUNIVERSIDAD.ParameterName = "PUNIVERSIDAD";
                        PUNIVERSIDAD.Value = creditoEduca.universidad;
                        PUNIVERSIDAD.Direction = ParameterDirection.Input;
                        PUNIVERSIDAD.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PUNIVERSIDAD);

                        DbParameter PSEMESTRE = cmdTransaccionFactory.CreateParameter();
                        PSEMESTRE.ParameterName = "PSEMESTRE";
                        PSEMESTRE.Value = creditoEduca.semestre;
                        PSEMESTRE.Direction = ParameterDirection.Input;
                        PSEMESTRE.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PSEMESTRE);

                        DbParameter PNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        PNUMERO_RADICACION.ParameterName = "PNUMERO_RADICACION";
                        PNUMERO_RADICACION.Value = Numero_Radicacion;
                        PNUMERO_RADICACION.Direction = ParameterDirection.InputOutput;
                        PNUMERO_RADICACION.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PNUMERO_RADICACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_EDUCATIVO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (PNUMERO_RADICACION.Value != null)
                            Numero_Radicacion = Convert.ToInt64(PNUMERO_RADICACION.Value);

                        return Numero_Radicacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "EliminarAtributos", ex);
                        return -1;
                    }
                }
            }
        }

        public CreditoOrdenServicio CrearCreditoOrdenServicio(CreditoOrdenServicio pCreditoOrdenServicio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidordenservicio = cmdTransaccionFactory.CreateParameter();
                        pidordenservicio.ParameterName = "p_idordenservicio";
                        pidordenservicio.Value = pCreditoOrdenServicio.idordenservicio;
                        pidordenservicio.Direction = ParameterDirection.Output;
                        pidordenservicio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidordenservicio);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pCreditoOrdenServicio.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pCreditoOrdenServicio.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pCreditoOrdenServicio.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pidproveedor = cmdTransaccionFactory.CreateParameter();
                        pidproveedor.ParameterName = "p_idproveedor";
                        if (pCreditoOrdenServicio.idproveedor == null)
                            pidproveedor.Value = DBNull.Value;
                        else
                            pidproveedor.Value = pCreditoOrdenServicio.idproveedor;
                        pidproveedor.Direction = ParameterDirection.Input;
                        pidproveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidproveedor);

                        DbParameter pnomproveedor = cmdTransaccionFactory.CreateParameter();
                        pnomproveedor.ParameterName = "p_nomproveedor";
                        if (pCreditoOrdenServicio.nomproveedor == null)
                            pnomproveedor.Value = DBNull.Value;
                        else
                            pnomproveedor.Value = pCreditoOrdenServicio.nomproveedor;
                        pnomproveedor.Direction = ParameterDirection.Input;
                        pnomproveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnomproveedor);

                        DbParameter pdetalle = cmdTransaccionFactory.CreateParameter();
                        pdetalle.ParameterName = "p_detalle";
                        if (pCreditoOrdenServicio.detalle == null)
                            pdetalle.Value = DBNull.Value;
                        else
                            pdetalle.Value = pCreditoOrdenServicio.detalle;
                        pdetalle.Direction = ParameterDirection.Input;
                        pdetalle.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdetalle);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pCreditoOrdenServicio.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pCreditoOrdenServicio.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CREDITO_OR_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pCreditoOrdenServicio.idordenservicio = Convert.ToInt64(pidordenservicio.Value);
                        return pCreditoOrdenServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "CrearCreditoOrdenServicio", ex);
                        return null;
                    }
                }
            }
        }

        public CreditoOrdenServicio ModificarCreditoOrdenServicio(CreditoOrdenServicio pCreditoOrdenServicio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidordenservicio = cmdTransaccionFactory.CreateParameter();
                        pidordenservicio.ParameterName = "p_idordenservicio";
                        pidordenservicio.Value = pCreditoOrdenServicio.idordenservicio;
                        pidordenservicio.Direction = ParameterDirection.Input;
                        pidordenservicio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidordenservicio);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pCreditoOrdenServicio.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pCreditoOrdenServicio.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pCreditoOrdenServicio.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pidproveedor = cmdTransaccionFactory.CreateParameter();
                        pidproveedor.ParameterName = "p_idproveedor";
                        if (pCreditoOrdenServicio.idproveedor == null)
                            pidproveedor.Value = DBNull.Value;
                        else
                            pidproveedor.Value = pCreditoOrdenServicio.idproveedor;
                        pidproveedor.Direction = ParameterDirection.Input;
                        pidproveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidproveedor);

                        DbParameter pnomproveedor = cmdTransaccionFactory.CreateParameter();
                        pnomproveedor.ParameterName = "p_nomproveedor";
                        if (pCreditoOrdenServicio.nomproveedor == null)
                            pnomproveedor.Value = DBNull.Value;
                        else
                            pnomproveedor.Value = pCreditoOrdenServicio.nomproveedor;
                        pnomproveedor.Direction = ParameterDirection.Input;
                        pnomproveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnomproveedor);

                        DbParameter pdetalle = cmdTransaccionFactory.CreateParameter();
                        pdetalle.ParameterName = "p_detalle";
                        if (pCreditoOrdenServicio.detalle == null)
                            pdetalle.Value = DBNull.Value;
                        else
                            pdetalle.Value = pCreditoOrdenServicio.detalle;
                        pdetalle.Direction = ParameterDirection.Input;
                        pdetalle.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdetalle);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pCreditoOrdenServicio.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pCreditoOrdenServicio.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pnumero_preimpreso = cmdTransaccionFactory.CreateParameter();
                        pnumero_preimpreso.ParameterName = "p_numero_preimpreso";
                        if (pCreditoOrdenServicio.numero_preimpreso == null)
                            pnumero_preimpreso.Value = DBNull.Value;
                        else
                            pnumero_preimpreso.Value = pCreditoOrdenServicio.numero_preimpreso;
                        pnumero_preimpreso.Direction = ParameterDirection.Input;
                        pnumero_preimpreso.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_preimpreso);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CREDITO_OR_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCreditoOrdenServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ModificarCreditoOrdenServicio", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarCreditoOrdenServicio(Int64 pId, Int64 pNum_radicacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidordenservicio = cmdTransaccionFactory.CreateParameter();
                        pidordenservicio.ParameterName = "p_idordenservicio";
                        pidordenservicio.Value = pId;
                        pidordenservicio.Direction = ParameterDirection.Input;
                        pidordenservicio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidordenservicio);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pNum_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CREDITO_OR_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "EliminarCreditoOrdenServicio", ex);
                    }
                }
            }
        }

        public List<Credito> RealizarPreAnalisis(bool preanalisis, DateTime pfecha, Int64 pCodPersona, decimal pDisponible, Int64 pNumeroCuotas, decimal pMontoSolicitado, Int32 pCodPeriodicidad, bool pEducativo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //Modificación para usar procedimiento en el scoring de crédito
                        string procedimiento = "";
                        if (preanalisis)
                            procedimiento = "USP_XPINN_CRE_PREANALISIS";
                        else if (!preanalisis)
                            procedimiento = "USP_XPINN_CRE_CUPOS_SCORING";

                        DbParameter pCod_Persona = cmdTransaccionFactory.CreateParameter();
                        pCod_Persona.ParameterName = "pCod_Persona";
                        pCod_Persona.Value = pCodPersona;
                        pCod_Persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCod_Persona);

                        DbParameter pFecha = cmdTransaccionFactory.CreateParameter();
                        pFecha.ParameterName = "pFecha";
                        pFecha.Value = pfecha;
                        pFecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pFecha);

                        DbParameter pTotalDisponible = cmdTransaccionFactory.CreateParameter();
                        pTotalDisponible.ParameterName = "pTotalDisponible";
                        pTotalDisponible.Value = pDisponible;
                        pTotalDisponible.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pTotalDisponible);

                        DbParameter pPlazo = cmdTransaccionFactory.CreateParameter();
                        pPlazo.ParameterName = "pNumeroCuotas";
                        pPlazo.Value = pNumeroCuotas;
                        pPlazo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pPlazo);

                        DbParameter pMonto = cmdTransaccionFactory.CreateParameter();
                        pMonto.ParameterName = "pMontoSolicitado";
                        pMonto.Value = pMontoSolicitado;
                        pMonto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pMonto);

                        DbParameter pCod_Periodicidad = cmdTransaccionFactory.CreateParameter();
                        pCod_Periodicidad.ParameterName = "pCod_Periodicidad";
                        pCod_Periodicidad.Value = pCodPeriodicidad;
                        pCod_Periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pCod_Periodicidad);

                        DbParameter pCreditoEducativo = cmdTransaccionFactory.CreateParameter();
                        pCreditoEducativo.ParameterName = "pCreditoEducativo";
                        pCreditoEducativo.Value = pEducativo;
                        pCreditoEducativo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pCreditoEducativo);

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = procedimiento;
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "RealizarPreAnalisis", ex);
                        return null;
                    }
                    //}

                    //using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                    //{
                    try
                    {
                        //cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select Temp_PreAnalisis.*, lineascredito.educativo, lineascredito.maneja_auxilio "
                                     + "From Temp_PreAnalisis Inner Join lineascredito On lineascredito.cod_linea_credito = temp_preanalisis.cod_linea_credito";

                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_linea_credito = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["MONTO_MAXIMO"] != DBNull.Value) entidad.monto_maximo = Convert.ToDecimal(resultado["MONTO_MAXIMO"]);
                            if (resultado["PLAZO_MAXIMO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO_MAXIMO"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["CUOTA_ESTIMADA"] != DBNull.Value) entidad.cuota_credito = Convert.ToDecimal(resultado["CUOTA_ESTIMADA"]);
                            if (resultado["EDUCATIVO"] != DBNull.Value) entidad.educativo = Convert.ToInt32(resultado["EDUCATIVO"]);
                            if (resultado["SALDO_ACTUAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_ACTUAL"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["MONTO"]);
                            if (resultado["RECIPROCIDAD"] != DBNull.Value) entidad.reciprocidad = Convert.ToInt32(resultado["RECIPROCIDAD"]);
                            if (resultado["REFINANCIAR"] != DBNull.Value) entidad.check = Convert.ToInt32(resultado["REFINANCIAR"]);
                            if (resultado["PORCENTAJE_AUXILIO"] != DBNull.Value) entidad.porcentaje_auxilio = Convert.ToDecimal(resultado["PORCENTAJE_AUXILIO"]);
                            if (resultado["VALOR_AUXILIO"] != DBNull.Value) entidad.valor_auxilio = Convert.ToDecimal(resultado["VALOR_AUXILIO"]);
                            if (resultado["MANEJA_AUXILIO"] != DBNull.Value) entidad.maneja_auxilio = Convert.ToInt32(resultado["MANEJA_AUXILIO"]);
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "RealizarPreAnalisis", ex);
                        return null;
                    }
                }

                return lstCredito;
            }
        }

        public Int64 ObtenerNumeroPreImpreso(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select max(NUMERO_PREIMPRESO) + 1 from CREDITO_ORDEN_SERVICIO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ObtenerNumeroPreImpreso", ex);
                        return -1;
                    }
                }
            }
        }

        public void ModificarCreditoOrdenServicio(Int64 pNum_radicacion, Int64? pNum_preImpreso, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (pNum_preImpreso != null)
                        {
                            sql = "UPDATE CREDITO_ORDEN_SERVICIO SET NUMERO_PREIMPRESO = " + pNum_preImpreso.ToString() + " WHERE NUMERO_RADICACION = " + pNum_radicacion.ToString();

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            cmdTransaccionFactory.ExecuteNonQuery();
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ModificarCredito", ex);
                    }
                }
            }
        }

        ///AGREGADO
        ///
        public Credito CREARCREDITOANALISIS(Credito pPersona1, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidpreanalisis = cmdTransaccionFactory.CreateParameter();
                        pidpreanalisis.ParameterName = "p_idpreanalisis";
                        pidpreanalisis.Value = pPersona1.idpreanalisis;
                        pidpreanalisis.Direction = ParameterDirection.Output;
                        pidpreanalisis.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidpreanalisis);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pPersona1.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pPersona1.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter psaldo_disponible = cmdTransaccionFactory.CreateParameter();
                        psaldo_disponible.ParameterName = "p_saldo_disponible";
                        if (pPersona1.saldo_disponible == 0)
                            psaldo_disponible.Value = DBNull.Value;
                        else
                            psaldo_disponible.Value = pPersona1.saldo_disponible;
                        psaldo_disponible.Direction = ParameterDirection.Input;
                        psaldo_disponible.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_disponible);

                        DbParameter pcuota_credito = cmdTransaccionFactory.CreateParameter();
                        pcuota_credito.ParameterName = "p_cuota_credito";
                        if (pPersona1.cuota_credito == 0)
                            pcuota_credito.Value = DBNull.Value;
                        else
                            pcuota_credito.Value = pPersona1.cuota_credito;
                        pcuota_credito.Direction = ParameterDirection.Input;
                        pcuota_credito.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuota_credito);

                        DbParameter pcuota_servicios = cmdTransaccionFactory.CreateParameter();
                        pcuota_servicios.ParameterName = "p_cuota_servicios";
                        if (pPersona1.cuota_servicios == 0)
                            pcuota_servicios.Value = DBNull.Value;
                        else
                            pcuota_servicios.Value = pPersona1.cuota_servicios;
                        pcuota_servicios.Direction = ParameterDirection.Input;
                        pcuota_servicios.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuota_servicios);

                        DbParameter ppago_terceros = cmdTransaccionFactory.CreateParameter();
                        ppago_terceros.ParameterName = "p_pago_terceros";
                        if (pPersona1.pago_terceros == 0)
                            ppago_terceros.Value = DBNull.Value;
                        else
                            ppago_terceros.Value = pPersona1.pago_terceros;
                        ppago_terceros.Direction = ParameterDirection.Input;
                        ppago_terceros.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ppago_terceros);

                        DbParameter pcuota_otros = cmdTransaccionFactory.CreateParameter();
                        pcuota_otros.ParameterName = "p_cuota_otros";
                        if (pPersona1.cuota_otros == 0)
                            pcuota_otros.Value = DBNull.Value;
                        else
                            pcuota_otros.Value = pPersona1.cuota_otros;
                        pcuota_otros.Direction = ParameterDirection.Input;
                        pcuota_otros.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuota_otros);

                        DbParameter pingresos_adicionales = cmdTransaccionFactory.CreateParameter();
                        pingresos_adicionales.ParameterName = "p_ingresos_adicionales";
                        if (pPersona1.ingresos_adicionales == 0)
                            pingresos_adicionales.Value = DBNull.Value;
                        else
                            pingresos_adicionales.Value = pPersona1.ingresos_adicionales;
                        pingresos_adicionales.Direction = ParameterDirection.Input;
                        pingresos_adicionales.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pingresos_adicionales);

                        DbParameter pmenos_smlmv = cmdTransaccionFactory.CreateParameter();
                        pmenos_smlmv.ParameterName = "p_menos_smlmv";
                        if (pPersona1.menos_smlmv == 0)
                            pmenos_smlmv.Value = DBNull.Value;
                        else
                            pmenos_smlmv.Value = pPersona1.menos_smlmv;
                        pmenos_smlmv.Direction = ParameterDirection.Input;
                        pmenos_smlmv.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmenos_smlmv);

                        DbParameter ptotal_disponible = cmdTransaccionFactory.CreateParameter();
                        ptotal_disponible.ParameterName = "p_total_disponible";
                        if (pPersona1.total_disponible == 0)
                            ptotal_disponible.Value = DBNull.Value;
                        else
                            ptotal_disponible.Value = pPersona1.total_disponible;
                        ptotal_disponible.Direction = ParameterDirection.Input;
                        ptotal_disponible.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptotal_disponible);

                        DbParameter paportes = cmdTransaccionFactory.CreateParameter();
                        paportes.ParameterName = "p_aportes";
                        if (pPersona1.aportes == 0)
                            paportes.Value = DBNull.Value;
                        else
                            paportes.Value = pPersona1.aportes;
                        paportes.Direction = ParameterDirection.Input;
                        paportes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(paportes);

                        DbParameter pcreditos = cmdTransaccionFactory.CreateParameter();
                        pcreditos.ParameterName = "p_creditos";
                        if (pPersona1.creditos == 0)
                            pcreditos.Value = DBNull.Value;
                        else
                            pcreditos.Value = pPersona1.creditos;
                        pcreditos.Direction = ParameterDirection.Input;
                        pcreditos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcreditos);

                        DbParameter pcapitalizacion = cmdTransaccionFactory.CreateParameter();
                        pcapitalizacion.ParameterName = "p_capitalizacion";
                        if (pPersona1.capitalizacion == 0)
                            pcapitalizacion.Value = DBNull.Value;
                        else
                            pcapitalizacion.Value = pPersona1.capitalizacion;
                        pcapitalizacion.Direction = ParameterDirection.Input;
                        pcapitalizacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcapitalizacion);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        if (pPersona1.cod_usuario == 0)
                            pcod_usuario.Value = DBNull.Value;
                        else
                            pcod_usuario.Value = pPersona1.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);
                        //
                        DbParameter pmonto_solicitado = cmdTransaccionFactory.CreateParameter();
                        pmonto_solicitado.ParameterName = "p_monto_solicitado";
                        if (pPersona1.monto == 0)
                            pmonto_solicitado.Value = DBNull.Value;
                        else
                            pmonto_solicitado.Value = pPersona1.monto;
                        pmonto_solicitado.Direction = ParameterDirection.Input;
                        pmonto_solicitado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmonto_solicitado);

                        DbParameter pplazo_solicitado = cmdTransaccionFactory.CreateParameter();
                        pplazo_solicitado.ParameterName = "p_plazo_solicitado";
                        if (pPersona1.plazo == 0)
                            pplazo_solicitado.Value = DBNull.Value;
                        else
                            pplazo_solicitado.Value = pPersona1.plazo;
                        pplazo_solicitado.Direction = ParameterDirection.Input;
                        pplazo_solicitado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pplazo_solicitado);

                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "p_cod_linea_credito";
                        if (pPersona1.cod_linea_credito == null)
                            pcod_linea_credito.Value = DBNull.Value;
                        else
                            pcod_linea_credito.Value = pPersona1.cod_linea_credito;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        pcod_linea_credito.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PER_PREANALISI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pPersona1.idpreanalisis = pidpreanalisis.Value != DBNull.Value ? Convert.ToInt64(pidpreanalisis.Value.ToString()) : 0;

                        dbConnectionFactory.CerrarConexion(connection);

                        return pPersona1;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "CREARCREDITOANALISIS", ex);
                        return null;
                    }
                }
            }
        }

        public decimal ObtenerSaldoTotalXpersona(Int64 pCodPersona, Usuario pUsuario)
        {
            decimal resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT SUM(C.SALDO_CAPITAL) FROM CREDITO C WHERE C.ESTADO NOT IN ('T','B')
                                     AND C.COD_DEUDOR = " + pCodPersona.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToDecimal(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ObtenerSaldoTotalXpersona", ex);
                        return -1;
                    }
                }
            }
        }

        public Credito_Beneficiario CrearCredito_Beneficiario(Credito_Beneficiario pCredito_Beneficiario, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodbeneficiariocre = cmdTransaccionFactory.CreateParameter();
                        pcodbeneficiariocre.ParameterName = "p_codbeneficiariocre";
                        pcodbeneficiariocre.Value = pCredito_Beneficiario.codbeneficiariocre;
                        pcodbeneficiariocre.Direction = ParameterDirection.Output;
                        pcodbeneficiariocre.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodbeneficiariocre);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pCredito_Beneficiario.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pCredito_Beneficiario.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pCredito_Beneficiario.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        if (pCredito_Beneficiario.nombre == null)
                            pnombre.Value = DBNull.Value;
                        else
                            pnombre.Value = pCredito_Beneficiario.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pcod_parentesco = cmdTransaccionFactory.CreateParameter();
                        pcod_parentesco.ParameterName = "p_cod_parentesco";
                        if (pCredito_Beneficiario.cod_parentesco == null)
                            pcod_parentesco.Value = DBNull.Value;
                        else
                            pcod_parentesco.Value = pCredito_Beneficiario.cod_parentesco;
                        pcod_parentesco.Direction = ParameterDirection.Input;
                        pcod_parentesco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_parentesco);

                        DbParameter pporcentaje_beneficiario = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_beneficiario.ParameterName = "p_porcentaje_beneficiario";
                        if (pCredito_Beneficiario.porcentaje_beneficiario == null)
                            pporcentaje_beneficiario.Value = DBNull.Value;
                        else
                            pporcentaje_beneficiario.Value = pCredito_Beneficiario.porcentaje_beneficiario;
                        pporcentaje_beneficiario.Direction = ParameterDirection.Input;
                        pporcentaje_beneficiario.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_beneficiario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CRED_BENEF_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pCredito_Beneficiario.codbeneficiariocre = Convert.ToInt64(pcodbeneficiariocre.Value);
                        return pCredito_Beneficiario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Credito_BeneficiarioData", "CrearCredito_Beneficiario", ex);
                        return null;
                    }
                }
            }
        }
        public List<Beneficiarios> ConsultarBeneficiariosCredito(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Beneficiarios> lstentidad = new List<Beneficiarios>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CREDITO_BENEFICIARIOS WHERE NUMERO_RADICACION = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Beneficiarios entidad = new Beneficiarios();
                            if (resultado["CODBENEFICIARIOCRE"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CODBENEFICIARIOCRE"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.cod_poliza = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_PARENTESCO"] != DBNull.Value) entidad.parentesco = Convert.ToString(resultado["COD_PARENTESCO"]);
                            if (resultado["PORCENTAJE_BENEFICIARIO"] != DBNull.Value) entidad.porcentaje = Convert.ToInt64(resultado["PORCENTAJE_BENEFICIARIO"]);

                            lstentidad.Add(entidad);

                        }

                        return lstentidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListarBeneficiariosData", "ConsultarBeneficiarios", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Mètodo para realizar el llamado al PL del desembolso del crèdito
        /// </summary>
        /// <param name="pCredito"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Int64 DesembolsarAvances(Credito pCredito, ref decimal pMontoDesembolso, ref string pError, Usuario pUsuario)
        {
            pError = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.Value = pCredito.numero_radicacion;

                        DbParameter p_fecha_prim_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_prim_pago.ParameterName = "p_fecha_prim_pago";
                        if (pCredito.fecha_prim_pago == null)
                            p_fecha_prim_pago.Value = DBNull.Value;
                        else
                            p_fecha_prim_pago.Value = pCredito.fecha_prim_pago;
                        p_fecha_prim_pago.DbType = DbType.Date;

                        DbParameter p_idavance = cmdTransaccionFactory.CreateParameter();
                        p_idavance.ParameterName = "p_idavance";
                        p_idavance.Value = pCredito.numero_credito;
                        p_idavance.DbType = DbType.AnsiString;
                        p_idavance.Direction = ParameterDirection.Input;

                        DbParameter p_fecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        p_fecha_desembolso.ParameterName = "p_fecha_desembolso";
                        p_fecha_desembolso.Value = pCredito.fecha_desembolso;

                        DbParameter p_Usuario = cmdTransaccionFactory.CreateParameter();
                        p_Usuario.ParameterName = "p_Usuario";
                        p_Usuario.Value = pUsuario.codusuario;
                        p_Usuario.Direction = ParameterDirection.Input;
                        p_Usuario.Size = 50;

                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        p_ip.Value = pUsuario.IP;

                        DbParameter p_valor_desembolsado = cmdTransaccionFactory.CreateParameter();
                        p_valor_desembolsado.ParameterName = "p_valor_desembolsado";
                        p_valor_desembolsado.Value = pCredito.monto;
                        p_valor_desembolsado.Direction = ParameterDirection.Input;

                        DbParameter p_Cod_Ope = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Ope.ParameterName = "p_cod_ope";
                        p_Cod_Ope.Value = pCredito.cod_ope;
                        p_Cod_Ope.Direction = ParameterDirection.Output;

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

                        DbParameter p_error = cmdTransaccionFactory.CreateParameter();
                        p_error.ParameterName = "p_error";
                        string s_error = " ";
                        for (int i = 0; i < 200; i++) s_error += " ";  // Se asigna espacios para efectos de poder ejecutar el PL
                        p_error.Value = s_error;
                        p_error.DbType = DbType.StringFixedLength;
                        p_error.Direction = ParameterDirection.Output;

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
                        cmdTransaccionFactory.Parameters.Add(p_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DESAVANCE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (p_error.Value != null)
                            pError = p_error.Value.ToString();
                        pCredito.cod_ope = 0;
                        if (p_Cod_Ope.Value != null)
                            if (p_Cod_Ope.Value.ToString() != "")
                                pCredito.cod_ope = Convert.ToInt64(p_Cod_Ope.Value.ToString());

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pCredito, "Credito", pUsuario, Accion.Modificar.ToString());

                        dbConnectionFactory.CerrarConexion(connection);

                        return pCredito.cod_ope;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "DesembolsarAvances", ex);
                        return 0;
                    }
                }
            }
        }

        public List<DescuentosCredito> ListarDescuentosCredito(DescuentosCredito pDescuentosCredito, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DescuentosCredito> lstDescuentosCredito = new List<DescuentosCredito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT d.*, a.nombre FROM DescuentosCredito d LEFT JOIN atributos a ON d.cod_atr = a.cod_atr" + ObtenerFiltro(pDescuentosCredito, "d.") + " ORDER BY d.cod_atr ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DescuentosCredito entidad = new DescuentosCredito();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_atr = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_DESCUENTO"] != DBNull.Value) entidad.tipo_descuento = Convert.ToInt32(resultado["TIPO_DESCUENTO"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.val_atr = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt32(resultado["COBRA_MORA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToDecimal(resultado["NUMERO_CUOTAS"]);
                            if (resultado["FORMA_DESCUENTO"] != DBNull.Value) entidad.forma_descuento = Convert.ToInt32(resultado["FORMA_DESCUENTO"]);
                            if (resultado["TIPO_IMPUESTO"] != DBNull.Value) entidad.tipo_impuesto = Convert.ToInt32(resultado["TIPO_IMPUESTO"]);
                            lstDescuentosCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDescuentosCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarDescuentosCredito", ex);
                        return null;
                    }
                }
            }
        }

        public DescuentosCredito ModificarDescuentosCredito(DescuentosCredito pDescuentosCredito, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pDescuentosCredito.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pDescuentosCredito.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter ptipo_descuento = cmdTransaccionFactory.CreateParameter();
                        ptipo_descuento.ParameterName = "p_tipo_descuento";
                        if (pDescuentosCredito.tipo_descuento == null)
                            ptipo_descuento.Value = DBNull.Value;
                        else
                            ptipo_descuento.Value = pDescuentosCredito.tipo_descuento;
                        ptipo_descuento.Direction = ParameterDirection.Input;
                        ptipo_descuento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_descuento);

                        DbParameter ptipo_liquidacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_liquidacion.ParameterName = "p_tipo_liquidacion";
                        if (pDescuentosCredito.tipo_liquidacion == null)
                            ptipo_liquidacion.Value = DBNull.Value;
                        else
                            ptipo_liquidacion.Value = pDescuentosCredito.tipo_liquidacion;
                        ptipo_liquidacion.Direction = ParameterDirection.Input;
                        ptipo_liquidacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_liquidacion);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pDescuentosCredito.val_atr;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pcobra_mora = cmdTransaccionFactory.CreateParameter();
                        pcobra_mora.ParameterName = "p_cobra_mora";
                        pcobra_mora.Value = pDescuentosCredito.cobra_mora;
                        pcobra_mora.Direction = ParameterDirection.Input;
                        pcobra_mora.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_mora);

                        DbParameter pnumero_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuotas.ParameterName = "p_numero_cuotas";
                        if (pDescuentosCredito.numero_cuotas == null)
                            pnumero_cuotas.Value = DBNull.Value;
                        else
                            pnumero_cuotas.Value = pDescuentosCredito.numero_cuotas;
                        pnumero_cuotas.Direction = ParameterDirection.Input;
                        pnumero_cuotas.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuotas);

                        DbParameter pforma_descuento = cmdTransaccionFactory.CreateParameter();
                        pforma_descuento.ParameterName = "p_forma_descuento";
                        pforma_descuento.Value = pDescuentosCredito.forma_descuento;
                        pforma_descuento.Direction = ParameterDirection.Input;
                        pforma_descuento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_descuento);

                        DbParameter ptipo_impuesto = cmdTransaccionFactory.CreateParameter();
                        ptipo_impuesto.ParameterName = "p_tipo_impuesto";
                        ptipo_impuesto.Value = pDescuentosCredito.tipo_impuesto;
                        ptipo_impuesto.Direction = ParameterDirection.Input;
                        ptipo_impuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_impuesto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DESCUECRE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDescuentosCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DescuentosCreditoData", "ModificarDescuentosCredito", ex);
                        return null;
                    }
                }
            }
        }

        public List<Credito> ListarCreditosEducativos(DateTime pFecha, string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado;

            List<Credito> lista = new List<Credito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sqlFecha = "";
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sqlFecha = " To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sqlFecha = " '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' ";
                        sql = @"Select h.numero_radicacion, h.cod_linea_credito, l.nombre As nom_linea_credito, h.cod_cliente, p.identificacion, p.nombre, h.cod_oficina, h.fecha_aprobacion, h.fecha_desembolso, 
                                h.monto, h.saldo_capital, h.valor_cuota, h.fecha_proximo_pago, l.maneja_auxilio
                                From Historico_cre h Inner Join lineascredito l On h.cod_linea_credito = l.cod_linea_credito
                                Inner Join v_persona p On h.cod_cliente = p.cod_persona
                                where fecha_historico = " + sqlFecha + pFiltro + " And l.educativo = 1 And h.saldo_capital > 0";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["nom_linea_credito"] != DBNull.Value) entidad.nom_linea_credito = Convert.ToString(resultado["nom_linea_credito"]);
                            if (resultado["cod_cliente"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["cod_cliente"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.codigo_oficina = Convert.ToInt32(resultado["cod_oficina"]);
                            if (resultado["fecha_aprobacion"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["fecha_aprobacion"]);
                            if (resultado["fecha_desembolso"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["fecha_desembolso"]);
                            if (resultado["monto"] != DBNull.Value) entidad.monto_aprobado = Convert.ToDecimal(resultado["monto"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["saldo_capital"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["valor_cuota"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_prox_pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["maneja_auxilio"] != DBNull.Value) entidad.maneja_auxilio = Convert.ToInt32(resultado["maneja_auxilio"]);
                            lista.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCreditosEducativos", ex);
                        return null;
                    }
                }
            }
        }


        //Anderson Acuña  -- Reporte 1026 DIAN    
        public List<Credito> Reporte_1026(Credito pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstPrestamos = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select 
                                    case when C.COD_CLASIFICA = 1 then 2 
                                    when C.COD_CLASIFICA = 2 then 1
                                    when C.COD_CLASIFICA = 3 then 3
                                    when C.COD_CLASIFICA = 4 then 4
                                    end as Codigo, --1 Prestamo Comercial, 2 Prestamo de Consumo, 3 Prestamo Hipotecario, 4 Otros Prestamos
                                    TI.DESCRIPCION as tipo_identificacion,
                                    P.IDENTIFICACION,
                                    case when P.TIPO_IDENTIFICACION = 2 then P.DIGITO_VERIFICACION  else P.DIGITO_VERIFICACION end as DV,
                                    P.PRIMER_APELLIDO,
                                    P.SEGUNDO_APELLIDO,
                                    P.PRIMER_NOMBRE,
                                    P.SEGUNDO_NOMBRE,
                                    case when P.TIPO_PERSONA = 'J' then P.RAZON_SOCIAL else '' end as RAZONSOCIAL,
                                    P.DIRECCION,
                                    CD.DEPENDE_DE as codDep,
                                    P.CODCIUDADRESIDENCIA as codMcp,
                                    sum(C.MONTO_DESEMBOLSADO) as SumValorPres
                                    from credito C
                                    inner join persona P on C.COD_DEUDOR = P.COD_PERSONA
                                    inner join TIPOIDENTIFICACION TI on P.TIPO_IDENTIFICACION = TI.CODTIPOIDENTIFICACION
                                    inner join Ciudades CD on p.CODCIUDADRESIDENCIA = CD.CODCIUDAD 
                                    where FECHA_DESEMBOLSO BETWEEN" + filtro +
                                    @" group by 
                                    C.COD_CLASIFICA,
                                    P.DIGITO_VERIFICACION,
                                    TI.DESCRIPCION,
                                    P.IDENTIFICACION,
                                    P.TIPO_IDENTIFICACION,
                                    P.DIGITO_VERIFICACION,
                                    P.PRIMER_APELLIDO,
                                    P.SEGUNDO_APELLIDO,
                                    P.PRIMER_NOMBRE,
                                    P.SEGUNDO_NOMBRE,
                                    P.TIPO_PERSONA,
                                    P.RAZON_SOCIAL,
                                    P.DIRECCION,
                                    CD.DEPENDE_DE,
                                    P.CODCIUDADRESIDENCIA";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();
                            if (resultado["Codigo"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["Codigo"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DV"] != DBNull.Value) entidad.cod_motivo = Convert.ToInt32(resultado["DV"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["RAZONSOCIAL"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["RAZONSOCIAL"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["codDep"] != DBNull.Value) entidad.cod_Departamento = Convert.ToInt32(resultado["codDep"]);
                            if (resultado["codMcp"] != DBNull.Value) entidad.Cod_MunoCiu = Convert.ToInt32(resultado["codMcp"]);
                            if (resultado["SumValorPres"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SumValorPres"]);
                            lstPrestamos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPrestamos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "Reporte1026", ex);
                        return null;
                    }
                }
            }
        }


        ///<summary>
        ///crear en la importación creditos de forma masiva
        ///</summary>
        ///<param name = "itemcred" > Entidad GAporte</param> S
        ///<param name = "lst_Num_cred">Entidad GAporte</param>S
        ///<returns>Entidad Aporte creada</returns>
        public Credito CrearCreditoImportado(Credito itemcred, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_IDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        P_IDENTIFICACION.ParameterName = "P_IDENTIFICACION";
                        P_IDENTIFICACION.Value = itemcred.identificacion;
                        P_IDENTIFICACION.Direction = ParameterDirection.InputOutput;
                        P_IDENTIFICACION.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_IDENTIFICACION);

                        DbParameter P_COD_LINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        P_COD_LINEA_CREDITO.ParameterName = "P_COD_LINEA_CREDITO";
                        P_COD_LINEA_CREDITO.Value = Convert.ToInt32(itemcred.cod_linea_credito);
                        P_COD_LINEA_CREDITO.Direction = ParameterDirection.Input;
                        P_COD_LINEA_CREDITO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_LINEA_CREDITO);

                        DbParameter P_COD_DESTINACION = cmdTransaccionFactory.CreateParameter();
                        P_COD_DESTINACION.ParameterName = "P_COD_DESTINACION";
                        P_COD_DESTINACION.Value = itemcred.Cod_Destinacion;
                        P_COD_DESTINACION.Direction = ParameterDirection.InputOutput;
                        P_COD_DESTINACION.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_DESTINACION);

                        DbParameter P_MONTO = cmdTransaccionFactory.CreateParameter();
                        P_MONTO.ParameterName = "P_MONTO";
                        P_MONTO.Value = itemcred.monto;
                        P_MONTO.Direction = ParameterDirection.InputOutput;
                        P_MONTO.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_MONTO);

                        DbParameter P_NUMERO_CUOTAS = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_CUOTAS.ParameterName = "P_NUMERO_CUOTAS";
                        P_NUMERO_CUOTAS.Value = itemcred.numero_cuotas;
                        P_NUMERO_CUOTAS.Direction = ParameterDirection.Input;
                        P_NUMERO_CUOTAS.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_CUOTAS);

                        DbParameter P_COD_PERIOCIDAD = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERIOCIDAD.ParameterName = "P_COD_PERIOCIDAD";
                        P_COD_PERIOCIDAD.Value = Convert.ToInt32(itemcred.periodicidad);
                        P_COD_PERIOCIDAD.Direction = ParameterDirection.Input;
                        P_COD_PERIOCIDAD.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERIOCIDAD);

                        DbParameter P_FECHA_PRIMERPAGO = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_PRIMERPAGO.ParameterName = "P_FECHA_PRIMERPAGO";
                        P_FECHA_PRIMERPAGO.Value = itemcred.fecha_prim_pago;
                        P_FECHA_PRIMERPAGO.Direction = ParameterDirection.Input;
                        P_FECHA_PRIMERPAGO.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_PRIMERPAGO);

                        DbParameter P_COD_OFICINA = cmdTransaccionFactory.CreateParameter();
                        P_COD_OFICINA.ParameterName = "P_COD_OFICINA";
                        P_COD_OFICINA.Value = vUsuario.cod_oficina;
                        P_COD_OFICINA.Direction = ParameterDirection.Input;
                        P_COD_OFICINA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_COD_OFICINA);

                        DbParameter P_FECHA_SOLICITUD = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_SOLICITUD.ParameterName = "P_FECHA_SOLICITUD";
                        P_FECHA_SOLICITUD.Value = itemcred.fecha_solicitud;
                        P_FECHA_SOLICITUD.Direction = ParameterDirection.Input;
                        P_FECHA_SOLICITUD.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_SOLICITUD);

                        DbParameter P_FECHA_CREACION = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_CREACION.ParameterName = "P_FECHA_CREACION";
                        P_FECHA_CREACION.Value = itemcred.fecha_creacion;
                        P_FECHA_CREACION.Direction = ParameterDirection.Input;
                        P_FECHA_CREACION.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_CREACION);

                        DbParameter P_USUARIO_CREACION = cmdTransaccionFactory.CreateParameter();
                        P_USUARIO_CREACION.ParameterName = "P_USUARIO_CREACION";
                        P_USUARIO_CREACION.Value = vUsuario.codusuario;
                        P_USUARIO_CREACION.Direction = ParameterDirection.Input;
                        P_USUARIO_CREACION.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_USUARIO_CREACION);

                        DbParameter P_FORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FORMA_PAGO.ParameterName = "P_FORMA_PAGO";
                        if (itemcred.forma_pago == "1")
                            itemcred.forma_pago = "C";
                        else
                            itemcred.forma_pago = "N";
                        P_FORMA_PAGO.Value = itemcred.forma_pago;
                        P_FORMA_PAGO.Direction = ParameterDirection.Input;
                        P_FORMA_PAGO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_FORMA_PAGO);

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = "G";
                        P_ESTADO.Direction = ParameterDirection.Input;
                        P_ESTADO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        DbParameter P_OBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        P_OBSERVACIONES.ParameterName = "P_OBSERVACIONES";
                        P_OBSERVACIONES.Value = "CARGADO MASIVAMENTE";
                        P_OBSERVACIONES.Direction = ParameterDirection.Input;
                        P_OBSERVACIONES.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_OBSERVACIONES);

                        DbParameter P_NUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_RADICACION.ParameterName = "P_NUMERO_RADICACION";
                        P_NUMERO_RADICACION.Value = itemcred.numero_radicacion;
                        P_NUMERO_RADICACION.Direction = ParameterDirection.Output;
                        P_NUMERO_RADICACION.DbType = DbType.UInt64;
                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_RADICACION);

                        DbParameter P_IDENPROV = cmdTransaccionFactory.CreateParameter();
                        P_IDENPROV.ParameterName = "P_IDENPROV";
                        P_IDENPROV.Value = itemcred.idenprov;
                        P_IDENPROV.Direction = ParameterDirection.Input;
                        P_IDENPROV.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_IDENPROV);

                        DbParameter P_ERROR = cmdTransaccionFactory.CreateParameter();
                        P_ERROR.ParameterName = "P_ERROR";
                        P_ERROR.Value = itemcred.mensaje;
                        P_ERROR.Direction = ParameterDirection.Output;
                        P_ERROR.DbType = DbType.AnsiString;
                        cmdTransaccionFactory.Parameters.Add(P_ERROR);
                        P_ERROR.Size = 1000;

                        DbParameter P_DESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        P_DESCRIPCION.ParameterName = "P_DESCRIPCION";
                        P_DESCRIPCION.Value = itemcred.descrpcion;
                        P_DESCRIPCION.Direction = ParameterDirection.Input;
                        P_DESCRIPCION.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_DESCRIPCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_MASIVO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        if (P_NUMERO_RADICACION.Value != null)
                        {

                            if (P_NUMERO_RADICACION.Value != DBNull.Value)
                            {
                                itemcred.idenprov = Convert.ToString(P_IDENTIFICACION.Value);
                                itemcred.numero_radicacion = Convert.ToInt64(P_NUMERO_RADICACION.Value);
                                itemcred.Cod_Destinacion = Convert.ToInt32(P_COD_DESTINACION.Value);
                                itemcred.monto = Convert.ToDecimal(P_MONTO.Value);
                                itemcred.mensaje = Convert.ToString(P_ERROR.Value);
                            }


                        }

                        return itemcred;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "CrearCreditoMasivo", ex);
                        return null;
                    }
                }
            }
        }





        public Credito ConsultarCierreCartera(Usuario vUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT MAX(FECHA) as fecha,estado FROM CIEREA WHERE TIPO = 'R' AND ESTADO = 'D'   group by estado";
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


        public bool ConsultarVacaciones(string pIdentificacion, DateTime pfecha, Usuario pUsuario)
        {
            DbDataReader resultado;
            DateTime fechaInicial = DateTime.MinValue;
            DateTime fechafinal = DateTime.MinValue;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT v.FECHA_INICIAL, v.FECHA_FINAL FROM VACACIONES v JOIN PERSONA p ON v.cod_persona = p.cod_persona WHERE p.identificacion = '" + pIdentificacion + "' AND TIPO_CALCULO = 0";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA_INICIAL"] != DBNull.Value) fechaInicial = Convert.ToDateTime(resultado["FECHA_INICIAL"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) fechafinal = Convert.ToDateTime(resultado["FECHA_FINAL"]);

                            if (fechaInicial != null && fechafinal != null)
                                if (fechaInicial <= pfecha && fechafinal >= pfecha)
                                    return true;
                        }
                        else
                        {
                            dbConnectionFactory.CerrarConexion(connection);
                            return false;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }



    }
}