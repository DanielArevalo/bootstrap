using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla CambioMoneda
    /// </summary>
    public class CambioMonedaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla CambioMoneda
        /// </summary>
        public CambioMonedaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public CambioMoneda CrearCambioMoneda(CambioMoneda pCambio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcambiomoneda = cmdTransaccionFactory.CreateParameter();
                        pidcambiomoneda.ParameterName = "p_idcambiomoneda";
                        pidcambiomoneda.Value = pCambio.idcambiomoneda;
                        pidcambiomoneda.Direction = ParameterDirection.Output;
                        pidcambiomoneda.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidcambiomoneda);

                        DbParameter pcod_moneda_ini = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda_ini.ParameterName = "p_cod_moneda_ini";
                        pcod_moneda_ini.Value = pCambio.cod_moneda_ini;
                        pcod_moneda_ini.Direction = ParameterDirection.Input;
                        pcod_moneda_ini.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda_ini);

                        DbParameter pcod_moneda_fin = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda_fin.ParameterName = "p_cod_moneda_fin";
                        pcod_moneda_fin.Value = pCambio.cod_moneda_fin;
                        pcod_moneda_fin.Direction = ParameterDirection.Input;
                        pcod_moneda_fin.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda_fin);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pCambio.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pvalor_compra = cmdTransaccionFactory.CreateParameter();
                        pvalor_compra.ParameterName = "p_valor_compra";
                        pvalor_compra.Value = pCambio.valor_compra;
                        pvalor_compra.Direction = ParameterDirection.Input;
                        pvalor_compra.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_compra);

                        DbParameter pvalor_venta = cmdTransaccionFactory.CreateParameter();
                        pvalor_venta.ParameterName = "p_valor_venta";
                        pvalor_venta.Value = pCambio.valor_venta;
                        pvalor_venta.Direction = ParameterDirection.Input;
                        pvalor_venta.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_venta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CAMBIOMONE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pCambio.idcambiomoneda = Convert.ToInt64(pidcambiomoneda.Value);
                        return pCambio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CambioMonedaData", "CrearCambioMoneda", ex);
                        return null;
                    }
                }
            }
        }


        public CambioMoneda ModificarCambioMoneda(CambioMoneda pCambio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcambiomoneda = cmdTransaccionFactory.CreateParameter();
                        pidcambiomoneda.ParameterName = "p_idcambiomoneda";
                        pidcambiomoneda.Value = pCambio.idcambiomoneda;
                        pidcambiomoneda.Direction = ParameterDirection.Input;
                        pidcambiomoneda.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidcambiomoneda);

                        DbParameter pcod_moneda_ini = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda_ini.ParameterName = "p_cod_moneda_ini";
                        pcod_moneda_ini.Value = pCambio.cod_moneda_ini;
                        pcod_moneda_ini.Direction = ParameterDirection.Input;
                        pcod_moneda_ini.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda_ini);

                        DbParameter pcod_moneda_fin = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda_fin.ParameterName = "p_cod_moneda_fin";
                        pcod_moneda_fin.Value = pCambio.cod_moneda_fin;
                        pcod_moneda_fin.Direction = ParameterDirection.Input;
                        pcod_moneda_fin.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda_fin);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pCambio.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pvalor_compra = cmdTransaccionFactory.CreateParameter();
                        pvalor_compra.ParameterName = "p_valor_compra";
                        pvalor_compra.Value = pCambio.valor_compra;
                        pvalor_compra.Direction = ParameterDirection.Input;
                        pvalor_compra.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_compra);

                        DbParameter pvalor_venta = cmdTransaccionFactory.CreateParameter();
                        pvalor_venta.ParameterName = "p_valor_venta";
                        pvalor_venta.Value = pCambio.valor_venta;
                        pvalor_venta.Direction = ParameterDirection.Input;
                        pvalor_venta.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_venta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CAMBIOMONE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCambio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CambioMonedaData", "ModificarCambioMoneda", ex);
                        return null;
                    }
                }
            }
        }



        public CambioMoneda ConsultarCambioMoneda(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            CambioMoneda entidad = new CambioMoneda();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Cambiomoneda WHERE IDCAMBIOMONEDA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCAMBIOMONEDA"] != DBNull.Value) entidad.idcambiomoneda = Convert.ToInt64(resultado["IDCAMBIOMONEDA"]);
                            if (resultado["COD_MONEDA_INI"] != DBNull.Value) entidad.cod_moneda_ini = Convert.ToInt32(resultado["COD_MONEDA_INI"]);
                            if (resultado["COD_MONEDA_FIN"] != DBNull.Value) entidad.cod_moneda_fin = Convert.ToInt32(resultado["COD_MONEDA_FIN"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["VALOR_COMPRA"] != DBNull.Value) entidad.valor_compra = Convert.ToDecimal(resultado["VALOR_COMPRA"]);
                            if (resultado["VALOR_VENTA"] != DBNull.Value) entidad.valor_venta = Convert.ToDecimal(resultado["VALOR_VENTA"]);
                        }                      
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CambioMonedaData", "ConsultarCambioMoneda", ex);
                        return null;
                    }
                }
            }
        }



        public List<CambioMoneda> ListarCambioMoneda(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CambioMoneda> lstCambio = new List<CambioMoneda>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.*,(SELECT T.DESCRIPCION FROM TIPOMONEDA T WHERE T.COD_MONEDA = C.COD_MONEDA_INI) AS NOM_MONEDAINI, "
                                    + "(SELECT M.DESCRIPCION FROM TIPOMONEDA M WHERE M.COD_MONEDA = C.COD_MONEDA_FIN ) AS NOM_MONEDAFIN "
                                    +"FROM CAMBIOMONEDA C  " + filtro + " ORDER BY C.IDCAMBIOMONEDA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CambioMoneda entidad = new CambioMoneda();
                            if (resultado["IDCAMBIOMONEDA"] != DBNull.Value) entidad.idcambiomoneda = Convert.ToInt64(resultado["IDCAMBIOMONEDA"]);
                            if (resultado["COD_MONEDA_INI"] != DBNull.Value) entidad.cod_moneda_ini = Convert.ToInt32(resultado["COD_MONEDA_INI"]);
                            if (resultado["COD_MONEDA_FIN"] != DBNull.Value) entidad.cod_moneda_fin = Convert.ToInt32(resultado["COD_MONEDA_FIN"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["VALOR_COMPRA"] != DBNull.Value) entidad.valor_compra = Convert.ToDecimal(resultado["VALOR_COMPRA"]);
                            if (resultado["VALOR_VENTA"] != DBNull.Value) entidad.valor_venta = Convert.ToDecimal(resultado["VALOR_VENTA"]);
                            if (resultado["NOM_MONEDAINI"] != DBNull.Value) entidad.nom_monedaOrigen = Convert.ToString(resultado["NOM_MONEDAINI"]);
                            if (resultado["NOM_MONEDAFIN"] != DBNull.Value) entidad.nom_monedaDestino = Convert.ToString(resultado["NOM_MONEDAFIN"]);
                            lstCambio.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCambio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CambioMonedaData", "ListarCambioMoneda", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarCambioMoneda(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM CAMBIOMONEDA WHERE IDCAMBIOMONEDA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CambioMonedaData", "EliminarCambioMoneda", ex);
                    }
                }
            }
        }


        
    }
}