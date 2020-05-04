using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Data
{
    public class ParametrizacionProcesoAfiliacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PERFIL_USUARIO
        /// </summary>
        public ParametrizacionProcesoAfiliacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public ParametrizacionProcesoAfiliacion validarProcesoAnterior(string cod_per, Int32 cod_proceso, Usuario vUsuario)
        {
            DbDataReader resultado;
            ParametrizacionProcesoAfiliacion estados = new ParametrizacionProcesoAfiliacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT E.ESTADO AS ESTADO_PROCESO, F.ESTADO AS ESTADO_ASOCIADO
                                    FROM ESTADO_PERSONA E 
                                    INNER JOIN PERSONA_AFILIACION F ON F.COD_PERSONA = " + cod_per + "LEFT JOIN CONTROL_AFILIACION C ON C.COD_PERSONA= " + cod_per + " WHERE E.COD_PROCESO = " + cod_proceso + " OR C.COD_PROCESO = " + cod_proceso + " GROUP BY E.ESTADO, F.ESTADO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            ParametrizacionProcesoAfiliacion pProceso = new ParametrizacionProcesoAfiliacion();
                            if (resultado["ESTADO_PROCESO"] != DBNull.Value) estados.estado_proceso = Convert.ToString(resultado["ESTADO_PROCESO"]);
                            if (resultado["ESTADO_ASOCIADO"] != DBNull.Value) estados.estado_asociado = Convert.ToString(resultado["ESTADO_ASOCIADO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return estados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrizacionProcesoAfiliacionData", "validarProcesoAnterior", ex);
                        return null;
                    }
                }
            }
        }
        public bool controlRegistrado(Int32 cod_proceso, Int64 cod_per, Usuario vUsuario)
        {
            DbDataReader resultado;
            bool r = false;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COUNT(*) AS PASO FROM CONTROL_AFILIACION WHERE COD_PERSONA = " + cod_per + " AND COD_PROCESO = " + cod_proceso;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["PASO"] != DBNull.Value)
                            {
                                if (Convert.ToInt64(resultado["PASO"]) > 0)
                                    r = true;
                            };
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return r;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrizacionProcesoAfiliacionData", "controlRegistrado", ex);
                        return false;
                    }
                }
            }
        }
        public List<ParametrizacionProcesoAfiliacion> ListarParametrosProcesoAfiliacion(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ParametrizacionProcesoAfiliacion> lstParamAfi = new List<ParametrizacionProcesoAfiliacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PARAMETRIZACION_AFILICACION ORDER BY ORDEN";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ParametrizacionProcesoAfiliacion pProceso = new ParametrizacionProcesoAfiliacion();
                            if (resultado["COD_PROCESO"] != DBNull.Value) pProceso.cod_proceso = Convert.ToInt32(resultado["COD_PROCESO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) pProceso.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["REQUERIDO"] != DBNull.Value) pProceso.requerido = Convert.ToInt32(resultado["REQUERIDO"]);
                            if (resultado["ORDEN"] != DBNull.Value) pProceso.orden = Convert.ToInt32(resultado["ORDEN"]);
                            if (resultado["ASOCIADO"] != DBNull.Value) pProceso.asociado = Convert.ToInt32(resultado["ASOCIADO"]);
                            if (resultado["ASESOR"] != DBNull.Value) pProceso.asesor = Convert.ToInt32(resultado["ASESOR"]);
                            if (resultado["OTRO"] != DBNull.Value) pProceso.otro = Convert.ToString(resultado["OTRO"]);
                            lstParamAfi.Add(pProceso);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstParamAfi;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrizacionProcesoAfiliacionData", "ListarParametrosProcesoAfiliacion", ex);
                        return null;
                    }
                }
            }
        }
        public List<ParametrizacionProcesoAfiliacion> ListarDetalleRuta(string iden, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ParametrizacionProcesoAfiliacion> lstParamAfi = new List<ParametrizacionProcesoAfiliacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.FECHA, CASE WHEN N.NOMBRE IS NULL THEN 'ACTIVO' ELSE E.DESCRIPCION END AS ESTADO, R.NOMBRE AS PROCESO_ANTERIOR, 
                                    CASE WHEN N.NOMBRE IS NULL THEN 'Asociado final' ELSE N.NOMBRE END AS PROCESO_SIGUIENTE FROM PERSONA P
                                    INNER JOIN CONTROL_AFILIACION C ON P.COD_PERSONA = C.COD_PERSONA
                                    INNER JOIN ESTADO_PERSONA E ON P.ESTADO = E.ESTADO
                                    INNER JOIN PARAMETRIZACION_AFILICACION R ON C.COD_PROCESO = R.COD_PROCESO
                                    LEFT JOIN PARAMETRIZACION_AFILICACION N ON N.ORDEN = R.ORDEN + 1
                                    LEFT JOIN PERSONA_AFILIACION F ON P.COD_PERSONA = F.COD_PERSONA
                                    WHERE P.IDENTIFICACION = " + iden;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ParametrizacionProcesoAfiliacion pProceso = new ParametrizacionProcesoAfiliacion();
                            if (resultado["FECHA"] != DBNull.Value) pProceso.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["ESTADO"] != DBNull.Value) pProceso.estado_asociado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["PROCESO_ANTERIOR"] != DBNull.Value) pProceso.antPaso = Convert.ToString(resultado["PROCESO_ANTERIOR"]);
                            if (resultado["PROCESO_SIGUIENTE"] != DBNull.Value) pProceso.sigPaso = Convert.ToString(resultado["PROCESO_SIGUIENTE"]);
                            lstParamAfi.Add(pProceso);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstParamAfi;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrizacionProcesoAfiliacionData", "ListarDetalleRuta", ex);
                        return null;
                    }
                }
            }
        }
        public bool solicitud(string iden, Usuario vUsuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        bool r = false;
                        string sql = @"SELECT IDENTIFICACION FROM SOLICITUD_PERSONA_AFI WHERE IDENTIFICACION = " + iden + " AND ESTADO = 'S'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["IDENTIFICACION"] != DBNull.Value)
                                r = true;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return r;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrizacionProcesoAfiliacionData", "solicitud", ex);
                        return false;
                    }
                }
            }
        }
        public List<ParametrizacionProcesoAfiliacion> ListarHistorialRuta(string iden, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ParametrizacionProcesoAfiliacion> lstParamAfi = new List<ParametrizacionProcesoAfiliacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string table = "PERSONA";
                        bool sol = solicitud(iden, vUsuario);
                        if (sol)
                            table = "SOLICITUD_PERSONA_AFI";
                        string sql = @"SELECT P.IDENTIFICACION, TI.DESCRIPCION AS TIPO_IDEN, P.PRIMER_NOMBRE ||' '|| P.SEGUNDO_NOMBRE||' '|| P.PRIMER_APELLIDO||' '|| P.SEGUNDO_APELLIDO AS NOMBRE,
                                        CASE WHEN N.NOMBRE IS NULL THEN 'ACTIVO' ELSE E.DESCRIPCION END AS ESTADO, R.NOMBRE AS PROCESO_ANTERIOR, 
                                        CASE WHEN N.NOMBRE IS NULL THEN 'Asociado final' ELSE N.NOMBRE END AS PROCESO_SIGUIENTE FROM " + table + " P ";

                        if (sol)
                            sql += @"INNER JOIN CONTROL_AFILIACION C ON P.IDENTIFICACION = C.IDENTIFICACION ";
                        else
                            sql += @"INNER JOIN CONTROL_AFILIACION C ON P.COD_PERSONA = C.COD_PERSONA ";

                        sql += @"INNER JOIN PARAMETRIZACION_AFILICACION R ON C.COD_PROCESO = R.COD_PROCESO
                                LEFT JOIN PARAMETRIZACION_AFILICACION N ON N.ORDEN = R.ORDEN + 1";
                        if (!sol)
                            sql += @" LEFT JOIN PERSONA_AFILIACION F ON P.COD_PERSONA = F.COD_PERSONA";

                        sql += @" LEFT JOIN ESTADO_PERSONA E ON P.ESTADO = E.ESTADO
                                 LEFT JOIN TIPOIDENTIFICACION TI ON P.TIPO_IDENTIFICACION = TI.CODTIPOIDENTIFICACION
                                 WHERE P.IDENTIFICACION =  " + iden + " ORDER BY C.FECHA DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ParametrizacionProcesoAfiliacion pProceso = new ParametrizacionProcesoAfiliacion();
                            if (resultado["TIPO_IDEN"] != DBNull.Value) pProceso.tipo_iden = Convert.ToString(resultado["TIPO_IDEN"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) pProceso.identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) pProceso.nombre_asociado = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["ESTADO"] != DBNull.Value) pProceso.estado_asociado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["PROCESO_ANTERIOR"] != DBNull.Value) pProceso.antPaso = Convert.ToString(resultado["PROCESO_ANTERIOR"]);
                            if (resultado["PROCESO_SIGUIENTE"] != DBNull.Value) pProceso.sigPaso = Convert.ToString(resultado["PROCESO_SIGUIENTE"]);
                            lstParamAfi.Add(pProceso);
                            break;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstParamAfi;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrizacionProcesoAfiliacionData", "ListarHistorialRuta", ex);
                        return null;
                    }
                }
            }
        }
        public void cambiarEstadoAsociado(string estado, Int64 cod_per, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_per = cmdTransaccionFactory.CreateParameter();
                        p_cod_per.ParameterName = "P_COD_PERSONA";
                        p_cod_per.Value = Convert.ToInt32(cod_per);
                        p_cod_per.Direction = ParameterDirection.Input;
                        p_cod_per.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_per);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "P_ESTADO";
                        p_estado.Value = estado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PROCESO_ESTADO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrizacionProcesoAfilicacionService", "cambiarEstadoAsociado", ex);
                    }
                }
            }
        }
        public ParametrizacionProcesoAfiliacion ModificarParametros(ParametrizacionProcesoAfiliacion eachParam, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string p_cod_proceso = "p_cod_proceso"+eachParam.cod_proceso;
                        DbParameter p_cod_proceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_proceso.ParameterName = "P_COD_PROCESO";
                        p_cod_proceso.Value = eachParam.cod_proceso;
                        p_cod_proceso.Direction = ParameterDirection.Input;
                        p_cod_proceso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_proceso);

                        DbParameter p_requerido = cmdTransaccionFactory.CreateParameter();
                        p_requerido.ParameterName = "P_REQUERIDO";
                        p_requerido.Value = eachParam.requerido;
                        p_requerido.Direction = ParameterDirection.Input;
                        p_requerido.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_requerido);

                        DbParameter p_orden = cmdTransaccionFactory.CreateParameter();
                        p_orden.ParameterName = "P_ORDEN";
                        p_orden.Value = eachParam.orden;
                        p_orden.Direction = ParameterDirection.Input;
                        p_orden.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_orden);

                        DbParameter p_asociado = cmdTransaccionFactory.CreateParameter();
                        p_asociado.ParameterName = "P_ASOCIADO";
                        p_asociado.Value = eachParam.asociado;
                        p_asociado.Direction = ParameterDirection.Input;
                        p_asociado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_asociado);

                        DbParameter p_asesor = cmdTransaccionFactory.CreateParameter();
                        p_asesor.ParameterName = "P_ASESOR";
                        p_asesor.Value = eachParam.asesor;
                        p_asesor.Direction = ParameterDirection.Input;
                        p_asesor.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_asesor);

                        DbParameter p_otro = cmdTransaccionFactory.CreateParameter();
                        p_otro.ParameterName = "P_OTRO";
                        p_otro.Value = eachParam.otro;
                        p_otro.Direction = ParameterDirection.Input;
                        p_otro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_otro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PROCESO_AFILIA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return eachParam;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrizacionProcesoAfiliacionData", "ModificarParametros", ex);
                        return null;
                    }
                }
            }
        }


    }
}
