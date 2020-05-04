using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.ConciliacionBancaria.Entities;
using System.Web;

namespace Xpinn.ConciliacionBancaria.Data
{
    public class EstructuraExtractoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public EstructuraExtractoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public EstructuraExtracto CrearEstructuraCarga(EstructuraExtracto pEstructura, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidestructuraextracto = cmdTransaccionFactory.CreateParameter();
                        pidestructuraextracto.ParameterName = "p_idestructuraextracto";
                        pidestructuraextracto.Value = pEstructura.idestructuraextracto;
                        pidestructuraextracto.Direction = ParameterDirection.Output;
                        pidestructuraextracto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidestructuraextracto);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pEstructura.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pEstructura.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pEstructura.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter ptipo_archivo = cmdTransaccionFactory.CreateParameter();
                        ptipo_archivo.ParameterName = "p_tipo_archivo";
                        ptipo_archivo.Value = pEstructura.tipo_archivo;
                        ptipo_archivo.Direction = ParameterDirection.Input;
                        ptipo_archivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_archivo);

                        DbParameter pdelimitador = cmdTransaccionFactory.CreateParameter();
                        pdelimitador.ParameterName = "p_delimitador";
                        if (pEstructura.delimitador != null) pdelimitador.Value = pEstructura.delimitador; else pdelimitador.Value = DBNull.Value;
                        pdelimitador.Direction = ParameterDirection.Input;
                        pdelimitador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdelimitador);

                        DbParameter pencabezado = cmdTransaccionFactory.CreateParameter();
                        pencabezado.ParameterName = "p_encabezado";
                        if (pEstructura.encabezado != 0) pencabezado.Value = pEstructura.encabezado; else pencabezado.Value = DBNull.Value;
                        pencabezado.Direction = ParameterDirection.Input;
                        pencabezado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pencabezado);

                        DbParameter ptotales = cmdTransaccionFactory.CreateParameter();
                        ptotales.ParameterName = "p_totales";
                        if (pEstructura.totales != 0) ptotales.Value = pEstructura.totales; else ptotales.Value = DBNull.Value;
                        ptotales.Direction = ParameterDirection.Input;
                        ptotales.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptotales);

                        DbParameter pcalificador = cmdTransaccionFactory.CreateParameter();
                        pcalificador.ParameterName = "p_calificador";
                        if (pEstructura.calificador != "-1") pcalificador.Value = pEstructura.calificador; else pcalificador.Value = DBNull.Value;
                        pcalificador.Direction = ParameterDirection.Input;
                        pcalificador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcalificador);

                        DbParameter pseparador_decimal = cmdTransaccionFactory.CreateParameter();
                        pseparador_decimal.ParameterName = "p_separador_decimal";
                        pseparador_decimal.Value = pEstructura.separador_decimal;
                        pseparador_decimal.Direction = ParameterDirection.Input;
                        pseparador_decimal.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador_decimal);

                        DbParameter p_separador_miles = cmdTransaccionFactory.CreateParameter();
                        p_separador_miles.ParameterName = "p_separador_miles";
                        p_separador_miles.Value = pEstructura.separador_miles;
                        p_separador_miles.Direction = ParameterDirection.Input;
                        p_separador_miles.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_separador_miles);

                        DbParameter p_formato_fecha = cmdTransaccionFactory.CreateParameter();
                        p_formato_fecha.ParameterName = "p_formato_fecha";
                        if (pEstructura.formato_fecha != null) p_formato_fecha.Value = pEstructura.formato_fecha; else p_formato_fecha.Value = DBNull.Value;
                        p_formato_fecha.Direction = ParameterDirection.Input;
                        p_formato_fecha.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_formato_fecha);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_EST_EXTRAC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pEstructura.idestructuraextracto = Convert.ToInt32(pidestructuraextracto.Value);
                        return pEstructura;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraExtractoData", "CrearEstructuraCarga", ex);
                        return null;
                    }
                }
            }
        }

        public EstructuraExtracto ModificarEstructuraCarga(EstructuraExtracto pEstructura, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidestructuraextracto = cmdTransaccionFactory.CreateParameter();
                        pidestructuraextracto.ParameterName = "p_idestructuraextracto";
                        pidestructuraextracto.Value = pEstructura.idestructuraextracto;
                        pidestructuraextracto.Direction = ParameterDirection.Input;
                        pidestructuraextracto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidestructuraextracto);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pEstructura.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pEstructura.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pEstructura.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter ptipo_archivo = cmdTransaccionFactory.CreateParameter();
                        ptipo_archivo.ParameterName = "p_tipo_archivo";
                        ptipo_archivo.Value = pEstructura.tipo_archivo;
                        ptipo_archivo.Direction = ParameterDirection.Input;
                        ptipo_archivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_archivo);

                        DbParameter pdelimitador = cmdTransaccionFactory.CreateParameter();
                        pdelimitador.ParameterName = "p_delimitador";
                        if (pEstructura.delimitador != null) pdelimitador.Value = pEstructura.delimitador; else pdelimitador.Value = DBNull.Value;
                        pdelimitador.Direction = ParameterDirection.Input;
                        pdelimitador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdelimitador);

                        DbParameter pencabezado = cmdTransaccionFactory.CreateParameter();
                        pencabezado.ParameterName = "p_encabezado";
                        if (pEstructura.encabezado != 0) pencabezado.Value = pEstructura.encabezado; else pencabezado.Value = DBNull.Value;
                        pencabezado.Direction = ParameterDirection.Input;
                        pencabezado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pencabezado);

                        DbParameter ptotales = cmdTransaccionFactory.CreateParameter();
                        ptotales.ParameterName = "p_totales";
                        if (pEstructura.totales != 0) ptotales.Value = pEstructura.totales; else ptotales.Value = DBNull.Value;
                        ptotales.Direction = ParameterDirection.Input;
                        ptotales.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptotales);

                        DbParameter pcalificador = cmdTransaccionFactory.CreateParameter();
                        pcalificador.ParameterName = "p_calificador";
                        if (pEstructura.calificador != "-1") pcalificador.Value = pEstructura.calificador; else pcalificador.Value = DBNull.Value;
                        pcalificador.Direction = ParameterDirection.Input;
                        pcalificador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcalificador);

                        DbParameter pseparador_decimal = cmdTransaccionFactory.CreateParameter();
                        pseparador_decimal.ParameterName = "p_separador_decimal";
                        pseparador_decimal.Value = pEstructura.separador_decimal;
                        pseparador_decimal.Direction = ParameterDirection.Input;
                        pseparador_decimal.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador_decimal);

                        DbParameter p_separador_miles = cmdTransaccionFactory.CreateParameter();
                        p_separador_miles.ParameterName = "p_separador_miles";
                        p_separador_miles.Value = pEstructura.separador_miles;
                        p_separador_miles.Direction = ParameterDirection.Input;
                        p_separador_miles.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_separador_miles);

                        DbParameter p_formato_fecha = cmdTransaccionFactory.CreateParameter();
                        p_formato_fecha.ParameterName = "p_formato_fecha";
                        if (pEstructura.formato_fecha != null) p_formato_fecha.Value = pEstructura.formato_fecha; else p_formato_fecha.Value = DBNull.Value;
                        p_formato_fecha.Direction = ParameterDirection.Input;
                        p_formato_fecha.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_formato_fecha);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_EST_EXTRAC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEstructura;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraExtractoData", "ModificarEstructuraCarga", ex);
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
                        pcod_estructura_carga.ParameterName = "p_idestructuraextracto";
                        pcod_estructura_carga.Value = pId;
                        pcod_estructura_carga.Direction = ParameterDirection.Input;
                        pcod_estructura_carga.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_estructura_carga);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_EST_EXTRAC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraExtractoData", "EliminarEstructuraCarga", ex);
                    }
                }
            }
        }

        public EstructuraExtracto ConsultarEstructuraCarga(EstructuraExtracto pEntidad, Usuario vUsuario)
        {
            DbDataReader resultado;
            EstructuraExtracto entidad = new EstructuraExtracto();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM estructura_extracto " + ObtenerFiltro(pEntidad);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDESTRUCTURAEXTRACTO"] != DBNull.Value) entidad.idestructuraextracto = Convert.ToInt32(resultado["IDESTRUCTURAEXTRACTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["TIPO_ARCHIVO"] != DBNull.Value) entidad.tipo_archivo = Convert.ToInt32(resultado["TIPO_ARCHIVO"]);
                            if (resultado["DELIMITADOR"] != DBNull.Value) entidad.delimitador = Convert.ToString(resultado["DELIMITADOR"]);
                            if (resultado["ENCABEZADO"] != DBNull.Value) entidad.encabezado = Convert.ToInt32(resultado["ENCABEZADO"]);
                            if (resultado["TOTALES"] != DBNull.Value) entidad.totales = Convert.ToInt32(resultado["TOTALES"]);
                            if (resultado["CALIFICADOR"] != DBNull.Value) entidad.calificador = Convert.ToString(resultado["CALIFICADOR"]);
                            if (resultado["SEPARADOR_DECIMAL"] != DBNull.Value) entidad.separador_decimal = Convert.ToString(resultado["SEPARADOR_DECIMAL"]);
                            if (resultado["SEPARADOR_MILES"] != DBNull.Value) entidad.separador_miles = Convert.ToString(resultado["SEPARADOR_MILES"]);
                            if (resultado["FORMATO_FECHA"] != DBNull.Value) entidad.formato_fecha = Convert.ToString(resultado["FORMATO_FECHA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraExtractoData", "ConsultarEstructuraCarga", ex);
                        return null;
                    }
                }
            }
        }


        public List<EstructuraExtracto> ListarEstructuraExtracto(String filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EstructuraExtracto> lstEstructura = new List<EstructuraExtracto>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select e.idestructuraextracto,e.nombre,e.cod_banco,b.nombrebanco, "
                                        +"case e.tipo_archivo when 0 then 'Excel' when 1 then 'Plano' end as nom_tipoarchivo "
                                        +"from estructura_extracto e left join bancos b on b.cod_banco = e.cod_banco where 1 = 1"
                                        + filtro +" order by 1"; 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EstructuraExtracto entidad = new EstructuraExtracto();
                            if (resultado["idestructuraextracto"] != DBNull.Value) entidad.idestructuraextracto = Convert.ToInt32(resultado["idestructuraextracto"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["cod_banco"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["cod_banco"]);
                            if (resultado["nombrebanco"] != DBNull.Value) entidad.nom_banco = Convert.ToString(resultado["nombrebanco"]);
                            if (resultado["nom_tipoarchivo"] != DBNull.Value) entidad.nom_tipoarchivo = Convert.ToString(resultado["nom_tipoarchivo"]);
                            lstEstructura.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEstructura;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraExtractoData", "ListarEstructuraExtracto", ex);
                        return null;
                    }
                }
            }
        }




        public List<DetEstructuraExtracto> ListarEstructuraDetalle(Int32 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DetEstructuraExtracto> lstEstructura = new List<DetEstructuraExtracto>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from estructura_extracto_detalle where idestructuraextracto = " + pId.ToString() + " order by IDDETESTRUCTURA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DetEstructuraExtracto entidad = new DetEstructuraExtracto();
                            if (resultado["IDDETESTRUCTURA"] != DBNull.Value) entidad.iddetestructura = Convert.ToInt32(resultado["IDDETESTRUCTURA"]);
                            if (resultado["IDESTRUCTURAEXTRACTO"] != DBNull.Value) entidad.idestructuraextracto = Convert.ToInt32(resultado["IDESTRUCTURAEXTRACTO"]);
                            if (resultado["CODIGO_CAMPO"] != DBNull.Value) entidad.codigo_campo = Convert.ToInt32(resultado["CODIGO_CAMPO"]);
                            if (resultado["NUMERO_COLUMNA"] != DBNull.Value) entidad.numero_columna = Convert.ToInt32(resultado["NUMERO_COLUMNA"]);
                            if (resultado["POSICION_INICIAL"] != DBNull.Value) entidad.posicion_inicial = Convert.ToInt32(resultado["POSICION_INICIAL"]);
                            if (resultado["LONGITUD"] != DBNull.Value) entidad.longitud = Convert.ToInt32(resultado["LONGITUD"]);
                            if (resultado["JUSTIFICACION"] != DBNull.Value) entidad.justificacion = Convert.ToInt32(resultado["JUSTIFICACION"]);
                            if (resultado["JUSTIFICADOR"] != DBNull.Value) entidad.justificador = Convert.ToString(resultado["JUSTIFICADOR"]);
                            if (resultado["DECIMALES"] != DBNull.Value) entidad.decimales = Convert.ToInt32(resultado["DECIMALES"]);
                            lstEstructura.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEstructura;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraExtractoData", "ListarEstructuraDetalle", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarEstructuraDetalle(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Delete from estructura_extracto_detalle where IDDETESTRUCTURA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraExtractoData", "EliminarEstructuraDetalle", ex);
                    }
                }
            }
        }

        public DetEstructuraExtracto CrearEstructuraDetalle(DetEstructuraExtracto pDeta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetestructura = cmdTransaccionFactory.CreateParameter();
                        piddetestructura.ParameterName = "p_iddetestructura";
                        piddetestructura.Value = pDeta.iddetestructura;
                        piddetestructura.Direction = ParameterDirection.Output;
                        piddetestructura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetestructura);

                        DbParameter pidestructuraextracto = cmdTransaccionFactory.CreateParameter();
                        pidestructuraextracto.ParameterName = "p_idestructuraextracto";
                        pidestructuraextracto.Value = pDeta.idestructuraextracto;
                        pidestructuraextracto.Direction = ParameterDirection.Input;
                        pidestructuraextracto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidestructuraextracto);

                        DbParameter pcodigo_campo = cmdTransaccionFactory.CreateParameter();
                        pcodigo_campo.ParameterName = "p_codigo_campo";
                        pcodigo_campo.Value = pDeta.codigo_campo;
                        pcodigo_campo.Direction = ParameterDirection.Input;
                        pcodigo_campo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_campo);

                        DbParameter pnumero_columna = cmdTransaccionFactory.CreateParameter();
                        pnumero_columna.ParameterName = "p_numero_columna";
                        if (pDeta.numero_columna != null) pnumero_columna.Value = pDeta.numero_columna; else pnumero_columna.Value = DBNull.Value;
                        pnumero_columna.Direction = ParameterDirection.Input;
                        pnumero_columna.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_columna);

                        DbParameter pposicion_inicial = cmdTransaccionFactory.CreateParameter();
                        pposicion_inicial.ParameterName = "p_posicion_inicial";
                        if (pDeta.posicion_inicial != null) pposicion_inicial.Value = pDeta.posicion_inicial; else pposicion_inicial.Value = DBNull.Value;
                        pposicion_inicial.Direction = ParameterDirection.Input;
                        pposicion_inicial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pposicion_inicial);

                        DbParameter plongitud = cmdTransaccionFactory.CreateParameter();
                        plongitud.ParameterName = "p_longitud";
                        if (pDeta.longitud != null) plongitud.Value = pDeta.longitud; else plongitud.Value = DBNull.Value;
                        plongitud.Direction = ParameterDirection.Input;
                        plongitud.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(plongitud);

                        DbParameter pjustificacion = cmdTransaccionFactory.CreateParameter();
                        pjustificacion.ParameterName = "p_justificacion";
                        pjustificacion.Value = pDeta.justificacion;
                        pjustificacion.Direction = ParameterDirection.Input;
                        pjustificacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pjustificacion);

                        DbParameter pjustificador = cmdTransaccionFactory.CreateParameter();
                        pjustificador.ParameterName = "p_justificador";
                        if (pDeta.justificador != null) pjustificador.Value = pDeta.justificador; else pjustificador.Value = DBNull.Value;
                        pjustificador.Direction = ParameterDirection.Input;
                        pjustificador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pjustificador);

                        DbParameter pdecimales = cmdTransaccionFactory.CreateParameter();
                        pdecimales.ParameterName = "p_decimales";
                        if (pDeta.decimales != null) pdecimales.Value = pDeta.decimales; else pdecimales.Value = DBNull.Value;
                        pdecimales.Direction = ParameterDirection.Input;
                        pdecimales.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdecimales);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_DET_ESTRUC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pDeta.iddetestructura = Convert.ToInt32(piddetestructura.Value);
                        return pDeta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraExtractoData", "CrearEstructuraDetalle", ex);
                        return null;
                    }
                }
            }
        }




        public DetEstructuraExtracto ModificarEstructuraDetalle(DetEstructuraExtracto pDeta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetestructura = cmdTransaccionFactory.CreateParameter();
                        piddetestructura.ParameterName = "p_iddetestructura";
                        piddetestructura.Value = pDeta.iddetestructura;
                        piddetestructura.Direction = ParameterDirection.Input;
                        piddetestructura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetestructura);

                        DbParameter pidestructuraextracto = cmdTransaccionFactory.CreateParameter();
                        pidestructuraextracto.ParameterName = "p_idestructuraextracto";
                        pidestructuraextracto.Value = pDeta.idestructuraextracto;
                        pidestructuraextracto.Direction = ParameterDirection.Input;
                        pidestructuraextracto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidestructuraextracto);

                        DbParameter pcodigo_campo = cmdTransaccionFactory.CreateParameter();
                        pcodigo_campo.ParameterName = "p_codigo_campo";
                        pcodigo_campo.Value = pDeta.codigo_campo;
                        pcodigo_campo.Direction = ParameterDirection.Input;
                        pcodigo_campo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_campo);

                        DbParameter pnumero_columna = cmdTransaccionFactory.CreateParameter();
                        pnumero_columna.ParameterName = "p_numero_columna";
                        if (pDeta.numero_columna != null) pnumero_columna.Value = pDeta.numero_columna; else pnumero_columna.Value = DBNull.Value;
                        pnumero_columna.Direction = ParameterDirection.Input;
                        pnumero_columna.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_columna);

                        DbParameter pposicion_inicial = cmdTransaccionFactory.CreateParameter();
                        pposicion_inicial.ParameterName = "p_posicion_inicial";
                        if (pDeta.posicion_inicial != null) pposicion_inicial.Value = pDeta.posicion_inicial; else pposicion_inicial.Value = DBNull.Value;
                        pposicion_inicial.Direction = ParameterDirection.Input;
                        pposicion_inicial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pposicion_inicial);

                        DbParameter plongitud = cmdTransaccionFactory.CreateParameter();
                        plongitud.ParameterName = "p_longitud";
                        if (pDeta.longitud != null) plongitud.Value = pDeta.longitud; else plongitud.Value = DBNull.Value;
                        plongitud.Direction = ParameterDirection.Input;
                        plongitud.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(plongitud);

                        DbParameter pjustificacion = cmdTransaccionFactory.CreateParameter();
                        pjustificacion.ParameterName = "p_justificacion";
                        pjustificacion.Value = pDeta.justificacion;
                        pjustificacion.Direction = ParameterDirection.Input;
                        pjustificacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pjustificacion);

                        DbParameter pjustificador = cmdTransaccionFactory.CreateParameter();
                        pjustificador.ParameterName = "p_justificador";
                        if (pDeta.justificador != null) pjustificador.Value = pDeta.justificador; else pjustificador.Value = DBNull.Value;
                        pjustificador.Direction = ParameterDirection.Input;
                        pjustificador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pjustificador);

                        DbParameter pdecimales = cmdTransaccionFactory.CreateParameter();
                        pdecimales.ParameterName = "p_decimales";
                        if (pDeta.decimales != null) pdecimales.Value = pDeta.decimales; else pdecimales.Value = DBNull.Value;
                        pdecimales.Direction = ParameterDirection.Input;
                        pdecimales.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdecimales);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_DET_ESTRUC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDeta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstructuraExtractoData", "ModificarEstructuraDetalle", ex);
                        return null;
                    }
                }
            }
        }


    }
}




