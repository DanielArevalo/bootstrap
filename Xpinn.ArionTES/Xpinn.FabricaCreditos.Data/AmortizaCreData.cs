using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    public class AmortizaCreData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public AmortizaCreData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public AmortizaCre CrearAmortizaCre(AmortizaCre pAmortizaCre, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidamortiza = cmdTransaccionFactory.CreateParameter();
                        pidamortiza.ParameterName = "p_idamortiza";
                        pidamortiza.Value = pAmortizaCre.idamortiza;
                        pidamortiza.Direction = ParameterDirection.Input;
                        pidamortiza.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidamortiza);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pAmortizaCre.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pAmortizaCre.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pfecha_cuota = cmdTransaccionFactory.CreateParameter();
                        pfecha_cuota.ParameterName = "p_fecha_cuota";
                        pfecha_cuota.Value = pAmortizaCre.fecha_cuota;
                        pfecha_cuota.Direction = ParameterDirection.Input;
                        pfecha_cuota.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_cuota);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pAmortizaCre.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        psaldo.Value = pAmortizaCre.saldo;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        DbParameter psaldo_base = cmdTransaccionFactory.CreateParameter();
                        psaldo_base.ParameterName = "p_saldo_base";
                        psaldo_base.Value = pAmortizaCre.saldo_base;
                        psaldo_base.Direction = ParameterDirection.Input;
                        psaldo_base.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_base);

                        DbParameter ptasa_base = cmdTransaccionFactory.CreateParameter();
                        ptasa_base.ParameterName = "p_tasa_base";
                        ptasa_base.Value = pAmortizaCre.tasa_base;
                        ptasa_base.Direction = ParameterDirection.Input;
                        ptasa_base.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_base);

                        DbParameter pdias_base = cmdTransaccionFactory.CreateParameter();
                        pdias_base.ParameterName = "p_dias_base";
                        pdias_base.Value = pAmortizaCre.dias_base;
                        pdias_base.Direction = ParameterDirection.Input;
                        pdias_base.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_base);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pAmortizaCre.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pAmortizaCre.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_AMORTIZA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAmortizaCre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AmortizaCreData", "CrearAmortizaCre", ex);
                        return null;
                    }
                }
            }
        }


        public AmortizaCre ModificarAmortizaCre(AmortizaCre pAmortizaCre, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidamortiza = cmdTransaccionFactory.CreateParameter();
                        pidamortiza.ParameterName = "p_idamortiza";
                        pidamortiza.Value = pAmortizaCre.idamortiza;
                        pidamortiza.Direction = ParameterDirection.Input;
                        pidamortiza.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidamortiza);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pAmortizaCre.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pAmortizaCre.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pfecha_cuota = cmdTransaccionFactory.CreateParameter();
                        pfecha_cuota.ParameterName = "p_fecha_cuota";
                        pfecha_cuota.Value = pAmortizaCre.fecha_cuota;
                        pfecha_cuota.Direction = ParameterDirection.Input;
                        pfecha_cuota.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_cuota);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pAmortizaCre.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        psaldo.Value = pAmortizaCre.saldo;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        DbParameter psaldo_base = cmdTransaccionFactory.CreateParameter();
                        psaldo_base.ParameterName = "p_saldo_base";
                        psaldo_base.Value = pAmortizaCre.saldo_base;
                        psaldo_base.Direction = ParameterDirection.Input;
                        psaldo_base.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_base);

                        DbParameter ptasa_base = cmdTransaccionFactory.CreateParameter();
                        ptasa_base.ParameterName = "p_tasa_base";
                        ptasa_base.Value = pAmortizaCre.tasa_base;
                        ptasa_base.Direction = ParameterDirection.Input;
                        ptasa_base.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_base);

                        DbParameter pdias_base = cmdTransaccionFactory.CreateParameter();
                        pdias_base.ParameterName = "p_dias_base";
                        pdias_base.Value = pAmortizaCre.dias_base;
                        pdias_base.Direction = ParameterDirection.Input;
                        pdias_base.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_base);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pAmortizaCre.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pAmortizaCre.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_AMORTIZA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAmortizaCre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AmortizaCreData", "ModificarAmortizaCre", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarAmortizaCre(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        AmortizaCre pAmortizaCre = new AmortizaCre();
                        pAmortizaCre = ConsultarAmortizaCre(pId, vUsuario);

                        DbParameter pidamortiza = cmdTransaccionFactory.CreateParameter();
                        pidamortiza.ParameterName = "p_idamortiza";
                        pidamortiza.Value = pAmortizaCre.idamortiza;
                        pidamortiza.Direction = ParameterDirection.Input;
                        pidamortiza.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidamortiza);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_AMORTIZA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AmortizaCreData", "EliminarAmortizaCre", ex);
                    }
                }
            }
        }


        public AmortizaCre ConsultarAmortizaCre(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            AmortizaCre entidad = new AmortizaCre();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM AMORTIZA_CRE WHERE IDAMORTIZA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDAMORTIZA"] != DBNull.Value) entidad.idamortiza = Convert.ToInt32(resultado["IDAMORTIZA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["FECHA_CUOTA"] != DBNull.Value) entidad.fecha_cuota = Convert.ToDateTime(resultado["FECHA_CUOTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["SALDO_BASE"] != DBNull.Value) entidad.saldo_base = Convert.ToDecimal(resultado["SALDO_BASE"]);
                            if (resultado["TASA_BASE"] != DBNull.Value) entidad.tasa_base = Convert.ToDecimal(resultado["TASA_BASE"]);
                            if (resultado["DIAS_BASE"] != DBNull.Value) entidad.dias_base = Convert.ToInt32(resultado["DIAS_BASE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
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
                        BOExcepcion.Throw("AmortizaCreData", "ConsultarAmortizaCre", ex);
                        return null;
                    }
                }
            }
        }


        public List<AmortizaCre> ListarAmortizaCre(AmortizaCre pAmortizaCre, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<AmortizaCre> lstAmortizaCre = new List<AmortizaCre>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM AMORTIZA_CRE " + ObtenerFiltro(pAmortizaCre) + " ORDER BY FECHA_CUOTA, COD_ATR DESC ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AmortizaCre entidad = new AmortizaCre();
                            if (resultado["IDAMORTIZA"] != DBNull.Value) entidad.idamortiza = Convert.ToInt32(resultado["IDAMORTIZA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["FECHA_CUOTA"] != DBNull.Value) entidad.fecha_cuota = Convert.ToDateTime(resultado["FECHA_CUOTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["SALDO_BASE"] != DBNull.Value) entidad.saldo_base = Convert.ToDecimal(resultado["SALDO_BASE"]);
                            if (resultado["TASA_BASE"] != DBNull.Value) entidad.tasa_base = Convert.ToDecimal(resultado["TASA_BASE"]);
                            if (resultado["DIAS_BASE"] != DBNull.Value) entidad.dias_base = Convert.ToInt32(resultado["DIAS_BASE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            lstAmortizaCre.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAmortizaCre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AmortizaCreData", "ListarAmortizaCre", ex);
                        return null;
                    }
                }
            }
        }


    }
}