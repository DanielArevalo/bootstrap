using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;

namespace Xpinn.Cartera.Data
{
    public class GarantiasClasificacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public GarantiasClasificacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public GarantiasClasificacion CrearGarantiasClasificacion(GarantiasClasificacion pGarantiasClasificacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidgarantia = cmdTransaccionFactory.CreateParameter();
                        pidgarantia.ParameterName = "p_idgarantia";
                        pidgarantia.Value = pGarantiasClasificacion.idgarantia;
                        pidgarantia.Direction = ParameterDirection.Input;
                        pidgarantia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidgarantia);

                        DbParameter pcod_clasifica = cmdTransaccionFactory.CreateParameter();
                        pcod_clasifica.ParameterName = "p_cod_clasifica";
                        pcod_clasifica.Value = pGarantiasClasificacion.cod_clasifica;
                        pcod_clasifica.Direction = ParameterDirection.Input;
                        pcod_clasifica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_clasifica);

                        DbParameter ptipo_garantia = cmdTransaccionFactory.CreateParameter();
                        ptipo_garantia.ParameterName = "p_tipo_garantia";
                        ptipo_garantia.Value = pGarantiasClasificacion.tipo_garantia;
                        ptipo_garantia.Direction = ParameterDirection.Input;
                        ptipo_garantia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_garantia);

                        DbParameter pdias_inicial = cmdTransaccionFactory.CreateParameter();
                        pdias_inicial.ParameterName = "p_dias_inicial";
                        if (pGarantiasClasificacion.dias_inicial == null)
                            pdias_inicial.Value = DBNull.Value;
                        else
                            pdias_inicial.Value = pGarantiasClasificacion.dias_inicial;
                        pdias_inicial.Direction = ParameterDirection.Input;
                        pdias_inicial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_inicial);

                        DbParameter pdias_final = cmdTransaccionFactory.CreateParameter();
                        pdias_final.ParameterName = "p_dias_final";
                        if (pGarantiasClasificacion.dias_final == null)
                            pdias_final.Value = DBNull.Value;
                        else
                            pdias_final.Value = pGarantiasClasificacion.dias_final;
                        pdias_final.Direction = ParameterDirection.Input;
                        pdias_final.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_final);

                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        pporcentaje.Value = pGarantiasClasificacion.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_GARCLASI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pGarantiasClasificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiasClasificacionData", "CrearGarantiasClasificacion", ex);
                        return null;
                    }
                }
            }
        }


        public GarantiasClasificacion ModificarGarantiasClasificacion(GarantiasClasificacion pGarantiasClasificacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidgarantia = cmdTransaccionFactory.CreateParameter();
                        pidgarantia.ParameterName = "p_idgarantia";
                        pidgarantia.Value = pGarantiasClasificacion.idgarantia;
                        pidgarantia.Direction = ParameterDirection.Input;
                        pidgarantia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidgarantia);

                        DbParameter pcod_clasifica = cmdTransaccionFactory.CreateParameter();
                        pcod_clasifica.ParameterName = "p_cod_clasifica";
                        pcod_clasifica.Value = pGarantiasClasificacion.cod_clasifica;
                        pcod_clasifica.Direction = ParameterDirection.Input;
                        pcod_clasifica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_clasifica);

                        DbParameter ptipo_garantia = cmdTransaccionFactory.CreateParameter();
                        ptipo_garantia.ParameterName = "p_tipo_garantia";
                        ptipo_garantia.Value = pGarantiasClasificacion.tipo_garantia;
                        ptipo_garantia.Direction = ParameterDirection.Input;
                        ptipo_garantia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_garantia);

                        DbParameter pdias_inicial = cmdTransaccionFactory.CreateParameter();
                        pdias_inicial.ParameterName = "p_dias_inicial";
                        if (pGarantiasClasificacion.dias_inicial == null)
                            pdias_inicial.Value = DBNull.Value;
                        else
                            pdias_inicial.Value = pGarantiasClasificacion.dias_inicial;
                        pdias_inicial.Direction = ParameterDirection.Input;
                        pdias_inicial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_inicial);

                        DbParameter pdias_final = cmdTransaccionFactory.CreateParameter();
                        pdias_final.ParameterName = "p_dias_final";
                        if (pGarantiasClasificacion.dias_final == null)
                            pdias_final.Value = DBNull.Value;
                        else
                            pdias_final.Value = pGarantiasClasificacion.dias_final;
                        pdias_final.Direction = ParameterDirection.Input;
                        pdias_final.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_final);

                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        pporcentaje.Value = pGarantiasClasificacion.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_GARCLASI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pGarantiasClasificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiasClasificacionData", "ModificarGarantiasClasificacion", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarGarantiasClasificacion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        GarantiasClasificacion pGarantiasClasificacion = new GarantiasClasificacion();
                        pGarantiasClasificacion = ConsultarGarantiasClasificacion(pId, vUsuario);

                        DbParameter pidgarantia = cmdTransaccionFactory.CreateParameter();
                        pidgarantia.ParameterName = "p_idgarantia";
                        pidgarantia.Value = pGarantiasClasificacion.idgarantia;
                        pidgarantia.Direction = ParameterDirection.Input;
                        pidgarantia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidgarantia);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_GARCLASI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiasClasificacionData", "EliminarGarantiasClasificacion", ex);
                    }
                }
            }
        }


        public GarantiasClasificacion ConsultarGarantiasClasificacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            GarantiasClasificacion entidad = new GarantiasClasificacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM GARANTIAS_CLASIFICACION WHERE IDGARANTIA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDGARANTIA"] != DBNull.Value) entidad.idgarantia = Convert.ToInt32(resultado["IDGARANTIA"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["COD_CLASIFICA"]);
                            if (resultado["TIPO_GARANTIA"] != DBNull.Value) entidad.tipo_garantia = Convert.ToInt32(resultado["TIPO_GARANTIA"]);
                            if (resultado["DIAS_INICIAL"] != DBNull.Value) entidad.dias_inicial = Convert.ToInt32(resultado["DIAS_INICIAL"]);
                            if (resultado["DIAS_FINAL"] != DBNull.Value) entidad.dias_final = Convert.ToInt32(resultado["DIAS_FINAL"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]);
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
                        BOExcepcion.Throw("GarantiasClasificacionData", "ConsultarGarantiasClasificacion", ex);
                        return null;
                    }
                }
            }
        }


        public List<GarantiasClasificacion> ListarGarantiasClasificacion(GarantiasClasificacion pGarantiasClasificacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<GarantiasClasificacion> lstGarantiasClasificacion = new List<GarantiasClasificacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM GARANTIAS_CLASIFICACION " + ObtenerFiltro(pGarantiasClasificacion) + " ORDER BY IDGARANTIA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            GarantiasClasificacion entidad = new GarantiasClasificacion();
                            if (resultado["IDGARANTIA"] != DBNull.Value) entidad.idgarantia = Convert.ToInt32(resultado["IDGARANTIA"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["COD_CLASIFICA"]);
                            if (resultado["TIPO_GARANTIA"] != DBNull.Value) entidad.tipo_garantia = Convert.ToInt32(resultado["TIPO_GARANTIA"]);
                            if (entidad.tipo_garantia == 1)
                                entidad.nom_tipo_garantia = "admisibles no hipotecarias";
                            else if (entidad.tipo_garantia == 2)
                                entidad.nom_tipo_garantia = "hipotecarias";
                            if (resultado["DIAS_INICIAL"] != DBNull.Value) entidad.dias_inicial = Convert.ToInt32(resultado["DIAS_INICIAL"]);
                            if (resultado["DIAS_FINAL"] != DBNull.Value) entidad.dias_final = Convert.ToInt32(resultado["DIAS_FINAL"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]);
                            lstGarantiasClasificacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGarantiasClasificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiasClasificacionData", "ListarGarantiasClasificacion", ex);
                        return null;
                    }
                }
            }
        }


    }
}
