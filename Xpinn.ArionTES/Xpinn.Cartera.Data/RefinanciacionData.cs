using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;

namespace Xpinn.Cartera.Data
{
    public class RefinanciacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla  cierre histórico
        /// </summary>
        public RefinanciacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Listar créditos a refinanciar
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(DateTime? pFecha, String filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.FabricaCreditos.Entities.Credito> lstCredito = new List<Xpinn.FabricaCreditos.Entities.Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string columna = "";
                        if (pFecha != null)
                        {
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                columna = "Calcular_TotalAPagar(v_creditos.numero_radicacion, To_Date('" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "'))";
                            else
                                columna = "Calcular_TotalAPagar(v_creditos.numero_radicacion, '" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "')";
                        }
                        else
                        {
                            columna = " 0 ";
                        }
                        string sql = "Select v_creditos.*, " + columna + " As TotalAPagar From v_creditos " + filtro ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Credito entidad = new Xpinn.FabricaCreditos.Entities.Credito();                            
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["LINEA"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["TIPO_REFINANCIA"] != DBNull.Value) entidad.tipo_refinancia = Convert.ToInt32(resultado["TIPO_REFINANCIA"]);
                            if (resultado["MAXIMO_REFINANCIA"] != DBNull.Value) entidad.maximo_refinancia = Convert.ToInt64(resultado["MAXIMO_REFINANCIA"]);
                            if (resultado["MINIMO_REFINANCIA"] != DBNull.Value) entidad.minimo_refinancia = Convert.ToInt64(resultado["MINIMO_REFINANCIA"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["TOTALAPAGAR"] != DBNull.Value) entidad.total_a_pagar = Convert.ToDecimal(resultado["TOTALAPAGAR"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["MINIMO_REFINANCIA"] != DBNull.Value) entidad.minimo_refinancia = Convert.ToInt64(resultado["MINIMO_REFINANCIA"]);
                            if (resultado["MAXIMO_REFINANCIA"] != DBNull.Value) entidad.maximo_refinancia = Convert.ToInt64(resultado["MAXIMO_REFINANCIA"]);
                            // Determinar el tipo de refinanciacion
                            entidad.valor_para_refinanciar = entidad.total_a_pagar;
                            if (entidad.tipo_refinancia == 0)            // Control de refinanciación según el saldo
                            {
                                entidad.nomtipo_refinancia = "Rango Saldo";
                                if (entidad.total_a_pagar > entidad.maximo_refinancia)
                                {
                                    entidad.valor_para_refinanciar = entidad.total_a_pagar - entidad.maximo_refinancia;
                                }
                            }
                            else if (entidad.tipo_refinancia == 2)       // Según porcentaje del saldo
                            {
                                entidad.nomtipo_refinancia = "% Saldo";
                                decimal porcentaje = 0;
                                if (entidad.monto != 0)
                                { 
                                    porcentaje = (Convert.ToDecimal(entidad.saldo) / Convert.ToDecimal(entidad.monto)) * 100;
                                    if (porcentaje > entidad.maximo_refinancia)
                                    {
                                       entidad.valor_para_refinanciar = Math.Round(entidad.monto * ((porcentaje - entidad.maximo_refinancia) / 100));
                                    }
                                }
                            }
                            else if (entidad.tipo_refinancia == 3)       // Según porcentaje de cuotas pagadas
                            {
                                decimal porcentaje = 0;
                                if (entidad.plazo != 0)
                                    porcentaje = (Convert.ToDecimal(entidad.cuotas_pagadas) / Convert.ToDecimal(entidad.plazo)) * 100;
                                else
                                    porcentaje = 100;
                                if (porcentaje < entidad.minimo_refinancia)
                                {
                                    decimal porcFaltante = 0 , cuotasFaltantes = 0;
                                    porcFaltante = entidad.minimo_refinancia - porcentaje;
                                    cuotasFaltantes = ((porcFaltante * entidad.plazo) / 100);
                                    entidad.valor_para_refinanciar = Math.Round(entidad.valor_cuota * cuotasFaltantes);
                                }
                            }
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RefinanciacionData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }

        public decimal TotalaPagarCredito(Int64 pNumeroRadicacion, DateTime pFecha, Usuario vusuario)
        {
            decimal valor_TotalAPagar = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vusuario))
            {
                DbDataReader resultado = default(DbDataReader);
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string columna = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            columna = "Calcular_TotalAPagar(c.numero_radicacion, To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "'))";
                        else
                            columna = "Calcular_TotalAPagar(c.numero_radicacion, '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "')";
                        string sql = "Select " + columna + " AS TotalAPagar From credito c Where c.numero_radicacion = " + pNumeroRadicacion.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Credito calcularTotal = new Xpinn.FabricaCreditos.Entities.Credito();
                            if (resultado["TotalAPagar"] != DBNull.Value) valor_TotalAPagar = Convert.ToInt32(resultado["TotalAPagar"]);

                            return valor_TotalAPagar;
                        }

                        return valor_TotalAPagar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RefinanciacionData", "TotalaPagarCredito", ex);
                        return valor_TotalAPagar;
                    }
                }
            }
        }


        public Refinanciacion CrearRefinanciacion(Refinanciacion pRefinanciacion, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "pnumero_radicacion";
                        pNUMERO_RADICACION.DbType = DbType.Int64;
                        pNUMERO_RADICACION.Value = pRefinanciacion.numero_radicacion;
                        pNUMERO_RADICACION.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);

                        DbParameter pFECHAREFINANCIACION = cmdTransaccionFactory.CreateParameter();
                        pFECHAREFINANCIACION.ParameterName = "pfecha_refinanciacion";
                        pFECHAREFINANCIACION.DbType = DbType.Date;
                        pFECHAREFINANCIACION.Value = pRefinanciacion.fecha_refinancia;
                        pFECHAREFINANCIACION.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pFECHAREFINANCIACION);

                        DbParameter pPLAZO = cmdTransaccionFactory.CreateParameter();
                        pPLAZO.ParameterName = "pplazo";
                        pPLAZO.DbType = DbType.Int64;
                        pPLAZO.Value = pRefinanciacion.plazo_ref;
                        pPLAZO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pPLAZO);

                        DbParameter pFECHAPROXIMOPAGO = cmdTransaccionFactory.CreateParameter();
                        pFECHAPROXIMOPAGO.ParameterName = "pfecha_proximo_pago";
                        pFECHAPROXIMOPAGO.DbType = DbType.Date;
                        pFECHAPROXIMOPAGO.Value = pRefinanciacion.fecha_prox_pago_ref;
                        pFECHAPROXIMOPAGO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pFECHAPROXIMOPAGO);

                        DbParameter pCUOTA = cmdTransaccionFactory.CreateParameter();
                        pCUOTA.ParameterName = "pcuota";
                        pCUOTA.DbType = DbType.Decimal;
                        pCUOTA.Value = pRefinanciacion.cuota_ref;
                        pCUOTA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pCUOTA);

                        DbParameter pVENCIMIENTO = cmdTransaccionFactory.CreateParameter();
                        pVENCIMIENTO.ParameterName = "pfecha_vencimiento";
                        pVENCIMIENTO.DbType = DbType.Date;
                        pVENCIMIENTO.Value = pRefinanciacion.fecha_vencimiento_ref;
                        pVENCIMIENTO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pVENCIMIENTO);

                        DbParameter pVALORPAGO = cmdTransaccionFactory.CreateParameter();
                        pVALORPAGO.ParameterName = "pvalor_pago";
                        pVALORPAGO.DbType = DbType.Decimal;
                        pVALORPAGO.Value = pRefinanciacion.valor_pago;
                        pVALORPAGO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pVALORPAGO);

                        DbParameter pVALORREFINANCIA = cmdTransaccionFactory.CreateParameter();
                        pVALORREFINANCIA.ParameterName = "pvalor_refinancia";
                        pVALORREFINANCIA.DbType = DbType.Decimal;
                        pVALORREFINANCIA.Value = pRefinanciacion.valor_refinancia;
                        pVALORREFINANCIA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pVALORREFINANCIA);

                        DbParameter pcod_linea_reestruc = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_reestruc.ParameterName = "pcod_linea_reestruc";
                        pcod_linea_reestruc.DbType = DbType.Int64;
                        pcod_linea_reestruc.Value = pRefinanciacion.cod_linea_resstruc;
                        pcod_linea_reestruc.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_reestruc);

                        DbParameter pCODUSU = cmdTransaccionFactory.CreateParameter();
                        pCODUSU.ParameterName = "pcod_usu";
                        pCODUSU.DbType = DbType.Int64;
                        pCODUSU.Value = pRefinanciacion.cod_usuario;
                        pCODUSU.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pCODUSU);

                        DbParameter pCODOFI = cmdTransaccionFactory.CreateParameter();
                        pCODOFI.ParameterName = "pcod_ofi";
                        pCODOFI.DbType = DbType.Int64;
                        pCODOFI.Value = pUsuario.cod_oficina;
                        pCODOFI.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pCODOFI);

                        DbParameter p_cod_nueva_periodicidad = cmdTransaccionFactory.CreateParameter();
                        p_cod_nueva_periodicidad.ParameterName = "p_cod_nueva_periodicidad";
                        p_cod_nueva_periodicidad.DbType = DbType.Int64;
                        p_cod_nueva_periodicidad.Value = pRefinanciacion.cod_nueva_periodicidad;
                        p_cod_nueva_periodicidad.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_cod_nueva_periodicidad);

                        DbParameter pCODOPE = cmdTransaccionFactory.CreateParameter();
                        pCODOPE.ParameterName = "pcod_ope";
                        pCODOPE.DbType = DbType.Int64;
                        pCODOPE.Value = 0;
                        pCODOPE.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pCODOPE);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_REFINANCIACION";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pRefinanciacion, "Refinanciacion", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pRefinanciacion.cod_ope = Convert.ToInt64(pCODOPE.Value);

                        return pRefinanciacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RefinanciacionData", "CrearRefinanciacion", ex);
                        return null;
                    }
                }
            }
        }

        public AtributosRefinanciar CrearAtributosRefinanciar(AtributosRefinanciar pAtributosRefinanciar, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pAtributosRefinanciar.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pAtributosRefinanciar.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pnom_atr = cmdTransaccionFactory.CreateParameter();
                        pnom_atr.ParameterName = "p_nom_atr";
                        pnom_atr.Value = pAtributosRefinanciar.nom_atr;
                        pnom_atr.Direction = ParameterDirection.Input;
                        pnom_atr.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_atr);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pAtributosRefinanciar.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter prefinanciar = cmdTransaccionFactory.CreateParameter();
                        prefinanciar.ParameterName = "p_refinanciar";
                        prefinanciar.Value = pAtributosRefinanciar.refinanciar;
                        prefinanciar.Direction = ParameterDirection.Input;
                        prefinanciar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(prefinanciar);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_ATRIBREF_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAtributosRefinanciar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RefinanciacionData", "CrearAtributosRefinanciar", ex);
                        return null;
                    }
                }
            }
        }


        public long Cuotas_Pagas { get; set; }
    }
}
