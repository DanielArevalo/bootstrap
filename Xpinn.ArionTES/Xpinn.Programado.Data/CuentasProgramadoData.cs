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
    public class CuentasProgramadoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public CuentasProgramadoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public CuentasProgramado Crear_ModAhorroProgramado(CuentasProgramado pCuentas, Usuario vUsuario, Int32 opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        CuentasProgramado cuentaAnterior = null;
                        // Si estoy modificando la cuenta
                        if (opcion == 1)
                        {
                            cuentaAnterior = ConsultarAhorroProgramado(Convert.ToString(pCuentas.numero_programado), vUsuario);
                        }

                        DbParameter pnumero_programado = cmdTransaccionFactory.CreateParameter();
                        pnumero_programado.ParameterName = "p_numero_programado";
                        pnumero_programado.Value = pCuentas.numero_programado;
                        pnumero_programado.Direction = ParameterDirection.Input;
                        pnumero_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_programado);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pCuentas.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pcod_linea_programado = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_programado.ParameterName = "p_cod_linea_programado";
                        pcod_linea_programado.Value = pCuentas.cod_linea_programado;
                        pcod_linea_programado.Direction = ParameterDirection.Input;
                        pcod_linea_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_programado);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pCuentas.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha_apertura = cmdTransaccionFactory.CreateParameter();
                        pfecha_apertura.ParameterName = "p_fecha_apertura";
                        pfecha_apertura.Value = pCuentas.fecha_apertura;
                        pfecha_apertura.Direction = ParameterDirection.Input;
                        pfecha_apertura.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_apertura);

                        DbParameter pfecha_primera_cuota = cmdTransaccionFactory.CreateParameter();
                        pfecha_primera_cuota.ParameterName = "p_fecha_primera_cuota";
                        pfecha_primera_cuota.Value = pCuentas.fecha_primera_cuota;
                        pfecha_primera_cuota.Direction = ParameterDirection.Input;
                        pfecha_primera_cuota.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_primera_cuota);

                        DbParameter pfecha_cierre = cmdTransaccionFactory.CreateParameter();
                        pfecha_cierre.ParameterName = "p_fecha_cierre";
                        if (pCuentas.fecha_cierre != DateTime.MinValue) pfecha_cierre.Value = pCuentas.fecha_cierre; else pfecha_cierre.Value = DBNull.Value;
                        pfecha_cierre.Direction = ParameterDirection.Input;
                        pfecha_cierre.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_cierre);

                        DbParameter pvalor_cuota = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota.ParameterName = "p_valor_cuota";
                        pvalor_cuota.Value = pCuentas.valor_cuota;
                        pvalor_cuota.Direction = ParameterDirection.Input;
                        pvalor_cuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota);

                        DbParameter pplazo = cmdTransaccionFactory.CreateParameter();
                        pplazo.ParameterName = "p_plazo";
                        pplazo.Value = pCuentas.plazo;
                        pplazo.Direction = ParameterDirection.Input;
                        pplazo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pplazo);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        pcod_periodicidad.Value = pCuentas.cod_periodicidad;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter pfecha_ultimo_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_ultimo_pago.ParameterName = "p_fecha_ultimo_pago";
                        if (pCuentas.fecha_ultimo_pago != DateTime.MinValue) pfecha_ultimo_pago.Value = pCuentas.fecha_ultimo_pago; else pfecha_ultimo_pago.Value = DBNull.Value;
                        pfecha_ultimo_pago.Direction = ParameterDirection.Input;
                        pfecha_ultimo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ultimo_pago);

                        DbParameter pfecha_proximo_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_proximo_pago.ParameterName = "p_fecha_proximo_pago";
                        pfecha_proximo_pago.Value = pCuentas.fecha_proximo_pago;
                        pfecha_proximo_pago.Direction = ParameterDirection.Input;
                        pfecha_proximo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_proximo_pago);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        psaldo.Value = pCuentas.saldo;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pCuentas.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pCuentas.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pcuotas_pagadas = cmdTransaccionFactory.CreateParameter();
                        pcuotas_pagadas.ParameterName = "p_cuotas_pagadas";
                        pcuotas_pagadas.Value = pCuentas.cuotas_pagadas;
                        pcuotas_pagadas.Direction = ParameterDirection.Input;
                        pcuotas_pagadas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuotas_pagadas);

                        DbParameter pcalculo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcalculo_tasa.ParameterName = "p_calculo_tasa";
                        pcalculo_tasa.Value = pCuentas.calculo_tasa;
                        pcalculo_tasa.Direction = ParameterDirection.Input;
                        pcalculo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcalculo_tasa);

                        DbParameter ptasa_interes = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes.ParameterName = "p_tasa_interes";
                        if (pCuentas.tasa_interes != 0) ptasa_interes.Value = pCuentas.tasa_interes; else ptasa_interes.Value = DBNull.Value;
                        ptasa_interes.Direction = ParameterDirection.Input;
                        ptasa_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes);

                        DbParameter pcod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        if (pCuentas.cod_tipo_tasa != 0) pcod_tipo_tasa.Value = pCuentas.cod_tipo_tasa; else pcod_tipo_tasa.Value = DBNull.Value;
                        pcod_tipo_tasa.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pCuentas.tipo_historico != 0) ptipo_historico.Value = pCuentas.tipo_historico; else ptipo_historico.Value = DBNull.Value;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pCuentas.desviacion != 0) pdesviacion.Value = pCuentas.desviacion; else pdesviacion.Value = DBNull.Value;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter pfecha_interes = cmdTransaccionFactory.CreateParameter();
                        pfecha_interes.ParameterName = "p_fecha_interes";
                        if (pCuentas.fecha_interes != DateTime.MinValue)
                            pfecha_interes.Value = pCuentas.fecha_interes;
                        else
                            pfecha_interes.Value = DBNull.Value;
                        pfecha_interes.Direction = ParameterDirection.Input;
                        pfecha_interes.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_interes);

                        DbParameter ptotal_interes = cmdTransaccionFactory.CreateParameter();
                        ptotal_interes.ParameterName = "p_total_interes";
                        if (pCuentas.total_interes != 0)
                            ptotal_interes.Value = pCuentas.total_interes;
                        else ptotal_interes.Value = 0;
                        ptotal_interes.Direction = ParameterDirection.Input;
                        ptotal_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptotal_interes);

                        DbParameter ptotal_retencion = cmdTransaccionFactory.CreateParameter();
                        ptotal_retencion.ParameterName = "p_total_retencion";
                        if (pCuentas.total_retencion != 0)
                            ptotal_retencion.Value = pCuentas.total_retencion;
                        else
                            ptotal_retencion.Value = 0;
                        ptotal_retencion.Direction = ParameterDirection.Input;
                        ptotal_retencion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptotal_retencion);

                        DbParameter pcod_motivo_apertura = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo_apertura.ParameterName = "p_cod_motivo_apertura";
                        if (pCuentas.cod_motivo_apertura != 0) pcod_motivo_apertura.Value = pCuentas.cod_motivo_apertura; else pcod_motivo_apertura.Value = DBNull.Value;
                        pcod_motivo_apertura.Direction = ParameterDirection.Input;
                        pcod_motivo_apertura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo_apertura);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCuentas.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter p_cod_asesor = cmdTransaccionFactory.CreateParameter();
                        p_cod_asesor.ParameterName = "p_cod_asesor";
                        if (pCuentas.cod_asesor.HasValue && pCuentas.cod_asesor != 0)
                        {
                            p_cod_asesor.Value = pCuentas.cod_asesor;
                        }
                        else
                        {
                            p_cod_asesor.Value = DBNull.Value;
                        }

                        p_cod_asesor.Direction = ParameterDirection.Input;
                        p_cod_asesor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_asesor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_AHORRO_PRO_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_AHORRO_PRO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (opcion == 1)
                        {
                            DAauditoria.InsertarLog(pCuentas, "Ahorro_programado", vUsuario, Accion.Crear.ToString(), TipoAuditoria.AhorroProgramado, "Creacion de ahorro programado con numero de programado " + pCuentas.numero_programado);
                        }
                        else
                        {
                            DAauditoria.InsertarLog(pCuentas, "Ahorro_programado", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.AhorroProgramado, "Modificacion de ahorro programado con numero de programado " + pCuentas.numero_programado, cuentaAnterior);
                        }

                        return pCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "Crear_ModAhorroProgramado", ex);
                        return null;
                    }
                }
            }
        }




        public void EliminarAhorroProgramado(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM AHORRO_PROGRAMADO WHERE NUMERO_PROGRAMADO = '" + pId.ToString() + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "EliminarAhorroProgramado", ex);
                    }
                }
            }
        }

        public List<CuentasProgramado> ListarAhorrosProgramado(String pFiltro, DateTime pFechaApe, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CuentasProgramado> lstConsulta = new List<CuentasProgramado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT A.*, CALCULAR_CUOTA_PROGRAMADO(A.NUMERO_PROGRAMADO) as CUOTA_CALCULADA,L.NOMBRE AS NOMLINEA,O.NOMBRE AS NOMOFICINA,M.DESCRIPCION AS NOMMOTIVO_PROGRA,V.IDENTIFICACION,V.NOMBRE, "
                                + "CASE A.FORMA_PAGO WHEN 1 THEN 'CAJA' WHEN 2 THEN 'NÓMINA' END AS NOMFORMA_PAGO, "
                                + "CASE A.ESTADO WHEN 0 THEN 'INACTIVO' WHEN 1 THEN 'ACTIVO' END AS NOM_ESTADO, P.DESCRIPCION AS NOMPERIODICIDAD, "
                                + "(Select sum(p.saldo) From pendiente_programado p Where p.numero_programado=a.numero_programado) As pendiente "
                                + "FROM AHORRO_PROGRAMADO A INNER JOIN LINEAPROGRAMADO L ON L.COD_LINEA_PROGRAMADO = A.COD_LINEA_PROGRAMADO "
                                + "LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA "
                                + "LEFT JOIN MOTIVO_PROGRAMADO M ON M.COD_MOTIVO_APERTURA = A.COD_MOTIVO_APERTURA "
                                + "LEFT JOIN V_PERSONA V ON A.COD_PERSONA = V.COD_PERSONA "
                                + "LEFT JOIN PERIODICIDAD P ON P.COD_PERIODICIDAD = A.COD_PERIODICIDAD " + pFiltro;

                        if (pFechaApe != null && pFechaApe != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " A.FECHA_APERTURA = To_Date('" + Convert.ToDateTime(pFechaApe).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " A.FECHA_APERTURA = '" + Convert.ToDateTime(pFechaApe).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        sql += " ORDER BY NUMERO_PROGRAMADO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentasProgramado entidad = new CuentasProgramado();
                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numero_programado = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_PRIMERA_CUOTA"] != DBNull.Value) entidad.fecha_primera_cuota = Convert.ToDateTime(resultado["FECHA_PRIMERA_CUOTA"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["CUOTA_CALCULADA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["CUOTA_CALCULADA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt64(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt32(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["CALCULO_TASA"] != DBNull.Value) entidad.calculo_tasa = Convert.ToInt32(resultado["CALCULO_TASA"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["TOTAL_INTERES"] != DBNull.Value) entidad.total_interes = Convert.ToDecimal(resultado["TOTAL_INTERES"]);
                            if (resultado["TOTAL_RETENCION"] != DBNull.Value) entidad.total_retencion = Convert.ToDecimal(resultado["TOTAL_RETENCION"]);
                            if (resultado["COD_MOTIVO_APERTURA"] != DBNull.Value) entidad.cod_motivo_apertura = Convert.ToInt32(resultado["COD_MOTIVO_APERTURA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["NOMLINEA"] != DBNull.Value) entidad.nomlinea = Convert.ToString(resultado["NOMLINEA"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["NOMMOTIVO_PROGRA"] != DBNull.Value) entidad.nommotivo_progra = Convert.ToString(resultado["NOMMOTIVO_PROGRA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOMFORMA_PAGO"] != DBNull.Value) entidad.nomforma_pago = Convert.ToString(resultado["NOMFORMA_PAGO"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["NOMPERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["NOMPERIODICIDAD"]);
                            if (resultado["PENDIENTE"] != DBNull.Value) entidad.pendiente = Convert.ToDecimal(resultado["PENDIENTE"]);
                            entidad.valor_total = entidad.valor_cuota + entidad.pendiente;

                            lstConsulta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ListarAhorrosProgramado", ex);
                        return null;
                    }
                }
            }
        }

        public string CrearSolicitudAhorroProgramado(CuentasProgramado pAhoProgra, Usuario pUsuario)
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
                        P_COD_PERSONA.Value = pAhoProgra.cod_persona;
                        P_COD_PERSONA.Direction = ParameterDirection.Input;
                        P_COD_PERSONA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                        DbParameter P_COD_TIPO_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_COD_TIPO_PRODUCTO.ParameterName = "P_COD_TIPO_PRODUCTO";
                        P_COD_TIPO_PRODUCTO.Value = pAhoProgra.cod_tipo_producto;
                        P_COD_TIPO_PRODUCTO.Direction = ParameterDirection.Input;
                        P_COD_TIPO_PRODUCTO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_TIPO_PRODUCTO);

                        DbParameter P_LINEA = cmdTransaccionFactory.CreateParameter();
                        P_LINEA.ParameterName = "P_LINEA";
                        P_LINEA.Value = pAhoProgra.cod_linea_programado;
                        P_LINEA.Direction = ParameterDirection.Input;
                        P_LINEA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_LINEA);

                        DbParameter P_PLAZO = cmdTransaccionFactory.CreateParameter();
                        P_PLAZO.ParameterName = "P_PLAZO";
                        P_PLAZO.Value = 0;
                        P_PLAZO.Direction = ParameterDirection.Input;
                        P_PLAZO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_PLAZO);                        

                        DbParameter P_NUM_CUOTAS = cmdTransaccionFactory.CreateParameter();
                        P_NUM_CUOTAS.ParameterName = "P_NUM_CUOTAS";
                        P_NUM_CUOTAS.Value = pAhoProgra.cuota;
                        P_NUM_CUOTAS.Direction = ParameterDirection.Input;
                        P_NUM_CUOTAS.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_NUM_CUOTAS);

                        DbParameter P_VALOR = cmdTransaccionFactory.CreateParameter();
                        P_VALOR.ParameterName = "P_VALOR";
                        P_VALOR.Value = pAhoProgra.valor_cuota;
                        P_VALOR.Direction = ParameterDirection.Input;
                        P_VALOR.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR);

                        DbParameter P_PERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        P_PERIODICIDAD.ParameterName = "P_PERIODICIDAD";
                        P_PERIODICIDAD.Value = pAhoProgra.cod_periodicidad;
                        P_PERIODICIDAD.Direction = ParameterDirection.Input;
                        P_PERIODICIDAD.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_PERIODICIDAD);

                        DbParameter P_FORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FORMA_PAGO.ParameterName = "P_FORMA_PAGO";
                        P_FORMA_PAGO.Value = pAhoProgra.cod_forma_pago;
                        P_FORMA_PAGO.Direction = ParameterDirection.Input;
                        P_FORMA_PAGO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_FORMA_PAGO);

                        DbParameter P_COD_DESTINO = cmdTransaccionFactory.CreateParameter();
                        P_COD_DESTINO.ParameterName = "P_COD_DESTINO";
                        P_COD_DESTINO.Value = pAhoProgra.cod_destino;
                        P_COD_DESTINO.Direction = ParameterDirection.Input;
                        P_COD_DESTINO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_DESTINO);

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = pAhoProgra.estado_modificacion1;
                        P_ESTADO.Direction = ParameterDirection.Input;
                        P_ESTADO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);                        

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_SOLPRODUCTO_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if(Convert.ToInt32(P_ID_SOL_PRODUCTO.Value) != 0)
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
                        BOExcepcion.Throw("CuentasProgramadoData", "CrearSolicitudAhorroProgramado", ex);
                        return null;
                    }
                }
            }
        }

        public void ModificarCuota(CuentasProgramado pAhorroProgramado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter numero = cmdTransaccionFactory.CreateParameter();
                        numero.ParameterName = "PNUMERO_PROGRAMADO";
                        numero.Value = pAhorroProgramado.numero_cuenta;
                        numero.Direction = ParameterDirection.Input;
                        numero.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(numero);

                        DbParameter fecha = cmdTransaccionFactory.CreateParameter();
                        fecha.ParameterName = "PFECHA";
                        fecha.Value = pAhorroProgramado.fecha_empieza_cambio;
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
                        p_val_anterior.Value = pAhorroProgramado.valor_cuota_anterior;
                        p_val_anterior.Direction = ParameterDirection.Input;
                        p_val_anterior.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_val_anterior);

                        DbParameter p_val_nuevo = cmdTransaccionFactory.CreateParameter();
                        p_val_nuevo.ParameterName = "PVAL_NUEVO";
                        p_val_nuevo.Value = pAhorroProgramado.valor_cuota;
                        p_val_nuevo.Direction = ParameterDirection.Input;
                        p_val_nuevo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_val_nuevo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_AHOPRO_MOD_CUOTA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ModificarCambioEstados", ex);
                    }
                }
            }
        }

        public CuentasProgramado Consultartasayplazos(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasProgramado entidad = new CuentasProgramado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"   SELECT * from lineaprogramado A where A.COD_LINEA_PROGRAMADO=" + pId;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            if (resultado["PLAZO_MINIMO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO_MINIMO"]);
                            if (resultado["CUOTA_MINIMA"] != DBNull.Value) entidad.cuota = Convert.ToInt32(resultado["CUOTA_MINIMA"]);
                            if (resultado["CALCULO_TASA"] != DBNull.Value) entidad.calculo_tasa = Convert.ToInt32(resultado["CALCULO_TASA"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "Consultartasayplazos", ex);
                        return null;
                    }
                }
            }
        }


        public CuentasProgramado ConsultarAhorroProgramado(String pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasProgramado entidad = new CuentasProgramado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT AHORRO_PROGRAMADO.*, FECHA_VENCIMIENTO_PROGRAMADO(NUMERO_PROGRAMADO) as FECHA_VENCIMIENTO, CALCULAR_CUOTA_PROGRAMADO(NUMERO_PROGRAMADO) AS CUOTA_CALCULADA FROM AHORRO_PROGRAMADO WHERE NUMERO_PROGRAMADO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numero_programado = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_PRIMERA_CUOTA"] != DBNull.Value) entidad.fecha_primera_cuota = Convert.ToDateTime(resultado["FECHA_PRIMERA_CUOTA"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["CUOTA_CALCULADA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["CUOTA_CALCULADA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt64(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt32(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["CALCULO_TASA"] != DBNull.Value) entidad.calculo_tasa = Convert.ToInt32(resultado["CALCULO_TASA"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["TOTAL_INTERES"] != DBNull.Value) entidad.total_interes = Convert.ToDecimal(resultado["TOTAL_INTERES"]);
                            if (resultado["TOTAL_RETENCION"] != DBNull.Value) entidad.total_retencion = Convert.ToDecimal(resultado["TOTAL_RETENCION"]);
                            if (resultado["COD_MOTIVO_APERTURA"] != DBNull.Value) entidad.cod_motivo_apertura = Convert.ToInt32(resultado["COD_MOTIVO_APERTURA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ConsultarAhorroProgramado", ex);
                        return null;
                    }
                }
            }
        }

        // consulta las cuentas de ahorro programado filtrada por el usuario
        public List<ReporteCuentasPeriodico> ReportePeriodico(string pCodLinea, DateTime pFechaInicial, DateTime pFechaFinal, Int64 pCodOficina, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ReporteCuentasPeriodico> listConsulta = new List<ReporteCuentasPeriodico>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        // se guarda el parametro string codigoLinea para cada pl se envian primero los parametros
                        DbParameter PCODLINEAPROGRAMADO = cmdTransaccionFactory.CreateParameter();
                        PCODLINEAPROGRAMADO.ParameterName = "PCODLINEAPROGRAMADO";
                        PCODLINEAPROGRAMADO.Value = pCodLinea;// parametro string fecha fin
                        PCODLINEAPROGRAMADO.Direction = ParameterDirection.Input;
                        PCODLINEAPROGRAMADO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PCODLINEAPROGRAMADO);

                        // se envia el parametro fecha inicio 
                        DbParameter PFECHAINICIO = cmdTransaccionFactory.CreateParameter();
                        PFECHAINICIO.ParameterName = "PFECHAINICIO";
                        PFECHAINICIO.Value = pFechaInicial;// parametro datetime fechainicial 
                        PFECHAINICIO.Direction = ParameterDirection.Input;
                        PFECHAINICIO.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PFECHAINICIO);

                        DbParameter PFECHAFINAL = cmdTransaccionFactory.CreateParameter();
                        PFECHAFINAL.ParameterName = "PFECHAFINAL";
                        PFECHAFINAL.Value = pFechaFinal;// parametro fecha final
                        PFECHAFINAL.Direction = ParameterDirection.Input;
                        PFECHAFINAL.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PFECHAFINAL);

                        DbParameter PCODOFICINA = cmdTransaccionFactory.CreateParameter();
                        PCODOFICINA.ParameterName = "PCODOFICINA";
                        if (pCodOficina != 0) PCODOFICINA.Value = pCodOficina; else PCODOFICINA.Value = DBNull.Value;// si el codigo of es 0 se envia null
                        PCODOFICINA.Direction = ParameterDirection.Input;
                        PCODOFICINA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCODOFICINA);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_REPORTEPER";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ReportePeriodico", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        // Configuracion conf = new Configuracion();
                        string sql = "SELECT * FROM XPINNADM.TEMP_REPORTEPRO";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            ReporteCuentasPeriodico entidad = new ReporteCuentasPeriodico();

                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numeroCuenta = Convert.ToDecimal(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fechaApertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["SALDO_INICIAL"] != DBNull.Value) entidad.saldoInicial = Convert.ToDecimal(resultado["SALDO_INICIAL"]);
                            if (resultado["DEPOSITOS"] != DBNull.Value) entidad.depocito = Convert.ToDecimal(resultado["DEPOSITOS"]);
                            if (resultado["RETIROS"] != DBNull.Value) entidad.retiro = Convert.ToDecimal(resultado["RETIROS"]);
                            if (resultado["INTERESES"] != DBNull.Value) entidad.intereses = Convert.ToDecimal(resultado["INTERESES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToDecimal(resultado["RETENCION"]);
                            if (resultado["SALDO_FINAL"] != DBNull.Value) entidad.saldoFinal = Convert.ToDecimal(resultado["SALDO_FINAL"]);

                            listConsulta.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return listConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ReportePeriodico", ex);
                        return null;
                    }
                }
            }
        }

        // carga el griv
        public List<CierreCuentaAhorroProgramado> cerrarCuentaProgramado(DateTime PfechaApertura, String Filtro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<CierreCuentaAhorroProgramado> ListaEntidad = new List<CierreCuentaAhorroProgramado>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select ap.numero_programado, ap.cod_linea_programado, lp.nombre As nom_linea, ap.cod_oficina, o.nombre as Nombre_oficina,"
                                      + " ap.fecha_apertura,  m.descripcion As motivo_apertura,vp.identificacion, vp.nombre as Nombre_Persona, vp.cod_nomina, ap.fecha_proximo_pago, ap.plazo, "
                                      + " ap.saldo, case ap.forma_pago when 1 then 'Caja' when 2 then 'Nomina' end as Nom_FormaPago, ap.estado,lp.retiro_parcial "
                                      + " From ahorro_programado ap "
                                      + " Inner Join lineaprogramado lp on lp.cod_linea_programado = ap.cod_linea_programado "
                                      + " Inner Join oficina o on ap.cod_oficina = o.cod_oficina "
                                      + " Inner Join v_persona vp on ap.cod_persona = vp.cod_persona "
                                      + " Left join motivo_programado m on m.cod_motivo_apertura=ap.cod_motivo_apertura Where ap.estado = 1 And ap.saldo > 0 " + Filtro;

                        Configuracion conf = new Configuracion();
                        if (PfechaApertura != null && PfechaApertura != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " ap.fecha_apertura = To_Date('" + Convert.ToDateTime(PfechaApertura).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " ap.fecha_apertura = '" + Convert.ToDateTime(PfechaApertura).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CierreCuentaAhorroProgramado entidad = new CierreCuentaAhorroProgramado();
                            if (resultado["numero_programado"] != DBNull.Value) entidad.Cuenta = Convert.ToString(resultado["numero_programado"]);
                            if (resultado["nom_linea"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["nom_linea"]);
                            if (resultado["Nombre_oficina"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["Nombre_oficina"]);
                            if (resultado["fecha_apertura"] != DBNull.Value) entidad.Fecha_Apertura = Convert.ToDateTime(resultado["fecha_apertura"]);
                            if (resultado["motivo_apertura"] != DBNull.Value) entidad.Motivo_Apertura = Convert.ToString(resultado["motivo_apertura"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["Nombre_Persona"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["Nombre_Persona"]);
                            if (resultado["Cod_nomina"] != DBNull.Value) entidad.Cod_nomina = Convert.ToString(resultado["Cod_nomina"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.Fecha_Proximo_Pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["plazo"] != DBNull.Value) entidad.Plazo = Convert.ToInt32(resultado["plazo"]);
                            if (resultado["saldo"] != DBNull.Value) entidad.Saldo = Convert.ToInt32(resultado["saldo"]);
                            if (resultado["Nom_FormaPago"] != DBNull.Value) entidad.Forma_Depago = Convert.ToString(resultado["Nom_FormaPago"]);
                            if (resultado["estado"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["estado"]);
                            if (entidad.Estado == "1")
                                entidad.Estado = "Activo";
                            else
                                entidad.Estado = "Inactivo";
                            if (resultado["retiro_parcial"] != DBNull.Value) entidad.retiro = Convert.ToString(resultado["retiro_parcial"]);

                            if (entidad.retiro == "1")
                                entidad.retiro = "Si";
                            else
                                entidad.retiro = "No";

                            ListaEntidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return ListaEntidad;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("cerrarCuentaProgramado", "CierredeCuentas", ex);
                        return null;
                    }
                }
            }
        }
        // carga el griv
        public List<CierreCuentaAhorroProgramado> ListarProrrogas(DateTime PfechaProrroga, String Filtro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<CierreCuentaAhorroProgramado> ListaEntidad = new List<CierreCuentaAhorroProgramado>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select ap.numero_programado, ap.cod_linea_programado, lp.nombre As nom_linea, ap.cod_oficina, o.nombre as Nombre_oficina,"
                                      + " ap.fecha_apertura,  m.descripcion As motivo_apertura,vp.identificacion, vp.nombre as Nombre_Persona, vp.cod_nomina, ap.fecha_proximo_pago, ap.plazo, "
                                      + " ap.saldo, case ap.forma_pago when 1 then 'Caja' when 2 then 'Nomina' end as Nom_FormaPago, ap.estado,lp.retiro_parcial, "
                                      + "  o.fecha_oper as fecha_prorroga,p.fecha_inicio,p.FECHA_FINAL "
                                      + " From ahorro_programado ap "
                                      + " Inner Join lineaprogramado lp on lp.cod_linea_programado = ap.cod_linea_programado "
                                      + " Inner Join oficina o on ap.cod_oficina = o.cod_oficina "
                                      + " Inner Join v_persona vp on ap.cod_persona = vp.cod_persona "
                                      + " inner join PRORROGA_AHO_PROGRAMADO p on p.NUMERO_PROGRAMADO = ap.NUMERO_PROGRAMADO "
                                      + " left join operacion o on o.cod_ope=p.cod_ope "
                                      + " Left join motivo_programado m on m.cod_motivo_apertura=ap.cod_motivo_apertura Where 1=1 " + Filtro;

                        Configuracion conf = new Configuracion();
                        if (PfechaProrroga != null && PfechaProrroga != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " o.fecha_oper = To_Date('" + Convert.ToDateTime(PfechaProrroga).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " o.fecha_oper = '" + Convert.ToDateTime(PfechaProrroga).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CierreCuentaAhorroProgramado entidad = new CierreCuentaAhorroProgramado();
                            if (resultado["numero_programado"] != DBNull.Value) entidad.Cuenta = Convert.ToString(resultado["numero_programado"]);
                            if (resultado["nom_linea"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["nom_linea"]);
                            if (resultado["Nombre_oficina"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["Nombre_oficina"]);
                            if (resultado["fecha_apertura"] != DBNull.Value) entidad.Fecha_Apertura = Convert.ToDateTime(resultado["fecha_apertura"]);
                            if (resultado["motivo_apertura"] != DBNull.Value) entidad.Motivo_Apertura = Convert.ToString(resultado["motivo_apertura"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["Nombre_Persona"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["Nombre_Persona"]);
                            if (resultado["Cod_nomina"] != DBNull.Value) entidad.Cod_nomina = Convert.ToString(resultado["Cod_nomina"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.Fecha_Proximo_Pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["plazo"] != DBNull.Value) entidad.Plazo = Convert.ToInt32(resultado["plazo"]);
                            if (resultado["saldo"] != DBNull.Value) entidad.Saldo = Convert.ToInt32(resultado["saldo"]);
                            if (resultado["Nom_FormaPago"] != DBNull.Value) entidad.Forma_Depago = Convert.ToString(resultado["Nom_FormaPago"]);
                            if (resultado["estado"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["estado"]);
                            if (entidad.Estado == "1")
                                entidad.Estado = "Activo";
                            else
                                entidad.Estado = "Inactivo";
                            if (resultado["retiro_parcial"] != DBNull.Value) entidad.retiro = Convert.ToString(resultado["retiro_parcial"]);

                            if (entidad.retiro == "1")
                                entidad.retiro = "Si";
                            else
                                entidad.retiro = "No";
                            if (resultado["fecha_prorroga"] != DBNull.Value) entidad.Fecha_Prorroga = Convert.ToDateTime(resultado["fecha_prorroga"]);
                            if (resultado["fecha_inicio"] != DBNull.Value) entidad.Fecha_Inicio = Convert.ToDateTime(resultado["fecha_inicio"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.Fecha_Final = Convert.ToDateTime(resultado["FECHA_FINAL"]);




                            ListaEntidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return ListaEntidad;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("cerrarCuentaProgramado", "ListarProrrogas", ex);
                        return null;
                    }
                }
            }
        }


        // carga el griv
        public List<CierreCuentaAhorroProgramado> ListarRenovaciones(DateTime PfechaProrroga, String Filtro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<CierreCuentaAhorroProgramado> ListaEntidad = new List<CierreCuentaAhorroProgramado>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select ap.numero_programado, ap.cod_linea_programado, lp.nombre As nom_linea, ap.cod_oficina, o.nombre as Nombre_oficina,"
                                      + " ap.fecha_apertura,  m.descripcion As motivo_apertura,vp.identificacion, vp.nombre as Nombre_Persona, vp.cod_nomina, ap.fecha_proximo_pago, ap.plazo, "
                                      + " ap.saldo, case ap.forma_pago when 1 then 'Caja' when 2 then 'Nomina' end as Nom_FormaPago, ap.estado,lp.retiro_parcial, "
                                      + "  o.fecha_oper as fecha_Renovacion,p.FECHA_VENCIMIENTO,p.NUMERO_PROGRAMADO_RENOVADO "
                                      + " From ahorro_programado ap "
                                      + " Inner Join lineaprogramado lp on lp.cod_linea_programado = ap.cod_linea_programado "
                                      + " Inner Join oficina o on ap.cod_oficina = o.cod_oficina "
                                      + " Inner Join v_persona vp on ap.cod_persona = vp.cod_persona "
                                      + " inner join RENOVACION_AHO_PROGRAMADO p on p.NUMERO_PROGRAMADO = ap.NUMERO_PROGRAMADO "
                                      + " left join operacion o on o.cod_ope=p.cod_ope "
                                      + " Left join motivo_programado m on m.cod_motivo_apertura=ap.cod_motivo_apertura Where 1=1 " + Filtro;

                        Configuracion conf = new Configuracion();
                        if (PfechaProrroga != null && PfechaProrroga != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " o.fecha_oper = To_Date('" + Convert.ToDateTime(PfechaProrroga).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " o.fecha_oper = '" + Convert.ToDateTime(PfechaProrroga).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CierreCuentaAhorroProgramado entidad = new CierreCuentaAhorroProgramado();
                            if (resultado["numero_programado"] != DBNull.Value) entidad.Cuenta = Convert.ToString(resultado["numero_programado"]);
                            if (resultado["nom_linea"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["nom_linea"]);
                            if (resultado["Nombre_oficina"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["Nombre_oficina"]);
                            if (resultado["fecha_apertura"] != DBNull.Value) entidad.Fecha_Apertura = Convert.ToDateTime(resultado["fecha_apertura"]);
                            if (resultado["motivo_apertura"] != DBNull.Value) entidad.Motivo_Apertura = Convert.ToString(resultado["motivo_apertura"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["Nombre_Persona"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["Nombre_Persona"]);
                            if (resultado["Cod_nomina"] != DBNull.Value) entidad.Cod_nomina = Convert.ToString(resultado["Cod_nomina"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.Fecha_Proximo_Pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["plazo"] != DBNull.Value) entidad.Plazo = Convert.ToInt32(resultado["plazo"]);
                            if (resultado["saldo"] != DBNull.Value) entidad.Saldo = Convert.ToInt32(resultado["saldo"]);
                            if (resultado["Nom_FormaPago"] != DBNull.Value) entidad.Forma_Depago = Convert.ToString(resultado["Nom_FormaPago"]);
                            if (resultado["estado"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["estado"]);
                            if (entidad.Estado == "1")
                                entidad.Estado = "Activo";
                            else
                                entidad.Estado = "Inactivo";
                            if (resultado["retiro_parcial"] != DBNull.Value) entidad.retiro = Convert.ToString(resultado["retiro_parcial"]);

                            if (entidad.retiro == "1")
                                entidad.retiro = "Si";
                            else
                                entidad.retiro = "No";
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.Fecha_Vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["NUMERO_PROGRAMADO_RENOVADO"] != DBNull.Value) entidad.Cuenta_renovada = Convert.ToString(resultado["NUMERO_PROGRAMADO_RENOVADO"]);
                            if (resultado["fecha_Renovacion"] != DBNull.Value) entidad.Fecha_Renovacion = Convert.ToDateTime(resultado["fecha_Renovacion"]);





                            ListaEntidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return ListaEntidad;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("cerrarCuentaProgramado", "ListarProrrogas", ex);
                        return null;
                    }
                }
            }
        }



        //cargar el detalle
        public cierreCuentaDetalle cerrarCuentaProgramadoData(String codigo, Usuario pUsuario)
        {
            DbDataReader resultado;

            cierreCuentaDetalle entidad = new cierreCuentaDetalle();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select ap.numero_programado as Numero_Cuenta,vp.nombre as Nombre_Persona,vp.identificacion,"
                                      + "  vp.tipo_identificacion,ap.fecha_apertura,o.nombre as Nombre_Oficina,lp.nombre as Nombre_Linea, lp.por_retiro_maximo,"
                                      + "  ap.plazo,CALCULAR_CUOTA_PROGRAMADO(ap.valor_cuota) as valor_cuota , pr.descripcion as Perioricidad, ap.fecha_proximo_pago, pr.cod_periodicidad, ap.cod_linea_programado, ap.saldo,"
                                      + " ap.calculo_tasa,ap.forma_pago,ap.fecha_interes,ap.total_interes, ap.total_retencion, ap.tipo_historico , vp.cod_persona,tp.descripcion "
                                      + " from ahorro_programado ap  inner join lineaprogramado lp "
                                      + " on lp.cod_linea_programado = ap.cod_linea_programado inner join v_persona vp "
                                      + " on ap.cod_persona = vp.cod_persona inner join  oficina o "
                                      + " on ap.cod_oficina = o.cod_oficina inner join periodicidad pr on ap.cod_periodicidad= pr.cod_periodicidad "
                                      + " left join tipoidentificacion tp on vp.tipo_identificacion = tp.codtipoidentificacion where ap.numero_programado= " + codigo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {

                            if (resultado["Numero_Cuenta"] != DBNull.Value) entidad.Cuenta = Convert.ToString(resultado["Numero_Cuenta"]);
                            if (resultado["Nombre_Persona"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["Nombre_Persona"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.TipoIdentificacion = Convert.ToInt32(resultado["tipo_identificacion"]);
                            if (resultado["fecha_apertura"] != DBNull.Value) entidad.Fecha_Apertura = Convert.ToDateTime(resultado["fecha_apertura"]);
                            if (resultado["Nombre_Oficina"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["Nombre_Oficina"]);
                            if (resultado["cod_linea_programado"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["cod_linea_programado"]);
                            if (resultado["plazo"] != DBNull.Value) entidad.Plazo = Convert.ToInt32(resultado["plazo"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.cuota = Convert.ToInt32(resultado["valor_cuota"]);
                            if (resultado["Perioricidad"] != DBNull.Value) entidad.Periodicidad = Convert.ToString(resultado["Perioricidad"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.Fecha_Proximo_Pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);// mirara si en apertura o activa
                            if (resultado["saldo"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["saldo"]);
                            if (resultado["cod_periodicidad"] != DBNull.Value) entidad.Codigo_Perioricidad = Convert.ToInt32(resultado["cod_periodicidad"]);
                            if (resultado["calculo_tasa"] != DBNull.Value) entidad.calculo_tasa = Convert.ToInt64(resultado["calculo_tasa"]);
                            if (resultado["forma_pago"] != DBNull.Value) entidad.forma_pago = Convert.ToInt64(resultado["forma_pago"]);
                            if (resultado["fecha_interes"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["fecha_interes"]);
                            if (resultado["total_interes"] != DBNull.Value) entidad.total_interes = Convert.ToDecimal(resultado["total_interes"]);
                            if (resultado["total_retencion"] != DBNull.Value) entidad.total_retencion = Convert.ToDecimal(resultado["total_retencion"]);
                            if (resultado["tipo_historico"] != DBNull.Value) entidad.tipo_historico = Convert.ToDecimal(resultado["tipo_historico"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.Cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.Descripcion_Id = resultado["descripcion"].ToString();
                            if (resultado["Nombre_Linea"] != DBNull.Value) entidad.nombre_linea = Convert.ToString(resultado["Nombre_Linea"]);
                            if (resultado["por_retiro_maximo"] != DBNull.Value) entidad.porcentaje = Convert.ToInt32(resultado["por_retiro_maximo"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("cerrarCuentaProgramado", "CierredeCuentas", ex);
                        return null;
                    }
                }
            }
        }
        public cierreCuentaDetalle ProrrogarCuentaProgramadoData(String codigo, Usuario pUsuario)
        {
            DbDataReader resultado;

            cierreCuentaDetalle entidad = new cierreCuentaDetalle();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select ap.numero_programado as Numero_Cuenta,vp.nombre as Nombre_Persona,vp.identificacion,"
                                      + "  vp.tipo_identificacion,ap.fecha_apertura,o.nombre as Nombre_Oficina,lp.nombre as Nombre_Linea, lp.por_retiro_maximo,"
                                      + "  ap.plazo,ap.valor_cuota as valor_cuota , pr.descripcion as Perioricidad, ap.fecha_proximo_pago, pr.cod_periodicidad, ap.cod_linea_programado, ap.saldo,"
                                      + " ap.calculo_tasa,ap.forma_pago,ap.fecha_interes,ap.total_interes, ap.total_retencion, ap.tipo_historico , vp.cod_persona,tp.descripcion "
                                      + " from ahorro_programado ap  inner join lineaprogramado lp "
                                      + " on lp.cod_linea_programado = ap.cod_linea_programado inner join v_persona vp "
                                      + " on ap.cod_persona = vp.cod_persona inner join  oficina o "
                                      + " on ap.cod_oficina = o.cod_oficina inner join periodicidad pr on ap.cod_periodicidad= pr.cod_periodicidad "
                                      + " left join tipoidentificacion tp on vp.tipo_identificacion = tp.codtipoidentificacion where ap.numero_programado= " + codigo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {

                            if (resultado["Numero_Cuenta"] != DBNull.Value) entidad.Cuenta = Convert.ToString(resultado["Numero_Cuenta"]);
                            if (resultado["Nombre_Persona"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["Nombre_Persona"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.TipoIdentificacion = Convert.ToInt32(resultado["tipo_identificacion"]);
                            if (resultado["fecha_apertura"] != DBNull.Value) entidad.Fecha_Apertura = Convert.ToDateTime(resultado["fecha_apertura"]);
                            if (resultado["Nombre_Oficina"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["Nombre_Oficina"]);
                            if (resultado["cod_linea_programado"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["cod_linea_programado"]);
                            if (resultado["plazo"] != DBNull.Value) entidad.Plazo = Convert.ToInt32(resultado["plazo"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.cuota = Convert.ToInt32(resultado["valor_cuota"]);
                            if (resultado["Perioricidad"] != DBNull.Value) entidad.Periodicidad = Convert.ToString(resultado["Perioricidad"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.Fecha_Proximo_Pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);// mirara si en apertura o activa
                            if (resultado["saldo"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["saldo"]);
                            if (resultado["cod_periodicidad"] != DBNull.Value) entidad.Codigo_Perioricidad = Convert.ToInt32(resultado["cod_periodicidad"]);
                            if (resultado["calculo_tasa"] != DBNull.Value) entidad.calculo_tasa = Convert.ToInt64(resultado["calculo_tasa"]);
                            if (resultado["forma_pago"] != DBNull.Value) entidad.forma_pago = Convert.ToInt64(resultado["forma_pago"]);
                            if (resultado["fecha_interes"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["fecha_interes"]);
                            if (resultado["total_interes"] != DBNull.Value) entidad.total_interes = Convert.ToDecimal(resultado["total_interes"]);
                            if (resultado["total_retencion"] != DBNull.Value) entidad.total_retencion = Convert.ToDecimal(resultado["total_retencion"]);
                            if (resultado["tipo_historico"] != DBNull.Value) entidad.tipo_historico = Convert.ToDecimal(resultado["tipo_historico"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.Cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.Descripcion_Id = resultado["descripcion"].ToString();
                            if (resultado["Nombre_Linea"] != DBNull.Value) entidad.nombre_linea = Convert.ToString(resultado["Nombre_Linea"]);
                            if (resultado["por_retiro_maximo"] != DBNull.Value) entidad.porcentaje = Convert.ToInt32(resultado["por_retiro_maximo"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("cerrarCuentaProgramado", "CierredeCuentas", ex);
                        return null;
                    }
                }
            }
        }

        // calculo de los campos intesres y pagar
        public cierreCuentaDetalle cerrarCuenta(cierreCuentaDetalle entidad, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pnumero_programado = cmdTransaccionFactory.CreateParameter();
                        pnumero_programado.ParameterName = "pnumero_programado";
                        pnumero_programado.Value = entidad.NumeroProgramado;
                        pnumero_programado.Direction = ParameterDirection.Input;
                        //pnumero_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_programado);


                        DbParameter pfecha_liquida = cmdTransaccionFactory.CreateParameter();
                        pfecha_liquida.ParameterName = "pfecha_liquida";
                        pfecha_liquida.Value = entidad.fecha_Liquida;
                        pfecha_liquida.Direction = ParameterDirection.Input;
                        //pfecha_liquida.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha_liquida);

                        DbParameter pcodigo_usuario = cmdTransaccionFactory.CreateParameter();
                        pcodigo_usuario.ParameterName = "pcodigo_usuario";
                        pcodigo_usuario.Value = pusuario.codusuario;
                        pcodigo_usuario.Direction = ParameterDirection.Input;
                        //pcodigo_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_usuario);


                        if (entidad.Tasa_Diferencial != 0)
                        {

                            DbParameter pTasaDiferencial = cmdTransaccionFactory.CreateParameter();
                            pTasaDiferencial.ParameterName = "pinteresval";
                            pTasaDiferencial.Value = entidad.Tasa_Diferencial;
                            pTasaDiferencial.Direction = ParameterDirection.Input;
                            //pcodigo_usuario.DbType = DbType.Int32;
                            cmdTransaccionFactory.Parameters.Add(pTasaDiferencial);

                        }

                        DbParameter pinteres = cmdTransaccionFactory.CreateParameter();
                        pinteres.ParameterName = "pinteres";
                        pinteres.Value = entidad.Interes_;
                        pinteres.Direction = ParameterDirection.Output;
                        // pinteres.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pinteres);


                       
                             

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "pretencion";
                        pretencion.Value = entidad.Menos_Retencion;
                        pretencion.Direction = ParameterDirection.Output;
                        //pretencion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pretencion);

                        DbParameter pValorGMF = cmdTransaccionFactory.CreateParameter();
                        pValorGMF.ParameterName = "pValorGMF";
                        pValorGMF.Value = entidad.Menos_GMF;
                        pValorGMF.Direction = ParameterDirection.Output;
                        // pValorGMF.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pValorGMF);


                        DbParameter pDescuentos = cmdTransaccionFactory.CreateParameter();
                        pDescuentos.ParameterName = "pDescuentos";
                        pDescuentos.Value = entidad.Menos_Descuento;
                        pDescuentos.Direction = ParameterDirection.Output;
                        // pDescuentos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pDescuentos);


                        DbParameter pValorPagar = cmdTransaccionFactory.CreateParameter();
                        pValorPagar.ParameterName = "pValorPagar";
                        pValorPagar.Value = entidad.Total_pagar;
                        pValorPagar.Direction = ParameterDirection.Output;
                        //pValorPagar.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pValorPagar);

                        DbParameter pDiasLiquidados = cmdTransaccionFactory.CreateParameter();
                        pDiasLiquidados.ParameterName = "pDiasLiquidados";
                        pDiasLiquidados.Value = entidad.Dias_Liquidados;
                        pDiasLiquidados.Direction = ParameterDirection.Output;
                        //pDiasLiquidados.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pDiasLiquidados);


                        DbParameter pinterescausado = cmdTransaccionFactory.CreateParameter();
                        pinterescausado.ParameterName = "pinterescausado";
                        pinterescausado.Value = entidad.Interes_causado;
                        pinterescausado.Direction = ParameterDirection.Output;
                        // pinteres.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pinterescausado);

                        DbParameter pretencioncausado = cmdTransaccionFactory.CreateParameter();
                        pretencioncausado.ParameterName = "pretencioncausado";
                        pretencioncausado.Value = entidad.Retencion_causada;
                        pretencioncausado.Direction = ParameterDirection.Output;
                        // pinteres.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pretencioncausado);


                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = entidad.Valor;
                        pvalor.Direction = ParameterDirection.Output;
                        // pinteres.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        cmdTransaccionFactory.CommandText = entidad.Tasa_Diferencial == 0 ? "USP_XPINN_PRO_LIQUIDACIERRE" : "USP_XPINN_PRO_LIQUIDA_INT"; 
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        entidad.Interes_ = Convert.ToDecimal(pinteres.Value);
                        entidad.Retencion_causada = Convert.ToDecimal(pretencioncausado.Value);
                        entidad.Interes_causado = Convert.ToDecimal(pinterescausado.Value);
                        entidad.Menos_Retencion = Convert.ToDecimal(pretencion.Value);
                        entidad.Menos_GMF = Convert.ToDecimal(pValorGMF.Value);
                        entidad.Menos_Descuento = Convert.ToDecimal(pDescuentos.Value);
                        entidad.Valor = pvalor.Value != DBNull.Value ? Convert.ToDecimal(pvalor.Value) : 0;
                        entidad.Total_pagar = Convert.ToDecimal(pValorPagar.Value);
                        entidad.Dias_Liquidados = Convert.ToInt32(pDiasLiquidados.Value);

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ReportePeriodico", ex);
                        return null;
                    }
                }
            }

        }
        public cierreCuentaDetalle AperturaCuentasServices(cierreCuentaDetalle entidad, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pnumero_programado = cmdTransaccionFactory.CreateParameter();
                        pnumero_programado.ParameterName = "PN_NUM_PRODUCTO";
                        pnumero_programado.Value = entidad.NumeroProgramado;
                        pnumero_programado.Direction = ParameterDirection.Input;
                        //pnumero_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_programado);


                        DbParameter PN_COD_CLIENTE = cmdTransaccionFactory.CreateParameter();
                        PN_COD_CLIENTE.ParameterName = "PN_COD_CLIENTE";
                        PN_COD_CLIENTE.Value = entidad.Cod_persona;
                        PN_COD_CLIENTE.Direction = ParameterDirection.Input;
                        //pfecha_liquida.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PN_COD_CLIENTE);

                        DbParameter PN_COD_OPE = cmdTransaccionFactory.CreateParameter();
                        PN_COD_OPE.ParameterName = "PN_COD_OPE";
                        PN_COD_OPE.Value = entidad.cod_ope;
                        PN_COD_OPE.Direction = ParameterDirection.Input;
                        //pfecha_liquida.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PN_COD_OPE);

                        DbParameter PF_FECHA_PAGO = cmdTransaccionFactory.CreateParameter();
                        PF_FECHA_PAGO.ParameterName = "PF_FECHA_PAGO";
                        PF_FECHA_PAGO.Value = entidad.fecha_Liquida;
                        PF_FECHA_PAGO.Direction = ParameterDirection.Input;
                        //pfecha_liquida.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PF_FECHA_PAGO);


                        DbParameter PN_VALOR_PAGO = cmdTransaccionFactory.CreateParameter();
                        PN_VALOR_PAGO.ParameterName = "PN_VALOR_PAGO";
                        PN_VALOR_PAGO.Value = entidad.Valor;
                        PN_VALOR_PAGO.Direction = ParameterDirection.Input;
                        //pfecha_liquida.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PN_VALOR_PAGO);

                        DbParameter PN_TIPO_TRAN = cmdTransaccionFactory.CreateParameter();
                        PN_TIPO_TRAN.ParameterName = "PN_TIPO_TRAN";
                        PN_TIPO_TRAN.Value = entidad.tipo_tran;
                        PN_TIPO_TRAN.Direction = ParameterDirection.Input;
                        //pfecha_liquida.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PN_TIPO_TRAN);

                        DbParameter PN_NUM_TRAN = cmdTransaccionFactory.CreateParameter();
                        PN_NUM_TRAN.ParameterName = "PN_NUM_TRAN";
                        PN_NUM_TRAN.Value = entidad.num_tran;
                        PN_NUM_TRAN.Direction = ParameterDirection.Output;                      
                        cmdTransaccionFactory.Parameters.Add(PN_NUM_TRAN);

                        DbParameter PN_CONCEPTO_ERROR = cmdTransaccionFactory.CreateParameter();
                        PN_CONCEPTO_ERROR.ParameterName = "PN_CONCEPTO_ERROR";
                        PN_CONCEPTO_ERROR.Value = entidad.concepto;
                        PN_CONCEPTO_ERROR.DbType = DbType.StringFixedLength;
                        PN_CONCEPTO_ERROR.Size = 1000;
                        PN_CONCEPTO_ERROR.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(PN_CONCEPTO_ERROR);



                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_REALIZAPAGO";

                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "AperturaCuentasServices", ex);
                        return null;
                    }
                }
            }

        }
        public cierreCuentaDetalle CrearRenovacionCuentasServices(cierreCuentaDetalle entidad, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                      

                        DbParameter P_IDRENOVACION = cmdTransaccionFactory.CreateParameter();
                        P_IDRENOVACION.ParameterName = "P_IDRENOVACION";
                        P_IDRENOVACION.Value =0;
                        P_IDRENOVACION.Direction = ParameterDirection.Output;
                        //P_IDRENOVACION.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_IDRENOVACION);


                        DbParameter pnumero_programado = cmdTransaccionFactory.CreateParameter();
                        pnumero_programado.ParameterName = "PNUMERO_PROGRAMADO";
                        pnumero_programado.Value = entidad.NumeroProgramado;
                        pnumero_programado.Direction = ParameterDirection.Input;
                       // pnumero_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_programado);


                         DbParameter P_FECHA_VENCIMIENTO = cmdTransaccionFactory.CreateParameter();
                         P_FECHA_VENCIMIENTO.ParameterName = "P_FECHA_VENCIMIENTO";
                         P_FECHA_VENCIMIENTO.Value = entidad.Fecha_Vencimiento;
                         P_FECHA_VENCIMIENTO.Direction = ParameterDirection.Input;
                        // P_FECHA_VENCIMIENTO.DbType = DbType.Date;
                         cmdTransaccionFactory.Parameters.Add(P_FECHA_VENCIMIENTO);




                        DbParameter PCOD_LINEA_PROGRAMADO = cmdTransaccionFactory.CreateParameter();
                        PCOD_LINEA_PROGRAMADO.ParameterName = "PCOD_LINEA_PROGRAMADO";
                        PCOD_LINEA_PROGRAMADO.Value = entidad.cod_linea;                      
                        PCOD_LINEA_PROGRAMADO.Direction = ParameterDirection.Input;
                      //  PCOD_LINEA_PROGRAMADO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PCOD_LINEA_PROGRAMADO);

                        DbParameter P_PLAZO = cmdTransaccionFactory.CreateParameter();
                        P_PLAZO.ParameterName = "P_PLAZO";
                        P_PLAZO.Value = entidad.Plazo;
                        P_PLAZO.Direction = ParameterDirection.Input;
                     //   P_PLAZO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_PLAZO);


                        DbParameter P_OBSERVACION = cmdTransaccionFactory.CreateParameter();
                        P_OBSERVACION.ParameterName = "P_OBSERVACION";
                        P_OBSERVACION.Value = entidad.Observacion;
                        P_OBSERVACION.Direction = ParameterDirection.Input;
                        //P_OBSERVACION.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_OBSERVACION);


                        DbParameter PNUMERO_PROGRAMADO_RENOVADO = cmdTransaccionFactory.CreateParameter();
                        PNUMERO_PROGRAMADO_RENOVADO.ParameterName = "PNUMERO_PROGRAMADO_RENOVADO";
                        PNUMERO_PROGRAMADO_RENOVADO.Value = entidad.NumeroProgramado_Renovado;
                        PNUMERO_PROGRAMADO_RENOVADO.Direction = ParameterDirection.Input;
                       // PNUMERO_PROGRAMADO_RENOVADO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PNUMERO_PROGRAMADO_RENOVADO);



                        DbParameter P_COD_OPE = cmdTransaccionFactory.CreateParameter();
                        P_COD_OPE.ParameterName = "P_COD_OPE";
                        P_COD_OPE.Value = entidad.cod_ope;
                        P_COD_OPE.Direction = ParameterDirection.Input;
                     //   P_COD_OPE.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_COD_OPE);


                    


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_RENOVACION";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        




                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "CrearRenovacionCuentasServices", ex);
                        return null;
                    }
                }
            }

        }

        // cierra la cuenta y cambia el estado 
        public void CambiarEstadoCuenta(cierreCuentaDetalle entidad, Int64 pacod_ope, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pnumero_programado = cmdTransaccionFactory.CreateParameter();
                        pnumero_programado.ParameterName = "pnumero_programado";
                        pnumero_programado.Value = entidad.NumeroProgramado;
                        pnumero_programado.Direction = ParameterDirection.Input;
                        //pnumero_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_programado);


                        DbParameter pfecha_cierre = cmdTransaccionFactory.CreateParameter();
                        pfecha_cierre.ParameterName = "pfecha_cierre";
                        pfecha_cierre.Value = entidad.fecha_Liquida;
                        pfecha_cierre.Direction = ParameterDirection.Input;
                        //pfecha_liquida.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha_cierre);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcod_ope";
                        pcod_ope.Value = pacod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        //pcodigo_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pinteres = cmdTransaccionFactory.CreateParameter();
                        pinteres.ParameterName = "pinteres";
                        pinteres.Value = entidad.Interes_;
                        pinteres.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pinteres);

                        DbParameter pInteres_causado = cmdTransaccionFactory.CreateParameter();
                        pInteres_causado.ParameterName = "pInteres_causado";
                        pInteres_causado.Value = entidad.Interes_causado;
                        pInteres_causado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pInteres_causado);

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "pretencion";
                        pretencion.Value = entidad.Menos_Retencion;
                        pretencion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pretencion);

                        DbParameter pRetencion_causada = cmdTransaccionFactory.CreateParameter();
                        pRetencion_causada.ParameterName = "pRetencion_causada";
                        pRetencion_causada.Value = entidad.Retencion_causada;
                        pRetencion_causada.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pRetencion_causada);

                        DbParameter pValorGMF = cmdTransaccionFactory.CreateParameter();
                        pValorGMF.ParameterName = "pValorGMF";
                        pValorGMF.Value = entidad.Menos_GMF;
                        pValorGMF.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pValorGMF);

                        DbParameter pDescuentos = cmdTransaccionFactory.CreateParameter();
                        pDescuentos.ParameterName = "pDescuentos";
                        pDescuentos.Value = entidad.Menos_Descuento;
                        pDescuentos.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pDescuentos);

                        DbParameter pValorPagar = cmdTransaccionFactory.CreateParameter();
                        pValorPagar.ParameterName = "pValorPagar";
                        pValorPagar.Value = entidad.Total_pagar;
                        pValorPagar.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pValorPagar);

                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_CIERRECUENTA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(entidad, "Ahorro_programado", pusuario, Accion.Crear.ToString(), TipoAuditoria.AhorroProgramado, "Creacion de cierre de ahorro programado con numero de programado " + entidad.NumeroProgramado);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "CambiarEstadoCuenta", ex);
                    }
                }
            }
        }

        public void Prorroga_programado(cierreCuentaDetalle entidad, Int64 pacod_ope, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pCodigo_prorroga = cmdTransaccionFactory.CreateParameter();
                        pCodigo_prorroga.ParameterName = "P_COD_PRORROGA";
                        pCodigo_prorroga.Value = entidad.Codigo_prorroga;
                        pCodigo_prorroga.Direction = ParameterDirection.Output;
                        //pnumero_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pCodigo_prorroga);



                        DbParameter pnumero_programado = cmdTransaccionFactory.CreateParameter();
                        pnumero_programado.ParameterName = "P_NUMERO_PROGRAMADO";
                        pnumero_programado.Value = entidad.NumeroProgramado;
                        pnumero_programado.Direction = ParameterDirection.Input;
                        //pnumero_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_programado);


                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "P_FECHA_INICIO";
                        pfecha_inicio.Value = entidad.Fecha_Apertura;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        //pfecha_liquida.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);


                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "P_FECHA_FINAL";
                        pfecha_final.Value = entidad.Fecha_Vencimiento;
                        pfecha_final.Direction = ParameterDirection.Input;
                        //pfecha_liquida.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);
              

                        DbParameter P_TIPOINTERES = cmdTransaccionFactory.CreateParameter();
                        P_TIPOINTERES.ParameterName = "P_TIPO_INTERES";
                        P_TIPOINTERES.Value = entidad.calculo_tasa;
                        P_TIPOINTERES.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_TIPOINTERES);


                        DbParameter P_TASAINTERES = cmdTransaccionFactory.CreateParameter();
                        P_TASAINTERES.ParameterName = "P_TASA_INTERES";
                        P_TASAINTERES.Value = entidad.Tasa;
                        P_TASAINTERES.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_TASAINTERES);

                        DbParameter P_COD_TIPOTASA = cmdTransaccionFactory.CreateParameter();
                        P_COD_TIPOTASA.ParameterName = "P_COD_TIPO_TASA";
                        P_COD_TIPOTASA.Value = entidad.TipoTasa;
                        P_COD_TIPOTASA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_TIPOTASA);

                        DbParameter P_TIPOHISTORICO = cmdTransaccionFactory.CreateParameter();
                        P_TIPOHISTORICO.ParameterName = "P_TIPO_HISTORICO";
                        P_TIPOHISTORICO.Value = entidad.tipo_historico;
                        P_TIPOHISTORICO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_TIPOHISTORICO);

                        DbParameter P_DESVIACION = cmdTransaccionFactory.CreateParameter();
                        P_DESVIACION.ParameterName = "P_DESVIACION";
                        P_DESVIACION.Value = entidad.desviacion;
                        P_DESVIACION.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_DESVIACION);

                        DbParameter P_COD_PERIODICIDAD_INT = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERIODICIDAD_INT.ParameterName = "P_COD_PERIODICIDAD_INT";
                        P_COD_PERIODICIDAD_INT.Value = entidad.Codigo_Perioricidad_int;
                        P_COD_PERIODICIDAD_INT.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERIODICIDAD_INT);                       

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "P_COD_OPE";
                        pcod_ope.Value = pacod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        //pcodigo_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);


                        DbParameter P_VALOR_CUOTA = cmdTransaccionFactory.CreateParameter();
                        P_VALOR_CUOTA.ParameterName = "P_VALOR_CUOTA";
                        P_VALOR_CUOTA.Value = entidad.cuota;
                        P_VALOR_CUOTA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR_CUOTA);
                        

                        DbParameter P_PLAZO = cmdTransaccionFactory.CreateParameter();
                        P_PLAZO.ParameterName = "P_PLAZO";
                        P_PLAZO.Value = entidad.Plazo;
                        P_PLAZO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_PLAZO);


                        DbParameter P_COD_PERIODICIDAD= cmdTransaccionFactory.CreateParameter();
                        P_COD_PERIODICIDAD.ParameterName = "P_COD_PERIODICIDAD";
                        P_COD_PERIODICIDAD.Value = entidad.Codigo_Perioricidad;
                        P_COD_PERIODICIDAD.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERIODICIDAD);



                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_PRORROGA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);



                     //   DAauditoria.InsertarLog(entidad, "Ahorro_programado", pusuario, Accion.Crear.ToString(), TipoAuditoria.AhorroProgramado, "Creacion de Prorroga de ahorro programado con numero de programado " + entidad.NumeroProgramado);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "Prorroga_programado", ex);
                    }
                }
            }
        }

        public List<ELiquidacionInteres> getListaCuentasLiquidar(DateTime pfechaLiquidacion, String pCodLinea, Usuario vUsuario)
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


                        DbParameter pcod_linea_programado = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_programado.ParameterName = "pcod_linea_programado";
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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LIQUIDACION";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "getListaCuentasLiquidar", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.Connection = connection;

                        string sql = @"select tem.*, vp.nombre, vp.identificacion, ap.FECHA_INTERES
                                        from TEMP_LIQUIDAPRO tem
                                        inner join AHORRO_PROGRAMADO ap on tem.NUMERO_PROGRAMADO = ap.NUMERO_PROGRAMADO
                                        inner join v_persona vp on tem.cod_persona = vp.cod_persona";
                        //sql = "select * from TEMP_LIQUIDAPRO";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ELiquidacionInteres entidad = new ELiquidacionInteres();

                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.NumeroCuenta = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.Linea = Convert.ToInt64(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.Identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.Cod_Usuario = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["COD_OFICINA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.Fecha_Apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.Tasa_interes = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["DIAS"] != DBNull.Value) entidad.dias = Convert.ToInt16(resultado["DIAS"]);
                            if (resultado["INTERES"] != DBNull.Value) entidad.Interes = Convert.ToDecimal(resultado["INTERES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.Retefuente = Convert.ToDecimal(resultado["RETENCION"]);
                            if (resultado["VALOR_NETO"] != DBNull.Value) entidad.valor_Neto = Convert.ToDecimal(resultado["VALOR_NETO"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_int = Convert.ToDateTime(resultado["FECHA_INTERES"]);

                            listaEntidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listaEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ELiquidacionInteres", ex);
                        return null;
                    }
                };
            }
        }

        // inserta la liquidacion de intereses 
        public void InsertLiquidacion(Int64 pcod_op, String pnum_programado, Int64 pcod_Cliente, Int64 ptipo_tran, Decimal pvalor, Usuario pusuario, Int64 pestado, DateTime fecha)
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
                        p_cod_ope.Value = pcod_op;
                        p_cod_ope.Direction = ParameterDirection.Input;
                        p_cod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        DbParameter p_numero_programado = cmdTransaccionFactory.CreateParameter();
                        p_numero_programado.ParameterName = "p_numero_programado";
                        p_numero_programado.Value = pnum_programado;
                        p_numero_programado.Direction = ParameterDirection.Input;
                        p_numero_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_numero_programado);

                        DbParameter p_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        p_cod_cliente.ParameterName = "p_cod_cliente";
                        p_cod_cliente.Value = pcod_Cliente;
                        p_cod_cliente.Direction = ParameterDirection.Input;
                        p_cod_cliente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cliente);

                        DbParameter p_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tran.ParameterName = "p_tipo_tran";
                        p_tipo_tran.Value = ptipo_tran;
                        p_tipo_tran.Direction = ParameterDirection.Input;
                        p_tipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tran);

                        DbParameter p_cod_det_lis = cmdTransaccionFactory.CreateParameter();
                        p_cod_det_lis.ParameterName = "p_cod_det_lis";
                        p_cod_det_lis.Value = DBNull.Value;
                        p_cod_det_lis.Direction = ParameterDirection.Input;
                        p_cod_det_lis.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_det_lis);


                        DbParameter p_documento_soporte = cmdTransaccionFactory.CreateParameter();
                        p_documento_soporte.ParameterName = "p_documento_soporte";
                        p_documento_soporte.Value = DBNull.Value;
                        p_documento_soporte.Direction = ParameterDirection.Input;
                        p_documento_soporte.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_documento_soporte);

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.Value = pvalor;
                        p_valor.Direction = ParameterDirection.Input;
                        p_valor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pestado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        DbParameter p_num_tran_anula = cmdTransaccionFactory.CreateParameter();
                        p_num_tran_anula.ParameterName = "p_num_tran_anula";
                        p_num_tran_anula.Value = DBNull.Value;
                        p_num_tran_anula.Direction = ParameterDirection.Input;
                        p_num_tran_anula.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_num_tran_anula);


                        DbParameter pn_fecha_interes = cmdTransaccionFactory.CreateParameter();
                        pn_fecha_interes.ParameterName = "pn_fecha_interes";
                        pn_fecha_interes.Value = fecha;
                        pn_fecha_interes.Direction = ParameterDirection.Input;
                        pn_fecha_interes.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pn_fecha_interes);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_TRAN_PROGR_CREAR";

                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "InsertLiquidacion", ex);
                    }
                }
            }
        }

        public List<Xpinn.Ahorros.Entities.ReporteMovimiento> ListarDetalleExtracto(String codcuenta, DateTime pFechaPago, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbDataReader resultado = default(DbDataReader);
                        List<Xpinn.Ahorros.Entities.ReporteMovimiento> lstExtracto = new List<Xpinn.Ahorros.Entities.ReporteMovimiento>();
                        Configuracion global = new Configuracion();
                        string sql1 = @"Select DISTINCT( o.cod_ope) AS COD_OPE, o.fecha_oper, o.tipo_ope, p.descripcion AS nomtipo_ope,  o.cod_ofi, f.nombre, 
                        Case r.tipo_mov When 1 Then 'Débito' When 2 Then 'Crédito' End As tipo_mov, t.valor
                        From TRAN_PROGRAMADO t 
                        Inner Join Operacion O On t.cod_ope = o.cod_ope 
                        Inner Join tipo_ope p On p.tipo_ope = o.tipo_ope
                        Inner Join oficina f On o.cod_ofi = f.cod_oficina
                        Inner Join tipo_tran r On t.tipo_tran = r.tipo_tran
                        Where t.numero_PROGRAMADO = '" + codcuenta + "'";
                        string sql = sql1 + " Order by o.fecha_oper ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        decimal saldo = 0;
                        while (resultado.Read())
                        {
                            Xpinn.Ahorros.Entities.ReporteMovimiento entidad = new Xpinn.Ahorros.Entities.ReporteMovimiento();
                            if (resultado["fecha_oper"] != DBNull.Value) entidad.fecha_oper = Convert.ToDateTime(resultado["fecha_oper"]);
                            if (resultado["tipo_ope"] != DBNull.Value) entidad.tipo_ope = Convert.ToString(resultado["tipo_ope"]);
                            if (resultado["nomtipo_ope"] != DBNull.Value) entidad.nomtipo_ope = Convert.ToString(resultado["nomtipo_ope"]);
                            if (resultado["cod_ofi"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["cod_ofi"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["tipo_mov"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["tipo_mov"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToInt32(resultado["valor"]);
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt32(resultado["cod_ope"]);

                            // Calcular el saldo de la cuenta
                            if (entidad.tipo_mov == "1")
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

        public List<CuentasProgramado> ListarAhorroExtractos(CuentasProgramado cuentasProgramado, Usuario usuario, String filtro)
        {
            DbDataReader resultado;
            List<CuentasProgramado> lstAhorroVista = new List<CuentasProgramado>();
            Configuracion conf = new Configuracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = @"SELECT pp.valor as pendiente,a.fecha_proximo_pago as fecha_cuota, a.*, l.nombre AS nom_linea_ahorro, o.nombre AS nom_oficina, p.* FROM Ahorro_Programado a INNER JOIN LineaProgramado l On a.cod_linea_programado = l.cod_linea_programado INNER JOIN Oficina o ON a.cod_oficina = o.cod_oficina INNER JOIN v_persona p ON a.cod_persona = p.cod_persona   left JOIN pendiente_programado pp  ON pp.numero_programado=a.numero_programado where 1=1" + filtro;

                        string sql = @"SELECT a.*, l.nombre AS nom_linea_ahorro, o.nombre AS nom_oficina, p.*,(Select sum(p.saldo) From pendiente_programado p Where p.numero_programado=a.numero_programado) As pendiente FROM Ahorro_Programado a INNER JOIN LineaProgramado l On a.cod_linea_programado = l.cod_linea_programado INNER JOIN Oficina o ON a.cod_oficina = o.cod_oficina INNER JOIN v_persona p ON a.cod_persona = p.cod_persona    where 1=1" + filtro;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentasProgramado entidad = new CuentasProgramado();
                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["NOM_LINEA_AHORRO"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA_AHORRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["TOTAL_RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["TOTAL_RETENCION"]);
                            if (resultado["FECHA_PRIMERA_CUOTA"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PRIMERA_CUOTA"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["PENDIENTE"] != DBNull.Value) entidad.pendiente = Convert.ToDecimal(resultado["PENDIENTE"]);
                            entidad.valor_total = entidad.valor_cuota + entidad.pendiente;
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);

                            lstAhorroVista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ListaAhorroExtractos", ex);
                        return null;
                    }
                }
            }
        }

        public void cierreHistorico(Usuario pUsuario, DateTime fechaCierre, String pEstado, ref String sError)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.Value = fechaCierre;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Direction = ParameterDirection.Input;
                        pestado.Value = pEstado;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "pusuario";
                        pusuario.Direction = ParameterDirection.Input;
                        pusuario.Value = pUsuario.nombre;
                        pusuario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_CIERREHISTORICO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "cierreHistorico", ex);
                        sError = ex.Message;
                    }
                }
            }
        }

        public DateTime verificaFecha(Usuario pUsuario)
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
                        string sql = "select max(fecha) as Fecha from cierea where tipo='L' and estado='D'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                            if (resultado["Fecha"] != DBNull.Value) fecha = Convert.ToDateTime(resultado["Fecha"]);

                        dbConnectionFactory.CerrarConexion(connection);
                        return fecha;

                        //if (pFecha <= fecha)
                        //    return false;
                        //else return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ListarAhorrosProgramado", ex);
                        return DateTime.MinValue;
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
                        BOExcepcion.Throw("CuentasProgramadoData", "ListarAhorrosProgramado", ex);
                        return DateTime.MinValue;
                    }
                }
            }
        }

        public DateTime getfechaUltimaCierreAhorroP(Usuario pUsuario)
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
                        string sql = "Select Max(fecha) as fecha From cierea Where tipo = 'L' And estado = 'D'";

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
                        BOExcepcion.Throw("CuentasProgramadoData", "ListarAhorrosProgramado", ex);
                        return DateTime.MinValue;
                    }
                }
            }
        }

        public void UpdateRetiroAhorro(Usuario pusuario, String pNumeroProgramado, Int64 pCodigoCliente, Int64 pCodigoOperacion, DateTime pFechaPago, decimal pValor)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pnumero_programado = cmdTransaccionFactory.CreateParameter();
                        pnumero_programado.ParameterName = "pnumero_programado";
                        pnumero_programado.Value = pNumeroProgramado;
                        pnumero_programado.Direction = ParameterDirection.Input;
                        pnumero_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_programado);

                        DbParameter pcod_cliente = cmdTransaccionFactory.CreateParameter();
                        pcod_cliente.ParameterName = "pcod_cliente";
                        pcod_cliente.Value = pCodigoCliente;
                        pcod_cliente.Direction = ParameterDirection.Input;
                        pcod_cliente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_cliente);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcod_ope";
                        pcod_ope.Value = pCodigoOperacion;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.UInt64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pfecha_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_pago.ParameterName = "pfecha_pago";
                        pfecha_pago.Value = pFechaPago;
                        pfecha_pago.Direction = ParameterDirection.Input;
                        pfecha_pago.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha_pago);

                        DbParameter pvalor_pago = cmdTransaccionFactory.CreateParameter();
                        pvalor_pago.ParameterName = "pvalor_pago";
                        pvalor_pago.Value = pValor;
                        pvalor_pago.Direction = ParameterDirection.Input;
                        pvalor_pago.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_pago);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_RETIROCUENTA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        CuentasProgramado cuentasProgramado = new CuentasProgramado
                        {
                            numero_programado = pNumeroProgramado,
                            cod_persona = pCodigoCliente,
                            cod_operacion = pCodigoOperacion,
                            fecha_pago = pFechaPago,
                            valor_pago = pValor
                        };

                        DAauditoria.InsertarLog(cuentasProgramado, "Ahorro_programado", pusuario, Accion.Crear.ToString(), TipoAuditoria.AhorroProgramado, "Creacion de retiro parcial de Ahorro progrmaado con numero de programado" + cuentasProgramado.numero_programado);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "UpdateRetiroAhorro", ex);
                    }
                }
            }
        }


        public void trasladoCuenta(String pnuemroP, Int64 pcodUsuario, Int64 pcod_opera, DateTime pfecha_pago, decimal valorpag, Int64 ptipotran, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pn_num_producto = cmdTransaccionFactory.CreateParameter();
                        pn_num_producto.ParameterName = "pn_num_producto";
                        pn_num_producto.Value = pnuemroP;
                        pn_num_producto.Direction = ParameterDirection.Input;
                        //pnumero_programado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pn_num_producto);

                        DbParameter pn_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        pn_cod_cliente.ParameterName = "pn_cod_cliente";
                        pn_cod_cliente.Value = pcodUsuario;
                        pn_cod_cliente.Direction = ParameterDirection.Input;
                        //pfecha_liquida.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pn_cod_cliente);

                        DbParameter Pn_Cod_Ope = cmdTransaccionFactory.CreateParameter();
                        Pn_Cod_Ope.ParameterName = "Pn_Cod_Ope";
                        Pn_Cod_Ope.Value = pcod_opera;
                        Pn_Cod_Ope.Direction = ParameterDirection.Input;
                        // pinteres.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(Pn_Cod_Ope);

                        DbParameter pf_fecha_pago = cmdTransaccionFactory.CreateParameter();
                        pf_fecha_pago.ParameterName = "pf_fecha_pago";
                        pf_fecha_pago.Value = pfecha_pago;
                        pf_fecha_pago.Direction = ParameterDirection.Input;
                        //pretencion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pf_fecha_pago);

                        DbParameter pn_valor_pago = cmdTransaccionFactory.CreateParameter();
                        pn_valor_pago.ParameterName = "pn_valor_pago";
                        pn_valor_pago.Value = valorpag;
                        pn_valor_pago.Direction = ParameterDirection.Input;
                        //pcodigo_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pn_valor_pago);

                        DbParameter pn_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        pn_tipo_tran.ParameterName = "pn_tipo_tran";
                        pn_tipo_tran.Value = ptipotran;
                        pn_tipo_tran.Direction = ParameterDirection.Input;
                        // pValorGMF.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pn_tipo_tran);

                        DbParameter pn_num_tran = cmdTransaccionFactory.CreateParameter();
                        pn_num_tran.ParameterName = "pn_num_tran";
                        pn_num_tran.Value = ptipotran;
                        pn_num_tran.Direction = ParameterDirection.Output;
                        // pValorGMF.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pn_num_tran);

                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_REALIZAPAGO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ReportePeriodico", ex);
                    }
                }
            }
        }

        public void creaDeposito(Xpinn.Programado.Entities.CuentasProgramado entidad, Usuario pUsuario)
        {

        }

        public List<CuentasProgramado> ListarParametrizacionCuentas(CuentasProgramado pNumeracionAhorros, Usuario pUsuario)
        {
            DbDataReader resultado;

            List<CuentasProgramado> lstNumeracionAhorros = new List<CuentasProgramado>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"SELECT * FROM NUMERACION_CUENTAS " + ObtenerFiltro(pNumeracionAhorros) + " ORDER BY posicion ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentasProgramado entidad = new CuentasProgramado();
                            if (resultado["POSICION"] != DBNull.Value) entidad.posicion = Convert.ToInt32(resultado["POSICION"]);
                            if (resultado["TIPO_CAMPO"] != DBNull.Value) entidad.tipo_campo = Convert.ToInt32(resultado["TIPO_CAMPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
                            if (resultado["LONGITUD"] != DBNull.Value) entidad.longitud = Convert.ToInt32(resultado["LONGITUD"]);
                            if (resultado["ALINEAR"] != DBNull.Value) entidad.alinear = Convert.ToString(resultado["ALINEAR"]);
                            if (resultado["CARACTER_LLENADO"] != DBNull.Value) entidad.caracter_llenado = Convert.ToString(resultado["CARACTER_LLENADO"]);
                            lstNumeracionAhorros.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNumeracionAhorros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ListarNumeracionAhorros", ex);
                        return null;
                    }
                }
            }
        }
        public CuentasProgramado ConsultarNumeracionProgramado(CuentasProgramado pAhorroProgramado, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasProgramado entidad = new CuentasProgramado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select max(numero_programado) from ahorro_programado";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["numero_programado"] != DBNull.Value) entidad.numero_programado = Convert.ToString(resultado["numero_programado"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ConsultarNumeracionProgramado", ex);
                        return null;
                    }
                }
            }
        }

        public CuentasProgramado ConsultarNumeracion(CuentasProgramado pCadt, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasProgramado entidad = new CuentasProgramado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from General where Codigo = 580";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) entidad.valornum = Convert.ToDecimal(resultado["VALOR"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ConsultarNumeracion", ex);
                        return null;
                    }
                }
            }
        }

        public ELiquidacionInteres CrearLiquidacionProgramado(ELiquidacionInteres pLiqui, Usuario vUsuario)
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

                        DbParameter p_dias = cmdTransaccionFactory.CreateParameter();
                        p_dias.ParameterName = "p_dias";
                        if (pLiqui.dias != null) p_dias.Value = pLiqui.dias; else p_dias.Value = DBNull.Value;
                        p_dias.Direction = ParameterDirection.Input;
                        p_dias.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_dias);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = 1;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_LIQUIDACIO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pLiqui.cod_interes = Convert.ToInt32(pcod_interes.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLiqui;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "CrearLiquidacionProgramado", ex);
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

        public DateTime? FechaPrimerPago(CuentasProgramado vProgramado, Usuario vUsuario)
        {
            DateTime? pFechaInicio = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_linea_programado = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_programado.ParameterName = "p_cod_linea_programado";
                        p_cod_linea_programado.Value = vProgramado.cod_linea_programado;
                        p_cod_linea_programado.Direction = ParameterDirection.Input;
                        p_cod_linea_programado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_programado);

                        DbParameter p_cod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        p_cod_periodicidad.ParameterName = "p_cod_periodicidad";
                        p_cod_periodicidad.Value = vProgramado.cod_periodicidad;
                        p_cod_periodicidad.Direction = ParameterDirection.Input;
                        p_cod_periodicidad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_periodicidad);

                        DbParameter p_cod_empresa = cmdTransaccionFactory.CreateParameter();
                        p_cod_empresa.ParameterName = "p_cod_empresa";
                        p_cod_empresa.Value = vProgramado.cod_empresa;
                        p_cod_empresa.Direction = ParameterDirection.Input;
                        p_cod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_empresa);

                        DbParameter p_fecha_apertura = cmdTransaccionFactory.CreateParameter();
                        p_fecha_apertura.ParameterName = "p_fecha_apertura";
                        p_fecha_apertura.Value = vProgramado.fecha_apertura;
                        p_fecha_apertura.Direction = ParameterDirection.Input;
                        p_fecha_apertura.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_apertura);

                        DbParameter p_forma_pago = cmdTransaccionFactory.CreateParameter();
                        p_forma_pago.ParameterName = "p_forma_pago";
                        p_forma_pago.Value = vProgramado.forma_pago;
                        p_forma_pago.Direction = ParameterDirection.Input;
                        p_forma_pago.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_forma_pago);

                        DbParameter p_fecha_inicio = cmdTransaccionFactory.CreateParameter();
                        p_fecha_inicio.ParameterName = "p_fecha_inicio";
                        p_fecha_inicio.Value = vProgramado.fecha_primera_cuota;
                        p_fecha_inicio.Direction = ParameterDirection.Output;
                        p_fecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_inicio);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_FECHAINICIO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (p_fecha_inicio.Value != null)
                            try { pFechaInicio = Convert.ToDateTime(p_fecha_inicio.Value); } catch { pFechaInicio = null; }

                        return pFechaInicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "FechaPrimerPago", ex);
                        return null;
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


        public List<provision_programado> ListarProvision(DateTime pFechaIni, provision_programado pAhorroProgramado, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<provision_programado> lstProvision = new List<provision_programado>();

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
                        cod_linea_ahorro.ParameterName = "PCODLINEAPROGRAMADO";
                        cod_linea_ahorro.Direction = ParameterDirection.Input;
                        cod_linea_ahorro.Value = pAhorroProgramado.cod_linea_programado;
                        cod_linea_ahorro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(cod_linea_ahorro);

                        DbParameter cod_oficina = cmdTransaccionFactory.CreateParameter();
                        cod_oficina.ParameterName = "PCODOFICINA";
                        cod_oficina.Direction = ParameterDirection.Input;
                        cod_oficina.Value = pAhorroProgramado.cod_oficina;
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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_PROVISION";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select * From TEMP_PROVISIONPRO ";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            provision_programado entidad = new provision_programado();

                            if (resultado["numero_programado"] != DBNull.Value) entidad.numero_programado = Convert.ToString(resultado["numero_programado"].ToString());
                            if (resultado["fecha_apertura"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["fecha_apertura"].ToString());
                            if (resultado["saldo"] != DBNull.Value) entidad.saldo_total = Convert.ToInt32(resultado["saldo"]);
                            if (resultado["saldo_base"] != DBNull.Value) entidad.saldo_base = Convert.ToInt32(resultado["saldo_base"]);
                            if (resultado["provision_interes"] != DBNull.Value) entidad.intereses = Convert.ToInt32(resultado["provision_interes"]);
                            if (resultado["retencion"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["retencion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["cod_oficina"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["nombre"]);
                            if (resultado["cod_linea_programado"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["cod_linea_programado"]);
                            if (resultado["tasa"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["tasa"]);
                            if (resultado["dias"] != DBNull.Value) entidad.dias = Convert.ToInt32(resultado["dias"]);
                            if (resultado["valor_acumulado"] != DBNull.Value) entidad.valor_acumulado = Convert.ToInt32(resultado["valor_acumulado"]);

                            lstProvision.Add(entidad);
                        }
                        return lstProvision;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ListarProvision", ex);
                        return null;
                    }
                }
            }
        }
        public provision_programado InsertarDatos(provision_programado Insertar_cuenta, Usuario vUsuario)
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
                        numero_cuenta.Value = Insertar_cuenta.numero_programado;
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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_CAUS_CREAR";
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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_CIEREA_CREAR";
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

        public CuentasProgramado ConsultarPeriodicidadProgramado(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasProgramado entidad = new CuentasProgramado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * from periodicidad WHERE cod_periodicidad = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["numero_dias"] != DBNull.Value) entidad.numero_dias = Convert.ToInt16(resultado["numero_dias"]);
                            if (resultado["tipo_calendario"] != DBNull.Value) entidad.tipo_calendario = Convert.ToInt16(resultado["tipo_calendario"]);
                           
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ConsultarPeriodicidadProgramado", ex);
                        return null;
                    }
                }
            }
        }


        public CuentasProgramado ConsultarCierreAhorroProgramado(Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasProgramado entidad = new CuentasProgramado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT MAX(FECHA) as fecha,estado FROM CIEREA WHERE TIPO = 'L' AND ESTADO = 'D'   group by estado";
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
        public List<CuentasProgramado> ListarPrograClubAhorradores(Int64 pcliente, Boolean pResult, string pFiltroAdd, Usuario vUsuario)
        {
            DbDataReader resultado;

            List<CuentasProgramado> lstAporte = new List<CuentasProgramado>();
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
                            sql = @"  SELECT DISTINCT Ahorro_programado.*, lineaprogramado.cod_linea_programado,lineaprogramado.Nombre AS nom_linea,  (SELECT e.nom_empresa FROM empresa_recaudo e WHERE e.cod_empresa = ahorro_programado.cod_empresa) AS nom_empresa,   
                                    CASE nov.estado WHEN '0' THEN 'Solicitado' WHEN '1' THEN 'Aprobado' WHEN '2' THEN 'Negado' ELSE ' ' END AS Estado_modificacion, 'PROPIO' AS TIPO_APORTE,SALDO_ACUMULADO(3,ahorro_programado.NUMERO_PROGRAMADO) AS VALOR_ACUMULADO
                                        FROM ahorro_programado 
                                        INNER JOIN lineaprogramado ON ahorro_programado.cod_linea_programado = lineaprogramado.cod_linea_programado
                                        LEFT JOIN Novedad_Cambio_Progra nov ON ahorro_programado.NUMERO_PROGRAMADO = nov.NUMERO_PROGRAMADO
                                        WHERE ahorro_programado.cod_persona = " + pcliente;
                            if (!string.IsNullOrWhiteSpace(pFiltroAdd))
                                sql += " " + pFiltroAdd;
                            sql += " order by lineaprogramado.cod_linea_programado";
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
                            CuentasProgramado entidad = new CuentasProgramado();
                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numero_programado = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["nom_linea"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["nom_linea"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (entidad.cod_forma_pago == 1)
                                entidad.nom_formapago = "Caja";
                            if (entidad.cod_forma_pago == 2)
                                entidad.nom_formapago = "Nomina";
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            //if (resultado["FECHA_ULTIMA_MOD"] != DBNull.Value) entidad.fecha_ultima_mod = Convert.ToDateTime(resultado["FECHA_ULTIMA_MOD"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["TOTAL_INTERES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["TOTAL_INTERES"]);
                            if (resultado["TOTAL_RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["TOTAL_RETENCION"]);
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
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
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
                        BOExcepcion.Throw("CuentasProgramadoData", "ListarPrograClubAhorradores", ex);
                        return null;
                    }
                }
            }
        }
        public CuentasProgramado CrearNovedadCambio(CuentasProgramado vProgramado, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idnovedad = cmdTransaccionFactory.CreateParameter();
                        p_idnovedad.ParameterName = "P_IDNOVEDAD";
                        p_idnovedad.Direction = ParameterDirection.Output;
                        p_idnovedad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idnovedad);

                        DbParameter p_numero_aporte = cmdTransaccionFactory.CreateParameter();
                        p_numero_aporte.ParameterName = "P_NUMERO_PROGRAMADO";
                        p_numero_aporte.Value = vProgramado.numero_programado;
                        p_numero_aporte.Direction = ParameterDirection.Input;
                        p_numero_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numero_aporte);

                        DbParameter p_fecha = cmdTransaccionFactory.CreateParameter();
                        p_fecha.ParameterName = "P_FECHA_CAMBIO_DES";
                        if (vProgramado.fecha_empieza_cambio == null)
                            p_fecha.Value = DBNull.Value;
                        else
                            p_fecha.Value = vProgramado.fecha_empieza_cambio;
                        p_fecha.Direction = ParameterDirection.Input;
                        p_fecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha);

                        DbParameter p_val_anterior = cmdTransaccionFactory.CreateParameter();
                        p_val_anterior.ParameterName = "P_VAL_ANTERIOR";
                        p_val_anterior.Value = vProgramado.valor_cuota.ToString();
                        p_val_anterior.Direction = ParameterDirection.Input;
                        p_val_anterior.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_val_anterior);

                        DbParameter p_val_nuevo = cmdTransaccionFactory.CreateParameter();
                        p_val_nuevo.ParameterName = "P_VAL_NUEVO";
                        p_val_nuevo.Value = vProgramado.nuevo_valor_cuota.ToString();
                        p_val_nuevo.Direction = ParameterDirection.Input;
                        p_val_nuevo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_val_nuevo);

                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "P_OBSERVACIONES";

                        if (!string.IsNullOrWhiteSpace(vProgramado.observaciones))
                        {
                            p_observaciones.Value = vProgramado.observaciones;
                        }
                        else
                        {
                            p_observaciones.Value = DBNull.Value;
                        }
                        p_observaciones.Direction = ParameterDirection.Input;
                        p_observaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_observaciones);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "P_ESTADO";
                        p_estado.Value = "0"; // Solicitado
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        DbParameter p_fecha_novedad = cmdTransaccionFactory.CreateParameter();
                        p_fecha_novedad.ParameterName = "P_FECHA_NOVEDAD";
                        p_fecha_novedad.Value = DateTime.Today;
                        p_fecha_novedad.Direction = ParameterDirection.Input;
                        p_fecha_novedad.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_novedad);

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "P_COD_PERSONA";
                        p_cod_persona.Value = vProgramado.cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_NOVEDAD_CA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        vProgramado.id_novedad_cambio = p_idnovedad.Value != DBNull.Value ? Convert.ToInt64(p_idnovedad.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);
                        return vProgramado;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "CrearNovedadCambio", ex);
                        return null;
                    }
                }
            }
        }
        public List<CuentasProgramado> ListarPrograNovedadesCambio(string filtro, Usuario usuario)
        {
            DbDataReader resultado;
            List<CuentasProgramado> lstEntidad = new List<CuentasProgramado>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @" SELECT DISTINCT ahorro_programado.*, nov.fecha_novedad, nov.idnovedad, nov.val_nuevo, nov.fecha_cambio_deseada, nov.observaciones as ob,
                                        CASE nov.estado WHEN '0' THEN 'Solicitado' WHEN '1' THEN 'Aprobado' WHEN '2' THEN 'Negado' ELSE ' ' END AS Estado_modificacion,
                                        lineaprogramado.nombre
                                        FROM ahorro_programado 
                                        INNER JOIN lineaprogramado ON ahorro_programado.cod_linea_programado = lineaprogramado.cod_linea_programado      
                                        JOIN novedad_cambio_progra nov ON ahorro_programado.numero_programado= nov.NUMERO_programado
                                        WHERE ahorro_programado.estado = 1 " + filtro + " order by nov.fecha_novedad desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentasProgramado entidad = new CuentasProgramado();
                            if (resultado["idnovedad"] != DBNull.Value) entidad.id_novedad_cambio = Convert.ToInt64(resultado["idnovedad"]);
                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numero_programado = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["nombre"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["val_nuevo"] != DBNull.Value) entidad.nuevo_valor_cuota = Convert.ToDecimal(resultado["val_nuevo"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["fecha_cambio_deseada"] != DBNull.Value) entidad.fecha_empieza_cambio = Convert.ToDateTime(resultado["fecha_cambio_deseada"]);
                            if (resultado["fecha_novedad"] != DBNull.Value) entidad.fecha_novedad_cambio = Convert.ToDateTime(resultado["fecha_novedad"]);
                            //if (resultado["FECHA_ULTIMA_MOD"] != DBNull.Value) entidad.fecha_ultima_mod = Convert.ToDateTime(resultado["FECHA_ULTIMA_MOD"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["Estado_Modificacion"] != DBNull.Value) entidad.estado_modificacion = Convert.ToString(resultado["Estado_Modificacion"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
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
                        BOExcepcion.Throw("CuentasProgramadoData", "ListarPrograNovedadesCambio", ex);
                        return null;
                    }
                }
            }
        }
        public void ModificarNovedadCuotaProgra(CuentasProgramado vprogramado, Usuario pUsuario)
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
                        p_numero_aporte.ParameterName = "p_numero_programado";
                        p_numero_aporte.Value = vprogramado.numero_programado;
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
                        p_val_anterior.Value = vprogramado.valor_cuota;
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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_NOVEDAD_CA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ModificarNovedadCuotaProgra", ex);
                    }
                }
            }
        }



        public List<ProgramadoCuotasExtras> ConsultarCuotasExtrasProgramado(String pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ProgramadoCuotasExtras> lstCuotasExtras = new List<ProgramadoCuotasExtras>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CUOTAS_EXT_AHO_PROGRAMADO WHERE NUMERO_PROGRAMADO = '" + pId + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        ProgramadoCuotasExtras entidad;
                        while (resultado.Read())
                        {
                            entidad = new ProgramadoCuotasExtras();


                            if (resultado["COD_CUOTA"] != DBNull.Value) entidad.cod_cuota = Convert.ToInt32(resultado["COD_CUOTA"]);
                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numero_programado = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["FECHA_PAGO"] != DBNull.Value) entidad.fecha_pago = Convert.ToDateTime(resultado["FECHA_PAGO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["VALOR_CAPITAL"] != DBNull.Value) entidad.valor_capital = Convert.ToDecimal(resultado["VALOR_CAPITAL"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);

                            lstCuotasExtras.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuotasExtras;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasProgramadoData", "ConsultarCuotasExtrasProgramado", ex);
                        return null;
                    }
                }
            }
        }


    }
}