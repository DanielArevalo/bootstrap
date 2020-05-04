using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Servicios.Entities;

namespace Xpinn.Servicios.Data
{
    public class AtributosTasasData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public AtributosTasasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public RangoTasas CrearModRangoTasas(RangoTasas pRangoTasas, Usuario vUsuario, int pOpcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodrango = cmdTransaccionFactory.CreateParameter();
                        pcodrango.ParameterName = "p_codrango";
                        pcodrango.Value = pRangoTasas.codrango;
                        pcodrango.Direction = pOpcion == 1 ? ParameterDirection.Output : ParameterDirection.Input;
                        pcodrango.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodrango);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pRangoTasas.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pcod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        pcod_linea_servicio.Value = pRangoTasas.cod_linea_servicio;
                        pcod_linea_servicio.Direction = ParameterDirection.Input;
                        pcod_linea_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_servicio);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = pOpcion == 1 ? "USP_XPINN_SER_RANGO_TASA_CREAR" : "USP_XPINN_SER_RANGO_TASA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (pOpcion == 1)
                            pRangoTasas.codrango = Convert.ToInt32(pcodrango.Value);
                        return pRangoTasas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AtributosTasasData", "CrearModRangoTasas", ex);
                        return null;
                    }
                }
            }
        }


        public RangoTasasTope CrearModRangoTasasTope(RangoTasasTope pRangoTasasTope, Usuario vUsuario, int pOpcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodtope = cmdTransaccionFactory.CreateParameter();
                        pcodtope.ParameterName = "p_codtope";
                        pcodtope.Value = pRangoTasasTope.codtope;
                        pcodtope.Direction = pOpcion == 1 ? ParameterDirection.Output : ParameterDirection.Input;
                        pcodtope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodtope);

                        DbParameter pcodrango = cmdTransaccionFactory.CreateParameter();
                        pcodrango.ParameterName = "p_codrango";
                        pcodrango.Value = pRangoTasasTope.codrango;
                        pcodrango.Direction = ParameterDirection.Input;
                        pcodrango.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodrango);

                        DbParameter ptipo_tope = cmdTransaccionFactory.CreateParameter();
                        ptipo_tope.ParameterName = "p_tipo_tope";
                        ptipo_tope.Value = pRangoTasasTope.tipo_tope;
                        ptipo_tope.Direction = ParameterDirection.Input;
                        ptipo_tope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tope);

                        DbParameter pminimo = cmdTransaccionFactory.CreateParameter();
                        pminimo.ParameterName = "p_minimo";
                        if (pRangoTasasTope.minimo == null)
                            pminimo.Value = DBNull.Value;
                        else
                            pminimo.Value = pRangoTasasTope.minimo;
                        pminimo.Direction = ParameterDirection.Input;
                        pminimo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pminimo);

                        DbParameter pmaximo = cmdTransaccionFactory.CreateParameter();
                        pmaximo.ParameterName = "p_maximo";
                        if (pRangoTasasTope.maximo == null)
                            pmaximo.Value = DBNull.Value;
                        else
                            pmaximo.Value = pRangoTasasTope.maximo;
                        pmaximo.Direction = ParameterDirection.Input;
                        pmaximo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmaximo);

                        DbParameter pcod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        pcod_linea_servicio.Value = pRangoTasasTope.cod_linea_servicio;
                        pcod_linea_servicio.Direction = ParameterDirection.Input;
                        pcod_linea_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_servicio);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = pOpcion == 1 ? "USP_XPINN_SER_TASATOPE_CREAR" : "USP_XPINN_SER_TASATOPE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (pOpcion == 1)
                            pRangoTasasTope.codtope = Convert.ToInt32(pcodtope.Value);
                        return pRangoTasasTope;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AtributosTasasData", "CrearModRangoTasasTope", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarSoloRangoTasasTope(long conseID, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_codtope = cmdTransaccionFactory.CreateParameter();
                        p_codtope.ParameterName = "p_codtope";
                        p_codtope.Value = conseID;
                        p_codtope.Direction = ParameterDirection.Input;
                        p_codtope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_codtope);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_RANGO_TAS_TOP_EL";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AtributosLineaServicioData", "EliminarSoloRangoTasasTope", ex);
                    }
                }
            }
        }

        public AtributosLineaServicio CrearModAtributosLineaServicio(AtributosLineaServicio pAtributosLineaServicio, Usuario vUsuario, int pOpcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodatrser = cmdTransaccionFactory.CreateParameter();
                        pcodatrser.ParameterName = "p_codatrser";
                        pcodatrser.Value = pAtributosLineaServicio.codatrser;
                        pcodatrser.Direction = pOpcion == 1 ? ParameterDirection.Output : ParameterDirection.Input;
                        pcodatrser.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodatrser);

                        DbParameter pcodrango = cmdTransaccionFactory.CreateParameter();
                        pcodrango.ParameterName = "p_codrango";
                        pcodrango.Value = pAtributosLineaServicio.codrango;
                        pcodrango.Direction = ParameterDirection.Input;
                        pcodrango.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodrango);

                        DbParameter pcod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        pcod_linea_servicio.Value = pAtributosLineaServicio.cod_linea_servicio;
                        pcod_linea_servicio.Direction = ParameterDirection.Input;
                        pcod_linea_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_servicio);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pAtributosLineaServicio.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pcalculo_atr = cmdTransaccionFactory.CreateParameter();
                        pcalculo_atr.ParameterName = "p_calculo_atr";
                        if (pAtributosLineaServicio.calculo_atr == null)
                            pcalculo_atr.Value = DBNull.Value;
                        else
                            pcalculo_atr.Value = pAtributosLineaServicio.calculo_atr;
                        pcalculo_atr.Direction = ParameterDirection.Input;
                        pcalculo_atr.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcalculo_atr);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pAtributosLineaServicio.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pAtributosLineaServicio.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pAtributosLineaServicio.desviacion == null)
                            pdesviacion.Value = DBNull.Value;
                        else
                            pdesviacion.Value = pAtributosLineaServicio.desviacion;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter ptipo_tasa = cmdTransaccionFactory.CreateParameter();
                        ptipo_tasa.ParameterName = "p_tipo_tasa";
                        if (pAtributosLineaServicio.tipo_tasa == null)
                            ptipo_tasa.Value = DBNull.Value;
                        else
                            ptipo_tasa.Value = pAtributosLineaServicio.tipo_tasa;
                        ptipo_tasa.Direction = ParameterDirection.Input;
                        ptipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tasa);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        if (pAtributosLineaServicio.tasa == null)
                            ptasa.Value = DBNull.Value;
                        else
                            ptasa.Value = pAtributosLineaServicio.tasa;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = pOpcion == 1 ? "USP_XPINN_SER_ATRIBUTOSL_CREAR" : "USP_XPINN_SER_ATRIBUTOSL_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (pOpcion == 1)
                            pAtributosLineaServicio.codatrser = Convert.ToInt32(pcodatrser.Value);
                        return pAtributosLineaServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AtributosLineaServicioData", "CrearAtributosLineaServicio", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarRangoTopes(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodrango = cmdTransaccionFactory.CreateParameter();
                        pcodrango.ParameterName = "p_codrango";
                        pcodrango.Value = pId;
                        pcodrango.Direction = ParameterDirection.Input;
                        pcodrango.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodrango);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_RANGO_TASA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AtributosLineaServicioData", "EliminarRangoTopes", ex);
                    }
                }
            }
        }


        public List<RangoTasas> ListarRangoTasas(RangoTasas pRangoTasas, string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<RangoTasas> lstRangoTasas = new List<RangoTasas>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM RANGO_TASAS " + ObtenerFiltro(pRangoTasas);
                        if (pFiltro != null && !string.IsNullOrWhiteSpace(pFiltro))
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " AND ";
                            else
                                sql += " WHERE ";
                        }
                        sql += pFiltro + " ORDER BY CODRANGO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            RangoTasas entidad = new RangoTasas();
                            if (resultado["CODRANGO"] != DBNull.Value) entidad.codrango = Convert.ToInt32(resultado["CODRANGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_LINEA_SERVICIO"] != DBNull.Value) entidad.cod_linea_servicio = Convert.ToString(resultado["COD_LINEA_SERVICIO"]);
                            lstRangoTasas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstRangoTasas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AtributosLineaServicioData", "ListarRangoTasas", ex);
                        return null;
                    }
                }
            }
        }


        public RangoTasas ConsultarRangoTasas(RangoTasas pRangoTasas, ref string pError, Usuario vUsuario)
        {
            DbDataReader resultado;
            RangoTasas entidad = new RangoTasas();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM RANGO_TASAS " + ObtenerFiltro(pRangoTasas) + " ORDER BY CODRANGO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODRANGO"] != DBNull.Value) entidad.codrango = Convert.ToInt32(resultado["CODRANGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_LINEA_SERVICIO"] != DBNull.Value) entidad.cod_linea_servicio = Convert.ToString(resultado["COD_LINEA_SERVICIO"]);
                        }
                        else
                            return null;
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }


        public List<RangoTasasTope> ListarRangoTasas(RangoTasasTope pRangoTasasTope, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<RangoTasasTope> lstRangoTopes = new List<RangoTasasTope>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM RANGO_TASAS_TOPE " + ObtenerFiltro(pRangoTasasTope) + " ORDER BY CODTOPE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            RangoTasasTope entidad = new RangoTasasTope();
                            if (resultado["CODTOPE"] != DBNull.Value) entidad.codtope = Convert.ToInt32(resultado["CODTOPE"]);
                            if (resultado["CODRANGO"] != DBNull.Value) entidad.codrango = Convert.ToInt32(resultado["CODRANGO"]);
                            if (resultado["TIPO_TOPE"] != DBNull.Value) entidad.tipo_tope = Convert.ToInt32(resultado["TIPO_TOPE"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["MAXIMO"]);
                            if (resultado["COD_LINEA_SERVICIO"] != DBNull.Value) entidad.cod_linea_servicio = Convert.ToString(resultado["COD_LINEA_SERVICIO"]);
                            lstRangoTopes.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstRangoTopes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AtributosLineaServicioData", "ListarRangoTasas", ex);
                        return null;
                    }
                }
            }
        }

        public Int64 ObtenerValorTopeMaximo(RangoTasasTope pRangoTasasTope, Usuario vUsuario)
        {
            DbDataReader resultado;
            Int64 valor_maximo = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select Max(StrToNumber(maximo)) as VALORMAXIMO from rango_tasas_tope where tipo_tope = 3 And cod_linea_servicio = '" + pRangoTasasTope.cod_linea_servicio + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VALORMAXIMO"] != DBNull.Value) valor_maximo = Convert.ToInt64(resultado["VALORMAXIMO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor_maximo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AtributosLineaServicioData", "ObtenerValorTopeMaximo", ex);
                        return valor_maximo;
                    }
                }
            }
        }

        public AtributosLineaServicio ConsultarAtributosLineaServicio(AtributosLineaServicio pAtributos, Usuario vUsuario)
        {
            DbDataReader resultado;
            AtributosLineaServicio entidad = new AtributosLineaServicio();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ATRIBUTOSLINSER " + ObtenerFiltro(pAtributos);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODATRSER"] != DBNull.Value) entidad.codatrser = Convert.ToInt32(resultado["CODATRSER"]);
                            if (resultado["CODRANGO"] != DBNull.Value) entidad.codrango = Convert.ToInt32(resultado["CODRANGO"]);
                            if (resultado["COD_LINEA_SERVICIO"] != DBNull.Value) entidad.cod_linea_servicio = Convert.ToString(resultado["COD_LINEA_SERVICIO"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["CALCULO_ATR"] != DBNull.Value) entidad.calculo_atr = Convert.ToString(resultado["CALCULO_ATR"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                        }
                        else
                            return null;
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AtributosLineaServicioData", "ConsultarAtributosLineaServicio", ex);
                        return null;
                    }
                }
            }
        }

        public Int64 ValorPagoServicio(string tipoPago, string numeroservicios, string fecha, Usuario vUsuario)
        {
            DbDataReader resultado;
            Int64 valor_maximo = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (tipoPago == "19")
                        {
                            sql = "select VALOR_CUOTA as VALOR from SERVICIOS where NUMERO_SERVICIO = " + numeroservicios;
                        }
                        else
                        {
                            sql = "select CALCULAR_TOTALAPAGAR_SERVICIO(" + numeroservicios + ", To_Date('" + Convert.ToDateTime(fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "')) as VALOR from dual";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        { }
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor_maximo = Convert.ToInt64(resultado["VALOR"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor_maximo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AtributosLineaServicioData", "ValorPagoServicio", ex);
                        return valor_maximo;
                    }
                }
            }
        }



    }
}
