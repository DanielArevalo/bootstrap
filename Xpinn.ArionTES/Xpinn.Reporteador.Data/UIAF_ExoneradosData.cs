
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Reporteador.Entities;

namespace Xpinn.Reporteador.Data
{
    public class UIAF_ExoneradosData : GlobalData
    {
 
        protected ConnectionDataBase dbConnectionFactory;

        public UIAF_ExoneradosData()
        {
        dbConnectionFactory = new ConnectionDataBase();
        }

        public UIAF_Exonerados CrearUIAF_Exonerados(UIAF_Exonerados pUIAF_Exonerados, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidexonerado = cmdTransaccionFactory.CreateParameter();
                        pidexonerado.ParameterName = "p_idexonerado";
                        pidexonerado.Value = pUIAF_Exonerados.idexonerado;
                        pidexonerado.Direction = ParameterDirection.Output;
                        pidexonerado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidexonerado);
                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();

                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pUIAF_Exonerados.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pUIAF_Exonerados.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pfecha_exoneracion = cmdTransaccionFactory.CreateParameter();
                        pfecha_exoneracion.ParameterName = "p_fecha_exoneracion";
                        if (pUIAF_Exonerados.fecha_exoneracion == null)
                            pfecha_exoneracion.Value = DBNull.Value;
                        else
                            pfecha_exoneracion.Value = pUIAF_Exonerados.fecha_exoneracion;
                        pfecha_exoneracion.Direction = ParameterDirection.Input;
                        pfecha_exoneracion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_exoneracion);

                        DbParameter ptipo_identificacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_identificacion.ParameterName = "p_tipo_identificacion";
                        if (pUIAF_Exonerados.tipo_identificacion == null)
                            ptipo_identificacion.Value = DBNull.Value;
                        else
                            ptipo_identificacion.Value = pUIAF_Exonerados.tipo_identificacion;
                        ptipo_identificacion.Direction = ParameterDirection.Input;
                        ptipo_identificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_identificacion);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pUIAF_Exonerados.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pUIAF_Exonerados.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pprimer_apellido = cmdTransaccionFactory.CreateParameter();
                        pprimer_apellido.ParameterName = "p_primer_apellido";
                        if (pUIAF_Exonerados.primer_apellido == null)
                            pprimer_apellido.Value = DBNull.Value;
                        else
                            pprimer_apellido.Value = pUIAF_Exonerados.primer_apellido;
                        pprimer_apellido.Direction = ParameterDirection.Input;
                        pprimer_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_apellido);

                        DbParameter psegundo_apellido = cmdTransaccionFactory.CreateParameter();
                        psegundo_apellido.ParameterName = "p_segundo_apellido";
                        if (pUIAF_Exonerados.segundo_apellido == null)
                            psegundo_apellido.Value = DBNull.Value;
                        else
                            psegundo_apellido.Value = pUIAF_Exonerados.segundo_apellido;
                        psegundo_apellido.Direction = ParameterDirection.Input;
                        psegundo_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_apellido);

                        DbParameter pprimer_nombre = cmdTransaccionFactory.CreateParameter();
                        pprimer_nombre.ParameterName = "p_primer_nombre";
                        if (pUIAF_Exonerados.primer_nombre == null)
                            pprimer_nombre.Value = DBNull.Value;
                        else
                            pprimer_nombre.Value = pUIAF_Exonerados.primer_nombre;
                        pprimer_nombre.Direction = ParameterDirection.Input;
                        pprimer_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_nombre);

                        DbParameter psegundo_nombre = cmdTransaccionFactory.CreateParameter();
                        psegundo_nombre.ParameterName = "p_segundo_nombre";
                        if (pUIAF_Exonerados.segundo_nombre == null)
                            psegundo_nombre.Value = DBNull.Value;
                        else
                            psegundo_nombre.Value = pUIAF_Exonerados.segundo_nombre;
                        psegundo_nombre.Direction = ParameterDirection.Input;
                        psegundo_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_nombre);

                        DbParameter prazon_social = cmdTransaccionFactory.CreateParameter();
                        prazon_social.ParameterName = "p_razon_social";
                        if (pUIAF_Exonerados.razon_social == null)
                            prazon_social.Value = DBNull.Value;
                        else
                            prazon_social.Value = pUIAF_Exonerados.razon_social;
                        prazon_social.Direction = ParameterDirection.Input;
                        prazon_social.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(prazon_social);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TRA_UIAF_EXONE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        pUIAF_Exonerados.idexonerado = Convert.ToInt64(pidexonerado.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pUIAF_Exonerados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAF_ExoneradosData", "CrearUIAF_Exonerados", ex);
                        return null;
                    }
                }
            }
        }


        public UIAF_Exonerados ModificarUIAF_Exonerados(UIAF_Exonerados pUIAF_Exonerados, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidexonerado = cmdTransaccionFactory.CreateParameter();
                        pidexonerado.ParameterName = "p_idexonerado";
                        pidexonerado.Value = pUIAF_Exonerados.idexonerado;
                        pidexonerado.Direction = ParameterDirection.Input;
                        pidexonerado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidexonerado);

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pUIAF_Exonerados.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pUIAF_Exonerados.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pfecha_exoneracion = cmdTransaccionFactory.CreateParameter();
                        pfecha_exoneracion.ParameterName = "p_fecha_exoneracion";
                        if (pUIAF_Exonerados.fecha_exoneracion == null)
                            pfecha_exoneracion.Value = DBNull.Value;
                        else
                            pfecha_exoneracion.Value = pUIAF_Exonerados.fecha_exoneracion;
                        pfecha_exoneracion.Direction = ParameterDirection.Input;
                        pfecha_exoneracion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_exoneracion);

                        DbParameter ptipo_identificacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_identificacion.ParameterName = "p_tipo_identificacion";
                        if (pUIAF_Exonerados.tipo_identificacion == null)
                            ptipo_identificacion.Value = DBNull.Value;
                        else
                            ptipo_identificacion.Value = pUIAF_Exonerados.tipo_identificacion;
                        ptipo_identificacion.Direction = ParameterDirection.Input;
                        ptipo_identificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_identificacion);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pUIAF_Exonerados.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pUIAF_Exonerados.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pprimer_apellido = cmdTransaccionFactory.CreateParameter();
                        pprimer_apellido.ParameterName = "p_primer_apellido";
                        if (pUIAF_Exonerados.primer_apellido == null)
                            pprimer_apellido.Value = DBNull.Value;
                        else
                            pprimer_apellido.Value = pUIAF_Exonerados.primer_apellido;
                        pprimer_apellido.Direction = ParameterDirection.Input;
                        pprimer_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_apellido);

                        DbParameter psegundo_apellido = cmdTransaccionFactory.CreateParameter();
                        psegundo_apellido.ParameterName = "p_segundo_apellido";
                        if (pUIAF_Exonerados.segundo_apellido == null)
                            psegundo_apellido.Value = DBNull.Value;
                        else
                            psegundo_apellido.Value = pUIAF_Exonerados.segundo_apellido;
                        psegundo_apellido.Direction = ParameterDirection.Input;
                        psegundo_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_apellido);

                        DbParameter pprimer_nombre = cmdTransaccionFactory.CreateParameter();
                        pprimer_nombre.ParameterName = "p_primer_nombre";
                        if (pUIAF_Exonerados.primer_nombre == null)
                            pprimer_nombre.Value = DBNull.Value;
                        else
                            pprimer_nombre.Value = pUIAF_Exonerados.primer_nombre;
                        pprimer_nombre.Direction = ParameterDirection.Input;
                        pprimer_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_nombre);

                        DbParameter psegundo_nombre = cmdTransaccionFactory.CreateParameter();
                        psegundo_nombre.ParameterName = "p_segundo_nombre";
                        if (pUIAF_Exonerados.segundo_nombre == null)
                            psegundo_nombre.Value = DBNull.Value;
                        else
                            psegundo_nombre.Value = pUIAF_Exonerados.segundo_nombre;
                        psegundo_nombre.Direction = ParameterDirection.Input;
                        psegundo_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_nombre);

                        DbParameter prazon_social = cmdTransaccionFactory.CreateParameter();
                        prazon_social.ParameterName = "p_razon_social";
                        if (pUIAF_Exonerados.razon_social == null)
                            prazon_social.Value = DBNull.Value;
                        else
                            prazon_social.Value = pUIAF_Exonerados.razon_social;
                        prazon_social.Direction = ParameterDirection.Input;
                        prazon_social.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(prazon_social);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TRA_UIAF_EXONE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pUIAF_Exonerados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAF_ExoneradosData", "ModificarUIAF_Exonerados", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarUIAF_Exonerados(String pFiltro, Int64 pIdreporte,  Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                
                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pIdreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter pidexonerado = cmdTransaccionFactory.CreateParameter();
                        pidexonerado.ParameterName = "p_idexonerados";
                        pidexonerado.Value = pFiltro;
                        pidexonerado.Direction = ParameterDirection.Input;
                        pidexonerado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidexonerado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TRA_UIAF_EXONE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAF_ExoneradosData", "EliminarUIAF_Exonerados", ex);
                    }
                }
            }
        }


        public UIAF_Exonerados ConsultarUIAF_Exonerados(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            UIAF_Exonerados entidad = new UIAF_Exonerados();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM UIAF_EXONERADOS WHERE IDEXONERADO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDEXONERADO"] != DBNull.Value) entidad.idexonerado = Convert.ToInt32(resultado["IDEXONERADO"]);
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt32(resultado["IDREPORTE"]);
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["FECHA_EXONERACION"] != DBNull.Value) entidad.fecha_exoneracion = Convert.ToDateTime(resultado["FECHA_EXONERACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
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
                        BOExcepcion.Throw("UIAF_ExoneradosData", "ConsultarUIAF_Exonerados", ex);
                        return null;
                    }
                }
            }
        }


        public List<UIAF_Exonerados> ListarUIAF_Exonerados(Int64 pIdreporte, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<UIAF_Exonerados> lstUIAF_Exonerados = new List<UIAF_Exonerados>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM UIAF_EXONERADOS WHERE IDREPORTE="+ pIdreporte;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            UIAF_Exonerados entidad = new UIAF_Exonerados();
                            if (resultado["IDEXONERADO"] != DBNull.Value) entidad.idexonerado = Convert.ToInt32(resultado["IDEXONERADO"]);
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt32(resultado["IDREPORTE"]);
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["FECHA_EXONERACION"] != DBNull.Value) entidad.fecha_exoneracion = Convert.ToDateTime(resultado["FECHA_EXONERACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            lstUIAF_Exonerados.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstUIAF_Exonerados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAF_ExoneradosData", "ListarUIAF_Exonerados", ex);
                        return null;
                    }
                }
            }
        }

        public List<UIAF_Exonerados> ListarUIAF_ExoneradosDate(DateTime pFechaCorte, Usuario vUsuario)
        {
            DbDataReader resultado;
            Configuracion conf = new Configuracion();
            List<UIAF_Exonerados> lstUIAF_Exonerados = new List<UIAF_Exonerados>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DateTime pFechaInicio = pFechaCorte;
                        if (pFechaInicio.Month <= 2)
                        {
                            pFechaInicio = new DateTime(pFechaInicio.Year-1, 12+(pFechaInicio.Month-2), 1);
                        }
                        else
                        {
                            pFechaInicio = new DateTime(pFechaInicio.Year, pFechaInicio.Month-2, 1);
                        }
                        string sql = @"SELECT * FROM UIAF_EXONERADOS WHERE FECHA_EXONERACION BETWEEN to_date('"+ pFechaInicio.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And to_date('" + pFechaCorte.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            UIAF_Exonerados entidad = new UIAF_Exonerados();
                            if (resultado["IDEXONERADO"] != DBNull.Value) entidad.idexonerado = Convert.ToInt32(resultado["IDEXONERADO"]);
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt32(resultado["IDREPORTE"]);
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["FECHA_EXONERACION"] != DBNull.Value) entidad.fecha_exoneracion = Convert.ToDateTime(resultado["FECHA_EXONERACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            lstUIAF_Exonerados.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstUIAF_Exonerados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UIAF_ExoneradosData", "ListarUIAF_Exonerados", ex);
                        return null;
                    }
                }
            }
        }

    }   
}