using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla SaldoCaja
    /// </summary>
    public class SaldoCajaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla SaldoCaja
        /// </summary>
        public SaldoCajaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla SaldoCaja de la base de datos
        /// </summary>
        /// <param name="pSaldoCaja">Entidad SaldoCaja</param>
        /// <returns>Entidad SaldoCaja creada</returns>
        public SaldoCaja CrearSaldoCaja(SaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Value = pSaldoCaja.fecha;
                        pfecha.DbType = DbType.DateTime;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.Size = 7;

                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = pSaldoCaja.cod_caja;
                        pcod_caja.Size = 8;
                        pcod_caja.DbType = DbType.Int16;
                        pcod_caja.Direction = ParameterDirection.Input;

                        DbParameter pcodx_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodx_cajero.ParameterName = "pcodigocajero";
                        pcodx_cajero.Value = pSaldoCaja.cod_cajero;
                        pcodx_cajero.Size = 8;
                        pcodx_cajero.DbType = DbType.Int16;
                        pcodx_cajero.Direction = ParameterDirection.Input;

                        DbParameter pcodi_moneda = cmdTransaccionFactory.CreateParameter();
                        pcodi_moneda.ParameterName = "pcodigomoneda";
                        pcodi_moneda.Value = 1;
                        pcodi_moneda.DbType = DbType.Int16;
                        pcodi_moneda.Direction = ParameterDirection.Input;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pSaldoCaja.valor;
                        pvalor.DbType = DbType.Decimal;
                        pvalor.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        cmdTransaccionFactory.Parameters.Add(pcod_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodx_cajero);
                        cmdTransaccionFactory.Parameters.Add(pcodi_moneda);
                        cmdTransaccionFactory.Parameters.Add(pvalor);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_SALDO_CAJA_RECEP";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pSaldoCaja, "SALDOCAJA", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Registrar Saldo Caja");
                            //DAauditoria.InsertarLog(pSaldoCaja, pUsuario, pSaldoCaja.cod_caja, "SALDOCAJA", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA
                        return pSaldoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldoCajaData", "CrearSaldoCaja", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla SaldoCaja de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad SaldoCaja modificada</returns>
        public SaldoCaja ModificarSaldoCaja(SaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_CAJA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CAJA.ParameterName = param + "COD_CAJA";
                        pCOD_CAJA.Value = pSaldoCaja.cod_caja;

                        DbParameter pCOD_CAJERO = cmdTransaccionFactory.CreateParameter();
                        pCOD_CAJERO.ParameterName = param + "COD_CAJERO";
                        pCOD_CAJERO.Value = pSaldoCaja.cod_cajero;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = param + "FECHA";
                        pFECHA.Value = pSaldoCaja.fecha;

                        DbParameter pTIPO_MONEDA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_MONEDA.ParameterName = param + "TIPO_MONEDA";
                        pTIPO_MONEDA.Value = pSaldoCaja.tipo_moneda;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = param + "VALOR";
                        pVALOR.Value = pSaldoCaja.valor;

                        cmdTransaccionFactory.Parameters.Add(pCOD_CAJA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CAJERO);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_MONEDA);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_CajaFin_SaldoCaja_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)                            
                            DAauditoria.InsertarLog(pSaldoCaja, pUsuario, pSaldoCaja.cod_caja, "SALDOCAJA", Accion.Modificar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        return pSaldoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldoCajaData", "ModificarSaldoCaja", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla SaldoCaja de la base de datos
        /// </summary>
        /// <param name="pId">identificador de SaldoCaja</param>
        public void EliminarSaldoCaja(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        SaldoCaja pSaldoCaja = new SaldoCaja();

                        //if (pUsuario.programaGeneraLog)
                        //    pSaldoCaja = ConsultarSaldoCaja(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_CAJA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CAJA.ParameterName = param + "COD_CAJA";
                        pCOD_CAJA.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_CAJA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_CajaFin_SaldoCaja_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pSaldoCaja, pUsuario, pSaldoCaja.cod_caja, "SALDOCAJA", Accion.Eliminar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldoCajaData", "InsertarSaldoCaja", ex);
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro en la tabla SaldoCaja de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla SaldoCaja</param>
        /// <returns>Entidad SaldoCaja consultado</returns>
        public SaldoCaja ConsultarSaldoCaja(SaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            DbDataReader resultado;
            SaldoCaja entidad = new SaldoCaja();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"SELECT COD_CAJA, COD_CAJERO, FECHA, TIPO_MONEDA, 
                                        (VALOR - nvl((select t.valor_minimo from topescaja t where t.cod_caja=s.cod_caja),0)) valor_total 
                                        FROM  SALDOCAJA s 
                                        WHERE COD_CAJA = " + pSaldoCaja.cod_caja +
                                        @" And TIPO_MONEDA = " + pSaldoCaja.tipo_moneda +
                                        @" And TRUNC(FECHA) = to_date('" + pSaldoCaja.fecha.ToShortDateString() + "', 'dd/MM/yyyy') ";

                        if (pSaldoCaja.cod_cajero.ToString() != "")
                            sql = sql + " And COD_CAJERO = " + pSaldoCaja.cod_cajero;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CAJA"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                            if (resultado["COD_CAJERO"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["COD_CAJERO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO_MONEDA"] != DBNull.Value) entidad.tipo_moneda = Convert.ToInt64(resultado["TIPO_MONEDA"]);
                            if (resultado["valor_total"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["valor_total"]);
                        }
                        else
                        {
                            entidad.valor = 0;
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        entidad.valor = 0;
                        return entidad;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro en la tabla SaldoCaja de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla SaldoCaja</param>
        /// <returns>Entidad SaldoCaja consultado</returns>
        public SaldoCaja ConsultarSaldoTesoreriaConsig(SaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            DbDataReader resultado;
            SaldoCaja entidad = new SaldoCaja();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT (a.VALOR - nvl((select t.valor_minimo from topescaja t where t.cod_caja = a.cod_caja),0)) saldo FROM  SALDOCAJA a WHERE  a.TIPO_MONEDA = " + pSaldoCaja.tipo_moneda + " AND to_char(a.FECHA,'dd/MM/yyyy') = '" + pSaldoCaja.fecha.ToShortDateString() + "' ";
                        if (pSaldoCaja.cod_cajero.ToString() != "")
                            sql = sql + " AND a.COD_CAJERO = " + pSaldoCaja.cod_cajero;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["saldo"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["saldo"]);
                        }
                        else
                        {
                            entidad.valor = 0;
                        }
                        return entidad;
                    }
                    catch (Exception)
                    {
                        entidad.valor = 0;
                        return entidad;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla SaldoCaja dados unos filtros
        /// </summary>
        /// <param name="pSaldoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SaldoCaja obtenidos</returns>
        public List<SaldoCaja> ListarSaldoCaja(SaldoCaja pSaldoCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SaldoCaja> lstSaldoCaja = new List<SaldoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  SALDOCAJA WHERE COD_CAJA=" + pSaldoCaja.cod_caja + " and COD_CAJERO=" + pSaldoCaja.cod_cajero + " and TIPO_MONEDA=" + pSaldoCaja.tipo_moneda + " and FECHA=" + pSaldoCaja.fecha;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            SaldoCaja entidad = new SaldoCaja();

                            if (resultado["COD_CAJA"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                            if (resultado["COD_CAJERO"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["COD_CAJERO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO_MONEDA"] != DBNull.Value) entidad.tipo_moneda = Convert.ToInt64(resultado["TIPO_MONEDA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);

                            lstSaldoCaja.Add(entidad);
                        }

                        return lstSaldoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldoCajaData", "ListarSaldoCaja", ex);
                        return null;
                    }
                }
            }
        }

    }
}