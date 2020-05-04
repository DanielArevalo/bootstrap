using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla CreditoRecoger
    /// </summary>
    public class CreditoRecogerData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla CreditoRecoger
        /// </summary>
        public CreditoRecogerData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla CreditoRecoger de la base de datos
        /// </summary>
        /// <param name="pCreditoRecoger">Entidad CreditoRecoger</param>
        /// <returns>Entidad CreditoRecoger creada</returns>
        public CreditoRecoger CrearCreditoRecoger(CreditoRecoger pCreditoRecoger, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.Value = pCreditoRecoger.numero_radicacion;

                        DbParameter p_numero_recoge = cmdTransaccionFactory.CreateParameter();
                        p_numero_recoge.ParameterName = "p_numero_recoge";
                        p_numero_recoge.Value = pCreditoRecoger.numero_credito;

                        DbParameter p_valorrecoge = cmdTransaccionFactory.CreateParameter();
                        p_valorrecoge.ParameterName = "p_valorrecoge";
                        p_valorrecoge.Value = pCreditoRecoger.valor_recoge;

                        DbParameter p_fechapago = cmdTransaccionFactory.CreateParameter();
                        p_fechapago.ParameterName = "p_fechapago";
                        p_fechapago.Value = pCreditoRecoger.fecha_pago;

                        DbParameter p_saldocapital = cmdTransaccionFactory.CreateParameter();
                        p_saldocapital.ParameterName = "p_saldocapital";
                        p_saldocapital.Value = pCreditoRecoger.saldo_capital;

                        DbParameter p_valor_nominas = cmdTransaccionFactory.CreateParameter();
                        p_valor_nominas.ParameterName = "p_valor_nominas";
                        p_valor_nominas.Value = pCreditoRecoger.valor_nominas;

                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_numero_recoge);
                        cmdTransaccionFactory.Parameters.Add(p_valorrecoge);
                        cmdTransaccionFactory.Parameters.Add(p_fechapago);
                        cmdTransaccionFactory.Parameters.Add(p_saldocapital);
                        cmdTransaccionFactory.Parameters.Add(p_valor_nominas);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_DESEM_DES_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCreditoRecoger, "CreditoRecoger", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        return pCreditoRecoger;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoRecogerData", "CrearCreditoRecoger", ex);
                        return null;
                    }
                }
            }
        }


        public int ConsultaPermisoModificarTasa(string cod, Usuario pUsuario)
        {

            DbDataReader resultado;
            List<CreditoRecoger> lstCreditoRecoger = new List<CreditoRecoger>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select count(*) as resultado From usuario_atribuciones Where codusuario=" + cod + " And tipoatribucion = 2 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        int resulta = 0;
                        if (resultado.Read())
                        {

                            if (resultado["resultado"] != DBNull.Value) resulta = Convert.ToInt32(resultado["resultado"]);

                        }
                        return resulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoRecogerData", "ConsultarCreditoRecoger", ex);
                        return 0;
                    }
                }
            }

        }
        /// <summary>
        /// Modifica un registro en la tabla CreditoRecoger de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad CreditoRecoger modificada</returns>
        public CreditoRecoger ModificarCreditoRecoger(CreditoRecoger pCreditoRecoger, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMERO_CREDITO = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_CREDITO.ParameterName = param + "NUMERO_CREDITO";
                        pNUMERO_CREDITO.Value = pCreditoRecoger.numero_credito;

                        DbParameter pLINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        pLINEA_CREDITO.ParameterName = param + "LINEA_CREDITO";
                        pLINEA_CREDITO.Value = pCreditoRecoger.linea_credito;

                        DbParameter pMONTO = cmdTransaccionFactory.CreateParameter();
                        pMONTO.ParameterName = param + "MONTO";
                        pMONTO.Value = pCreditoRecoger.monto;

                        DbParameter pSALDO_CAPITAL = cmdTransaccionFactory.CreateParameter();
                        pSALDO_CAPITAL.ParameterName = param + "SALDO_CAPITAL";
                        pSALDO_CAPITAL.Value = pCreditoRecoger.saldo_capital;

                        DbParameter pINTERES_CORRIENTE = cmdTransaccionFactory.CreateParameter();
                        pINTERES_CORRIENTE.ParameterName = param + "INTERES_CORRIENTE";
                        pINTERES_CORRIENTE.Value = pCreditoRecoger.interes_corriente;

                        DbParameter pINTERES_MORA = cmdTransaccionFactory.CreateParameter();
                        pINTERES_MORA.ParameterName = param + "INTERES_MORA";
                        pINTERES_MORA.Value = pCreditoRecoger.interes_mora;

                        DbParameter pOTROS = cmdTransaccionFactory.CreateParameter();
                        pOTROS.ParameterName = param + "OTROS";
                        pOTROS.Value = pCreditoRecoger.otros;

                        DbParameter pRECOGER = cmdTransaccionFactory.CreateParameter();
                        pRECOGER.ParameterName = param + "RECOGER";
                        pRECOGER.Value = pCreditoRecoger.recoger;

                        cmdTransaccionFactory.Parameters.Add(pNUMERO_CREDITO);
                        cmdTransaccionFactory.Parameters.Add(pLINEA_CREDITO);
                        cmdTransaccionFactory.Parameters.Add(pMONTO);
                        cmdTransaccionFactory.Parameters.Add(pSALDO_CAPITAL);
                        cmdTransaccionFactory.Parameters.Add(pINTERES_CORRIENTE);
                        cmdTransaccionFactory.Parameters.Add(pINTERES_MORA);
                        cmdTransaccionFactory.Parameters.Add(pOTROS);
                        cmdTransaccionFactory.Parameters.Add(pRECOGER);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_FabricaCreditos_CreditoRecoger_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pCreditoRecoger, "CreditoRecoger", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pCreditoRecoger;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoRecogerData", "ModificarCreditoRecoger", ex);
                        return null;
                    }
                }
            }
        }

        public decimal ConsultarValorNoCapitalizado(string numeroRadicacion, Usuario usuario)
        {
            DbDataReader resultado;
            decimal valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(numeroRadicacion))
                        {
                            throw new ArgumentException("Has pasado un numero de radicación invalido!.");
                        }

                        string sql = "Select valor From descuentoscredito Where cod_atr = 44 and numero_radicacion = " + numeroRadicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if(resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToDecimal(resultado["VALOR"]);
                        }

                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoRecogerData", "ConsultarValorNoCapitalizado", ex);
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla CreditoRecoger de la base de datos
        /// </summary>
        /// <param name="pId">identificador de CreditoRecoger</param>
        public void EliminarCreditoRecoger(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_recoge = cmdTransaccionFactory.CreateParameter();
                        p_numero_recoge.ParameterName = "p_numero_recoge";
                        p_numero_recoge.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(p_numero_recoge);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_DESEM_DES_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCreditoRecoger, "CreditoRecoger", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoRecogerData", "EliminarCreditoRecoger", ex);
                    }
                }
            }
        }


        public List<CreditoRecoger> ConsultarCreditoRecoger(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<CreditoRecoger> lstCreditoRecoger = new List<CreditoRecoger>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT c.numero_radicacion, C.Cod_Linea_Credito || '-' || l.nombre AS linea, c.monto_aprobado, r.saldocapital, 0 as interes_corriente, 0 as interes_mora, 0 as otros, 1 as recoger, r.valorrecoge, r.valor_nominas FROM  LINEASCREDITO L, CREDITOSRECOGIDOS R, CREDITO C WHERE r.numero_recoge = c.numero_radicacion And c.cod_linea_credito = l.cod_linea_credito And R.numero_radicacion = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CreditoRecoger entidad = new CreditoRecoger();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["SALDOCAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDOCAPITAL"]);
                            if (resultado["INTERES_CORRIENTE"] == DBNull.Value) entidad.interes_corriente = Convert.ToInt64(resultado["INTERES_CORRIENTE"]);
                            if (resultado["INTERES_MORA"] != DBNull.Value) entidad.interes_mora = Convert.ToInt64(resultado["INTERES_MORA"]);
                            if (resultado["OTROS"] != DBNull.Value) entidad.otros = Convert.ToInt64(resultado["OTROS"]);
                            if (resultado["VALORRECOGE"] != DBNull.Value) entidad.valor_recoge = Convert.ToInt64(resultado["VALORRECOGE"]);
                            if (resultado["RECOGER"] != DBNull.Value) entidad.recoger = Convert.ToBoolean(resultado["RECOGER"]);
                            if (resultado["VALOR_NOMINAS"] != DBNull.Value) entidad.valor_nominas = Convert.ToDecimal(resultado["VALOR_NOMINAS"]);
                            lstCreditoRecoger.Add(entidad);
                        }
                        return lstCreditoRecoger;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoRecogerData", "ConsultarCreditoRecoger", ex);
                        return null;
                    }
                }
            }
        }


        public List<CreditoRecoger> Consultarterminosfijos(string radicacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<CreditoRecoger> lstCreditoRecoger = new List<CreditoRecoger>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select fecha_pago,valor,forma_pago,saldo_capital  from cuotasextras where numero_radicacion =" + radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CreditoRecoger entidad = new CreditoRecoger();
                            if (resultado["fecha_pago"] != DBNull.Value) entidad.fecha_pago = Convert.ToDateTime(resultado["fecha_pago"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor_total = Convert.ToInt64(resultado["valor"]);
                            if (resultado["forma_pago"] != DBNull.Value) entidad.formapago = Convert.ToString(resultado["forma_pago"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["saldo_capital"]);

                            lstCreditoRecoger.Add(entidad);
                        }
                        return lstCreditoRecoger;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoRecogerData", "ConsultarCreditoRecoger", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla CreditoRecoger dados unos filtros
        /// </summary>
        /// <param name="pCreditoRecoger">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CreditoRecoger obtenidos</returns>
        public List<CreditoRecoger> ListarCreditoRecoger(CreditoRecoger pCreditoRecoger, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CreditoRecoger> lstCreditoRecoger = new List<CreditoRecoger>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        
                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "pnumero_radicacion";
                        pnumero_radicacion.Value = pCreditoRecoger.numero_radicacion;

                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "pcod_linea_credito";
                        if (pCreditoRecoger.linea_credito == null)
                            pcod_linea_credito.Value = DBNull.Value;
                        else
                            pcod_linea_credito.Value = pCreditoRecoger.linea_credito;

                        DbParameter pcod_deudor = cmdTransaccionFactory.CreateParameter();
                        pcod_deudor.ParameterName = "pcod_deudor";
                        if (pCreditoRecoger.cod_deudor == null)
                            pcod_deudor.Value = DBNull.Value;
                        else
                            pcod_deudor.Value = pCreditoRecoger.cod_deudor;

                        DbParameter pfecha_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_pago.ParameterName = "pfecha_pago";
                        pfecha_pago.DbType = DbType.Date;
                        pfecha_pago.Value = DateTime.Now;

                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);
                        cmdTransaccionFactory.Parameters.Add(pcod_deudor);
                        cmdTransaccionFactory.Parameters.Add(pfecha_pago);

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RECOGER";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCreditoRecoger, "CreditoRecoger", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsultarCreditoRecoger", "ConsultarCreditoRecoger", ex);
                    }
                }

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"With novedad_producto As (Select d.numero_producto, nvl(count(1), 0) As Cantidad_Nominas, Sum(d.valor) As Valor_Nominas From empresa_novedad e, detempresa_novedad d
                                                                    where e.numero_novedad = d.numero_novedad And CodigoProducto(d.tipo_producto) = 2 And e.estado = 1 Group by d.numero_producto) " + 
                                     @"Select t.numero_radicacion, t.linea, t.monto, t.saldo_capital, t.interes_corriente, t.interes_mora, t.seguro, t.leymipyme, t.iva_leymipyme, t.otros, t.total_recoger, t.cuotas_pagadas, n.valor_nominas, " +
                                     @"Decode((Select sign(count(*)) from creditosrecogidos cr Where cr.numero_recoge = t.numero_radicacion And cr.numero_radicacion = " + pCreditoRecoger.numero_radicacion + "), 0, 'False', 1, 'True') As recoge " +
                                     @"From temp_recoger t Left Join novedad_producto n On t.numero_radicacion = n.numero_producto";

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        decimal total;
                        while (resultado.Read())
                        {
                            CreditoRecoger entidad = new CreditoRecoger();
                            total = 0;
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_credito = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["INTERES_CORRIENTE"] != DBNull.Value) entidad.interes_corriente = Convert.ToInt64(resultado["INTERES_CORRIENTE"]);
                            if (resultado["INTERES_MORA"] != DBNull.Value) entidad.interes_mora = Convert.ToInt64(resultado["INTERES_MORA"]);
                            if (resultado["OTROS"] != DBNull.Value) entidad.otros = Convert.ToInt64(resultado["OTROS"]);
                            if (resultado["RECOGE"] != DBNull.Value) entidad.recoger = Convert.ToBoolean(resultado["RECOGE"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt32(resultado["CUOTAS_PAGADAS"]);
                            total += entidad.saldo_capital;
                            total += entidad.interes_corriente;
                            total += entidad.interes_mora;
                            total += entidad.otros;
                            entidad.valor_total = total;
                            if (resultado["VALOR_NOMINAS"] != DBNull.Value)
                            {
                                entidad.valor_nominas = Convert.ToDecimal(resultado["VALOR_NOMINAS"]);
                                if (entidad.valor_nominas > 0)
                                    if (entidad.valor_nominas > entidad.valor_total)
                                        entidad.valor_nominas = entidad.valor_total;
                            }
                            lstCreditoRecoger.Add(entidad);
                        }

                        return lstCreditoRecoger;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoRecogerData", "ListarCreditoRecoger", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla CreditoRecoger dados unos filtros
        /// </summary>
        /// <param name="pCreditoRecoger">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CreditoRecoger obtenidos</returns>
        public List<CreditoRecoger> ListarCreditoRecogerSolicitud(CreditoRecoger pCreditoRecoger, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CreditoRecoger> lstCreditoRecoger = new List<CreditoRecoger>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "pnumero_radicacion";
                        pnumero_radicacion.Value = pCreditoRecoger.numero_radicacion;

                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "pcod_linea_credito";
                        if (pCreditoRecoger.linea_credito == null)
                            pcod_linea_credito.Value = DBNull.Value;
                        else
                            pcod_linea_credito.Value = pCreditoRecoger.linea_credito;

                        DbParameter pcod_deudor = cmdTransaccionFactory.CreateParameter();
                        pcod_deudor.ParameterName = "pcod_deudor";
                        if (pCreditoRecoger.cod_deudor == null)
                            pcod_deudor.Value = DBNull.Value;
                        else
                            pcod_deudor.Value = pCreditoRecoger.cod_deudor;

                        DbParameter pfecha_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_pago.ParameterName = "pfecha_pago";
                        pfecha_pago.DbType = DbType.Date;
                        pfecha_pago.Value = DateTime.Now;

                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);
                        cmdTransaccionFactory.Parameters.Add(pcod_deudor);
                        cmdTransaccionFactory.Parameters.Add(pfecha_pago);

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RECOGER";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCreditoRecoger, "CreditoRecoger", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsultarCreditoRecoger", "ConsultarCreditoRecoger", ex);
                    }
                }

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"With novedad_producto As (Select d.numero_producto, nvl(count(1), 0) As Cantidad_Nominas, Sum(d.valor) As Valor_Nominas From empresa_novedad e, detempresa_novedad d  
                                                                    where e.numero_novedad = d.numero_novedad And CodigoProducto(d.tipo_producto) = 2 And e.estado = 1 Group by d.numero_producto) 
                                        Select t.numero_radicacion, t.linea, t.monto, t.saldo_capital, t.interes_corriente, t.interes_mora, t.seguro, t.leymipyme, t.iva_leymipyme, t.otros, t.total_recoger, t.cuotas_pagadas, t.numerocre_linea As recoge, n.Cantidad_Nominas, n.Valor_Nominas
                                        From temp_recoger t Left Join novedad_producto n On t.numero_radicacion = n.numero_producto";

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CreditoRecoger entidad = new CreditoRecoger();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_credito = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["MONTO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["INTERES_CORRIENTE"] != DBNull.Value) entidad.interes_corriente = Convert.ToDecimal(resultado["INTERES_CORRIENTE"]);
                            if (resultado["INTERES_MORA"] != DBNull.Value) entidad.interes_mora = Convert.ToDecimal(resultado["INTERES_MORA"]);
                            if (resultado["SEGURO"] != DBNull.Value) entidad.seguro = Convert.ToDecimal(resultado["SEGURO"]);
                            if (resultado["LEYMIPYME"] != DBNull.Value) entidad.leymipyme = Convert.ToDecimal(resultado["LEYMIPYME"]);
                            if (resultado["IVA_LEYMIPYME"] != DBNull.Value) entidad.iva_leymipyme = Convert.ToDecimal(resultado["IVA_LEYMIPYME"]);
                            if (resultado["OTROS"] != DBNull.Value) entidad.otros = Convert.ToDecimal(resultado["OTROS"]);
                            if (resultado["TOTAL_RECOGER"] != DBNull.Value) entidad.valor_recoge = Convert.ToDecimal(resultado["TOTAL_RECOGER"]);  
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt32(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["RECOGE"] != DBNull.Value) entidad.recoger = Convert.ToBoolean(resultado["RECOGE"]);
                            if (resultado["CANTIDAD_NOMINAS"] != DBNull.Value) entidad.cantidad_nominas = Convert.ToInt32(resultado["CANTIDAD_NOMINAS"]);
                            if (resultado["VALOR_NOMINAS"] != DBNull.Value)
                            { 
                                entidad.valor_nominas = Convert.ToInt64(resultado["VALOR_NOMINAS"]);
                                if (entidad.valor_nominas > 0)
                                    if (entidad.valor_nominas > entidad.valor_recoge + entidad.saldo_capital)
                                        entidad.valor_nominas = entidad.valor_recoge + entidad.saldo_capital;
                            }
                            lstCreditoRecoger.Add(entidad);
                        }

                        return lstCreditoRecoger;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoRecogerData", "ListarCreditoRecoger", ex);
                        return null;
                    }
                }
            }
        }

        public List<CreditoRecoger> ListarCreditoARecoger(string pCodPersona, DateTime pfecha_calculo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CreditoRecoger> lstCreditoRecoger = new List<CreditoRecoger>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "pcod_persona";
                        pcod_persona.Value = pCodPersona;
                        pcod_persona.DbType = DbType.Int64;

                        DbParameter pfecha_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_pago.ParameterName = "pfecha_pago";
                        pfecha_pago.DbType = DbType.Date;
                        if (pfecha_calculo != null)
                            pfecha_pago.Value = pfecha_calculo;
                        else
                            pfecha_pago.Value = DateTime.Now;

                        cmdTransaccionFactory.Parameters.Add(pcod_persona);
                        cmdTransaccionFactory.Parameters.Add(pfecha_pago);

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RECOGERPER";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoRecogerData", "ListarCreditoARecoger", ex);
                    }
                }

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select t.numero_radicacion, t.linea, t.monto, t.saldo_capital, t.interes_corriente, t.interes_mora, t.seguro, t.leymipyme, t.iva_leymipyme, t.otros, t.total_recoger, 0 As recoge " +
                                     @"From temp_recoger t";

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CreditoRecoger entidad = new CreditoRecoger();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_credito = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["INTERES_CORRIENTE"] != DBNull.Value) entidad.interes_corriente = Convert.ToInt64(resultado["INTERES_CORRIENTE"]);
                            if (resultado["INTERES_MORA"] != DBNull.Value) entidad.interes_mora = Convert.ToInt64(resultado["INTERES_MORA"]);
                            if (resultado["OTROS"] != DBNull.Value) entidad.otros = Convert.ToInt64(resultado["OTROS"]);
                            if (resultado["TOTAL_RECOGER"] != DBNull.Value) entidad.valor_total = Convert.ToInt64(resultado["TOTAL_RECOGER"]);
                            if (resultado["RECOGE"] != DBNull.Value) entidad.recoger = Convert.ToBoolean(resultado["RECOGE"]);

                            lstCreditoRecoger.Add(entidad);
                        }

                        return lstCreditoRecoger;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoRecogerData", "ListarCreditoARecoger", ex);
                        return null;
                    }
                }
            }
        }

    }
}