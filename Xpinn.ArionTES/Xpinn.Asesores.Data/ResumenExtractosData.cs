using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class ResumenExtractosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public ResumenExtractosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de detalles para el extracto
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Lineas de credito obtenidos</returns>
        public List<ResumenExtractos> ListarResumenExtractos(ResumenExtractos entidad, Int64 vNumRadic, out DataTable dDataTable, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ResumenExtractos> lstResumenExtractos = new List<ResumenExtractos>();
            dDataTable = new DataTable();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;                    

                        DbParameter pNUM_RADIC = cmdTransaccionFactory.CreateParameter();
                        pNUM_RADIC.ParameterName = "p_numero_radic";
                        pNUM_RADIC.Value = vNumRadic;
                        cmdTransaccionFactory.Parameters.Add(pNUM_RADIC);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RESUMENEXT";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "SELECT * FROM TEMP_EXTRACTO";
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        
                        dDataTable.Load(cmdTransaccionFactory.ExecuteReader());

                        while (resultado.Read())
                        {
                            entidad = new ResumenExtractos();
                            //Asociar todos los valores a la entidad
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.CodPersona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.CodigoLineaDeCredito = Convert.ToInt32(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["SALDO_INICIAL"] != DBNull.Value) entidad.SaldoInicial = Convert.ToDouble(resultado["SALDO_INICIAL"]);
                            if (resultado["DEBITOS"] != DBNull.Value) entidad.Debitos = Convert.ToDouble(resultado["DEBITOS"]);
                            if (resultado["CREDITOS"] != DBNull.Value) entidad.Creditos = Convert.ToDouble(resultado["CREDITOS"]);
                            if (resultado["SALDO_FINAL"] != DBNull.Value) entidad.SaldoFinal = Convert.ToDouble(resultado["SALDO_FINAL"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.Email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["PAGOS"] != DBNull.Value) entidad.Pagos = Convert.ToDouble(resultado["PAGOS"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.Ciudad = Convert.ToString(resultado["CIUDAD"]);
                            if (resultado["BARRIO"] != DBNull.Value) entidad.Barrio = Convert.ToString(resultado["BARRIO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["FECHA_CORTE"] != DBNull.Value) entidad.FechaCorte = Convert.ToDateTime(resultado["FECHA_CORTE"]);
                            if (resultado["FEC_INI"] != DBNull.Value) entidad.FechaInicial = Convert.ToDateTime(resultado["FEC_INI"]);
                            if (resultado["FEC_FIN"] != DBNull.Value) entidad.FechaFinal = Convert.ToDateTime(resultado["FEC_FIN"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.Asesor = Convert.ToString(resultado["ASESOR"]);

                            lstResumenExtractos.Add(entidad);
                        }

                        return lstResumenExtractos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ResumenExtractosData", "ListarResumenExtractoss", ex);
                        return null;
                    }
                }
            }
        }


        public List<ResumenExtractos> GeneraryListarResumen(ResumenExtractos entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ResumenExtractos> lstResumenExtractos = new List<ResumenExtractos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    DbParameter pNUM_RADIC = cmdTransaccionFactory.CreateParameter();
                    pNUM_RADIC.ParameterName = "p_numero_radic";
                    pNUM_RADIC.Value = entidad.NumeroRadicacion;
                    pNUM_RADIC.Direction = ParameterDirection.Input;
                    pNUM_RADIC.DbType = DbType.Int64;
                    cmdTransaccionFactory.Parameters.Add(pNUM_RADIC);

                    DbParameter pfec_ini = cmdTransaccionFactory.CreateParameter();
                    pfec_ini.ParameterName = "p_fec_ini";
                    pfec_ini.Value = entidad.FechaInicial;
                    pfec_ini.Direction = ParameterDirection.Input;
                    pfec_ini.DbType = DbType.DateTime;
                    cmdTransaccionFactory.Parameters.Add(pfec_ini);

                    DbParameter pfec_fin = cmdTransaccionFactory.CreateParameter();
                    pfec_fin.ParameterName = "p_fec_fin";
                    pfec_fin.Value = entidad.FechaInicial;
                    pfec_fin.Direction = ParameterDirection.Input;
                    pfec_fin.DbType = DbType.DateTime;
                    cmdTransaccionFactory.Parameters.Add(pfec_fin);

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                    cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RESUMENEXT";
                    cmdTransaccionFactory.ExecuteNonQuery();
                    dbConnectionFactory.CerrarConexion(connection);
                }

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string sql = "Select * from TEMP_EXTRACTO_DATOS order by NUMERO_RADICACION";

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    while (resultado.Read())
                    {
                        entidad = new ResumenExtractos();
                        
                        if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                        if (resultado["COD_PERSONA"] != DBNull.Value) entidad.CodPersona = Convert.ToInt64(resultado["COD_PERSONA"]);
                        if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.Identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                        if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["NOMBRE"]);
                        if (resultado["LINEA"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["LINEA"]);
                        if (resultado["SALDO_INICIAL"] != DBNull.Value) entidad.SaldoInicial = Convert.ToDouble(resultado["SALDO_INICIAL"]);
                        if (resultado["SALDO_FINAL"] != DBNull.Value) entidad.SaldoFinal = Convert.ToDouble(resultado["SALDO_FINAL"]);
                        if (resultado["OFICINA"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["OFICINA"]);
                        if (resultado["FEC_INI"] != DBNull.Value) entidad.FechaInicial = Convert.ToDateTime(resultado["FEC_INI"]);
                        if (resultado["FEC_FIN"] != DBNull.Value) entidad.FechaFinal = Convert.ToDateTime(resultado["FEC_FIN"]);
                        if (resultado["DIRECCION"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["DIRECCION"]);
                        if (resultado["BARRIO"] != DBNull.Value) entidad.Barrio = Convert.ToString(resultado["BARRIO"]);
                        if (resultado["CIUDAD"] != DBNull.Value) entidad.Ciudad = Convert.ToString(resultado["CIUDAD"]);
                        if (resultado["ASESOR"] != DBNull.Value) entidad.Asesor = Convert.ToString(resultado["ASESOR"]);
                        if (resultado["NUMPAGARE"] != DBNull.Value) entidad.numpagare = Convert.ToString(resultado["NUMPAGARE"]);
                        if (resultado["NOMPERIODICIDAD"] != DBNull.Value) entidad.nomperiodicidad = Convert.ToString(resultado["NOMPERIODICIDAD"]);
                        if (resultado["ESTADOCRE"] != DBNull.Value) entidad.estadocre = Convert.ToString(resultado["ESTADOCRE"]);
                        if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                        if (resultado["FEC_PROXIMO_PAGO"] != DBNull.Value) entidad.fec_proximo_pago = Convert.ToDateTime(resultado["FEC_PROXIMO_PAGO"]);
                        if (resultado["EMAIL"] != DBNull.Value) entidad.Email = Convert.ToString(resultado["EMAIL"]);
                        if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                        lstResumenExtractos.Add(entidad);
                    }

                    dbConnectionFactory.CerrarConexion(connection);
                    return lstResumenExtractos;
                }
            }
        }



        public List<ResumenExtractos> ListarBancos(string codlinea, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ResumenExtractos> lstBancos = new List<ResumenExtractos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select cod_parametro, valor From Parametros_Linea Where Cod_Linea_Credito = '" + codlinea.ToString() + "' And Cod_Parametro In(580,581,582,583) Order By Cod_Linea_Credito,Cod_Parametro";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ResumenExtractos entidad = new ResumenExtractos();
                            //Asociar todos los valores a la entidad
                            //if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["COD_PARAMETRO"] != DBNull.Value) entidad.cod_banco_para = Convert.ToInt64(resultado["COD_PARAMETRO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.nombrebanco = Convert.ToString(resultado["VALOR"]);
                            lstBancos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstBancos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ResumenExtractosData", "ListarBancos", ex);
                        return null;
                    }
                }
            }
        }


        public List<ResumenExtractos> GeneraryListarResumenDetalle(ResumenExtractos pGenera, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ResumenExtractos> lstResumenExtractos = new List<ResumenExtractos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    DbParameter pNUM_RADIC = cmdTransaccionFactory.CreateParameter();
                    pNUM_RADIC.ParameterName = "lnumero_radicacion";
                    pNUM_RADIC.Value = pGenera.NumeroRadicacion;
                    pNUM_RADIC.Direction = ParameterDirection.Input;
                    pNUM_RADIC.DbType = DbType.Int64;
                    cmdTransaccionFactory.Parameters.Add(pNUM_RADIC);

                    DbParameter pfec_ini = cmdTransaccionFactory.CreateParameter();
                    pfec_ini.ParameterName = "pf_fecha_corte";
                    pfec_ini.Value = pGenera.FechaFinal;
                    pfec_ini.Direction = ParameterDirection.Input;
                    pfec_ini.DbType = DbType.DateTime;
                    cmdTransaccionFactory.Parameters.Add(pfec_ini);

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                    cmdTransaccionFactory.CommandText = "XPF_AS_DETALLEEXTRACTO";
                    cmdTransaccionFactory.ExecuteNonQuery();
                    dbConnectionFactory.CerrarConexion(connection);
                }

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string sql = "Select * from TEMP_DETAEXTRACTO";

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    while (resultado.Read())
                    {
                        ResumenExtractos entidad = new ResumenExtractos();

                        if (resultado["CAPITAL"] != DBNull.Value) entidad.capital = Convert.ToDecimal(resultado["CAPITAL"]);
                        if (resultado["INTERES_CTE"] != DBNull.Value) entidad.interes_cte = Convert.ToDecimal(resultado["INTERES_CTE"]);
                        if (resultado["SEGURO"] != DBNull.Value) entidad.seguro = Convert.ToDecimal(resultado["SEGURO"]);
                        if (resultado["INTERES_MORA"] != DBNull.Value) entidad.interes_mora = Convert.ToDecimal(resultado["INTERES_MORA"]);
                        if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToString(resultado["TASA_INTERES"]);
                        lstResumenExtractos.Add(entidad);
                    }

                    dbConnectionFactory.CerrarConexion(connection);
                    return lstResumenExtractos;
                }
            }
        }





    }
}
