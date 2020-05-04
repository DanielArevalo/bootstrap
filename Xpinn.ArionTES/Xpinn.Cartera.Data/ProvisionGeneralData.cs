using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;
 
namespace Xpinn.Cartera.Data
{
    public class ProvisionGeneralData : GlobalData
    {
 
        protected ConnectionDataBase dbConnectionFactory;

        public ProvisionGeneralData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public ProvisionGeneral CrearProvisionGeneral(ProvisionGeneral pProvisionGeneral, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidprovision = cmdTransaccionFactory.CreateParameter();
                        pidprovision.ParameterName = "p_idprovision";
                        pidprovision.Value = pProvisionGeneral.idprovision;
                        pidprovision.Direction = ParameterDirection.Input;
                        pidprovision.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidprovision);
 
                        DbParameter pfecha_corte = cmdTransaccionFactory.CreateParameter();
                        pfecha_corte.ParameterName = "p_fecha_corte";
                        if (pProvisionGeneral.fecha_corte == null)
                            pfecha_corte.Value = DBNull.Value;
                        else
                            pfecha_corte.Value = pProvisionGeneral.fecha_corte;
                        pfecha_corte.Direction = ParameterDirection.Input;
                        pfecha_corte.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_corte);
 
                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pProvisionGeneral.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);
 
                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pProvisionGeneral.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);
 
                        DbParameter pvalor_total = cmdTransaccionFactory.CreateParameter();
                        pvalor_total.ParameterName = "p_valor_total";
                        if (pProvisionGeneral.valor_total == null)
                            pvalor_total.Value = DBNull.Value;
                        else
                            pvalor_total.Value = pProvisionGeneral.valor_total;
                        pvalor_total.Direction = ParameterDirection.Input;
                        pvalor_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_total);
 
                        DbParameter pprovision_acumulada = cmdTransaccionFactory.CreateParameter();
                        pprovision_acumulada.ParameterName = "p_provision_acumulada";
                        if (pProvisionGeneral.provision_acumulada == null)
                            pprovision_acumulada.Value = DBNull.Value;
                        else
                            pprovision_acumulada.Value = pProvisionGeneral.provision_acumulada;
                        pprovision_acumulada.Direction = ParameterDirection.Input;
                        pprovision_acumulada.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pprovision_acumulada);
 
                        DbParameter pvalor_provision = cmdTransaccionFactory.CreateParameter();
                        pvalor_provision.ParameterName = "p_valor_provision";
                        if (pProvisionGeneral.valor_provision == null)
                            pvalor_provision.Value = DBNull.Value;
                        else
                            pvalor_provision.Value = pProvisionGeneral.valor_provision;
                        pvalor_provision.Direction = ParameterDirection.Input;
                        pvalor_provision.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_provision);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_PROVGEN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pProvisionGeneral;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProvisionGeneralData", "CrearProvisionGeneral", ex);
                        return null;
                    }
                }
            }
        }
 
 
        public ProvisionGeneral ModificarProvisionGeneral(ProvisionGeneral pProvisionGeneral, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidprovision = cmdTransaccionFactory.CreateParameter();
                        pidprovision.ParameterName = "p_idprovision";
                        pidprovision.Value = pProvisionGeneral.idprovision;
                        pidprovision.Direction = ParameterDirection.Input;
                        pidprovision.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidprovision);
 
                        DbParameter pfecha_corte = cmdTransaccionFactory.CreateParameter();
                        pfecha_corte.ParameterName = "p_fecha_corte";
                        if (pProvisionGeneral.fecha_corte == null)
                            pfecha_corte.Value = DBNull.Value;
                        else
                            pfecha_corte.Value = pProvisionGeneral.fecha_corte;
                        pfecha_corte.Direction = ParameterDirection.Input;
                        pfecha_corte.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_corte);
 
                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pProvisionGeneral.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);
 
                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pProvisionGeneral.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);
 
                        DbParameter pvalor_total = cmdTransaccionFactory.CreateParameter();
                        pvalor_total.ParameterName = "p_valor_total";
                        if (pProvisionGeneral.valor_total == null)
                            pvalor_total.Value = DBNull.Value;
                        else
                            pvalor_total.Value = pProvisionGeneral.valor_total;
                        pvalor_total.Direction = ParameterDirection.Input;
                        pvalor_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_total);
 
                        DbParameter pprovision_acumulada = cmdTransaccionFactory.CreateParameter();
                        pprovision_acumulada.ParameterName = "p_provision_acumulada";
                        if (pProvisionGeneral.provision_acumulada == null)
                            pprovision_acumulada.Value = DBNull.Value;
                        else
                            pprovision_acumulada.Value = pProvisionGeneral.provision_acumulada;
                        pprovision_acumulada.Direction = ParameterDirection.Input;
                        pprovision_acumulada.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pprovision_acumulada);
 
                        DbParameter pvalor_provision = cmdTransaccionFactory.CreateParameter();
                        pvalor_provision.ParameterName = "p_valor_provision";
                        if (pProvisionGeneral.valor_provision == null)
                            pvalor_provision.Value = DBNull.Value;
                        else
                            pvalor_provision.Value = pProvisionGeneral.valor_provision;
                        pvalor_provision.Direction = ParameterDirection.Input;
                        pvalor_provision.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_provision);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_PROVGEN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pProvisionGeneral;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProvisionGeneralData", "ModificarProvisionGeneral", ex);
                        return null;
                    }
                }
            }
        }
 
 
        public void EliminarProvisionGeneral(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ProvisionGeneral pProvisionGeneral = new ProvisionGeneral();
                        pProvisionGeneral = ConsultarProvisionGeneral(pId, vUsuario);
                        
                        DbParameter pidprovision = cmdTransaccionFactory.CreateParameter();
                        pidprovision.ParameterName = "p_idprovision";
                        pidprovision.Value = pProvisionGeneral.idprovision;
                        pidprovision.Direction = ParameterDirection.Input;
                        pidprovision.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidprovision);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_PROVGEN_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProvisionGeneralData", "EliminarProvisionGeneral", ex);
                    }
                }
            }
        }
 
 
        public ProvisionGeneral ConsultarProvisionGeneral(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ProvisionGeneral entidad = new ProvisionGeneral();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PROVISION_GENERAL WHERE IDPROVISION = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPROVISION"] != DBNull.Value) entidad.idprovision = Convert.ToInt32(resultado["IDPROVISION"]);
                            if (resultado["FECHA_CORTE"] != DBNull.Value) entidad.fecha_corte = Convert.ToDateTime(resultado["FECHA_CORTE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["VALOR_TOTAL"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["VALOR_TOTAL"]);
                            if (resultado["PROVISION_ACUMULADA"] != DBNull.Value) entidad.provision_acumulada = Convert.ToDecimal(resultado["PROVISION_ACUMULADA"]);
                            if (resultado["VALOR_PROVISION"] != DBNull.Value) entidad.valor_provision = Convert.ToDecimal(resultado["VALOR_PROVISION"]);
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
                        BOExcepcion.Throw("ProvisionGeneralData", "ConsultarProvisionGeneral", ex);
                        return null;
                    }
                }
            }
        }
 
 
        public List<ProvisionGeneral> ListarProvisionGeneral(ProvisionGeneral pProvisionGeneral, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ProvisionGeneral> lstProvisionGeneral = new List<ProvisionGeneral>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PROVISION_GENERAL " + ObtenerFiltro(pProvisionGeneral) + " ORDER BY IDPROVISION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ProvisionGeneral entidad = new ProvisionGeneral();
                            if (resultado["IDPROVISION"] != DBNull.Value) entidad.idprovision = Convert.ToInt32(resultado["IDPROVISION"]);
                            if (resultado["FECHA_CORTE"] != DBNull.Value) entidad.fecha_corte = Convert.ToDateTime(resultado["FECHA_CORTE"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["VALOR_TOTAL"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["VALOR_TOTAL"]);
                            if (resultado["PROVISION_ACUMULADA"] != DBNull.Value) entidad.provision_acumulada = Convert.ToDecimal(resultado["PROVISION_ACUMULADA"]);
                            if (resultado["VALOR_PROVISION"] != DBNull.Value) entidad.valor_provision = Convert.ToDecimal(resultado["VALOR_PROVISION"]);
                            lstProvisionGeneral.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProvisionGeneral;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProvisionGeneralData", "ListarProvisionGeneral", ex);
                        return null;
                    }
                }
            }
        }

        public List<ProvisionGeneral> ProvisionGeneral(DateTime pFechaCorte, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProvisionGeneral> lstDetalle = new List<ProvisionGeneral>();
            DateTime fechaAnterior = new DateTime(pFechaCorte.Year, pFechaCorte.Month, 1).AddDays(-1);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select cod_oficina, nombre From oficina Order by 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProvisionGeneral entidad = new ProvisionGeneral();
                            decimal provisionAcumuladaSin = 0;
                            decimal provisionAcumuladaCon = 0;

                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_oficina = Convert.ToString(resultado["NOMBRE"]);

                            entidad.fecha_corte = pFechaCorte;
                            entidad.valor_sinlibranza = ValorCartera(pFechaCorte, 1, Convert.ToInt64(entidad.cod_oficina), pUsuario);
                            decimal porcentaje_provision = PorcentajeProvision(1, pUsuario);
                            entidad.total_provision_sinlibranza = (entidad.valor_sinlibranza * porcentaje_provision) / 100;
                            entidad.total_provision_sinlibranza = Math.Round(entidad.total_provision_sinlibranza.Value);
                            provisionAcumuladaSin = ConsultarProvisionGeneral(fechaAnterior, Convert.ToInt64(entidad.cod_oficina), 1, pUsuario);
                            entidad.provision_sinlibranza = entidad.total_provision_sinlibranza - provisionAcumuladaSin;

                            decimal porcentaje_provisioncon = PorcentajeProvision(2, pUsuario);
                            entidad.valor_conlibranza = ValorCartera(pFechaCorte, 2, Convert.ToInt64(entidad.cod_oficina), pUsuario);
                            entidad.total_provision_conlibranza = (entidad.valor_conlibranza * porcentaje_provisioncon) / 100;
                            entidad.total_provision_conlibranza = Math.Round(entidad.total_provision_conlibranza.Value);
                            provisionAcumuladaCon = ConsultarProvisionGeneral(fechaAnterior, Convert.ToInt64(entidad.cod_oficina), 2, pUsuario);
                            entidad.provision_conlibranza = entidad.total_provision_conlibranza - provisionAcumuladaCon;

                            lstDetalle.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProvisionGeneralData", "ProvisionGeneral", ex);
                        return null;
                    }
                }
            }
        }

        public decimal ValorCartera(DateTime pFecha, int pFormaPago, Int64 pCodOficina, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select sum(c.saldo_capital) As valor
                                        From historico_cre c Inner Join credito d On c.numero_radicacion = d.numero_radicacion
                                        Inner Join lineascredito p On c.cod_linea_credito = p.cod_linea_credito And (p.aplica_asociado = 1 Or p.cod_linea_credito In (Select x.cod_linea_credito From parametros_linea x Where x.cod_parametro = 604 And Trim(x.valor) = '1' ))
                                        Inner Join persona x On c.cod_cliente = x.cod_persona ";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql += " Where c.fecha_historico = To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') And c.cod_oficina = " + pCodOficina;
                        else
                            sql += " Where c.fecha_historico = '" + pFecha + @"' And c.cod_oficina = " + pCodOficina;
                        if (pFormaPago == 2)
                            sql += " And (c.forma_pago In ('2', 'N') Or c.cod_linea_credito In (Select xx.cod_linea_credito From parametros_linea xx Where xx.cod_linea_credito = d.cod_linea_credito and xx.cod_parametro = 121))";
                        else
                            sql += " And (c.forma_pago In ('1', 'C') And c.cod_linea_credito Not In (Select xx.cod_linea_credito From parametros_linea xx Where xx.cod_linea_credito = d.cod_linea_credito and xx.cod_parametro = 121))";
                        sql += " And c.cod_linea_credito Not In (Select cod_linea_credito From parametros_linea Where cod_parametro = 320)";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToDecimal(resultado["VALOR"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch
                    {
                        return valor;
                    }
                }
            }
        }


        public decimal ProvisionAcumulada(DateTime pFecha, int pFormaPago, Int64 pCodOficina, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select sum(c.saldo_capital) As valor
                                        From historico_cre c Inner Join credito d On c.numero_radicacion = d.numero_radicacion
                                        Inner Join lineascredito p On c.cod_linea_credito = p.cod_linea_credito
                                        Inner Join persona x On c.cod_cliente = x.cod_persona ";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql += " Where c.fecha_historico = To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') And c.cod_oficina = " + pCodOficina;
                        else
                            sql += " Where c.fecha_historico = '" + pFecha + @"' And c.cod_oficina = " + pCodOficina;
                        if (pFormaPago == 2)
                            sql += " And (c.forma_pago = 2 Or c.cod_linea_credito In (Select xx.cod_linea_credito From parametros_linea xx Where xx.cod_linea_credito = d.cod_linea_credito and xx.cod_parametro = 121))";
                        else
                            sql += " And (c.forma_pago = 1 And c.cod_linea_credito Not In (Select xx.cod_linea_credito From parametros_linea xx Where xx.cod_linea_credito = d.cod_linea_credito and xx.cod_parametro = 121))";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToDecimal(resultado["VALOR"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch
                    {
                        return valor;
                    }
                }
            }
        }


        public decimal ConsultarProvisionGeneral(DateTime pFecha, Int64 pCodOficina, int pFormaPago, Usuario vUsuario)
        {
            DbDataReader resultado;
            decimal ProvisionAcumulada = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = @"SELECT * FROM PROVISION_GENERAL WHERE FECHA_CORTE = TO_DATE('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " + " AND FORMA_PAGO = " + pFormaPago + " AND COD_OFICINA = " + pCodOficina.ToString();
                        else
                            sql = @"SELECT * FROM PROVISION_GENERAL WHERE FECHA_CORTE = '" + pFecha.ToString() + "' AND FORMA_PAGO = " + pFormaPago + " AND COD_OFICINA = " + pCodOficina.ToString(); 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["PROVISION_ACUMULADA"] != DBNull.Value) ProvisionAcumulada = Convert.ToDecimal(resultado["PROVISION_ACUMULADA"]);                            
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return ProvisionAcumulada;
                    }
                    catch 
                    {
                        return ProvisionAcumulada;
                    }
                }
            }
        }


        public decimal PorcentajeProvision(int pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal porcentajeProvision = 0;
            string eValor = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (pTipo == 1)
                            sql = "SELECT VALOR FROM GENERAL WHERE CODIGO = 13";
                        else
                            sql = "SELECT VALOR FROM GENERAL WHERE CODIGO = 14";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) eValor = Convert.ToString(resultado["VALOR"]);
                            try
                            {
                                porcentajeProvision = Convert.ToDecimal(eValor);
                            }
                            catch
                            {
                                porcentajeProvision = 0;
                            }
                        }

                        return porcentajeProvision;
                    }
                    catch
                    {
                        return porcentajeProvision;
                    }
                }
            }
        }
 
    }
}

