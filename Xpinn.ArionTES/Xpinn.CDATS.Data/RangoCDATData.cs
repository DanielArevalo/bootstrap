using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla RangoCDAT
    /// </summary>
    public class RangoCDATData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla LineaCDATS
        /// </summary>
        public RangoCDATData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public RangoCDAT CrearRangoCDAT(RangoCDAT pRangoCDAT, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_rango = cmdTransaccionFactory.CreateParameter();
                        pcod_rango.ParameterName = "p_cod_rango";
                        pcod_rango.Value = pRangoCDAT.cod_rango;
                        pcod_rango.Direction = ParameterDirection.Output;
                        pcod_rango.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_rango);
 
                        DbParameter pcod_lineacdat = cmdTransaccionFactory.CreateParameter();
                        pcod_lineacdat.ParameterName = "p_cod_lineacdat";
                        pcod_lineacdat.Value = pRangoCDAT.cod_lineacdat;
                        pcod_lineacdat.Direction = ParameterDirection.Input;
                        pcod_lineacdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_lineacdat);
 
                        DbParameter ptipo_tope = cmdTransaccionFactory.CreateParameter();
                        ptipo_tope.ParameterName = "p_tipo_tope";
                        if (pRangoCDAT.tipo_tope == null)
                            ptipo_tope.Value = DBNull.Value;
                        else
                            ptipo_tope.Value = pRangoCDAT.tipo_tope;
                        ptipo_tope.Direction = ParameterDirection.Input;
                        ptipo_tope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tope);
 
                        DbParameter pminimo = cmdTransaccionFactory.CreateParameter();
                        pminimo.ParameterName = "p_minimo";
                        if (pRangoCDAT.minimo == null)
                            pminimo.Value = DBNull.Value;
                        else
                            pminimo.Value = pRangoCDAT.minimo;
                        pminimo.Direction = ParameterDirection.Input;
                        pminimo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pminimo);
 
                        DbParameter pmaximo = cmdTransaccionFactory.CreateParameter();
                        pmaximo.ParameterName = "p_maximo";
                        if (pRangoCDAT.maximo == null)
                            pmaximo.Value = DBNull.Value;
                        else
                            pmaximo.Value = pRangoCDAT.maximo;
                        pmaximo.Direction = ParameterDirection.Input;
                        pmaximo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmaximo);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_RANGOCDAT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (pcod_rango.Value != null)
                            pRangoCDAT.cod_rango = Convert.ToInt64(pcod_rango.Value);

                        return pRangoCDAT;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RangoCDATData", "CrearRangoCDAT", ex);
                        return null;
                    }
                }
            }
        }
        public RangoCDAT ModificarRangoCDAT(RangoCDAT pRangoCDAT, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_rango = cmdTransaccionFactory.CreateParameter();
                        pcod_rango.ParameterName = "p_cod_rango";
                        pcod_rango.Value = pRangoCDAT.cod_rango;
                        pcod_rango.Direction = ParameterDirection.Input;
                        pcod_rango.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_rango);
 
                        DbParameter pcod_lineacdat = cmdTransaccionFactory.CreateParameter();
                        pcod_lineacdat.ParameterName = "p_cod_lineacdat";
                        pcod_lineacdat.Value = pRangoCDAT.cod_lineacdat;
                        pcod_lineacdat.Direction = ParameterDirection.Input;
                        pcod_lineacdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_lineacdat);
 
                        DbParameter ptipo_tope = cmdTransaccionFactory.CreateParameter();
                        ptipo_tope.ParameterName = "p_tipo_tope";
                        if (pRangoCDAT.tipo_tope == null)
                            ptipo_tope.Value = DBNull.Value;
                        else
                            ptipo_tope.Value = pRangoCDAT.tipo_tope;
                        ptipo_tope.Direction = ParameterDirection.Input;
                        ptipo_tope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tope);
 
                        DbParameter pminimo = cmdTransaccionFactory.CreateParameter();
                        pminimo.ParameterName = "p_minimo";
                        if (pRangoCDAT.minimo == null)
                            pminimo.Value = DBNull.Value;
                        else
                            pminimo.Value = pRangoCDAT.minimo;
                        pminimo.Direction = ParameterDirection.Input;
                        pminimo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pminimo);
 
                        DbParameter pmaximo = cmdTransaccionFactory.CreateParameter();
                        pmaximo.ParameterName = "p_maximo";
                        if (pRangoCDAT.maximo == null)
                            pmaximo.Value = DBNull.Value;
                        else
                            pmaximo.Value = pRangoCDAT.maximo;
                        pmaximo.Direction = ParameterDirection.Input;
                        pmaximo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmaximo);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_RANGOCDAT_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pRangoCDAT;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RangoCDATData", "ModificarRangoCDAT", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarRangoCDAT(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        RangoCDAT pRangoCDAT = new RangoCDAT();
                        pRangoCDAT = ConsultarRangoCDAT(pId, vUsuario);
                        
                        DbParameter pcod_rango = cmdTransaccionFactory.CreateParameter();
                        pcod_rango.ParameterName = "p_cod_rango";
                        pcod_rango.Value = pRangoCDAT.cod_rango;
                        pcod_rango.Direction = ParameterDirection.Input;
                        pcod_rango.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_rango);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_RANGOCDAT_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RangoCDATData", "EliminarRangoCDAT", ex);
                    }
                }
            }
        }

        public RangoCDAT ConsultarRangoCDAT(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            RangoCDAT entidad = new RangoCDAT();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Rango_CDAT WHERE COD_RANGO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_RANGO"] != DBNull.Value) entidad.cod_rango = Convert.ToInt64(resultado["COD_RANGO"]);
                            if (resultado["COD_LINEACDAT"] != DBNull.Value) entidad.cod_lineacdat = Convert.ToString(resultado["COD_LINEACDAT"]);
                            if (resultado["TIPO_TOPE"] != DBNull.Value) entidad.tipo_tope = Convert.ToInt32(resultado["TIPO_TOPE"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["MAXIMO"]);
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
                        BOExcepcion.Throw("RangoCDATData", "ConsultarRangoCDAT", ex);
                        return null;
                    }
                }
            }
        }

        public List<RangoCDAT> ListarRangoCDAT(RangoCDAT pRangoCDAT, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<RangoCDAT> lstRangoCDAT = new List<RangoCDAT>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Rango_CDAT  where cod_lineacdat = " + pRangoCDAT.cod_lineacdat + "ORDER BY COD_RANGO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            RangoCDAT entidad = new RangoCDAT();
                            if (resultado["COD_RANGO"] != DBNull.Value) entidad.cod_rango = Convert.ToInt64(resultado["COD_RANGO"]);
                            if (resultado["COD_LINEACDAT"] != DBNull.Value) entidad.cod_lineacdat = Convert.ToString(resultado["COD_LINEACDAT"]);
                            if (resultado["TIPO_TOPE"] != DBNull.Value) entidad.tipo_tope = Convert.ToInt32(resultado["TIPO_TOPE"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["MAXIMO"]);
                            lstRangoCDAT.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstRangoCDAT;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RangoCDATData", "ListarRangoCDAT", ex);
                        return null;
                    }
                }
            }
        }

    }
}
