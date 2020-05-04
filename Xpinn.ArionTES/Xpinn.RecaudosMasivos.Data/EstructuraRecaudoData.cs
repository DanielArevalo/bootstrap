using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;
using System.Web;

namespace Xpinn.Tesoreria.Data
{
    public class EstructuraRecaudoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public EstructuraRecaudoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public Estructura_Carga CrearEstructuraCarga(Estructura_Carga pEstructura, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_estructura_carga = cmdTransaccionFactory.CreateParameter();
                        pcod_estructura_carga.ParameterName = "p_cod_estructura_carga";
                        pcod_estructura_carga.Value = pEstructura.cod_estructura_carga;
                        pcod_estructura_carga.Direction = ParameterDirection.Output;
                        pcod_estructura_carga.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_estructura_carga);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pEstructura.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptipo_archivo = cmdTransaccionFactory.CreateParameter();
                        ptipo_archivo.ParameterName = "p_tipo_archivo";
                        ptipo_archivo.Value = pEstructura.tipo_archivo;
                        ptipo_archivo.Direction = ParameterDirection.Input;
                        ptipo_archivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_archivo);

                        DbParameter ptipo_datos = cmdTransaccionFactory.CreateParameter();
                        ptipo_datos.ParameterName = "p_tipo_datos";
                        if (pEstructura.tipo_datos != -1) ptipo_datos.Value = pEstructura.tipo_datos; else ptipo_datos.Value = DBNull.Value;
                        ptipo_datos.Direction = ParameterDirection.Input;
                        ptipo_datos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_datos);

                        DbParameter pseparador_campo = cmdTransaccionFactory.CreateParameter();
                        pseparador_campo.ParameterName = "p_separador_campo";
                        if (pEstructura.separador_campo != null) pseparador_campo.Value = pEstructura.separador_campo; else pseparador_campo.Value = DBNull.Value;
                        pseparador_campo.Direction = ParameterDirection.Input;
                        pseparador_campo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador_campo);

                        DbParameter pencabezado = cmdTransaccionFactory.CreateParameter();
                        pencabezado.ParameterName = "p_encabezado";
                        if (pEstructura.encabezado != 0) pencabezado.Value = pEstructura.encabezado; else pencabezado.Value = DBNull.Value;
                        pencabezado.Direction = ParameterDirection.Input;
                        pencabezado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pencabezado);

                        DbParameter pfinal = cmdTransaccionFactory.CreateParameter();
                        pfinal.ParameterName = "p_final";
                        if (pEstructura.final != 0) pfinal.Value = pEstructura.final; else pfinal.Value = DBNull.Value;
                        pfinal.Direction = ParameterDirection.Input;
                        pfinal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pfinal);

                        DbParameter pformato_fecha = cmdTransaccionFactory.CreateParameter();
                        pformato_fecha.ParameterName = "p_formato_fecha";
                        if (pEstructura.formato_fecha != null) pformato_fecha.Value = pEstructura.formato_fecha; else pformato_fecha.Value = DBNull.Value;
                        pformato_fecha.Direction = ParameterDirection.Input;
                        pformato_fecha.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pformato_fecha);

                        DbParameter pseparador_decimal = cmdTransaccionFactory.CreateParameter();
                        pseparador_decimal.ParameterName = "p_separador_decimal";
                        if (pEstructura.separador_decimal != null) pseparador_decimal.Value = pEstructura.separador_decimal; else pseparador_decimal.Value = DBNull.Value;
                        pseparador_decimal.Direction = ParameterDirection.Input;
                        pseparador_decimal.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador_decimal);

                        DbParameter pseparador_miles = cmdTransaccionFactory.CreateParameter();
                        pseparador_miles.ParameterName = "p_separador_miles";
                        pseparador_miles.Value = pEstructura.separador_miles;
                        pseparador_miles.Direction = ParameterDirection.Input;
                        pseparador_miles.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador_miles);

                        DbParameter ptotalizar = cmdTransaccionFactory.CreateParameter();
                        ptotalizar.ParameterName = "p_totalizar";
                        if (pEstructura.totalizar == null)
                            ptotalizar.Value = DBNull.Value;
                        else
                            ptotalizar.Value = pEstructura.totalizar;
                        ptotalizar.Direction = ParameterDirection.Input;
                        ptotalizar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptotalizar);

                        DbParameter pcod_estructura = cmdTransaccionFactory.CreateParameter();
                        pcod_estructura.ParameterName = "p_cod_estructura";
                        if (pEstructura.cod_estructura == null)
                            pcod_estructura.Value = DBNull.Value;
                        else
                            pcod_estructura.Value = pEstructura.cod_estructura;
                        pcod_estructura.Direction = ParameterDirection.Input;
                        pcod_estructura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_estructura);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_ESTRUCTURA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pEstructura.cod_estructura_carga = Convert.ToInt32(pcod_estructura_carga.Value);
                        return pEstructura;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraRecaudoData", "CrearEstructuraCarga", ex);
                        return null;
                    }
                }
            }
        }

        public Estructura_Carga ModificarEstructuraCarga(Estructura_Carga pEstructura, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_estructura_carga = cmdTransaccionFactory.CreateParameter();
                        pcod_estructura_carga.ParameterName = "p_cod_estructura_carga";
                        pcod_estructura_carga.Value = pEstructura.cod_estructura_carga;
                        pcod_estructura_carga.Direction = ParameterDirection.Input;
                        pcod_estructura_carga.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_estructura_carga);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pEstructura.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptipo_archivo = cmdTransaccionFactory.CreateParameter();
                        ptipo_archivo.ParameterName = "p_tipo_archivo";
                        ptipo_archivo.Value = pEstructura.tipo_archivo;
                        ptipo_archivo.Direction = ParameterDirection.Input;
                        ptipo_archivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_archivo);

                        DbParameter ptipo_datos = cmdTransaccionFactory.CreateParameter();
                        ptipo_datos.ParameterName = "p_tipo_datos";
                        if (pEstructura.tipo_datos != -1)
                            ptipo_datos.Value = pEstructura.tipo_datos;
                        else
                            ptipo_datos.Value = DBNull.Value;
                        ptipo_datos.Direction = ParameterDirection.Input;
                        ptipo_datos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_datos);

                        DbParameter pseparador_campo = cmdTransaccionFactory.CreateParameter();
                        pseparador_campo.ParameterName = "p_separador_campo";
                        if (pEstructura.separador_campo != null)
                            pseparador_campo.Value = pEstructura.separador_campo;
                        else
                            pseparador_campo.Value = DBNull.Value;
                        pseparador_campo.Direction = ParameterDirection.Input;
                        pseparador_campo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador_campo);

                        DbParameter pencabezado = cmdTransaccionFactory.CreateParameter();
                        pencabezado.ParameterName = "p_encabezado";
                        if (pEstructura.encabezado != 0)
                            pencabezado.Value = pEstructura.encabezado;
                        else
                            pencabezado.Value = DBNull.Value;
                        pencabezado.Direction = ParameterDirection.Input;
                        pencabezado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pencabezado);

                        DbParameter pfinal = cmdTransaccionFactory.CreateParameter();
                        pfinal.ParameterName = "p_final";
                        if (pEstructura.final != 0)
                            pfinal.Value = pEstructura.final;
                        else
                            pfinal.Value = DBNull.Value;
                        pfinal.Direction = ParameterDirection.Input;
                        pfinal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pfinal);

                        DbParameter pformato_fecha = cmdTransaccionFactory.CreateParameter();
                        pformato_fecha.ParameterName = "p_formato_fecha";
                        if (pEstructura.formato_fecha != null)
                            pformato_fecha.Value = pEstructura.formato_fecha;
                        else
                            pformato_fecha.Value = DBNull.Value;
                        pformato_fecha.Direction = ParameterDirection.Input;
                        pformato_fecha.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pformato_fecha);

                        DbParameter pseparador_decimal = cmdTransaccionFactory.CreateParameter();
                        pseparador_decimal.ParameterName = "p_separador_decimal";
                        if (pEstructura.separador_decimal != null)
                            pseparador_decimal.Value = pEstructura.separador_decimal;
                        else
                            pseparador_decimal.Value = DBNull.Value;
                        pseparador_decimal.Direction = ParameterDirection.Input;
                        pseparador_decimal.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador_decimal);

                        DbParameter pseparador_miles = cmdTransaccionFactory.CreateParameter();
                        pseparador_miles.ParameterName = "p_separador_miles";
                        if (pEstructura.separador_miles != null)
                            pseparador_miles.Value = pEstructura.separador_miles;
                        else
                            pseparador_miles.Value = DBNull.Value;
                        pseparador_miles.Direction = ParameterDirection.Input;
                        pseparador_miles.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador_miles);

                        DbParameter ptotalizar = cmdTransaccionFactory.CreateParameter();
                        ptotalizar.ParameterName = "p_totalizar";
                        if (pEstructura.totalizar == null)
                            ptotalizar.Value = DBNull.Value;
                        else
                            ptotalizar.Value = pEstructura.totalizar;
                        ptotalizar.Direction = ParameterDirection.Input;
                        ptotalizar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptotalizar);

                        DbParameter pcod_estructura = cmdTransaccionFactory.CreateParameter();
                        pcod_estructura.ParameterName = "p_cod_estructura";
                        if (pEstructura.cod_estructura == null)
                            pcod_estructura.Value = DBNull.Value;
                        else
                            pcod_estructura.Value = pEstructura.cod_estructura;
                        pcod_estructura.Direction = ParameterDirection.Input;
                        pcod_estructura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_estructura);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_ESTRUCTURA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEstructura;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraRecaudoData", "ModificarEstructuraCarga", ex);
                        return null;
                    }
                }
            }
        }



        public void EliminarEstructuraCarga(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_estructura_carga = cmdTransaccionFactory.CreateParameter();
                        pcod_estructura_carga.ParameterName = "p_cod_estructura_carga";
                        pcod_estructura_carga.Value = pId;
                        pcod_estructura_carga.Direction = ParameterDirection.Input;
                        pcod_estructura_carga.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_estructura_carga);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_ESTRUCTURA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraRecaudoData", "EliminarEstructuraCarga", ex);
                    }
                }
            }
        }

        public Estructura_Carga ConsultarEstructuraCarga(Estructura_Carga pEntidad, Usuario vUsuario)
        {
            DbDataReader resultado;
            Estructura_Carga entidad = new Estructura_Carga();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ESTRUCTURA_CARGA " + ObtenerFiltro(pEntidad);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_ESTRUCTURA_CARGA"] != DBNull.Value) entidad.cod_estructura_carga = Convert.ToInt32(resultado["COD_ESTRUCTURA_CARGA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_ARCHIVO"] != DBNull.Value) entidad.tipo_archivo = Convert.ToInt32(resultado["TIPO_ARCHIVO"]);
                            if (resultado["TIPO_DATOS"] != DBNull.Value) entidad.tipo_datos = Convert.ToInt32(resultado["TIPO_DATOS"]);
                            if (resultado["SEPARADOR_CAMPO"] != DBNull.Value) entidad.separador_campo = Convert.ToString(resultado["SEPARADOR_CAMPO"]);
                            if (resultado["ENCABEZADO"] != DBNull.Value) entidad.encabezado = Convert.ToInt32(resultado["ENCABEZADO"]);
                            if (resultado["FINAL"] != DBNull.Value) entidad.final = Convert.ToInt32(resultado["FINAL"]);
                            if (resultado["FORMATO_FECHA"] != DBNull.Value) entidad.formato_fecha = Convert.ToString(resultado["FORMATO_FECHA"]);
                            if (resultado["SEPARADOR_DECIMAL"] != DBNull.Value) entidad.separador_decimal = Convert.ToString(resultado["SEPARADOR_DECIMAL"]);
                            if (resultado["SEPARADOR_MILES"] != DBNull.Value) entidad.separador_miles = Convert.ToString(resultado["SEPARADOR_MILES"]);
                            if (resultado["TOTALIZAR"] != DBNull.Value) entidad.totalizar = Convert.ToInt32(resultado["TOTALIZAR"]);
                            if (resultado["COD_ESTRUCTURA"] != DBNull.Value) entidad.cod_estructura = Convert.ToInt32(resultado["COD_ESTRUCTURA"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraRecaudoData", "ConsultarEstructuraCarga", ex);
                        return null;
                    }
                }
            }
        }


        public List<Estructura_Carga> ListarEstructuraRecaudo(Estructura_Carga pEstructura, Usuario vUsuario, String filtro,int op)
        {
            DbDataReader resultado;
            List<Estructura_Carga> lstEstructura = new List<Estructura_Carga>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql;
                        if (op == 1)
                        {
                            sql = @"select cod_estructura_carga, descripcion,  " +
                               "   case (tipo_archivo) when 0 then 'Excel' " +
                               "   when 1 then 'Plano' end as tipo_archivo from estructura_carga " + filtro +
                               "   ORDER BY COD_ESTRUCTURA_CARGA";

                        }
                        else
                        {
                            sql = @"select cod_estructura_carga, descripcion,  " +
                              "   case (tipo_archivo) when 0 then 'Excel' " +
                              "   when 1 then 'Plano' end as tipo_archivo from estructura_carga " + filtro +
                              "   ORDER BY COD_ESTRUCTURA_CARGA";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Estructura_Carga entidad = new Estructura_Carga();
                            if (resultado["COD_ESTRUCTURA_CARGA"] != DBNull.Value) entidad.cod_estructura_carga = Convert.ToInt32(resultado["COD_ESTRUCTURA_CARGA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_ARCHIVO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO_ARCHIVO"]);
                            //if (resultado["TIPO_DATOS"] != DBNull.Value) entidad.tipo_datos = Convert.ToInt32(resultado["TIPO_DATOS"]);
                            //if (resultado["SEPARADOR_CAMPO"] != DBNull.Value) entidad.separador_campo = Convert.ToString(resultado["SEPARADOR_CAMPO"]);
                            //if (resultado["ENCABEZADO"] != DBNull.Value) entidad.encabezado = Convert.ToInt32(resultado["ENCABEZADO"]);
                            //if (resultado["FINAL"] != DBNull.Value) entidad.final = Convert.ToInt32(resultado["FINAL"]);
                            //if (resultado["FORMATO_FECHA"] != DBNull.Value) entidad.formato_fecha = Convert.ToString(resultado["FORMATO_FECHA"]);
                            //if (resultado["SEPARADOR_DECIMAL"] != DBNull.Value) entidad.separador_decimal = Convert.ToString(resultado["SEPARADOR_DECIMAL"]);
                            //if (resultado["COD_ESTRUCTURA"] != DBNull.Value) entidad.cod_estructura = Convert.ToInt32(resultado["COD_ESTRUCTURA"]);
                            lstEstructura.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEstructura;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraRecaudoData", "ListarEstructuraRecaudo", ex);
                        return null;
                    }
                }
            }
        }




        public List<Estructura_Carga_Detalle> ListarEstructuraDetalle(Estructura_Carga_Detalle pEstructura,string pOrden, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Estructura_Carga_Detalle> lstEstructura = new List<Estructura_Carga_Detalle>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ESTRUCTURA_CARGA_DETALLE " + ObtenerFiltro(pEstructura) + pOrden;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Estructura_Carga_Detalle entidad = new Estructura_Carga_Detalle();
                            if (resultado["COD_ESTRUCTURA_DETALLE"] != DBNull.Value) entidad.cod_estructura_detalle = Convert.ToInt32(resultado["COD_ESTRUCTURA_DETALLE"]);
                            if (resultado["COD_ESTRUCTURA_CARGA"] != DBNull.Value) entidad.cod_estructura_carga = Convert.ToInt32(resultado["COD_ESTRUCTURA_CARGA"]);
                            if (resultado["CODIGO_CAMPO"] != DBNull.Value) entidad.codigo_campo = Convert.ToInt32(resultado["CODIGO_CAMPO"]);
                            if (resultado["NUMERO_COLUMNA"] != DBNull.Value) entidad.numero_columna = Convert.ToInt32(resultado["NUMERO_COLUMNA"]);
                            if (resultado["POSICION_INICIAL"] != DBNull.Value) entidad.posicion_inicial = Convert.ToInt32(resultado["POSICION_INICIAL"]);
                            if (resultado["LONGITUD"] != DBNull.Value) entidad.longitud = Convert.ToInt32(resultado["LONGITUD"]);
                            if (resultado["JUSTIFICACION"] != DBNull.Value) entidad.justificacion = Convert.ToString(resultado["JUSTIFICACION"]);
                            if (resultado["JUSTIFICADOR"] != DBNull.Value) entidad.justificador = Convert.ToString(resultado["JUSTIFICADOR"]);
                            if (resultado["VR_CAMPO_FIJO"] != DBNull.Value) entidad.vr_campo_fijo = Convert.ToString(resultado["VR_CAMPO_FIJO"]);
                            lstEstructura.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEstructura;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraRecaudoData", "ListarEstructuraDetalle", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarEstructuraDetalle(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_estructura_detalle = cmdTransaccionFactory.CreateParameter();
                        pcod_estructura_detalle.ParameterName = "p_cod_estructura_detalle";
                        pcod_estructura_detalle.Value = pId;
                        pcod_estructura_detalle.Direction = ParameterDirection.Input;
                        pcod_estructura_detalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_estructura_detalle);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_ESTRUCDETA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraRecaudoData", "EliminarEstructuraDetalle", ex);
                    }
                }
            }
        }


        public Estructura_Carga_Detalle CrearEstructuraDetalle(Estructura_Carga_Detalle pDETALLE, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_estructura_detalle = cmdTransaccionFactory.CreateParameter();
                        pcod_estructura_detalle.ParameterName = "p_cod_estructura_detalle";
                        pcod_estructura_detalle.Value = pDETALLE.cod_estructura_detalle;
                        pcod_estructura_detalle.Direction = ParameterDirection.Output;
                        pcod_estructura_detalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_estructura_detalle);

                        DbParameter pcod_estructura_carga = cmdTransaccionFactory.CreateParameter();
                        pcod_estructura_carga.ParameterName = "p_cod_estructura_carga";
                        pcod_estructura_carga.Value = pDETALLE.cod_estructura_carga;
                        pcod_estructura_carga.Direction = ParameterDirection.Input;
                        pcod_estructura_carga.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_estructura_carga);

                        DbParameter pcodigo_campo = cmdTransaccionFactory.CreateParameter();
                        pcodigo_campo.ParameterName = "p_codigo_campo";
                        pcodigo_campo.Value = pDETALLE.codigo_campo;
                        pcodigo_campo.Direction = ParameterDirection.Input;
                        pcodigo_campo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_campo);

                        DbParameter pnumero_columna = cmdTransaccionFactory.CreateParameter();
                        pnumero_columna.ParameterName = "p_numero_columna";
                        if (pDETALLE.numero_columna != null) pnumero_columna.Value = pDETALLE.numero_columna; else pnumero_columna.Value = DBNull.Value;
                        pnumero_columna.Direction = ParameterDirection.Input;
                        pnumero_columna.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_columna);

                        DbParameter pposicion_inicial = cmdTransaccionFactory.CreateParameter();
                        pposicion_inicial.ParameterName = "p_posicion_inicial";
                        if (pDETALLE.posicion_inicial != null) pposicion_inicial.Value = pDETALLE.posicion_inicial; else pposicion_inicial.Value = DBNull.Value;
                        pposicion_inicial.Direction = ParameterDirection.Input;
                        pposicion_inicial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pposicion_inicial);

                        DbParameter plongitud = cmdTransaccionFactory.CreateParameter();
                        plongitud.ParameterName = "p_longitud";
                        if (pDETALLE.longitud != null) plongitud.Value = pDETALLE.longitud; else plongitud.Value = DBNull.Value;
                        plongitud.Direction = ParameterDirection.Input;
                        plongitud.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(plongitud);

                        DbParameter pjustificacion = cmdTransaccionFactory.CreateParameter();
                        pjustificacion.ParameterName = "p_justificacion";
                        pjustificacion.Value = pDETALLE.justificacion;
                        pjustificacion.Direction = ParameterDirection.Input;
                        pjustificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pjustificacion);

                        DbParameter pjustificador = cmdTransaccionFactory.CreateParameter();
                        pjustificador.ParameterName = "p_justificador";
                        if (pDETALLE.justificador != null) pjustificador.Value = pDETALLE.justificador; else pjustificador.Value = DBNull.Value;
                        pjustificador.Direction = ParameterDirection.Input;
                        pjustificador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pjustificador);

                        DbParameter pvr_campo_fijo = cmdTransaccionFactory.CreateParameter();
                        pvr_campo_fijo.ParameterName = "p_vr_campo_fijo";
                        if (pDETALLE.vr_campo_fijo != null) pvr_campo_fijo.Value = pDETALLE.vr_campo_fijo; else pvr_campo_fijo.Value = DBNull.Value;
                        pvr_campo_fijo.Direction = ParameterDirection.Input;
                        pvr_campo_fijo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvr_campo_fijo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_ESTRUCDETA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pDETALLE.cod_estructura_detalle = Convert.ToInt32(pcod_estructura_detalle.Value);
                        return pDETALLE;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraRecaudoData", "CrearEstructuraDetalle", ex);
                        return null;
                    }
                }
            }
        }



        public Estructura_Carga_Detalle ModificarEstructuraDetalle(Estructura_Carga_Detalle pDETALLE, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_estructura_detalle = cmdTransaccionFactory.CreateParameter();
                        pcod_estructura_detalle.ParameterName = "p_cod_estructura_detalle";
                        pcod_estructura_detalle.Value = pDETALLE.cod_estructura_detalle;
                        pcod_estructura_detalle.Direction = ParameterDirection.Input;
                        pcod_estructura_detalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_estructura_detalle);

                        DbParameter pcod_estructura_carga = cmdTransaccionFactory.CreateParameter();
                        pcod_estructura_carga.ParameterName = "p_cod_estructura_carga";
                        pcod_estructura_carga.Value = pDETALLE.cod_estructura_carga;
                        pcod_estructura_carga.Direction = ParameterDirection.Input;
                        pcod_estructura_carga.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_estructura_carga);

                        DbParameter pcodigo_campo = cmdTransaccionFactory.CreateParameter();
                        pcodigo_campo.ParameterName = "p_codigo_campo";
                        pcodigo_campo.Value = pDETALLE.codigo_campo;
                        pcodigo_campo.Direction = ParameterDirection.Input;
                        pcodigo_campo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_campo);

                        DbParameter pnumero_columna = cmdTransaccionFactory.CreateParameter();
                        pnumero_columna.ParameterName = "p_numero_columna";
                        if (pDETALLE.numero_columna != null)
                            pnumero_columna.Value = pDETALLE.numero_columna;
                        else
                            pnumero_columna.Value = DBNull.Value;
                        pnumero_columna.Direction = ParameterDirection.Input;
                        pnumero_columna.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_columna);

                        DbParameter pposicion_inicial = cmdTransaccionFactory.CreateParameter();
                        pposicion_inicial.ParameterName = "p_posicion_inicial";
                        if (pDETALLE.posicion_inicial != null)
                            pposicion_inicial.Value = pDETALLE.posicion_inicial;
                        else
                            pposicion_inicial.Value = DBNull.Value;
                        pposicion_inicial.Direction = ParameterDirection.Input;
                        pposicion_inicial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pposicion_inicial);

                        DbParameter plongitud = cmdTransaccionFactory.CreateParameter();
                        plongitud.ParameterName = "p_longitud";
                        if (pDETALLE.longitud != null)
                            plongitud.Value = pDETALLE.longitud;
                        else
                            plongitud.Value = DBNull.Value;
                        plongitud.Direction = ParameterDirection.Input;
                        plongitud.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(plongitud);

                        DbParameter pjustificacion = cmdTransaccionFactory.CreateParameter();
                        pjustificacion.ParameterName = "p_justificacion";
                        if (pDETALLE.justificacion != null)
                            pjustificacion.Value = pDETALLE.justificacion;
                        else
                            pjustificacion.Value = DBNull.Value;
                        pjustificacion.Direction = ParameterDirection.Input;
                        pjustificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pjustificacion);

                        DbParameter pjustificador = cmdTransaccionFactory.CreateParameter();
                        pjustificador.ParameterName = "p_justificador";
                        if (pDETALLE.justificador != null)
                            pjustificador.Value = pDETALLE.justificador;
                        else
                            pjustificador.Value = DBNull.Value;
                        pjustificador.Direction = ParameterDirection.Input;
                        pjustificador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pjustificador);

                        DbParameter pvr_campo_fijo = cmdTransaccionFactory.CreateParameter();
                        pvr_campo_fijo.ParameterName = "p_vr_campo_fijo";
                        if (pDETALLE.vr_campo_fijo != null) pvr_campo_fijo.Value = pDETALLE.vr_campo_fijo; else pvr_campo_fijo.Value = DBNull.Value;
                        pvr_campo_fijo.Direction = ParameterDirection.Input;
                        pvr_campo_fijo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvr_campo_fijo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_ESTRUCDETA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pDETALLE;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraRecaudoData", "ModificarEstructuraDetalle", ex);
                        return null;
                    }
                }
            }
        }


    }
}




