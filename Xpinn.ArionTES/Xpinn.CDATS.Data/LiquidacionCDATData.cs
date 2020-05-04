using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Data
{
    public class LiquidacionCDATData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public LiquidacionCDATData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }



        public void GENERAR_LiquidacionCDAT(LiquidacionCDAT pCdat, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha_liquidacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_liquidacion.ParameterName = "p_fecha_liquidacion";
                        pfecha_liquidacion.Value = pCdat.fecha_liquidacion;
                        pfecha_liquidacion.Direction = ParameterDirection.Input;
                        pfecha_liquidacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_liquidacion);

                        DbParameter pnumero_cdat = cmdTransaccionFactory.CreateParameter();
                        pnumero_cdat.ParameterName = "p_numero_cdat";
                        pnumero_cdat.Value = pCdat.numero_cdat;
                        pnumero_cdat.Direction = ParameterDirection.Input;
                        pnumero_cdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cdat);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pCdat.identificacion == null || pCdat.identificacion.Trim() == "")
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pCdat.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_LIQUIDACION";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCDATData", "GENERAR_LiquidacionCDAT", ex);
                    }
                }
            }
        }


        public List<LiquidacionCDAT> ListarTemporal_LiquidacionCDAT(LiquidacionCDAT pTemp, DateTime pFecha, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionCDAT> lstTemp = new List<LiquidacionCDAT>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT * FROM TEMP_LIQUIDACION_CDAT Order by NUMERO_CDAT";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LiquidacionCDAT entidad = new LiquidacionCDAT();
                            if (resultado["FECHA_LIQUIDACION"] != DBNull.Value) entidad.fecha_liquidacion = Convert.ToDateTime(resultado["FECHA_LIQUIDACION"]);
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt64(resultado["CODIGO_CDAT"]);
                            if (resultado["NUMERO_CDAT"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["NUMERO_CDAT"]);
                            if (resultado["FECHA_INICIAL"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["FECHA_INICIAL"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FINAL"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHA_INT"] != DBNull.Value) entidad.fecha_int = Convert.ToDateTime(resultado["FECHA_INT"]);
                            if (resultado["INTERES"] != DBNull.Value) entidad.interes = Convert.ToDecimal(resultado["INTERES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToDecimal(resultado["RETENCION"]);
                            if (resultado["DIAS_LIQUIDA"] != DBNull.Value) entidad.dias_liquida = Convert.ToDecimal(resultado["DIAS_LIQUIDA"]);
                            if (resultado["INTERES_CAUSADO"] != DBNull.Value) entidad.interes_causado = Convert.ToDecimal(resultado["INTERES_CAUSADO"]);
                            if (resultado["RETENCION_CAUSADO"] != DBNull.Value) entidad.retencion_causado = Convert.ToDecimal(resultado["RETENCION_CAUSADO"]);
                            if (resultado["VALOR_GMF"] != DBNull.Value) entidad.valor_gmf = Convert.ToDecimal(resultado["VALOR_GMF"]);
                            if (resultado["INTERES_NETO"] != DBNull.Value) entidad.interes_neto = Convert.ToDecimal(resultado["INTERES_NETO"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["CTA_AHORROS"] != DBNull.Value) entidad.cta_ahorros = Convert.ToString(resultado["CTA_AHORROS"]);
                            entidad.totalinteres = entidad.interes + entidad.interes_causado;
                            lstTemp.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTemp;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCDATData", "ListarTemporal_LiquidacionCDAT", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionCDAT CrearLiquidacionCDAT(LiquidacionCDAT pLiqui, Usuario vUsuario)
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
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = pLiqui.codigo_cdat;
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
                        if (pLiqui.tasa != 0) ptasa.Value = pLiqui.tasa; else ptasa.Value = DBNull.Value;
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
                        if (pLiqui.interes != 0) pintereses.Value = pLiqui.interes; else pintereses.Value = DBNull.Value;
                        pintereses.Direction = ParameterDirection.Input;
                        pintereses.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pintereses);

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "p_retencion";
                        if (pLiqui.retencion != 0) pretencion.Value = pLiqui.retencion; else pretencion.Value = DBNull.Value;
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
                        if (pLiqui.valor != 0) pvalor_neto.Value = pLiqui.valor; else pvalor_neto.Value = DBNull.Value;//VALOR NETO
                        pvalor_neto.Direction = ParameterDirection.Input;
                        pvalor_neto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_neto);

                        DbParameter pinteres_causado = cmdTransaccionFactory.CreateParameter();
                        pinteres_causado.ParameterName = "p_interes_causado";
                        if (pLiqui.interes_causado != 0) pinteres_causado.Value = pLiqui.interes_causado; else pinteres_causado.Value = DBNull.Value;
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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_LIQUIDACIO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pLiqui.cod_interes = Convert.ToInt32(pcod_interes.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLiqui;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCDATData", "CrearLiquidacionCDAT", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// CALCULAR LIQUIDACION --- CIERRE CDAT
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="pusuario"></param>
        /// <returns></returns>
        public LiquidacionCDAT CalculoLiquidacionCDAT(LiquidacionCDAT pLiqui, Usuario pusuario)
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

                        DbParameter pnumero_cdat = cmdTransaccionFactory.CreateParameter();
                        pnumero_cdat.ParameterName = "pnumero_cdat";
                        pnumero_cdat.Value = pLiqui.numero_cdat;
                        pnumero_cdat.Direction = ParameterDirection.Input;
                        pnumero_cdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cdat);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pLiqui.valor;
                        pvalor.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pinteres_capitalzado = cmdTransaccionFactory.CreateParameter();
                        pinteres_capitalzado.ParameterName = "pinteres_capitalzado";
                        pinteres_capitalzado.Value = pLiqui.interes_causado;
                        pinteres_capitalzado.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pinteres_capitalzado);

                        DbParameter pinteres = cmdTransaccionFactory.CreateParameter();
                        pinteres.ParameterName = "pinteres";
                        pinteres.Value = pLiqui.interes;
                        pinteres.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pinteres);                                                
                  
                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "pretencion";
                        pretencion.Value = pLiqui.retencion;
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

                        DbParameter p_origen = cmdTransaccionFactory.CreateParameter();
                        p_origen.ParameterName = "p_origen";
                        p_origen.Value = pLiqui.origen;
                        p_origen.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_origen);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText =  "USP_XPINN_CDA_LIQUIDACDAT" ;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiqui.valor = Convert.ToDecimal(pvalor.Value);
                        pLiqui.interes_causado = Convert.ToDecimal(pinteres_capitalzado.Value);
                        pLiqui.interes = Convert.ToDecimal(pinteres.Value);
                        pLiqui.retencion = Convert.ToDecimal(pretencion.Value);
                        pLiqui.valor_gmf = Convert.ToDecimal(pgmf.Value);
                        pLiqui.valor_pagar = Convert.ToDecimal(pvalor_a_pagar.Value);

                        return pLiqui;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCDATData", "CalculoLiquidacionCDAT", ex);
                        return null;
                    }
                }
            }
        }


        public void CierreLiquidacionCDAT(LiquidacionCDAT pLiqui, ref string psError, Usuario pusuario)
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

                        DbParameter pnumero_cdat = cmdTransaccionFactory.CreateParameter();
                        pnumero_cdat.ParameterName = "pnumero_cdat";
                        pnumero_cdat.Value = pLiqui.numero_cdat;
                        pnumero_cdat.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cdat);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcod_ope";
                        pcod_ope.Value = pLiqui.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pLiqui.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pinteres_capitalzado = cmdTransaccionFactory.CreateParameter();
                        pinteres_capitalzado.ParameterName = "pinteres_capitalzado";
                        pinteres_capitalzado.Value = pLiqui.interes_causado;
                        pinteres_capitalzado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pinteres_capitalzado);

                        DbParameter pinteres = cmdTransaccionFactory.CreateParameter();
                        pinteres.ParameterName = "pinteres";
                        pinteres.Value = pLiqui.interes;
                        pinteres.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pinteres);

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "pretencion";
                        pretencion.Value = pLiqui.retencion;
                        pretencion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pretencion);

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

                        DbParameter pn_liquida_interes = cmdTransaccionFactory.CreateParameter();
                        pn_liquida_interes.ParameterName = "pn_liquida_interes";
                        pn_liquida_interes.Value = pLiqui.capitalizar_int;
                        pn_liquida_interes.Direction = ParameterDirection.Input;
                        pn_liquida_interes.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pn_liquida_interes);

                        DbParameter perror = cmdTransaccionFactory.CreateParameter();
                        perror.ParameterName = "perror";
                        perror.Direction = ParameterDirection.Output;
                        perror.DbType = DbType.StringFixedLength;
                        perror.Size = 500;
                        cmdTransaccionFactory.Parameters.Add(perror);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_CIERRECDAT";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pLiqui, "CDAT", pusuario, Accion.Crear.ToString(), TipoAuditoria.CDAT, "Creacion de cierre de CDAT con numero de cuenta " + pLiqui.numero_cdat);

                        if (perror != null)
                            if (perror.Value != null)
                                psError = perror.Value.ToString();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCDATData", "CierreLiquidacionCDAT", ex);
                    }
                }
            }

        }

        public void LiquidacionInteresCDAT(LiquidacionCDAT pLiqui, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pnumero_cdat = cmdTransaccionFactory.CreateParameter();
                        pnumero_cdat.ParameterName = "pnumero_cdat";
                        pnumero_cdat.Value = pLiqui.codigo_cdat;
                        pnumero_cdat.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cdat);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcod_ope";
                        pcod_ope.Value = pLiqui.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pLiqui.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pinteres = cmdTransaccionFactory.CreateParameter();
                        pinteres.ParameterName = "pinteres";
                        pinteres.Value = pLiqui.interes;
                        pinteres.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pinteres);

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "pretencion";
                        pretencion.Value = pLiqui.retencion;
                        pretencion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pretencion);

                        DbParameter pgmf = cmdTransaccionFactory.CreateParameter();
                        pgmf.ParameterName = "pgmf";
                        pgmf.Value = pLiqui.valor_gmf;
                        pgmf.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pgmf);

                        DbParameter p_num_tran = cmdTransaccionFactory.CreateParameter();
                        p_num_tran.ParameterName = "p_num_tran";
                        p_num_tran.Value = 0;
                        p_num_tran.Direction = ParameterDirection.Output;
                        p_num_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_num_tran);

                        DbParameter pinterescausado = cmdTransaccionFactory.CreateParameter();
                        pinterescausado.ParameterName = "pinterescausado";
                        pinterescausado.Value = pLiqui.interes_causado;
                        pinterescausado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pinterescausado);

                        DbParameter pretencioncausado = cmdTransaccionFactory.CreateParameter();
                        pretencioncausado.ParameterName = "pretencioncausado";
                        pretencioncausado.Value = pLiqui.retencion_causado;
                        pretencioncausado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pretencioncausado);

                        DbParameter pn_fecha_interes = cmdTransaccionFactory.CreateParameter();
                        pn_fecha_interes.ParameterName = "pn_fecha_interes";
                        pn_fecha_interes.Value = pLiqui.fecha_int;
                        pn_fecha_interes.Direction = ParameterDirection.Input;
                        pn_fecha_interes.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pn_fecha_interes);

                        DbParameter pn_liquida_interes = cmdTransaccionFactory.CreateParameter();
                        pn_liquida_interes.ParameterName = "pn_liquida_interes";
                        pn_liquida_interes.Value = pLiqui.capitalizar_int;
                        pn_liquida_interes.Direction = ParameterDirection.Input;
                        pn_liquida_interes.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pn_liquida_interes);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_LIQU_INT_CDAT";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCDATData", "CierreLiquidacionCDAT", ex);
                    }
                }
            }

        }

        public LiquidacionCDAT Listartitular(LiquidacionCDAT pTemp, Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionCDAT entidad = new LiquidacionCDAT();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        //string sql = @"SELECT * FROM persona where identificacion ="'    pTemp.identificacion +"'" + Order by NUMERO_CDAT";
                        string sql = " SELECT cod_persona FROM PERSONA where identificacion like '" + pTemp.identificacion + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            //  LiquidacionCDAT entidad = new LiquidacionCDAT();

                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["cod_persona"]);

                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCDATData", "ConsultarPersona", ex);
                        return null;
                    }



                }
            }

        }



        public LiquidacionCDAT ConsultarCierreCdats(Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionCDAT entidad = new LiquidacionCDAT();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT MAX(FECHA) as fecha,estado FROM CIEREA WHERE TIPO = 'M' AND ESTADO = 'D'   group by estado";
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

    }
}
