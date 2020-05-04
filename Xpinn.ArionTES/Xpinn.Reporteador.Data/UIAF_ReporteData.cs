using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Reporteador.Entities;

namespace Xpinn.Reporteador.Data
{
    public class UIAF_ReporteData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public UIAF_ReporteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public UIAF_Reporte CrearUIAF_Reporte(UIAF_Reporte pUIAF_Reporte, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pUIAF_Reporte.idreporte;
                        pidreporte.Direction = ParameterDirection.Output;
                        pidreporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter pidformato = cmdTransaccionFactory.CreateParameter();
                        pidformato.ParameterName = "p_idformato";
                        if (pUIAF_Reporte.idformato == null)
                            pidformato.Value = DBNull.Value;
                        else
                            pidformato.Value = pUIAF_Reporte.idformato;
                        pidformato.Direction = ParameterDirection.Input;
                        pidformato.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidformato);

                        DbParameter pfecha_generacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_generacion.ParameterName = "p_fecha_generacion";
                        if (pUIAF_Reporte.fecha_generacion == null)
                            pfecha_generacion.Value = DBNull.Value;
                        else
                            pfecha_generacion.Value = pUIAF_Reporte.fecha_generacion;
                        pfecha_generacion.Direction = ParameterDirection.Input;
                        pfecha_generacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_generacion);

                        DbParameter pnumero_registros = cmdTransaccionFactory.CreateParameter();
                        pnumero_registros.ParameterName = "p_numero_registros";
                        if (pUIAF_Reporte.numero_registros == null)
                            pnumero_registros.Value = DBNull.Value;
                        else
                            pnumero_registros.Value = pUIAF_Reporte.numero_registros;
                        pnumero_registros.Direction = ParameterDirection.Input;
                        pnumero_registros.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_registros);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        pcodusuario.Value = pUIAF_Reporte.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "p_fecha_inicial";
                        if (pUIAF_Reporte.fecha_inicial == null)
                            pfecha_inicial.Value = DBNull.Value;
                        else
                            pfecha_inicial.Value = pUIAF_Reporte.fecha_inicial;
                        pfecha_inicial.Direction = ParameterDirection.Input;
                        pfecha_inicial.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "p_fecha_final";
                        if (pUIAF_Reporte.fecha_final == null)
                            pfecha_final.Value = DBNull.Value;
                        else
                            pfecha_final.Value = pUIAF_Reporte.fecha_final;
                        pfecha_final.Direction = ParameterDirection.Input;
                        pfecha_final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_UIAF_REPORT_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        pUIAF_Reporte.idreporte = Convert.ToInt64(pidreporte.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pUIAF_Reporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAF_ReporteData", "CrearUIAF_Reporte", ex);
                        return null;
                    }
                }
            }
        }


        public UIAF_Reporte ModificarUIAF_Reporte(UIAF_Reporte pUIAF_Reporte, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pUIAF_Reporte.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter pidformato = cmdTransaccionFactory.CreateParameter();
                        pidformato.ParameterName = "p_idformato";
                        if (pUIAF_Reporte.idformato == null)
                            pidformato.Value = DBNull.Value;
                        else
                            pidformato.Value = pUIAF_Reporte.idformato;
                        pidformato.Direction = ParameterDirection.Input;
                        pidformato.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidformato);

                        DbParameter pfecha_generacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_generacion.ParameterName = "p_fecha_generacion";
                        if (pUIAF_Reporte.fecha_generacion == null)
                            pfecha_generacion.Value = DBNull.Value;
                        else
                            pfecha_generacion.Value = pUIAF_Reporte.fecha_generacion;
                        pfecha_generacion.Direction = ParameterDirection.Input;
                        pfecha_generacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_generacion);

                        DbParameter pnumero_registros = cmdTransaccionFactory.CreateParameter();
                        pnumero_registros.ParameterName = "p_numero_registros";
                        if (pUIAF_Reporte.numero_registros == null)
                            pnumero_registros.Value = DBNull.Value;
                        else
                            pnumero_registros.Value = pUIAF_Reporte.numero_registros;
                        pnumero_registros.Direction = ParameterDirection.Input;
                        pnumero_registros.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_registros);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        pcodusuario.Value = pUIAF_Reporte.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "p_fecha_inicial";
                        if (pUIAF_Reporte.fecha_inicial == null)
                            pfecha_inicial.Value = DBNull.Value;
                        else
                            pfecha_inicial.Value = pUIAF_Reporte.fecha_inicial;
                        pfecha_inicial.Direction = ParameterDirection.Input;
                        pfecha_inicial.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "p_fecha_final";
                        if (pUIAF_Reporte.fecha_final == null)
                            pfecha_final.Value = DBNull.Value;
                        else
                            pfecha_final.Value = pUIAF_Reporte.fecha_final;
                        pfecha_final.Direction = ParameterDirection.Input;
                        pfecha_final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_UIAF_REPORT_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pUIAF_Reporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAF_ReporteData", "ModificarUIAF_Reporte", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarUIAF_Reporte(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        UIAF_Reporte pUIAF_Reporte = new UIAF_Reporte();
                        

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pUIAF_Reporte.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_UIAF_REPOR_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAF_ReporteData", "EliminarUIAF_Reporte", ex);
                    }
                }
            }
        }


        public UIAF_Reporte ConsultarUIAF_Reporte(DateTime pFecha, Usuario vUsuario)
        {
            DbDataReader resultado;
            UIAF_Reporte entidad = new UIAF_Reporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT * FROM UIAF_REPORTE WHERE FECHA_GENERACION =to_date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt32(resultado["IDREPORTE"]);
                            if (resultado["IDFORMATO"] != DBNull.Value) entidad.idformato = Convert.ToString(resultado["IDFORMATO"]);
                            if (resultado["FECHA_GENERACION"] != DBNull.Value) entidad.fecha_generacion = Convert.ToDateTime(resultado["FECHA_GENERACION"]);
                            if (resultado["NUMERO_REGISTROS"] != DBNull.Value) entidad.numero_registros = Convert.ToInt32(resultado["NUMERO_REGISTROS"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt32(resultado["CODUSUARIO"]);
                            if (resultado["FECHA_INICIAL"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["FECHA_INICIAL"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FINAL"]);
                        }
                        else
                        {
                            return null;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAF_ReporteData", "ConsultarUIAF_Reporte", ex);
                        return null;
                    }
                }
            }
        }


        public List<UIAF_Reporte> ListarUIAF_Reporte(UIAF_Reporte pUIAF_Reporte, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<UIAF_Reporte> lstUIAF_Reporte = new List<UIAF_Reporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM UIAF_REPORTE " + ObtenerFiltro(pUIAF_Reporte) + " ORDER BY IDREPORTE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            UIAF_Reporte entidad = new UIAF_Reporte();
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt32(resultado["IDREPORTE"]);
                            if (resultado["IDFORMATO"] != DBNull.Value) entidad.idformato = Convert.ToString(resultado["IDFORMATO"]);
                            if (resultado["FECHA_GENERACION"] != DBNull.Value) entidad.fecha_generacion = Convert.ToDateTime(resultado["FECHA_GENERACION"]);
                            if (resultado["NUMERO_REGISTROS"] != DBNull.Value) entidad.numero_registros = Convert.ToInt32(resultado["NUMERO_REGISTROS"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt32(resultado["CODUSUARIO"]);
                            if (resultado["FECHA_INICIAL"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["FECHA_INICIAL"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FINAL"]);
                            lstUIAF_Reporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstUIAF_Reporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAF_ReporteData", "ListarUIAF_Reporte", ex);
                        return null;
                    }
                }
            }
        }


    }
}