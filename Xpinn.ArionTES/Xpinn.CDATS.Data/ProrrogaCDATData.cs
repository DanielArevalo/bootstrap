using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Data
{
    public class ProrrogaCDATData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public ProrrogaCDATData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }



        public Cdat ModificarCDATProrroga(Cdat pCdat, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_Codigo_Cdat";
                        pcodigo_cdat.Value = pCdat.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);
                       
                        DbParameter pplazo = cmdTransaccionFactory.CreateParameter();
                        pplazo.ParameterName = "p_Plazo";
                        pplazo.Value = pCdat.plazo;
                        pplazo.Direction = ParameterDirection.Input;
                        pplazo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pplazo);
                                                
                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_Fecha_Inicio";
                        pfecha_inicio.Value = pCdat.fecha_inicio;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter pfecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        pfecha_vencimiento.ParameterName = "p_Fecha_Vencimiento";
                        if (pCdat.fecha_vencimiento != DateTime.MinValue) pfecha_vencimiento.Value = pCdat.fecha_vencimiento; else pfecha_vencimiento.Value = DBNull.Value;
                        pfecha_vencimiento.Direction = ParameterDirection.Input;
                        pfecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_vencimiento);

                        DbParameter ptipo_interes = cmdTransaccionFactory.CreateParameter();
                        ptipo_interes.ParameterName = "p_Tipo_Interes";
                        ptipo_interes.Value = pCdat.tipo_interes;
                        ptipo_interes.Direction = ParameterDirection.Input;
                        ptipo_interes.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_interes);

                        DbParameter ptasa_interes = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes.ParameterName = "p_Tasa_Interes";
                        if (pCdat.tasa_interes != 0) ptasa_interes.Value = pCdat.tasa_interes; else ptasa_interes.Value = DBNull.Value;
                        ptasa_interes.Direction = ParameterDirection.Input;
                        ptasa_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes);

                        DbParameter pcod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa.ParameterName = "p_Cod_Tipo_Tasa";
                        if (pCdat.cod_tipo_tasa != 0) pcod_tipo_tasa.Value = pCdat.cod_tipo_tasa; else pcod_tipo_tasa.Value = DBNull.Value;
                        pcod_tipo_tasa.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_Tipo_Historico";
                        if (pCdat.tipo_historico != 0) ptipo_historico.Value = pCdat.tipo_historico; else ptipo_historico.Value = DBNull.Value;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_Desviacion";
                        if (pCdat.desviacion != 0) pdesviacion.Value = pCdat.desviacion; else pdesviacion.Value = DBNull.Value;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter pFecha_intereses = cmdTransaccionFactory.CreateParameter();
                        pFecha_intereses.ParameterName = "p_Fecha_intereses";
                        if (pCdat.fecha_intereses != DateTime.MinValue) pFecha_intereses.Value = pCdat.fecha_intereses; else pFecha_intereses.Value = DBNull.Value;
                        pFecha_intereses.Direction = ParameterDirection.Input;
                        pFecha_intereses.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pFecha_intereses);

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        p_cod_ope.Value = pCdat.cod_ope; 
                        p_cod_ope.Direction = ParameterDirection.Input;
                        p_cod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_DATOSPRORRO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProrrogaCDATData", "ModificarCDATProrroga", ex);
                        return null;
                    }
                }
            }
        }


        public ProrrogaCDAT CrearCDATProrroga(ProrrogaCDAT pProrro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_prorroga = cmdTransaccionFactory.CreateParameter();
                        pcod_prorroga.ParameterName = "p_cod_prorroga";
                        pcod_prorroga.Value = pProrro.cod_prorroga;
                        pcod_prorroga.Direction = ParameterDirection.Output;
                        pcod_prorroga.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_prorroga);

                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = pProrro.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_fecha_inicio";
                        pfecha_inicio.Value = pProrro.fecha_inicio;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "p_fecha_final";
                        pfecha_final.Value = pProrro.fecha_final;
                        pfecha_final.Direction = ParameterDirection.Input;
                        pfecha_final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);

                        DbParameter ptipo_interes = cmdTransaccionFactory.CreateParameter();
                        ptipo_interes.ParameterName = "p_tipo_interes";
                        ptipo_interes.Value = pProrro.tipo_interes;
                        ptipo_interes.Direction = ParameterDirection.Input;
                        ptipo_interes.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_interes);

                        DbParameter ptasa_interes = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes.ParameterName = "p_tasa_interes";
                        if (pProrro.tasa_interes != 0) ptasa_interes.Value = pProrro.tasa_interes; else ptasa_interes.Value = DBNull.Value;
                        ptasa_interes.Direction = ParameterDirection.Input;
                        ptasa_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes);

                        DbParameter pcod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        if (pProrro.cod_tipo_tasa != 0) pcod_tipo_tasa.Value = pProrro.cod_tipo_tasa; else pcod_tipo_tasa.Value = DBNull.Value;
                        pcod_tipo_tasa.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pProrro.tipo_historico != 0) ptipo_historico.Value = pProrro.tipo_historico; else ptipo_historico.Value = DBNull.Value;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pProrro.desviacion != 0) pdesviacion.Value = pProrro.desviacion; else pdesviacion.Value = DBNull.Value;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter pcod_periodicidad_int = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad_int.ParameterName = "p_cod_periodicidad_int";
                        pcod_periodicidad_int.Value = pProrro.cod_periodicidad_int;
                        pcod_periodicidad_int.Direction = ParameterDirection.Input;
                        pcod_periodicidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad_int);


                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        p_cod_ope.Value = pProrro.cod_ope;
                        p_cod_ope.Direction = ParameterDirection.Input;
                        p_cod_ope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_PRORROGA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pProrro.cod_prorroga = Convert.ToInt64(pcod_prorroga.Value);
                        return pProrro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProrrogaCDATData", "CrearCDATProrroga", ex);
                        return null;
                    }
                }
            }
        }


        public ProrrogaCDAT ModificarProrroga_CDAT(ProrrogaCDAT pProrro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = pProrro.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_fecha_inicio";
                        pfecha_inicio.Value = pProrro.fecha_inicio;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "p_fecha_final";
                        pfecha_final.Value = pProrro.fecha_final;
                        pfecha_final.Direction = ParameterDirection.Input;
                        pfecha_final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);

                        DbParameter ptipo_interes = cmdTransaccionFactory.CreateParameter();
                        ptipo_interes.ParameterName = "p_tipo_interes";
                        ptipo_interes.Value = pProrro.tipo_interes;
                        ptipo_interes.Direction = ParameterDirection.Input;
                        ptipo_interes.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_interes);

                        DbParameter ptasa_interes = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes.ParameterName = "p_tasa_interes";
                        if (pProrro.tasa_interes != 0) ptasa_interes.Value = pProrro.tasa_interes; else ptasa_interes.Value = DBNull.Value;
                        ptasa_interes.Direction = ParameterDirection.Input;
                        ptasa_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes);

                        DbParameter pcod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        if (pProrro.cod_tipo_tasa != 0) pcod_tipo_tasa.Value = pProrro.cod_tipo_tasa; else pcod_tipo_tasa.Value = DBNull.Value;
                        pcod_tipo_tasa.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pProrro.tipo_historico != 0) ptipo_historico.Value = pProrro.tipo_historico; else ptipo_historico.Value = DBNull.Value;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pProrro.desviacion != 0) pdesviacion.Value = pProrro.desviacion; else pdesviacion.Value = DBNull.Value;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter pcod_periodicidad_int = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad_int.ParameterName = "p_cod_periodicidad_int";
                        pcod_periodicidad_int.Value = pProrro.cod_periodicidad_int;
                        pcod_periodicidad_int.Direction = ParameterDirection.Input;
                        pcod_periodicidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad_int);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_PRORROGA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pProrro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProrrogaCDATData", "ModificarProrroga_CDAT", ex);
                        return null;
                    }
                }
            }
        }



        public ProrrogaCDAT ConsultarCDATProrroga(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ProrrogaCDAT entidad = new ProrrogaCDAT();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PRORROGA_CDAT WHERE CODIGO_CDAT = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_PRORROGA"] != DBNull.Value) entidad.cod_prorroga = Convert.ToInt64(resultado["COD_PRORROGA"]);
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt64(resultado["CODIGO_CDAT"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FINAL"]);
                            if (resultado["TIPO_INTERES"] != DBNull.Value) entidad.tipo_interes = Convert.ToString(resultado["TIPO_INTERES"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["COD_PERIODICIDAD_INT"] != DBNull.Value) entidad.cod_periodicidad_int = Convert.ToInt32(resultado["COD_PERIODICIDAD_INT"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProrrogaCDATData", "ConsultarCDATProrroga", ex);
                        return null;
                    }
                }
            }
        }

        //SOLICITADO DESDE ATENCION AL CLIENTE
        public SolicitudRenovacion CrearSolicitudRenovacion(SolicitudRenovacion pSolicitud, ref string pError, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidrenovacion = cmdTransaccionFactory.CreateParameter();
                        pidrenovacion.ParameterName = "p_idrenovacion";
                        pidrenovacion.Value = pSolicitud.idrenovacion;
                        pidrenovacion.Direction = ParameterDirection.Output;
                        pidrenovacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidrenovacion);

                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = pSolicitud.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter pfecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        pfecha_vencimiento.ParameterName = "p_fecha_vencimiento";
                        if (pSolicitud.fecha_vencimiento == null)
                            pfecha_vencimiento.Value = DBNull.Value;
                        else
                            pfecha_vencimiento.Value = pSolicitud.fecha_vencimiento;
                        pfecha_vencimiento.Direction = ParameterDirection.Input;
                        pfecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_vencimiento);

                        DbParameter pfecha_solicitud = cmdTransaccionFactory.CreateParameter();
                        pfecha_solicitud.ParameterName = "p_fecha_solicitud";
                        if (pSolicitud.fecha_solicitud == null)
                            pfecha_solicitud.Value = DBNull.Value;
                        else
                            pfecha_solicitud.Value = pSolicitud.fecha_solicitud;
                        pfecha_solicitud.Direction = ParameterDirection.Input;
                        pfecha_solicitud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_solicitud);

                        DbParameter pcod_lineacdat = cmdTransaccionFactory.CreateParameter();
                        pcod_lineacdat.ParameterName = "p_cod_lineacdat";
                        if (pSolicitud.cod_lineacdat == null)
                            pcod_lineacdat.Value = DBNull.Value;
                        else
                            pcod_lineacdat.Value = pSolicitud.cod_lineacdat;
                        pcod_lineacdat.Direction = ParameterDirection.Input;
                        pcod_lineacdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_lineacdat);

                        DbParameter pplazo = cmdTransaccionFactory.CreateParameter();
                        pplazo.ParameterName = "p_plazo";
                        if (pSolicitud.plazo == null)
                            pplazo.Value = DBNull.Value;
                        else
                            pplazo.Value = pSolicitud.plazo;
                        pplazo.Direction = ParameterDirection.Input;
                        pplazo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pplazo);

                        DbParameter pobservacion = cmdTransaccionFactory.CreateParameter();
                        pobservacion.ParameterName = "p_observacion";
                        if (pSolicitud.observacion == null)
                            pobservacion.Value = DBNull.Value;
                        else
                            pobservacion.Value = pSolicitud.observacion;
                        pobservacion.Direction = ParameterDirection.Input;
                        pobservacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservacion);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pSolicitud.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pSolicitud.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pmensaje_error = cmdTransaccionFactory.CreateParameter();
                        pmensaje_error.ParameterName = "p_mensaje_error";
                        pmensaje_error.Value = DBNull.Value;
                        pmensaje_error.Size = 8000;
                        pmensaje_error.DbType = DbType.String;
                        pmensaje_error.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pmensaje_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_RENOVARSOL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pSolicitud.mensaje_error = Convert.ToString(pmensaje_error.Value);
                        if (pSolicitud.mensaje_error != null)
                        {
                            pError = pSolicitud.mensaje_error;
                            return null;
                        }
                        pSolicitud.idrenovacion = Convert.ToInt64(pidrenovacion.Value);
                        return pSolicitud;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }



    }
}
